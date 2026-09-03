using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionClientes : Form
    {
        // Guarda el ID del cliente seleccionado en la grilla (null = alta nueva).
        // No usamos un TextBox visible para esto, así no aparece ni en tiempo de
        // ejecución ni en el lienzo de diseño de Visual Studio.
        private int? _idSeleccionado;

        public FormGestionClientes()
        {
            InitializeComponent();
            CargarClientes();
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

        // Permite dígitos, un único punto decimal y teclas de control (backspace, etc.)
        private void SoloNumerosDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;
            if (char.IsControl(e.KeyChar)) return;

            if (e.KeyChar == '.' && !txt.Text.Contains('.'))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // ─────────────────────────────────────────────
        //  CARGAR GRILLA
        // ─────────────────────────────────────────────
        private void CargarClientes()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = @"
                    SELECT c.id_cliente,
                           p.nombre,
                           p.apellido,
                           p.telefono,
                           p.dni,
                           p.email,
                           p.direccion,
                           p.cuit,
                           p.razon_social,
                           IFNULL(cc.saldo, 0) AS saldo,
                           p.activo
                    FROM cliente c
                    JOIN persona p ON p.id_persona = c.id_persona
                    LEFT JOIN cuenta_corriente cc ON cc.id_cliente = c.id_cliente
                    ORDER BY c.id_cliente";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                gridClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridClientes.DataSource = dt;

                // Personalizar columnas
                gridClientes.Columns["id_cliente"].HeaderText   = "ID";
                gridClientes.Columns["nombre"].HeaderText        = "Nombre";
                gridClientes.Columns["apellido"].HeaderText      = "Apellido";
                gridClientes.Columns["telefono"].HeaderText      = "Teléfono";
                gridClientes.Columns["dni"].HeaderText           = "DNI";
                gridClientes.Columns["email"].HeaderText         = "Email";
                gridClientes.Columns["direccion"].HeaderText     = "Dirección";
                gridClientes.Columns["cuit"].HeaderText          = "CUIT";
                gridClientes.Columns["razon_social"].HeaderText  = "Razón Social";
                gridClientes.Columns["saldo"].HeaderText         = "Cuenta Corriente";
                gridClientes.Columns["activo"].HeaderText        = "Activo";

                gridClientes.Columns["id_cliente"].Width  = 40;
                gridClientes.Columns["nombre"].Width       = 100;
                gridClientes.Columns["apellido"].Width     = 100;
                gridClientes.Columns["telefono"].Width     = 90;
                gridClientes.Columns["dni"].Width          = 80;
                gridClientes.Columns["email"].Width        = 140;
                gridClientes.Columns["direccion"].Width    = 140;
                gridClientes.Columns["cuit"].Width         = 90;
                gridClientes.Columns["razon_social"].Width = 120;
                gridClientes.Columns["saldo"].Width        = 110;
                gridClientes.Columns["saldo"].DefaultCellStyle.Format = "C2";
                gridClientes.Columns["activo"].Width       = 55;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar clientes:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  BOTÓN NUEVO
        // ─────────────────────────────────────────────
        private void Nuevo_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Guardar_Boton.Text = "Crear Cliente";
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

            if (!decimal.TryParse(txtSaldo.Text, out decimal saldo))
            {
                MessageBox.Show("El saldo de cuenta corriente debe ser un número válido.",
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
                    CrearCliente(conn, tx, saldo);
                else
                    EditarCliente(conn, tx, saldo);

                tx.Commit();
                MessageBox.Show("Operación realizada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarClientes();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearCliente(MySqlConnection conn, MySqlTransaction tx, decimal saldo)
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
            // para no chocar con otro cliente que también lo deje vacío.
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

            string insertCliente = @"
                INSERT INTO cliente (fecha_alta, id_persona)
                VALUES (CURDATE(), @idp);
                SELECT LAST_INSERT_ID();";

            int idCliente;
            using (MySqlCommand cmd = new MySqlCommand(insertCliente, conn, tx))
            {
                cmd.Parameters.AddWithValue("@idp", idPersona);
                idCliente = Convert.ToInt32(cmd.ExecuteScalar());
            }

            string insertCuenta = @"
                INSERT INTO cuenta_corriente (saldo, id_cliente)
                VALUES (@s, @idc)";
            using MySqlCommand cmdCuenta = new MySqlCommand(insertCuenta, conn, tx);
            cmdCuenta.Parameters.AddWithValue("@s",   saldo);
            cmdCuenta.Parameters.AddWithValue("@idc", idCliente);
            cmdCuenta.ExecuteNonQuery();
        }

        private void EditarCliente(MySqlConnection conn, MySqlTransaction tx, decimal saldo)
        {
            int id = _idSeleccionado!.Value;

            object emailValue = string.IsNullOrWhiteSpace(txtEmail.Text)
                ? DBNull.Value
                : txtEmail.Text.Trim();

            if (emailValue is string email)
            {
                string checkEmail = @"
                    SELECT COUNT(*) FROM persona p
                    JOIN cliente c ON c.id_persona = p.id_persona
                    WHERE p.email = @e AND c.id_cliente <> @id";
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
                JOIN cliente c ON c.id_persona = p.id_persona
                SET p.nombre        = @n,
                    p.apellido      = @ap,
                    p.telefono      = @t,
                    p.dni           = @d,
                    p.email         = @e,
                    p.direccion     = @dir,
                    p.cuit          = @cu,
                    p.razon_social  = @rs,
                    p.activo        = @a
                WHERE c.id_cliente = @id";

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

            // La cuenta corriente puede no existir todavía (por datos viejos) -> upsert
            string upsertCuenta = @"
                INSERT INTO cuenta_corriente (saldo, id_cliente)
                VALUES (@s, @id)
                ON DUPLICATE KEY UPDATE saldo = @s";
            using MySqlCommand cmdCuenta = new MySqlCommand(upsertCuenta, conn, tx);
            cmdCuenta.Parameters.AddWithValue("@s",  saldo);
            cmdCuenta.Parameters.AddWithValue("@id", id);
            cmdCuenta.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────
        //  BOTÓN ELIMINAR (BAJA LÓGICA)
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un cliente de la lista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Desactivar al cliente '{txtNombre.Text} {txtApellido.Text}'?\n(No se eliminará permanentemente)",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = @"
                    UPDATE persona p
                    JOIN cliente c ON c.id_persona = p.id_persona
                    SET p.activo = 0
                    WHERE c.id_cliente = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cliente desactivado.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarClientes();
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
        private void gridClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = gridClientes.Rows[e.RowIndex];

            _idSeleccionado    = Convert.ToInt32(row.Cells["id_cliente"].Value);
            txtNombre.Text     = row.Cells["nombre"].Value?.ToString() ?? "";
            txtApellido.Text   = row.Cells["apellido"].Value?.ToString() ?? "";
            txtTelefono.Text   = row.Cells["telefono"].Value?.ToString() ?? "";
            txtDni.Text        = row.Cells["dni"].Value?.ToString() ?? "";
            txtEmail.Text      = row.Cells["email"].Value?.ToString() ?? "";
            txtDireccion.Text  = row.Cells["direccion"].Value?.ToString() ?? "";
            txtCuit.Text       = row.Cells["cuit"].Value?.ToString() ?? "";
            txtRazonSocial.Text = row.Cells["razon_social"].Value?.ToString() ?? "";

            decimal saldo = row.Cells["saldo"].Value != null
                ? Convert.ToDecimal(row.Cells["saldo"].Value)
                : 0m;
            txtSaldo.Text = saldo.ToString("0.00");

            chkActivo.Checked = Convert.ToBoolean(row.Cells["activo"].Value);
            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  BUSCAR
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridClientes.DataSource is System.Data.DataTable dt)
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
            _idSeleccionado     = null;
            txtNombre.Text      = "";
            txtApellido.Text    = "";
            txtTelefono.Text    = "";
            txtDni.Text          = "";
            txtEmail.Text        = "";
            txtDireccion.Text    = "";
            txtCuit.Text         = "";
            txtRazonSocial.Text  = "";
            txtSaldo.Text        = "0.00";
            chkActivo.Checked    = true;
            Guardar_Boton.Text   = "Crear Cliente";
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
