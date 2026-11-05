using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public class LineaVenta
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
        public int? IdReserva { get; set; }
    }

    public class CargoPendiente
    {
        public int IdMovimiento { get; set; }
        public string Descripcion { get; set; } = "";
        public decimal Importe { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class VentaParaCancelar
    {
        public int IdVenta { get; set; }
        public bool Cancelada { get; set; }
        public string MedioPago { get; set; } = "";
        public int IdCliente { get; set; }
        public int? IdSesion { get; set; }
        public decimal Total { get; set; }
    }

    public class ResultadoPagoCC
    {
        public int IdVenta { get; set; }
        public bool AlcanzoProducto { get; set; }
        public decimal TotalSaldado { get; set; }
        public decimal SaldoFavor { get; set; }
        public decimal MenorCostoRequerido { get; set; }
        public string NombreProductoMenor { get; set; } = "";
        public List<string> ProductosPagados { get; set; } = new List<string>();
        public string Mensaje { get; set; } = "";
    }

    public static class GestorVentas
    {
        public const decimal PRECIO_MAXIMO = 99999999.99m;

        // ─────────────────────────────────────────────
        //  COMPATIBILIDAD CON BASE DE DATOS
        // ─────────────────────────────────────────────
        public static void AsegurarTablas()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                // 1. Columna saldo_favor en cuenta_corriente
                EjecutarSqlSeguro(conn, "ALTER TABLE cuenta_corriente ADD COLUMN saldo_favor DECIMAL(10,2) NOT NULL DEFAULT 0.00");

                // 2. Columnas en movimiento_cuenta_corriente
                EjecutarSqlSeguro(conn, "ALTER TABLE movimiento_cuenta_corriente ADD COLUMN descripcion VARCHAR(255) NULL");
                EjecutarSqlSeguro(conn, "ALTER TABLE movimiento_cuenta_corriente ADD COLUMN id_producto INT NULL");
                EjecutarSqlSeguro(conn, "ALTER TABLE movimiento_cuenta_corriente ADD COLUMN id_venta INT NULL");
                EjecutarSqlSeguro(conn, "ALTER TABLE movimiento_cuenta_corriente ADD COLUMN medio_pago VARCHAR(50) NULL");
                EjecutarSqlSeguro(conn, "ALTER TABLE persona ADD COLUMN condicion_fiscal VARCHAR(50) DEFAULT 'Consumidor Final'");

                // 2b. Columnas para cancelación de ventas (baja lógica, la venta no se borra)
                EjecutarSqlSeguro(conn, "ALTER TABLE venta ADD COLUMN cancelada TINYINT(1) NOT NULL DEFAULT 0");
                EjecutarSqlSeguro(conn, "ALTER TABLE venta ADD COLUMN fecha_cancelacion DATETIME NULL");
                EjecutarSqlSeguro(conn, "ALTER TABLE venta ADD COLUMN motivo_cancelacion VARCHAR(255) NULL");
                EjecutarSqlSeguro(conn, "ALTER TABLE venta ADD COLUMN id_usuario_cancelacion INT NULL");

                // 3. Crear cuenta corriente para clientes que no la tengan
                const string sqlCrearCC = @"
                    INSERT INTO cuenta_corriente (saldo, saldo_favor, id_cliente)
                    SELECT 0.00, 0.00, c.id_cliente
                    FROM cliente c
                    WHERE c.id_cliente NOT IN (SELECT id_cliente FROM cuenta_corriente);";
                EjecutarSqlSeguro(conn, sqlCrearCC);

                // 4. Productos de servicio para Canchas y Pagos si detalle_venta tiene FK
                const string sqlProdServicio = @"
                    INSERT IGNORE INTO producto (id_producto, nombre, descripcion, precio_venta, stock, activo, id_marca, id_categoria, id_proveedor, codigo)
                    VALUES (999, 'Alquiler Cancha', 'Servicio de alquiler de cancha', 0.00, 9999, 1, 1, 1, 1, 'CANCHA-01');
                    INSERT IGNORE INTO producto (id_producto, nombre, descripcion, precio_venta, stock, activo, id_marca, id_categoria, id_proveedor, codigo)
                    VALUES (998, 'Cobro Cuenta Corriente', 'Abono a cuenta corriente', 0.00, 9999, 1, 1, 1, 1, 'CTACTE-01');";
                EjecutarSqlSeguro(conn, sqlProdServicio);

                // 5. Tabla para control de reservas pagadas
                const string sqlResPagada = @"
                    CREATE TABLE IF NOT EXISTS reserva_pagada (
                        id_reserva INT PRIMARY KEY,
                        id_venta INT NOT NULL,
                        fecha_pago DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        id_usuario INT NOT NULL,
                        INDEX idx_rp_venta (id_venta)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";
                EjecutarSqlSeguro(conn, sqlResPagada);

                // 6. Asegurar Consumidor Final en persona y cliente para integridad referencial
                const string sqlConsumidorFinal = @"
                    INSERT IGNORE INTO persona (id_persona, nombre, apellido, dni, activo)
                    VALUES (9999, 'Consumidor', 'Final', '0', 1);
                    INSERT IGNORE INTO cliente (id_cliente, fecha_alta, id_persona)
                    VALUES (9999, CURDATE(), 9999);";
                EjecutarSqlSeguro(conn, sqlConsumidorFinal);
            }
            catch { }
        }

        private static void EjecutarSqlSeguro(MySqlConnection conn, string sql)
        {
            try
            {
                using MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        public static string GenerarNumeroComprobante(string tipoComprobante, int idVenta)
        {
            string prefijo = "TK";
            if (tipoComprobante.Contains("A")) prefijo = "FA";
            else if (tipoComprobante.Contains("B")) prefijo = "FB";
            else if (tipoComprobante.Contains("C")) prefijo = "FC";
            else if (tipoComprobante.StartsWith("Recibo", StringComparison.OrdinalIgnoreCase)) prefijo = "RC";

            return $"{prefijo}-0001-{idVenta:D8}";
        }

        public static string MapearEnumComprobante(string tipoComprobante)
        {
            if (tipoComprobante.Contains("A")) return "FacturaA";
            if (tipoComprobante.Contains("B")) return "FacturaB";
            if (tipoComprobante.Contains("C")) return "FacturaC";
            return "Ticket";
        }

        // ─────────────────────────────────────────────
        //  VALIDACIONES
        // ─────────────────────────────────────────────

        public static string? ValidarLinea(int idProducto, int cantidad, decimal precioUnitario, int stockDisponible)
        {
            if (idProducto <= 0)
                return "Seleccioná un producto.";
            if (cantidad <= 0)
                return "La cantidad debe ser mayor a 0.";
            // La restricción de stock fue eliminada para permitir ventas con stock negativo
            if (precioUnitario <= 0)
                return "El precio del producto debe ser mayor a 0.";
            if (precioUnitario > PRECIO_MAXIMO || cantidad * precioUnitario > PRECIO_MAXIMO)
                return "El importe es demasiado grande.";
            return null;
        }

        public static string? ValidarVenta(List<LineaVenta>? lineas, string? medioPago, int? idCliente)
        {
            if (lineas == null || lineas.Count == 0)
                return "Agregá al menos un producto a la venta.";
            if (string.IsNullOrWhiteSpace(medioPago))
                return "Seleccioná un medio de pago.";
            if (!idCliente.HasValue || idCliente.Value <= 0)
                return "Debe seleccionar un cliente registrado. El sistema no permite ventas anónimas.";

            return null;
        }

        public static decimal CalcularTotal(IEnumerable<LineaVenta> lineas)
        {
            return lineas.Sum(l => l.Subtotal);
        }

        public static string? AgregarLinea(List<LineaVenta> lineas, LineaVenta nueva)
        {
            string? error = ValidarLinea(nueva.IdProducto, nueva.Cantidad, nueva.PrecioUnitario, nueva.StockDisponible);
            if (error != null) return error;

            if (lineas.Any(l => l.IdProducto == nueva.IdProducto))
                return "Ese producto ya está en la lista. Quitalo y volvé a cargarlo si querés modificar la cantidad.";

            lineas.Add(nueva);
            return null;
        }

        public static bool QuitarLinea(List<LineaVenta> lineas, int idProducto)
        {
            return lineas.RemoveAll(l => l.IdProducto == idProducto) > 0;
        }

        // ─────────────────────────────────────────────
        //  REGISTRO DE VENTA
        // ─────────────────────────────────────────────

        public static int RegistrarVenta(int idUsuario, int idCliente, int idSesionCaja,
                                         string medioPago, string tipoComprobante, List<LineaVenta> lineas)
        {
            AsegurarTablas();

            string? error = ValidarVenta(lineas, medioPago, idCliente);
            if (error != null) throw new ArgumentException(error);

            foreach (LineaVenta linea in lineas)
            {
                error = ValidarLinea(linea.IdProducto, linea.Cantidad, linea.PrecioUnitario, linea.StockDisponible);
                if (error != null) throw new ArgumentException(error);
            }

            decimal totalVenta = CalcularTotal(lineas);
            int cantidadTotalItems = lineas.Sum(l => l.Cantidad);
            string enumTipo = MapearEnumComprobante(tipoComprobante);

            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            using MySqlTransaction tx = conn.BeginTransaction();

            try
            {
                // 1. Insertar venta
                const string queryVenta = @"
                    INSERT INTO venta (fecha, tipo_comprobante, cantidad, medio_pago, id_cliente, id_sesion, id_usuario, total)
                    VALUES (NOW(), @tipo, @cant, @medio, @cliente, @sesion, @usu, @total);
                    SELECT LAST_INSERT_ID();";

                int idVenta;
                using (MySqlCommand cmd = new MySqlCommand(queryVenta, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@tipo", enumTipo);
                    cmd.Parameters.AddWithValue("@cant", cantidadTotalItems);
                    cmd.Parameters.AddWithValue("@medio", medioPago);
                    cmd.Parameters.AddWithValue("@cliente", idCliente);
                    cmd.Parameters.AddWithValue("@sesion", idSesionCaja);
                    cmd.Parameters.AddWithValue("@usu", idUsuario);
                    cmd.Parameters.AddWithValue("@total", totalVenta);
                    idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string numComp = GenerarNumeroComprobante(tipoComprobante, idVenta);
                using (MySqlCommand cmdNum = new MySqlCommand("UPDATE venta SET numero_comprobante = @num WHERE id_venta = @id", conn, tx))
                {
                    cmdNum.Parameters.AddWithValue("@num", numComp);
                    cmdNum.Parameters.AddWithValue("@id", idVenta);
                    cmdNum.ExecuteNonQuery();
                }

                // 2. Insertar detalles y descontar stock
                foreach (LineaVenta linea in lineas)
                {
                    const string queryDet = @"
                        INSERT INTO detalle_venta (id_venta, id_producto, cantidad, total, precio_unitario, descripcion)
                        VALUES (@venta, @prod, @cant, @total, @precio, @desc)";
                    using (MySqlCommand cmdDet = new MySqlCommand(queryDet, conn, tx))
                    {
                        cmdDet.Parameters.AddWithValue("@venta", idVenta);
                        cmdDet.Parameters.AddWithValue("@prod", linea.IdProducto);
                        cmdDet.Parameters.AddWithValue("@cant", linea.Cantidad);
                        cmdDet.Parameters.AddWithValue("@total", linea.Subtotal);
                        cmdDet.Parameters.AddWithValue("@precio", linea.PrecioUnitario);
                        cmdDet.Parameters.AddWithValue("@desc", linea.Nombre);
                        cmdDet.ExecuteNonQuery();
                    }

                    // Descontar stock (permite stock negativo según requerimiento)
                    if (linea.IdProducto != 998 && linea.IdProducto != 999)
                    {
                        const string queryStock = @"
                            UPDATE producto
                            SET stock = stock - @cant
                            WHERE id_producto = @prod";
                        using (MySqlCommand cmdStock = new MySqlCommand(queryStock, conn, tx))
                        {
                            cmdStock.Parameters.AddWithValue("@cant", linea.Cantidad);
                            cmdStock.Parameters.AddWithValue("@prod", linea.IdProducto);
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    // Si la línea corresponde a una reserva de cancha, marcarla como pagada
                    if (linea.IdReserva.HasValue)
                    {
                        const string sqlInsRP = "INSERT IGNORE INTO reserva_pagada (id_reserva, id_venta, id_usuario) VALUES (@id, @idVenta, @usu)";
                        using (MySqlCommand cmdRP = new MySqlCommand(sqlInsRP, conn, tx))
                        {
                            cmdRP.Parameters.AddWithValue("@id", linea.IdReserva.Value);
                            cmdRP.Parameters.AddWithValue("@idVenta", idVenta);
                            cmdRP.Parameters.AddWithValue("@usu", idUsuario);
                            cmdRP.ExecuteNonQuery();
                        }

                        try
                        {
                            using MySqlCommand cmdUpdRes = new MySqlCommand("UPDATE reserva SET estado = 'Pagada' WHERE id_reserva = @id", conn, tx);
                            cmdUpdRes.Parameters.AddWithValue("@id", linea.IdReserva.Value);
                            cmdUpdRes.ExecuteNonQuery();
                        }
                        catch
                        {
                            using MySqlCommand cmdUpdRes2 = new MySqlCommand("UPDATE reserva SET estado = 'Confirmada' WHERE id_reserva = @id", conn, tx);
                            cmdUpdRes2.Parameters.AddWithValue("@id", linea.IdReserva.Value);
                            cmdUpdRes2.ExecuteNonQuery();
                        }
                    }
                }

                // 3. Si fue Cuenta Corriente, asentar deuda e ítems pendientes
                if (medioPago.Equals("Cuenta Corriente", StringComparison.OrdinalIgnoreCase))
                {
                    int idCuenta = ObtenerOCrearIdCuentaCorriente(conn, tx, idCliente);

                    // Aumentar saldo deudor
                    const string sqlUpdSaldo = "UPDATE cuenta_corriente SET saldo = saldo + @total WHERE id_cuenta = @idCuenta";
                    using (MySqlCommand cmdUpdCC = new MySqlCommand(sqlUpdSaldo, conn, tx))
                    {
                        cmdUpdCC.Parameters.AddWithValue("@total", totalVenta);
                        cmdUpdCC.Parameters.AddWithValue("@idCuenta", idCuenta);
                        cmdUpdCC.ExecuteNonQuery();
                    }

                    // Insertar cada ítem como Cargo 'Pendiente' para poder pagarlo de menor a mayor costo
                    foreach (LineaVenta linea in lineas)
                    {
                        const string sqlMov = @"
                            INSERT INTO movimiento_cuenta_corriente (fecha, tipo_movimiento, importe, estado, id_cuenta, descripcion, id_producto, id_venta, medio_pago)
                            VALUES (CURDATE(), 'Cargo', @imp, 'Pendiente', @idCuenta, @desc, @prod, @idVenta, 'Cuenta Corriente')";
                        using (MySqlCommand cmdMov = new MySqlCommand(sqlMov, conn, tx))
                        {
                            cmdMov.Parameters.AddWithValue("@imp", linea.Subtotal);
                            cmdMov.Parameters.AddWithValue("@idCuenta", idCuenta);
                            cmdMov.Parameters.AddWithValue("@desc", linea.Nombre);
                            cmdMov.Parameters.AddWithValue("@prod", linea.IdProducto);
                            cmdMov.Parameters.AddWithValue("@idVenta", idVenta);
                            cmdMov.ExecuteNonQuery();
                        }
                    }
                }

                tx.Commit();
                return idVenta;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        private static int ObtenerOCrearIdCuentaCorriente(MySqlConnection conn, MySqlTransaction tx, int idCliente)
        {
            const string sqlSel = "SELECT id_cuenta FROM cuenta_corriente WHERE id_cliente = @cli LIMIT 1";
            using (MySqlCommand cmd = new MySqlCommand(sqlSel, conn, tx))
            {
                cmd.Parameters.AddWithValue("@cli", idCliente);
                object? res = cmd.ExecuteScalar();
                if (res != null && res != DBNull.Value)
                    return Convert.ToInt32(res);
            }

            const string sqlIns = "INSERT INTO cuenta_corriente (saldo, saldo_favor, id_cliente) VALUES (0.00, 0.00, @cli); SELECT LAST_INSERT_ID();";
            using (MySqlCommand cmdIns = new MySqlCommand(sqlIns, conn, tx))
            {
                cmdIns.Parameters.AddWithValue("@cli", idCliente);
                return Convert.ToInt32(cmdIns.ExecuteScalar());
            }
        }

        // ─────────────────────────────────────────────
        //  CUENTA CORRIENTE Y SALDO A FAVOR
        // ─────────────────────────────────────────────

        public static (decimal saldoDeudor, decimal saldoFavor, int idCuenta) ObtenerSaldosCuentaCorriente(int idCliente)
        {
            AsegurarTablas();

            const string sql = "SELECT id_cuenta, saldo, saldo_favor FROM cuenta_corriente WHERE id_cliente = @cli LIMIT 1";
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            using MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cli", idCliente);
            using MySqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {
                int idC = r.GetInt32("id_cuenta");
                decimal s = r.GetDecimal("saldo");
                decimal sf = r.GetDecimal("saldo_favor");
                return (s, sf, idC);
            }

            return (0m, 0m, 0);
        }

        public static DataTable ObtenerCargosPendientes(int idCliente)
        {
            AsegurarTablas();

            // Ordenados DE MENOR COSTO A MAYOR COSTO
            const string sql = @"
                SELECT m.id_movimiento_ccc,
                       m.fecha,
                       COALESCE(m.descripcion, 'Producto en Cta. Cte.') AS descripcion,
                       m.importe,
                       m.estado
                FROM movimiento_cuenta_corriente m
                JOIN cuenta_corriente cc ON cc.id_cuenta = m.id_cuenta
                WHERE cc.id_cliente = @cli
                  AND m.tipo_movimiento = 'Cargo'
                  AND m.estado = 'Pendiente'
                ORDER BY m.importe ASC, m.fecha ASC";

            return LlenarTabla(sql, ("@cli", idCliente));
        }

        public static DataTable ObtenerHistorialMovimientosCC(int idCliente)
        {
            AsegurarTablas();

            const string sql = @"
                SELECT m.fecha,
                       COALESCE(m.descripcion, m.tipo_movimiento) AS concepto,
                       CASE WHEN m.tipo_movimiento = 'Cargo' THEN m.importe ELSE NULL END AS cargo,
                       CASE WHEN m.tipo_movimiento = 'Abono' THEN m.importe ELSE NULL END AS abono,
                       COALESCE(m.medio_pago, '-') AS medio_pago,
                       m.estado
                FROM movimiento_cuenta_corriente m
                JOIN cuenta_corriente cc ON cc.id_cuenta = m.id_cuenta
                WHERE cc.id_cliente = @cli
                ORDER BY m.fecha DESC, m.id_movimiento_ccc DESC";

            return LlenarTabla(sql, ("@cli", idCliente));
        }

        public static ResultadoPagoCC ProcesarPagoCuentaCorriente(int idCliente, decimal montoAbonado,
                                                                 string medioPago, string tipoComprobante,
                                                                 int idUsuario, int idSesionCaja)
        {
            AsegurarTablas();

            if (montoAbonado <= 0)
                throw new ArgumentException("El monto a abonar debe ser mayor a 0.");

            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            using MySqlTransaction tx = conn.BeginTransaction();

            try
            {
                int idCuenta = ObtenerOCrearIdCuentaCorriente(conn, tx, idCliente);

                // Leer saldo deudor y saldo a favor actual
                decimal saldoDeudor = 0m;
                decimal saldoFavorActual = 0m;

                using (MySqlCommand cmdSaldos = new MySqlCommand("SELECT saldo, saldo_favor FROM cuenta_corriente WHERE id_cuenta = @id FOR UPDATE", conn, tx))
                {
                    cmdSaldos.Parameters.AddWithValue("@id", idCuenta);
                    using MySqlDataReader r = cmdSaldos.ExecuteReader();
                    if (r.Read())
                    {
                        saldoDeudor = r.GetDecimal("saldo");
                        saldoFavorActual = r.GetDecimal("saldo_favor");
                    }
                }

                // El cliente puede pagar lo que quiera; se suma al saldo a favor acumulado
                decimal disponible = montoAbonado + saldoFavorActual;

                // Cargar cargos pendientes ordenados de menor a mayor costo
                const string sqlCargos = @"
                    SELECT id_movimiento_ccc, descripcion, importe, fecha
                    FROM movimiento_cuenta_corriente
                    WHERE id_cuenta = @id AND tipo_movimiento = 'Cargo' AND estado = 'Pendiente'
                    ORDER BY importe ASC, fecha ASC
                    FOR UPDATE";

                List<CargoPendiente> pendientes = new List<CargoPendiente>();
                using (MySqlCommand cmdC = new MySqlCommand(sqlCargos, conn, tx))
                {
                    cmdC.Parameters.AddWithValue("@id", idCuenta);
                    using MySqlDataReader rc = cmdC.ExecuteReader();
                    while (rc.Read())
                    {
                        pendientes.Add(new CargoPendiente
                        {
                            IdMovimiento = rc.GetInt32("id_movimiento_ccc"),
                            Descripcion = rc.GetString("descripcion"),
                            Importe = rc.GetDecimal("importe"),
                            Fecha = rc.GetDateTime("fecha")
                        });
                    }
                }

                // Registrar la venta de cobro de caja vinculada a la sesión activa
                string enumTipo = MapearEnumComprobante(tipoComprobante);
                const string queryVenta = @"
                    INSERT INTO venta (fecha, tipo_comprobante, cantidad, medio_pago, id_cliente, id_sesion, id_usuario, total)
                    VALUES (NOW(), @tipo, 1, @medio, @cli, @ses, @usu, @total);
                    SELECT LAST_INSERT_ID();";

                int idVenta;
                using (MySqlCommand cmdV = new MySqlCommand(queryVenta, conn, tx))
                {
                    cmdV.Parameters.AddWithValue("@tipo", enumTipo);
                    cmdV.Parameters.AddWithValue("@medio", medioPago);
                    cmdV.Parameters.AddWithValue("@cli", idCliente);
                    cmdV.Parameters.AddWithValue("@ses", idSesionCaja);
                    cmdV.Parameters.AddWithValue("@usu", idUsuario);
                    cmdV.Parameters.AddWithValue("@total", montoAbonado);
                    idVenta = Convert.ToInt32(cmdV.ExecuteScalar());
                }

                string numComp = GenerarNumeroComprobante(tipoComprobante, idVenta);
                using (MySqlCommand cmdNum = new MySqlCommand("UPDATE venta SET numero_comprobante = @num WHERE id_venta = @id", conn, tx))
                {
                    cmdNum.Parameters.AddWithValue("@num", numComp);
                    cmdNum.Parameters.AddWithValue("@id", idVenta);
                    cmdNum.ExecuteNonQuery();
                }

                // Detalle de venta
                const string queryDet = @"
                    INSERT INTO detalle_venta (id_venta, id_producto, cantidad, total, precio_unitario, descripcion)
                    VALUES (@venta, 998, 1, @total, @total, @desc)";
                using (MySqlCommand cmdDet = new MySqlCommand(queryDet, conn, tx))
                {
                    cmdDet.Parameters.AddWithValue("@venta", idVenta);
                    cmdDet.Parameters.AddWithValue("@total", montoAbonado);
                    cmdDet.Parameters.AddWithValue("@desc", $"Abono a Cuenta Corriente - {tipoComprobante} N° {numComp}");
                    cmdDet.ExecuteNonQuery();
                }

                ResultadoPagoCC res = new ResultadoPagoCC { IdVenta = idVenta };

                if (pendientes.Count > 0)
                {
                    decimal menorCosto = pendientes[0].Importe;

                    if (disponible < menorCosto)
                    {
                        // NO alcanza para el producto de menor costo: todo queda como saldo a favor en el textbox
                        decimal nuevoSaldoFavor = disponible;

                        using (MySqlCommand cmdUpd = new MySqlCommand("UPDATE cuenta_corriente SET saldo_favor = @sf WHERE id_cuenta = @id", conn, tx))
                        {
                            cmdUpd.Parameters.AddWithValue("@sf", nuevoSaldoFavor);
                            cmdUpd.Parameters.AddWithValue("@id", idCuenta);
                            cmdUpd.ExecuteNonQuery();
                        }

                        // Registrar movimiento de Abono
                        const string sqlAbono = @"
                            INSERT INTO movimiento_cuenta_corriente (fecha, tipo_movimiento, importe, estado, id_cuenta, descripcion, id_venta, medio_pago)
                            VALUES (CURDATE(), 'Abono', @imp, 'Pagado', @id, @desc, @idV, @med)";
                        using (MySqlCommand cmdAb = new MySqlCommand(sqlAbono, conn, tx))
                        {
                            cmdAb.Parameters.AddWithValue("@imp", montoAbonado);
                            cmdAb.Parameters.AddWithValue("@id", idCuenta);
                            cmdAb.Parameters.AddWithValue("@desc", $"Ingreso a Saldo a Favor (No alcanza para {pendientes[0].Descripcion})");
                            cmdAb.Parameters.AddWithValue("@idV", idVenta);
                            cmdAb.Parameters.AddWithValue("@med", medioPago);
                            cmdAb.ExecuteNonQuery();
                        }

                        tx.Commit();

                        res.AlcanzoProducto = false;
                        res.SaldoFavor = nuevoSaldoFavor;
                        res.MenorCostoRequerido = menorCosto;
                        res.NombreProductoMenor = pendientes[0].Descripcion;
                        res.Mensaje = $"El monto disponible (${disponible:N2}) no alcanza para cubrir el producto de menor costo: '{pendientes[0].Descripcion}' (${menorCosto:N2}).\n\nQuedó asentado en 'Saldo a Favor' un monto de ${nuevoSaldoFavor:N2} para completar el pago del producto en el próximo abono.";
                        return res;
                    }
                    else
                    {
                        // ALCANZA para cancelar productos de menor a mayor costo
                        decimal totalSaldado = 0m;
                        List<string> pagados = new List<string>();

                        foreach (var cargo in pendientes)
                        {
                            if (disponible >= cargo.Importe)
                            {
                                disponible -= cargo.Importe;
                                totalSaldado += cargo.Importe;
                                pagados.Add($"{cargo.Descripcion} (${cargo.Importe:N2})");

                                using (MySqlCommand cmdMarcar = new MySqlCommand("UPDATE movimiento_cuenta_corriente SET estado = 'Pagado' WHERE id_movimiento_ccc = @id", conn, tx))
                                {
                                    cmdMarcar.Parameters.AddWithValue("@id", cargo.IdMovimiento);
                                    cmdMarcar.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                break; // No alcanza para el siguiente producto de mayor costo
                            }
                        }

                        decimal nuevoSaldoFavor = disponible; // Remanente que no alcanzó para el próximo producto
                        decimal nuevoSaldoDeudor = Math.Max(0m, saldoDeudor - totalSaldado);

                        using (MySqlCommand cmdUpd = new MySqlCommand("UPDATE cuenta_corriente SET saldo = @sd, saldo_favor = @sf WHERE id_cuenta = @id", conn, tx))
                        {
                            cmdUpd.Parameters.AddWithValue("@sd", nuevoSaldoDeudor);
                            cmdUpd.Parameters.AddWithValue("@sf", nuevoSaldoFavor);
                            cmdUpd.Parameters.AddWithValue("@id", idCuenta);
                            cmdUpd.ExecuteNonQuery();
                        }

                        // Movimiento de Abono
                        const string sqlAbono = @"
                            INSERT INTO movimiento_cuenta_corriente (fecha, tipo_movimiento, importe, estado, id_cuenta, descripcion, id_venta, medio_pago)
                            VALUES (CURDATE(), 'Abono', @imp, 'Pagado', @id, @desc, @idV, @med)";
                        using (MySqlCommand cmdAb = new MySqlCommand(sqlAbono, conn, tx))
                        {
                            cmdAb.Parameters.AddWithValue("@imp", montoAbonado);
                            cmdAb.Parameters.AddWithValue("@id", idCuenta);
                            cmdAb.Parameters.AddWithValue("@desc", $"Abono aplicado: {string.Join(", ", pagados)}");
                            cmdAb.Parameters.AddWithValue("@idV", idVenta);
                            cmdAb.Parameters.AddWithValue("@med", medioPago);
                            cmdAb.ExecuteNonQuery();
                        }

                        tx.Commit();

                        res.AlcanzoProducto = true;
                        res.TotalSaldado = totalSaldado;
                        res.SaldoFavor = nuevoSaldoFavor;
                        res.ProductosPagados = pagados;
                        res.Mensaje = $"¡Cobro procesado con éxito!\n\nSe cancelaron {pagados.Count} producto(s) (de menor a mayor costo):\n- {string.Join("\n- ", pagados)}\n\nTotal deuda saldada: ${totalSaldado:N2}\nSaldo a favor restante en cuenta: ${nuevoSaldoFavor:N2}";
                        return res;
                    }
                }
                else
                {
                    // No hay cargos específicos desglosados (saldo general)
                    decimal saldoAplicado = Math.Min(montoAbonado, saldoDeudor);
                    decimal nuevoSaldoDeudor = Math.Max(0m, saldoDeudor - montoAbonado);
                    decimal nuevoSaldoFavor = saldoFavorActual + Math.Max(0m, montoAbonado - saldoDeudor);

                    using (MySqlCommand cmdUpd = new MySqlCommand("UPDATE cuenta_corriente SET saldo = @sd, saldo_favor = @sf WHERE id_cuenta = @id", conn, tx))
                    {
                        cmdUpd.Parameters.AddWithValue("@sd", nuevoSaldoDeudor);
                        cmdUpd.Parameters.AddWithValue("@sf", nuevoSaldoFavor);
                        cmdUpd.Parameters.AddWithValue("@id", idCuenta);
                        cmdUpd.ExecuteNonQuery();
                    }

                    const string sqlAbono = @"
                        INSERT INTO movimiento_cuenta_corriente (fecha, tipo_movimiento, importe, estado, id_cuenta, descripcion, id_venta, medio_pago)
                        VALUES (CURDATE(), 'Abono', @imp, 'Pagado', @id, @desc, @idV, @med)";
                    using (MySqlCommand cmdAb = new MySqlCommand(sqlAbono, conn, tx))
                    {
                        cmdAb.Parameters.AddWithValue("@imp", montoAbonado);
                        cmdAb.Parameters.AddWithValue("@id", idCuenta);
                        cmdAb.Parameters.AddWithValue("@desc", "Abono general a cuenta corriente");
                        cmdAb.Parameters.AddWithValue("@idV", idVenta);
                        cmdAb.Parameters.AddWithValue("@med", medioPago);
                        cmdAb.ExecuteNonQuery();
                    }

                    tx.Commit();

                    res.AlcanzoProducto = true;
                    res.TotalSaldado = saldoAplicado;
                    res.SaldoFavor = nuevoSaldoFavor;
                    res.Mensaje = $"¡Abono registrado! Se saldaron ${saldoAplicado:N2} de deuda.\nSaldo a favor: ${nuevoSaldoFavor:N2}.";
                    return res;
                }
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        // ─────────────────────────────────────────────
        //  COBRO DE RESERVAS DE CANCHAS
        // ─────────────────────────────────────────────

        public static DataTable ObtenerReservasPendientesPago(int? idCliente = null)
        {
            AsegurarTablas();

            string sql = @"
                SELECT r.id_reserva,
                       r.id_cliente,
                       c.nombre AS cancha,
                       r.fecha,
                       CONCAT(TIME_FORMAT(h.hora_inicio, '%H:%i'), ' - ', TIME_FORMAT(h.hora_fin, '%H:%i')) AS horario,
                       CONCAT(p.apellido, ', ', p.nombre) AS cliente,
                       c.precio,
                       r.estado
                FROM reserva r
                INNER JOIN horario h ON h.id_horario = r.id_horario
                INNER JOIN cliente cl ON cl.id_cliente = r.id_cliente
                INNER JOIN persona p ON p.id_persona = cl.id_persona
                INNER JOIN cancha c ON c.id_cancha = r.id_cancha
                WHERE r.estado NOT IN ('Cancelada', 'Pagada')
                  AND r.id_reserva NOT IN (SELECT id_reserva FROM reserva_pagada)
                  AND r.fecha = CURDATE()";

            if (idCliente.HasValue && idCliente.Value > 0)
            {
                sql += " AND r.id_cliente = @cli";
                sql += " ORDER BY r.fecha DESC, h.hora_inicio";
                return LlenarTabla(sql, ("@cli", idCliente.Value));
            }

            sql += " ORDER BY r.fecha DESC, h.hora_inicio";
            return LlenarTabla(sql);
        }

        public static int RegistrarCobroCanchas(List<int> idsReserva, int idUsuario, int idCliente,
                                               int idSesionCaja, string medioPago, string tipoComprobante, decimal total)
        {
            AsegurarTablas();

            if (idsReserva == null || idsReserva.Count == 0)
                throw new ArgumentException("Seleccioná al menos una reserva para cobrar.");

            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            using MySqlTransaction tx = conn.BeginTransaction();

            try
            {
                string enumTipo = MapearEnumComprobante(tipoComprobante);

                // 1. Insertar venta
                const string queryVenta = @"
                    INSERT INTO venta (fecha, tipo_comprobante, cantidad, medio_pago, id_cliente, id_sesion, id_usuario, total)
                    VALUES (NOW(), @tipo, @cant, @medio, @cli, @ses, @usu, @total);
                    SELECT LAST_INSERT_ID();";

                int idVenta;
                using (MySqlCommand cmd = new MySqlCommand(queryVenta, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@tipo", enumTipo);
                    cmd.Parameters.AddWithValue("@cant", idsReserva.Count);
                    cmd.Parameters.AddWithValue("@medio", medioPago);
                    cmd.Parameters.AddWithValue("@cli", idCliente);
                    cmd.Parameters.AddWithValue("@ses", idSesionCaja);
                    cmd.Parameters.AddWithValue("@usu", idUsuario);
                    cmd.Parameters.AddWithValue("@total", total);
                    idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string numComp = GenerarNumeroComprobante(tipoComprobante, idVenta);
                using (MySqlCommand cmdNum = new MySqlCommand("UPDATE venta SET numero_comprobante = @num WHERE id_venta = @id", conn, tx))
                {
                    cmdNum.Parameters.AddWithValue("@num", numComp);
                    cmdNum.Parameters.AddWithValue("@id", idVenta);
                    cmdNum.ExecuteNonQuery();
                }

                // 2. Traer info de cada reserva y asentar detalle
                foreach (int idRes in idsReserva)
                {
                    const string sqlInfoRes = @"
                        SELECT c.nombre AS cancha, r.fecha, h.hora_inicio, h.hora_fin, c.precio
                        FROM reserva r
                        JOIN cancha c ON c.id_cancha = r.id_cancha
                        JOIN horario h ON h.id_horario = r.id_horario
                        WHERE r.id_reserva = @id";

                    string desc = $"Alquiler Cancha (Reserva #{idRes})";
                    decimal precio = 0m;

                    using (MySqlCommand cmdInfo = new MySqlCommand(sqlInfoRes, conn, tx))
                    {
                        cmdInfo.Parameters.AddWithValue("@id", idRes);
                        using (MySqlDataReader r = cmdInfo.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                string cNom = r.GetString("cancha");
                                DateTime f = r.GetDateTime("fecha");
                                TimeSpan hIni = (TimeSpan)r["hora_inicio"];
                                TimeSpan hFin = (TimeSpan)r["hora_fin"];
                                precio = Convert.ToDecimal(r["precio"]);
                                desc = $"Cancha {cNom} - {f:dd/MM} ({hIni:hh\\:mm} a {hFin:hh\\:mm})";
                            }
                        }
                    }

                    // Detalle
                    const string queryDet = @"
                        INSERT INTO detalle_venta (id_venta, id_producto, cantidad, total, precio_unitario, descripcion)
                        VALUES (@venta, 999, 1, @precio, @precio, @desc)";
                    using (MySqlCommand cmdDet = new MySqlCommand(queryDet, conn, tx))
                    {
                        cmdDet.Parameters.AddWithValue("@venta", idVenta);
                        cmdDet.Parameters.AddWithValue("@precio", precio);
                        cmdDet.Parameters.AddWithValue("@desc", desc);
                        cmdDet.ExecuteNonQuery();
                    }

                    // Marcar en reserva_pagada para que desaparezca inmediatamente del grid
                    const string sqlInsRP = "INSERT IGNORE INTO reserva_pagada (id_reserva, id_venta, id_usuario) VALUES (@id, @idVenta, @usu)";
                    using (MySqlCommand cmdRP = new MySqlCommand(sqlInsRP, conn, tx))
                    {
                        cmdRP.Parameters.AddWithValue("@id", idRes);
                        cmdRP.Parameters.AddWithValue("@idVenta", idVenta);
                        cmdRP.Parameters.AddWithValue("@usu", idUsuario);
                        cmdRP.ExecuteNonQuery();
                    }

                    // Intentar actualizar enum si admite 'Pagada' o dejar en 'Confirmada'
                    try
                    {
                        using MySqlCommand cmdUpd = new MySqlCommand("UPDATE reserva SET estado = 'Pagada' WHERE id_reserva = @id", conn, tx);
                        cmdUpd.Parameters.AddWithValue("@id", idRes);
                        cmdUpd.ExecuteNonQuery();
                    }
                    catch
                    {
                        using MySqlCommand cmdUpd2 = new MySqlCommand("UPDATE reserva SET estado = 'Confirmada' WHERE id_reserva = @id", conn, tx);
                        cmdUpd2.Parameters.AddWithValue("@id", idRes);
                        cmdUpd2.ExecuteNonQuery();
                    }
                }

                // Si fue en Cuenta Corriente, asentar en CC
                if (medioPago.Equals("Cuenta Corriente", StringComparison.OrdinalIgnoreCase))
                {
                    int idCuenta = ObtenerOCrearIdCuentaCorriente(conn, tx, idCliente);

                    using (MySqlCommand cmdUpdCC = new MySqlCommand("UPDATE cuenta_corriente SET saldo = saldo + @tot WHERE id_cuenta = @id", conn, tx))
                    {
                        cmdUpdCC.Parameters.AddWithValue("@tot", total);
                        cmdUpdCC.Parameters.AddWithValue("@id", idCuenta);
                        cmdUpdCC.ExecuteNonQuery();
                    }

                    const string sqlMov = @"
                        INSERT INTO movimiento_cuenta_corriente (fecha, tipo_movimiento, importe, estado, id_cuenta, descripcion, id_producto, id_venta, medio_pago)
                        VALUES (CURDATE(), 'Cargo', @imp, 'Pendiente', @idCuenta, @desc, 999, @idVenta, 'Cuenta Corriente')";
                    using (MySqlCommand cmdMov = new MySqlCommand(sqlMov, conn, tx))
                    {
                        cmdMov.Parameters.AddWithValue("@imp", total);
                        cmdMov.Parameters.AddWithValue("@idCuenta", idCuenta);
                        cmdMov.Parameters.AddWithValue("@desc", $"Alquiler Cancha(s) - {tipoComprobante} N° {numComp}");
                        cmdMov.Parameters.AddWithValue("@idVenta", idVenta);
                        cmdMov.ExecuteNonQuery();
                    }
                }

                tx.Commit();
                return idVenta;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        // ─────────────────────────────────────────────
        //  COMPROBANTES Y CONSULTAS GENERALES
        // ─────────────────────────────────────────────

        public static DatosComprobante ObtenerDatosComprobante(int idVenta)
        {
            AsegurarTablas();

            const string sql = @"
                SELECT v.id_venta,
                       v.fecha,
                       v.total,
                       v.medio_pago,
                       COALESCE(v.tipo_comprobante, 'Ticket') AS tipo_comprobante,
                       COALESCE(v.numero_comprobante, CONCAT('TK-0001-', LPAD(v.id_venta, 8, '0'))) AS numero_comprobante,
                       CONCAT(p.apellido, ', ', p.nombre) AS cliente,
                       COALESCE(NULLIF(p.cuit, ''), NULLIF(p.dni, ''), '-') AS documento,
                       COALESCE(p.direccion, '-') AS direccion,
                       COALESCE(p.email, '') AS email,
                       COALESCE(u.nombre_completo, 'Usuario') AS cajero
                FROM venta v
                JOIN cliente c ON c.id_cliente = v.id_cliente
                JOIN persona p ON p.id_persona = c.id_persona
                JOIN usuario u ON u.id_usuario = v.id_usuario
                WHERE v.id_venta = @id";

            DatosComprobante datos = new DatosComprobante { IdVenta = idVenta };

            using (MySqlConnection conn = Conexion.ObtenerConexion())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idVenta);
                    using MySqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        datos.Fecha = r.GetDateTime("fecha");
                        datos.Total = Convert.ToDecimal(r["total"]);
                        datos.MedioPago = r.GetString("medio_pago");
                        datos.TipoComprobante = r.GetString("tipo_comprobante");
                        datos.NumeroComprobante = r.GetString("numero_comprobante");
                        datos.NombreCliente = r.GetString("cliente");
                        datos.DocumentoCliente = r.GetString("documento");
                        datos.DireccionCliente = r.GetString("direccion");
                        datos.EmailCliente = r.GetString("email");
                        datos.Cajero = r.GetString("cajero");
                    }
                }

                // Cargar ítems
                const string sqlItems = @"
                    SELECT COALESCE(dv.descripcion, p.nombre, 'Producto / Servicio') AS descripcion,
                           dv.cantidad,
                           dv.precio_unitario,
                           dv.total
                    FROM detalle_venta dv
                    LEFT JOIN producto p ON p.id_producto = dv.id_producto
                    WHERE dv.id_venta = @id";

                using (MySqlCommand cmdItems = new MySqlCommand(sqlItems, conn))
                {
                    cmdItems.Parameters.AddWithValue("@id", idVenta);
                    using MySqlDataReader r = cmdItems.ExecuteReader();
                    while (r.Read())
                    {
                        datos.Items.Add(new ItemComprobante
                        {
                            Descripcion = r.GetString("descripcion"),
                            Cantidad = Convert.ToInt32(r["cantidad"]),
                            PrecioUnitario = Convert.ToDecimal(r["precio_unitario"])
                        });
                    }
                }
            }

            return datos;
        }

        public static DataTable ObtenerVentas()
        {
            AsegurarTablas();

            const string query = @"
                SELECT v.id_venta,
                       v.fecha,
                       COALESCE(v.tipo_comprobante, 'Ticket') AS comprobante,
                       COALESCE(v.numero_comprobante, CONCAT('TK-0001-', LPAD(v.id_venta, 8, '0'))) AS numero,
                       CONCAT(p.apellido, ', ', p.nombre) AS cliente,
                       v.medio_pago,
                       v.total,
                       u.nombre_completo AS cajero,
                       IF(v.cancelada = 1, 'Cancelada', 'Activa') AS estado
                FROM venta v
                JOIN cliente c ON c.id_cliente = v.id_cliente
                JOIN persona p ON p.id_persona = c.id_persona
                JOIN usuario u ON u.id_usuario = v.id_usuario
                ORDER BY v.id_venta DESC";

            return LlenarTabla(query);
        }

        public static DataTable ObtenerDetalleVenta(int idVenta)
        {
            AsegurarTablas();

            const string query = @"
                SELECT COALESCE(dv.descripcion, p.nombre, 'Producto / Servicio') AS producto,
                       dv.cantidad,
                       dv.precio_unitario,
                       dv.total AS subtotal
                FROM detalle_venta dv
                LEFT JOIN producto p ON p.id_producto = dv.id_producto
                WHERE dv.id_venta = @id";

            return LlenarTabla(query, ("@id", idVenta));
        }

        // Solo productos con stock > 0 y activos (excluyendo comodines de servicio)
        public static DataTable ObtenerProductos()
        {
            const string query = @"
                SELECT producto.id_producto,
                       producto.codigo,
                       producto.nombre,
                       producto.precio_venta,
                       producto.stock,
                       producto.nombre AS descripcion
                FROM producto
                WHERE producto.activo = 1 AND producto.id_producto NOT IN (998, 999)
                ORDER BY producto.nombre";

            return LlenarTabla(query);
        }

        // Clientes registrados + opción predeterminada de Consumidor Final
        public static DataTable ObtenerClientes(bool incluirConsumidorFinal = true)
        {
            AsegurarTablas();

            const string query = @"
                SELECT c.id_cliente,
                       CONCAT(p.apellido, ', ', p.nombre, ' (DNI: ', p.dni, ')') AS nombre_completo,
                       p.email,
                       p.dni,
                       p.cuit,
                       p.razon_social,
                       COALESCE(p.condicion_fiscal, CASE WHEN p.razon_social IS NOT NULL AND p.razon_social <> '' THEN 'Responsable Inscripto' ELSE 'Consumidor Final' END) AS condicion_fiscal
                FROM cliente c
                INNER JOIN persona p ON p.id_persona = c.id_persona
                WHERE p.activo = 1 AND c.id_cliente <> 9999
                ORDER BY p.apellido, p.nombre";

            DataTable dt = LlenarTabla(query);

            if (incluirConsumidorFinal)
            {
                DataRow rowCF = dt.NewRow();
                rowCF["id_cliente"] = 9999;
                rowCF["nombre_completo"] = "[ Consumidor Final ]";
                rowCF["email"] = "";
                rowCF["dni"] = "0";
                rowCF["cuit"] = "-";
                rowCF["razon_social"] = "";
                rowCF["condicion_fiscal"] = "Consumidor Final";
                dt.Rows.InsertAt(rowCF, 0);
            }

            return dt;
        }

        public static int? ObtenerIdSesionCajaAbierta(int idUsuario)
        {
            try
            {
                DataRow? sesion = CajaService.ObtenerSesionAbierta(1, idUsuario);
                if (sesion != null)
                {
                    return Convert.ToInt32(sesion["id_sesion"]);
                }
            }
            catch { }
            return null;
        }

        public static void EliminarVenta(int idVenta)
        {
            AsegurarTablas();

            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            using MySqlTransaction tx = conn.BeginTransaction();

            try
            {
                // Devolver stock de productos de cantina
                const string queryDetalle = @"
                    SELECT id_producto, cantidad
                    FROM detalle_venta
                    WHERE id_venta = @id AND id_producto IS NOT NULL AND id_producto NOT IN (998, 999)
                    FOR UPDATE";

                List<(int idProducto, int cantidad)> items = new List<(int, int)>();
                using (MySqlCommand cmdDetalle = new MySqlCommand(queryDetalle, conn, tx))
                {
                    cmdDetalle.Parameters.AddWithValue("@id", idVenta);
                    using MySqlDataReader reader = cmdDetalle.ExecuteReader();
                    while (reader.Read())
                    {
                        items.Add((reader.GetInt32("id_producto"), reader.GetInt32("cantidad")));
                    }
                }

                foreach (var item in items)
                {
                    const string queryStock = @"
                        UPDATE producto
                        SET stock = stock + @cant
                        WHERE id_producto = @prod";
                    using MySqlCommand cmdStock = new MySqlCommand(queryStock, conn, tx);
                    cmdStock.Parameters.AddWithValue("@cant", item.cantidad);
                    cmdStock.Parameters.AddWithValue("@prod", item.idProducto);
                    cmdStock.ExecuteNonQuery();
                }

                // Borrar movimientos asociados en cuenta_corriente
                using (MySqlCommand cmdBorrarCC = new MySqlCommand("DELETE FROM movimiento_cuenta_corriente WHERE id_venta = @id", conn, tx))
                {
                    cmdBorrarCC.Parameters.AddWithValue("@id", idVenta);
                    cmdBorrarCC.ExecuteNonQuery();
                }

                // Borrar detalle y venta
                using (MySqlCommand cmdBorrarDet = new MySqlCommand("DELETE FROM detalle_venta WHERE id_venta = @id", conn, tx))
                {
                    cmdBorrarDet.Parameters.AddWithValue("@id", idVenta);
                    cmdBorrarDet.ExecuteNonQuery();
                }

                using (MySqlCommand cmdBorrarVenta = new MySqlCommand("DELETE FROM venta WHERE id_venta = @id", conn, tx))
                {
                    cmdBorrarVenta.Parameters.AddWithValue("@id", idVenta);
                    cmdBorrarVenta.ExecuteNonQuery();
                }

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        // ─────────────────────────────────────────────
        //  CANCELACIÓN DE VENTAS
        // ─────────────────────────────────────────────

        // Cancela la venta (baja lógica: queda registrada con cancelada = 1), devuelve el stock,
        // revierte la deuda en cuenta corriente y libera las reservas de cancha que había pagado.
        // Todo en una sola transacción: si algo falla no se modifica nada.
        // Lanza ArgumentException si el motivo no es válido e InvalidOperationException si la venta
        // no se puede cancelar (no existe, ya cancelada, caja cerrada, cobro de cta. cte. o cargos ya pagados).
        public static void CancelarVenta(int idVenta, int idUsuario, string motivo)
        {
            AsegurarTablas();

            string? errorMotivo = ValidarMotivoCancelacion(motivo);
            if (errorMotivo != null) throw new ArgumentException(errorMotivo);

            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            using MySqlTransaction tx = conn.BeginTransaction();

            VentaParaCancelar? venta = ObtenerVentaParaCancelar(conn, tx, idVenta);
            bool cajaAbierta = venta != null && CajaDeVentaAbierta(conn, tx, venta.IdSesion);
            bool esCobroCC = venta != null && EsCobroCuentaCorriente(conn, tx, idVenta);
            bool cargosPagados = venta != null && TieneCargosPagados(conn, tx, idVenta);

            string? error = ValidarVentaCancelable(venta, cajaAbierta, esCobroCC, cargosPagados);
            if (error != null) throw new InvalidOperationException(error);

            DevolverStockDeVenta(conn, tx, idVenta);
            RevertirCuentaCorriente(conn, tx, venta!);
            LiberarReservasDeVenta(conn, tx, idVenta);
            MarcarVentaCancelada(conn, tx, idVenta, idUsuario, motivo);

            tx.Commit();
        }

        public static bool EsProductoDeServicio(int idProducto)
        {
            return idProducto == 998 || idProducto == 999;
        }

        public static string? ValidarMotivoCancelacion(string? motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo)) return "Indicá el motivo de la cancelación.";
            if (motivo.Trim().Length > 255) return "El motivo no puede superar los 255 caracteres.";
            return null;
        }

        public static string? ValidarVentaCancelable(VentaParaCancelar? venta, bool cajaAbierta, bool esCobroCuentaCorriente, bool tieneCargosPagados)
        {
            if (venta == null) return "La venta no existe.";
            if (venta.Cancelada) return "La venta ya fue cancelada.";
            if (esCobroCuentaCorriente) return "No se puede cancelar un cobro de cuenta corriente.";
            if (!cajaAbierta) return "No se puede cancelar: la caja en la que se hizo la venta ya está cerrada.";
            if (tieneCargosPagados) return "No se puede cancelar: la deuda de esta venta en cuenta corriente ya fue pagada total o parcialmente.";
            return null;
        }

        // FOR UPDATE bloquea la fila para que dos cancelaciones simultáneas no devuelvan el stock dos veces.
        // Devuelve null si la venta no existe.
        public static VentaParaCancelar? ObtenerVentaParaCancelar(MySqlConnection conn, MySqlTransaction tx, int idVenta)
        {
            const string query = @"
                SELECT venta.id_venta,
                       venta.cancelada,
                       venta.medio_pago,
                       venta.id_cliente,
                       venta.id_sesion,
                       venta.total
                FROM venta
                WHERE venta.id_venta = @id
                FOR UPDATE";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@id", idVenta);
            using MySqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new VentaParaCancelar
            {
                IdVenta = Convert.ToInt32(reader["id_venta"]),
                Cancelada = Convert.ToInt32(reader["cancelada"]) == 1,
                MedioPago = reader["medio_pago"] == DBNull.Value ? "" : reader["medio_pago"].ToString() ?? "",
                IdCliente = Convert.ToInt32(reader["id_cliente"]),
                IdSesion = reader["id_sesion"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["id_sesion"]),
                Total = Convert.ToDecimal(reader["total"])
            };
        }

        public static bool CajaDeVentaAbierta(MySqlConnection conn, MySqlTransaction tx, int? idSesion)
        {
            if (!idSesion.HasValue) return false;

            const string query = @"
                SELECT COUNT(*)
                FROM caja
                WHERE caja.id_sesion = @sesion AND caja.estado = 'Abierta'";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@sesion", idSesion.Value);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public static bool EsCobroCuentaCorriente(MySqlConnection conn, MySqlTransaction tx, int idVenta)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM detalle_venta
                WHERE detalle_venta.id_venta = @id AND detalle_venta.id_producto = 998";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@id", idVenta);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public static bool TieneCargosPagados(MySqlConnection conn, MySqlTransaction tx, int idVenta)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM movimiento_cuenta_corriente
                WHERE movimiento_cuenta_corriente.id_venta = @id
                  AND movimiento_cuenta_corriente.tipo_movimiento = 'Cargo'
                  AND movimiento_cuenta_corriente.estado = 'Pagado'";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@id", idVenta);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        // ─────────────────────────────────────────────
        //  ACTUALIZACIÓN DE STOCK
        // ─────────────────────────────────────────────

        // Suma (diferencia positiva) o resta (diferencia negativa) stock de un producto.
        // Abre su propia conexión y transacción.
        public static void ActualizarStock(int idProducto, int diferencia)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();
            using MySqlTransaction tx = conn.BeginTransaction();

            ActualizarStock(conn, tx, idProducto, diferencia);

            tx.Commit();
        }

        // Versión para usar dentro de una transacción ya abierta.
        // No bloquea el stock negativo: igual que RegistrarVenta, que permite vender sin stock.
        public static void ActualizarStock(MySqlConnection conn, MySqlTransaction tx, int idProducto, int diferencia)
        {
            string? error = ValidarAjusteStock(idProducto, diferencia);
            if (error != null) throw new ArgumentException(error);

            const string query = @"
                UPDATE producto
                SET producto.stock = producto.stock + @dif
                WHERE producto.id_producto = @prod";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@dif", diferencia);
            cmd.Parameters.AddWithValue("@prod", idProducto);

            if (cmd.ExecuteNonQuery() == 0)
                throw new InvalidOperationException($"El producto {idProducto} no existe.");
        }

        public static string? ValidarAjusteStock(int idProducto, int diferencia)
        {
            if (idProducto <= 0) return "Producto inválido.";
            if (EsProductoDeServicio(idProducto)) return "Los servicios no manejan stock.";
            if (diferencia == 0) return "La diferencia de stock no puede ser 0.";
            return null;
        }

        // Devuelve al stock lo que descontó la venta. Los servicios (canchas, cobros) no tienen stock.
        // Devuelve la cantidad de productos a los que se les actualizó el stock.
        public static int DevolverStockDeVenta(MySqlConnection conn, MySqlTransaction tx, int idVenta)
        {
            List<LineaVenta> items = LeerItemsDeVenta(conn, tx, idVenta);

            foreach (LineaVenta item in items)
                ActualizarStock(conn, tx, item.IdProducto, item.Cantidad);

            return items.Count;
        }

        // Solo productos con stock (excluye servicios y líneas sin producto).
        public static List<LineaVenta> LeerItemsDeVenta(MySqlConnection conn, MySqlTransaction tx, int idVenta)
        {
            const string query = @"
                SELECT detalle_venta.id_producto,
                       detalle_venta.cantidad,
                       detalle_venta.descripcion,
                       detalle_venta.precio_unitario
                FROM detalle_venta
                WHERE detalle_venta.id_venta = @id
                  AND detalle_venta.id_producto IS NOT NULL
                  AND detalle_venta.id_producto NOT IN (998, 999)";

            List<LineaVenta> items = new List<LineaVenta>();
            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@id", idVenta);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new LineaVenta
                {
                    IdProducto = Convert.ToInt32(reader["id_producto"]),
                    Cantidad = Convert.ToInt32(reader["cantidad"]),
                    Nombre = reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"].ToString() ?? "",
                    PrecioUnitario = Convert.ToDecimal(reader["precio_unitario"])
                });
            }
            return items;
        }

        // ─────────────────────────────────────────────
        //  PASOS DE LA CANCELACIÓN
        // ─────────────────────────────────────────────

        // Si la venta fue a cuenta corriente: borra sus cargos pendientes y baja el saldo deudor.
        // Devuelve el importe revertido (0 si no era de cuenta corriente).
        public static decimal RevertirCuentaCorriente(MySqlConnection conn, MySqlTransaction tx, VentaParaCancelar venta)
        {
            if (!venta.MedioPago.Equals("Cuenta Corriente", StringComparison.OrdinalIgnoreCase)) return 0m;

            const string querySuma = @"
                SELECT COALESCE(SUM(movimiento_cuenta_corriente.importe), 0)
                FROM movimiento_cuenta_corriente
                WHERE movimiento_cuenta_corriente.id_venta = @id
                  AND movimiento_cuenta_corriente.tipo_movimiento = 'Cargo'
                  AND movimiento_cuenta_corriente.estado = 'Pendiente'";

            decimal importe;
            using (MySqlCommand cmdSuma = new MySqlCommand(querySuma, conn, tx))
            {
                cmdSuma.Parameters.AddWithValue("@id", venta.IdVenta);
                importe = Convert.ToDecimal(cmdSuma.ExecuteScalar());
            }

            const string queryBorrar = @"
                DELETE FROM movimiento_cuenta_corriente
                WHERE movimiento_cuenta_corriente.id_venta = @id
                  AND movimiento_cuenta_corriente.tipo_movimiento = 'Cargo'
                  AND movimiento_cuenta_corriente.estado = 'Pendiente'";

            using (MySqlCommand cmdBorrar = new MySqlCommand(queryBorrar, conn, tx))
            {
                cmdBorrar.Parameters.AddWithValue("@id", venta.IdVenta);
                cmdBorrar.ExecuteNonQuery();
            }

            int idCuenta = ObtenerOCrearIdCuentaCorriente(conn, tx, venta.IdCliente);

            // GREATEST evita que el saldo deudor quede negativo.
            const string queryDeuda = @"
                UPDATE cuenta_corriente
                SET cuenta_corriente.saldo = GREATEST(cuenta_corriente.saldo - @importe, 0)
                WHERE cuenta_corriente.id_cuenta = @cuenta";

            using (MySqlCommand cmdDeuda = new MySqlCommand(queryDeuda, conn, tx))
            {
                cmdDeuda.Parameters.AddWithValue("@importe", importe);
                cmdDeuda.Parameters.AddWithValue("@cuenta", idCuenta);
                cmdDeuda.ExecuteNonQuery();
            }

            return importe;
        }

        // Las reservas de cancha pagadas con esta venta vuelven a quedar pendientes de cobro.
        // Devuelve la cantidad de reservas liberadas.
        public static int LiberarReservasDeVenta(MySqlConnection conn, MySqlTransaction tx, int idVenta)
        {
            const string queryLeer = @"
                SELECT reserva_pagada.id_reserva
                FROM reserva_pagada
                WHERE reserva_pagada.id_venta = @id";

            List<int> idsReserva = new List<int>();
            using (MySqlCommand cmdLeer = new MySqlCommand(queryLeer, conn, tx))
            {
                cmdLeer.Parameters.AddWithValue("@id", idVenta);
                using MySqlDataReader reader = cmdLeer.ExecuteReader();
                while (reader.Read())
                    idsReserva.Add(Convert.ToInt32(reader["id_reserva"]));
            }

            foreach (int idReserva in idsReserva)
            {
                using MySqlCommand cmdEstado = new MySqlCommand(
                    "UPDATE reserva SET reserva.estado = 'Confirmada' WHERE reserva.id_reserva = @reserva", conn, tx);
                cmdEstado.Parameters.AddWithValue("@reserva", idReserva);
                cmdEstado.ExecuteNonQuery();
            }

            using (MySqlCommand cmdBorrar = new MySqlCommand(
                "DELETE FROM reserva_pagada WHERE reserva_pagada.id_venta = @id", conn, tx))
            {
                cmdBorrar.Parameters.AddWithValue("@id", idVenta);
                cmdBorrar.ExecuteNonQuery();
            }

            return idsReserva.Count;
        }

        public static void MarcarVentaCancelada(MySqlConnection conn, MySqlTransaction tx, int idVenta, int idUsuario, string motivo)
        {
            // cancelada = 0 en el WHERE: si otra cancelación se coló primero, esta no pisa nada y falla.
            const string query = @"
                UPDATE venta
                SET venta.cancelada = 1,
                    venta.fecha_cancelacion = NOW(),
                    venta.motivo_cancelacion = @motivo,
                    venta.id_usuario_cancelacion = @usuario
                WHERE venta.id_venta = @id AND venta.cancelada = 0";

            using MySqlCommand cmd = new MySqlCommand(query, conn, tx);
            cmd.Parameters.AddWithValue("@motivo", motivo.Trim());
            cmd.Parameters.AddWithValue("@usuario", idUsuario);
            cmd.Parameters.AddWithValue("@id", idVenta);

            if (cmd.ExecuteNonQuery() == 0)
                throw new InvalidOperationException("La venta no existe o ya fue cancelada.");
        }

        private static DataTable LlenarTabla(string query, params (string nombre, object valor)[] parametros)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            foreach (var (nombre, valor) in parametros)
                cmd.Parameters.AddWithValue(nombre, valor);

            DataTable dt = new DataTable();
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// Actualiza el precio de los cargos pendientes en cuenta corriente cuando se modifica el precio del producto.
        /// </summary>
        public static void ActualizarPreciosCuentaCorrienteProducto(MySqlConnection conn, MySqlTransaction tx, int idProducto, decimal nuevoPrecioVenta)
        {
            AsegurarTablas();

            List<int> cuentasAfectadas = new List<int>();
            List<int> ventasAfectadas = new List<int>();

            const string sqlBuscar = @"
                SELECT DISTINCT id_cuenta, id_venta
                FROM movimiento_cuenta_corriente
                WHERE id_producto = @prod
                  AND tipo_movimiento = 'Cargo'
                  AND estado = 'Pendiente'";

            using (MySqlCommand cmdBuscar = new MySqlCommand(sqlBuscar, conn, tx))
            {
                cmdBuscar.Parameters.AddWithValue("@prod", idProducto);
                using MySqlDataReader r = cmdBuscar.ExecuteReader();
                while (r.Read())
                {
                    if (!r.IsDBNull(r.GetOrdinal("id_cuenta")))
                    {
                        int c = r.GetInt32("id_cuenta");
                        if (!cuentasAfectadas.Contains(c)) cuentasAfectadas.Add(c);
                    }
                    if (!r.IsDBNull(r.GetOrdinal("id_venta")))
                    {
                        int v = r.GetInt32("id_venta");
                        if (!ventasAfectadas.Contains(v)) ventasAfectadas.Add(v);
                    }
                }
            }

            if (cuentasAfectadas.Count == 0 && ventasAfectadas.Count == 0) return;

            // Actualizar detalle_venta y venta.total para ventas pendientes
            foreach (int idVenta in ventasAfectadas)
            {
                const string sqlUpdDet = @"
                    UPDATE detalle_venta
                    SET precio_unitario = @p,
                        total = cantidad * @p
                    WHERE id_venta = @venta AND id_producto = @prod";
                using (MySqlCommand cmdUpdDet = new MySqlCommand(sqlUpdDet, conn, tx))
                {
                    cmdUpdDet.Parameters.AddWithValue("@p", nuevoPrecioVenta);
                    cmdUpdDet.Parameters.AddWithValue("@venta", idVenta);
                    cmdUpdDet.Parameters.AddWithValue("@prod", idProducto);
                    cmdUpdDet.ExecuteNonQuery();
                }

                const string sqlUpdVenta = @"
                    UPDATE venta
                    SET total = (SELECT COALESCE(SUM(total), 0) FROM detalle_venta WHERE id_venta = @venta)
                    WHERE id_venta = @venta";
                using MySqlCommand cmdUpdVenta = new MySqlCommand(sqlUpdVenta, conn, tx);
                cmdUpdVenta.Parameters.AddWithValue("@venta", idVenta);
                cmdUpdVenta.ExecuteNonQuery();
            }

            // Actualizar importes en movimiento_cuenta_corriente
            const string sqlUpdMov = @"
                UPDATE movimiento_cuenta_corriente m
                LEFT JOIN detalle_venta dv ON dv.id_venta = m.id_venta AND dv.id_producto = m.id_producto
                SET m.importe = CASE WHEN dv.cantidad IS NOT NULL THEN dv.cantidad * @p ELSE @p END
                WHERE m.id_producto = @prod
                  AND m.tipo_movimiento = 'Cargo'
                  AND m.estado = 'Pendiente'";
            using (MySqlCommand cmdUpdMov = new MySqlCommand(sqlUpdMov, conn, tx))
            {
                cmdUpdMov.Parameters.AddWithValue("@p", nuevoPrecioVenta);
                cmdUpdMov.Parameters.AddWithValue("@prod", idProducto);
                cmdUpdMov.ExecuteNonQuery();
            }

            // Recalcular saldo adeudado en cada cuenta corriente afectada
            foreach (int idCuenta in cuentasAfectadas)
            {
                const string sqlUpdSaldo = @"
                    UPDATE cuenta_corriente
                    SET saldo = (
                        SELECT COALESCE(SUM(importe), 0)
                        FROM movimiento_cuenta_corriente
                        WHERE id_cuenta = @cta
                          AND tipo_movimiento = 'Cargo'
                          AND estado = 'Pendiente'
                    )
                    WHERE id_cuenta = @cta";
                using (MySqlCommand cmdUpdSaldo = new MySqlCommand(sqlUpdSaldo, conn, tx))
                {
                    cmdUpdSaldo.Parameters.AddWithValue("@cta", idCuenta);
                    cmdUpdSaldo.ExecuteNonQuery();
                }
            }
        }
    }
}
