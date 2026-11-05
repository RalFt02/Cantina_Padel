using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Cantina_Padel
{
    public partial class FormCancelarVenta : Form
    {
        private readonly int _idUsuario;

        public FormCancelarVenta(int idUsuario)
        {
            _idUsuario = idUsuario > 0 ? idUsuario : 1;
            InitializeComponent();
            this.BindingContext ??= new BindingContext();

            ConfigurarGrilla(gridVentas);
            ConfigurarGrilla(gridDetalle);

            gridVentas.DataBindingComplete += (_, _) => MarcarVentasCanceladas();
            this.Load += (_, _) =>
            {
                MarcarVentasCanceladas();
                CargarDetalle();
            };

            CargarVentas();
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

        private void CargarVentas()
        {
            try
            {
                this.BindingContext ??= new BindingContext();
                gridVentas.AutoGenerateColumns = true;
                gridVentas.BindingContext = this.BindingContext;
                gridVentas.DataSource = GestorVentas.ObtenerVentas();
                MarcarVentasCanceladas();
                CargarDetalle();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar las ventas:\n" + ex.Message);
            }
        }

        private void MarcarVentasCanceladas()
        {
            if (gridVentas == null || !gridVentas.Columns.Contains("estado")) return;
            foreach (DataGridViewRow fila in gridVentas.Rows)
            {
                if (fila.Cells["estado"]?.Value?.ToString() == "Cancelada")
                    fila.DefaultCellStyle.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }

        private int? ObtenerIdVentaSeleccionada()
        {
            if (gridVentas == null || gridVentas.CurrentRow == null || gridVentas.CurrentRow.IsNewRow) return null;
            var val = gridVentas.Columns.Contains("id_venta")
                ? gridVentas.CurrentRow.Cells["id_venta"]?.Value
                : (gridVentas.CurrentRow.Cells.Count > 0 ? gridVentas.CurrentRow.Cells[0]?.Value : null);
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private bool VentaSeleccionadaEstaCancelada()
        {
            if (gridVentas == null || gridVentas.CurrentRow == null || !gridVentas.Columns.Contains("estado")) return false;
            return gridVentas.CurrentRow.Cells["estado"]?.Value?.ToString() == "Cancelada";
        }

        private void CargarDetalle()
        {
            int? idVenta = ObtenerIdVentaSeleccionada();
            this.BindingContext ??= new BindingContext();
            gridDetalle.AutoGenerateColumns = true;
            gridDetalle.BindingContext = this.BindingContext;
            gridDetalle.DataSource = idVenta.HasValue ? GestorVentas.ObtenerDetalleVenta(idVenta.Value) : null;
        }

        private void CancelarVentaSeleccionada()
        {
            int? idVenta = ObtenerIdVentaSeleccionada();
            if (!idVenta.HasValue)
            {
                MostrarAviso("Seleccioná la venta que querés cancelar.");
                return;
            }

            if (VentaSeleccionadaEstaCancelada())
            {
                MostrarAviso("Esa venta ya está cancelada.");
                return;
            }

            string? errorMotivo = GestorVentas.ValidarMotivoCancelacion(txtMotivo.Text);
            if (errorMotivo != null)
            {
                MostrarAviso(errorMotivo);
                txtMotivo.Focus();
                return;
            }

            DialogResult confirmar = MessageBox.Show(
                $"¿Cancelar la venta N° {idVenta}?\nSe devuelve el stock y se revierte lo cargado en cuenta corriente.",
                "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes) return;

            try
            {
                GestorVentas.CancelarVenta(idVenta.Value, _idUsuario, txtMotivo.Text);
                MessageBox.Show($"La venta N° {idVenta} fue cancelada.", "Venta cancelada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMotivo.Clear();
                CargarVentas();
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo cancelar la venta:\n" + ex.Message);
            }
        }

        private void MostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gridVentas_SelectionChanged(object? sender, EventArgs e)
        {
            CargarDetalle();
        }

        private void btnCancelarVenta_Click(object? sender, EventArgs e)
        {
            CancelarVentaSeleccionada();
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
