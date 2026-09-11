using MySql.Data.MySqlClient;
using System.Globalization;

namespace Cantina_Padel
{
    public partial class FormGestionHorarios : Form
    {
        // Guarda el ID del horario seleccionado en la grilla (null = alta nueva).
        // No usamos un TextBox visible para esto, así no aparece ni en tiempo de
        // ejecución ni en el lienzo de diseño de Visual Studio.
        private int? _idSeleccionado;

        public FormGestionHorarios()
        {
            InitializeComponent();
            CargarCombos();
            CargarHorarios();
        }

        // Permite dígitos, dos puntos (:) y teclas de control (backspace, etc.)
        private void SoloHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;
            if (char.IsControl(e.KeyChar)) return;

            if (e.KeyChar == ':' && !txt.Text.Contains(':'))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // ─────────────────────────────────────────────
        //  CARGAR COMBO DE CANCHAS (desde el módulo de Canchas)
        // ─────────────────────────────────────────────
        private void CargarCombos()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                // Solo canchas activas (mismo criterio que Productos con Marca/Categoría/Proveedor)
                var da = new MySqlDataAdapter("SELECT id_cancha, nombre FROM cancha WHERE activo = 1 ORDER BY nombre", conn);
                var dt = new System.Data.DataTable();
                da.Fill(dt);

                cmbCancha.DataSource    = dt;
                cmbCancha.DisplayMember = "nombre";
                cmbCancha.ValueMember   = "id_cancha";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar las canchas:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  CARGAR GRILLA
        // ─────────────────────────────────────────────
        private void CargarHorarios()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = @"
                    SELECT h.id_horario,
                           c.nombre AS cancha,
                           h.dia_semana,
                           h.hora_inicio,
                           h.hora_fin,
                           h.id_cancha
                    FROM horario h
                    JOIN cancha c ON c.id_cancha = h.id_cancha
                    ORDER BY c.nombre,
                             FIELD(h.dia_semana, 'Lunes','Martes','Miércoles','Jueves','Viernes','Sábado','Domingo'),
                             h.hora_inicio";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                gridHorarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridHorarios.DataSource = dt;

                gridHorarios.Columns["id_horario"].HeaderText  = "ID";
                gridHorarios.Columns["cancha"].HeaderText      = "Cancha";
                gridHorarios.Columns["dia_semana"].HeaderText  = "Día";
                gridHorarios.Columns["hora_inicio"].HeaderText = "Hora Inicio";
                gridHorarios.Columns["hora_fin"].HeaderText    = "Hora Fin";

                gridHorarios.Columns["id_horario"].Width  = 40;
                gridHorarios.Columns["cancha"].Width       = 90;
                gridHorarios.Columns["dia_semana"].Width   = 90;
                gridHorarios.Columns["hora_inicio"].Width  = 90;
                gridHorarios.Columns["hora_fin"].Width     = 90;

                gridHorarios.Columns["hora_inicio"].DefaultCellStyle.Format = @"hh\:mm";
                gridHorarios.Columns["hora_fin"].DefaultCellStyle.Format    = @"hh\:mm";

                // El id_cancha viaja oculto, solo se usa para preseleccionar el combo al editar
                gridHorarios.Columns["id_cancha"].Visible = false;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar horarios:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  BOTÓN NUEVO
        // ─────────────────────────────────────────────
        private void Nuevo_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Guardar_Boton.Text = "Crear Horario";
        }

