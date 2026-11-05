using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cantina_Padel
{
    public partial class FormPrincipal_Usuario : Form
    {
        public FormPrincipal_Usuario()
        {
            InitializeComponent();
        }

        private int ObtenerIdUsuarioActual() => SesionActual.HayUsuario ? SesionActual.IdUsuario : 1;

        private void Clientes_Boton_Click(object sender, EventArgs e)
        {
            using FormGestionClientes formClientes = new FormGestionClientes();
            formClientes.ShowDialog(this);
        }

        private void Reservas_Boton_Click(object sender, EventArgs e)
        {
            using FormGestionHorarios formhorarios = new FormGestionHorarios();
            formhorarios.ShowDialog(this);
        }

        private void Caja_Boton_Click(object sender, EventArgs e)
        {
            using FormGestionCaja formCaja = new FormGestionCaja(ObtenerIdUsuarioActual());
            formCaja.ShowDialog(this);
        }

        private void Ventas_Boton_Click(object sender, EventArgs e)
        {
            int idUsu = ObtenerIdUsuarioActual();
            int? idSesion = GestorVentas.ObtenerIdSesionCajaAbierta(idUsu);
            if (!idSesion.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using FormGestionVentas formVentas = new FormGestionVentas(idUsu, idSesion.Value);
            formVentas.ShowDialog(this);
        }

        private void Reportes_Boton_Click(object sender, EventArgs e)
        {
            using FormReportes formReportes = new FormReportes();
            formReportes.ShowDialog(this);
        }

        private bool _volviendoAlLogin = false;
        private void Volver_Menu_Click(object sender, EventArgs e)
        {
            if (GestorVentas.ObtenerIdSesionCajaAbierta(ObtenerIdUsuarioActual()).HasValue)
            {
                MessageBox.Show(
                    "Debe cerrar la caja antes de salir del sistema.",
                    "Caja abierta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (this.Owner != null)
            {
                _volviendoAlLogin = true;
                this.Owner.Show();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void FormPrincipal_Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing &&
                SesionActual.HayUsuario &&
                GestorVentas.ObtenerIdSesionCajaAbierta(SesionActual.IdUsuario).HasValue)
            {
                MessageBox.Show(
                    "Debe cerrar la caja antes de salir del sistema.",
                    "Caja abierta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                e.Cancel = true;
                _volviendoAlLogin = false;
                return;
            }

            if (!_volviendoAlLogin)
            {
                Application.Exit();
            }
        }
    }
}
