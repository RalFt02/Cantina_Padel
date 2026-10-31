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
        public List<TurnoCanchaSeleccionado> TurnosParaVenta { get; } = new List<TurnoCanchaSeleccionado>();

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

        private void btnEnviarAVentas_Click(object sender, EventArgs e)
        {
            if (gridReservas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná al menos un turno de la lista para cobrar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TurnosParaVenta.Clear();
            DateTime hoy = DateTime.Today;

            foreach (DataGridViewRow row in gridReservas.SelectedRows)
            {
                if (row.Cells["id_reserva"].Value != null && row.Cells["id_reserva"].Value != DBNull.Value)
                {
                    DateTime fechaRes = Convert.ToDateTime(row.Cells["fecha"].Value);
                    if (fechaRes.Date != hoy)
                    {
                        MessageBox.Show("Solo se pueden cobrar canchas del día de hoy. No se permite cobrar por adelantado.",
                            "Cobro adelantado no permitido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TurnosParaVenta.Clear();
                        return;
                    }

                    int idRes = Convert.ToInt32(row.Cells["id_reserva"].Value);
                    int idCli = (row.Cells["id_cliente"].Value != null && row.Cells["id_cliente"].Value != DBNull.Value)
                        ? Convert.ToInt32(row.Cells["id_cliente"].Value)
                        : 9999;
                    string cancha = row.Cells["cancha"].Value?.ToString() ?? "Cancha";
                    string horario = row.Cells["horario"].Value?.ToString() ?? "";
                    string cliente = row.Cells["cliente"].Value?.ToString() ?? "";
                    decimal precio = (row.Cells["precio"].Value != null && row.Cells["precio"].Value != DBNull.Value)
                        ? Convert.ToDecimal(row.Cells["precio"].Value)
                        : 0m;

                    TurnosParaVenta.Add(new TurnoCanchaSeleccionado
                    {
                        IdReserva = idRes,
                        IdCliente = idCli,
                        Cancha = cancha,
                        Horario = horario,
                        Precio = precio,
                        Fecha = fechaRes,
                        Cliente = cliente
                    });
                }
            }

            if (TurnosParaVenta.Count == 0)
            {
                MessageBox.Show("No se seleccionó ningún turno válido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class TurnoCanchaSeleccionado
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public string Cancha { get; set; } = "";
        public string Horario { get; set; } = "";
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = "";
    }

}
