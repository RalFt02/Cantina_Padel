using MySql.Data.MySqlClient;
using System.Data;

namespace Cantina_Padel
{
    /// <summary>
    /// Lógica de negocio para Caja y Retiros.
    /// Todos los métodos son estáticos y testeables de forma independiente.
    /// </summary>
    public static class CajaService
    {
        // ═══════════════════════════════════════════════════
        //  VALIDACIONES (puras, sin BD — fáciles de testear)
        // ═══════════════════════════════════════════════════

        /// <summary>Valida que el monto de inicio sea mayor a cero.</summary>
        public static bool MontoInicioEsValido(decimal monto) => monto > 0;

        /// <summary>Valida que el monto de retiro sea mayor a cero.</summary>
        public static bool MontoRetiroEsValido(decimal monto) => monto > 0;

        /// <summary>Valida que el motivo no esté vacío.</summary>
        public static bool MotivoEsValido(string motivo) =>
            !string.IsNullOrWhiteSpace(motivo);

        /// <summary>
        /// Valida que el monto de retiro no supere el efectivo disponible en caja.
        /// disponible = monto_inicio + ingresos - retiros anteriores
        /// </summary>
        public static bool RetiroNoCierraEnNegativo(decimal montoRetiro, decimal disponible) =>
            montoRetiro <= disponible;

        /// <summary>Valida que el texto ingresado sea un decimal positivo válido.</summary>
        public static bool TryParseMonto(string texto, out decimal monto)
        {
            monto = 0;
            if (string.IsNullOrWhiteSpace(texto)) return false;
            string normalizado = texto.Replace(',', '.');
            if (!decimal.TryParse(normalizado,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out monto)) return false;
            return monto >= 0;
        }

        /// <summary>
        /// Calcula el efectivo disponible en caja:
        /// monto_inicio + suma_ventas_efectivo - suma_retiros
        /// </summary>
        public static decimal CalcularDisponible(decimal montoInicio,
                                                  decimal totalVentas,
                                                  decimal totalRetiros)
            => montoInicio + totalVentas - totalRetiros;

        // ═══════════════════════════════════════════════════
        //  CONSULTAS A LA BD
        // ═══════════════════════════════════════════════════

