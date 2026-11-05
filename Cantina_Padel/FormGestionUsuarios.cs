using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionUsuarios : Form
    {
        private int? _idSeleccionado;

        public FormGestionUsuarios()
        {
            InitializeComponent();
            this.BindingContext ??= new BindingContext();
            gridUsuarios.AutoGenerateColumns = true;
            gridUsuarios.BindingContext = this.BindingContext;
            gridUsuarios.DataBindingComplete += (_, _) => FormatearColumnasUsuarios();
            this.Load += (_, _) => FormatearColumnasUsuarios();
            CargarUsuarios();
        }

        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // ─────────────────────────────────────────────
        //  CARGAR GRILLA
        // ─────────────────────────────────────────────
        private void CargarUsuarios()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = @"
                    SELECT u.id_usuario,
                           u.username,
                           u.nombre_completo,
                           r.nombre_rol,
                           u.activo,
                           u.ultimo_acceso
                    FROM Usuario u
                    LEFT JOIN Rol r ON r.id_usuario = u.id_usuario
                    ORDER BY u.id_usuario";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                this.BindingContext ??= new BindingContext();
                gridUsuarios.AutoGenerateColumns = true;
                gridUsuarios.BindingContext = this.BindingContext;
                gridUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridUsuarios.DataSource = dt;

                FormatearColumnasUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasUsuarios()
        {
            if (gridUsuarios == null || gridUsuarios.Columns.Count == 0) return;

            void Configurar(string colName, string header, int width)
            {
                if (gridUsuarios.Columns[colName] is DataGridViewColumn col)
                {
                    col.HeaderText = header;
                    col.Width = width;
                }
            }

            Configurar("id_usuario", "ID", 40);
            Configurar("username", "Usuario", 110);
            Configurar("nombre_completo", "Nombre", 160);
            Configurar("nombre_rol", "Rol", 90);
            Configurar("activo", "Activo", 55);
            Configurar("ultimo_acceso", "Último acceso", 130);
        }

        // ─────────────────────────────────────────────
        //  BOTÓN NUEVO
        // ─────────────────────────────────────────────
        private void Nuevo_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Guardar_Boton.Text = "Crear Usuario";
        }

        // ─────────────────────────────────────────────
        //  BOTÓN GUARDAR (CREAR / EDITAR)
        // ─────────────────────────────────────────────
        private void Guardar_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text)   ||
                cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Completá todos los campos obligatorios.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esNuevo = _idSeleccionado == null;

            if (esNuevo && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("La contraseña es obligatoria para nuevos usuarios.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();
                using MySqlTransaction tx = conn.BeginTransaction();

                bool exito = esNuevo
                    ? CrearUsuario(conn, tx)
                    : EditarUsuario(conn, tx);

                if (!exito)
                {
                    try { tx.Rollback(); } catch { }
                    return;
                }

                tx.Commit();
                MessageBox.Show("Operación realizada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("El nombre de usuario ingresado ya existe.",
                    "Usuario duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CrearUsuario(MySqlConnection conn, MySqlTransaction tx)
        {
            string checkQuery = "SELECT COUNT(*) FROM Usuario WHERE username = @u";
            using (MySqlCommand check = new MySqlCommand(checkQuery, conn, tx))
            {
                check.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                long count = Convert.ToInt64(check.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("El nombre de usuario ya existe.",
                        "Usuario ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            string insertUsuario = @"
                INSERT INTO Usuario (username, password_hash, nombre_completo, activo)
                VALUES (@u, @p, @n, @a);
                SELECT LAST_INSERT_ID();";

            int nuevoId;
            using (MySqlCommand cmd = new MySqlCommand(insertUsuario, conn, tx))
            {
                cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@p", txtPassword.Text);
                cmd.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@a", chkActivo.Checked ? 1 : 0);
                nuevoId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            string insertRol = @"
                INSERT INTO Rol (id_usuario, nombre_rol)
                VALUES (@id, @r)";
            using MySqlCommand cmdRol = new MySqlCommand(insertRol, conn, tx);
            cmdRol.Parameters.AddWithValue("@id", nuevoId);
            cmdRol.Parameters.AddWithValue("@r", cmbRol.SelectedItem!.ToString());
            cmdRol.ExecuteNonQuery();
            return true;
        }

        private bool EditarUsuario(MySqlConnection conn, MySqlTransaction tx)
        {
            int id = _idSeleccionado!.Value;

            string checkQuery = "SELECT COUNT(*) FROM Usuario WHERE username = @u AND id_usuario <> @id";
            using (MySqlCommand check = new MySqlCommand(checkQuery, conn, tx))
            {
                check.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                check.Parameters.AddWithValue("@id", id);
                long count = Convert.ToInt64(check.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Ya existe otro usuario con ese nombre de usuario.",
                        "Usuario ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            string updateUsuario = @"
                UPDATE Usuario
                SET username       = @u,
                    nombre_completo = @n,
                    activo          = @a
                WHERE id_usuario = @id";

            using (MySqlCommand cmd = new MySqlCommand(updateUsuario, conn, tx))
            {
                cmd.Parameters.AddWithValue("@u",  txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@n",  txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@a",  chkActivo.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                using MySqlCommand cmdPass = new MySqlCommand(
                    "UPDATE Usuario SET password_hash = @p WHERE id_usuario = @id", conn, tx);
                cmdPass.Parameters.AddWithValue("@p",  txtPassword.Text);
                cmdPass.Parameters.AddWithValue("@id", id);
                cmdPass.ExecuteNonQuery();
            }

            string updateRol = @"
                UPDATE Rol SET nombre_rol = @r WHERE id_usuario = @id";
            using MySqlCommand cmdRol = new MySqlCommand(updateRol, conn, tx);
            cmdRol.Parameters.AddWithValue("@r",  cmbRol.SelectedItem!.ToString());
            cmdRol.Parameters.AddWithValue("@id", id);
            cmdRol.ExecuteNonQuery();
            return true;
        }

        // ─────────────────────────────────────────────
        //  BOTÓN ELIMINAR
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un usuario de la lista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Desactivar al usuario '{txtUsername.Text}'?\n(No se eliminará permanentemente)",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "UPDATE Usuario SET activo = 0 WHERE id_usuario = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Usuario desactivado.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  SELECCIONAR FILA EN GRILLA
        // ─────────────────────────────────────────────
        private void gridUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= gridUsuarios.Rows.Count) return;
            var row = gridUsuarios.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var idVal = gridUsuarios.Columns.Contains("id_usuario") ? row.Cells["id_usuario"]?.Value : (row.Cells.Count > 0 ? row.Cells[0]?.Value : null);
            if (idVal == null || idVal == DBNull.Value) return;

            _idSeleccionado  = Convert.ToInt32(idVal);
            txtUsername.Text = gridUsuarios.Columns.Contains("username") ? (row.Cells["username"]?.Value?.ToString() ?? "") : "";
            txtNombre.Text   = gridUsuarios.Columns.Contains("nombre_completo") ? (row.Cells["nombre_completo"]?.Value?.ToString() ?? "") : "";
            txtPassword.Text = "";

            string rol = gridUsuarios.Columns.Contains("nombre_rol") ? (row.Cells["nombre_rol"]?.Value?.ToString() ?? "Cajero") : "Cajero";
            cmbRol.SelectedItem = rol;

            var actVal = gridUsuarios.Columns.Contains("activo") ? row.Cells["activo"]?.Value : null;
            chkActivo.Checked = actVal != null && actVal != DBNull.Value && Convert.ToBoolean(actVal);
            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  BUSCAR
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridUsuarios.DataSource is System.Data.DataTable dt)
            {
                string filtro = txtBuscar.Text.Replace("'", "''");
                dt.DefaultView.RowFilter =
                    $"username LIKE '%{filtro}%' OR nombre_completo LIKE '%{filtro}%'";
            }
        }

        // ─────────────────────────────────────────────
        //  BOTÓN CAMBIAR CONTRASEÑA
        // ─────────────────────────────────────────────
        private void CambiarPass_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un usuario primero.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var form = new FormCambiarPassword(_idSeleccionado.Value, txtUsername.Text);
            form.ShowDialog(this);
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            _idSeleccionado  = null;
            txtUsername.Text = "";
            txtNombre.Text   = "";
            txtPassword.Text = "";
            if (cmbRol.Items.Count > 1) cmbRol.SelectedIndex = 1;
            chkActivo.Checked = true;
            Guardar_Boton.Text = "Crear Usuario";
        }

        private void Cancelar_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void Volver_Boton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
