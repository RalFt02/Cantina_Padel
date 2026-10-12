using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace Cantina_Padel
{
    public partial class FormReservasFijas : Form
    {
        public FormReservasFijas()
        {
            InitializeComponent();
            dtpDesde.MinDate = DateTime.Today;
            dtpHasta.MinDate = DateTime.Today.AddDays(7);
            dtpHasta.Enabled = false;
            dtpDesde.ValueChanged += (_, _) => AjustarFechaHasta();
            AjustarFechaHasta();
            cmbCancha.SelectedIndexChanged += (_, _) => RefrescarTurnosDisponibles();
            CargarClientes();
            CargarCanchas();
            CargarReservasFijas();
        }

        /// <summary>
        /// Calcula la fecha final de la serie.
        /// Mensual = 30 días desde la fecha de inicio.
        /// Anual = 1 año desde la fecha de inicio.
        /// </summary>
        /// <summary>
        /// Obtiene la fecha real de inicio de la serie aplicando una semana de anticipación.
        /// Ejemplo: si se solicita el 22/09/2026, la primera reserva será el 29/09/2026.
        /// </summary>
        private DateTime ObtenerFechaInicioReserva()
        {
            return dtpDesde.Value.Date.AddDays(7);
        }

        /// <summary>
        /// Calcula la fecha final de la serie a partir de la fecha real de inicio.
        /// Mensual = 30 días desde el inicio efectivo.
        /// Anual = 1 año desde el inicio efectivo.
        /// </summary>
        private void AjustarFechaHasta()
        {
            DateTime inicioReserva = ObtenerFechaInicioReserva();
            dtpHasta.MinDate = inicioReserva;
            dtpHasta.Enabled = false;

            if (cmbPeriodicidad.Text == "Mensual")
            {
                dtpHasta.Value = inicioReserva.AddDays(30);
                lblInfo.Text = $"Solicitud: {dtpDesde.Value:dd/MM/yyyy}. Reserva desde {inicioReserva:dd/MM/yyyy} hasta {dtpHasta.Value:dd/MM/yyyy} (30 días).";
            }
            else
            {
                dtpHasta.Value = inicioReserva.AddYears(1);
                lblInfo.Text = $"Solicitud: {dtpDesde.Value:dd/MM/yyyy}. Reserva desde {inicioReserva:dd/MM/yyyy} hasta {dtpHasta.Value:dd/MM/yyyy} (1 año).";
            }

            RefrescarTurnosDisponibles();
        }

        private void cmbPeriodicidad_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AjustarFechaHasta();
        }

        private void cmbDia_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AjustarFechaHasta();
        }

        private bool TryObtenerTurnoSeleccionado(out Turno turno)
        {
            if (cmbHorario.SelectedItem is Turno seleccionado)
            {
                turno = seleccionado;
                return true;
            }

            turno = default;
            MessageBox.Show("Seleccioná un horario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // Reservas activas de la cancha en el período de la serie (se usan para ocultar turnos ocupados).
        private List<Ocupacion> CargarOcupaciones(int idCancha, DateTime desde, DateTime hasta)
        {
            var lista = new List<Ocupacion>();
            try
            {
                using var conn = Conexion.ObtenerConexion(); conn.Open();
                const string sql = @"SELECT r.fecha, h.hora_inicio, h.hora_fin
                                     FROM reserva r INNER JOIN horario h ON h.id_horario = r.id_horario
                                     WHERE r.id_cancha = @cancha AND r.fecha BETWEEN @desde AND @hasta
                                       AND r.estado IN ('Pendiente','Confirmada')";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cancha", idCancha);
                cmd.Parameters.AddWithValue("@desde", desde.AddDays(-1));
                cmd.Parameters.AddWithValue("@hasta", hasta.AddDays(1));
                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                    lista.Add(new Ocupacion(rd.GetDateTime("fecha").Date, rd.GetTimeSpan("hora_inicio"), rd.GetTimeSpan("hora_fin")));
            }
            catch (MySqlException ex) { MessageBox.Show("Error al consultar reservas:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return lista;
        }

        // Arma el desplegable con los turnos libres en TODAS las fechas de la serie (cancha + día + período).
        // Si todavía no hay cancha o día elegidos, muestra todos los turnos.
        private void RefrescarTurnosDisponibles()
        {
            Turno? previo = cmbHorario.SelectedItem is Turno t ? t : null;
            List<Turno> disponibles = HorariosTurnos.GenerarTurnos().ToList();

            if (cmbDia.SelectedIndex >= 0 && TryObtenerIdCombo(cmbCancha, out int idCancha))
            {
                DayOfWeek dia = (DayOfWeek)((cmbDia.SelectedIndex + 1) % 7);
                DateTime inicioReserva = ObtenerFechaInicioReserva();
                List<DateTime> fechas = GenerarFechas(inicioReserva, dtpHasta.Value.Date, dia, cmbPeriodicidad.Text)
                    .ToList();

                disponibles = HorariosTurnos.FiltrarDisponibles(
                    HorariosTurnos.GenerarTurnos(),
                    fechas,
                    CargarOcupaciones(idCancha, dtpDesde.Value.Date, dtpHasta.Value.Date));
            }

            cmbHorario.DataSource = disponibles;
            cmbHorario.SelectedIndex = previo.HasValue ? disponibles.IndexOf(previo.Value) : -1;
        }

        private void CargarClientes()
        {
            try
            {
                using var conn = Conexion.ObtenerConexion(); conn.Open();
                const string sql = @"SELECT c.id_cliente, CONCAT(p.apellido, ', ', p.nombre, ' - DNI ', p.dni) cliente
                                     FROM cliente c INNER JOIN persona p ON p.id_persona=c.id_persona
                                     WHERE p.activo=1 ORDER BY p.apellido,p.nombre";
                var dt = new DataTable(); using var da = new MySqlDataAdapter(sql, conn); da.Fill(dt);
                cmbCliente.DataSource = dt; cmbCliente.DisplayMember = "cliente"; cmbCliente.ValueMember = "id_cliente";
            }
            catch (MySqlException ex) { MessageBox.Show("Error al cargar clientes:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CargarCanchas()
        {
            try
            {
                using var conn = Conexion.ObtenerConexion(); conn.Open();
                const string sql = "SELECT id_cancha,nombre FROM cancha WHERE activo=1 AND estado='Activo' ORDER BY nombre";
                var dt = new DataTable(); using var da = new MySqlDataAdapter(sql, conn); da.Fill(dt);
                cmbCancha.DataSource = dt; cmbCancha.DisplayMember = "nombre"; cmbCancha.ValueMember = "id_cancha";
            }
            catch (MySqlException ex) { MessageBox.Show("Error al cargar las canchas:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private static bool TryObtenerIdCombo(ComboBox combo, out int id)
        {
            id = 0;
            if (combo.SelectedValue == null || combo.SelectedValue == DBNull.Value) return false;
            try { id = Convert.ToInt32(combo.SelectedValue, CultureInfo.InvariantCulture); return true; }
            catch (InvalidCastException)
            {
                if (combo.SelectedItem is DataRowView row && row.Row.Table.Columns.Contains(combo.ValueMember))
                { id = Convert.ToInt32(row[combo.ValueMember], CultureInfo.InvariantCulture); return true; }
                return false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Carga las reservas fijas ordenadas por fecha, hora y cancha.
        /// </summary>
        private void CargarReservasFijas()
        {
            try
            {
                using var conn = Conexion.ObtenerConexion();
                conn.Open();

                const string sql = @"SELECT r.id_reserva,r.id_cliente,r.id_cancha,r.fecha,h.dia_semana,r.estado,r.reserva_fija,
                                            c.nombre cancha, CONCAT(p.apellido, ', ', p.nombre) cliente,
                                            h.hora_inicio,h.hora_fin
                                     FROM reserva r
                                     INNER JOIN cliente cl ON cl.id_cliente=r.id_cliente
                                     INNER JOIN persona p ON p.id_persona=cl.id_persona
                                     INNER JOIN cancha c ON c.id_cancha=r.id_cancha
                                     INNER JOIN horario h ON h.id_horario=r.id_horario
                                     WHERE r.reserva_fija IS NOT NULL
                                     ORDER BY r.fecha,h.hora_inicio,c.nombre";

                var dt = new DataTable();
                using var da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);
                gridFijas.DataSource = dt;
                ConfigurarGridReservasFijas();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar reservas fijas:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefrescarTurnosDisponibles();
        }

        /// <summary>
        /// Configura las columnas visibles y el formato del grid de reservas fijas.
        /// </summary>
        private void ConfigurarGridReservasFijas()
        {
            if (gridFijas.Columns.Count == 0) return;

            gridFijas.Columns["id_reserva"].Visible = false;
            gridFijas.Columns["id_cliente"].Visible = false;
            gridFijas.Columns["id_cancha"].Visible = false;
            gridFijas.Columns["reserva_fija"].HeaderText = "Serie desde";
            gridFijas.Columns["fecha"].HeaderText = "Fecha";
            gridFijas.Columns["dia_semana"].HeaderText = "Día";
            gridFijas.Columns["estado"].HeaderText = "Estado";
            gridFijas.Columns["cancha"].HeaderText = "Cancha";
            gridFijas.Columns["cliente"].HeaderText = "Cliente";
            gridFijas.Columns["hora_inicio"].HeaderText = "Inicio";
            gridFijas.Columns["hora_fin"].HeaderText = "Fin";
            gridFijas.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            gridFijas.Columns["hora_inicio"].DefaultCellStyle.Format = @"hh\:mm";
            gridFijas.Columns["hora_fin"].DefaultCellStyle.Format = @"hh\:mm";
        }

        /// <summary>
        /// Genera todas las fechas que coinciden con el día de la semana elegido.
        /// </summary>
        private IEnumerable<DateTime> GenerarFechas(DateTime desde, DateTime hasta, DayOfWeek dia, string periodicidad)
        {
            DateTime d = desde.Date;

            while (d.DayOfWeek != dia)
                d = d.AddDays(1);

            for (; d <= hasta; d = d.AddDays(7))
                yield return d;
        }

        private static DateTime FechaHoraNormalizada(DateTime fecha, TimeSpan hora)
        {
            DateTime resultado = fecha.Date.Add(hora);
            if (hora.TotalMinutes <= 3 * 60) resultado = resultado.AddDays(1);
            return resultado;
        }

        private bool ExisteSolapamiento(MySqlConnection conn, MySqlTransaction tx, DateTime fecha, int idCancha, TimeSpan inicio, TimeSpan fin)
        {
            DateTime nuevoInicio = FechaHoraNormalizada(fecha, inicio);
            DateTime nuevoFin = FechaHoraNormalizada(fecha, fin);
            if (nuevoFin <= nuevoInicio) nuevoFin = nuevoFin.AddDays(1);

            const string sql = @"SELECT r.fecha,h.hora_inicio,h.hora_fin
                                 FROM reserva r INNER JOIN horario h ON h.id_horario=r.id_horario
                                 WHERE r.id_cancha=@cancha AND r.fecha BETWEEN @desde AND @hasta
                                   AND r.estado IN ('Pendiente','Confirmada')";
            using var cmd = new MySqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@cancha",idCancha); cmd.Parameters.AddWithValue("@desde",fecha.AddDays(-1)); cmd.Parameters.AddWithValue("@hasta",fecha.AddDays(1));
            using var rd=cmd.ExecuteReader();
            while(rd.Read())
            {
                DateTime fr=rd.GetDateTime("fecha").Date; TimeSpan ei=rd.GetTimeSpan("hora_inicio"); TimeSpan ef=rd.GetTimeSpan("hora_fin");
                DateTime existenteInicio=FechaHoraNormalizada(fr,ei); DateTime existenteFin=FechaHoraNormalizada(fr,ef); if(existenteFin<=existenteInicio)existenteFin=existenteFin.AddDays(1);
                if(nuevoInicio<existenteFin && nuevoFin>existenteInicio)return true;
            }
            return false;
        }

        private int ObtenerOCrearHorario(MySqlConnection conn,MySqlTransaction tx,int idCancha,DateTime fecha,TimeSpan inicio,TimeSpan fin)
        {
            string dia=NombreDia(fecha);
            const string q="SELECT id_horario FROM horario WHERE id_cancha=@c AND dia_semana=@d AND hora_inicio=@i AND hora_fin=@f LIMIT 1";
            using(var cmd=new MySqlCommand(q,conn,tx)){cmd.Parameters.AddWithValue("@c",idCancha);cmd.Parameters.AddWithValue("@d",dia);cmd.Parameters.AddWithValue("@i",inicio);cmd.Parameters.AddWithValue("@f",fin);var x=cmd.ExecuteScalar();if(x!=null)return Convert.ToInt32(x,CultureInfo.InvariantCulture);}
            const string ins="INSERT INTO horario(hora_inicio,hora_fin,id_cancha,dia_semana) VALUES(@i,@f,@c,@d); SELECT LAST_INSERT_ID();";
            using var ic=new MySqlCommand(ins,conn,tx);ic.Parameters.AddWithValue("@i",inicio);ic.Parameters.AddWithValue("@f",fin);ic.Parameters.AddWithValue("@c",idCancha);ic.Parameters.AddWithValue("@d",dia);return Convert.ToInt32(ic.ExecuteScalar(),CultureInfo.InvariantCulture);
        }

        private static string NombreDia(DateTime d)=>d.DayOfWeek switch{DayOfWeek.Monday=>"Lunes",DayOfWeek.Tuesday=>"Martes",DayOfWeek.Wednesday=>"Miércoles",DayOfWeek.Thursday=>"Jueves",DayOfWeek.Friday=>"Viernes",DayOfWeek.Saturday=>"Sábado",_=>"Domingo"};

        /// <summary>
        /// Calcula el inicio efectivo de una reserva fija a partir de la fecha de solicitud.
        /// </summary>
        private DateTime CalcularFechaInicioReserva(DateTime fechaSolicitud)
        {
            return fechaSolicitud.Date.AddDays(7);
        }

        /// <summary>
        /// Calcula el final de la serie según la periodicidad.
        /// </summary>
        private DateTime CalcularFechaFinReserva(DateTime inicioReserva, string periodicidad)
        {
            return periodicidad == "Mensual"
                ? inicioReserva.Date.AddDays(30)
                : inicioReserva.Date.AddYears(1);
        }

        /// <summary>
        /// Valida que la fecha de solicitud no sea anterior a hoy.
        /// La reserva efectiva se genera una semana después.
        /// </summary>
        private bool ValidarAnticipacion(DateTime fechaSolicitud, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (fechaSolicitud.Date < DateTime.Today)
            {
                mensajeError = "La fecha de solicitud no puede ser anterior a hoy.";
                return false;
            }

            return true;
        }

        private bool CrearReservaFija(int idCliente, int idCancha, string diaTexto, int diaIndex, string periodicidad, TimeSpan inicio, TimeSpan fin, DateTime desde, DateTime hasta, out string mensajeError, out int ocurrenciasGeneradas)
        {
            mensajeError = string.Empty;
            ocurrenciasGeneradas = 0;

            if (!HorariosTurnos.EsTurnoValido(new Turno(inicio, fin)))
            {
                mensajeError = "El horario debe ser un turno de 1 hora entre las 08:00 y las 03:00 del día siguiente.";
                return false;
            }

            if (!ValidarAnticipacion(desde, out mensajeError))
                return false;

            DateTime inicioReserva = CalcularFechaInicioReserva(desde);
            DateTime finReserva = CalcularFechaFinReserva(inicioReserva, periodicidad);

            if (finReserva < inicioReserva)
            {
                mensajeError = "La fecha final no puede ser anterior a la inicial.";
                return false;
            }

            DayOfWeek dia = (DayOfWeek)((diaIndex + 1) % 7);
            var fechas = GenerarFechas(inicioReserva, finReserva, dia, periodicidad).ToList();

            if (fechas.Count == 0)
            {
                mensajeError = "No hay fechas que coincidan con el día seleccionado.";
                return false;
            }

            try
            {
                using var conn = Conexion.ObtenerConexion();
                conn.Open();
                using var tx = conn.BeginTransaction();

                foreach (DateTime fecha in fechas)
                {
                    if (ExisteSolapamiento(conn, tx, fecha, idCancha, inicio, fin))
                    {
                        tx.Rollback();
                        mensajeError = $"La fecha {fecha:dd/MM/yyyy} ya está ocupada. No se creó la reserva fija.";
                        return false;
                    }

                    int idHorario = ObtenerOCrearHorario(conn, tx, idCancha, fecha, inicio, fin);
                    const string ins = @"INSERT INTO reserva(fecha,reserva_fija,estado,id_cliente,id_cancha,id_horario,id_usuario) 
                                VALUES(@fecha,@serie,'Confirmada',@cliente,@cancha,@horario,@usuario)";
                    using var cmd = new MySqlCommand(ins, conn, tx);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@serie", inicioReserva);
                    cmd.Parameters.AddWithValue("@cliente", idCliente);
                    cmd.Parameters.AddWithValue("@cancha", idCancha);
                    cmd.Parameters.AddWithValue("@horario", idHorario);
                    cmd.Parameters.AddWithValue("@usuario", 1);
                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
                ocurrenciasGeneradas = fechas.Count;
                return true;
            }
            catch (MySqlException ex)
            {
                mensajeError = "Error al crear reserva fija:\n" + ex.Message;
                return false;
            }
        }
        private void CrearFija_Boton_Click(object sender, EventArgs e)
        {
            if (!TryObtenerIdCombo(cmbCliente, out int idCliente) || !TryObtenerIdCombo(cmbCancha, out int idCancha) || cmbDia.SelectedItem == null)
            {
                MessageBox.Show("Completá cliente, cancha y día.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryObtenerTurnoSeleccionado(out Turno turno)) return;

            if (CrearReservaFija(idCliente, idCancha, cmbDia.Text, cmbDia.SelectedIndex, cmbPeriodicidad.Text, turno.Inicio, turno.Fin, dtpDesde.Value.Date, dtpHasta.Value.Date, out string mensajeError, out int ocurrencias))
            {
                MessageBox.Show($"Reserva fija creada. Se generaron {ocurrencias} ocurrencias.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarReservasFijas();
            }
            else if (!string.IsNullOrEmpty(mensajeError))
            {
                MessageBox.Show(mensajeError, "Aviso / Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Cancelar_Boton_Click(object sender, EventArgs e)
        {
            if (gridFijas.CurrentRow == null || gridFijas.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccioná una reserva fija de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow fila = gridFijas.CurrentRow;

                if (fila.Cells["reserva_fija"].Value == null || fila.Cells["reserva_fija"].Value == DBNull.Value)
                {
                    MessageBox.Show("La fila seleccionada no pertenece a una reserva fija.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fechaSeleccionada = Convert.ToDateTime(fila.Cells["fecha"].Value).Date;
                DateTime serie = Convert.ToDateTime(fila.Cells["reserva_fija"].Value).Date;
                string estado = Convert.ToString(fila.Cells["estado"].Value) ?? string.Empty;

                if (fechaSeleccionada < DateTime.Today)
                {
                    MessageBox.Show("No se puede cancelar desde una fecha que ya pasó. Las ocurrencias anteriores quedan en el historial.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (estado == "Cancelada")
                {
                    MessageBox.Show("La ocurrencia seleccionada ya está cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmar = MessageBox.Show(
                    $"Se cancelarán todas las fechas futuras de la reserva fija iniciada el {serie:dd/MM/yyyy}.\n\nLas fechas anteriores quedarán en el historial.\n\n¿Continuar?",
                    "Cancelar reserva fija",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmar != DialogResult.Yes) return;

                int cantidadCancelada;

                using (MySqlConnection conn = Conexion.ObtenerConexion())
                {
                    conn.Open();

                    const string sql = @"
                        UPDATE reserva
                        SET estado = 'Cancelada'
                        WHERE reserva_fija = @serie
                          AND fecha >= @hoy
                          AND estado IN ('Pendiente', 'Confirmada')";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@serie", MySqlDbType.Date).Value = serie;
                        cmd.Parameters.Add("@hoy", MySqlDbType.Date).Value = DateTime.Today;
                        cantidadCancelada = cmd.ExecuteNonQuery();
                    }
                }

                if (cantidadCancelada == 0)
                {
                    MessageBox.Show(
                        $"No se encontró ninguna ocurrencia futura activa para la serie {serie:dd/MM/yyyy}.\n\nRevisá que estés ejecutando la aplicación contra la misma base de datos del dump.",
                        "Sin cambios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    $"Reserva fija cancelada correctamente.\n\nOcurrencias futuras canceladas: {cantidadCancelada}\nLas fechas anteriores permanecen en el historial.",
                    "Reserva cancelada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarReservasFijas();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de MySQL al cancelar la reserva fija:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar la reserva fija:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Realquilar_Boton_Click(object sender, EventArgs e)
        {
            if (gridFijas.CurrentRow == null || gridFijas.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccioná una reserva fija de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow fila = gridFijas.CurrentRow;
                string estado = Convert.ToString(fila.Cells["estado"].Value) ?? string.Empty;

                if (estado != "Cancelada")
                {
                    MessageBox.Show("Primero cancelá la ocurrencia para poder realquilarla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int cliente = Convert.ToInt32(fila.Cells["id_cliente"].Value);
                int cancha = Convert.ToInt32(fila.Cells["id_cancha"].Value);
                DateTime fecha = Convert.ToDateTime(fila.Cells["fecha"].Value).Date;
                TimeSpan inicio = (TimeSpan)fila.Cells["hora_inicio"].Value;
                TimeSpan fin = (TimeSpan)fila.Cells["hora_fin"].Value;

                if (fecha < DateTime.Today)
                {
                    MessageBox.Show("Una fecha pasada queda solamente en el historial y no puede realquilarse.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (FormGestionHorarios form = new FormGestionHorarios(cliente, cancha, fecha, inicio, fin))
                {
                    form.ShowDialog(this);
                }

                CargarReservasFijas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al preparar el realquiler:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Volver_Boton_Click(object sender,EventArgs e)=>Close();
    }
}