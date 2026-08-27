using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionUsuarios : Form
    {
        public FormGestionUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
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

                gridUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridUsuarios.DataSource = dt;

                // Personalizar columnas
                gridUsuarios.Columns["id_usuario"].HeaderText      = "ID";
                gridUsuarios.Columns["username"].HeaderText         = "Usuario";
                gridUsuarios.Columns["nombre_completo"].HeaderText  = "Nombre";
                gridUsuarios.Columns["nombre_rol"].HeaderText       = "Rol";
                gridUsuarios.Columns["activo"].HeaderText           = "Activo";
                gridUsuarios.Columns["ultimo_acceso"].HeaderText    = "Último acceso";

                gridUsuarios.Columns["id_usuario"].Width     = 40;
                gridUsuarios.Columns["username"].Width        = 110;
                gridUsuarios.Columns["nombre_completo"].Width = 160;
                gridUsuarios.Columns["nombre_rol"].Width      = 90;
                gridUsuarios.Columns["activo"].Width          = 55;
                gridUsuarios.Columns["ultimo_acceso"].Width   = 130;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar usuarios:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  BOTÓN NUEVO
        // ─────────────────────────────────────────────
        private void Nuevo_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Guardar_Boton.Text = "Crear Usuario";
            txtId.Text = "";
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

            bool esNuevo = string.IsNullOrEmpty(txtId.Text);

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

                if (esNuevo)
                    CrearUsuario(conn, tx);
                else
                    EditarUsuario(conn, tx);

                tx.Commit();
                MessageBox.Show("Operación realizada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearUsuario(MySqlConnection conn, MySqlTransaction tx)
        {
            // Verificar username duplicado
            string checkQuery = "SELECT COUNT(*) FROM Usuario WHERE username = @u";
            using (MySqlCommand check = new MySqlCommand(checkQuery, conn, tx))
            {
                check.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                long count = (long)check.ExecuteScalar()!;
                if (count > 0)
                {
                    MessageBox.Show("El nombre de usuario ya existe.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx.Rollback();
                    return;
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
        }

        private void EditarUsuario(MySqlConnection conn, MySqlTransaction tx)
        {
            int id = int.Parse(txtId.Text);

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

            // Si se ingresó una nueva contraseña, actualizarla
            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                using MySqlCommand cmdPass = new MySqlCommand(
                    "UPDATE Usuario SET password_hash = @p WHERE id_usuario = @id", conn, tx);
                cmdPass.Parameters.AddWithValue("@p",  txtPassword.Text);
                cmdPass.Parameters.AddWithValue("@id", id);
                cmdPass.ExecuteNonQuery();
            }

            // Actualizar rol
            string updateRol = @"
                UPDATE Rol SET nombre_rol = @r WHERE id_usuario = @id";
            using MySqlCommand cmdRol = new MySqlCommand(updateRol, conn, tx);
            cmdRol.Parameters.AddWithValue("@r",  cmbRol.SelectedItem!.ToString());
            cmdRol.Parameters.AddWithValue("@id", id);
            cmdRol.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────
        //  BOTÓN ELIMINAR
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
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
                cmd.Parameters.AddWithValue("@id", int.Parse(txtId.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Usuario desactivado.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
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
            if (e.RowIndex < 0) return;
            var row = gridUsuarios.Rows[e.RowIndex];

            txtId.Text       = row.Cells["id_usuario"].Value?.ToString() ?? "";
            txtUsername.Text = row.Cells["username"].Value?.ToString() ?? "";
            txtNombre.Text   = row.Cells["nombre_completo"].Value?.ToString() ?? "";
            txtPassword.Text = "";  // no mostramos el hash

            string rol = row.Cells["nombre_rol"].Value?.ToString() ?? "Cajero";
            cmbRol.SelectedItem = rol;

            chkActivo.Checked = Convert.ToBoolean(row.Cells["activo"].Value);
            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  BUSCAR
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridUsuarios.DataSource is System.Data.DataTable dt)
            {
                dt.DefaultView.RowFilter =
                    $"username LIKE '%{txtBuscar.Text}%' OR nombre_completo LIKE '%{txtBuscar.Text}%'";
            }
        }

        // ─────────────────────────────────────────────
        //  BOTÓN CAMBIAR CONTRASEÑA
        // ─────────────────────────────────────────────
        private void CambiarPass_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Seleccioná un usuario primero.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var form = new FormCambiarPassword(int.Parse(txtId.Text), txtUsername.Text);
            form.ShowDialog(this);
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            txtId.Text       = "";
            txtUsername.Text = "";
            txtNombre.Text   = "";
            txtPassword.Text = "";
            cmbRol.SelectedIndex = 1; // Cajero por defecto
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
