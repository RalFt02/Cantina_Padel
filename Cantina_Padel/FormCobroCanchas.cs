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

            InitializeComponent();
            this.BindingContext ??= new BindingContext();

            if (!idSesion.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Load += (s, e) => this.Close();
                return;
            }

            ConfigurarGrilla(gridReservas);
            gridReservas.DataBindingComplete += (_, _) => FormatearColumnasReservas();
            this.Load += (_, _) => FormatearColumnasReservas();
            CargarClientes();
            CargarReservas();
        }

        private void ConfigurarGrilla(DataGridView grid)
        {
            this.BindingContext ??= new BindingContext();
            grid.AutoGenerateColumns = true;
            grid.BindingContext = this.BindingContext;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                if (cmbCliente.SelectedValue != null && cmbCliente.SelectedValue != DBNull.Value && !(cmbCliente.SelectedValue is DataRowView))
                {
                    int val = Convert.ToInt32(cmbCliente.SelectedValue);
                    if (val > 0) idCliente = val;
                }

                DataTable dt = GestorVentas.ObtenerReservasPendientesPago(idCliente);
                this.BindingContext ??= new BindingContext();
                gridReservas.AutoGenerateColumns = true;
                gridReservas.BindingContext = this.BindingContext;
                gridReservas.DataSource = dt;

                FormatearColumnasReservas();
                ActualizarSeleccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reservas pendientes:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasReservas()
        {
            if (gridReservas == null || gridReservas.Columns.Count == 0) return;

            if (gridReservas.Columns["id_reserva"] is DataGridViewColumn colId)
            {
                colId.HeaderText = "ID";
                colId.Width = 45;
            }
            if (gridReservas.Columns["id_cliente"] is DataGridViewColumn colIdCli)
            {
                colIdCli.Visible = false;
            }
            if (gridReservas.Columns["cancha"] is DataGridViewColumn colCan)
            {
                colCan.HeaderText = "Cancha";
                colCan.Width = 110;
            }
            if (gridReservas.Columns["fecha"] is DataGridViewColumn colFec)
            {
                colFec.HeaderText = "Fecha";
                colFec.Width = 95;
                colFec.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (gridReservas.Columns["horario"] is DataGridViewColumn colHor)
            {
                colHor.HeaderText = "Turno";
                colHor.Width = 95;
            }
            if (gridReservas.Columns["cliente"] is DataGridViewColumn colCli)
            {
                colCli.HeaderText = "Cliente";
                colCli.Width = 150;
            }
            if (gridReservas.Columns["precio"] is DataGridViewColumn colPre)
            {
                colPre.HeaderText = "Precio ($)";
                colPre.Width = 85;
                colPre.DefaultCellStyle.Format = "N2";
            }
        }

        private void ActualizarSeleccion()
        {
            _reservasSeleccionadas.Clear();
            _totalSeleccionado = 0m;

            if (gridReservas == null || !gridReservas.Columns.Contains("id_reserva"))
            {
                lblTotal.Text = "Total: " + _totalSeleccionado.ToString("C2");
                return;
            }

            foreach (DataGridViewRow row in gridReservas.SelectedRows)
            {
                var idVal = row.Cells["id_reserva"]?.Value;
                if (idVal != null && idVal != DBNull.Value)
                {
                    int idRes = Convert.ToInt32(idVal);
                    var preVal = gridReservas.Columns.Contains("precio") ? row.Cells["precio"]?.Value : null;
                    decimal precio = preVal != null && preVal != DBNull.Value ? Convert.ToDecimal(preVal) : 0m;

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
            if (gridReservas.SelectedRows.Count == 0 || !gridReservas.Columns.Contains("id_reserva"))
            {
                MessageBox.Show("Seleccioná al menos un turno de la lista para cobrar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TurnosParaVenta.Clear();
            DateTime hoy = DateTime.Today;

            foreach (DataGridViewRow row in gridReservas.SelectedRows)
            {
                var idVal = row.Cells["id_reserva"]?.Value;
                if (idVal != null && idVal != DBNull.Value)
                {
                    DateTime fechaRes = Convert.ToDateTime(row.Cells["fecha"]?.Value);
                    if (fechaRes.Date != hoy)
                    {
                        MessageBox.Show("Solo se pueden cobrar canchas del día de hoy. No se permite cobrar por adelantado.",
                            "Cobro adelantado no permitido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TurnosParaVenta.Clear();
                        return;
                    }

                    int idRes = Convert.ToInt32(idVal);
                    var cliIdVal = gridReservas.Columns.Contains("id_cliente") ? row.Cells["id_cliente"]?.Value : null;
                    int idCli = (cliIdVal != null && cliIdVal != DBNull.Value)
                        ? Convert.ToInt32(cliIdVal)
                        : 9999;
                    string cancha = gridReservas.Columns.Contains("cancha") ? (row.Cells["cancha"]?.Value?.ToString() ?? "Cancha") : "Cancha";
                    string horario = gridReservas.Columns.Contains("horario") ? (row.Cells["horario"]?.Value?.ToString() ?? "") : "";
                    string cliente = gridReservas.Columns.Contains("cliente") ? (row.Cells["cliente"]?.Value?.ToString() ?? "") : "";
                    var preVal = gridReservas.Columns.Contains("precio") ? row.Cells["precio"]?.Value : null;
                    decimal precio = (preVal != null && preVal != DBNull.Value)
                        ? Convert.ToDecimal(preVal)
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
