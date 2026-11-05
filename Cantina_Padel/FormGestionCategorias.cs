using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionCategorias : Form
    {
        private int? _idSeleccionado;

        public FormGestionCategorias()
        {
            InitializeComponent();
            this.BindingContext ??= new BindingContext();
            gridCategorias.AutoGenerateColumns = true;
            gridCategorias.BindingContext = this.BindingContext;
            gridCategorias.DataBindingComplete += (_, _) => FormatearColumnasCategorias();
            this.Load += (_, _) => FormatearColumnasCategorias();
            CargarCategorias();
        }

        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

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
        private void CargarCategorias()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "SELECT id_categoria, nombre, porcentaje_ganancia, activo FROM categoria ORDER BY nombre";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                this.BindingContext ??= new BindingContext();
                gridCategorias.AutoGenerateColumns = true;
                gridCategorias.BindingContext = this.BindingContext;
                gridCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridCategorias.DataSource = dt;

                FormatearColumnasCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasCategorias()
        {
            if (gridCategorias == null || gridCategorias.Columns.Count == 0) return;

            if (gridCategorias.Columns["id_categoria"] is DataGridViewColumn colId)
            {
                colId.HeaderText = "ID";
                colId.Width = 40;
            }
            if (gridCategorias.Columns["nombre"] is DataGridViewColumn colNom)
            {
                colNom.HeaderText = "Nombre";
                colNom.Width = 180;
            }
            if (gridCategorias.Columns["porcentaje_ganancia"] is DataGridViewColumn colPg)
            {
                colPg.HeaderText = "% Ganancia";
                colPg.Width = 90;
            }
            if (gridCategorias.Columns["activo"] is DataGridViewColumn colAct)
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
            Guardar_Boton.Text = "Crear Categoría";
        }

        // ─────────────────────────────────────────────
        //  BOTÓN GUARDAR (CREAR / EDITAR)
        // ─────────────────────────────────────────────
        private void Guardar_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de la categoría es obligatorio.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPorcentaje.Text, out decimal porcentaje) || porcentaje < 0)
            {
                MessageBox.Show("El porcentaje de ganancia debe ser un número válido (0 o mayor).",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esNuevo = _idSeleccionado == null;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                if (esNuevo)
                    CrearCategoria(conn, porcentaje);
                else
                    EditarCategoria(conn, porcentaje);
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Ya existe una categoría con ese nombre.",
                    "Categoría duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearCategoria(MySqlConnection conn, decimal porcentaje)
        {
            string checkQuery = "SELECT COUNT(*) FROM categoria WHERE nombre = @n";
            using (MySqlCommand check = new MySqlCommand(checkQuery, conn))
            {
                check.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
                long count = Convert.ToInt64(check.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Ya existe una categoría con ese nombre.",
                        "Categoría ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string insert = "INSERT INTO categoria (nombre, porcentaje_ganancia, activo) VALUES (@n, @pg, @a)";
            using MySqlCommand cmd = new MySqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@n",  txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@pg", porcentaje);
            cmd.Parameters.AddWithValue("@a",  chkActivo.Checked ? 1 : 0);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Categoría creada con éxito.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarCategorias();
            LimpiarFormulario();
        }

        private void EditarCategoria(MySqlConnection conn, decimal porcentaje)
        {
            int id = _idSeleccionado!.Value;

            string checkQuery = "SELECT COUNT(*) FROM categoria WHERE nombre = @n AND id_categoria <> @id";
            using (MySqlCommand check = new MySqlCommand(checkQuery, conn))
            {
                check.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
                check.Parameters.AddWithValue("@id", id);
                long count = Convert.ToInt64(check.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Ya existe otra categoría con ese nombre.",
                        "Categoría ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!chkActivo.Checked)
            {
                string countProdQuery = "SELECT COUNT(*) FROM producto WHERE id_categoria = @id AND activo = 1";
                using MySqlCommand cmdCount = new MySqlCommand(countProdQuery, conn);
                cmdCount.Parameters.AddWithValue("@id", id);
                long productosAfectados = Convert.ToInt64(cmdCount.ExecuteScalar());

                if (productosAfectados > 0)
                {
                    var confirm = MessageBox.Show(
                        $"Hay {productosAfectados} producto(s) activo(s) con esta categoría.\n" +
                        "Van a seguir teniéndola asignada, pero la categoría dejará de estar disponible " +
                        "para elegir en nuevos productos.\n\n¿Continuar?",
                        "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirm != DialogResult.Yes) return;
                }
            }

            string update = "UPDATE categoria SET nombre = @n, porcentaje_ganancia = @pg, activo = @a WHERE id_categoria = @id";
            using MySqlCommand cmd = new MySqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@n",  txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@pg", porcentaje);
            cmd.Parameters.AddWithValue("@a",  chkActivo.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Categoría actualizada con éxito.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarCategorias();
            LimpiarFormulario();
        }

        // ─────────────────────────────────────────────
        //  BOTÓN ELIMINAR (BAJA LÓGICA)
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná una categoría de la lista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = _idSeleccionado.Value;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string countProdQuery = "SELECT COUNT(*) FROM producto WHERE id_categoria = @id AND activo = 1";
                using (MySqlCommand cmdCount = new MySqlCommand(countProdQuery, conn))
                {
                    cmdCount.Parameters.AddWithValue("@id", id);
                    long productosAfectados = Convert.ToInt64(cmdCount.ExecuteScalar());

                    string mensaje = productosAfectados > 0
                        ? $"¿Desactivar la categoría '{txtNombre.Text}'?\n" +
                          $"Hay {productosAfectados} producto(s) activo(s) con esta categoría; van a seguir " +
                          "teniéndola, pero dejará de poder elegirse en nuevos productos."
                        : $"¿Desactivar la categoría '{txtNombre.Text}'?\n(No se eliminará permanentemente)";

                    var confirm = MessageBox.Show(mensaje, "Confirmar",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;
                }

                string query = "UPDATE categoria SET activo = 0 WHERE id_categoria = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Categoría desactivada.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategorias();
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
        private void gridCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= gridCategorias.Rows.Count) return;
            var row = gridCategorias.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var idVal = gridCategorias.Columns.Contains("id_categoria") ? row.Cells["id_categoria"]?.Value : (row.Cells.Count > 0 ? row.Cells[0]?.Value : null);
            if (idVal == null || idVal == DBNull.Value) return;

            _idSeleccionado = Convert.ToInt32(idVal);
            txtNombre.Text = gridCategorias.Columns.Contains("nombre") ? (row.Cells["nombre"]?.Value?.ToString() ?? "") : "";
            var pgVal = gridCategorias.Columns.Contains("porcentaje_ganancia") ? row.Cells["porcentaje_ganancia"]?.Value : null;
            txtPorcentaje.Text = pgVal != null && pgVal != DBNull.Value
                ? Convert.ToDecimal(pgVal).ToString("0.##")
                : "0";
            var actVal = gridCategorias.Columns.Contains("activo") ? row.Cells["activo"]?.Value : null;
            chkActivo.Checked = actVal != null && actVal != DBNull.Value && Convert.ToBoolean(actVal);
            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  BUSCAR
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridCategorias.DataSource is System.Data.DataTable dt)
            {
                string filtro = txtBuscar.Text.Replace("'", "''");
                dt.DefaultView.RowFilter = $"nombre LIKE '%{filtro}%'";
            }
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            _idSeleccionado    = null;
            txtNombre.Text     = "";
            txtPorcentaje.Text = "0";
            chkActivo.Checked  = true;
            Guardar_Boton.Text = "Crear Categoría";
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
