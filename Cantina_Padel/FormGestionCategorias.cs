using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionCategorias : Form
    {
        // Guarda el ID de la categoría seleccionada en la grilla (null = alta nueva).
        // No usamos un TextBox oculto: así no aparece ni en tiempo de ejecución
        // ni en el lienzo de diseño de Visual Studio.
        private int? _idSeleccionado;

        public FormGestionCategorias()
        {
            InitializeComponent();
            CargarCategorias();
        }

        // Permite solo letras (incluye acentos y ñ), espacios y teclas de control (backspace, etc.)
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
        private void CargarCategorias()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "SELECT id_categoria, nombre, activo FROM categoria ORDER BY nombre";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                gridCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridCategorias.DataSource = dt;

                gridCategorias.Columns["id_categoria"].HeaderText = "ID";
                gridCategorias.Columns["nombre"].HeaderText        = "Nombre";
                gridCategorias.Columns["activo"].HeaderText        = "Activo";

                gridCategorias.Columns["id_categoria"].Width = 40;
                gridCategorias.Columns["nombre"].Width        = 240;
                gridCategorias.Columns["activo"].Width         = 55;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar categorías:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            bool esNuevo = _idSeleccionado == null;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                if (esNuevo)
                    CrearCategoria(conn);
                else
                    EditarCategoria(conn);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearCategoria(MySqlConnection conn)
        {
            string checkQuery = "SELECT COUNT(*) FROM categoria WHERE nombre = @n";
            using (MySqlCommand check = new MySqlCommand(checkQuery, conn))
            {
                check.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
                long count = (long)check.ExecuteScalar()!;
                if (count > 0)
                {
                    MessageBox.Show("Ya existe una categoría con ese nombre.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string insert = "INSERT INTO categoria (nombre, activo) VALUES (@n, @a)";
            using MySqlCommand cmd = new MySqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@a", chkActivo.Checked ? 1 : 0);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Categoría creada con éxito.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarCategorias();
            LimpiarFormulario();
        }

        private void EditarCategoria(MySqlConnection conn)
        {
            int id = _idSeleccionado!.Value;

            string checkQuery = "SELECT COUNT(*) FROM categoria WHERE nombre = @n AND id_categoria <> @id";
            using (MySqlCommand check = new MySqlCommand(checkQuery, conn))
            {
                check.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
                check.Parameters.AddWithValue("@id", id);
                long count = (long)check.ExecuteScalar()!;
                if (count > 0)
                {
                    MessageBox.Show("Ya existe otra categoría con ese nombre.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Si se está desactivando, avisamos cuántos productos activos quedarían
            // con una categoría no seleccionable (no se les quita la categoría, solo
            // deja de aparecer en el combo de Productos para asignaciones nuevas).
            if (!chkActivo.Checked)
            {
                string countProdQuery = "SELECT COUNT(*) FROM producto WHERE id_categoria = @id AND activo = 1";
                using MySqlCommand cmdCount = new MySqlCommand(countProdQuery, conn);
                cmdCount.Parameters.AddWithValue("@id", id);
                long productosAfectados = (long)cmdCount.ExecuteScalar()!;

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

            string update = "UPDATE categoria SET nombre = @n, activo = @a WHERE id_categoria = @id";
            using MySqlCommand cmd = new MySqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@n",  txtNombre.Text.Trim());
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
                    long productosAfectados = (long)cmdCount.ExecuteScalar()!;

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
            catch (MySqlException ex)
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
            if (e.RowIndex < 0) return;
            var row = gridCategorias.Rows[e.RowIndex];

            _idSeleccionado = Convert.ToInt32(row.Cells["id_categoria"].Value);
            txtNombre.Text = row.Cells["nombre"].Value?.ToString() ?? "";
            chkActivo.Checked = Convert.ToBoolean(row.Cells["activo"].Value);
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
