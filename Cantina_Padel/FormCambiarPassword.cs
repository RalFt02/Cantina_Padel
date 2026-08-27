using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormCambiarPassword : Form
    {
        private readonly int    _idUsuario;
        private readonly string _username;

        public FormCambiarPassword(int idUsuario, string username)
        {
            _idUsuario = idUsuario;
            _username  = username;
            InitializeComponent();
            lblUsuarioValor.Text = username;
        }

        private void Guardar_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNueva.Text))
            {
                MessageBox.Show("Ingresá la nueva contraseña.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNueva.Text != txtConfirmar.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNueva.Text.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "UPDATE Usuario SET password_hash = @p WHERE id_usuario = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@p",  txtNueva.Text);
                cmd.Parameters.AddWithValue("@id", _idUsuario);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Contraseña actualizada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al actualizar la contraseña:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancelar_Boton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
