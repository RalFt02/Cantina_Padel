using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionClientes : Form
    {
        private int? _idSeleccionado;

        public FormGestionClientes()
        {
            InitializeComponent();
            this.BindingContext ??= new BindingContext();
            gridClientes.AutoGenerateColumns = true;
            gridClientes.BindingContext = this.BindingContext;
            gridClientes.DataBindingComplete += (_, _) => FormatearColumnasClientes();
            this.Load += (_, _) => FormatearColumnasClientes();
            if (cmbCondicionFiscal.Items.Count > 0)
                cmbCondicionFiscal.SelectedIndex = 0;
            ActualizarVisibilidadCamposFiscales();
            CargarClientes();
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
        private void CargarClientes()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();
                AsegurarColumnaCondicionFiscal(conn);

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
                           COALESCE(p.condicion_fiscal, CASE WHEN p.razon_social IS NOT NULL AND p.razon_social <> '' THEN 'Responsable Inscripto' ELSE 'Consumidor Final' END) AS condicion_fiscal,
                           p.activo
                    FROM cliente c
                    JOIN persona p ON p.id_persona = c.id_persona
                    ORDER BY c.id_cliente";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                this.BindingContext ??= new BindingContext();
                gridClientes.AutoGenerateColumns = true;
                gridClientes.BindingContext = this.BindingContext;
                gridClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridClientes.DataSource = dt;

                FormatearColumnasClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasClientes()
        {
            if (gridClientes == null || gridClientes.Columns.Count == 0) return;

            void Configurar(string colName, string header, int width)
            {
                if (gridClientes.Columns[colName] is DataGridViewColumn col)
                {
                    col.HeaderText = header;
                    col.Width = width;
                }
            }

            Configurar("id_cliente", "ID", 40);
            Configurar("nombre", "Nombre", 100);
            Configurar("apellido", "Apellido", 100);
            Configurar("telefono", "Teléfono", 90);
            Configurar("dni", "DNI", 80);
            Configurar("email", "Email", 140);
            Configurar("direccion", "Dirección", 140);
            Configurar("cuit", "CUIT", 90);
            Configurar("razon_social", "Razón Social", 120);
            Configurar("condicion_fiscal", "Condición", 110);
            Configurar("activo", "Activo", 55);
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

            bool esNuevo = _idSeleccionado == null;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();
                using MySqlTransaction tx = conn.BeginTransaction();

                bool exito = esNuevo
                    ? CrearCliente(conn, tx)
                    : EditarCliente(conn, tx);

                if (!exito)
                {
                    try { tx.Rollback(); } catch { }
                    return;
                }

                tx.Commit();
                MessageBox.Show("Operación realizada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarClientes();
                LimpiarFormulario();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Ya existe un cliente o persona registrada con esos mismos datos (DNI, Email o CUIT duplicado).",
                    "Datos duplicados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDuplicadosPersona(MySqlConnection conn, MySqlTransaction tx, int? idClienteActual)
        {
            string dni = txtDni.Text.Trim();
            string email = txtEmail.Text.Trim();
            string cuit = txtCuit.Text.Trim();
            string filtroExcluir = idClienteActual.HasValue
                ? " AND id_persona <> (SELECT id_persona FROM cliente WHERE id_cliente = @idCli LIMIT 1)"
                : "";

            // 1. Verificar DNI duplicado
            string checkDni = "SELECT COUNT(*) FROM persona WHERE dni = @d" + filtroExcluir;
            using (MySqlCommand cmdDni = new MySqlCommand(checkDni, conn, tx))
            {
                cmdDni.Parameters.AddWithValue("@d", dni);
                if (idClienteActual.HasValue) cmdDni.Parameters.AddWithValue("@idCli", idClienteActual.Value);
                long count = Convert.ToInt64(cmdDni.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show($"Ya existe un registro con el DNI '{dni}'.",
                        "Cliente ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // 2. Verificar Email duplicado (si se ingresó)
            if (!string.IsNullOrWhiteSpace(email))
            {
                string checkEmail = "SELECT COUNT(*) FROM persona WHERE email = @e" + filtroExcluir;
                using MySqlCommand cmdEmail = new MySqlCommand(checkEmail, conn, tx);
                cmdEmail.Parameters.AddWithValue("@e", email);
                if (idClienteActual.HasValue) cmdEmail.Parameters.AddWithValue("@idCli", idClienteActual.Value);
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
                if (idClienteActual.HasValue) cmdCuit.Parameters.AddWithValue("@idCli", idClienteActual.Value);
                long countC = Convert.ToInt64(cmdCuit.ExecuteScalar());
                if (countC > 0)
                {
                    MessageBox.Show($"Ya existe un registro con el CUIT '{cuit}'.",
                        "CUIT ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private bool CrearCliente(MySqlConnection conn, MySqlTransaction tx)
        {
            if (!ValidarDuplicadosPersona(conn, tx, null))
                return false;

            object emailValue = string.IsNullOrWhiteSpace(txtEmail.Text)
                ? DBNull.Value
                : txtEmail.Text.Trim();

            string insertPersona = @"
                INSERT INTO persona (nombre, apellido, telefono, dni, email, direccion, cuit, razon_social, condicion_fiscal, activo)
                VALUES (@n, @ap, @t, @d, @e, @dir, @cu, @rs, @cf, @a);
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
                cmd.Parameters.AddWithValue("@cf",  cmbCondicionFiscal.SelectedItem?.ToString() ?? "Consumidor Final");
                cmd.Parameters.AddWithValue("@a",   chkActivo.Checked ? 1 : 0);
                idPersona = Convert.ToInt32(cmd.ExecuteScalar());
            }

            string insertCliente = @"
                INSERT INTO cliente (fecha_alta, id_persona)
                VALUES (CURDATE(), @idp)";
            using MySqlCommand cmdCliente = new MySqlCommand(insertCliente, conn, tx);
            cmdCliente.Parameters.AddWithValue("@idp", idPersona);
            cmdCliente.ExecuteNonQuery();
            return true;
        }

        private bool EditarCliente(MySqlConnection conn, MySqlTransaction tx)
        {
            int id = _idSeleccionado!.Value;

            if (!ValidarDuplicadosPersona(conn, tx, id))
                return false;

            object emailValue = string.IsNullOrWhiteSpace(txtEmail.Text)
                ? DBNull.Value
                : txtEmail.Text.Trim();

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
                    p.condicion_fiscal = @cf,
                    p.activo        = @a
                WHERE c.id_cliente = @id";

            using MySqlCommand cmd = new MySqlCommand(updatePersona, conn, tx);
            cmd.Parameters.AddWithValue("@n",   txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@ap",  txtApellido.Text.Trim());
            cmd.Parameters.AddWithValue("@t",   txtTelefono.Text.Trim());
            cmd.Parameters.AddWithValue("@d",   txtDni.Text.Trim());
            cmd.Parameters.AddWithValue("@e",   emailValue);
            cmd.Parameters.AddWithValue("@dir", string.IsNullOrWhiteSpace(txtDireccion.Text) ? DBNull.Value : txtDireccion.Text.Trim());
            cmd.Parameters.AddWithValue("@cu",  string.IsNullOrWhiteSpace(txtCuit.Text) ? DBNull.Value : txtCuit.Text.Trim());
            cmd.Parameters.AddWithValue("@rs",  string.IsNullOrWhiteSpace(txtRazonSocial.Text) ? DBNull.Value : txtRazonSocial.Text.Trim());
            cmd.Parameters.AddWithValue("@cf",  cmbCondicionFiscal.SelectedItem?.ToString() ?? "Consumidor Final");
            cmd.Parameters.AddWithValue("@a",   chkActivo.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@id",  id);
            cmd.ExecuteNonQuery();
            return true;
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
            catch (Exception ex)
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
            if (e.RowIndex < 0 || e.RowIndex >= gridClientes.Rows.Count) return;
            var row = gridClientes.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var idVal = gridClientes.Columns.Contains("id_cliente") ? row.Cells["id_cliente"]?.Value : (row.Cells.Count > 0 ? row.Cells[0]?.Value : null);
            if (idVal == null || idVal == DBNull.Value) return;

            string LeerCelda(string col) => gridClientes.Columns.Contains(col) ? (row.Cells[col]?.Value?.ToString() ?? "") : "";

            _idSeleccionado    = Convert.ToInt32(idVal);
            txtNombre.Text     = LeerCelda("nombre");
            txtApellido.Text   = LeerCelda("apellido");
            txtTelefono.Text   = LeerCelda("telefono");
            txtDni.Text        = LeerCelda("dni");
            txtEmail.Text      = LeerCelda("email");
            txtDireccion.Text  = LeerCelda("direccion");
            txtCuit.Text       = LeerCelda("cuit");
            txtRazonSocial.Text = LeerCelda("razon_social");

            string cond = LeerCelda("condicion_fiscal");
            if (string.IsNullOrWhiteSpace(cond))
            {
                string rs = LeerCelda("razon_social");
                cond = !string.IsNullOrWhiteSpace(rs) ? "Responsable Inscripto" : "Consumidor Final";
            }
            cmbCondicionFiscal.SelectedItem = cond;
            ActualizarVisibilidadCamposFiscales();

            var actVal = gridClientes.Columns.Contains("activo") ? row.Cells["activo"]?.Value : null;
            chkActivo.Checked = actVal != null && actVal != DBNull.Value && Convert.ToBoolean(actVal);
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
                    $"OR telefono LIKE '%{filtro}%' OR email LIKE '%{filtro}%' OR razon_social LIKE '%{filtro}%' OR condicion_fiscal LIKE '%{filtro}%'";
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
            if (cmbCondicionFiscal.Items.Count > 0) cmbCondicionFiscal.SelectedIndex = 0;
            ActualizarVisibilidadCamposFiscales();
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
        private void cmbCondicionFiscal_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarVisibilidadCamposFiscales();
        }

        private void ActualizarVisibilidadCamposFiscales()
        {
            string seleccion = cmbCondicionFiscal.SelectedItem?.ToString() ?? "Consumidor Final";
            bool requiereDatosFiscales = seleccion == "Monotributista" || seleccion == "Responsable Inscripto";

            lblCuit.Visible = requiereDatosFiscales;
            txtCuit.Visible = requiereDatosFiscales;
            lblRazonSocial.Visible = requiereDatosFiscales;
            txtRazonSocial.Visible = requiereDatosFiscales;

            if (!requiereDatosFiscales)
            {
                txtCuit.Text = "";
                txtRazonSocial.Text = "";
            }
        }

        private void AsegurarColumnaCondicionFiscal(MySqlConnection conn)
        {
            try
            {
                using MySqlCommand cmd = new MySqlCommand("ALTER TABLE persona ADD COLUMN condicion_fiscal VARCHAR(50) DEFAULT 'Consumidor Final'", conn);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
    }
}
