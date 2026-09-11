using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Cantina_Padel
{
    public partial class FormGestionCanchas : Form
    {
        // Guarda el ID de la cancha seleccionada en la grilla (null = alta nueva).
        // No usamos un TextBox visible para esto, así no aparece ni en tiempo de
        // ejecución ni en el lienzo de diseño de Visual Studio.
        private int? _idSeleccionado;

        public FormGestionCanchas()
        {
            InitializeComponent();
            CargarCanchas();
        }

        // Permite solo letras (incluye acentos y ñ), espacios, números y teclas de
        // control (backspace, etc.) — el nombre de cancha suele ser "Cancha 1", "Cancha 2A", etc.
        private void NombreCancha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Una cancha "Fuera de Servicio" o "En Reparación" no puede figurar como
        // activa al mismo tiempo: se desmarca y se bloquea el checkbox mientras
        // dure ese estado. Se llama tanto al cambiar el combo como al cargar una
        // fila de la grilla, para que no queden datos inconsistentes en pantalla.
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

                string query = "SELECT id_cancha, nombre, estado, activo FROM cancha ORDER BY id_cancha";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                gridCanchas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridCanchas.DataSource = dt;

                gridCanchas.Columns["id_cancha"].HeaderText = "ID";
                gridCanchas.Columns["nombre"].HeaderText     = "Nombre";
                gridCanchas.Columns["estado"].HeaderText     = "Estado";
                gridCanchas.Columns["activo"].HeaderText     = "Activo";

                gridCanchas.Columns["id_cancha"].Width = 40;
                gridCanchas.Columns["nombre"].Width     = 150;
                gridCanchas.Columns["estado"].Width     = 140;
                gridCanchas.Columns["activo"].Width      = 55;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar canchas:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("El nombre no puede empezar ni terminar con espacios en blanco.");
                return;
            }

            bool esNuevo = _idSeleccionado == null;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                if (esNuevo)
                    CrearCancha(conn);
                else
                    EditarCancha(conn);

                MessageBox.Show("Cancha guardada con éxito.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCanchas();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearCancha(MySqlConnection conn)
        {
            string estado = cmbEstado.SelectedItem!.ToString()!;
            bool activo = estado == "Activo" && chkActivo.Checked;

            string insert = "INSERT INTO cancha (nombre, estado, activo) VALUES (@n, @e, @a)";
            using MySqlCommand cmd = new MySqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@n", txtNombre.Text);
            cmd.Parameters.AddWithValue("@e", estado);
            cmd.Parameters.AddWithValue("@a", activo ? 1 : 0);
            cmd.ExecuteNonQuery();
        }

        private void EditarCancha(MySqlConnection conn)
        {
            string estado = cmbEstado.SelectedItem!.ToString()!;
            bool activo = estado == "Activo" && chkActivo.Checked;

            string update = "UPDATE cancha SET nombre = @n, estado = @e, activo = @a WHERE id_cancha = @id";
            using MySqlCommand cmd = new MySqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@n",  txtNombre.Text);
            cmd.Parameters.AddWithValue("@e",  estado);
            cmd.Parameters.AddWithValue("@a",  activo ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
            cmd.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────
        //  BOTÓN ELIMINAR (BAJA LÓGICA)
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná una cancha de la lista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Desactivar la cancha '{txtNombre.Text}'?\n(No se eliminará permanentemente)",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "UPDATE cancha SET activo = 0 WHERE id_cancha = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cancha desactivada.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCanchas();
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
        private void gridCanchas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = gridCanchas.Rows[e.RowIndex];

            _idSeleccionado   = Convert.ToInt32(row.Cells["id_cancha"].Value);
            txtNombre.Text    = row.Cells["nombre"].Value?.ToString() ?? "";
            cmbEstado.SelectedItem = row.Cells["estado"].Value?.ToString() ?? "Activo";
            chkActivo.Checked = Convert.ToBoolean(row.Cells["activo"].Value);
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
            cmbEstado.SelectedIndex = 0; // "Activo" por defecto
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
