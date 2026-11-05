using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionProveedores : Form
    {
        private int? _idSeleccionado;

        public FormGestionProveedores()
        {
            InitializeComponent();
            this.BindingContext ??= new BindingContext();
            gridProveedores.AutoGenerateColumns = true;
            gridProveedores.BindingContext = this.BindingContext;
            gridProveedores.DataBindingComplete += (_, _) => FormatearColumnasProveedores();
            this.Load += (_, _) => FormatearColumnasProveedores();
            CargarProveedores();
        }

        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

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

                this.BindingContext ??= new BindingContext();
                gridProveedores.AutoGenerateColumns = true;
                gridProveedores.BindingContext = this.BindingContext;
                gridProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridProveedores.DataSource = dt;

                FormatearColumnasProveedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasProveedores()
        {
            if (gridProveedores == null || gridProveedores.Columns.Count == 0) return;

            void Configurar(string colName, string header, int width)
            {
                if (gridProveedores.Columns[colName] is DataGridViewColumn col)
                {
                    col.HeaderText = header;
                    col.Width = width;
                }
            }

            Configurar("id_proveedor", "ID", 40);
            Configurar("nombre", "Nombre", 100);
            Configurar("apellido", "Apellido", 100);
            Configurar("telefono", "Teléfono", 90);
            Configurar("dni", "DNI", 80);
            Configurar("email", "Email", 140);
            Configurar("direccion", "Dirección", 140);
            Configurar("cuit", "CUIT", 90);
            Configurar("razon_social", "Razón Social", 120);
            Configurar("observaciones", "Observaciones", 160);
            Configurar("activo", "Activo", 55);
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

                bool exito = esNuevo
                    ? CrearProveedor(conn, tx)
                    : EditarProveedor(conn, tx);

                if (!exito)
                {
                    try { tx.Rollback(); } catch { }
                    return;
                }

                tx.Commit();
                MessageBox.Show("Operación realizada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProveedores();
                LimpiarFormulario();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Ya existe un proveedor o persona registrada con esos mismos datos (DNI, Email, CUIT o Razón Social duplicados).",
                    "Datos duplicados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDuplicadosProveedor(MySqlConnection conn, MySqlTransaction tx, int? idProveedorActual)
        {
            string dni = txtDni.Text.Trim();
            string email = txtEmail.Text.Trim();
            string cuit = txtCuit.Text.Trim();
            string razonSocial = txtRazonSocial.Text.Trim();
            string filtroExcluir = idProveedorActual.HasValue
                ? " AND id_persona <> (SELECT id_persona FROM proveedor WHERE id_proveedor = @idProv LIMIT 1)"
                : "";

            // 1. Verificar DNI duplicado
            string checkDni = "SELECT COUNT(*) FROM persona WHERE dni = @d" + filtroExcluir;
            using (MySqlCommand cmdDni = new MySqlCommand(checkDni, conn, tx))
            {
                cmdDni.Parameters.AddWithValue("@d", dni);
                if (idProveedorActual.HasValue) cmdDni.Parameters.AddWithValue("@idProv", idProveedorActual.Value);
                long count = Convert.ToInt64(cmdDni.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show($"Ya existe un registro con el DNI '{dni}'.",
                        "Proveedor ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // 2. Verificar Email duplicado (si se ingresó)
            if (!string.IsNullOrWhiteSpace(email))
            {
                string checkEmail = "SELECT COUNT(*) FROM persona WHERE email = @e" + filtroExcluir;
                using MySqlCommand cmdEmail = new MySqlCommand(checkEmail, conn, tx);
                cmdEmail.Parameters.AddWithValue("@e", email);
                if (idProveedorActual.HasValue) cmdEmail.Parameters.AddWithValue("@idProv", idProveedorActual.Value);
                long countE = Convert.ToInt64(cmdEmail.ExecuteScalar());
                if (countE > 0)
                {
                    MessageBox.Show($"Ya existe un registro con el email '{email}'.",
                        "Email ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // 3. Verificar CUIT duplicado (si se ingresó)
            if (!string.IsNullOrWhiteSpace(cuit))
            {
                string checkCuit = "SELECT COUNT(*) FROM persona WHERE cuit = @cu" + filtroExcluir;
                using MySqlCommand cmdCuit = new MySqlCommand(checkCuit, conn, tx);
                cmdCuit.Parameters.AddWithValue("@cu", cuit);
                if (idProveedorActual.HasValue) cmdCuit.Parameters.AddWithValue("@idProv", idProveedorActual.Value);
                long countC = Convert.ToInt64(cmdCuit.ExecuteScalar());
                if (countC > 0)
                {
                    MessageBox.Show($"Ya existe un registro con el CUIT '{cuit}'.",
                        "CUIT ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // 4. Verificar Razón Social duplicada en proveedores (si se ingresó)
            if (!string.IsNullOrWhiteSpace(razonSocial))
            {
                string checkRS = @"
                    SELECT COUNT(*) FROM persona p
                    JOIN proveedor pr ON pr.id_persona = p.id_persona
                    WHERE p.razon_social = @rs" + (idProveedorActual.HasValue ? " AND pr.id_proveedor <> @idProv" : "");
                using MySqlCommand cmdRS = new MySqlCommand(checkRS, conn, tx);
                cmdRS.Parameters.AddWithValue("@rs", razonSocial);
                if (idProveedorActual.HasValue) cmdRS.Parameters.AddWithValue("@idProv", idProveedorActual.Value);
                long countRS = Convert.ToInt64(cmdRS.ExecuteScalar());
                if (countRS > 0)
                {
                    MessageBox.Show($"Ya existe un proveedor registrado con la Razón Social '{razonSocial}'.",
                        "Razón Social ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private bool CrearProveedor(MySqlConnection conn, MySqlTransaction tx)
        {
            if (!ValidarDuplicadosProveedor(conn, tx, null))
                return false;

            object emailValue = string.IsNullOrWhiteSpace(txtEmail.Text)
                ? DBNull.Value
                : txtEmail.Text.Trim();

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
            return true;
        }

        private bool EditarProveedor(MySqlConnection conn, MySqlTransaction tx)
        {
            int id = _idSeleccionado!.Value;

            if (!ValidarDuplicadosProveedor(conn, tx, id))
                return false;

            object emailValue = string.IsNullOrWhiteSpace(txtEmail.Text)
                ? DBNull.Value
                : txtEmail.Text.Trim();

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
            return true;
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
            catch (Exception ex)
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
            if (e.RowIndex < 0 || e.RowIndex >= gridProveedores.Rows.Count) return;
            var row = gridProveedores.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var idVal = gridProveedores.Columns.Contains("id_proveedor") ? row.Cells["id_proveedor"]?.Value : (row.Cells.Count > 0 ? row.Cells[0]?.Value : null);
            if (idVal == null || idVal == DBNull.Value) return;

            string LeerCelda(string col) => gridProveedores.Columns.Contains(col) ? (row.Cells[col]?.Value?.ToString() ?? "") : "";

            _idSeleccionado        = Convert.ToInt32(idVal);
            txtNombre.Text        = LeerCelda("nombre");
            txtApellido.Text      = LeerCelda("apellido");
            txtTelefono.Text      = LeerCelda("telefono");
            txtDni.Text            = LeerCelda("dni");
            txtEmail.Text          = LeerCelda("email");
            txtDireccion.Text      = LeerCelda("direccion");
            txtCuit.Text            = LeerCelda("cuit");
            txtRazonSocial.Text     = LeerCelda("razon_social");
            txtObservaciones.Text   = LeerCelda("observaciones");

            var actVal = gridProveedores.Columns.Contains("activo") ? row.Cells["activo"]?.Value : null;
            chkActivo.Checked = actVal != null && actVal != DBNull.Value && Convert.ToBoolean(actVal);
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
