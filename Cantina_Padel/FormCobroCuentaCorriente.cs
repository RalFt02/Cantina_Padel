using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Cantina_Padel
{
    public partial class FormCobroCuentaCorriente : Form
    {
        private readonly int _idUsuario;
        private int? _idClienteSeleccionado;
        private decimal _saldoDeudorActual = 0m;
        private decimal _saldoFavorActual = 0m;
        private DatosComprobante? _ultimoComprobante;

        public FormCobroCuentaCorriente(int idUsuario, int? idClienteInicial = null)
        {
            _idUsuario = idUsuario > 0 ? idUsuario : 1;
            int? idSesion = GestorVentas.ObtenerIdSesionCajaAbierta(_idUsuario);
            if (!idSesion.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Load += (s, e) => this.Close();
                return;
            }

            InitializeComponent();
            _idClienteSeleccionado = idClienteInicial;

            // El botón de impresión manual se quita de Cuenta Corriente (se mantiene el desplegable de comprobante)
            btnImprimir.Visible = false;

            ConfigurarGrilla(gridMovimientos);
            CargarMediosDePago();
            CargarComprobantes();
            CargarClientes();

            if (_idClienteSeleccionado.HasValue)
            {
                cmbCliente.SelectedValue = _idClienteSeleccionado.Value;
            }
        }

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

        private void CargarMediosDePago()
        {
            cmbMedioPago.Items.Clear();
            cmbMedioPago.Items.Add("Efectivo");
            cmbMedioPago.Items.Add("Tarjeta de Débito");
            cmbMedioPago.Items.Add("Tarjeta de Crédito");
            cmbMedioPago.Items.Add("Billetera Virtual");
            cmbMedioPago.SelectedIndex = 0;
        }

        private void CargarComprobantes()
        {
            cmbComprobante.Items.Clear();
            cmbComprobante.Items.Add("Ticket");
            cmbComprobante.Items.Add("Factura B");
            cmbComprobante.Items.Add("Factura C");
            cmbComprobante.Items.Add("Factura A");
            cmbComprobante.SelectedIndex = 0;
        }

        private void ActualizarComprobantesSegunCliente()
        {
            string condicion = "Consumidor Final";
            string razonSocial = "";

            if (cmbCliente.SelectedItem is DataRowView drv)
            {
                if (drv.Row.Table.Columns.Contains("condicion_fiscal") && drv["condicion_fiscal"] != DBNull.Value)
                    condicion = drv["condicion_fiscal"].ToString() ?? "Consumidor Final";
                if (drv.Row.Table.Columns.Contains("razon_social") && drv["razon_social"] != DBNull.Value)
                    razonSocial = drv["razon_social"].ToString() ?? "";
            }

            if (string.IsNullOrWhiteSpace(condicion) && !string.IsNullOrWhiteSpace(razonSocial))
            {
                condicion = "Responsable Inscripto";
            }

            string comprobanteActual = cmbComprobante.SelectedItem?.ToString() ?? "";
            cmbComprobante.Items.Clear();

            if (condicion.Equals("Responsable Inscripto", StringComparison.OrdinalIgnoreCase) || !string.IsNullOrWhiteSpace(razonSocial))
            {
                cmbComprobante.Items.Add("Factura A");
                cmbComprobante.Items.Add("Factura B");
                cmbComprobante.Items.Add("Factura C");
                cmbComprobante.Items.Add("Ticket");
            }
            else if (condicion.Equals("Monotributista", StringComparison.OrdinalIgnoreCase))
            {
                cmbComprobante.Items.Add("Factura B");
                cmbComprobante.Items.Add("Factura C");
                cmbComprobante.Items.Add("Factura A");
                cmbComprobante.Items.Add("Ticket");
            }
            else // Consumidor Final
            {
                cmbComprobante.Items.Add("Ticket");
                cmbComprobante.Items.Add("Factura B");
                cmbComprobante.Items.Add("Factura C");
            }

            int idx = cmbComprobante.Items.IndexOf(comprobanteActual);
            cmbComprobante.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private string ObtenerFacturaSeleccionadaOSegunCliente()
        {
            string seleccionado = cmbComprobante.SelectedItem?.ToString() ?? "";
            if (seleccionado.StartsWith("Factura", StringComparison.OrdinalIgnoreCase))
            {
                return seleccionado;
            }

            string condicion = "Consumidor Final";
            string razonSocial = "";

            if (cmbCliente.SelectedItem is DataRowView drv)
            {
                if (drv.Row.Table.Columns.Contains("condicion_fiscal") && drv["condicion_fiscal"] != DBNull.Value)
                    condicion = drv["condicion_fiscal"].ToString() ?? "Consumidor Final";
                if (drv.Row.Table.Columns.Contains("razon_social") && drv["razon_social"] != DBNull.Value)
                    razonSocial = drv["razon_social"].ToString() ?? "";
            }

            if (condicion.Equals("Responsable Inscripto", StringComparison.OrdinalIgnoreCase) || !string.IsNullOrWhiteSpace(razonSocial))
            {
                return "Factura A";
            }

            return "Factura B";
        }

        private void CargarClientes()
        {
            try
            {
                DataTable dt = GestorVentas.ObtenerClientes(incluirConsumidorFinal: false);
                cmbCliente.DisplayMember = "nombre_completo";
                cmbCliente.ValueMember = "id_cliente";
                cmbCliente.DataSource = dt;

                if (cmbCliente.Items.Count > 0)
                {
                    cmbCliente.SelectedIndex = 0;
                    ActualizarComprobantesSegunCliente();
                    CargarEstadoCuenta();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarEstadoCuenta()
        {
            if (cmbCliente.SelectedValue == null || cmbCliente.SelectedValue is DataRowView)
                return;

            int idCliente = Convert.ToInt32(cmbCliente.SelectedValue);
            _idClienteSeleccionado = idCliente;

            try
            {
                var (deudor, favor, idCuenta) = GestorVentas.ObtenerSaldosCuentaCorriente(idCliente);
                _saldoDeudorActual = deudor;
                _saldoFavorActual = favor;

                lblSaldoValor.Text = _saldoDeudorActual.ToString("C2");
                lblSaldoValor.ForeColor = _saldoDeudorActual > 0
                    ? Color.FromArgb(239, 68, 68)
                    : Color.FromArgb(163, 230, 53);

                txtSaldoAFavor.Text = _saldoFavorActual.ToString("C2");

                // Requerimiento: mostrar monto con el saldo a favor ya restado (para no calcular a mano)
                decimal saldoNeto = Math.Max(0m, _saldoDeudorActual - _saldoFavorActual);
                lblSaldoNetoValor.Text = saldoNeto.ToString("C2");
                lblSaldoNetoValor.ForeColor = saldoNeto > 0 ? Color.FromArgb(250, 204, 21) : Color.FromArgb(163, 230, 53);

                // Cargar cargos pendientes ordenados DE MENOR A MAYOR COSTO
                DataTable dtCargos = GestorVentas.ObtenerCargosPendientes(idCliente);
                gridMovimientos.DataSource = dtCargos;

                if (gridMovimientos.Columns.Contains("id_movimiento_ccc"))
                {
                    gridMovimientos.Columns["id_movimiento_ccc"].HeaderText = "ID";
                    gridMovimientos.Columns["id_movimiento_ccc"].Width = 45;
                }
                if (gridMovimientos.Columns.Contains("fecha"))
                {
                    gridMovimientos.Columns["fecha"].HeaderText = "Fecha";
                    gridMovimientos.Columns["fecha"].Width = 95;
                    gridMovimientos.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (gridMovimientos.Columns.Contains("descripcion"))
                {
                    gridMovimientos.Columns["descripcion"].HeaderText = "Producto / Detalle";
                    gridMovimientos.Columns["descripcion"].Width = 240;
                }
                if (gridMovimientos.Columns.Contains("importe"))
                {
                    gridMovimientos.Columns["importe"].HeaderText = "Precio ($)";
                    gridMovimientos.Columns["importe"].Width = 90;
                    gridMovimientos.Columns["importe"].DefaultCellStyle.Format = "N2";
                }
                if (gridMovimientos.Columns.Contains("estado"))
                {
                    gridMovimientos.Columns["estado"].HeaderText = "Estado";
                    gridMovimientos.Columns["estado"].Width = 70;
                }

                txtMonto.Text = saldoNeto > 0 ? saldoNeto.ToString("0.00", CultureInfo.InvariantCulture) : "0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar cuenta corriente:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCliente_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarComprobantesSegunCliente();
            CargarEstadoCuenta();
        }

        private void SoloNumerosDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;
            if (char.IsControl(e.KeyChar)) return;

            if ((e.KeyChar == '.' || e.KeyChar == ',') && !txt.Text.Contains('.') && !txt.Text.Contains(','))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            if (!_idClienteSeleccionado.HasValue || _idClienteSeleccionado.Value <= 0)
            {
                MessageBox.Show("Seleccioná un cliente registrado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? idSesion = GestorVentas.ObtenerIdSesionCajaAbierta(_idUsuario);
            if (!idSesion.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string txt = txtMonto.Text.Trim().Replace(',', '.');
            if (!decimal.TryParse(txt, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingresá un monto a abonar válido mayor a cero.", "Monto inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            string medioPago = cmbMedioPago.SelectedItem?.ToString() ?? "Efectivo";
            string facturaDisponible = ObtenerFacturaSeleccionadaOSegunCliente();

            DialogResult conf = MessageBox.Show(
                $"¿Confirmar cobro de {monto:C2} a {cmbCliente.Text}?\nMedio: {medioPago}",
                "Confirmar Abono", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (conf != DialogResult.Yes) return;

            // Aviso consultando si desea factura o no; si acepta emite la factura correspondiente (A/B/C), si no emite Ticket
            DialogResult deseaFactura = MessageBox.Show(
                $"¿Desea emitir factura ({facturaDisponible}) para este pago?\n\nSeleccione 'Sí' para emitir {facturaDisponible} o 'No' para emitir Ticket.",
                "Comprobante de Pago",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            string tipoComprobante = (deseaFactura == DialogResult.Yes)
                ? facturaDisponible
                : "Ticket";

            try
            {
                ResultadoPagoCC res = GestorVentas.ProcesarPagoCuentaCorriente(
                    _idClienteSeleccionado.Value, monto, medioPago, tipoComprobante, _idUsuario, idSesion.Value);

                _ultimoComprobante = GestorVentas.ObtenerDatosComprobante(res.IdVenta);

                MessageBox.Show(res.Mensaje, "Resultado del Abono", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Generación automática del Ticket / Comprobante al completar el cobro
                ComprobanteService.Imprimir(_ultimoComprobante, this);

                CargarEstadoCuenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el cobro:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (_ultimoComprobante == null)
            {
                MessageBox.Show("Registrá un cobro primero para imprimir el comprobante correspondiente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ComprobanteService.Imprimir(_ultimoComprobante, this);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
