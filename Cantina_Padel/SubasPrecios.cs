using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    /// <summary>
    /// Suba masiva de precios de venta por proveedor, categoría, marca, producto o unidad.
    /// Regla de negocio: el precio de venta SOLO se actualiza si el nuevo precio es
    /// superior al actual Y el producto tiene stock mayor a 0.
    /// Cada cambio queda registrado en historial_precio.
    /// </summary>
    public class SubasPrecios
    {
        // Columnas permitidas como filtro (evita inyección SQL en el nombre de columna)
        private static bool ColumnaValida(string columna)
        {
            switch (columna)
            {
                case "id_proveedor":
                case "id_categoria":
                case "id_marca":
                case "id_producto":
                case "id_unidad":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>Crea la tabla de historial si todavía no existe (mismo criterio que GestorVentas.AsegurarTablas).</summary>
        private static void AsegurarHistorial(MySqlConnection conn)
        {
            const string sql = @"
                CREATE TABLE IF NOT EXISTS historial_precio (
                    id_historial    INT AUTO_INCREMENT PRIMARY KEY,
                    id_producto     INT NOT NULL,
                    precio_anterior DECIMAL(10,2) NOT NULL,
                    precio_nuevo    DECIMAL(10,2) NOT NULL,
                    fecha           DATETIME NOT NULL,
                    id_usuario      INT NULL
                )";
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Indica si la BD soporta la suba por unidad (tabla 'unidad' y columna producto.id_unidad).
        /// </summary>
        public static bool SoportaUnidad()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();
                const string sql = @"
                    SELECT
                      (SELECT COUNT(*) FROM information_schema.columns
                        WHERE table_schema = DATABASE() AND table_name = 'producto' AND column_name = 'id_unidad')
                    + (SELECT COUNT(*) FROM information_schema.tables
                        WHERE table_schema = DATABASE() AND table_name = 'unidad')";
                using MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar()) >= 2;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sube el precio_venta un 'porcentaje' % a los productos donde columna = valor,
        /// SOLO si stock > 0 y el nuevo precio es superior al actual.
        /// Devuelve cuántos productos se actualizaron.
        /// </summary>
        public int SubirPrecio(string columna, int valor, decimal porcentaje, int idUsuario)
        {
            if (!ColumnaValida(columna))
                throw new ArgumentException("Columna no válida: " + columna);
            if (porcentaje <= 0)
                throw new ArgumentException("El porcentaje debe ser mayor a 0.");

            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();

            // El CREATE TABLE confirma implícitamente en MySQL, por eso va antes de la transacción.
            AsegurarHistorial(conn);

            using MySqlTransaction tx = conn.BeginTransaction();
            try
            {
                // 1) Productos que cumplen: filtro + stock > 0 + nuevo precio superior al actual
                string sqlSeleccion =
                    "SELECT id_producto, precio_venta, ROUND(precio_venta * (1 + @pct / 100), 2) AS nuevo_precio " +
                    "FROM producto " +
                    "WHERE " + columna + " = @valor " +
                    "AND stock > 0 " +
                    "AND ROUND(precio_venta * (1 + @pct / 100), 2) > precio_venta " +
                    "FOR UPDATE";

                var cambios = new List<(int Id, decimal Anterior, decimal Nuevo)>();
                using (var cmdSel = new MySqlCommand(sqlSeleccion, conn, tx))
                {
                    cmdSel.Parameters.AddWithValue("@pct", porcentaje);
                    cmdSel.Parameters.AddWithValue("@valor", valor);
                    using MySqlDataReader r = cmdSel.ExecuteReader();
                    while (r.Read())
                        cambios.Add((r.GetInt32("id_producto"), r.GetDecimal("precio_venta"), r.GetDecimal("nuevo_precio")));
                }

                // 2) Por cada producto: historial + update + propagación a cuenta corriente pendiente
                foreach (var c in cambios)
                {
                    using (var cmdH = new MySqlCommand(
                        "INSERT INTO historial_precio (id_producto, precio_anterior, precio_nuevo, fecha, id_usuario) " +
                        "VALUES (@id, @ant, @nue, NOW(), @usr)", conn, tx))
                    {
                        cmdH.Parameters.AddWithValue("@id", c.Id);
                        cmdH.Parameters.AddWithValue("@ant", c.Anterior);
                        cmdH.Parameters.AddWithValue("@nue", c.Nuevo);
                        cmdH.Parameters.AddWithValue("@usr", idUsuario);
                        cmdH.ExecuteNonQuery();
                    }

                    using (var cmdU = new MySqlCommand(
                        "UPDATE producto SET precio_venta = @nue WHERE id_producto = @id", conn, tx))
                    {
                        cmdU.Parameters.AddWithValue("@nue", c.Nuevo);
                        cmdU.Parameters.AddWithValue("@id", c.Id);
                        cmdU.ExecuteNonQuery();
                    }

                    // Igual que al editar un producto: se actualizan los cargos pendientes en cuenta corriente
                    GestorVentas.ActualizarPreciosCuentaCorrienteProducto(conn, tx, c.Id, c.Nuevo);
                }

                tx.Commit();
                return cambios.Count;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        // ---- Funciones por criterio ----
        public int SubirPorProveedor(int idProveedor, decimal pct, int idUsuario)
            => SubirPrecio("id_proveedor", idProveedor, pct, idUsuario);

        public int SubirPorCategoria(int idCategoria, decimal pct, int idUsuario)
            => SubirPrecio("id_categoria", idCategoria, pct, idUsuario);

        public int SubirPorMarca(int idMarca, decimal pct, int idUsuario)
            => SubirPrecio("id_marca", idMarca, pct, idUsuario);

        public int SubirPorProducto(int idProducto, decimal pct, int idUsuario)
            => SubirPrecio("id_producto", idProducto, pct, idUsuario);

        // Requiere que exista la columna id_unidad en producto
        public int SubirPorUnidad(int idUnidad, decimal pct, int idUsuario)
            => SubirPrecio("id_unidad", idUnidad, pct, idUsuario);
    }
}
