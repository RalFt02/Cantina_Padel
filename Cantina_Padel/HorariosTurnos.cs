namespace Cantina_Padel
{
    /// <summary>Un turno de 1 hora. Ej.: 08:00 - 09:00, o 23:00 - 00:00.</summary>
    public readonly record struct Turno(TimeSpan Inicio, TimeSpan Fin)
    {
        // Es lo que muestra el ComboBox.
        public override string ToString() => $"{Inicio.ToString(@"hh\:mm")} - {Fin.ToString(@"hh\:mm")}";
    }

    /// <summary>Una reserva existente (fecha de la reserva + horario guardado en la tabla horario).</summary>
    public readonly record struct Ocupacion(DateTime Fecha, TimeSpan Inicio, TimeSpan Fin);

    /// <summary>
    /// Lógica pura de turnos (sin base de datos ni controles) para poder testearla directamente.
    /// </summary>
    public static class HorariosTurnos
    {
        private const int PrimeraHora = 8;       // 08:00
        private const int UltimaHoraFin = 27;    // 03:00 del día siguiente (3 + 24)

        /// <summary>Los 19 turnos posibles: 08:00-09:00 ... 23:00-00:00, 00:00-01:00 ... 02:00-03:00.</summary>
        public static IReadOnlyList<Turno> GenerarTurnos()
        {
            var turnos = new List<Turno>();
            for (int hora = PrimeraHora; hora < UltimaHoraFin; hora++)
                turnos.Add(new Turno(TimeSpan.FromHours(hora % 24), TimeSpan.FromHours((hora + 1) % 24)));
            return turnos;
        }

        public static bool EsTurnoValido(Turno turno) => GenerarTurnos().Contains(turno);

        /// <summary>Lleva la hora al eje del turno: de 00:00 a 03:00 cuenta como día siguiente.</summary>
        public static DateTime Normalizar(DateTime fecha, TimeSpan hora)
        {
            DateTime resultado = fecha.Date.Add(hora);
            if (hora.TotalMinutes <= 3 * 60) resultado = resultado.AddDays(1);
            return resultado;
        }

        /// <summary>¿El turno, en esa fecha, pisa a la ocupación existente?</summary>
        public static bool Solapa(DateTime fecha, Turno turno, Ocupacion ocupacion)
        {
            DateTime inicio = Normalizar(fecha, turno.Inicio);
            DateTime fin = Normalizar(fecha, turno.Fin);
            if (fin <= inicio) fin = fin.AddDays(1);

            DateTime ocupadoInicio = Normalizar(ocupacion.Fecha, ocupacion.Inicio);
            DateTime ocupadoFin = Normalizar(ocupacion.Fecha, ocupacion.Fin);
            if (ocupadoFin <= ocupadoInicio) ocupadoFin = ocupadoFin.AddDays(1);

            return inicio < ocupadoFin && fin > ocupadoInicio;
        }

        /// <summary>
        /// ¿El turno de esa fecha ya empezó respecto de "ahora"?
        /// Ej.: son las 15:45 -> el turno 15:00-16:00 y todos los anteriores ya pasaron.
        /// Los turnos de 00:00 a 03:00 cuentan como del día siguiente (ver Normalizar).
        /// </summary>
        public static bool YaPaso(DateTime fecha, Turno turno, DateTime ahora)
            => Normalizar(fecha, turno.Inicio) <= ahora;

        /// <summary>
        /// Devuelve solo los turnos libres en TODAS las fechas indicadas.
        /// Reserva común: pasar una sola fecha. Reserva fija: pasar todas las fechas de la serie.
        /// Si se pasa "ahora", también se descartan los turnos que ya pasaron.
        /// </summary>
        public static List<Turno> FiltrarDisponibles(
            IEnumerable<Turno> turnos,
            IEnumerable<DateTime> fechas,
            IEnumerable<Ocupacion> ocupadas,
            DateTime? ahora = null)
        {
            List<DateTime> listaFechas = fechas.Select(f => f.Date).Distinct().ToList();
            List<Ocupacion> listaOcupadas = ocupadas.ToList();

            return turnos
                .Where(t => ahora is null || !listaFechas.Any(f => YaPaso(f, t, ahora.Value)))
                .Where(t => !listaFechas.Any(f => listaOcupadas.Any(o => Solapa(f, t, o))))
                .ToList();
        }
    }
}
