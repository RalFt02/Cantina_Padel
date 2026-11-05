using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormReportesCompras : Form
    {
        // Última tabla generada (es la que se exporta a CSV).
        private DataTable? _reporteActual;

        public FormReportesCompras()
        {
            InitializeComponent();

            ConfigurarGrilla(gridReporte);
            CargarTiposReporte();
            CargarProveedores();
            ConfigurarFechasIniciales();
            ActualizarFiltros();
        }

        // ─────────────────────────────────────────────
        //  CARGA INICIAL
        // ─────────────────────────────────────────────
        private void CargarTiposReporte()
        {
            cmbReporte.DisplayMember = "Texto";
            cmbReporte.ValueMember = "Valor";
            cmbReporte.DataSource = new[]
            {
                new { Texto = "Compras por período", Valor = TipoReporteCompras.PorPeriodo },
                new { Texto = "Compras por proveedor", Valor = TipoReporteCompras.PorProveedor },
                new { Texto = "Productos más comprados", Valor = TipoReporteCompras.PorProducto }
            };
        }

        private void CargarProveedores()
        {
            try
            {
                cmbProveedor.DisplayMember = "nombre";
                cmbProveedor.ValueMember = "id_proveedor";
                cmbProveedor.DataSource = GestorReportesCompras.ObtenerProveedores(true);
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al cargar proveedores:\n" + ex.Message);
            }
        }

        // Por defecto, el mes en curso.
        private void ConfigurarFechasIniciales()
        {
            DateTime hoy = DateTime.Today;
            dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
            dtpHasta.Value = hoy;
        }

        // ─────────────────────────────────────────────
        //  LECTURA DE CONTROLES
        // ─────────────────────────────────────────────
        private TipoReporteCompras ObtenerTipoSeleccionado()
        {
            if (cmbReporte.SelectedValue is TipoReporteCompras tipo)
                return tipo;
            return TipoReporteCompras.PorPeriodo;
        }

        private int? ObtenerIdProveedorFiltro()
        {
            if (cmbProveedor.SelectedValue == null || cmbProveedor.SelectedValue is DataRowView)
                return null;
            return GestorReportesCompras.IdProveedorAFiltro(Convert.ToInt32(cmbProveedor.SelectedValue));
        }

        // El filtro de proveedor no aplica al reporte "por proveedor".
        private void ActualizarFiltros()
        {
            bool admite = GestorReportesCompras.TipoAdmiteFiltroProveedor(ObtenerTipoSeleccionado());
            cmbProveedor.Enabled = admite;
            if (!admite && cmbProveedor.Items.Count > 0)
                cmbProveedor.SelectedIndex = 0;
        }

        // ─────────────────────────────────────────────
        //  GENERAR REPORTE
        // ─────────────────────────────────────────────
        private void GenerarReporte()
        {
            TipoReporteCompras tipo = ObtenerTipoSeleccionado();

            try
            {
                _reporteActual = GestorReportesCompras.GenerarReporte(
                    tipo, dtpDesde.Value, dtpHasta.Value, ObtenerIdProveedorFiltro());

                gridReporte.DataSource = _reporteActual;
                ConfigurarColumnas(tipo);
                MostrarResumen(tipo);
            }
            catch (ArgumentException ex)
            {
                MostrarAviso(ex.Message);
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al generar el reporte:\n" + ex.Message);
            }
        }

        // El resumen siempre se calcula sobre las compras del período (con el mismo filtro),
        // sin importar qué reporte se esté mirando.
        private void MostrarResumen(TipoReporteCompras tipo)
        {
            DataTable compras = tipo == TipoReporteCompras.PorPeriodo && _reporteActual != null
                ? _reporteActual
                : GestorReportesCompras.ObtenerComprasPorPeriodo(dtpDesde.Value, dtpHasta.Value, ObtenerIdProveedorFiltro());

            ResumenCompras resumen = GestorReportesCompras.CalcularResumen(compras);

            lblCantidadCompras.Text = "Compras: " + resumen.CantidadCompras;
            lblUnidades.Text = "Unidades: " + resumen.UnidadesCompradas;
            lblTotal.Text = "Total: " + resumen.Total.ToString("C2");
            lblTotalGanancia.Text = "Total con ganancia: " + resumen.TotalConGanancia.ToString("C2");
            lblPromedio.Text = "Promedio por compra: " + resumen.PromedioPorCompra.ToString("C2");
        }

        private void ConfigurarColumnas(TipoReporteCompras tipo)
        {
            switch (tipo)
            {
                case TipoReporteCompras.PorPeriodo:
                    Columna("id_compra", "N° compra", 80);
                    Columna("fecha", "Fecha", 110, "dd/MM/yyyy");
                    Columna("proveedor", "Proveedor", 220);
                    Columna("unidades", "Unidades", 80);
                    Columna("total", "Total", 120, "C2");
                    Columna("total_con_ganancia", "Total c/ ganancia", 140, "C2");
                    break;

                case TipoReporteCompras.PorProveedor:
                    gridReporte.Columns["id_proveedor"].Visible = false;
                    Columna("proveedor", "Proveedor", 240);
                    Columna("cantidad_compras", "Compras", 80);
                    Columna("unidades", "Unidades", 80);
                    Columna("total", "Total", 120, "C2");
                    Columna("total_con_ganancia", "Total c/ ganancia", 140, "C2");
                    break;

                case TipoReporteCompras.PorProducto:
                    gridReporte.Columns["id_producto"].Visible = false;
                    Columna("nombre", "Producto", 240);
                    Columna("cantidad_compras", "Compras", 80);
                    Columna("unidades", "Unidades", 80);
                    Columna("total", "Total gastado", 120, "C2");
                    Columna("costo_promedio", "Costo promedio", 120, "C2");
                    break;
            }
        }

        private void Columna(string nombre, string titulo, int ancho, string? formato = null)
        {
            gridReporte.Columns[nombre].HeaderText = titulo;
            gridReporte.Columns[nombre].Width = ancho;
            if (formato != null)
                gridReporte.Columns[nombre].DefaultCellStyle.Format = formato;
        }

        // ─────────────────────────────────────────────
        //  EXPORTAR
        // ─────────────────────────────────────────────
        private void ExportarCsv()
        {
            if (_reporteActual == null || _reporteActual.Rows.Count == 0)
            {
                MostrarAviso("Generá un reporte con datos antes de exportar.");
                return;
            }

            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                FileName = $"reporte_compras_{DateTime.Today:yyyyMMdd}.csv"
            };
            if (dialogo.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                GestorReportesCompras.GuardarCsv(_reporteActual, dialogo.FileName);
                MessageBox.Show("Reporte exportado.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MostrarAviso(ex.Message);
            }
            catch (IOException ex)
            {
                MostrarError("No se pudo guardar el archivo:\n" + ex.Message);
            }
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void MostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Mismo estilo oscuro que el resto de los formularios.
        private void ConfigurarGrilla(DataGridView grid)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 65, 85);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(163, 230, 53);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            grid.DefaultCellStyle.ForeColor = Color.White;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(163, 230, 53);
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        // ─────────────────────────────────────────────
        //  EVENTOS (solo delegan en los métodos de arriba)
        // ─────────────────────────────────────────────
        private void cmbReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarFiltros();
        }

        private void Generar_Boton_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void Exportar_Boton_Click(object sender, EventArgs e)
        {
            ExportarCsv();
        }

        private void Volver_Boton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
