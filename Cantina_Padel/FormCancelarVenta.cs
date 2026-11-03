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
            _idUsuario = idUsuario;
            InitializeComponent();

            ConfigurarGrilla(gridVentas);
            ConfigurarGrilla(gridDetalle);
            CargarVentas();
        }

        private void ConfigurarGrilla(DataGridView grid)
        {
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
            foreach (DataGridViewRow fila in gridVentas.Rows)
            {
                if (fila.Cells["estado"].Value?.ToString() == "Cancelada")
                    fila.DefaultCellStyle.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }

        private int? ObtenerIdVentaSeleccionada()
        {
            if (gridVentas.CurrentRow == null) return null;
            return Convert.ToInt32(gridVentas.CurrentRow.Cells["id_venta"].Value);
        }

        private bool VentaSeleccionadaEstaCancelada()
        {
            if (gridVentas.CurrentRow == null) return false;
            return gridVentas.CurrentRow.Cells["estado"].Value?.ToString() == "Cancelada";
        }

        private void CargarDetalle()
        {
            int? idVenta = ObtenerIdVentaSeleccionada();
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
