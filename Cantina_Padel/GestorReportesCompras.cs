using System.Data;
using System.Globalization;
using System.Text;
using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public enum TipoReporteCompras
    {
        PorPeriodo,
        PorProveedor,
        PorProducto
    }

    // Totales que se muestran arriba de la grilla del reporte.
    public class ResumenCompras
    {
        public int CantidadCompras { get; set; }
        public int UnidadesCompradas { get; set; }
        public decimal Total { get; set; }
        public decimal TotalConGanancia { get; set; }
        public decimal PromedioPorCompra => CantidadCompras == 0 ? 0 : Math.Round(Total / CantidadCompras, 2);
    }

    // Toda la lógica de los reportes de Compras vive acá, separada del formulario,
    // para poder testear cada método sin abrir ninguna ventana.
    // Los métodos de validación/cálculo/CSV no tocan la base; los de consulta sí.
    public static class GestorReportesCompras
    {
        // ─────────────────────────────────────────────
        //  VALIDACIONES Y CÁLCULOS (sin base de datos)
        // ─────────────────────────────────────────────

        // Devuelve null si el rango es válido, o el mensaje de error.
        public static string? ValidarRango(DateTime desde, DateTime hasta)
        {
            if (desde.Date > hasta.Date)
                return "La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.";
            return null;
        }

        // La fecha 'Hasta' es inclusiva: la consulta usa fecha < (hasta + 1 día),
        // así entran las compras de todo el día aunque la columna tenga hora.
        public static DateTime CalcularLimiteSuperior(DateTime hasta)
        {
            return hasta.Date.AddDays(1);
        }

        // El combo "Todos los proveedores" tiene id 0: eso significa sin filtro.
        public static int? IdProveedorAFiltro(int idProveedorSeleccionado)
        {
            return idProveedorSeleccionado > 0 ? idProveedorSeleccionado : null;
        }

        // El reporte por proveedor ya agrupa por proveedor, no admite filtrar por uno.
        public static bool TipoAdmiteFiltroProveedor(TipoReporteCompras tipo)
        {
            return tipo != TipoReporteCompras.PorProveedor;
        }

        // Arma el resumen a partir de la tabla del reporte "por período"
        // (una fila por compra, con las columnas unidades, total y total_con_ganancia).
        public static ResumenCompras CalcularResumen(DataTable comprasPeriodo)
        {
            ResumenCompras resumen = new ResumenCompras();

            foreach (DataRow fila in comprasPeriodo.Rows)
            {
                resumen.CantidadCompras++;
                resumen.UnidadesCompradas += Convert.ToInt32(fila["unidades"]);
                resumen.Total += Convert.ToDecimal(fila["total"]);
                resumen.TotalConGanancia += Convert.ToDecimal(fila["total_con_ganancia"]);
            }

            return resumen;
        }

        // ─────────────────────────────────────────────
        //  EXPORTACIÓN A CSV (sin base de datos)
        // ─────────────────────────────────────────────

        // Encierra el valor entre comillas si tiene ; " o salto de línea.
        public static string EscaparCsv(string? valor)
        {
            if (string.IsNullOrEmpty(valor)) return "";

            if (valor.Contains(';') || valor.Contains('"') || valor.Contains('\n') || valor.Contains('\r'))
                return "\"" + valor.Replace("\"", "\"\"") + "\"";

            return valor;
        }

        // Separador ';' (el que abre bien Excel con configuración regional argentina).
        // Decimales con coma, fechas dd/MM/yyyy.
        public static string ConvertirACsv(DataTable tabla)
        {
            StringBuilder sb = new StringBuilder();
            CultureInfo cultura = new CultureInfo("es-AR");

            sb.AppendLine(string.Join(";", tabla.Columns.Cast<DataColumn>().Select(c => EscaparCsv(c.ColumnName))));

            foreach (DataRow fila in tabla.Rows)
            {
                IEnumerable<string> celdas = tabla.Columns.Cast<DataColumn>()
                    .Select(c => EscaparCsv(FormatearCelda(fila[c], cultura)));
                sb.AppendLine(string.Join(";", celdas));
            }

            return sb.ToString();
        }

        private static string FormatearCelda(object valor, CultureInfo cultura)
        {
            if (valor == null || valor == DBNull.Value) return "";
            if (valor is DateTime fecha) return fecha.ToString("dd/MM/yyyy", cultura);
            if (valor is decimal dec) return dec.ToString("0.00", cultura);
            if (valor is IFormattable formateable) return formateable.ToString(null, cultura);
            return valor.ToString() ?? "";
        }

        // Guarda el CSV en la ruta indicada (UTF-8 con BOM para que Excel respete los acentos).
        // Lanza ArgumentException si no hay ruta o la tabla está vacía.
        public static void GuardarCsv(DataTable tabla, string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new ArgumentException("Indicá dónde guardar el archivo.");
            if (tabla == null || tabla.Rows.Count == 0)
                throw new ArgumentException("No hay datos para exportar.");

            File.WriteAllText(ruta, ConvertirACsv(tabla), new UTF8Encoding(true));
        }

        // ─────────────────────────────────────────────
        //  CONSULTAS
        // ─────────────────────────────────────────────

        // Devuelve el reporte pedido. Lanza ArgumentException si el rango no es válido.
        public static DataTable GenerarReporte(TipoReporteCompras tipo, DateTime desde, DateTime hasta, int? idProveedor)
        {
            string? error = ValidarRango(desde, hasta);
            if (error != null) throw new ArgumentException(error);

            switch (tipo)
            {
                case TipoReporteCompras.PorPeriodo:
                    return ObtenerComprasPorPeriodo(desde, hasta, idProveedor);
                case TipoReporteCompras.PorProveedor:
                    return ObtenerComprasPorProveedor(desde, hasta);
                case TipoReporteCompras.PorProducto:
                    return ObtenerProductosComprados(desde, hasta, idProveedor);
                default:
                    throw new ArgumentException("Tipo de reporte desconocido.");
            }
        }

        // Proveedores para el combo de filtro. Con incluirTodos agrega la fila "Todos" (id 0).
        public static DataTable ObtenerProveedores(bool incluirTodos = true)
        {
            const string query = @"
                SELECT proveedor.id_proveedor,
                       COALESCE(NULLIF(persona.razon_social, ''), CONCAT(persona.nombre, ' ', persona.apellido)) AS nombre
                FROM proveedor
                JOIN persona ON persona.id_persona = proveedor.id_persona
                ORDER BY nombre";

            DataTable dt = LlenarTabla(query);

            if (incluirTodos)
            {
                DataRow todos = dt.NewRow();
                todos["id_proveedor"] = 0;
                todos["nombre"] = "Todos los proveedores";
                dt.Rows.InsertAt(todos, 0);
            }

            return dt;
        }

        // Una fila por compra del período.
        public static DataTable ObtenerComprasPorPeriodo(DateTime desde, DateTime hasta, int? idProveedor)
        {
            const string query = @"
                SELECT compra.id_compra,
                       compra.fecha,
                       COALESCE(NULLIF(persona.razon_social, ''), CONCAT(persona.nombre, ' ', persona.apellido)) AS proveedor,
                       COALESCE(SUM(detalle_compra.cantidad), 0) AS unidades,
                       COALESCE(SUM(detalle_compra.total), 0) AS total,
                       COALESCE(SUM(detalle_compra.total * (1 + categoria.porcentaje_ganancia / 100)), 0) AS total_con_ganancia
                FROM compra
                JOIN proveedor ON proveedor.id_proveedor = compra.id_proveedor
                JOIN persona ON persona.id_persona = proveedor.id_persona
                LEFT JOIN detalle_compra ON detalle_compra.id_compra = compra.id_compra
                LEFT JOIN producto ON producto.id_producto = detalle_compra.id_producto
                LEFT JOIN categoria ON categoria.id_categoria = producto.id_categoria
                WHERE compra.fecha >= @desde
                  AND compra.fecha < @hasta
                  AND (@prov IS NULL OR compra.id_proveedor = @prov)
                GROUP BY compra.id_compra, compra.fecha, persona.razon_social, persona.nombre, persona.apellido
                ORDER BY compra.fecha DESC, compra.id_compra DESC";

            return LlenarTabla(query,
                ("@desde", desde.Date),
                ("@hasta", CalcularLimiteSuperior(hasta)),
                ("@prov", (object?)idProveedor ?? DBNull.Value));
        }

        // Una fila por proveedor: cuántas compras se le hicieron y cuánto se gastó.
        public static DataTable ObtenerComprasPorProveedor(DateTime desde, DateTime hasta)
        {
            const string query = @"
                SELECT compra.id_proveedor,
                       COALESCE(NULLIF(persona.razon_social, ''), CONCAT(persona.nombre, ' ', persona.apellido)) AS proveedor,
                       COUNT(DISTINCT compra.id_compra) AS cantidad_compras,
                       COALESCE(SUM(detalle_compra.cantidad), 0) AS unidades,
                       COALESCE(SUM(detalle_compra.total), 0) AS total,
                       COALESCE(SUM(detalle_compra.total * (1 + categoria.porcentaje_ganancia / 100)), 0) AS total_con_ganancia
                FROM compra
                JOIN proveedor ON proveedor.id_proveedor = compra.id_proveedor
                JOIN persona ON persona.id_persona = proveedor.id_persona
                LEFT JOIN detalle_compra ON detalle_compra.id_compra = compra.id_compra
                LEFT JOIN producto ON producto.id_producto = detalle_compra.id_producto
                LEFT JOIN categoria ON categoria.id_categoria = producto.id_categoria
                WHERE compra.fecha >= @desde
                  AND compra.fecha < @hasta
                GROUP BY compra.id_proveedor, persona.razon_social, persona.nombre, persona.apellido
                ORDER BY total DESC";

            return LlenarTabla(query,
                ("@desde", desde.Date),
                ("@hasta", CalcularLimiteSuperior(hasta)));
        }

        // Una fila por producto: unidades compradas y gasto, de lo más comprado a lo menos.
        public static DataTable ObtenerProductosComprados(DateTime desde, DateTime hasta, int? idProveedor)
        {
            const string query = @"
                SELECT producto.id_producto,
                       producto.nombre,
                       COUNT(DISTINCT compra.id_compra) AS cantidad_compras,
                       SUM(detalle_compra.cantidad) AS unidades,
                       SUM(detalle_compra.total) AS total,
                       SUM(detalle_compra.total) / SUM(detalle_compra.cantidad) AS costo_promedio
                FROM detalle_compra
                JOIN compra ON compra.id_compra = detalle_compra.id_compra
                JOIN producto ON producto.id_producto = detalle_compra.id_producto
                WHERE compra.fecha >= @desde
                  AND compra.fecha < @hasta
                  AND (@prov IS NULL OR compra.id_proveedor = @prov)
                GROUP BY producto.id_producto, producto.nombre
                ORDER BY unidades DESC, producto.nombre";

            return LlenarTabla(query,
                ("@desde", desde.Date),
                ("@hasta", CalcularLimiteSuperior(hasta)),
                ("@prov", (object?)idProveedor ?? DBNull.Value));
        }

        private static DataTable LlenarTabla(string query, params (string nombre, object valor)[] parametros)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            foreach (var (nombre, valor) in parametros)
                cmd.Parameters.AddWithValue(nombre, valor);

            DataTable dt = new DataTable();
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}
