using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class Principal : Form
    {
        // Pequeño contenedor con los datos que necesitamos del usuario para
        // decidir si puede entrar o no. Separarlo así permite testear la lógica
        // de login sin depender de un MySqlDataReader abierto.
        private class UsuarioLogin
        {
            public int IdUsuario { get; set; }
            public string PasswordHash { get; set; } = "";
            public string Rol { get; set; } = "";
            public bool Activo { get; set; }
        }

        public Principal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Se dispara cada vez que el form pasa a visible, sin importar quién
        // llamó a Show() (el propio arranque de la app, o el botón "Volver al
        // Menu" de Admin/Usuario). Así no queda usuario ni contraseña cargados
        // de la sesión anterior.
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                LimpiarCampos();
                SesionActual.Limpiar(); // al volver al login ya no hay nadie logueado
            }
        }

        private void LimpiarCampos()
        {
            Usuario_Campo.Text = "";
            Contra_Campo.Text = "";
        }

        // ─────────────────────────────────────────────
        //  BOTÓN INICIAR SESIÓN (solo orquesta, no valida nada por su cuenta)
        // ─────────────────────────────────────────────
        private void Iniciar_Sesion_Click(object sender, EventArgs e)
        {
            string usuario = Usuario_Campo.Text;
            string password = Contra_Campo.Text;

            if (!CamposCompletos(usuario, password))
            {
                MessageBox.Show("Usuario y contraseña son obligatorios.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UsuarioLogin? datos;
            try
            {
                datos = BuscarUsuario(usuario);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos.\n" + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (datos == null || !ContraseñaValida(password, datos.PasswordHash))
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!datos.Activo)
            {
                MessageBox.Show("Usuario desactivado.", "Error de acceso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RegistrarUltimoAcceso(datos.IdUsuario);
            // Guardamos quién entró para que los demás forms (caja, ventas, etc.) lo usen
            SesionActual.Iniciar(datos.IdUsuario, datos.Rol);
            this.Hide();
            AbrirFormularioSegunRol(datos.Rol);
        }

        // ─────────────────────────────────────────────
        //  VALIDACIÓN DE CAMPOS
        // ─────────────────────────────────────────────
        private bool CamposCompletos(string usuario, string password)
        {
            return !string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(password);
        }

        // ─────────────────────────────────────────────
        //  BÚSQUEDA DEL USUARIO EN LA BASE
        // ─────────────────────────────────────────────
        // Devuelve null si no existe ningún usuario con ese username.
        private UsuarioLogin? BuscarUsuario(string username)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();

            string query = @"
                SELECT u.id_usuario, u.password_hash, u.activo, r.nombre_rol
                FROM Usuario u
                INNER JOIN Rol r ON r.id_usuario = u.id_usuario
                WHERE u.username = @usuario";

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@usuario", username);

            using MySqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return new UsuarioLogin
            {
                IdUsuario    = Convert.ToInt32(reader["id_usuario"]),
                PasswordHash = reader["password_hash"].ToString()!,
                Activo       = Convert.ToBoolean(reader["activo"]),
                Rol          = reader["nombre_rol"].ToString()!
            };
        }

        // ─────────────────────────────────────────────
        //  VALIDACIÓN DE CONTRASEÑA
        // ─────────────────────────────────────────────
        // Nota: compara texto plano porque así está guardado hoy en la base
        // (la columna se llama "password_hash" pero no se aplica ningún hash
        // todavía). Si en algún momento migran a hashing real, este es el
        // único lugar que hay que tocar.
        private bool ContraseñaValida(string ingresada, string hashGuardado)
        {
            return ingresada == hashGuardado;
        }

        // ─────────────────────────────────────────────
        //  ACTUALIZAR ÚLTIMO ACCESO
        // ─────────────────────────────────────────────
        private void RegistrarUltimoAcceso(int idUsuario)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();

            string query = "UPDATE Usuario SET ultimo_acceso = NOW() WHERE id_usuario = @id";
            using MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", idUsuario);
            cmd.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────
        //  ABRIR EL MENÚ SEGÚN EL ROL
        // ─────────────────────────────────────────────
        private void AbrirFormularioSegunRol(string rol)
        {
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
