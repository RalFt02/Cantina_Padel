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
            CargarHorarios();
        }

        // Permite solo dígitos y teclas de control (backspace, etc.)
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
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
        //  CARGAR GRILLA
        // ─────────────────────────────────────────────
        private void CargarHorarios()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = @"
                    SELECT id_horario,
                           numero_cancha,
                           dia_semana,
                           hora_inicio,
                           hora_fin,
                           activo
                    FROM horario
                    ORDER BY numero_cancha,
                             FIELD(dia_semana, 'Lunes','Martes','Miércoles','Jueves','Viernes','Sábado','Domingo'),
                             hora_inicio";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                gridHorarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridHorarios.DataSource = dt;

                gridHorarios.Columns["id_horario"].HeaderText     = "ID";
                gridHorarios.Columns["numero_cancha"].HeaderText  = "Cancha";
                gridHorarios.Columns["dia_semana"].HeaderText     = "Día";
                gridHorarios.Columns["hora_inicio"].HeaderText    = "Hora Inicio";
                gridHorarios.Columns["hora_fin"].HeaderText       = "Hora Fin";
                gridHorarios.Columns["activo"].HeaderText         = "Activo";

                gridHorarios.Columns["id_horario"].Width     = 40;
                gridHorarios.Columns["numero_cancha"].Width  = 70;
                gridHorarios.Columns["dia_semana"].Width     = 90;
                gridHorarios.Columns["hora_inicio"].Width    = 90;
                gridHorarios.Columns["hora_fin"].Width       = 90;
                gridHorarios.Columns["activo"].Width         = 55;

                gridHorarios.Columns["hora_inicio"].DefaultCellStyle.Format = @"hh\:mm";
                gridHorarios.Columns["hora_fin"].DefaultCellStyle.Format    = @"hh\:mm";
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
            if (!int.TryParse(txtCancha.Text, out int numeroCancha) || numeroCancha <= 0)
            {
                MessageBox.Show("Ingresá un número de cancha válido.",
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

            string dia = cmbDia.SelectedItem!.ToString()!;
            bool esNuevo = _idSeleccionado == null;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();
                using MySqlTransaction tx = conn.BeginTransaction();

                if (esNuevo)
                    CrearHorario(conn, tx, numeroCancha, dia, horaInicio, horaFin);
                else
                    EditarHorario(conn, tx, numeroCancha, dia, horaInicio, horaFin);

                tx.Commit();
                MessageBox.Show("Operación realizada con éxito.",
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

        private void CrearHorario(MySqlConnection conn, MySqlTransaction tx,
            int numeroCancha, string dia, TimeSpan horaInicio, TimeSpan horaFin)
        {
            if (ExisteSolapamiento(conn, tx, numeroCancha, dia, horaInicio, horaFin, null))
            {
                MessageBox.Show("Ya existe un horario que se superpone para esa cancha y día.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tx.Rollback();
                return;
            }

            string insertHorario = @"
                INSERT INTO horario (numero_cancha, dia_semana, hora_inicio, hora_fin, activo)
                VALUES (@nc, @d, @hi, @hf, 1)";

            using MySqlCommand cmd = new MySqlCommand(insertHorario, conn, tx);
            cmd.Parameters.AddWithValue("@nc", numeroCancha);
            cmd.Parameters.AddWithValue("@d",  dia);
            cmd.Parameters.AddWithValue("@hi", horaInicio);
            cmd.Parameters.AddWithValue("@hf", horaFin);
            cmd.ExecuteNonQuery();
        }

        private void EditarHorario(MySqlConnection conn, MySqlTransaction tx,
            int numeroCancha, string dia, TimeSpan horaInicio, TimeSpan horaFin)
        {
            int id = _idSeleccionado!.Value;

            if (ExisteSolapamiento(conn, tx, numeroCancha, dia, horaInicio, horaFin, id))
            {
                MessageBox.Show("Ya existe otro horario que se superpone para esa cancha y día.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tx.Rollback();
                return;
            }

            string updateHorario = @"
                UPDATE horario
                SET numero_cancha = @nc,
                    dia_semana    = @d,
                    hora_inicio   = @hi,
                    hora_fin      = @hf
                WHERE id_horario = @id";

            using MySqlCommand cmd = new MySqlCommand(updateHorario, conn, tx);
            cmd.Parameters.AddWithValue("@nc", numeroCancha);
            cmd.Parameters.AddWithValue("@d",  dia);
            cmd.Parameters.AddWithValue("@hi", horaInicio);
            cmd.Parameters.AddWithValue("@hf", horaFin);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // Verifica si ya existe otro horario activo para la misma cancha y día
        // cuyo rango se superponga con el que se quiere guardar.
        private bool ExisteSolapamiento(MySqlConnection conn, MySqlTransaction tx,
            int numeroCancha, string dia, TimeSpan horaInicio, TimeSpan horaFin, int? idExcluido)
        {
            string query = @"
                SELECT COUNT(*) FROM horario
                WHERE numero_cancha = @nc
                  AND dia_semana = @d
                  AND activo = 1
                  AND hora_inicio < @hf
                  AND hora_fin > @hi" +
                  (idExcluido != null ? " AND id_horario <> @id" : "");

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@nc", numeroCancha);
            cmd.Parameters.AddWithValue("@d",  dia);
            cmd.Parameters.AddWithValue("@hi", horaInicio);
            cmd.Parameters.AddWithValue("@hf", horaFin);
            if (idExcluido != null)
                cmd.Parameters.AddWithValue("@id", idExcluido.Value);

            long count = (long)cmd.ExecuteScalar()!;
            return count > 0;
        }

        // ─────────────────────────────────────────────
        //  BOTÓN ELIMINAR (BAJA LÓGICA)
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un horario de la lista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Desactivar el horario de la cancha {txtCancha.Text} ({cmbDia.SelectedItem})?\n(No se eliminará permanentemente)",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "UPDATE horario SET activo = 0 WHERE id_horario = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Horario desactivado.", "Listo",
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

            _idSeleccionado     = Convert.ToInt32(row.Cells["id_horario"].Value);
            txtCancha.Text      = row.Cells["numero_cancha"].Value?.ToString() ?? "";
            cmbDia.SelectedItem = row.Cells["dia_semana"].Value?.ToString() ?? "";

            TimeSpan horaInicio = (TimeSpan)row.Cells["hora_inicio"].Value;
            TimeSpan horaFin    = (TimeSpan)row.Cells["hora_fin"].Value;
            txtHoraInicio.Text  = horaInicio.ToString(@"hh\:mm");
            txtHoraFin.Text     = horaFin.ToString(@"hh\:mm");

            chkActivo.Checked = Convert.ToBoolean(row.Cells["activo"].Value);
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
                    $"CONVERT(numero_cancha, System.String) LIKE '%{filtro}%' OR dia_semana LIKE '%{filtro}%'";
            }
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            _idSeleccionado    = null;
            txtCancha.Text     = "";
            if (cmbDia.Items.Count > 0) cmbDia.SelectedIndex = 0;
            txtHoraInicio.Text = "";
            txtHoraFin.Text    = "";
            chkActivo.Checked  = true;
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
