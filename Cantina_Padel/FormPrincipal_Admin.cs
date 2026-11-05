using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cantina_Padel
{
    public partial class FormPrincipal_Admin : Form
    {
        public FormPrincipal_Admin()
        {
            InitializeComponent();
        }

        private void Usuarios_Boton_Click(object sender, EventArgs e)
        {
            FormGestionUsuarios formUsuarios = new FormGestionUsuarios();
            formUsuarios.ShowDialog(this);
        }

        private void Clientes_Boton_Click(object sender, EventArgs e)
        {
            FormGestionClientes formClientes = new FormGestionClientes();
            formClientes.ShowDialog(this);
        }

        private void Productos_Boton_Click(object sender, EventArgs e)
        {
            FormGestionProductos formProductos = new FormGestionProductos();
            formProductos.ShowDialog(this);
        }

        private void Categorias_Boton_Click(object sender, EventArgs e)
        {
            FormGestionCategorias formCategorias = new FormGestionCategorias();
            formCategorias.ShowDialog(this);
        }

        private void Proveedores_Boton_Click(object sender, EventArgs e)
        {
            FormGestionProveedores formProveedores = new FormGestionProveedores();
            formProveedores.ShowDialog(this);
        }

        private void Marcas_Boton_Click(object sender, EventArgs e)
        {
            FormGestionMarcas formMarcas = new FormGestionMarcas();
            formMarcas.ShowDialog(this);
        }

        private void Canchas_Reservas_Click(object sender, EventArgs e)
        {
            FormGestionCanchas formCanchas = new FormGestionCanchas();
            formCanchas.ShowDialog(this);
        }

        private void Reservas_Boton_Click(object sender, EventArgs e)
        {
            FormGestionHorarios formhorarios = new FormGestionHorarios();
            formhorarios.ShowDialog(this);
        }

        private void Caja_Boton_Click(object sender, EventArgs e)
        {
            FormGestionCaja formCaja = new FormGestionCaja(SesionActual.IdUsuario);
            formCaja.ShowDialog(this);
        }

        private void Compras_Boton_Click(object sender, EventArgs e)
        {
            FormGestionCompras formCompras = new FormGestionCompras();
            formCompras.ShowDialog(this);
        }

        private void Reportes_Boton_Click(object sender, EventArgs e)
        {
            FormReportesCompras formReportes = new FormReportesCompras();
            formReportes.ShowDialog(this);
        }

        private void Ventas_Boton_Click(object sender, EventArgs e)
        {
            int? idSesion = GestorVentas.ObtenerIdSesionCajaAbierta(SesionActual.IdUsuario);
            if (!idSesion.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormGestionVentas formVentas = new FormGestionVentas(SesionActual.IdUsuario, idSesion.Value);
            formVentas.ShowDialog(this);
        }

        private bool _volviendoAlLogin = false;
        private void Volver_Menu_Click(object sender, EventArgs e)
        {
            if (GestorVentas.ObtenerIdSesionCajaAbierta(SesionActual.IdUsuario).HasValue)
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
                _volviendoAlLogin = true; // se activa para volver al menu
                this.Owner.Show();        // muestra el login
                this.Close();             // se cierra el formulario
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

            // si no se vuelve al login el usuario cerro con la X
            if (!_volviendoAlLogin)
            {
                Application.Exit(); // con esto cerramos toda la app
            }
        }
    }
}
