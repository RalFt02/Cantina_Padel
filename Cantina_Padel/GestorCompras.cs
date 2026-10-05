using System.Data;
using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    // Ítem de una compra (una fila del detalle).
    public class LineaCompra
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }

    // Toda la lógica de Compras vive acá, separada de los formularios,
    // para poder testear cada método sin abrir ninguna ventana.
    // Los métodos de validación/cálculo no tocan la base; los demás sí.
    public static class GestorCompras
    {
        public const decimal PRECIO_MAXIMO = 99999999.99m;

        // ─────────────────────────────────────────────
        //  VALIDACIONES Y CÁLCULOS (sin base de datos)
        // ─────────────────────────────────────────────

        // Devuelve null si el ítem es válido, o el mensaje de error.
        public static string? ValidarLinea(int idProducto, int cantidad, decimal precioUnitario)
        {
            if (idProducto <= 0)
                return "Seleccioná un producto.";
            if (cantidad <= 0)
                return "La cantidad debe ser mayor a 0.";
            if (precioUnitario <= 0)
                return "El costo unitario debe ser mayor a 0.";
            if (precioUnitario > PRECIO_MAXIMO || cantidad * precioUnitario > PRECIO_MAXIMO)
                return "El importe es demasiado grande.";
            return null;
        }

        // Devuelve null si la compra se puede registrar, o el mensaje de error.
        public static string? ValidarCompra(int idProveedor, List<LineaCompra>? lineas)
        {
            if (idProveedor <= 0)
                return "Seleccioná un proveedor.";
            if (lineas == null || lineas.Count == 0)
                return "Agregá al menos un producto a la compra.";
            return null;
        }

        public static decimal CalcularTotal(IEnumerable<LineaCompra> lineas)
        {
            return lineas.Sum(l => l.Subtotal);
        }

        // Agrega un ítem a la lista. Devuelve null si se agregó, o el mensaje de error.
        public static string? AgregarLinea(List<LineaCompra> lineas, LineaCompra nueva)
        {
            string? error = ValidarLinea(nueva.IdProducto, nueva.Cantidad, nueva.PrecioUnitario);
            if (error != null) return error;

            if (lineas.Any(l => l.IdProducto == nueva.IdProducto))
                return "Ese producto ya está en la compra. Quitalo y volvé a cargarlo si querés cambiar la cantidad o el costo.";

            lineas.Add(nueva);
            return null;
        }

        // Quita un ítem de la lista. Devuelve true si lo encontró.
        public static bool QuitarLinea(List<LineaCompra> lineas, int idProducto)
        {
            return lineas.RemoveAll(l => l.IdProducto == idProducto) > 0;
        }

        // ─────────────────────────────────────────────
        //  ALTA DE COMPRA
        // ─────────────────────────────────────────────

        // Registra la compra, sus ítems (con el total de cada línea) y suma el stock,
        // todo en una sola transacción.
        // Devuelve el id de la compra creada.
        // Lanza ArgumentException si los datos no son válidos.
        public static int CrearCompra(int idProveedor, int idUsuario, List<LineaCompra> lineas)
        {
            string? error = ValidarCompra(idProveedor, lineas);
            if (error != null) throw new ArgumentException(error);

            foreach (LineaCompra linea in lineas)
            {
                error = ValidarLinea(linea.IdProducto, linea.Cantidad, linea.PrecioUnitario);
                if (error != null) throw new ArgumentException(error);
            }

            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            using MySqlTransaction tx = conn.BeginTransaction();

            int idCompra = InsertarCompra(conn, tx, idProveedor, idUsuario);

            foreach (LineaCompra linea in lineas)
            {
                InsertarDetalle(conn, tx, idCompra, linea);
                SumarStock(conn, tx, linea);
            }

            tx.Commit();
            return idCompra;
        }

        private static int InsertarCompra(MySqlConnection conn, MySqlTransaction tx,
                                          int idProveedor, int idUsuario)
        {
            const string query = @"
                INSERT INTO compra (fecha, id_proveedor, id_usuario)
                VALUES (CURDATE(), @prov, @usu);
                SELECT LAST_INSERT_ID();";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@prov", idProveedor);
            cmd.Parameters.AddWithValue("@usu", idUsuario);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private static void InsertarDetalle(MySqlConnection conn, MySqlTransaction tx,
                                            int idCompra, LineaCompra linea)
        {
            const string query = @"
                INSERT INTO detalle_compra (id_compra, id_producto, cantidad, total, precio_unitario)
                VALUES (@compra, @prod, @cant, @total, @precio)";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@cant", linea.Cantidad);
            cmd.Parameters.AddWithValue("@total", linea.Subtotal);
            cmd.Parameters.AddWithValue("@precio", linea.PrecioUnitario);
            cmd.Parameters.AddWithValue("@compra", idCompra);
            cmd.Parameters.AddWithValue("@prod", linea.IdProducto);
            cmd.ExecuteNonQuery();
        }

        private static void SumarStock(MySqlConnection conn, MySqlTransaction tx, LineaCompra linea)
        {
            const string query = @"
                UPDATE producto
                SET producto.stock = producto.stock + @cant
                WHERE producto.id_producto = @prod";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@cant", linea.Cantidad);
            cmd.Parameters.AddWithValue("@prod", linea.IdProducto);

            if (cmd.ExecuteNonQuery() == 0)
                throw new InvalidOperationException($"El producto '{linea.Nombre}' ya no existe.");
        }

        // ─────────────────────────────────────────────
        //  ELIMINACIÓN DE COMPRA
        // ─────────────────────────────────────────────

        // Elimina la compra y su detalle, y descuenta del stock lo que había sumado.
        // Todo en una sola transacción: si algo falla no se borra nada.
        // (La tabla compra no tiene columna activo, por eso el borrado es real.)
        // Lanza InvalidOperationException si la compra no existe o si el stock actual
        // no alcanza para revertirla (porque ya se vendió parte).
        public static void EliminarCompra(int idCompra)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            using MySqlTransaction tx = conn.BeginTransaction();

            if (!CompraExiste(conn, tx, idCompra))
                throw new InvalidOperationException("La compra no existe o ya fue eliminada.");

            foreach (LineaCompra linea in LeerLineasDeCompra(conn, tx, idCompra))
                RestarStock(conn, tx, linea);

            EliminarDetalle(conn, tx, idCompra);
            EliminarCabecera(conn, tx, idCompra);

            tx.Commit();
        }

        private static bool CompraExiste(MySqlConnection conn, MySqlTransaction tx, int idCompra)
        {
            // FOR UPDATE bloquea la fila para que dos eliminaciones simultáneas no resten stock dos veces.
            const string query = @"
                SELECT compra.id_compra
                FROM compra
                WHERE compra.id_compra = @id
                FOR UPDATE";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@id", idCompra);
            object? resultado = cmd.ExecuteScalar();
            return resultado != null && resultado != DBNull.Value;
        }

        private static List<LineaCompra> LeerLineasDeCompra(MySqlConnection conn, MySqlTransaction tx, int idCompra)
        {
            const string query = @"
                SELECT detalle_compra.id_producto,
                       producto.nombre,
                       detalle_compra.cantidad,
                       detalle_compra.precio_unitario
                FROM detalle_compra
                JOIN producto ON producto.id_producto = detalle_compra.id_producto
                WHERE detalle_compra.id_compra = @id";

            List<LineaCompra> lineas = new List<LineaCompra>();
            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@id", idCompra);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lineas.Add(new LineaCompra
                {
                    IdProducto = Convert.ToInt32(reader["id_producto"]),
                    Nombre = reader["nombre"].ToString() ?? "",
                    Cantidad = Convert.ToInt32(reader["cantidad"]),
                    PrecioUnitario = Convert.ToDecimal(reader["precio_unitario"])
                });
            }
            return lineas;
        }

        private static void RestarStock(MySqlConnection conn, MySqlTransaction tx, LineaCompra linea)
        {
            // La condición stock >= cantidad evita que el stock quede negativo.
            const string query = @"
                UPDATE producto
                SET producto.stock = producto.stock - @cant
                WHERE producto.id_producto = @prod
                  AND producto.stock >= @cant";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@cant", linea.Cantidad);
            cmd.Parameters.AddWithValue("@prod", linea.IdProducto);

            if (cmd.ExecuteNonQuery() == 0)
                throw new InvalidOperationException(
                    $"No se puede eliminar: el stock actual de '{linea.Nombre}' es menor a la cantidad de esta compra ({linea.Cantidad}).");
        }

        private static void EliminarDetalle(MySqlConnection conn, MySqlTransaction tx, int idCompra)
        {
            const string query = @"
                DELETE FROM detalle_compra
                WHERE detalle_compra.id_compra = @id";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@id", idCompra);
            cmd.ExecuteNonQuery();
        }

        private static void EliminarCabecera(MySqlConnection conn, MySqlTransaction tx, int idCompra)
        {
            const string query = @"
                DELETE FROM compra
                WHERE compra.id_compra = @id";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@id", idCompra);
            cmd.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────
        //  CONSULTAS
        // ─────────────────────────────────────────────

        public static DataTable ObtenerCompras()
        {
            const string query = @"
                SELECT compra.id_compra,
                       compra.fecha,
                       COALESCE(NULLIF(persona.razon_social, ''), CONCAT(persona.nombre, ' ', persona.apellido)) AS proveedor,
                       COALESCE(SUM(detalle_compra.total), 0) AS total
                FROM compra
                JOIN proveedor ON proveedor.id_proveedor = compra.id_proveedor
                JOIN persona ON persona.id_persona = proveedor.id_persona
                LEFT JOIN detalle_compra ON detalle_compra.id_compra = compra.id_compra
                GROUP BY compra.id_compra, compra.fecha, persona.razon_social, persona.nombre, persona.apellido
                ORDER BY compra.id_compra DESC";

            return LlenarTabla(query);
        }

        public static DataTable ObtenerDetalleCompra(int idCompra)
        {
            const string query = @"
                SELECT producto.nombre,
                       detalle_compra.cantidad,
                       detalle_compra.precio_unitario,
                       detalle_compra.total AS subtotal
                FROM detalle_compra
                JOIN producto ON producto.id_producto = detalle_compra.id_producto
                WHERE detalle_compra.id_compra = @id
                ORDER BY producto.nombre";

            return LlenarTabla(query, ("@id", idCompra));
        }

        public static DataTable ObtenerProveedores()
        {
            const string query = @"
                SELECT proveedor.id_proveedor,
                       COALESCE(NULLIF(persona.razon_social, ''), CONCAT(persona.nombre, ' ', persona.apellido)) AS nombre_proveedor
                FROM proveedor
                JOIN persona ON persona.id_persona = proveedor.id_persona
                WHERE persona.activo = 1
                ORDER BY nombre_proveedor";

            return LlenarTabla(query);
        }

        public static DataTable ObtenerProductosDeProveedor(int idProveedor)
        {
            const string query = @"
                SELECT producto.id_producto,
                       producto.nombre,
                       CONCAT(producto.nombre, ' (stock: ', producto.stock, ')') AS descripcion
                FROM producto
                WHERE producto.id_proveedor = @prov
                  AND producto.activo = 1
                ORDER BY producto.nombre";

            return LlenarTabla(query, ("@prov", idProveedor));
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
