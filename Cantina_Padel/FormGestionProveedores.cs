using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionProveedores : Form
    {
        // Guarda el ID del proveedor seleccionado en la grilla (null = alta nueva).
        // Ya no usamos un TextBox oculto para esto: así no aparece ni en tiempo de
        // ejecución NI en el lienzo de diseño de Visual Studio.
        private int? _idSeleccionado;

        public FormGestionProveedores()
        {
            InitializeComponent();
            CargarProveedores();
        }

        // Permite solo letras (incluye acentos y ñ), espacios y teclas de control (backspace, etc.)
        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Permite solo dígitos y teclas de control (backspace, etc.)
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // ─────────────────────────────────────────────
        //  CARGAR GRILLA
        // ─────────────────────────────────────────────
        private void CargarProveedores()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = @"
                    SELECT pr.id_proveedor,
                           p.nombre,
                           p.apellido,
                           p.telefono,
                           p.dni,
                           p.email,
                           p.direccion,
                           p.cuit,
                           p.razon_social,
                           pr.observaciones,
                           p.activo
                    FROM proveedor pr
                    JOIN persona p ON p.id_persona = pr.id_persona
                    ORDER BY pr.id_proveedor";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                gridProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridProveedores.DataSource = dt;

                // Personalizar columnas
                gridProveedores.Columns["id_proveedor"].HeaderText  = "ID";
                gridProveedores.Columns["nombre"].HeaderText         = "Nombre";
                gridProveedores.Columns["apellido"].HeaderText       = "Apellido";
                gridProveedores.Columns["telefono"].HeaderText       = "Teléfono";
                gridProveedores.Columns["dni"].HeaderText            = "DNI";
                gridProveedores.Columns["email"].HeaderText          = "Email";
                gridProveedores.Columns["direccion"].HeaderText      = "Dirección";
                gridProveedores.Columns["cuit"].HeaderText           = "CUIT";
                gridProveedores.Columns["razon_social"].HeaderText   = "Razón Social";
                gridProveedores.Columns["observaciones"].HeaderText  = "Observaciones";
                gridProveedores.Columns["activo"].HeaderText         = "Activo";

                gridProveedores.Columns["id_proveedor"].Width  = 40;
                gridProveedores.Columns["nombre"].Width         = 100;
                gridProveedores.Columns["apellido"].Width       = 100;
                gridProveedores.Columns["telefono"].Width       = 90;
                gridProveedores.Columns["dni"].Width            = 80;
                gridProveedores.Columns["email"].Width          = 140;
                gridProveedores.Columns["direccion"].Width      = 140;
                gridProveedores.Columns["cuit"].Width           = 90;
                gridProveedores.Columns["razon_social"].Width   = 120;
                gridProveedores.Columns["observaciones"].Width  = 160;
                gridProveedores.Columns["activo"].Width         = 55;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar proveedores:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  BOTÓN NUEVO
        // ─────────────────────────────────────────────
        private void Nuevo_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Guardar_Boton.Text = "Crear Proveedor";
        }

        // ─────────────────────────────────────────────
        //  BOTÓN GUARDAR (CREAR / EDITAR)
        // ─────────────────────────────────────────────
        private void Guardar_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text)   ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show("Completá Nombre, Apellido y DNI (son obligatorios).",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esNuevo = _idSeleccionado == null;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();
                using MySqlTransaction tx = conn.BeginTransaction();

                if (esNuevo)
                    CrearProveedor(conn, tx);
                else
                    EditarProveedor(conn, tx);

                tx.Commit();
                MessageBox.Show("Operación realizada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProveedores();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearProveedor(MySqlConnection conn, MySqlTransaction tx)
        {
            // Verificar DNI duplicado
            string checkQuery = "SELECT COUNT(*) FROM persona WHERE dni = @d";
            using (MySqlCommand check = new MySqlCommand(checkQuery, conn, tx))
            {
                check.Parameters.AddWithValue("@d", txtDni.Text.Trim());
                long count = (long)check.ExecuteScalar()!;
                if (count > 0)
                {
                    MessageBox.Show("Ya existe una persona registrada con ese DNI.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx.Rollback();
                    return;
                }
            }

            // El email es único en la base; si viene vacío, se guarda como NULL
            // para no chocar con otro proveedor que también lo deje vacío.
            object emailValue = string.IsNullOrWhiteSpace(txtEmail.Text)
                ? DBNull.Value
                : txtEmail.Text.Trim();

            if (emailValue is string email)
            {
                string checkEmail = "SELECT COUNT(*) FROM persona WHERE email = @e";
                using MySqlCommand checkE = new MySqlCommand(checkEmail, conn, tx);
                checkE.Parameters.AddWithValue("@e", email);
                long countE = (long)checkE.ExecuteScalar()!;
                if (countE > 0)
                {
                    MessageBox.Show("Ya existe una persona registrada con ese email.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx.Rollback();
                    return;
                }
            }

            string insertPersona = @"
                INSERT INTO persona (nombre, apellido, telefono, dni, email, direccion, cuit, razon_social, activo)
                VALUES (@n, @ap, @t, @d, @e, @dir, @cu, @rs, @a);
                SELECT LAST_INSERT_ID();";

            int idPersona;
            using (MySqlCommand cmd = new MySqlCommand(insertPersona, conn, tx))
            {
                cmd.Parameters.AddWithValue("@n",   txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@ap",  txtApellido.Text.Trim());
                cmd.Parameters.AddWithValue("@t",   txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@d",   txtDni.Text.Trim());
                cmd.Parameters.AddWithValue("@e",   emailValue);
                cmd.Parameters.AddWithValue("@dir", string.IsNullOrWhiteSpace(txtDireccion.Text) ? DBNull.Value : txtDireccion.Text.Trim());
                cmd.Parameters.AddWithValue("@cu",  string.IsNullOrWhiteSpace(txtCuit.Text) ? DBNull.Value : txtCuit.Text.Trim());
                cmd.Parameters.AddWithValue("@rs",  string.IsNullOrWhiteSpace(txtRazonSocial.Text) ? DBNull.Value : txtRazonSocial.Text.Trim());
                cmd.Parameters.AddWithValue("@a",   chkActivo.Checked ? 1 : 0);
                idPersona = Convert.ToInt32(cmd.ExecuteScalar());
            }

            string insertProveedor = @"
                INSERT INTO proveedor (observaciones, id_persona)
                VALUES (@obs, @idp)";

            using MySqlCommand cmdProv = new MySqlCommand(insertProveedor, conn, tx);
            cmdProv.Parameters.AddWithValue("@obs", string.IsNullOrWhiteSpace(txtObservaciones.Text) ? DBNull.Value : txtObservaciones.Text.Trim());
            cmdProv.Parameters.AddWithValue("@idp", idPersona);
            cmdProv.ExecuteNonQuery();
        }

        private void EditarProveedor(MySqlConnection conn, MySqlTransaction tx)
        {
            int id = _idSeleccionado!.Value;

            object emailValue = string.IsNullOrWhiteSpace(txtEmail.Text)
                ? DBNull.Value
                : txtEmail.Text.Trim();

            if (emailValue is string email)
            {
                string checkEmail = @"
                    SELECT COUNT(*) FROM persona p
                    JOIN proveedor pr ON pr.id_persona = p.id_persona
                    WHERE p.email = @e AND pr.id_proveedor <> @id";
                using MySqlCommand checkE = new MySqlCommand(checkEmail, conn, tx);
                checkE.Parameters.AddWithValue("@e", email);
                checkE.Parameters.AddWithValue("@id", id);
                long countE = (long)checkE.ExecuteScalar()!;
                if (countE > 0)
                {
                    MessageBox.Show("Ya existe otra persona registrada con ese email.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tx.Rollback();
                    return;
                }
            }

            string updatePersona = @"
                UPDATE persona p
                JOIN proveedor pr ON pr.id_persona = p.id_persona
                SET p.nombre        = @n,
                    p.apellido      = @ap,
                    p.telefono      = @t,
                    p.dni           = @d,
                    p.email         = @e,
                    p.direccion     = @dir,
                    p.cuit          = @cu,
                    p.razon_social  = @rs,
                    p.activo        = @a
                WHERE pr.id_proveedor = @id";

            using (MySqlCommand cmd = new MySqlCommand(updatePersona, conn, tx))
            {
                cmd.Parameters.AddWithValue("@n",   txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@ap",  txtApellido.Text.Trim());
                cmd.Parameters.AddWithValue("@t",   txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@d",   txtDni.Text.Trim());
                cmd.Parameters.AddWithValue("@e",   emailValue);
                cmd.Parameters.AddWithValue("@dir", string.IsNullOrWhiteSpace(txtDireccion.Text) ? DBNull.Value : txtDireccion.Text.Trim());
                cmd.Parameters.AddWithValue("@cu",  string.IsNullOrWhiteSpace(txtCuit.Text) ? DBNull.Value : txtCuit.Text.Trim());
                cmd.Parameters.AddWithValue("@rs",  string.IsNullOrWhiteSpace(txtRazonSocial.Text) ? DBNull.Value : txtRazonSocial.Text.Trim());
                cmd.Parameters.AddWithValue("@a",   chkActivo.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@id",  id);
                cmd.ExecuteNonQuery();
            }

            string updateProveedor = @"
                UPDATE proveedor
                SET observaciones = @obs
                WHERE id_proveedor = @id";

            using MySqlCommand cmdProv = new MySqlCommand(updateProveedor, conn, tx);
            cmdProv.Parameters.AddWithValue("@obs", string.IsNullOrWhiteSpace(txtObservaciones.Text) ? DBNull.Value : txtObservaciones.Text.Trim());
            cmdProv.Parameters.AddWithValue("@id",  id);
            cmdProv.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────
        //  BOTÓN ELIMINAR (BAJA LÓGICA)
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un proveedor de la lista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Desactivar al proveedor '{txtNombre.Text} {txtApellido.Text}'?\n(No se eliminará permanentemente)",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = @"
                    UPDATE persona p
                    JOIN proveedor pr ON pr.id_persona = p.id_persona
                    SET p.activo = 0
                    WHERE pr.id_proveedor = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Proveedor desactivado.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProveedores();
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
        private void gridProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = gridProveedores.Rows[e.RowIndex];

            _idSeleccionado        = Convert.ToInt32(row.Cells["id_proveedor"].Value);
            txtNombre.Text        = row.Cells["nombre"].Value?.ToString() ?? "";
            txtApellido.Text      = row.Cells["apellido"].Value?.ToString() ?? "";
            txtTelefono.Text      = row.Cells["telefono"].Value?.ToString() ?? "";
            txtDni.Text            = row.Cells["dni"].Value?.ToString() ?? "";
            txtEmail.Text          = row.Cells["email"].Value?.ToString() ?? "";
            txtDireccion.Text      = row.Cells["direccion"].Value?.ToString() ?? "";
            txtCuit.Text            = row.Cells["cuit"].Value?.ToString() ?? "";
            txtRazonSocial.Text     = row.Cells["razon_social"].Value?.ToString() ?? "";
            txtObservaciones.Text   = row.Cells["observaciones"].Value?.ToString() ?? "";

            chkActivo.Checked = Convert.ToBoolean(row.Cells["activo"].Value);
            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  BUSCAR
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridProveedores.DataSource is System.Data.DataTable dt)
            {
                string filtro = txtBuscar.Text.Replace("'", "''");
                dt.DefaultView.RowFilter =
                    $"nombre LIKE '%{filtro}%' OR apellido LIKE '%{filtro}%' OR dni LIKE '%{filtro}%' " +
                    $"OR telefono LIKE '%{filtro}%' OR email LIKE '%{filtro}%' OR razon_social LIKE '%{filtro}%'";
            }
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            _idSeleccionado          = null;
            txtNombre.Text         = "";
            txtApellido.Text       = "";
            txtTelefono.Text       = "";
            txtDni.Text             = "";
            txtEmail.Text           = "";
            txtDireccion.Text       = "";
            txtCuit.Text             = "";
            txtRazonSocial.Text      = "";
            txtObservaciones.Text    = "";
            chkActivo.Checked        = true;
            Guardar_Boton.Text       = "Crear Proveedor";
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
