using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Iniciar_Sesion_Click(object sender, EventArgs e)
        {
            string usuario = Usuario_Campo.Text;
            string password = Contra_Campo.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Usuario y contraseña son obligatorios.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           

            try
            {
                using (MySqlConnection conn = Conexion.ObtenerConexion())
                {
                    conn.Open();

                    string query = @"
                        SELECT u.id_usuario, u.password_hash, r.nombre_rol
                        FROM Usuario u
                        INNER JOIN Rol r ON r.id_usuario = u.id_usuario
                        WHERE u.username = @usuario";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            string hashGuardado = reader["password_hash"].ToString()!;
                            string rol = reader["nombre_rol"].ToString()!;
                            int idUsuario = Convert.ToInt32(reader["id_usuario"]);

                            reader.Close();

                            if (password != hashGuardado)
                            {
                                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Actualizar ultimo_acceso
                            string updateQuery = "UPDATE Usuario SET ultimo_acceso = NOW() WHERE id_usuario = @id";
                            using (MySqlCommand cmdUpdate = new MySqlCommand(updateQuery, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@id", idUsuario);
                                cmdUpdate.ExecuteNonQuery();
                            }

                            this.Hide();

                            if (rol == "Admin")
                            {
                                FormPrincipal_Admin formAdmin = new FormPrincipal_Admin();
                                formAdmin.Owner = this; // Le pasamos el Login como dueño
                                formAdmin.Show();
                            }
                            else
                            {
                                FormPrincipal_Usuario formUsuario = new FormPrincipal_Usuario();
                                formUsuario.Owner = this; // Le pasamos el Login como empleado
                                formUsuario.Show();
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos.\n" + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
