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
            DateTime finAnioActual = new DateTime(DateTime.Today.Year, 12, 31);
            dtpDesde.MinDate = DateTime.Today;
            dtpDesde.MaxDate = finAnioActual;
            dtpHasta.MinDate = DateTime.Today;
            dtpHasta.MaxDate = finAnioActual;
            dtpHasta.Enabled = false;
            dtpDesde.ValueChanged += (_, _) => AjustarFechaHasta();
            AjustarFechaHasta();
            txtHoraInicio.TextChanged += Hora_TextChanged;
            txtHoraFin.TextChanged += Hora_TextChanged;
            CargarClientes();
            CargarCanchas();
            CargarReservasFijas();
        }

        private void AjustarFechaHasta()
        {
            DateTime finAnioActual = new DateTime(DateTime.Today.Year, 12, 31);
            dtpHasta.MinDate = dtpDesde.Value.Date;
            dtpHasta.MaxDate = finAnioActual;
            dtpHasta.Enabled = false;

            if (cmbPeriodicidad.Text == "Mensual")
            {
                DateTime limite = dtpDesde.Value.Date.AddMonths(1);
                if (limite.Year != DateTime.Today.Year)
                    limite = finAnioActual;

                dtpHasta.Value = limite > dtpHasta.MaxDate ? dtpHasta.MaxDate : limite;
                lblInfo.Text = $"Mensual: todos los {cmbDia.Text} desde {dtpDesde.Value:dd/MM/yyyy} hasta {dtpHasta.Value:dd/MM/yyyy}.";
            }
            else
            {
                dtpHasta.Value = finAnioActual;
                lblInfo.Text = $"Anual: todos los {cmbDia.Text} desde {dtpDesde.Value:dd/MM/yyyy} hasta {finAnioActual:dd/MM/yyyy}.";
            }
        }

        private void cmbPeriodicidad_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AjustarFechaHasta();
        }

        private void cmbDia_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AjustarFechaHasta();
        }

        private static void SoloHora_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == ':') return;
            e.Handled = true;
        }

        private static void Hora_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox txt || txt.Text.Contains(':')) return;

            // Si se escriben cuatro dígitos (por ejemplo 2000), queda automáticamente 20:00.
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

        private static bool TryParseHora(string texto, out TimeSpan hora)
            => TimeSpan.TryParseExact(texto.Trim(), new[] { @"hh\:mm", @"h\:mm" }, CultureInfo.InvariantCulture, out hora);

        private static bool TryObtenerMinutosTurno(TimeSpan hora, out int minutos)
        {
            minutos = 0;
            int total = (int)hora.TotalMinutes;
            if (total >= 8 * 60 && total <= 23 * 60 + 59) { minutos = total; return true; }
            if (total >= 0 && total <= 3 * 60) { minutos = total + 24 * 60; return true; }
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

        private void CargarReservasFijas()
        {
            try
            {
                using var conn = Conexion.ObtenerConexion(); conn.Open();
                const string sql = @"SELECT r.id_reserva,r.id_cliente,r.id_cancha,r.fecha,r.estado,r.reserva_fija,
                                            c.nombre cancha, CONCAT(p.apellido, ', ', p.nombre) cliente,
                                            h.hora_inicio,h.hora_fin
                                     FROM reserva r
                                     INNER JOIN cliente cl ON cl.id_cliente=r.id_cliente
                                     INNER JOIN persona p ON p.id_persona=cl.id_persona
                                     INNER JOIN cancha c ON c.id_cancha=r.id_cancha
                                     INNER JOIN horario h ON h.id_horario=r.id_horario
                                     WHERE r.reserva_fija IS NOT NULL
                                     ORDER BY r.fecha,h.hora_inicio";
                var dt = new DataTable(); using var da = new MySqlDataAdapter(sql, conn); da.Fill(dt); gridFijas.DataSource = dt;
                if (gridFijas.Columns.Count > 0)
                {
                    gridFijas.Columns["id_reserva"].Visible=false; gridFijas.Columns["id_cliente"].Visible=false; gridFijas.Columns["id_cancha"].Visible=false;
                    gridFijas.Columns["reserva_fija"].HeaderText="Serie desde"; gridFijas.Columns["fecha"].HeaderText="Fecha"; gridFijas.Columns["estado"].HeaderText="Estado";
                    gridFijas.Columns["cancha"].HeaderText="Cancha"; gridFijas.Columns["cliente"].HeaderText="Cliente"; gridFijas.Columns["hora_inicio"].HeaderText="Inicio"; gridFijas.Columns["hora_fin"].HeaderText="Fin";
                    gridFijas.Columns["fecha"].DefaultCellStyle.Format="dd/MM/yyyy"; gridFijas.Columns["hora_inicio"].DefaultCellStyle.Format=@"hh\:mm"; gridFijas.Columns["hora_fin"].DefaultCellStyle.Format=@"hh\:mm";
                    foreach (DataGridViewRow row in gridFijas.Rows)
                        if (Convert.ToString(row.Cells["estado"].Value) == "Cancelada") row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
            catch (MySqlException ex) { MessageBox.Show("Error al cargar reservas fijas:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

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

        private void CrearFija_Boton_Click(object sender, EventArgs e)
        {
            if(!TryObtenerIdCombo(cmbCliente,out int idCliente)||!TryObtenerIdCombo(cmbCancha,out int idCancha)||cmbDia.SelectedItem==null){MessageBox.Show("Completá cliente, cancha y día.","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);return;}
            if(!TryObtenerRango(out TimeSpan inicio,out TimeSpan fin,out _,out _))return;
            DateTime desde=dtpDesde.Value.Date,hasta=dtpHasta.Value.Date;
            DateTime finAnioActual = new DateTime(DateTime.Today.Year, 12, 31);
            if(desde.Year != DateTime.Today.Year || hasta.Year != DateTime.Today.Year)
            {
                MessageBox.Show($"Las reservas fijas solo pueden realizarse durante {DateTime.Today.Year}. No se permiten fechas de {DateTime.Today.Year + 1}.","Año no permitido",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if(hasta<desde){MessageBox.Show("La fecha final no puede ser anterior a la inicial.","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);return;}
            if(hasta>finAnioActual){MessageBox.Show($"La reserva fija no puede superar el {finAnioActual:dd/MM/yyyy}.","Fecha no permitida",MessageBoxButtons.OK,MessageBoxIcon.Warning);return;}
            DayOfWeek dia=(DayOfWeek)cmbDia.SelectedIndex; string periodicidad=cmbPeriodicidad.Text;
            var fechas=GenerarFechas(desde,hasta,dia,periodicidad)
                .Where(f => f.Year == DateTime.Today.Year)
                .ToList();
            if(fechas.Count==0){MessageBox.Show("No hay fechas que coincidan con el día seleccionado.","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);return;}
            try
            {
                using var conn=Conexion.ObtenerConexion();conn.Open();using var tx=conn.BeginTransaction();
                foreach(DateTime fecha in fechas)
                {
                    if(ExisteSolapamiento(conn,tx,fecha,idCancha,inicio,fin)){tx.Rollback();MessageBox.Show($"La fecha {fecha:dd/MM/yyyy} ya está ocupada. No se creó la reserva fija.","Solapamiento",MessageBoxButtons.OK,MessageBoxIcon.Warning);return;}
                    int idHorario=ObtenerOCrearHorario(conn,tx,idCancha,fecha,inicio,fin);
                    const string ins=@"INSERT INTO reserva(fecha,reserva_fija,estado,id_cliente,id_cancha,id_horario,id_usuario) VALUES(@fecha,@serie,'Confirmada',@cliente,@cancha,@horario,@usuario)";
                    using var cmd=new MySqlCommand(ins,conn,tx);cmd.Parameters.AddWithValue("@fecha",fecha);cmd.Parameters.AddWithValue("@serie",desde);cmd.Parameters.AddWithValue("@cliente",idCliente);cmd.Parameters.AddWithValue("@cancha",idCancha);cmd.Parameters.AddWithValue("@horario",idHorario);cmd.Parameters.AddWithValue("@usuario",1);cmd.ExecuteNonQuery();
                }
                tx.Commit();MessageBox.Show($"Reserva fija creada. Se generaron {fechas.Count} ocurrencias.","Éxito",MessageBoxButtons.OK,MessageBoxIcon.Information);CargarReservasFijas();
            }
            catch(MySqlException ex){MessageBox.Show("Error al crear reserva fija:\n"+ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);}
        }

        private void Realquilar_Boton_Click(object sender,EventArgs e)
        {
            if(gridFijas.CurrentRow==null)return;
            string estado=Convert.ToString(gridFijas.CurrentRow.Cells["estado"].Value)??"";
            if(estado!="Cancelada"){MessageBox.Show("Primero cancelá la ocurrencia seleccionada para poder realquilarla.","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);return;}
            int cliente=Convert.ToInt32(gridFijas.CurrentRow.Cells["id_cliente"].Value);int cancha=Convert.ToInt32(gridFijas.CurrentRow.Cells["id_cancha"].Value);DateTime fecha=Convert.ToDateTime(gridFijas.CurrentRow.Cells["fecha"].Value);TimeSpan inicio=(TimeSpan)gridFijas.CurrentRow.Cells["hora_inicio"].Value;TimeSpan fin=(TimeSpan)gridFijas.CurrentRow.Cells["hora_fin"].Value;
            using var form=new FormGestionHorarios(cliente,cancha,fecha,inicio,fin);form.ShowDialog(this);CargarReservasFijas();
        }

        private void Cancelar_Boton_Click(object sender,EventArgs e)
        {
            if(gridFijas.CurrentRow==null)return;int id=Convert.ToInt32(gridFijas.CurrentRow.Cells["id_reserva"].Value);
            using var conn=Conexion.ObtenerConexion();conn.Open();using var cmd=new MySqlCommand("UPDATE reserva SET estado='Cancelada' WHERE id_reserva=@id",conn);cmd.Parameters.AddWithValue("@id",id);cmd.ExecuteNonQuery();CargarReservasFijas();
        }
        private void Volver_Boton_Click(object sender,EventArgs e)=>Close();
    }
}
