using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace Cantina_Padel
{
    public partial class FormGestionHorarios : Form
    {
        private readonly HashSet<string> _horasOcupadas = new();
        private bool _cargando;

        public FormGestionHorarios(int? clienteInicial = null, int? canchaInicial = null, DateTime? fechaInicial = null, TimeSpan? horaInicial = null, TimeSpan? horaFinInicial = null)
        {
            InitializeComponent();
            ConfigurarControles();
            CargarClientes();
            CargarCombosCanchas();

            if (clienteInicial.HasValue) SeleccionarComboPorId(cmbCliente, clienteInicial.Value);
            if (canchaInicial.HasValue) SeleccionarComboPorId(cmbCancha, canchaInicial.Value);
            if (fechaInicial.HasValue)
                dtpFecha.Value = fechaInicial.Value < dtpFecha.MinDate ? dtpFecha.MinDate : fechaInicial.Value;

            CargarHorasOcupadas();

            if (horaInicial.HasValue)
            {
                // FIX: SeleccionarTurno reemplazado por asignación directa a los TextBox
                txtHoraInicio.Text = horaInicial.Value.ToString(@"hh\:mm");
                TimeSpan fin = horaFinInicial ?? horaInicial.Value.Add(TimeSpan.FromHours(1));
                if (fin.TotalHours >= 24) fin = fin.Subtract(TimeSpan.FromHours(24));
                txtHoraFin.Text = fin.ToString(@"hh\:mm");
                ActualizarEstadoHorario();
            }
        }

        private void ConfigurarControles()
        {
            dtpFecha.MinDate = DateTime.Today;
            dtpFecha.ValueChanged += (_, _) => { if (!_cargando) CargarHorasOcupadas(); };
            cmbCancha.SelectedIndexChanged += (_, _) => { if (!_cargando) CargarHorasOcupadas(); };
            txtHoraInicio.KeyPress += SoloHora_KeyPress;
            txtHoraFin.KeyPress += SoloHora_KeyPress;
            txtHoraInicio.TextChanged += Hora_TextChanged;
            txtHoraFin.TextChanged += Hora_TextChanged;
            txtHoraInicio.Leave += (_, _) => ActualizarEstadoHorario();
            txtHoraFin.Leave += (_, _) => ActualizarEstadoHorario();
        }

        // FIX: no static para que el Designer pueda suscribirlos como event handlers
        private void SoloHora_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == ':') return;
            e.Handled = true;
        }

        private void Hora_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox txt || txt.Text.Contains(':')) return;

            if (txt.Text.Length == 4 && txt.Text.All(char.IsDigit))
            {
                int posicion = txt.SelectionStart;
                txt.Text = txt.Text.Insert(2, ":");
                txt.SelectionStart = Math.Min(posicion + 1, txt.Text.Length);
            }
            else if (txt.Text.Length == 2 && txt.Text.All(char.IsDigit))
            {
                int posicion = txt.SelectionStart;
                txt.Text += ":";
                txt.SelectionStart = Math.Min(posicion + 1, txt.Text.Length);
            }
        }

        private void CargarClientes()
        {
            try
            {
                using var conn = Conexion.ObtenerConexion();
                conn.Open();
                const string sql = @"
                    SELECT c.id_cliente,
                           CONCAT(p.apellido, ', ', p.nombre, ' - DNI ', p.dni) AS cliente
                    FROM cliente c
                    INNER JOIN persona p ON p.id_persona = c.id_persona
                    WHERE p.activo = 1
                    ORDER BY p.apellido, p.nombre";
                var dt = new DataTable();
                using var da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);
                cmbCliente.DataSource = dt;
                cmbCliente.DisplayMember = "cliente";
                cmbCliente.ValueMember = "id_cliente";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar clientes:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCombosCanchas()
        {
            try
            {
                using var conn = Conexion.ObtenerConexion();
                conn.Open();
                const string sql = "SELECT id_cancha, nombre FROM cancha WHERE activo = 1 AND estado = 'Activo' ORDER BY nombre";
                var dt = new DataTable();
                using var da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);
                cmbCancha.DataSource = dt;
                cmbCancha.DisplayMember = "nombre";
                cmbCancha.ValueMember = "id_cancha";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar las canchas:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHorasOcupadas()
        {
            if (!TryObtenerIdCombo(cmbCancha, out int idCancha)) return;

            _cargando = true;
            try
            {
                _horasOcupadas.Clear();
                DateTime fecha = dtpFecha.Value.Date;

                using var conn = Conexion.ObtenerConexion();
                conn.Open();
                const string sql = @"
                    SELECT r.id_reserva,
                           r.fecha,
                           h.hora_inicio,
                           h.hora_fin,
                           CONCAT(p.apellido, ', ', p.nombre, ' - DNI ', p.dni) AS cliente,
                           r.estado,
                           CASE WHEN r.reserva_fija IS NULL THEN 'No' ELSE 'Sí' END AS reserva_fija
                    FROM reserva r
                    INNER JOIN horario h ON h.id_horario = r.id_horario
                    INNER JOIN cliente cl ON cl.id_cliente = r.id_cliente
                    INNER JOIN persona p ON p.id_persona = cl.id_persona
                    WHERE r.id_cancha = @cancha
                      AND r.fecha = @fecha
                      AND r.estado IN ('Pendiente','Confirmada')
                    ORDER BY h.hora_inicio";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cancha", idCancha);
                cmd.Parameters.AddWithValue("@fecha", fecha);

                var dt = new DataTable();
                using (var da = new MySqlDataAdapter(cmd)) da.Fill(dt);
                gridOcupados.DataSource = dt;
                if (gridOcupados.Columns.Count > 0)
                {
                    gridOcupados.Columns["id_reserva"].Visible = false;
                    gridOcupados.Columns["fecha"].HeaderText = "Fecha";
                    gridOcupados.Columns["hora_inicio"].HeaderText = "Inicio";
                    gridOcupados.Columns["hora_fin"].HeaderText = "Fin";
                    gridOcupados.Columns["cliente"].HeaderText = "Cliente que reservó";
                    gridOcupados.Columns["estado"].HeaderText = "Estado";
                    gridOcupados.Columns["reserva_fija"].HeaderText = "Reserva fija";
                    gridOcupados.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    gridOcupados.Columns["hora_inicio"].DefaultCellStyle.Format = @"hh\:mm";
                    gridOcupados.Columns["hora_fin"].DefaultCellStyle.Format = @"hh\:mm";
                    foreach (DataGridViewRow row in gridOcupados.Rows)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(71, 71, 71);
                        row.DefaultCellStyle.ForeColor = Color.LightGray;
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    DateTime fechaReserva = Convert.ToDateTime(row["fecha"]).Date;
                    TimeSpan inicio = (TimeSpan)row["hora_inicio"];
                    TimeSpan fin = (TimeSpan)row["hora_fin"];
                    _horasOcupadas.Add(ClaveReserva(fechaReserva, inicio, fin));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al consultar reservas:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargando = false;
            }

            ActualizarEstadoHorario();
        }

        private static string ClaveReserva(DateTime fecha, TimeSpan inicio, TimeSpan fin)
            => $"{fecha:yyyyMMdd}|{inicio:c}|{fin:c}";

        private static bool TryObtenerIdCombo(ComboBox combo, out int id)
        {
            id = 0;
            if (combo.SelectedValue == null || combo.SelectedValue == DBNull.Value) return false;

            try
            {
                id = Convert.ToInt32(combo.SelectedValue, CultureInfo.InvariantCulture);
                return true;
            }
            catch (InvalidCastException)
            {
                if (combo.SelectedItem is DataRowView row && row.Row.Table.Columns.Count > 0)
                {
                    string column = combo.ValueMember;
                    if (!string.IsNullOrWhiteSpace(column) && row.Row.Table.Columns.Contains(column))
                    {
                        id = Convert.ToInt32(row[column], CultureInfo.InvariantCulture);
                        return true;
                    }
                }
                return false;
            }
            catch (FormatException) { return false; }
            catch (ArgumentException) { return false; }
        }

        private static void SeleccionarComboPorId(ComboBox combo, int id)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i] is DataRowView row && row.Row.Table.Columns.Contains(combo.ValueMember))
                {
                    if (Convert.ToInt32(row[combo.ValueMember], CultureInfo.InvariantCulture) == id)
                    {
                        combo.SelectedIndex = i;
                        return;
                    }
                }
            }
            try { combo.SelectedValue = id; } catch { }
        }

        private static bool TryParseHora(string texto, out TimeSpan hora)
        {
            return TimeSpan.TryParseExact(texto.Trim(), new[] { @"hh\:mm", @"h\:mm" }, CultureInfo.InvariantCulture, out hora);
        }

        // FIX: agregado return false al final que faltaba
        private static bool TryObtenerMinutosTurno(TimeSpan hora, out int minutos)
        {
            minutos = 0;
            int total = (int)hora.TotalMinutes;
            if (total >= 8 * 60 && total <= 23 * 60 + 59)
            {
                minutos = total;
                return true;
            }
            if (total >= 0 && total <= 3 * 60)
            {
                minutos = total + 24 * 60;
                return true;
            }
            return false;
        }

        private bool TryObtenerRango(out TimeSpan inicio, out TimeSpan fin, out int inicioTurno, out int finTurno)
        {
            inicio = default; fin = default; inicioTurno = 0; finTurno = 0;

            if (!TryParseHora(txtHoraInicio.Text, out inicio) || !TryParseHora(txtHoraFin.Text, out fin))
            {
                MessageBox.Show("Las horas deben tener formato HH:mm. Ejemplo: 17:30", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!TryObtenerMinutosTurno(inicio, out inicioTurno) || !TryObtenerMinutosTurno(fin, out finTurno))
            {
                MessageBox.Show("Las horas permitidas son de 08:00 a 03:00 del día siguiente.", "Horario fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (finTurno <= inicioTurno)
            {
                MessageBox.Show("La hora de fin debe ser posterior a la hora de inicio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private static DateTime FechaHoraNormalizada(DateTime fecha, TimeSpan hora)
        {
            DateTime resultado = fecha.Date.Add(hora);
            if (hora.TotalMinutes <= 3 * 60) resultado = resultado.AddDays(1);
            return resultado;
        }

        private bool ExisteSolapamiento(DateTime fecha, int idCancha, TimeSpan inicio, TimeSpan fin)
        {
            DateTime nuevoInicio = FechaHoraNormalizada(fecha, inicio);
            DateTime nuevoFin = FechaHoraNormalizada(fecha, fin);
            if (nuevoFin <= nuevoInicio) nuevoFin = nuevoFin.AddDays(1);

            using var conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT r.fecha, h.hora_inicio, h.hora_fin
                FROM reserva r
                INNER JOIN horario h ON h.id_horario = r.id_horario
                WHERE r.id_cancha = @cancha
                  AND r.fecha BETWEEN @desde AND @hasta
                  AND r.estado IN ('Pendiente','Confirmada')";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cancha", idCancha);
            cmd.Parameters.AddWithValue("@desde", fecha.Date.AddDays(-1));
            cmd.Parameters.AddWithValue("@hasta", fecha.Date.AddDays(1));

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                DateTime fechaExistente = rd.GetDateTime("fecha").Date;
                TimeSpan inicioExistente = rd.GetTimeSpan("hora_inicio");
                TimeSpan finExistente = rd.GetTimeSpan("hora_fin");
                DateTime existenteInicio = FechaHoraNormalizada(fechaExistente, inicioExistente);
                DateTime existenteFin = FechaHoraNormalizada(fechaExistente, finExistente);
                if (existenteFin <= existenteInicio) existenteFin = existenteFin.AddDays(1);
                if (nuevoInicio < existenteFin && nuevoFin > existenteInicio) return true;
            }
            return false;
        }

        private int ObtenerOCrearHorario(MySqlConnection conn, MySqlTransaction tx, int idCancha, DateTime fecha, TimeSpan inicio, TimeSpan fin)
        {
            string dia = ObtenerDiaSemana(fecha);
            const string select = @"
                SELECT id_horario FROM horario
                WHERE id_cancha = @cancha AND dia_semana = @dia
                  AND hora_inicio = @inicio AND hora_fin = @fin
                LIMIT 1";
            using (var cmd = new MySqlCommand(select, conn, tx))
            {
                cmd.Parameters.AddWithValue("@cancha", idCancha);
                cmd.Parameters.AddWithValue("@dia", dia);
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fin", fin);
                object? value = cmd.ExecuteScalar();
                if (value != null && value != DBNull.Value)
                    return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }

            const string insert = @"
                INSERT INTO horario (hora_inicio, hora_fin, id_cancha, dia_semana)
                VALUES (@inicio, @fin, @cancha, @dia);
                SELECT LAST_INSERT_ID();";
            using var insertCmd = new MySqlCommand(insert, conn, tx);
            insertCmd.Parameters.AddWithValue("@inicio", inicio);
            insertCmd.Parameters.AddWithValue("@fin", fin);
            insertCmd.Parameters.AddWithValue("@cancha", idCancha);
            insertCmd.Parameters.AddWithValue("@dia", dia);
            return Convert.ToInt32(insertCmd.ExecuteScalar(), CultureInfo.InvariantCulture);
        }

        private static string ObtenerDiaSemana(DateTime fecha) => fecha.DayOfWeek switch
        {
            DayOfWeek.Monday => "Lunes",
            DayOfWeek.Tuesday => "Martes",
            DayOfWeek.Wednesday => "Miércoles",
            DayOfWeek.Thursday => "Jueves",
            DayOfWeek.Friday => "Viernes",
            DayOfWeek.Saturday => "Sábado",
            _ => "Domingo"
        };

        private bool ValidarSeleccion(out int idCliente, out int idCancha, out TimeSpan inicio, out TimeSpan fin)
        {
            idCliente = 0; idCancha = 0; inicio = default; fin = default;
            if (!TryObtenerIdCombo(cmbCliente, out idCliente))
            {
                MessageBox.Show("Seleccioná un cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!TryObtenerIdCombo(cmbCancha, out idCancha))
            {
                MessageBox.Show("Seleccioná una cancha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return TryObtenerRango(out inicio, out fin, out _, out _);
        }

        private string ObtenerClienteDelSolapamiento(DateTime fecha, int idCancha, TimeSpan inicio, TimeSpan fin)
        {
            DateTime nuevoInicio = FechaHoraNormalizada(fecha, inicio);
            DateTime nuevoFin = FechaHoraNormalizada(fecha, fin);
            if (nuevoFin <= nuevoInicio) nuevoFin = nuevoFin.AddDays(1);

            using var conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT CONCAT(p.apellido, ', ', p.nombre, ' - DNI ', p.dni),
                       r.fecha, h.hora_inicio, h.hora_fin
                FROM reserva r
                INNER JOIN horario h ON h.id_horario = r.id_horario
                INNER JOIN cliente cl ON cl.id_cliente = r.id_cliente
                INNER JOIN persona p ON p.id_persona = cl.id_persona
                WHERE r.id_cancha = @cancha
                  AND r.fecha BETWEEN @desde AND @hasta
                  AND r.estado IN ('Pendiente','Confirmada')
                ORDER BY r.fecha, h.hora_inicio";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cancha", idCancha);
            cmd.Parameters.AddWithValue("@desde", fecha.Date.AddDays(-1));
            cmd.Parameters.AddWithValue("@hasta", fecha.Date.AddDays(1));
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                DateTime fr = rd.GetDateTime(1).Date;
                TimeSpan ei = rd.GetTimeSpan(2);
                TimeSpan ef = rd.GetTimeSpan(3);
                DateTime existenteInicio = FechaHoraNormalizada(fr, ei);
                DateTime existenteFin = FechaHoraNormalizada(fr, ef);
                if (existenteFin <= existenteInicio) existenteFin = existenteFin.AddDays(1);
                if (nuevoInicio < existenteFin && nuevoFin > existenteInicio) return rd.GetString(0);
            }
            return "";
        }

        private void Reservar_Boton_Click(object sender, EventArgs e)
        {
            if (!ValidarSeleccion(out int idCliente, out int idCancha, out TimeSpan inicio, out TimeSpan fin)) return;
            DateTime fecha = dtpFecha.Value.Date;

            if (ExisteSolapamiento(fecha, idCancha, inicio, fin))
            {
                MessageBox.Show("Ese rango horario ya está ocupado. Elegí otro horario.", "Horario ocupado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarHorasOcupadas();
                return;
            }

            try
            {
                using var conn = Conexion.ObtenerConexion();
                conn.Open();
                using var tx = conn.BeginTransaction();
                int idHorario = ObtenerOCrearHorario(conn, tx, idCancha, fecha, inicio, fin);
                const string sql = @"
                    INSERT INTO reserva (fecha, reserva_fija, estado, id_cliente, id_cancha, id_horario, id_usuario)
                    VALUES (@fecha, NULL, 'Confirmada', @cliente, @cancha, @horario, @usuario)";
                using var cmd = new MySqlCommand(sql, conn, tx);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@cliente", idCliente);
                cmd.Parameters.AddWithValue("@cancha", idCancha);
                cmd.Parameters.AddWithValue("@horario", idHorario);
                cmd.Parameters.AddWithValue("@usuario", ObtenerUsuarioActual());
                cmd.ExecuteNonQuery();
                tx.Commit();
                MessageBox.Show("Reserva creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarHorasOcupadas();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al crear la reserva:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerUsuarioActual() => 1;

        private void ReservasFijas_Boton_Click(object sender, EventArgs e)
        {
            using var form = new FormReservasFijas();
            form.ShowDialog(this);
            CargarHorasOcupadas();
        }

        private void ActualizarEstadoHorario()
        {
            if (!TryParseHora(txtHoraInicio.Text, out TimeSpan inicio) ||
                !TryParseHora(txtHoraFin.Text, out TimeSpan fin))
            {
                lblEstado.Text = "Ingresá inicio y fin (HH:mm)";
                lblEstado.ForeColor = Color.LightGray;
                return;
            }

            if (!TryObtenerMinutosTurno(inicio, out int ini) ||
                !TryObtenerMinutosTurno(fin, out int f))
            {
                lblEstado.Text = "Fuera del rango 08:00 - 03:00";
                lblEstado.ForeColor = Color.IndianRed;
                return;
            }

            if (f <= ini)
            {
                lblEstado.Text = "La hora de fin debe ser posterior";
                lblEstado.ForeColor = Color.IndianRed;
                return;
            }

            if (TryObtenerIdCombo(cmbCancha, out int cancha) &&
                ExisteSolapamiento(dtpFecha.Value.Date, cancha, inicio, fin))
            {
                string cliente = ObtenerClienteDelSolapamiento(dtpFecha.Value.Date, cancha, inicio, fin);
                lblEstado.Text = string.IsNullOrWhiteSpace(cliente)
                    ? "Ocupado / bloqueado"
                    : $"Ocupado por: {cliente}";
                lblEstado.ForeColor = Color.LightGray;
            }
            else
            {
                lblEstado.Text = "Disponible";
                lblEstado.ForeColor = Color.FromArgb(163, 230, 53);
            }
        }

        private bool ObtenerReservaSeleccionada(out int idReserva, out DateTime fecha, out TimeSpan inicio, out TimeSpan fin)
        {
            idReserva = 0; fecha = DateTime.MinValue; inicio = default; fin = default;

            if (gridOcupados.CurrentRow == null || gridOcupados.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccioná una reserva de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                idReserva = Convert.ToInt32(gridOcupados.CurrentRow.Cells["id_reserva"].Value, CultureInfo.InvariantCulture);
                fecha = Convert.ToDateTime(gridOcupados.CurrentRow.Cells["fecha"].Value).Date;
                inicio = (TimeSpan)gridOcupados.CurrentRow.Cells["hora_inicio"].Value;
                fin = (TimeSpan)gridOcupados.CurrentRow.Cells["hora_fin"].Value;
                return true;
            }
            catch
            {
                MessageBox.Show("No se pudo obtener la reserva seleccionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void CancelarReserva_Boton_Click(object sender, EventArgs e)
        {
            if (!ObtenerReservaSeleccionada(out int idReserva, out _, out _, out _)) return;

            if (MessageBox.Show(
                "¿Querés cancelar la reserva seleccionada?\nLa hora quedará disponible para volver a alquilarla.",
                "Cancelar reserva", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using var conn = Conexion.ObtenerConexion();
                conn.Open();
                const string sql = "UPDATE reserva SET estado = 'Cancelada' WHERE id_reserva = @id AND estado IN ('Pendiente','Confirmada')";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idReserva);
                int afectados = cmd.ExecuteNonQuery();

                if (afectados == 0)
                {
                    MessageBox.Show("La reserva ya estaba cancelada o no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Reserva cancelada. La hora quedó disponible.", "Reserva cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarHorasOcupadas();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cancelar la reserva:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Realquilar_Boton_Click(object sender, EventArgs e)
        {
            if (!ObtenerReservaSeleccionada(out int idReserva, out DateTime fecha, out TimeSpan inicio, out TimeSpan fin)) return;

            if (MessageBox.Show(
                "Se cancelará la reserva seleccionada y se cargará su fecha y horario para poder alquilarlo nuevamente.\n\n¿Continuar?",
                "Realquilar hora", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using var conn = Conexion.ObtenerConexion();
                conn.Open();
                const string sql = "UPDATE reserva SET estado = 'Cancelada' WHERE id_reserva = @id AND estado IN ('Pendiente','Confirmada')";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idReserva);
                int afectados = cmd.ExecuteNonQuery();

                if (afectados == 0)
                {
                    MessageBox.Show("La reserva ya estaba cancelada o no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dtpFecha.Value = fecha < dtpFecha.MinDate ? dtpFecha.MinDate : fecha;
                txtHoraInicio.Text = inicio.ToString(@"hh\:mm");
                txtHoraFin.Text = fin.ToString(@"hh\:mm");
                CargarHorasOcupadas();
                lblEstado.Text = "Hora liberada. Seleccioná el cliente y presioná Reservar.";
                lblEstado.ForeColor = Color.FromArgb(163, 230, 53);
                cmbCliente.Focus();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al realquilar la hora:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar_Boton_Click(object sender, EventArgs e)
        {
            if (cmbCliente.Items.Count > 0) cmbCliente.SelectedIndex = 0;
            if (cmbCancha.Items.Count > 0) cmbCancha.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Today;
            txtHoraInicio.Clear();
            txtHoraFin.Clear();
            lblEstado.Text = "";
            CargarHorasOcupadas();
        }

        private void Volver_Boton_Click(object sender, EventArgs e) => Close();
    }
}