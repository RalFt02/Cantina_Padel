using System;
using System.Windows.Forms;

namespace Cantina_Padel
{
    public partial class FormEnviarMail : Form
    {
        private readonly DatosComprobante _datos;

        public FormEnviarMail(DatosComprobante datos)
        {
            InitializeComponent();
            _datos = datos;

            txtDestinatario.Text = !string.IsNullOrWhiteSpace(datos.EmailCliente) ? datos.EmailCliente : "";
            txtAsunto.Text = $"Comprobante {datos.TipoComprobante} N° {datos.NumeroComprobante} - Cantina Padel Club";
            txtVistaPrevia.Text = ComprobanteService.GenerarTextoPlano(datos);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string email = txtDestinatario.Text.Trim();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                MessageBox.Show("Por favor, ingresá una dirección de correo válida.", "Email inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDestinatario.Focus();
                return;
            }

            Cursor = Cursors.WaitCursor;
            bool ok = ComprobanteService.EnviarPorMail(_datos, email, out string? error);
            Cursor = Cursors.Default;

            if (ok)
            {
                MessageBox.Show(
                    $"¡El comprobante fue enviado con éxito a {email}!",
                    "Envío Exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    $"No se pudo enviar el correo:\n{error}\n\nVerificá la configuración del servidor de salida o tu conexión.",
                    "Error al enviar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