        // ─────────────────────────────────────────────
        //  BOTÓN GUARDAR (CREAR / EDITAR)
        // ─────────────────────────────────────────────
        private void Guardar_Boton_Click(object sender, EventArgs e)
        {
            if (cmbCancha.SelectedValue == null)
            {
                MessageBox.Show("Seleccioná una cancha.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbDia.SelectedItem == null)
            {
                MessageBox.Show("Seleccioná el día.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TimeSpan.TryParseExact(txtHoraInicio.Text, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan horaInicio) ||
                !TimeSpan.TryParseExact(txtHoraFin.Text, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan horaFin))
            {
                MessageBox.Show("Ingresá las horas en formato HH:mm (ej: 08:30).",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (horaFin <= horaInicio)
            {
                MessageBox.Show("La hora de fin debe ser posterior a la hora de inicio.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCancha = (int)cmbCancha.SelectedValue!;
            string dia = cmbDia.SelectedItem!.ToString()!;
            bool esNuevo = _idSeleccionado == null;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();
                using MySqlTransaction tx = conn.BeginTransaction();

                bool guardado;

                if (esNuevo)
                    guardado = CrearHorario(conn, tx, idCancha, dia, horaInicio, horaFin);
                else
                    guardado = EditarHorario(conn, tx, idCancha, dia, horaInicio, horaFin);

                if (!guardado)
                {
                    tx.Rollback();
                    return;
                }

                tx.Commit();
                MessageBox.Show("Cancha reservada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarHorarios();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CrearHorario(MySqlConnection conn, MySqlTransaction tx,
            int idCancha, string dia, TimeSpan horaInicio, TimeSpan horaFin)
        {
            if (ExisteSolapamiento(conn, tx, idCancha, dia, horaInicio, horaFin, null))
            {
                MessageBox.Show("Ya existe un horario que se superpone para esa cancha y día.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string insertHorario = @"
            INSERT INTO horario (id_cancha, dia_semana, hora_inicio, hora_fin)
            VALUES (@idc, @d, @hi, @hf)";

            using MySqlCommand cmd = new MySqlCommand(insertHorario, conn, tx);
            cmd.Parameters.AddWithValue("@idc", idCancha);
            cmd.Parameters.AddWithValue("@d", dia);
            cmd.Parameters.AddWithValue("@hi", horaInicio);
            cmd.Parameters.AddWithValue("@hf", horaFin);
            cmd.ExecuteNonQuery();

            return true;
        }

        private bool EditarHorario(MySqlConnection conn, MySqlTransaction tx,
            int idCancha, string dia, TimeSpan horaInicio, TimeSpan horaFin)
        {
            int id = _idSeleccionado!.Value;

            if (ExisteSolapamiento(conn, tx, idCancha, dia, horaInicio, horaFin, id))
            {
                MessageBox.Show("Ya existe otro horario que se superpone para esa cancha y día.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string updateHorario = @"
            UPDATE horario
            SET id_cancha   = @idc,
            dia_semana  = @d,
            hora_inicio = @hi,
            hora_fin    = @hf
            WHERE id_horario = @id";

            using MySqlCommand cmd = new MySqlCommand(updateHorario, conn, tx);
            cmd.Parameters.AddWithValue("@idc", idCancha);
            cmd.Parameters.AddWithValue("@d", dia);
            cmd.Parameters.AddWithValue("@hi", horaInicio);
            cmd.Parameters.AddWithValue("@hf", horaFin);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            return true;
        }

        // Verifica si ya existe otro horario para la misma cancha y día cuyo
        // rango se superponga con el que se quiere guardar. Como ya no hay
        // campo "activo" en esta tabla, se compara contra todos los horarios.
        private bool ExisteSolapamiento(MySqlConnection conn, MySqlTransaction tx,
            int idCancha, string dia, TimeSpan horaInicio, TimeSpan horaFin, int? idExcluido)
        {
            string query = @"
                SELECT COUNT(*) FROM horario
                WHERE id_cancha = @idc
                  AND dia_semana = @d
                  AND hora_inicio < @hf
                  AND hora_fin > @hi" +
                  (idExcluido != null ? " AND id_horario <> @id" : "");

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@idc", idCancha);
            cmd.Parameters.AddWithValue("@d",   dia);
            cmd.Parameters.AddWithValue("@hi",  horaInicio);
            cmd.Parameters.AddWithValue("@hf",  horaFin);
            if (idExcluido != null)
                cmd.Parameters.AddWithValue("@id", idExcluido.Value);

            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }

        // ─────────────────────────────────────────────
        //  BOTÓN ELIMINAR
        // ─────────────────────────────────────────────
        // Ya no hay columna "activo" en esta tabla, así que esto borra el
        // registro de verdad (no es baja lógica como en los otros módulos).
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un horario de la lista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Eliminar el horario de '{cmbCancha.Text}' ({cmbDia.SelectedItem})?\nEsta acción no se puede deshacer.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "DELETE FROM horario WHERE id_horario = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Horario eliminado.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarHorarios();
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
        private void gridHorarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = gridHorarios.Rows[e.RowIndex];

            _idSeleccionado         = Convert.ToInt32(row.Cells["id_horario"].Value);
            cmbCancha.SelectedValue = row.Cells["id_cancha"].Value;
            cmbDia.SelectedItem     = row.Cells["dia_semana"].Value?.ToString() ?? "";

            TimeSpan horaInicio = (TimeSpan)row.Cells["hora_inicio"].Value;
            TimeSpan horaFin    = (TimeSpan)row.Cells["hora_fin"].Value;
            txtHoraInicio.Text  = horaInicio.ToString(@"hh\:mm");
            txtHoraFin.Text     = horaFin.ToString(@"hh\:mm");

            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  BUSCAR
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridHorarios.DataSource is System.Data.DataTable dt)
            {
                string filtro = txtBuscar.Text.Replace("'", "''");
                dt.DefaultView.RowFilter =
                    $"cancha LIKE '%{filtro}%' OR dia_semana LIKE '%{filtro}%'";
            }
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            _idSeleccionado    = null;
            if (cmbCancha.Items.Count > 0) cmbCancha.SelectedIndex = 0;
            if (cmbDia.Items.Count > 0)    cmbDia.SelectedIndex = 0;
            txtHoraInicio.Text = "";
            txtHoraFin.Text    = "";
            Guardar_Boton.Text = "Crear Horario";
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
