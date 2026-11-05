using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    /// <summary>
    /// Servicio de lógica y consultas a base de datos para el módulo de Reportes.
    /// Contempla Reportes de Caja (con desglose de medios de pago y fiado en Cuenta Corriente),
    /// Reportes de Ventas (sin Cuenta Corriente, con horas de pádel y división Cantina vs. Pádel),
    /// Reportes de Compras y Detalles de Producto.
    /// </summary>
    public static class ReportesService
    {
        // ═══════════════════════════════════════════════════
        //  REPORTES DE CAJA
        // ═══════════════════════════════════════════════════

        public static DataTable ObtenerReporteCaja(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddSeconds(-1);

            const string query = @"
                SELECT u.nombre_completo        AS 'Cajero',
                       c.fecha_apertura         AS 'Apertura',
                       c.fecha_cierre           AS 'Cierre',
                       c.monto_inicio           AS 'Inicio ($)',
                       COALESCE((
                           SELECT SUM(dv.total)
                           FROM venta v
                           INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                           WHERE v.id_sesion = c.id_sesion
                             AND v.cancelada = 0
                             AND (v.estado IS NULL OR v.estado <> 'Anulada')
                             AND v.medio_pago = 'Efectivo'
                       ), 0)                    AS 'Ventas Efectivo ($)',
                       COALESCE((
                           SELECT SUM(dv.total)
                           FROM venta v
                           INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                           WHERE v.id_sesion = c.id_sesion
                             AND v.cancelada = 0
                             AND (v.estado IS NULL OR v.estado <> 'Anulada')
                             AND v.medio_pago = 'Tarjeta de Débito'
                       ), 0)                    AS 'Tarjeta Débito ($)',
                       COALESCE((
                           SELECT SUM(dv.total)
                           FROM venta v
                           INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                           WHERE v.id_sesion = c.id_sesion
                             AND v.cancelada = 0
                             AND (v.estado IS NULL OR v.estado <> 'Anulada')
                             AND v.medio_pago = 'Tarjeta de Crédito'
                       ), 0)                    AS 'Tarjeta Crédito ($)',
                       COALESCE((
                           SELECT SUM(dv.total)
                           FROM venta v
                           INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                           WHERE v.id_sesion = c.id_sesion
                             AND v.cancelada = 0
                             AND (v.estado IS NULL OR v.estado <> 'Anulada')
                             AND v.medio_pago = 'Billetera Virtual'
                       ), 0)                    AS 'Billetera Virtual ($)',
                       COALESCE((
                           SELECT SUM(dv.total)
                           FROM venta v
                           INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                           WHERE v.id_sesion = c.id_sesion
                             AND v.cancelada = 0
                             AND (v.estado IS NULL OR v.estado <> 'Anulada')
                             AND v.medio_pago = 'Cuenta Corriente'
                       ), 0)                    AS 'Fiado Cta. Cte. ($)',
                       COALESCE((
                           SELECT SUM(r.monto)
                           FROM retiro_efectivo r
                           WHERE r.id_sesion = c.id_sesion
                       ), 0)                    AS 'Retiros ($)',
                       c.monto_cierre           AS 'Cierre Registrado ($)',
                       CASE
                           WHEN c.estado = 'Cerrada' AND c.monto_cierre IS NOT NULL THEN
                               c.monto_cierre - (
                                   c.monto_inicio
                                   + COALESCE((
                                       SELECT SUM(dv.total)
                                       FROM venta v
                                       INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                                       WHERE v.id_sesion = c.id_sesion
                                         AND v.cancelada = 0
                                         AND (v.estado IS NULL OR v.estado <> 'Anulada')
                                         AND v.medio_pago = 'Efectivo'
                                   ), 0)
                                   - COALESCE((
                                       SELECT SUM(r.monto)
                                       FROM retiro_efectivo r
                                       WHERE r.id_sesion = c.id_sesion
                                   ), 0)
                               )
                           ELSE 0.00
                       END                      AS 'Diferencia ($)',
                       c.estado                 AS 'Estado'
                FROM caja c
                INNER JOIN usuario u ON u.id_usuario = c.id_usuario_apertura
                WHERE c.fecha_apertura >= @desde AND c.fecha_apertura <= @hasta
                ORDER BY c.fecha_apertura DESC;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        /// <summary>
        /// Obtiene el total de dinero fiado pendiente de cobro (lo que se debe) en Cuenta Corriente.
        /// </summary>
        public static decimal ObtenerDeudaTotalCuentaCorriente()
        {
            const string query = @"
                SELECT COALESCE(SUM(saldo), 0) AS total_deuda
                FROM cuenta_corriente
                WHERE saldo > 0;";

            DataTable dt = EjecutarConsulta(query);
            if (dt.Rows.Count > 0 && dt.Rows[0]["total_deuda"] != DBNull.Value)
            {
                return Convert.ToDecimal(dt.Rows[0]["total_deuda"]);
            }
            return 0m;
        }

        /// <summary>
        /// Obtiene el listado detallado de cargos pendientes (fiado / lo que se debe) en Cuenta Corriente.
        /// Sin columna de ID y con N° Comprobante en primer lugar.
        /// </summary>
        public static DataTable ObtenerDeudasCuentaCorriente(DateTime desde, DateTime hasta)
        {
            const string query = @"
                SELECT COALESCE(v.numero_comprobante, CONCAT('CC-0001-', LPAD(m.id_movimiento_ccc, 8, '0'))) AS 'N° Comprobante',
                       m.fecha                                                                               AS 'Fecha',
                       COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido))               AS 'Cliente',
                       COALESCE(prod.nombre, m.descripcion, 'Cargo en Cuenta Corriente')                     AS 'Producto',
                       m.importe                                                                             AS 'Importe Fiado ($)',
                       cc.saldo                                                                              AS 'Deuda Total Cliente ($)',
                       m.estado                                                                              AS 'Estado'
                FROM movimiento_cuenta_corriente m
                INNER JOIN cuenta_corriente cc ON cc.id_cuenta = m.id_cuenta
                INNER JOIN cliente cl ON cl.id_cliente = cc.id_cliente
                INNER JOIN persona p ON p.id_persona = cl.id_persona
                LEFT JOIN venta v ON v.id_venta = m.id_venta
                LEFT JOIN producto prod ON prod.id_producto = m.id_producto
                WHERE m.tipo_movimiento = 'Cargo'
                  AND m.estado = 'Pendiente'
                ORDER BY m.fecha DESC, m.id_movimiento_ccc DESC;";

            return EjecutarConsulta(query);
        }

        public static Dictionary<string, object> ObtenerResumenCaja(DateTime desde, DateTime hasta)
        {
            decimal deudaCtaCte = ObtenerDeudaTotalCuentaCorriente();

            var resumen = new Dictionary<string, object>
            {
                ["TotalSesiones"] = 0,
                ["SesionesAbiertas"] = 0,
                ["TotalInicio"] = 0m,
                ["TotalVentasEfectivo"] = 0m,
                ["TotalTarjetaDebito"] = 0m,
                ["TotalTarjetaCredito"] = 0m,
                ["TotalBilleteraVirtual"] = 0m,
                ["TotalOtrosMedios"] = 0m,
                ["TotalFiadoPeriodo"] = 0m,
                ["TotalDeudaCtaCte"] = deudaCtaCte,
                ["TotalRetiros"] = 0m,
                ["TotalEsperado"] = 0m,
                ["TotalCierreRegistrado"] = 0m,
                ["TotalDiferencia"] = 0m
            };

            DataTable dt = ObtenerReporteCaja(desde, hasta);
            if (dt.Rows.Count == 0) return resumen;

            int totalSesiones = dt.Rows.Count;
            int abiertas = 0;
            decimal totalInicio = 0m;
            decimal totalVentasEfvo = 0m;
            decimal totalDebito = 0m;
            decimal totalCredito = 0m;
            decimal totalBilletera = 0m;
            decimal totalFiado = 0m;
            decimal totalRetiros = 0m;
            decimal totalCierres = 0m;
            decimal totalDif = 0m;

            foreach (DataRow r in dt.Rows)
            {
                if (r["Estado"].ToString() == "Abierta") abiertas++;
                totalInicio += Convert.ToDecimal(r["Inicio ($)"]);
                totalVentasEfvo += Convert.ToDecimal(r["Ventas Efectivo ($)"]);
                totalDebito += Convert.ToDecimal(r["Tarjeta Débito ($)"]);
                totalCredito += Convert.ToDecimal(r["Tarjeta Crédito ($)"]);
                totalBilletera += Convert.ToDecimal(r["Billetera Virtual ($)"]);
                totalFiado += Convert.ToDecimal(r["Fiado Cta. Cte. ($)"]);
                totalRetiros += Convert.ToDecimal(r["Retiros ($)"]);
                if (r["Cierre Registrado ($)"] != DBNull.Value && r["Cierre Registrado ($)"] != null)
                    totalCierres += Convert.ToDecimal(r["Cierre Registrado ($)"]);
                totalDif += Convert.ToDecimal(r["Diferencia ($)"]);
            }

            resumen["TotalSesiones"] = totalSesiones;
            resumen["SesionesAbiertas"] = abiertas;
            resumen["TotalInicio"] = totalInicio;
            resumen["TotalVentasEfectivo"] = totalVentasEfvo;
            resumen["TotalTarjetaDebito"] = totalDebito;
            resumen["TotalTarjetaCredito"] = totalCredito;
            resumen["TotalBilleteraVirtual"] = totalBilletera;
            resumen["TotalOtrosMedios"] = totalDebito + totalCredito + totalBilletera;
            resumen["TotalFiadoPeriodo"] = totalFiado;
            resumen["TotalDeudaCtaCte"] = deudaCtaCte;
            resumen["TotalRetiros"] = totalRetiros;
            resumen["TotalEsperado"] = totalInicio + totalVentasEfvo - totalRetiros;
            resumen["TotalCierreRegistrado"] = totalCierres;
            resumen["TotalDiferencia"] = totalDif;

            return resumen;
        }

        public static DataTable ObtenerRetirosCaja(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddSeconds(-1);

            const string query = @"
                SELECT CONCAT('RT-0001-', LPAD(r.id_retiro, 8, '0')) AS 'N° Comprobante',
                       r.fecha                                       AS 'Fecha / Hora',
                       u.nombre_completo                             AS 'Registrado por',
                       r.monto                                       AS 'Monto ($)',
                       r.motivo                                      AS 'Motivo'
                FROM retiro_efectivo r
                INNER JOIN usuario u ON u.id_usuario = r.id_usuario
                WHERE r.fecha >= @desde AND r.fecha <= @hasta
                ORDER BY r.fecha DESC;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        // ═══════════════════════════════════════════════════
        //  REPORTES DE VENTAS (CANCHA Y CANTINA, SIN CTA. CTE.)
        // ═══════════════════════════════════════════════════

        public static DataTable ObtenerReporteVentas(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddSeconds(-1);

            const string query = @"
                SELECT COALESCE(v.numero_comprobante, CONCAT('TK-0001-', LPAD(v.id_venta, 8, '0'))) AS 'N° Comprobante',
                       v.fecha                                     AS 'Fecha / Hora',
                       v.tipo_comprobante                          AS 'Tipo Comprobante',
                       GROUP_CONCAT(CONCAT(COALESCE(prod.nombre, dv.descripcion, 'Producto'), ' (x', dv.cantidad, ')') SEPARATOR ', ') AS 'Producto',
                       COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido)) AS 'Cliente',
                       v.medio_pago                                AS 'Medio de Pago',
                       COALESCE(SUM(CASE WHEN dv.id_producto = 999 THEN dv.cantidad ELSE 0 END), 0) AS 'Horas Pádel',
                       COALESCE(SUM(CASE WHEN dv.id_producto = 999 THEN dv.total ELSE 0 END), 0) AS 'Cancha Pádel ($)',
                       COALESCE(SUM(CASE WHEN dv.id_producto NOT IN (998, 999) THEN dv.total ELSE 0 END), 0) AS 'Cantina ($)',
                       COALESCE(SUM(dv.total), 0)                  AS 'Total Venta ($)',
                       COALESCE(SUM(
                           CASE
                               WHEN dv.id_producto = 999 THEN dv.total
                               WHEN dv.id_producto NOT IN (998, 999) THEN
                                   dv.total - (dv.total / (1 + COALESCE(cat.porcentaje_ganancia, 20) / 100))
                               ELSE 0
                           END
                       ), 0)                                       AS 'Ganancia Estimada ($)',
                       u.nombre_completo                           AS 'Cajero'
                FROM venta v
                INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                INNER JOIN cliente c ON c.id_cliente = v.id_cliente
                INNER JOIN persona p ON p.id_persona = c.id_persona
                INNER JOIN usuario u ON u.id_usuario = v.id_usuario
                LEFT JOIN producto prod ON prod.id_producto = dv.id_producto
                LEFT JOIN categoria cat ON cat.id_categoria = prod.id_categoria
                WHERE v.cancelada = 0
                  AND (v.estado IS NULL OR v.estado <> 'Anulada')
                  AND dv.id_producto <> 998
                  AND (v.medio_pago IS NULL OR v.medio_pago <> 'Cuenta Corriente')
                  AND v.fecha >= @desde AND v.fecha <= @hasta
                GROUP BY v.id_venta, v.fecha, v.tipo_comprobante, v.numero_comprobante,
                         p.razon_social, p.nombre, p.apellido, v.medio_pago, u.nombre_completo
                ORDER BY v.fecha DESC;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        /// <summary>
        /// Tabla exclusiva de Ventas de Cantina (para mostrar dividida al lado de la tabla de Pádel).
        /// </summary>
        public static DataTable ObtenerVentasCantina(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddSeconds(-1);

            const string query = @"
                SELECT COALESCE(v.numero_comprobante, CONCAT('TK-0001-', LPAD(v.id_venta, 8, '0'))) AS 'N° Comprobante',
                       v.fecha                                                                      AS 'Fecha / Hora',
                       COALESCE(prod.nombre, dv.descripcion, 'Producto')                            AS 'Producto',
                       dv.cantidad                                                                  AS 'Cant.',
                       COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido))      AS 'Cliente',
                       v.medio_pago                                                                 AS 'Medio de Pago',
                       dv.total                                                                     AS 'Total Cantina ($)',
                       ROUND(dv.total - (dv.total / (1 + COALESCE(cat.porcentaje_ganancia, 20) / 100)), 2) AS 'Ganancia ($)'
                FROM detalle_venta dv
                INNER JOIN venta v ON v.id_venta = dv.id_venta
                INNER JOIN cliente c ON c.id_cliente = v.id_cliente
                INNER JOIN persona p ON p.id_persona = c.id_persona
                LEFT JOIN producto prod ON prod.id_producto = dv.id_producto
                LEFT JOIN categoria cat ON cat.id_categoria = prod.id_categoria
                WHERE v.cancelada = 0
                  AND (v.estado IS NULL OR v.estado <> 'Anulada')
                  AND dv.id_producto NOT IN (998, 999)
                  AND (v.medio_pago IS NULL OR v.medio_pago <> 'Cuenta Corriente')
                  AND v.fecha >= @desde AND v.fecha <= @hasta
                ORDER BY v.fecha DESC, v.id_venta DESC;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        /// <summary>
        /// Tabla exclusiva de Horas y Ventas de Canchas de Pádel (para mostrar dividida al lado de Cantina).
        /// </summary>
        public static DataTable ObtenerVentasPadel(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddSeconds(-1);

            const string query = @"
                SELECT COALESCE(v.numero_comprobante, CONCAT('TK-0001-', LPAD(v.id_venta, 8, '0'))) AS 'N° Comprobante',
                       v.fecha                                                                      AS 'Fecha / Hora',
                       COALESCE(NULLIF(dv.descripcion, ''), prod.nombre, 'Alquiler Cancha')         AS 'Producto',
                       dv.cantidad                                                                  AS 'Horas Pádel',
                       COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido))      AS 'Cliente',
                       v.medio_pago                                                                 AS 'Medio de Pago',
                       dv.total                                                                     AS 'Total Facturado ($)'
                FROM detalle_venta dv
                INNER JOIN venta v ON v.id_venta = dv.id_venta
                INNER JOIN cliente c ON c.id_cliente = v.id_cliente
                INNER JOIN persona p ON p.id_persona = c.id_persona
                LEFT JOIN producto prod ON prod.id_producto = dv.id_producto
                WHERE v.cancelada = 0
                  AND (v.estado IS NULL OR v.estado <> 'Anulada')
                  AND dv.id_producto = 999
                  AND (v.medio_pago IS NULL OR v.medio_pago <> 'Cuenta Corriente')
                  AND v.fecha >= @desde AND v.fecha <= @hasta
                ORDER BY v.fecha DESC, v.id_venta DESC;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        public static Dictionary<string, object> ObtenerResumenVentas(DateTime desde, DateTime hasta)
        {
            var resumen = new Dictionary<string, object>
            {
                ["TotalVentas"] = 0m,
                ["HorasPadel"] = 0,
                ["TotalCancha"] = 0m,
                ["TotalCantina"] = 0m,
                ["CostoCantina"] = 0m,
                ["GananciaCancha"] = 0m,
                ["GananciaCantina"] = 0m,
                ["GananciaTotal"] = 0m,
                ["MargenGlobal"] = 0m,
                ["CantidadVentas"] = 0,
                ["CantidadItems"] = 0
            };

            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddSeconds(-1);

            const string query = @"
                SELECT
                    COUNT(DISTINCT v.id_venta) AS cant_ventas,
                    COALESCE(SUM(dv.cantidad), 0) AS cant_items,
                    COALESCE(SUM(dv.total), 0) AS total_general,
                    COALESCE(SUM(CASE WHEN dv.id_producto = 999 THEN dv.cantidad ELSE 0 END), 0) AS horas_padel,
                    COALESCE(SUM(CASE WHEN dv.id_producto = 999 THEN dv.total ELSE 0 END), 0) AS total_cancha,
                    COALESCE(SUM(CASE WHEN dv.id_producto NOT IN (998, 999) THEN dv.total ELSE 0 END), 0) AS total_cantina,
                    COALESCE(SUM(CASE WHEN dv.id_producto NOT IN (998, 999) THEN (dv.total / (1 + COALESCE(cat.porcentaje_ganancia, 20) / 100)) ELSE 0 END), 0) AS costo_cantina,
                    COALESCE(SUM(CASE WHEN dv.id_producto NOT IN (998, 999) THEN (dv.total - (dv.total / (1 + COALESCE(cat.porcentaje_ganancia, 20) / 100))) ELSE 0 END), 0) AS ganancia_cantina
                FROM detalle_venta dv
                INNER JOIN venta v ON v.id_venta = dv.id_venta
                LEFT JOIN producto prod ON prod.id_producto = dv.id_producto
                LEFT JOIN categoria cat ON cat.id_categoria = prod.id_categoria
                WHERE v.cancelada = 0
                  AND (v.estado IS NULL OR v.estado <> 'Anulada')
                  AND dv.id_producto <> 998
                  AND (v.medio_pago IS NULL OR v.medio_pago <> 'Cuenta Corriente')
                  AND v.fecha >= @desde AND v.fecha <= @hasta;";

            DataTable dt = EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                decimal totalGral = Convert.ToDecimal(r["total_general"]);
                int horasPadel = Convert.ToInt32(r["horas_padel"]);
                decimal totalCancha = Convert.ToDecimal(r["total_cancha"]);
                decimal totalCantina = Convert.ToDecimal(r["total_cantina"]);
                decimal costoCantina = Convert.ToDecimal(r["costo_cantina"]);
                decimal gananciaCantina = Convert.ToDecimal(r["ganancia_cantina"]);
                decimal gananciaCancha = totalCancha;
                decimal gananciaTotal = gananciaCancha + gananciaCantina;

                resumen["CantidadVentas"] = Convert.ToInt32(r["cant_ventas"]);
                resumen["CantidadItems"] = Convert.ToInt32(r["cant_items"]);
                resumen["TotalVentas"] = totalGral;
                resumen["HorasPadel"] = horasPadel;
                resumen["TotalCancha"] = totalCancha;
                resumen["TotalCantina"] = totalCantina;
                resumen["CostoCantina"] = costoCantina;
                resumen["GananciaCancha"] = gananciaCancha;
                resumen["GananciaCantina"] = gananciaCantina;
                resumen["GananciaTotal"] = gananciaTotal;
                resumen["MargenGlobal"] = totalGral > 0 ? Math.Round((gananciaTotal / totalGral) * 100, 2) : 0m;
            }

            return resumen;
        }

        public static DataTable ObtenerDiferenciacionCanchaCantina(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddSeconds(-1);

            const string query = @"
                SELECT
                    'Cancha de Pádel'                              AS 'Rubro',
                    COUNT(DISTINCT dv.id_venta)                    AS 'Operaciones',
                    COALESCE(SUM(dv.cantidad), 0)                  AS 'Horas / Unidades',
                    COALESCE(SUM(dv.total), 0)                     AS 'Total Facturado ($)',
                    0.00                                           AS 'Costo Mercadería ($)',
                    COALESCE(SUM(dv.total), 0)                     AS 'Ganancia Neta ($)',
                    100.00                                         AS 'Margen (%)'
                FROM detalle_venta dv
                INNER JOIN venta v ON v.id_venta = dv.id_venta
                WHERE v.cancelada = 0
                  AND (v.estado IS NULL OR v.estado <> 'Anulada')
                  AND dv.id_producto = 999
                  AND (v.medio_pago IS NULL OR v.medio_pago <> 'Cuenta Corriente')
                  AND v.fecha >= @desde AND v.fecha <= @hasta
                UNION ALL
                SELECT
                    'Cantina (Productos)'                          AS 'Rubro',
                    COUNT(DISTINCT dv.id_venta)                    AS 'Operaciones',
                    COALESCE(SUM(dv.cantidad), 0)                  AS 'Horas / Unidades',
                    COALESCE(SUM(dv.total), 0)                     AS 'Total Facturado ($)',
                    COALESCE(SUM(dv.total / (1 + COALESCE(cat.porcentaje_ganancia, 20) / 100)), 0) AS 'Costo Mercadería ($)',
                    COALESCE(SUM(dv.total - (dv.total / (1 + COALESCE(cat.porcentaje_ganancia, 20) / 100))), 0) AS 'Ganancia Neta ($)',
                    ROUND(CASE
                        WHEN SUM(dv.total) > 0 THEN
                            (SUM(dv.total - (dv.total / (1 + COALESCE(cat.porcentaje_ganancia, 20) / 100))) / SUM(dv.total)) * 100
                        ELSE 0
                    END, 2)                                        AS 'Margen (%)'
                FROM detalle_venta dv
                INNER JOIN venta v ON v.id_venta = dv.id_venta
                LEFT JOIN producto prod ON prod.id_producto = dv.id_producto
                LEFT JOIN categoria cat ON cat.id_categoria = prod.id_categoria
                WHERE v.cancelada = 0
                  AND (v.estado IS NULL OR v.estado <> 'Anulada')
                  AND dv.id_producto NOT IN (998, 999)
                  AND (v.medio_pago IS NULL OR v.medio_pago <> 'Cuenta Corriente')
                  AND v.fecha >= @desde AND v.fecha <= @hasta;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        public static DataTable ObtenerVentasPorTipoComprobante(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddSeconds(-1);

            const string query = @"
                SELECT v.tipo_comprobante                          AS 'Tipo de Comprobante',
                       COUNT(DISTINCT v.id_venta)                  AS 'Cantidad Ventas',
                       COALESCE(SUM(CASE WHEN dv.id_producto = 999 THEN dv.cantidad ELSE 0 END), 0) AS 'Horas Pádel',
                       COALESCE(SUM(CASE WHEN dv.id_producto = 999 THEN dv.total ELSE 0 END), 0) AS 'Subtotal Cancha ($)',
                       COALESCE(SUM(CASE WHEN dv.id_producto NOT IN (998, 999) THEN dv.total ELSE 0 END), 0) AS 'Subtotal Cantina ($)',
                       COALESCE(SUM(dv.total), 0)                  AS 'Total Facturado ($)',
                       COALESCE(SUM(
                           CASE
                               WHEN dv.id_producto = 999 THEN dv.total
                               WHEN dv.id_producto NOT IN (998, 999) THEN
                                   dv.total - (dv.total / (1 + COALESCE(cat.porcentaje_ganancia, 20) / 100))
                               ELSE 0
                           END
                       ), 0)                                       AS 'Ganancia Estimada ($)'
                FROM venta v
                INNER JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                LEFT JOIN producto prod ON prod.id_producto = dv.id_producto
                LEFT JOIN categoria cat ON cat.id_categoria = prod.id_categoria
                WHERE v.cancelada = 0
                  AND (v.estado IS NULL OR v.estado <> 'Anulada')
                  AND dv.id_producto <> 998
                  AND (v.medio_pago IS NULL OR v.medio_pago <> 'Cuenta Corriente')
                  AND v.fecha >= @desde AND v.fecha <= @hasta
                GROUP BY v.tipo_comprobante
                ORDER BY v.tipo_comprobante;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        public static DataTable ObtenerVentasPorMedioPago(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddSeconds(-1);

            const string query = @"
                SELECT m.medio_pago                                AS 'Medio de Pago',
                       COUNT(DISTINCT v.id_venta)                  AS 'Cantidad Operaciones',
                       COALESCE(SUM(CASE WHEN dv.id_producto = 999 THEN dv.cantidad ELSE 0 END), 0) AS 'Horas Pádel',
                       COALESCE(SUM(CASE WHEN dv.id_producto = 999 THEN dv.total ELSE 0 END), 0) AS 'Total Pádel ($)',
                       COALESCE(SUM(CASE WHEN dv.id_producto NOT IN (998, 999) THEN dv.total ELSE 0 END), 0) AS 'Total Cantina ($)',
                       COALESCE(SUM(dv.total), 0)                  AS 'Total Cobrado ($)',
                       COALESCE(ROUND(
                           (COALESCE(SUM(dv.total), 0) / NULLIF((
                               SELECT SUM(dv2.total)
                               FROM venta v2
                               INNER JOIN detalle_venta dv2 ON dv2.id_venta = v2.id_venta
                               WHERE v2.cancelada = 0
                                 AND (v2.estado IS NULL OR v2.estado <> 'Anulada')
                                 AND dv2.id_producto <> 998
                                 AND (v2.medio_pago IS NULL OR v2.medio_pago <> 'Cuenta Corriente')
                                 AND v2.fecha >= @desde AND v2.fecha <= @hasta
                           ), 0)) * 100, 2
                       ), 0.00)                                    AS 'Participación (%)'
                FROM (
                    SELECT 'Efectivo' AS medio_pago, 1 AS orden
                    UNION ALL SELECT 'Tarjeta de Débito', 2
                    UNION ALL SELECT 'Tarjeta de Crédito', 3
                    UNION ALL SELECT 'Billetera Virtual', 4
                ) m
                LEFT JOIN venta v ON v.medio_pago = m.medio_pago
                                 AND v.cancelada = 0
                                 AND (v.estado IS NULL OR v.estado <> 'Anulada')
                                 AND v.fecha >= @desde AND v.fecha <= @hasta
                LEFT JOIN detalle_venta dv ON dv.id_venta = v.id_venta
                                          AND dv.id_producto <> 998
                GROUP BY m.medio_pago, m.orden
                ORDER BY m.orden ASC;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        // ═══════════════════════════════════════════════════
        //  REPORTES DE COMPRAS
        // ═══════════════════════════════════════════════════

        public static DataTable ObtenerReporteCompras(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date;

            const string query = @"
                SELECT CONCAT('CP-0001-', LPAD(c.id_compra, 8, '0')) AS 'N° Comprobante',
                       c.fecha                                    AS 'Fecha',
                       COALESCE(GROUP_CONCAT(CONCAT(prod.nombre, ' (x', dc.cantidad, ')') SEPARATOR ', '), '-') AS 'Producto',
                       COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido)) AS 'Proveedor',
                       COUNT(dc.id_producto)                      AS 'Cant. Ítems',
                       COALESCE(SUM(dc.cantidad), 0)              AS 'Unidades Compradas',
                       COALESCE(SUM(dc.total), 0)                 AS 'Total Costo ($)',
                       COALESCE(SUM(dc.total * (1 + COALESCE(cat.porcentaje_ganancia, 0) / 100)), 0) AS 'Venta Proyectada ($)',
                       COALESCE(SUM(dc.total * (COALESCE(cat.porcentaje_ganancia, 0) / 100)), 0) AS 'Ganancia Proyectada ($)',
                       ROUND(CASE
                           WHEN SUM(dc.total) > 0 THEN
                               (SUM(dc.total * (COALESCE(cat.porcentaje_ganancia, 0) / 100)) / SUM(dc.total)) * 100
                           ELSE 0
                       END, 2)                                    AS 'Margen (%)',
                       u.nombre_completo                          AS 'Registrado por'
                FROM compra c
                INNER JOIN proveedor prov ON prov.id_proveedor = c.id_proveedor
                INNER JOIN persona p ON p.id_persona = prov.id_persona
                INNER JOIN usuario u ON u.id_usuario = c.id_usuario
                LEFT JOIN detalle_compra dc ON dc.id_compra = c.id_compra
                LEFT JOIN producto prod ON prod.id_producto = dc.id_producto
                LEFT JOIN categoria cat ON cat.id_categoria = prod.id_categoria
                WHERE c.fecha >= @desde AND c.fecha <= @hasta
                GROUP BY c.id_compra, c.fecha, p.razon_social, p.nombre, p.apellido, u.nombre_completo
                ORDER BY c.fecha DESC, c.id_compra DESC;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        public static Dictionary<string, object> ObtenerResumenCompras(DateTime desde, DateTime hasta)
        {
            var resumen = new Dictionary<string, object>
            {
                ["TotalCompras"] = 0m,
                ["CantidadCompras"] = 0,
                ["UnidadesCompradas"] = 0,
                ["TotalVentaProyectada"] = 0m,
                ["GananciaProyectada"] = 0m,
                ["MargenPromedio"] = 0m
            };

            DataTable dt = ObtenerReporteCompras(desde, hasta);
            if (dt.Rows.Count == 0) return resumen;

            int comprasCount = dt.Rows.Count;
            int unidades = 0;
            decimal totalCosto = 0m;
            decimal totalVentaProy = 0m;
            decimal totalGananciaProy = 0m;

            foreach (DataRow r in dt.Rows)
            {
                unidades += Convert.ToInt32(r["Unidades Compradas"]);
                totalCosto += Convert.ToDecimal(r["Total Costo ($)"]);
                totalVentaProy += Convert.ToDecimal(r["Venta Proyectada ($)"]);
                totalGananciaProy += Convert.ToDecimal(r["Ganancia Proyectada ($)"]);
            }

            resumen["CantidadCompras"] = comprasCount;
            resumen["UnidadesCompradas"] = unidades;
            resumen["TotalCompras"] = totalCosto;
            resumen["TotalVentaProyectada"] = totalVentaProy;
            resumen["GananciaProyectada"] = totalGananciaProy;
            resumen["MargenPromedio"] = totalCosto > 0 ? Math.Round((totalGananciaProy / totalCosto) * 100, 2) : 0m;

            return resumen;
        }

        public static DataTable ObtenerDetalleCompras(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date;

            const string query = @"
                SELECT CONCAT('CP-0001-', LPAD(dc.id_compra, 8, '0')) AS 'N° Comprobante',
                       c.fecha                                    AS 'Fecha',
                       prod.nombre                                AS 'Producto',
                       COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido)) AS 'Proveedor',
                       COALESCE(cat.nombre, 'Sin categoría')       AS 'Categoría',
                       dc.cantidad                                AS 'Cantidad',
                       dc.precio_unitario                         AS 'Costo Unit. ($)',
                       dc.total                                   AS 'Subtotal Costo ($)',
                       COALESCE(cat.porcentaje_ganancia, 0)       AS '% Ganancia',
                       ROUND(dc.total * (1 + COALESCE(cat.porcentaje_ganancia, 0) / 100), 2) AS 'Subtotal c/ Ganancia ($)',
                       ROUND(dc.total * (COALESCE(cat.porcentaje_ganancia, 0) / 100), 2)     AS 'Ganancia Estimada ($)'
                FROM detalle_compra dc
                INNER JOIN compra c ON c.id_compra = dc.id_compra
                INNER JOIN proveedor prov ON prov.id_proveedor = c.id_proveedor
                INNER JOIN persona p ON p.id_persona = prov.id_persona
                INNER JOIN producto prod ON prod.id_producto = dc.id_producto
                LEFT JOIN categoria cat ON cat.id_categoria = prod.id_categoria
                WHERE c.fecha >= @desde AND c.fecha <= @hasta
                ORDER BY c.fecha DESC, dc.id_compra DESC;";

            return EjecutarConsulta(query, ("@desde", inicio), ("@hasta", fin));
        }

        // ═══════════════════════════════════════════════════
        //  DETALLES DE PRODUCTO (PARA VENTANA LATERAL)
        // ═══════════════════════════════════════════════════

        public static DataTable ObtenerDetallesProductoPorComprobanteVenta(string numeroComprobante)
        {
            int idVenta = ExtraerIdNumerico(numeroComprobante);

            const string query = @"
                SELECT COALESCE(prod.nombre, dv.descripcion, 'Producto') AS 'Producto',
                       COALESCE(prod.codigo, '-')                        AS 'Codigo',
                       COALESCE(m.nombre, '-')                           AS 'Marca',
                       COALESCE(cat.nombre, 'Servicio')                  AS 'Categoria',
                       COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido), '-') AS 'Proveedor',
                       dv.cantidad                                       AS 'Cant.',
                       dv.precio_unitario                                AS 'Unitario ($)',
                       dv.total                                          AS 'Subtotal ($)',
                       COALESCE(prod.precio_venta, dv.precio_unitario)   AS 'PrecioVentaActual',
                       COALESCE(prod.stock, 0)                           AS 'StockActual',
                       COALESCE(cat.porcentaje_ganancia, 0)              AS 'PorcentajeGanancia',
                       COALESCE(NULLIF(dv.descripcion, ''), NULLIF(prod.descripcion, ''), prod.nombre, '-') AS 'Descripcion'
                FROM detalle_venta dv
                INNER JOIN venta v ON v.id_venta = dv.id_venta
                LEFT JOIN producto prod ON prod.id_producto = dv.id_producto
                LEFT JOIN marca m ON m.id_marca = prod.id_marca
                LEFT JOIN categoria cat ON cat.id_categoria = prod.id_categoria
                LEFT JOIN proveedor prov ON prov.id_proveedor = prod.id_proveedor
                LEFT JOIN persona p ON p.id_persona = prov.id_persona
                WHERE v.numero_comprobante = @num OR v.id_venta = @idVenta;";

            return EjecutarConsulta(query, ("@num", numeroComprobante), ("@idVenta", idVenta));
        }

        public static DataTable ObtenerDetallesProductoPorComprobanteCompra(string numeroComprobante)
        {
            int idCompra = ExtraerIdNumerico(numeroComprobante);

            const string query = @"
                SELECT prod.nombre                                       AS 'Producto',
                       COALESCE(prod.codigo, '-')                        AS 'Codigo',
                       COALESCE(m.nombre, '-')                           AS 'Marca',
                       COALESCE(cat.nombre, 'Sin categoría')             AS 'Categoria',
                       COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido), '-') AS 'Proveedor',
                       dc.cantidad                                       AS 'Cant.',
                       dc.precio_unitario                                AS 'Unitario ($)',
                       dc.total                                          AS 'Subtotal ($)',
                       COALESCE(prod.precio_venta, dc.precio_unitario)   AS 'PrecioVentaActual',
                       COALESCE(prod.stock, 0)                           AS 'StockActual',
                       COALESCE(cat.porcentaje_ganancia, 0)              AS 'PorcentajeGanancia',
                       COALESCE(NULLIF(prod.descripcion, ''), prod.nombre, '-') AS 'Descripcion'
                FROM detalle_compra dc
                INNER JOIN compra c ON c.id_compra = dc.id_compra
                INNER JOIN producto prod ON prod.id_producto = dc.id_producto
                LEFT JOIN marca m ON m.id_marca = prod.id_marca
                LEFT JOIN categoria cat ON cat.id_categoria = prod.id_categoria
                LEFT JOIN proveedor prov ON prov.id_proveedor = c.id_proveedor
                LEFT JOIN persona p ON p.id_persona = prov.id_persona
                WHERE c.id_compra = @idCompra;";

            return EjecutarConsulta(query, ("@idCompra", idCompra));
        }

        private static int ExtraerIdNumerico(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return -1;
            string[] partes = codigo.Split('-');
            string ultima = partes[partes.Length - 1].Trim();
            if (int.TryParse(ultima, out int id))
                return id;
            string digitos = new string(codigo.Where(char.IsDigit).ToArray());
            if (int.TryParse(digitos, out int id2))
                return id2;
            return -1;
        }

        private static DataTable EjecutarConsulta(string query, params (string nombre, object valor)[] parametros)
        {
            using MySqlConnection conn = Conexion.ObtenerConexion();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            foreach (var (nombre, valor) in parametros)
            {
                cmd.Parameters.AddWithValue(nombre, valor);
            }

            DataTable dt = new DataTable();
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}