        /// <summary>
        /// Devuelve la sesión abierta de la caja dada que pertenece al usuario indicado,
        /// o null si ese usuario no tiene una caja abierta.
        /// </summary>
        public static DataRow? ObtenerSesionAbierta(int idCaja, int idUsuario)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT c.id_sesion, c.id_caja, c.fecha_apertura,
                       c.monto_inicio, u.nombre_completo AS cajero
                FROM caja c
                INNER JOIN usuario u ON u.id_usuario = c.id_usuario_apertura
                WHERE c.id_caja = @id
                  AND c.estado = 'Abierta'
                  AND c.id_usuario_apertura = @usuario
                LIMIT 1";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", idCaja);
            cmd.Parameters.AddWithValue("@usuario", idUsuario);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        /// <summary>
        /// Devuelve la sesión abierta de la caja dada, sin importar quién la abrió,
        /// o null si la caja está libre. Sirve para detectar que otro usuario la tiene abierta.
        /// </summary>
        public static DataRow? ObtenerSesionAbiertaDeCaja(int idCaja)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT c.id_sesion, c.id_caja, c.fecha_apertura,
                       c.monto_inicio, c.id_usuario_apertura,
                       u.nombre_completo AS cajero
                FROM caja c
                INNER JOIN usuario u ON u.id_usuario = c.id_usuario_apertura
                WHERE c.id_caja = @id AND c.estado = 'Abierta'
                LIMIT 1";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", idCaja);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        /// <summary>Devuelve todas las sesiones (abiertas y cerradas) para mostrar en grilla.</summary>
        public static DataTable ObtenerTodasLasSesiones()
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT c.id_sesion      AS 'ID',
                       c.id_caja        AS 'N° Caja',
                       u.nombre_completo AS 'Cajero',
                       c.fecha_apertura  AS 'Apertura',
                       c.fecha_cierre    AS 'Cierre',
                       c.monto_inicio    AS 'Inicio ($)',
                       c.monto_cierre    AS 'Cierre ($)',
                       c.estado          AS 'Estado'
                FROM caja c
                INNER JOIN usuario u ON u.id_usuario = c.id_usuario_apertura
                ORDER BY c.fecha_apertura DESC";
            using MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>Devuelve los retiros de una sesión.</summary>
        public static DataTable ObtenerRetirosDeSesion(int idSesion)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT r.fecha   AS 'Fecha',
                       r.monto          AS 'Monto ($)',
                       r.motivo         AS 'Motivo',
                       u.nombre_completo AS 'Registró'
                FROM retiro_efectivo r
                INNER JOIN usuario u ON u.id_usuario = r.id_usuario
                WHERE r.id_sesion = @sesion
                ORDER BY r.fecha";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@sesion", idSesion);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>Calcula el total retirado en una sesión.</summary>
        public static decimal ObtenerTotalRetiros(int idSesion)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT COALESCE(SUM(monto), 0)
                FROM retiro_efectivo
                WHERE id_sesion = @sesion";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@sesion", idSesion);
            object? result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToDecimal(result);
        }

        /// <summary>Suma de ventas cobradas en efectivo durante una sesión de caja.</summary>
        public static decimal ObtenerTotalVentasEfectivo(int idSesion)
        {
            GestorVentas.AsegurarTablas();
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT COALESCE(SUM(dv.total), 0)
                FROM venta v
                INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                WHERE v.id_sesion = @sesion
                  AND v.cancelada = 0
                  AND v.medio_pago = 'Efectivo'";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@sesion", idSesion);
            object? result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToDecimal(result);
        }

        /// <summary>Suma de ventas por medio de pago específico durante una sesión de caja.</summary>
        public static decimal ObtenerTotalVentasPorMedioPago(int idSesion, string medioPago)
        {
            GestorVentas.AsegurarTablas();
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT COALESCE(SUM(dv.total), 0)
                FROM venta v
                INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                WHERE v.id_sesion = @sesion
                  AND v.cancelada = 0
                  AND v.medio_pago = @medio";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@sesion", idSesion);
            cmd.Parameters.AddWithValue("@medio", medioPago);
            object? result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToDecimal(result);
        }

        /// <summary>Efectivo que debería haber en caja: inicio + ventas en efectivo - retiros.</summary>
        public static decimal CalcularEfectivoEsperado(int idSesion, decimal montoInicio)
            => CalcularDisponible(montoInicio,
                                  ObtenerTotalVentasEfectivo(idSesion),
                                  ObtenerTotalRetiros(idSesion));

        // ═══════════════════════════════════════════════════
        //  OPERACIONES
        // ═══════════════════════════════════════════════════

        /// <summary>
        /// Abre una nueva sesión de caja. Devuelve el id_sesion generado.
        /// Falla si la caja ya está abierta (por cualquier usuario).
        /// </summary>
        public static int AbrirCaja(int idCaja, decimal montoInicio, int idUsuario)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();

            // El INSERT solo se ejecuta si no hay otra sesión abierta en esa caja.
            const string sql = @"
                INSERT INTO caja (id_caja, fecha_apertura, monto_inicio, estado, id_usuario_apertura)
                SELECT @caja, @apertura, @inicio, 'Abierta', @usuario
                FROM DUAL
                WHERE NOT EXISTS (
                    SELECT 1 FROM caja WHERE id_caja = @caja AND estado = 'Abierta')";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@caja",     idCaja);
            cmd.Parameters.AddWithValue("@apertura", DateTime.Now);
            cmd.Parameters.AddWithValue("@inicio",   montoInicio);
            cmd.Parameters.AddWithValue("@usuario",  idUsuario);

            int filas = cmd.ExecuteNonQuery();
            if (filas == 0)
            {
                DataRow? ocupada = ObtenerSesionAbiertaDeCaja(idCaja);
                string quien = ocupada != null ? ocupada["cajero"].ToString()! : "otro usuario";
                throw new InvalidOperationException(
                    $"La caja ya está abierta por {quien}. Solo esa persona puede cerrarla.");
            }

            return Convert.ToInt32(cmd.LastInsertedId);
        }

        /// <summary>
        /// Cierra la sesión. El monto de cierre se calcula en la BD:
        /// monto_inicio + ventas en efectivo - retiros. Devuelve el monto registrado.
        /// Solo puede cerrarla el mismo usuario que la abrió.
        /// </summary>
        public static decimal CerrarCaja(int idSesion, int idUsuario)
        {
            GestorVentas.AsegurarTablas();
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                UPDATE caja c
                SET c.fecha_cierre = @cierre,
                    c.estado       = 'Cerrada',
                    c.monto_cierre = c.monto_inicio
                        + (SELECT COALESCE(SUM(dv.total), 0)
                           FROM venta v
                           INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                           WHERE v.id_sesion = c.id_sesion AND v.cancelada = 0 AND v.medio_pago = 'Efectivo')
                        - (SELECT COALESCE(SUM(r.monto), 0)
                           FROM retiro_efectivo r
                           WHERE r.id_sesion = c.id_sesion)
                WHERE c.id_sesion = @id
                  AND c.id_usuario_apertura = @usuario
                  AND c.estado = 'Abierta'";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cierre",  DateTime.Now);
            cmd.Parameters.AddWithValue("@id",      idSesion);
            cmd.Parameters.AddWithValue("@usuario", idUsuario);

            if (cmd.ExecuteNonQuery() == 0)
                throw new InvalidOperationException(
                    "Solo el usuario que abrió la caja puede cerrarla.");

            using MySqlCommand leer = new MySqlCommand(
                "SELECT monto_cierre FROM caja WHERE id_sesion = @id", conn);
            leer.Parameters.AddWithValue("@id", idSesion);
            return Convert.ToDecimal(leer.ExecuteScalar());
        }

        /// <summary>Registra un retiro de efectivo en la sesión activa.</summary>
        public static void RegistrarRetiro(int idSesion, decimal monto,
                                            string motivo, int idUsuario)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                INSERT INTO retiro_efectivo (id_sesion, monto, motivo, fecha, id_usuario)
                VALUES (@sesion, @monto, @motivo, @fecha, @usuario)";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@sesion",  idSesion);
            cmd.Parameters.AddWithValue("@monto",   monto);
            cmd.Parameters.AddWithValue("@motivo",  motivo);
            cmd.Parameters.AddWithValue("@fecha",   DateTime.Now);
            cmd.Parameters.AddWithValue("@usuario", idUsuario);
            cmd.ExecuteNonQuery();
        }
    }
}
