using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Cantina_Padel
{
    public partial class FormGestionCanchas : Form
    {
        private int? _idSeleccionado;

        public FormGestionCanchas()
        {
            InitializeComponent();
            this.BindingContext ??= new BindingContext();
            gridCanchas.AutoGenerateColumns = true;
            gridCanchas.BindingContext = this.BindingContext;
            gridCanchas.DataBindingComplete += (_, _) => FormatearColumnasCanchas();
            this.Load += (_, _) => FormatearColumnasCanchas();
            CargarCanchas();
        }

        private void Precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '.')
                return;

            e.Handled = true;
        }

        private void NombreCancha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AplicarReglaActivoSegunEstado()
        {
            string estado = cmbEstado.SelectedItem?.ToString() ?? "";

            if (estado == "Fuera de Servicio" || estado == "En Reparación")
            {
                chkActivo.Checked = false;
                chkActivo.Enabled = false;
            }
            else
            {
                chkActivo.Enabled = true;
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarReglaActivoSegunEstado();
        }

        // ─────────────────────────────────────────────
        //  CARGAR GRILLA
        // ─────────────────────────────────────────────
        private void CargarCanchas()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "SELECT id_cancha, nombre, estado, precio, activo FROM cancha ORDER BY id_cancha";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                this.BindingContext ??= new BindingContext();
                gridCanchas.AutoGenerateColumns = true;
                gridCanchas.BindingContext = this.BindingContext;
                gridCanchas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridCanchas.DataSource = dt;

                FormatearColumnasCanchas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar canchas:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasCanchas()
        {
            if (gridCanchas == null || gridCanchas.Columns.Count == 0) return;

            if (gridCanchas.Columns["id_cancha"] is DataGridViewColumn colId)
            {
                colId.HeaderText = "ID";
                colId.Width = 40;
            }
            if (gridCanchas.Columns["nombre"] is DataGridViewColumn colNom)
            {
                colNom.HeaderText = "Nombre";
                colNom.Width = 150;
            }
            if (gridCanchas.Columns["estado"] is DataGridViewColumn colEst)
            {
                colEst.HeaderText = "Estado";
                colEst.Width = 120;
            }
            if (gridCanchas.Columns["precio"] is DataGridViewColumn colPre)
            {
                colPre.HeaderText = "Precio";
                colPre.Width = 80;
            }
            if (gridCanchas.Columns["activo"] is DataGridViewColumn colAct)
            {
                colAct.HeaderText = "Activo";
                colAct.Width = 55;
            }
        }

        // ─────────────────────────────────────────────
        //  BOTÓN NUEVO
        // ─────────────────────────────────────────────
        private void Nuevo_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // ─────────────────────────────────────────────
        //  BOTÓN GUARDAR (CREAR / EDITAR)
        // ─────────────────────────────────────────────
        private void Guardar_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || cmbEstado.SelectedItem == null)
            {
                MessageBox.Show("Completá el nombre y el estado de la cancha.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNombre.Text.Length > 0 && (char.IsWhiteSpace(txtNombre.Text[0]) || char.IsWhiteSpace(txtNombre.Text[txtNombre.Text.Length - 1])))
            {
                MessageBox.Show("El nombre no puede empezar ni terminar con espacios en blanco.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBox1.Text, out decimal precio) || precio < 0)
            {
                MessageBox.Show("Ingresá un precio válido mayor o igual a 0.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            bool esNuevo = _idSeleccionado == null;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                bool exito = esNuevo
                    ? CrearCancha(conn)
                    : EditarCancha(conn);

                if (!exito) return;

                MessageBox.Show("Cancha guardada con éxito.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCanchas();
                LimpiarFormulario();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Ya existe una cancha con ese nombre.",
                    "Cancha duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CrearCancha(MySqlConnection conn)
        {
            string nombre = txtNombre.Text.Trim();
            string check = "SELECT COUNT(*) FROM cancha WHERE nombre = @n";
            using (MySqlCommand cmdCheck = new MySqlCommand(check, conn))
            {
                cmdCheck.Parameters.AddWithValue("@n", nombre);
                long count = Convert.ToInt64(cmdCheck.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Ya existe una cancha con ese nombre.",
                        "Cancha ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            string estado = cmbEstado.SelectedItem!.ToString()!;
            bool activo = estado == "Activo" && chkActivo.Checked;

            string insert = "INSERT INTO cancha (nombre, estado, precio, activo) VALUES (@n, @e, @p, @a)";
            using MySqlCommand cmd = new MySqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@n", nombre);
            cmd.Parameters.AddWithValue("@e", estado);
            cmd.Parameters.AddWithValue("@p", decimal.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@a", activo ? 1 : 0);
            cmd.ExecuteNonQuery();
            return true;
        }

        private bool EditarCancha(MySqlConnection conn)
        {
            int id = _idSeleccionado!.Value;
            string nombre = txtNombre.Text.Trim();

            string check = "SELECT COUNT(*) FROM cancha WHERE nombre = @n AND id_cancha <> @id";
            using (MySqlCommand cmdCheck = new MySqlCommand(check, conn))
            {
                cmdCheck.Parameters.AddWithValue("@n", nombre);
                cmdCheck.Parameters.AddWithValue("@id", id);
                long count = Convert.ToInt64(cmdCheck.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Ya existe otra cancha con ese nombre.",
                        "Cancha ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            string estado = cmbEstado.SelectedItem!.ToString()!;
            bool activo = estado == "Activo" && chkActivo.Checked;

            string update = "UPDATE cancha SET nombre = @n, estado = @e, precio = @p, activo = @a WHERE id_cancha = @id";
            using MySqlCommand cmd = new MySqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@n",  nombre);
            cmd.Parameters.AddWithValue("@e",  estado);
            cmd.Parameters.AddWithValue("@p",  decimal.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@a",  activo ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", id);
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
                MessageBox.Show("Seleccioná una cancha de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Desactivar la cancha '{txtNombre.Text}'?\n(No se eliminará permanentemente)",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                DesactivarCancha(_idSeleccionado.Value);
            }
        }

        private void DesactivarCancha(int id)
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "UPDATE cancha SET activo = 0 WHERE id_cancha = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cancha desactivada.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCanchas();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  SELECCIONAR FILA EN GRILLA
        // ─────────────────────────────────────────────
        private void gridCanchas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= gridCanchas.Rows.Count) return;
            var row = gridCanchas.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var idVal = gridCanchas.Columns.Contains("id_cancha") ? row.Cells["id_cancha"]?.Value : (row.Cells.Count > 0 ? row.Cells[0]?.Value : null);
            if (idVal == null || idVal == DBNull.Value) return;

            _idSeleccionado   = Convert.ToInt32(idVal);
            txtNombre.Text    = gridCanchas.Columns.Contains("nombre") ? (row.Cells["nombre"]?.Value?.ToString() ?? "") : "";
            cmbEstado.SelectedItem = gridCanchas.Columns.Contains("estado") ? (row.Cells["estado"]?.Value?.ToString() ?? "Activo") : "Activo";
            var precioVal = gridCanchas.Columns.Contains("precio") ? row.Cells["precio"]?.Value : null;
            textBox1.Text = precioVal == null || precioVal == DBNull.Value
                ? "0.00"
                : Convert.ToDecimal(precioVal).ToString("0.00");
            var activoVal = gridCanchas.Columns.Contains("activo") ? row.Cells["activo"]?.Value : null;
            chkActivo.Checked = activoVal != null && activoVal != DBNull.Value && Convert.ToBoolean(activoVal);
            AplicarReglaActivoSegunEstado();
            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  BUSCAR
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridCanchas.DataSource is System.Data.DataTable dt)
            {
                string filtro = txtBuscar.Text.Replace("'", "''");
                dt.DefaultView.RowFilter = $"nombre LIKE '%{filtro}%' OR estado LIKE '%{filtro}%'";
            }
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            _idSeleccionado    = null;
            txtNombre.Text     = "";
            if (cmbEstado.Items.Count > 0) cmbEstado.SelectedIndex = 0;
            textBox1.Text = "0.00";
            chkActivo.Checked  = true;
            Guardar_Boton.Text = "Crear Cancha";
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
