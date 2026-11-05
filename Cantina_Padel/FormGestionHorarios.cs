using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace Cantina_Padel
{
    public partial class FormGestionHorarios : Form
    {
        private readonly HashSet<string> _horasOcupadas = new();
        private readonly List<Ocupacion> _ocupaciones = new();
        private bool _cargando;

        public FormGestionHorarios(int? clienteInicial = null, int? canchaInicial = null, DateTime? fechaInicial = null, TimeSpan? horaInicial = null, TimeSpan? horaFinInicial = null)
        {
            InitializeComponent();
            this.BindingContext ??= new BindingContext();
            gridOcupados.AutoGenerateColumns = true;
            gridOcupados.BindingContext = this.BindingContext;
            gridOcupados.DataBindingComplete += (_, _) => FormatearColumnasOcupados();
            this.Load += (_, _) => FormatearColumnasOcupados();

            ConfigurarControles();
            CargarClientes();
            CargarCombosCanchas();

            if (clienteInicial.HasValue) SeleccionarComboPorId(cmbCliente, clienteInicial.Value);
            if (canchaInicial.HasValue) SeleccionarComboPorId(cmbCancha, canchaInicial.Value);
            if (fechaInicial.HasValue)
                dtpFecha.Value = fechaInicial.Value < dtpFecha.MinDate ? dtpFecha.MinDate : fechaInicial.Value;

            // Primero se cargan los turnos libres de esa cancha/fecha y recién después se elige el pedido.
            CargarHorasOcupadas();
            if (horaInicial.HasValue)
            {
                SeleccionarTurno(horaInicial.Value);
                ActualizarEstadoHorario();
            }
        }

        private void ConfigurarControles()
        {
            dtpFecha.MinDate = DateTime.Today;
            dtpFecha.ValueChanged += (_, _) => { if (!_cargando) CargarHorasOcupadas(); };
            cmbCancha.SelectedIndexChanged += (_, _) => { if (!_cargando) CargarHorasOcupadas(); };
            cmbHorario.SelectedIndexChanged += (_, _) => { if (!_cargando) ActualizarEstadoHorario(); };
            RefrescarTurnosDisponibles();
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
                cmbCliente.DisplayMember = "cliente";
                cmbCliente.ValueMember = "id_cliente";
                cmbCliente.DataSource = dt;
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
                cmbCancha.DisplayMember = "nombre";
                cmbCancha.ValueMember = "id_cancha";
                cmbCancha.DataSource = dt;
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
                _ocupaciones.Clear();
                DateTime fecha = dtpFecha.Value.Date;

                using var conn = Conexion.ObtenerConexion();
                conn.Open();
                const string sql = @"
                    SELECT r.id_reserva,
                           r.id_cancha,
                           c.nombre AS cancha,
                           r.fecha,
                           h.dia_semana,
                           h.hora_inicio,
                           h.hora_fin,
                           CONCAT(p.apellido, ', ', p.nombre, ' - DNI ', p.dni) AS cliente,
                           r.estado,
                           CASE WHEN r.reserva_fija IS NULL THEN 'No' ELSE 'Sí' END AS reserva_fija
                    FROM reserva r
                    INNER JOIN horario h ON h.id_horario = r.id_horario
                    INNER JOIN cliente cl ON cl.id_cliente = r.id_cliente
                    INNER JOIN persona p ON p.id_persona = cl.id_persona
                    INNER JOIN cancha c ON c.id_cancha = r.id_cancha
                    WHERE r.fecha = @fecha
                      AND r.estado IN ('Pendiente','Confirmada')
                    ORDER BY r.fecha, h.hora_inicio, c.nombre";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fecha", fecha);

                var dt = new DataTable();
                using (var da = new MySqlDataAdapter(cmd)) da.Fill(dt);

                this.BindingContext ??= new BindingContext();
                gridOcupados.AutoGenerateColumns = true;
                gridOcupados.BindingContext = this.BindingContext;
                gridOcupados.DataSource = dt;

                FormatearColumnasOcupados();

                foreach (DataRow row in dt.Rows)
                {
                    int canchaReserva = Convert.ToInt32(row["id_cancha"], CultureInfo.InvariantCulture);
                    if (canchaReserva != idCancha)
                        continue;

                    DateTime fechaReserva = Convert.ToDateTime(row["fecha"]).Date;
                    TimeSpan inicio = (TimeSpan)row["hora_inicio"];
                    TimeSpan fin = (TimeSpan)row["hora_fin"];
                    _horasOcupadas.Add(ClaveReserva(fechaReserva, inicio, fin));
                    _ocupaciones.Add(new Ocupacion(fechaReserva, inicio, fin));
                }

                RefrescarTurnosDisponibles();
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

        private void FormatearColumnasOcupados()
        {
            if (gridOcupados == null || gridOcupados.Columns.Count == 0) return;

            if (gridOcupados.Columns["id_reserva"] is DataGridViewColumn colIdRes) colIdRes.Visible = false;
            if (gridOcupados.Columns["id_cancha"] is DataGridViewColumn colIdCan) colIdCan.Visible = false;
            if (gridOcupados.Columns["cancha"] is DataGridViewColumn colCan) colCan.HeaderText = "Cancha";
            if (gridOcupados.Columns["fecha"] is DataGridViewColumn colFec)
            {
                colFec.HeaderText = "Fecha";
                colFec.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (gridOcupados.Columns["dia_semana"] is DataGridViewColumn colDia) colDia.HeaderText = "Dia";
            if (gridOcupados.Columns["hora_inicio"] is DataGridViewColumn colIni)
            {
                colIni.HeaderText = "Inicio";
                colIni.DefaultCellStyle.Format = @"hh\:mm";
            }
            if (gridOcupados.Columns["hora_fin"] is DataGridViewColumn colFin)
            {
                colFin.HeaderText = "Fin";
                colFin.DefaultCellStyle.Format = @"hh\:mm";
            }
            if (gridOcupados.Columns["cliente"] is DataGridViewColumn colCli) colCli.HeaderText = "Cliente que reservó";
            if (gridOcupados.Columns["estado"] is DataGridViewColumn colEst) colEst.HeaderText = "Estado";
            if (gridOcupados.Columns["reserva_fija"] is DataGridViewColumn colRf) colRf.HeaderText = "Reserva fija";

            foreach (DataGridViewRow row in gridOcupados.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(71, 71, 71);
                row.DefaultCellStyle.ForeColor = Color.LightGray;
            }
        }

        private static string ClaveReserva(DateTime fecha, TimeSpan inicio, TimeSpan fin)
            => $"{fecha:yyyyMMdd}|{inicio:c}|{fin:c}";

        private static bool TryObtenerIdCombo(ComboBox combo, out int id)
        {
            id = 0;
            if (combo.SelectedValue == null || combo.SelectedValue == DBNull.Value) return false;

            try
            {
                if (combo.SelectedValue is DataRowView drv)
                {
                    string col = combo.ValueMember;
                    if (!string.IsNullOrWhiteSpace(col) && drv.Row.Table.Columns.Contains(col) && drv[col] != DBNull.Value)
                    {
                        id = Convert.ToInt32(drv[col], CultureInfo.InvariantCulture);
                        return true;
                    }
                    return false;
                }
                id = Convert.ToInt32(combo.SelectedValue, CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
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

        private void RefrescarTurnosDisponibles()
        {
            Turno? previo = cmbHorario.SelectedItem is Turno t ? t : null;

            List<Turno> disponibles = HorariosTurnos.FiltrarDisponibles(
                HorariosTurnos.GenerarTurnos(),
                new[] { dtpFecha.Value.Date },
                _ocupaciones,
                DateTime.Now);

            bool cargandoAntes = _cargando;
            _cargando = true;
            try
            {
                cmbHorario.DataSource = disponibles;
                cmbHorario.SelectedIndex = previo.HasValue ? disponibles.IndexOf(previo.Value) : -1;
            }
            finally
            {
                _cargando = cargandoAntes;
            }
        }

        private bool SeleccionarTurno(TimeSpan inicio)
        {
            for (int i = 0; i < cmbHorario.Items.Count; i++)
            {
                if (cmbHorario.Items[i] is Turno turno && turno.Inicio == inicio)
                {
                    cmbHorario.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        private bool TryObtenerTurnoSeleccionado(out TimeSpan inicio, out TimeSpan fin)
        {
            inicio = default; fin = default;

            if (cmbHorario.SelectedItem is not Turno turno)
            {
                MessageBox.Show("Seleccioná un horario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            inicio = turno.Inicio;
            fin = turno.Fin;
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
                if (value != null && value != DBNull.Value) return Convert.ToInt32(value, CultureInfo.InvariantCulture);
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
            if (!TryObtenerIdCombo(cmbCliente, out idCliente)) { MessageBox.Show("Seleccioná un cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (!TryObtenerIdCombo(cmbCancha, out idCancha)) { MessageBox.Show("Seleccioná una cancha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            return TryObtenerTurnoSeleccionado(out inicio, out fin);
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

            if (HorariosTurnos.YaPaso(fecha, new Turno(inicio, fin), DateTime.Now))
            {
                MessageBox.Show("Ese horario ya pasó. Elegí uno posterior.", "Horario vencido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RefrescarTurnosDisponibles();
                ActualizarEstadoHorario();
                return;
            }

            if (ExisteSolapamiento(fecha, idCancha, inicio, fin))
            {
                MessageBox.Show("Ese rango horario ya está ocupado. Elegí otro horario.", "Horario ocupado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarHorasOcupadas();
                return;
            }
            CrearReserva(idCliente, idCancha, fecha, inicio, fin);
        }

        private void CrearReserva(int idCliente, int idCancha, DateTime fecha, TimeSpan inicio, TimeSpan fin)
        {
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

        private int ObtenerUsuarioActual() => SesionActual.HayUsuario ? SesionActual.IdUsuario : 1;

        private void ReservasFijas_Boton_Click(object sender, EventArgs e)
        {
            using var form = new FormReservasFijas();
            form.ShowDialog(this);
            CargarHorasOcupadas();
        }

        private void ActualizarEstadoHorario()
        {
            if (cmbHorario.Items.Count == 0)
            {
                lblEstado.Text = "No quedan horarios disponibles para esta fecha";
                lblEstado.ForeColor = Color.IndianRed;
                return;
            }

            if (cmbHorario.SelectedItem is not Turno turno)
            {
                lblEstado.Text = "Seleccioná un horario";
                lblEstado.ForeColor = Color.LightGray;
                return;
            }
            TimeSpan inicio = turno.Inicio;
            TimeSpan fin = turno.Fin;

            if (TryObtenerIdCombo(cmbCancha, out int cancha) && ExisteSolapamiento(dtpFecha.Value.Date, cancha, inicio, fin))
            {
                string cliente = ObtenerClienteDelSolapamiento(dtpFecha.Value.Date, cancha, inicio, fin);
                lblEstado.Text = string.IsNullOrWhiteSpace(cliente) ? "Ocupado / bloqueado" : $"Ocupado por: {cliente}";
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
            idReserva = 0;
            fecha = DateTime.MinValue;
            inicio = default;
            fin = default;

            if (gridOcupados.CurrentRow == null || gridOcupados.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccioná una reserva de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                var row = gridOcupados.CurrentRow;
                var idVal = gridOcupados.Columns.Contains("id_reserva") ? row.Cells["id_reserva"]?.Value : (row.Cells.Count > 0 ? row.Cells[0]?.Value : null);
                if (idVal == null || idVal == DBNull.Value) return false;

                idReserva = Convert.ToInt32(idVal, CultureInfo.InvariantCulture);
                fecha = Convert.ToDateTime(row.Cells["fecha"]?.Value).Date;
                inicio = (TimeSpan)row.Cells["hora_inicio"]!.Value!;
                fin = (TimeSpan)row.Cells["hora_fin"]!.Value!;
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

            DialogResult confirmar = MessageBox.Show(
                "¿Querés cancelar la reserva seleccionada?\nLa hora quedará disponible para volver a alquilarla.",
                "Cancelar reserva", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;

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

            DialogResult confirmar = MessageBox.Show(
                "Se cancelará la reserva seleccionada y se cargará su fecha y horario para poder alquilarlo nuevamente.\n\n¿Continuar?",
                "Realquilar hora", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;

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
                CargarHorasOcupadas();
                SeleccionarTurno(inicio);
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
            cmbHorario.SelectedIndex = -1;
            lblEstado.Text = "";
            CargarHorasOcupadas();
        }

        private void Volver_Boton_Click(object sender, EventArgs e) => Close();
    }
}
