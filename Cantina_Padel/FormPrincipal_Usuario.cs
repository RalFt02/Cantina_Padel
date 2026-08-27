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

        private void Clientes_Boton_Click(object sender, EventArgs e)
        {
            FormGestionClientes formClientes = new FormGestionClientes();
            formClientes.ShowDialog(this);
        }

        private bool _volviendoAlLogin = false;
        private void Volver_Menu_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                _volviendoAlLogin = true; // se activa para volver al menu
                this.Owner.Show();        // muestra el login
                this.Close();             // se cierra el formulario
            }
        }
        private void FormPrincipal_Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // si no se vuelve al login el usuario cerro con la X
            if (!_volviendoAlLogin)
            {
                Application.Exit(); // con esto cerramos toda la app
            }
        }
    }
}
