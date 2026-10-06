using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public class Conexion
    {
        private static readonly string connectionString =
            "Server=localhost;Database=cantina_padel;Uid=root;Pwd=154558080";

        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(connectionString);
        }
    }
}