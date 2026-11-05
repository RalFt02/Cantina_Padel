using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Cantina_Padel
{
    public partial class FormReportes : Form
    {
        private enum VistaReporte
        {
            Ventas,
            Caja,
            Compras
        }

        private class EstadoPaginacionGrilla
        {
            public DataGridView Grid { get; set; } = null!;
            public DataTable? TablaCompleta { get; set; }
            public int PaginaActual { get; set; }
            public Label LblInfo { get; set; } = null!;
            public Button BtnAnterior { get; set; } = null!;
            public Button BtnSiguiente { get; set; } = null!;
            public Action? FormatearColumnas { get; set; }
        }

        private const int REGISTROS_POR_PAGINA = 15;

        private VistaReporte _vistaActual = VistaReporte.Ventas;

        private DataTable? _dtVentasDetalle;
        private DataTable? _dtVentasCantina;
        private DataTable? _dtVentasPadel;
        private DataTable? _dtVentasDiferenciacion;
        private DataTable? _dtVentasComprobante;
        private DataTable? _dtVentasMedioPago;
        private Dictionary<string, object>? _kpisVentas;

        private DataTable? _dtCajaSesiones;
        private DataTable? _dtCajaCuentaCorriente;
        private DataTable? _dtCajaRetiros;
        private Dictionary<string, object>? _kpisCaja;

        private DataTable? _dtComprasResumen;
        private DataTable? _dtComprasDetalle;
        private Dictionary<string, object>? _kpisCompras;

        private DataTable? _dtDetallesProductoActual;
        private bool _cargandoGrillas = false;

        private readonly Dictionary<DataGridView, EstadoPaginacionGrilla> _paginadores =
            new Dictionary<DataGridView, EstadoPaginacionGrilla>();

        public FormReportes()
        {
            InitializeComponent();
        }

        private void FormReportes_Load(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Today;
            dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
            dtpHasta.Value = hoy;

            ConfigurarEstiloGrillas();
            InicializarBarrasPaginacion();
            VincularEventosSeleccionProducto();

            SeleccionarMenu(VistaReporte.Ventas);
            CargarTodosLosReportes();
        }

        private void ConfigurarEstiloGrillas()
        {
            DataGridView[] grillas = new DataGridView[]
            {
                gridVentasDetalle,
                gridVentasCantina,
                gridVentasPadel,
                gridVentasComprobante,
                gridVentasMedioPago,
                gridCajaSesiones,
                gridCajaCuentaCorriente,
                gridCajaRetiros,
                gridComprasResumen,
                gridComprasDetalle,
                gridItemsComprobante
            };

            foreach (var g in grillas)
            {
                g.EnableHeadersVisualStyles = false;
                g.BackgroundColor = Color.FromArgb(30, 41, 59);
                g.BorderStyle = BorderStyle.None;
                g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                g.GridColor = Color.FromArgb(51, 65, 85);
                g.RowHeadersVisible = false;
                g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                g.MultiSelect = false;
                g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                g.AllowUserToResizeRows = false;
                g.RowTemplate.Height = 26;

                g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                g.ColumnHeadersHeight = 32;
                g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 65, 85);
                g.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(163, 230, 53);
                g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.75F, FontStyle.Bold);
                g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                g.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
                g.DefaultCellStyle.ForeColor = Color.White;
                g.DefaultCellStyle.Font = new Font("Segoe UI", 8.5F);
                g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(163, 230, 53);
                g.DefaultCellStyle.SelectionForeColor = Color.Black;

                g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(24, 34, 53);
                g.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
                g.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(163, 230, 53);
                g.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

        // ═══════════════════════════════════════════════════
        //  PAGINACIÓN (MÁXIMO 15 REGISTROS + FLECHA "-->")
        // ═══════════════════════════════════════════════════

        private void InicializarBarrasPaginacion()
        {
            RegistrarPaginador(gridVentasDetalle, FormatearColumnasVentas);
            RegistrarPaginador(gridVentasCantina, FormatearColumnasVentas);
            RegistrarPaginador(gridVentasPadel, FormatearColumnasVentas);
            RegistrarPaginador(gridVentasComprobante, FormatearColumnasVentas);
            RegistrarPaginador(gridVentasMedioPago, FormatearColumnasVentas);

            RegistrarPaginador(gridCajaSesiones, FormatearColumnasCaja);
            RegistrarPaginador(gridCajaCuentaCorriente, FormatearColumnasCaja);
            RegistrarPaginador(gridCajaRetiros, FormatearColumnasCaja);

            RegistrarPaginador(gridComprasResumen, FormatearColumnasCompras);
            RegistrarPaginador(gridComprasDetalle, FormatearColumnasCompras);
        }

        private void RegistrarPaginador(DataGridView grid, Action formatearColumnas)
        {
            if (grid.Parent == null) return;
            Control contenedor = grid.Parent;

            Panel pnlPaginacion = new Panel
            {
                Height = 34,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(24, 34, 53),
                Padding = new Padding(8, 4, 8, 4)
            };

            Label lblInfo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(8, 8),
                Text = "Mostrando 0 registros"
            };

            FlowLayoutPanel flpBotones = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            Button btnAnterior = new Button
            {
                Text = "<-- Anteriores 15",
                Size = new Size(125, 25),
                BackColor = Color.FromArgb(51, 65, 85),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.25F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Visible = false,
                Margin = new Padding(0, 0, 6, 0)
            };
            btnAnterior.FlatAppearance.BorderSize = 0;

            Button btnSiguiente = new Button
            {
                Text = "Próximos 15 -->",
                Size = new Size(125, 25),
                BackColor = Color.FromArgb(163, 230, 53),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.25F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Visible = false,
                Margin = new Padding(0)
            };
            btnSiguiente.FlatAppearance.BorderSize = 0;

            flpBotones.Controls.Add(btnAnterior);
            flpBotones.Controls.Add(btnSiguiente);

            pnlPaginacion.Controls.Add(flpBotones);
            pnlPaginacion.Controls.Add(lblInfo);

            contenedor.Controls.Add(pnlPaginacion);
            grid.BringToFront();

            var estado = new EstadoPaginacionGrilla
            {
                Grid = grid,
                PaginaActual = 0,
                LblInfo = lblInfo,
                BtnAnterior = btnAnterior,
                BtnSiguiente = btnSiguiente,
                FormatearColumnas = formatearColumnas
            };

            btnAnterior.Click += (s, e) =>
            {
                if (estado.PaginaActual > 0)
                {
                    estado.PaginaActual--;
                    RenderizarPaginaGrilla(estado);
                }
            };

            btnSiguiente.Click += (s, e) =>
            {
                if (estado.TablaCompleta != null)
                {
                    int totalPaginas = (int)Math.Ceiling((double)estado.TablaCompleta.Rows.Count / REGISTROS_POR_PAGINA);
                    if (estado.PaginaActual + 1 < totalPaginas)
                    {
                        estado.PaginaActual++;
                        RenderizarPaginaGrilla(estado);
                    }
                }
            };

            _paginadores[grid] = estado;
        }

        private void AsignarDatosPaginados(DataGridView grid, DataTable? dtCompleto)
        {
            if (!_paginadores.TryGetValue(grid, out var estado))
            {
                grid.DataSource = dtCompleto;
                OcultarColumnasId(grid);
                return;
            }

            estado.TablaCompleta = dtCompleto;
            estado.PaginaActual = 0;
            RenderizarPaginaGrilla(estado);
        }

        private void RenderizarPaginaGrilla(EstadoPaginacionGrilla estado)
        {
            _cargandoGrillas = true;
            try
            {
                DataTable? dt = estado.TablaCompleta;
                if (dt == null || dt.Rows.Count == 0)
                {
                    estado.Grid.DataSource = dt;
                    estado.LblInfo.Text = "Sin registros en este período";
                    estado.BtnAnterior.Visible = false;
                    estado.BtnSiguiente.Visible = false;
                    return;
                }

                int totalRegistros = dt.Rows.Count;
                int totalPaginas = (int)Math.Ceiling((double)totalRegistros / REGISTROS_POR_PAGINA);
                if (estado.PaginaActual >= totalPaginas) estado.PaginaActual = totalPaginas - 1;
                if (estado.PaginaActual < 0) estado.PaginaActual = 0;

                int desdeIdx = estado.PaginaActual * REGISTROS_POR_PAGINA;
                int hastaIdx = Math.Min(desdeIdx + REGISTROS_POR_PAGINA, totalRegistros);

                DataTable dtPagina = dt.Clone();
                for (int i = desdeIdx; i < hastaIdx; i++)
                {
                    dtPagina.ImportRow(dt.Rows[i]);
                }

                estado.Grid.DataSource = dtPagina;
                OcultarColumnasId(estado.Grid);
                estado.FormatearColumnas?.Invoke();

                estado.LblInfo.Text = $"Mostrando {desdeIdx + 1} - {hastaIdx} de {totalRegistros} (Máx. {REGISTROS_POR_PAGINA} por pág.)";
                estado.BtnAnterior.Visible = estado.PaginaActual > 0;
                estado.BtnSiguiente.Visible = (estado.PaginaActual + 1) < totalPaginas;
            }
            finally
            {
                _cargandoGrillas = false;
            }
        }

        private void OcultarColumnasId(DataGridView grid)
        {
            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (col.Name.StartsWith("ID", StringComparison.OrdinalIgnoreCase) ||
                    col.HeaderText.StartsWith("ID", StringComparison.OrdinalIgnoreCase))
                {
                    col.Visible = false;
                }
            }

            if (grid.Columns.Contains("N° Comprobante"))
            {
                grid.Columns["N° Comprobante"].DisplayIndex = 0;
            }
        }

        // ═══════════════════════════════════════════════════
        //  VENTANA LATERAL DE DETALLE DE PRODUCTO
        // ═══════════════════════════════════════════════════

        private void VincularEventosSeleccionProducto()
        {
            gridVentasDetalle.SelectionChanged += (s, e) => MostrarDetalleDesdeGrilla(gridVentasDetalle, esCompra: false);
            gridVentasCantina.SelectionChanged += (s, e) => MostrarDetalleDesdeGrilla(gridVentasCantina, esCompra: false);
            gridVentasPadel.SelectionChanged += (s, e) => MostrarDetalleDesdeGrilla(gridVentasPadel, esCompra: false);
            gridCajaCuentaCorriente.SelectionChanged += (s, e) => MostrarDetalleDesdeGrilla(gridCajaCuentaCorriente, esCompra: false);

            gridComprasResumen.SelectionChanged += (s, e) => MostrarDetalleDesdeGrilla(gridComprasResumen, esCompra: true);
            gridComprasDetalle.SelectionChanged += (s, e) => MostrarDetalleDesdeGrilla(gridComprasDetalle, esCompra: true);

            gridItemsComprobante.SelectionChanged += GridItemsComprobante_SelectionChanged;
        }

        private void MostrarDetalleDesdeGrilla(DataGridView grid, bool esCompra)
        {
            if (_cargandoGrillas) return;
            if (grid.CurrentRow == null || !grid.Columns.Contains("N° Comprobante")) return;

            string? numComp = grid.CurrentRow.Cells["N° Comprobante"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(numComp)) return;

            string? productoFila = null;
            if (grid.Columns.Contains("Producto"))
            {
                productoFila = grid.CurrentRow.Cells["Producto"].Value?.ToString();
            }

            try
            {
                _dtDetallesProductoActual = esCompra
                    ? ReportesService.ObtenerDetallesProductoPorComprobanteCompra(numComp)
                    : ReportesService.ObtenerDetallesProductoPorComprobanteVenta(numComp);

                lblSubDetalleProd.Text = $"Comprobante: {numComp}";

                if (_dtDetallesProductoActual == null || _dtDetallesProductoActual.Rows.Count == 0)
                {
                    LimpiarVentanaDetalleProducto();
                    return;
                }

                // Armar vista reducida para la mini-grilla lateral
                DataTable dtMini = new DataTable();
                dtMini.Columns.Add("Producto", typeof(string));
                dtMini.Columns.Add("Cant.", typeof(int));
                dtMini.Columns.Add("Unitario ($)", typeof(decimal));
                dtMini.Columns.Add("Subtotal ($)", typeof(decimal));

                int filaASeleccionar = 0;
                for (int i = 0; i < _dtDetallesProductoActual.Rows.Count; i++)
                {
                    DataRow r = _dtDetallesProductoActual.Rows[i];
                    string nomProd = r["Producto"]?.ToString() ?? "";
                    dtMini.Rows.Add(
                        nomProd,
                        Convert.ToInt32(r["Cant."]),
                        Convert.ToDecimal(r["Unitario ($)"]),
                        Convert.ToDecimal(r["Subtotal ($)"])
                    );

                    if (!string.IsNullOrWhiteSpace(productoFila) &&
                        string.Equals(nomProd, productoFila, StringComparison.OrdinalIgnoreCase))
                    {
                        filaASeleccionar = i;
                    }
                }

                _cargandoGrillas = true;
                gridItemsComprobante.DataSource = dtMini;
                if (gridItemsComprobante.Columns.Contains("Unitario ($)"))
                {
                    gridItemsComprobante.Columns["Unitario ($)"].DefaultCellStyle.Format = "C2";
                    gridItemsComprobante.Columns["Unitario ($)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (gridItemsComprobante.Columns.Contains("Subtotal ($)"))
                {
                    gridItemsComprobante.Columns["Subtotal ($)"].DefaultCellStyle.Format = "C2";
                    gridItemsComprobante.Columns["Subtotal ($)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                _cargandoGrillas = false;

                if (gridItemsComprobante.Rows.Count > filaASeleccionar)
                {
                    gridItemsComprobante.ClearSelection();
                    gridItemsComprobante.Rows[filaASeleccionar].Selected = true;
                }

                MostrarFichaProducto(_dtDetallesProductoActual.Rows[filaASeleccionar]);
            }
            catch
            {
                _cargandoGrillas = false;
            }
        }

        private void GridItemsComprobante_SelectionChanged(object? sender, EventArgs e)
        {
            if (_cargandoGrillas || _dtDetallesProductoActual == null) return;
            if (gridItemsComprobante.CurrentRow == null) return;

            int idx = gridItemsComprobante.CurrentRow.Index;
            if (idx >= 0 && idx < _dtDetallesProductoActual.Rows.Count)
            {
                MostrarFichaProducto(_dtDetallesProductoActual.Rows[idx]);
            }
        }

        private void MostrarFichaProducto(DataRow r)
        {
            lblDetProductoValor.Text = $"Producto: {r["Producto"]}";
            lblDetCodigoValor.Text = $"Código: {r["Codigo"]}";
            lblDetCategoriaValor.Text = $"Categoría: {r["Categoria"]}";
            lblDetMarcaValor.Text = $"Marca: {r["Marca"]}";
            lblDetProveedorValor.Text = $"Proveedor: {r["Proveedor"]}";
            lblDetCantidadValor.Text = $"Cantidad en operación: {r["Cant."]} und.";
            lblDetPrecioUnitValor.Text = $"Precio / Costo Unit.: ${Convert.ToDecimal(r["Unitario ($)"]):N2}";
            lblDetSubtotalValor.Text = $"Subtotal: ${Convert.ToDecimal(r["Subtotal ($)"]):N2}";
            lblDetPrecioVentaValor.Text = $"Precio Venta Actual: ${Convert.ToDecimal(r["PrecioVentaActual"]):N2}";
            lblDetStockValor.Text = $"Stock Disponible: {r["StockActual"]} und.";
            lblDetGananciaValor.Text = $"% Ganancia Rubro: {r["PorcentajeGanancia"]}%";
            lblDetDescripcionValor.Text = $"Detalle: {r["Descripcion"]}";
        }

        private void LimpiarVentanaDetalleProducto()
        {
            lblDetProductoValor.Text = "Producto: -";
            lblDetCodigoValor.Text = "Código: -";
            lblDetCategoriaValor.Text = "Categoría: -";
            lblDetMarcaValor.Text = "Marca: -";
            lblDetProveedorValor.Text = "Proveedor: -";
            lblDetCantidadValor.Text = "Cantidad en operación: -";
            lblDetPrecioUnitValor.Text = "Precio / Costo Unit.: -";
            lblDetSubtotalValor.Text = "Subtotal: -";
            lblDetPrecioVentaValor.Text = "Precio Venta Actual: -";
            lblDetStockValor.Text = "Stock Disponible: -";
            lblDetGananciaValor.Text = "% Ganancia Rubro: -";
            lblDetDescripcionValor.Text = "Detalle: -";
            gridItemsComprobante.DataSource = null;
        }

        // ═══════════════════════════════════════════════════
        //  CARGA DE REPORTES Y KPIS
        // ═══════════════════════════════════════════════════

        private void CargarTodosLosReportes()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date;

                if (desde > hasta)
                {
                    MessageBox.Show("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.",
                        "Rango de fechas inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Reportes de Ventas (sin Cuenta Corriente, con Cantina y Pádel divididos)
                _dtVentasDetalle = ReportesService.ObtenerReporteVentas(desde, hasta);
                _dtVentasCantina = ReportesService.ObtenerVentasCantina(desde, hasta);
                _dtVentasPadel = ReportesService.ObtenerVentasPadel(desde, hasta);
                _dtVentasDiferenciacion = ReportesService.ObtenerDiferenciacionCanchaCantina(desde, hasta);
                _dtVentasComprobante = ReportesService.ObtenerVentasPorTipoComprobante(desde, hasta);
                _dtVentasMedioPago = ReportesService.ObtenerVentasPorMedioPago(desde, hasta);
                _kpisVentas = ReportesService.ObtenerResumenVentas(desde, hasta);

                ActualizarKpisVentas();
                AsignarDatosPaginados(gridVentasDetalle, _dtVentasDetalle);
                AsignarDatosPaginados(gridVentasCantina, _dtVentasCantina);
                AsignarDatosPaginados(gridVentasPadel, _dtVentasPadel);
                AsignarDatosPaginados(gridVentasComprobante, _dtVentasComprobante);
                AsignarDatosPaginados(gridVentasMedioPago, _dtVentasMedioPago);

                // 2. Reportes de Caja (con desglose de otros medios y Fiado en Cuenta Corriente)
                _dtCajaSesiones = ReportesService.ObtenerReporteCaja(desde, hasta);
                _dtCajaCuentaCorriente = ReportesService.ObtenerDeudasCuentaCorriente(desde, hasta);
                _dtCajaRetiros = ReportesService.ObtenerRetirosCaja(desde, hasta);
                _kpisCaja = ReportesService.ObtenerResumenCaja(desde, hasta);

                ActualizarKpisCaja();
                AsignarDatosPaginados(gridCajaSesiones, _dtCajaSesiones);
                AsignarDatosPaginados(gridCajaCuentaCorriente, _dtCajaCuentaCorriente);
                AsignarDatosPaginados(gridCajaRetiros, _dtCajaRetiros);

                // 3. Reportes de Compras (con Producto y N° Comprobante primero)
                _dtComprasResumen = ReportesService.ObtenerReporteCompras(desde, hasta);
                _dtComprasDetalle = ReportesService.ObtenerDetalleCompras(desde, hasta);
                _kpisCompras = ReportesService.ObtenerResumenCompras(desde, hasta);

                ActualizarKpisCompras();
                AsignarDatosPaginados(gridComprasResumen, _dtComprasResumen);
                AsignarDatosPaginados(gridComprasDetalle, _dtComprasDetalle);

                // Mostrar primer producto en la ventana lateral según la vista activa
                ActualizarDetalleSegunVistaActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los reportes: " + ex.Message,
                    "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ActualizarDetalleSegunVistaActual()
        {
            if (_vistaActual == VistaReporte.Ventas && gridVentasDetalle.Rows.Count > 0)
            {
                MostrarDetalleDesdeGrilla(gridVentasDetalle, esCompra: false);
            }
            else if (_vistaActual == VistaReporte.Compras && gridComprasResumen.Rows.Count > 0)
            {
                MostrarDetalleDesdeGrilla(gridComprasResumen, esCompra: true);
            }
            else if (_vistaActual == VistaReporte.Caja && gridCajaCuentaCorriente.Rows.Count > 0)
            {
                MostrarDetalleDesdeGrilla(gridCajaCuentaCorriente, esCompra: false);
            }
        }

        private void ActualizarKpisVentas()
        {
            pnlKpisVentas.Controls.Clear();
            if (_kpisVentas == null) return;

            int horasPadel = Convert.ToInt32(_kpisVentas["HorasPadel"]);
            decimal totalCancha = Convert.ToDecimal(_kpisVentas["TotalCancha"]);
            decimal totalCantina = Convert.ToDecimal(_kpisVentas["TotalCantina"]);
            decimal gananciaCantina = Convert.ToDecimal(_kpisVentas["GananciaCantina"]);
            decimal totalVentas = Convert.ToDecimal(_kpisVentas["TotalVentas"]);
            decimal gananciaTotal = Convert.ToDecimal(_kpisVentas["GananciaTotal"]);
            int cantVentas = Convert.ToInt32(_kpisVentas["CantidadVentas"]);

            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("HORAS DE PÁDEL", $"{horasPadel} hs", Color.FromArgb(163, 230, 53), 148));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("TOTAL FACTURADO PÁDEL", $"${totalCancha:N2}", Color.FromArgb(163, 230, 53), 155));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("TOTAL CANTINA", $"${totalCantina:N2}", Color.FromArgb(56, 189, 248), 148));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("GANANCIA CANTINA", $"${gananciaCantina:N2}", Color.FromArgb(56, 189, 248), 148));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("TOTAL FACTURADO", $"${totalVentas:N2}", Color.White, 150));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("COMPROBANTES", cantVentas.ToString(), Color.FromArgb(250, 204, 21), 135));

            lblHeaderTablaCantina.Text = $"🍔 CANTINA — Facturado: ${totalCantina:N2} | Ganancia: ${gananciaCantina:N2}";
            lblHeaderTablaPadel.Text = $"🎾 PÁDEL — Horas: {horasPadel} hs | Total Facturado: ${totalCancha:N2}";
        }

        private void ActualizarKpisCaja()
        {
            pnlKpisCaja.Controls.Clear();
            if (_kpisCaja == null) return;

            decimal totalInicio = Convert.ToDecimal(_kpisCaja["TotalInicio"]);
            decimal totalVentasEfvo = Convert.ToDecimal(_kpisCaja["TotalVentasEfectivo"]);
            decimal totalDebito = Convert.ToDecimal(_kpisCaja["TotalTarjetaDebito"]);
            decimal totalCredito = Convert.ToDecimal(_kpisCaja["TotalTarjetaCredito"]);
            decimal totalBilletera = Convert.ToDecimal(_kpisCaja["TotalBilleteraVirtual"]);
            decimal totalDeudaCtaCte = Convert.ToDecimal(_kpisCaja["TotalDeudaCtaCte"]);
            decimal totalRetiros = Convert.ToDecimal(_kpisCaja["TotalRetiros"]);
            decimal totalEsperado = Convert.ToDecimal(_kpisCaja["TotalEsperado"]);

            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("MONTO INICIO", $"${totalInicio:N2}", Color.FromArgb(203, 213, 225), 110));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("EFECTIVO", $"${totalVentasEfvo:N2}", Color.FromArgb(163, 230, 53), 112));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("TARJETA DÉBITO", $"${totalDebito:N2}", Color.FromArgb(56, 189, 248), 115));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("TARJETA CRÉDITO", $"${totalCredito:N2}", Color.FromArgb(56, 189, 248), 115));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("BILLETERA VIRTUAL", $"${totalBilletera:N2}", Color.FromArgb(250, 204, 21), 120));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("FIADO CTA. CTE. (DEBE)", $"${totalDeudaCtaCte:N2}", Color.FromArgb(251, 146, 60), 135));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("RETIROS", $"${totalRetiros:N2}", Color.FromArgb(239, 68, 68), 102));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("EFECTIVO ESPERADO", $"${totalEsperado:N2}", Color.White, 120));
        }

        private void ActualizarKpisCompras()
        {
            pnlKpisCompras.Controls.Clear();
            if (_kpisCompras == null) return;

            decimal totalCompras = Convert.ToDecimal(_kpisCompras["TotalCompras"]);
            int cantidadCompras = Convert.ToInt32(_kpisCompras["CantidadCompras"]);
            int unidades = Convert.ToInt32(_kpisCompras["UnidadesCompradas"]);
            decimal ventaProy = Convert.ToDecimal(_kpisCompras["TotalVentaProyectada"]);
            decimal gananciaProy = Convert.ToDecimal(_kpisCompras["GananciaProyectada"]);
            decimal margen = Convert.ToDecimal(_kpisCompras["MargenPromedio"]);

            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("TOTAL INVERTIDO", $"${totalCompras:N2}", Color.White, 150));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("CANT. COMPRAS", cantidadCompras.ToString(), Color.FromArgb(203, 213, 225), 140));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("UNIDADES STOCK", unidades.ToString(), Color.FromArgb(56, 189, 248), 140));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("VENTA PROYECTADA", $"${ventaProy:N2}", Color.FromArgb(163, 230, 53), 155));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("GANANCIA PROY.", $"${gananciaProy:N2}", Color.FromArgb(250, 204, 21), 150));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("MARGEN PROMEDIO", $"{margen:N2}%", Color.FromArgb(192, 132, 252), 145));
        }

        private Panel CrearTarjetaKpi(string titulo, string valor, Color colorValor, int ancho = 150)
        {
            var pnl = new Panel
            {
                Size = new Size(ancho, 60),
                BackColor = Color.FromArgb(30, 41, 59),
                Margin = new Padding(0, 0, 6, 0)
            };

            var lblT = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 7F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(6, 7),
                Size = new Size(ancho - 12, 14),
                AutoEllipsis = true
            };

            var lblV = new Label
            {
                Text = valor,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = colorValor,
                Location = new Point(6, 24),
                Size = new Size(ancho - 12, 28),
                AutoEllipsis = true
            };

            pnl.Controls.Add(lblT);
            pnl.Controls.Add(lblV);
            return pnl;
        }

        private void FormatearColumnasVentas()
        {
            string[] colsMoneda = { "Cancha Pádel ($)", "Cantina ($)", "Total Venta ($)", "Ganancia Estimada ($)" };
            foreach (string c in colsMoneda)
            {
                if (gridVentasDetalle.Columns.Contains(c))
                {
                    gridVentasDetalle.Columns[c].DefaultCellStyle.Format = "C2";
                    gridVentasDetalle.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            string[] colsCantina = { "Total Cantina ($)", "Ganancia ($)" };
            foreach (string c in colsCantina)
            {
                if (gridVentasCantina.Columns.Contains(c))
                {
                    gridVentasCantina.Columns[c].DefaultCellStyle.Format = "C2";
                    gridVentasCantina.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (gridVentasPadel.Columns.Contains("Total Facturado ($)"))
            {
                gridVentasPadel.Columns["Total Facturado ($)"].DefaultCellStyle.Format = "C2";
                gridVentasPadel.Columns["Total Facturado ($)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            string[] colsComp = { "Subtotal Cancha ($)", "Subtotal Cantina ($)", "Total Facturado ($)", "Ganancia Estimada ($)" };
            foreach (string c in colsComp)
            {
                if (gridVentasComprobante.Columns.Contains(c))
                {
                    gridVentasComprobante.Columns[c].DefaultCellStyle.Format = "C2";
                    gridVentasComprobante.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            string[] colsMedio = { "Total Pádel ($)", "Total Cantina ($)", "Total Cobrado ($)" };
            foreach (string c in colsMedio)
            {
                if (gridVentasMedioPago.Columns.Contains(c))
                {
                    gridVentasMedioPago.Columns[c].DefaultCellStyle.Format = "C2";
                    gridVentasMedioPago.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            if (gridVentasMedioPago.Columns.Contains("Participación (%)"))
            {
                gridVentasMedioPago.Columns["Participación (%)"].DefaultCellStyle.Format = "N2";
                gridVentasMedioPago.Columns["Participación (%)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void FormatearColumnasCaja()
        {
            string[] colsMoneda =
            {
                "Inicio ($)",
                "Ventas Efectivo ($)",
                "Tarjeta Débito ($)",
                "Tarjeta Crédito ($)",
                "Billetera Virtual ($)",
                "Fiado Cta. Cte. ($)",
                "Retiros ($)",
                "Cierre Registrado ($)",
                "Diferencia ($)"
            };
            foreach (string c in colsMoneda)
            {
                if (gridCajaSesiones.Columns.Contains(c))
                {
                    gridCajaSesiones.Columns[c].DefaultCellStyle.Format = "C2";
                    gridCajaSesiones.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            string[] colsCtaCte = { "Importe Fiado ($)", "Deuda Total Cliente ($)" };
            foreach (string c in colsCtaCte)
            {
                if (gridCajaCuentaCorriente.Columns.Contains(c))
                {
                    gridCajaCuentaCorriente.Columns[c].DefaultCellStyle.Format = "C2";
                    gridCajaCuentaCorriente.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (gridCajaRetiros.Columns.Contains("Monto ($)"))
            {
                gridCajaRetiros.Columns["Monto ($)"].DefaultCellStyle.Format = "C2";
                gridCajaRetiros.Columns["Monto ($)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void FormatearColumnasCompras()
        {
            string[] colsMoneda = { "Total Costo ($)", "Venta Proyectada ($)", "Ganancia Proyectada ($)" };
            foreach (string c in colsMoneda)
            {
                if (gridComprasResumen.Columns.Contains(c))
                {
                    gridComprasResumen.Columns[c].DefaultCellStyle.Format = "C2";
                    gridComprasResumen.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            string[] colsDet = { "Costo Unit. ($)", "Subtotal Costo ($)", "Subtotal c/ Ganancia ($)", "Ganancia Estimada ($)" };
            foreach (string c in colsDet)
            {
                if (gridComprasDetalle.Columns.Contains(c))
                {
                    gridComprasDetalle.Columns[c].DefaultCellStyle.Format = "C2";
                    gridComprasDetalle.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        private void SeleccionarMenu(VistaReporte vista)
        {
            _vistaActual = vista;

            btnMenuVentas.BackColor = Color.FromArgb(51, 65, 85);
            btnMenuVentas.ForeColor = Color.White;
            btnMenuCaja.BackColor = Color.FromArgb(51, 65, 85);
            btnMenuCaja.ForeColor = Color.White;
            btnMenuCompras.BackColor = Color.FromArgb(51, 65, 85);
            btnMenuCompras.ForeColor = Color.White;

            pnlReporteVentas.Visible = false;
            pnlReporteCaja.Visible = false;
            pnlReporteCompras.Visible = false;

            switch (vista)
            {
                case VistaReporte.Ventas:
                    btnMenuVentas.BackColor = Color.FromArgb(163, 230, 53);
                    btnMenuVentas.ForeColor = Color.Black;
                    pnlReporteVentas.Visible = true;
                    pnlReporteVentas.BringToFront();
                    break;

                case VistaReporte.Caja:
                    btnMenuCaja.BackColor = Color.FromArgb(163, 230, 53);
                    btnMenuCaja.ForeColor = Color.Black;
                    pnlReporteCaja.Visible = true;
                    pnlReporteCaja.BringToFront();
                    break;

                case VistaReporte.Compras:
                    btnMenuCompras.BackColor = Color.FromArgb(163, 230, 53);
                    btnMenuCompras.ForeColor = Color.Black;
                    pnlReporteCompras.Visible = true;
                    pnlReporteCompras.BringToFront();
                    break;
            }

            ActualizarDetalleSegunVistaActual();
        }

        private void btnMenuVentas_Click(object sender, EventArgs e) => SeleccionarMenu(VistaReporte.Ventas);
        private void btnMenuCaja_Click(object sender, EventArgs e) => SeleccionarMenu(VistaReporte.Caja);
        private void btnMenuCompras_Click(object sender, EventArgs e) => SeleccionarMenu(VistaReporte.Compras);

        private void btnFiltrar_Click(object sender, EventArgs e) => CargarTodosLosReportes();

        private void btnHoy_Click(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;
            CargarTodosLosReportes();
        }

        private void btnEsteMes_Click(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Today;
            dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
            dtpHasta.Value = hoy;
            CargarTodosLosReportes();
        }

        private void btnMesAnterior_Click(object sender, EventArgs e)
        {
            DateTime primerDiaEsteMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime ultimoDiaMesAnt = primerDiaEsteMes.AddDays(-1);
            DateTime primerDiaMesAnt = new DateTime(ultimoDiaMesAnt.Year, ultimoDiaMesAnt.Month, 1);

            dtpDesde.Value = primerDiaMesAnt;
            dtpHasta.Value = ultimoDiaMesAnt;
            CargarTodosLosReportes();
        }

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            dtpDesde.Value = new DateTime(2025, 1, 1);
            dtpHasta.Value = DateTime.Today.AddDays(30);
            CargarTodosLosReportes();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportarVentasExcel_Click(object sender, EventArgs e)
        {
            if (_dtVentasDetalle == null || _dtVentasDetalle.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos de ventas en el período seleccionado para exportar.",
                    "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Libro de Excel (*.xlsx)|*.xlsx|Archivo de valores separados por comas (*.csv)|*.csv",
                FileName = $"Reporte_Ventas_{dtpDesde.Value:yyyyMMdd}_a_{dtpHasta.Value:yyyyMMdd}.xlsx",
                Title = "Guardar Reporte de Ventas en Excel"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string periodo = $"{dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}";
                    var kpisDict = ObtenerDiccionarioKpisVentas();

                    ExportadorService.ExportarAExcel(
                        sfd.FileName,
                        "Reporte de Ventas - Horas de Pádel y Cantina",
                        periodo,
                        kpisDict,
                        _dtVentasDetalle,
                        _dtVentasDiferenciacion,
                        "Detalle de Ventas",
                        "Cantina vs Padel"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error de exportación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportarVentasPdf_Click(object sender, EventArgs e)
        {
            if (_dtVentasDetalle == null || _dtVentasDetalle.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos de ventas en el período seleccionado para exportar.",
                    "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Documento PDF (*.pdf)|*.pdf",
                FileName = $"Reporte_Ventas_{dtpDesde.Value:yyyyMMdd}_a_{dtpHasta.Value:yyyyMMdd}.pdf",
                Title = "Guardar Reporte de Ventas en PDF"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string periodo = $"{dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}";
                    var kpisDict = ObtenerDiccionarioKpisVentas();

                    ExportadorService.ExportarAPdf(
                        sfd.FileName,
                        "Reporte de Ventas - Horas de Pádel y Cantina",
                        periodo,
                        kpisDict,
                        _dtVentasDetalle,
                        _dtVentasDiferenciacion,
                        "Diferenciación Cantina vs. Canchas de Pádel"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a PDF: " + ex.Message, "Error de exportación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Dictionary<string, string> ObtenerDiccionarioKpisVentas()
        {
            var dict = new Dictionary<string, string>();
            if (_kpisVentas == null) return dict;

            dict["Horas de Pádel"] = $"{_kpisVentas["HorasPadel"]} hs";
            dict["Total Facturado Pádel"] = $"${Convert.ToDecimal(_kpisVentas["TotalCancha"]):N2}";
            dict["Total Cantina"] = $"${Convert.ToDecimal(_kpisVentas["TotalCantina"]):N2}";
            dict["Ganancias Cantina"] = $"${Convert.ToDecimal(_kpisVentas["GananciaCantina"]):N2}";
            dict["Total Facturado"] = $"${Convert.ToDecimal(_kpisVentas["TotalVentas"]):N2}";
            dict["Comprobantes Emitidos"] = _kpisVentas["CantidadVentas"].ToString()!;
            return dict;
        }

        private void btnExportarCajaExcel_Click(object sender, EventArgs e)
        {
            if (_dtCajaSesiones == null || _dtCajaSesiones.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos de caja en el período seleccionado para exportar.",
                    "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Libro de Excel (*.xlsx)|*.xlsx|Archivo de valores separados por comas (*.csv)|*.csv",
                FileName = $"Reporte_Caja_{dtpDesde.Value:yyyyMMdd}_a_{dtpHasta.Value:yyyyMMdd}.xlsx",
                Title = "Guardar Reporte de Caja en Excel"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string periodo = $"{dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}";
                    var kpisDict = ObtenerDiccionarioKpisCaja();

                    ExportadorService.ExportarAExcel(
                        sfd.FileName,
                        "Reporte de Sesiones de Caja, Medios de Pago y Cuenta Corriente",
                        periodo,
                        kpisDict,
                        _dtCajaSesiones,
                        _dtCajaCuentaCorriente,
                        "Sesiones de Caja",
                        "Fiado Cta Corriente"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error de exportación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportarCajaPdf_Click(object sender, EventArgs e)
        {
            if (_dtCajaSesiones == null || _dtCajaSesiones.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos de caja en el período seleccionado para exportar.",
                    "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Documento PDF (*.pdf)|*.pdf",
                FileName = $"Reporte_Caja_{dtpDesde.Value:yyyyMMdd}_a_{dtpHasta.Value:yyyyMMdd}.pdf",
                Title = "Guardar Reporte de Caja en PDF"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string periodo = $"{dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}";
                    var kpisDict = ObtenerDiccionarioKpisCaja();

                    ExportadorService.ExportarAPdf(
                        sfd.FileName,
                        "Reporte de Sesiones de Caja, Medios de Pago y Cuenta Corriente",
                        periodo,
                        kpisDict,
                        _dtCajaSesiones,
                        _dtCajaCuentaCorriente,
                        "Deudas en Cuenta Corriente (Lo que se debe)"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a PDF: " + ex.Message, "Error de exportación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Dictionary<string, string> ObtenerDiccionarioKpisCaja()
        {
            var dict = new Dictionary<string, string>();
            if (_kpisCaja == null) return dict;

            dict["Ingresos Efectivo"] = $"${Convert.ToDecimal(_kpisCaja["TotalVentasEfectivo"]):N2}";
            dict["Tarjeta de Débito"] = $"${Convert.ToDecimal(_kpisCaja["TotalTarjetaDebito"]):N2}";
            dict["Tarjeta de Crédito"] = $"${Convert.ToDecimal(_kpisCaja["TotalTarjetaCredito"]):N2}";
            dict["Billetera Virtual"] = $"${Convert.ToDecimal(_kpisCaja["TotalBilleteraVirtual"]):N2}";
            dict["Fiado Cta. Cte. (Se debe)"] = $"${Convert.ToDecimal(_kpisCaja["TotalDeudaCtaCte"]):N2}";
            dict["Efectivo Esperado"] = $"${Convert.ToDecimal(_kpisCaja["TotalEsperado"]):N2}";
            return dict;
        }

        private void btnExportarComprasExcel_Click(object sender, EventArgs e)
        {
            if (_dtComprasResumen == null || _dtComprasResumen.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos de compras en el período seleccionado para exportar.",
                    "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Libro de Excel (*.xlsx)|*.xlsx|Archivo de valores separados por comas (*.csv)|*.csv",
                FileName = $"Reporte_Compras_{dtpDesde.Value:yyyyMMdd}_a_{dtpHasta.Value:yyyyMMdd}.xlsx",
                Title = "Guardar Reporte de Compras en Excel"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string periodo = $"{dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}";
                    var kpisDict = ObtenerDiccionarioKpisCompras();

                    ExportadorService.ExportarAExcel(
                        sfd.FileName,
                        "Reporte de Compras a Proveedores y Productos",
                        periodo,
                        kpisDict,
                        _dtComprasResumen,
                        _dtComprasDetalle,
                        "Resumen de Compras",
                        "Detalle de Productos"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error de exportación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportarComprasPdf_Click(object sender, EventArgs e)
        {
            if (_dtComprasResumen == null || _dtComprasResumen.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos de compras en el período seleccionado para exportar.",
                    "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Documento PDF (*.pdf)|*.pdf",
                FileName = $"Reporte_Compras_{dtpDesde.Value:yyyyMMdd}_a_{dtpHasta.Value:yyyyMMdd}.pdf",
                Title = "Guardar Reporte de Compras en PDF"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string periodo = $"{dtpDesde.Value:dd/MM/yyyy} al {dtpHasta.Value:dd/MM/yyyy}";
                    var kpisDict = ObtenerDiccionarioKpisCompras();

                    ExportadorService.ExportarAPdf(
                        sfd.FileName,
                        "Reporte de Compras a Proveedores y Productos",
                        periodo,
                        kpisDict,
                        _dtComprasResumen,
                        _dtComprasDetalle,
                        "Detalle de Productos Comprados"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a PDF: " + ex.Message, "Error de exportación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Dictionary<string, string> ObtenerDiccionarioKpisCompras()
        {
            var dict = new Dictionary<string, string>();
            if (_kpisCompras == null) return dict;

            dict["Total Invertido"] = $"${Convert.ToDecimal(_kpisCompras["TotalCompras"]):N2}";
            dict["Cantidad de Compras"] = _kpisCompras["CantidadCompras"].ToString()!;
            dict["Unidades de Stock"] = _kpisCompras["UnidadesCompradas"].ToString()!;
            dict["Venta Proyectada"] = $"${Convert.ToDecimal(_kpisCompras["TotalVentaProyectada"]):N2}";
            dict["Ganancia Proyectada"] = $"${Convert.ToDecimal(_kpisCompras["GananciaProyectada"]):N2}";
            dict["Margen Promedio"] = $"{Convert.ToDecimal(_kpisCompras["MargenPromedio"]):N2}%";
            return dict;
        }

        private void NotificarExportacionExitosa(string rutaArchivo)
        {
            DialogResult dr = MessageBox.Show(
                $"Reporte exportado exitosamente a:\n{rutaArchivo}\n\n¿Desea abrir el archivo generado ahora?",
                "Exportación Exitosa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    Process.Start(new ProcessStartInfo(rutaArchivo) { UseShellExecute = true });
                }
                catch { }
            }
        }
    }
}
