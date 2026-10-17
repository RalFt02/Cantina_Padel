using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Cantina_Padel
{
    public partial class FormCobroCanchas : Form
    {
        private readonly int _idUsuario;
        private decimal _totalSeleccionado = 0m;
        private readonly List<int> _reservasSeleccionadas = new List<int>();
        private DatosComprobante? _ultimoComprobante;

        public FormCobroCanchas(int idUsuario)
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

            ConfigurarGrilla(gridReservas);
            CargarMediosDePago();
            CargarComprobantes();
            CargarClientes();
            CargarReservas();
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
            cmbMedioPago.Items.Add("Cuenta Corriente");
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

        private void CargarClientes()
        {
            try
            {
                DataTable dt = GestorVentas.ObtenerClientes(incluirConsumidorFinal: false);
                DataRow rowTodos = dt.NewRow();
                rowTodos["id_cliente"] = -1;
                rowTodos["nombre_completo"] = "[ Todos los clientes ]";
                dt.Rows.InsertAt(rowTodos, 0);

                cmbCliente.DisplayMember = "nombre_completo";
                cmbCliente.ValueMember = "id_cliente";
                cmbCliente.DataSource = dt;
                cmbCliente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReservas()
        {
            try
            {
                int? idCliente = null;
                if (cmbCliente.SelectedValue != null && !(cmbCliente.SelectedValue is DataRowView))
                {
                    int val = Convert.ToInt32(cmbCliente.SelectedValue);
                    if (val > 0) idCliente = val;
                }

                DataTable dt = GestorVentas.ObtenerReservasPendientesPago(idCliente);
                gridReservas.DataSource = dt;

                if (gridReservas.Columns.Contains("id_reserva"))
                {
                    gridReservas.Columns["id_reserva"].HeaderText = "ID";
                    gridReservas.Columns["id_reserva"].Width = 45;
                }
                if (gridReservas.Columns.Contains("cancha"))
                {
                    gridReservas.Columns["cancha"].HeaderText = "Cancha";
                    gridReservas.Columns["cancha"].Width = 110;
                }
                if (gridReservas.Columns.Contains("fecha"))
                {
                    gridReservas.Columns["fecha"].HeaderText = "Fecha";
                    gridReservas.Columns["fecha"].Width = 95;
                    gridReservas.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (gridReservas.Columns.Contains("horario"))
                {
                    gridReservas.Columns["horario"].HeaderText = "Turno";
                    gridReservas.Columns["horario"].Width = 95;
                }
                if (gridReservas.Columns.Contains("cliente"))
                {
                    gridReservas.Columns["cliente"].HeaderText = "Cliente";
                    gridReservas.Columns["cliente"].Width = 150;
                }
                if (gridReservas.Columns.Contains("precio"))
                {
                    gridReservas.Columns["precio"].HeaderText = "Precio ($)";
                    gridReservas.Columns["precio"].Width = 85;
                    gridReservas.Columns["precio"].DefaultCellStyle.Format = "N2";
                }

                ActualizarSeleccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reservas pendientes:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarSeleccion()
        {
            _reservasSeleccionadas.Clear();
            _totalSeleccionado = 0m;

            foreach (DataGridViewRow row in gridReservas.SelectedRows)
            {
                if (row.Cells["id_reserva"].Value != null)
                {
                    int idRes = Convert.ToInt32(row.Cells["id_reserva"].Value);
                    decimal precio = row.Cells["precio"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["precio"].Value) : 0m;

                    _reservasSeleccionadas.Add(idRes);
                    _totalSeleccionado += precio;
                }
            }

            lblTotal.Text = "Total: " + _totalSeleccionado.ToString("C2");
        }

        private void gridReservas_SelectionChanged(object? sender, EventArgs e)
        {
            ActualizarSeleccion();
        }

        private void cmbCliente_SelectedIndexChanged(object? sender, EventArgs e)
        {
            CargarReservas();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarReservas();
        }

        private void btnRegistrarCobro_Click(object sender, EventArgs e)
        {
            if (_reservasSeleccionadas.Count == 0)
            {
                MessageBox.Show("Seleccioná al menos un turno/reserva de la lista para cobrar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string medioPago = cmbMedioPago.SelectedItem?.ToString() ?? "Efectivo";
            string tipoComprobante = cmbComprobante.SelectedItem?.ToString() ?? "Ticket";
            int? idSesion = GestorVentas.ObtenerIdSesionCajaAbierta(_idUsuario);
            if (!idSesion.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el id_cliente de la primera reserva seleccionada si está disponible
            int? idCliente = null;
            if (gridReservas.SelectedRows.Count > 0 && gridReservas.Columns.Contains("id_cliente"))
            {
                var val = gridReservas.SelectedRows[0].Cells["id_cliente"].Value;
                if (val != null && val != DBNull.Value && Convert.ToInt32(val) > 0)
                    idCliente = Convert.ToInt32(val);
            }

            if (!idCliente.HasValue || idCliente.Value <= 0)
            {
                MessageBox.Show("Solo clientes registrados interactúan con el sistema. La reserva debe tener un cliente asignado.",
                    "Cliente requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult conf = MessageBox.Show(
                $"¿Confirmar cobro de {_reservasSeleccionadas.Count} turno(s) por un total de {_totalSeleccionado:C2}?\n" +
                $"Medio de Pago: {medioPago}\n" +
                $"Comprobante: {tipoComprobante}",
                "Confirmar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (conf != DialogResult.Yes) return;

            try
            {
                int idVenta = GestorVentas.RegistrarCobroCanchas(
                    _reservasSeleccionadas, _idUsuario, idCliente.Value, idSesion.Value, medioPago, tipoComprobante, _totalSeleccionado);

                _ultimoComprobante = GestorVentas.ObtenerDatosComprobante(idVenta);

                MessageBox.Show(
                    $"¡Cobro registrado con éxito!\nSe generó la Venta N° {idVenta} ({tipoComprobante}).",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarReservas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el cobro de canchas:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (_ultimoComprobante == null)
            {
                MessageBox.Show("Registrá un cobro primero para imprimir su comprobante.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ComprobanteService.Imprimir(_ultimoComprobante, this);
        }

        private void btnEnviarMail_Click(object sender, EventArgs e)
        {
            if (_ultimoComprobante == null)
            {
                MessageBox.Show("Registrá un cobro primero para enviar su comprobante por email.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormEnviarMail f = new FormEnviarMail(_ultimoComprobante);
            f.ShowDialog(this);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
