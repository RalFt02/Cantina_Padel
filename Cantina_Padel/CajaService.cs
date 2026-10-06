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

        /// <summary>Devuelve la sesión abierta para la caja dada, o null si no hay.</summary>
        public static DataRow? ObtenerSesionAbierta(int idCaja)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                SELECT c.id_sesion, c.id_caja, c.fecha_apertura,
                       c.monto_inicio, u.nombre_completo AS cajero
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
                SELECT r.id_retiro      AS 'ID',
                       r.fecha_retiro   AS 'Fecha',
                       r.monto          AS 'Monto ($)',
                       r.motivo         AS 'Motivo',
                       u.nombre_completo AS 'Registró'
                FROM retiro_efectivo r
                INNER JOIN usuario u ON u.id_usuario = r.id_usuario
                WHERE r.id_sesion = @sesion
                ORDER BY r.fecha_retiro";
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

        // ═══════════════════════════════════════════════════
        //  OPERACIONES
        // ═══════════════════════════════════════════════════

        /// <summary>Abre una nueva sesión de caja. Devuelve el id_sesion generado.</summary>
        public static int AbrirCaja(int idCaja, decimal montoInicio, int idUsuario)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                INSERT INTO caja (id_caja, fecha_apertura, monto_inicio, estado, id_usuario_apertura)
                VALUES (@caja, @apertura, @inicio, 'Abierta', @usuario);
                SELECT LAST_INSERT_ID();";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@caja",     idCaja);
            cmd.Parameters.AddWithValue("@apertura", DateTime.Now);
            cmd.Parameters.AddWithValue("@inicio",   montoInicio);
            cmd.Parameters.AddWithValue("@usuario",  idUsuario);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /// <summary>Cierra una sesión de caja registrando el monto de cierre.</summary>
        public static void CerrarCaja(int idSesion, decimal montoCierre)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                UPDATE caja
                SET fecha_cierre  = @cierre,
                    monto_cierre  = @monto,
                    estado        = 'Cerrada'
                WHERE id_sesion = @id";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cierre", DateTime.Now);
            cmd.Parameters.AddWithValue("@monto",  montoCierre);
            cmd.Parameters.AddWithValue("@id",     idSesion);
            cmd.ExecuteNonQuery();
        }

        /// <summary>Registra un retiro de efectivo en la sesión activa.</summary>
        public static void RegistrarRetiro(int idSesion, decimal monto,
                                            string motivo, int idUsuario)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            const string sql = @"
                INSERT INTO retiro_efectivo (id_sesion, monto, motivo, fecha_retiro, id_usuario)
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
