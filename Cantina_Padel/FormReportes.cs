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

        private VistaReporte _vistaActual = VistaReporte.Ventas;

        private DataTable? _dtVentasDetalle;
        private DataTable? _dtVentasDiferenciacion;
        private DataTable? _dtVentasComprobante;
        private DataTable? _dtVentasMedioPago;
        private Dictionary<string, object>? _kpisVentas;

        private DataTable? _dtCajaSesiones;
        private DataTable? _dtCajaRetiros;
        private Dictionary<string, object>? _kpisCaja;

        private DataTable? _dtComprasResumen;
        private DataTable? _dtComprasDetalle;
        private Dictionary<string, object>? _kpisCompras;

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
            SeleccionarMenu(VistaReporte.Ventas);
            CargarTodosLosReportes();
        }

        private void ConfigurarEstiloGrillas()
        {
            DataGridView[] grillas = new DataGridView[]
            {
                gridVentasDetalle,
                gridVentasDiferenciacion,
                gridVentasComprobante,
                gridVentasMedioPago,
                gridCajaSesiones,
                gridCajaRetiros,
                gridComprasResumen,
                gridComprasDetalle
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
                g.RowTemplate.Height = 28;

                g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                g.ColumnHeadersHeight = 34;
                g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 65, 85);
                g.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(163, 230, 53);
                g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
                g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                g.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
                g.DefaultCellStyle.ForeColor = Color.White;
                g.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(163, 230, 53);
                g.DefaultCellStyle.SelectionForeColor = Color.Black;

                g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(24, 34, 53);
                g.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
                g.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(163, 230, 53);
                g.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

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

                _dtVentasDetalle = ReportesService.ObtenerReporteVentas(desde, hasta);
                _dtVentasDiferenciacion = ReportesService.ObtenerDiferenciacionCanchaCantina(desde, hasta);
                _dtVentasComprobante = ReportesService.ObtenerVentasPorTipoComprobante(desde, hasta);
                _dtVentasMedioPago = ReportesService.ObtenerVentasPorMedioPago(desde, hasta);
                _kpisVentas = ReportesService.ObtenerResumenVentas(desde, hasta);

                gridVentasDetalle.DataSource = _dtVentasDetalle;
                gridVentasDiferenciacion.DataSource = _dtVentasDiferenciacion;
                gridVentasComprobante.DataSource = _dtVentasComprobante;
                gridVentasMedioPago.DataSource = _dtVentasMedioPago;
                ActualizarKpisVentas();
                FormatearColumnasVentas();

                _dtCajaSesiones = ReportesService.ObtenerReporteCaja(desde, hasta);
                _dtCajaRetiros = ReportesService.ObtenerRetirosCaja(desde, hasta);
                _kpisCaja = ReportesService.ObtenerResumenCaja(desde, hasta);

                gridCajaSesiones.DataSource = _dtCajaSesiones;
                gridCajaRetiros.DataSource = _dtCajaRetiros;
                ActualizarKpisCaja();
                FormatearColumnasCaja();

                _dtComprasResumen = ReportesService.ObtenerReporteCompras(desde, hasta);
                _dtComprasDetalle = ReportesService.ObtenerDetalleCompras(desde, hasta);
                _kpisCompras = ReportesService.ObtenerResumenCompras(desde, hasta);

                gridComprasResumen.DataSource = _dtComprasResumen;
                gridComprasDetalle.DataSource = _dtComprasDetalle;
                ActualizarKpisCompras();
                FormatearColumnasCompras();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los reportes:" + ex.Message,
                    "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ActualizarKpisVentas()
        {
            pnlKpisVentas.Controls.Clear();
            if (_kpisVentas == null) return;

            decimal totalVentas = Convert.ToDecimal(_kpisVentas["TotalVentas"]);
            decimal totalCancha = Convert.ToDecimal(_kpisVentas["TotalCancha"]);
            decimal totalCantina = Convert.ToDecimal(_kpisVentas["TotalCantina"]);
            decimal gananciaTotal = Convert.ToDecimal(_kpisVentas["GananciaTotal"]);
            decimal gananciaCantina = Convert.ToDecimal(_kpisVentas["GananciaCantina"]);
            decimal totalCtaCte = Convert.ToDecimal(_kpisVentas["TotalCtaCte"]);
            int cantVentas = Convert.ToInt32(_kpisVentas["CantidadVentas"]);

            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("TOTAL FACTURADO", $"${totalVentas:N2}", Color.White));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("GANANCIAS CANCHAS PÁDEL", $"${totalCancha:N2}", Color.FromArgb(163, 230, 53)));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("GANANCIAS CANTINA", $"${gananciaCantina:N2}", Color.FromArgb(56, 189, 248)));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("GANANCIA TOTAL ESTIMADA", $"${gananciaTotal:N2}", Color.FromArgb(250, 204, 21)));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("COBROS CUENTA CTE.", $"${totalCtaCte:N2}", Color.FromArgb(192, 132, 252)));
            pnlKpisVentas.Controls.Add(CrearTarjetaKpi("COMPROBANTES EMITIDOS", cantVentas.ToString(), Color.FromArgb(203, 213, 225)));
        }

        private void ActualizarKpisCaja()
        {
            pnlKpisCaja.Controls.Clear();
            if (_kpisCaja == null) return;

            int totalSesiones = Convert.ToInt32(_kpisCaja["TotalSesiones"]);
            decimal totalInicio = Convert.ToDecimal(_kpisCaja["TotalInicio"]);
            decimal totalVentasEfvo = Convert.ToDecimal(_kpisCaja["TotalVentasEfectivo"]);
            decimal totalRetiros = Convert.ToDecimal(_kpisCaja["TotalRetiros"]);
            decimal totalEsperado = Convert.ToDecimal(_kpisCaja["TotalEsperado"]);
            decimal totalDiferencia = Convert.ToDecimal(_kpisCaja["TotalDiferencia"]);

            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("TOTAL SESIONES", totalSesiones.ToString(), Color.White));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("TOTAL MONTO INICIO", $"${totalInicio:N2}", Color.FromArgb(203, 213, 225)));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("INGRESOS EFECTIVO", $"${totalVentasEfvo:N2}", Color.FromArgb(163, 230, 53)));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("RETIROS EFECTIVO", $"${totalRetiros:N2}", Color.FromArgb(239, 68, 68)));
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("EFECTIVO ESPERADO", $"${totalEsperado:N2}", Color.FromArgb(56, 189, 248)));
            Color colDif = totalDiferencia >= 0 ? Color.FromArgb(163, 230, 53) : Color.FromArgb(239, 68, 68);
            pnlKpisCaja.Controls.Add(CrearTarjetaKpi("DIFERENCIA BALANCE", $"${totalDiferencia:N2}", colDif));
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

            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("TOTAL INVERTIDO EN COMPRAS", $"${totalCompras:N2}", Color.White));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("CANTIDAD DE COMPRAS", cantidadCompras.ToString(), Color.FromArgb(203, 213, 225)));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("UNIDADES DE STOCK", unidades.ToString(), Color.FromArgb(56, 189, 248)));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("VENTA PROYECTADA", $"${ventaProy:N2}", Color.FromArgb(163, 230, 53)));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("GANANCIA PROYECTADA", $"${gananciaProy:N2}", Color.FromArgb(250, 204, 21)));
            pnlKpisCompras.Controls.Add(CrearTarjetaKpi("MARGEN PROMEDIO", $"{margen:N2}%", Color.FromArgb(192, 132, 252)));
        }

        private Panel CrearTarjetaKpi(string titulo, string valor, Color colorValor)
        {
            var pnl = new Panel
            {
                Size = new Size(198, 60),
                BackColor = Color.FromArgb(30, 41, 59),
                Margin = new Padding(0, 0, 10, 0)
            };

            var lblT = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 7.25F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(8, 7),
                Size = new Size(182, 14),
                AutoEllipsis = true
            };

            var lblV = new Label
            {
                Text = valor,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = colorValor,
                Location = new Point(8, 24),
                Size = new Size(182, 28),
                AutoEllipsis = true
            };

            pnl.Controls.Add(lblT);
            pnl.Controls.Add(lblV);
            return pnl;
        }

        private void FormatearColumnasVentas()
        {
            string[] colsMoneda = { "Cancha Pádel ($)", "Cantina ($)", "Cobro Cta Cte ($)", "Total Venta ($)", "Ganancia Estimada ($)" };
            foreach (string c in colsMoneda)
            {
                if (gridVentasDetalle.Columns.Contains(c))
                {
                    gridVentasDetalle.Columns[c].DefaultCellStyle.Format = "C2";
                    gridVentasDetalle.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (gridVentasDiferenciacion.Columns.Contains("Ingresos ($)"))
            {
                gridVentasDiferenciacion.Columns["Ingresos ($)"].DefaultCellStyle.Format = "C2";
                gridVentasDiferenciacion.Columns["Ingresos ($)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (gridVentasDiferenciacion.Columns.Contains("Costo Mercadería ($)"))
            {
                gridVentasDiferenciacion.Columns["Costo Mercadería ($)"].DefaultCellStyle.Format = "C2";
                gridVentasDiferenciacion.Columns["Costo Mercadería ($)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (gridVentasDiferenciacion.Columns.Contains("Ganancia Neta ($)"))
            {
                gridVentasDiferenciacion.Columns["Ganancia Neta ($)"].DefaultCellStyle.Format = "C2";
                gridVentasDiferenciacion.Columns["Ganancia Neta ($)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (gridVentasDiferenciacion.Columns.Contains("Margen (%)"))
            {
                gridVentasDiferenciacion.Columns["Margen (%)"].DefaultCellStyle.Format = "N2";
                gridVentasDiferenciacion.Columns["Margen (%)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            string[] colsComp = { "Subtotal Cancha ($)", "Subtotal Cantina ($)", "Subtotal Cta Cte ($)", "Total Facturado ($)", "Ganancia Estimada ($)" };
            foreach (string c in colsComp)
            {
                if (gridVentasComprobante.Columns.Contains(c))
                {
                    gridVentasComprobante.Columns[c].DefaultCellStyle.Format = "C2";
                    gridVentasComprobante.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (gridVentasMedioPago.Columns.Contains("Total Cobrado ($)"))
            {
                gridVentasMedioPago.Columns["Total Cobrado ($)"].DefaultCellStyle.Format = "C2";
                gridVentasMedioPago.Columns["Total Cobrado ($)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (gridVentasMedioPago.Columns.Contains("Participación (%)"))
            {
                gridVentasMedioPago.Columns["Participación (%)"].DefaultCellStyle.Format = "N2";
                gridVentasMedioPago.Columns["Participación (%)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void FormatearColumnasCaja()
        {
            string[] colsMoneda = { "Inicio ($)", "Ventas Efectivo ($)", "Otros Medios ($)", "Retiros ($)", "Cierre Registrado ($)", "Diferencia ($)" };
            foreach (string c in colsMoneda)
            {
                if (gridCajaSesiones.Columns.Contains(c))
                {
                    gridCajaSesiones.Columns[c].DefaultCellStyle.Format = "C2";
                    gridCajaSesiones.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                        "Reporte de Ventas - Diferenciación Cancha y Cantina",
                        periodo,
                        kpisDict,
                        _dtVentasDetalle,
                        _dtVentasDiferenciacion,
                        "Detalle de Ventas",
                        "Cancha vs Cantina"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel:" + ex.Message, "Error de exportación",
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
                        "Reporte de Ventas - Diferenciación Cancha y Cantina",
                        periodo,
                        kpisDict,
                        _dtVentasDetalle,
                        _dtVentasDiferenciacion,
                        "Diferenciación Canchas vs. Cantina"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a PDF:" + ex.Message, "Error de exportación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Dictionary<string, string> ObtenerDiccionarioKpisVentas()
        {
            var dict = new Dictionary<string, string>();
            if (_kpisVentas == null) return dict;

            dict["Total Facturado"] = $"${Convert.ToDecimal(_kpisVentas["TotalVentas"]):N2}";
            dict["Ganancias Canchas Pádel"] = $"${Convert.ToDecimal(_kpisVentas["TotalCancha"]):N2}";
            dict["Ganancias Cantina"] = $"${Convert.ToDecimal(_kpisVentas["GananciaCantina"]):N2}";
            dict["Ganancia Total Estimada"] = $"${Convert.ToDecimal(_kpisVentas["GananciaTotal"]):N2}";
            dict["Cobros Cuenta Corriente"] = $"${Convert.ToDecimal(_kpisVentas["TotalCtaCte"]):N2}";
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
                        "Reporte de Sesiones de Caja y Movimientos",
                        periodo,
                        kpisDict,
                        _dtCajaSesiones,
                        _dtCajaRetiros,
                        "Sesiones de Caja",
                        "Retiros de Efectivo"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel:" + ex.Message, "Error de exportación",
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
                        "Reporte de Sesiones de Caja y Movimientos",
                        periodo,
                        kpisDict,
                        _dtCajaSesiones,
                        _dtCajaRetiros,
                        "Retiros de Efectivo"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a PDF:" + ex.Message, "Error de exportación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Dictionary<string, string> ObtenerDiccionarioKpisCaja()
        {
            var dict = new Dictionary<string, string>();
            if (_kpisCaja == null) return dict;

            dict["Total Sesiones"] = _kpisCaja["TotalSesiones"].ToString()!;
            dict["Total Monto Inicio"] = $"${Convert.ToDecimal(_kpisCaja["TotalInicio"]):N2}";
            dict["Ingresos Efectivo"] = $"${Convert.ToDecimal(_kpisCaja["TotalVentasEfectivo"]):N2}";
            dict["Retiros Efectivo"] = $"${Convert.ToDecimal(_kpisCaja["TotalRetiros"]):N2}";
            dict["Efectivo Esperado"] = $"${Convert.ToDecimal(_kpisCaja["TotalEsperado"]):N2}";
            dict["Diferencia Balance"] = $"${Convert.ToDecimal(_kpisCaja["TotalDiferencia"]):N2}";
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
                        "Reporte de Compras a Proveedores y Stock",
                        periodo,
                        kpisDict,
                        _dtComprasResumen,
                        _dtComprasDetalle,
                        "Resumen de Compras",
                        "Detalle de Ítems"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel:" + ex.Message, "Error de exportación",
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
                        "Reporte de Compras a Proveedores y Stock",
                        periodo,
                        kpisDict,
                        _dtComprasResumen,
                        _dtComprasDetalle,
                        "Detalle de Ítems Comprados"
                    );

                    NotificarExportacionExitosa(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a PDF:" + ex.Message, "Error de exportación",
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
                $"Reporte exportado exitosamente a:{rutaArchivo}¿Desea abrir el archivo generado ahora?",
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
