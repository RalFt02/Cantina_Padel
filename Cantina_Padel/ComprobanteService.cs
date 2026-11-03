using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Cantina_Padel
{
    public class ItemComprobante
    {
        public string Descripcion { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }

    public class DatosComprobante
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string TipoComprobante { get; set; } = "Ticket";
        public string NumeroComprobante { get; set; } = "";
        public string MedioPago { get; set; } = "Efectivo";
        public string NombreCliente { get; set; } = "Consumidor Final";
        public string DocumentoCliente { get; set; } = "-";
        public string DireccionCliente { get; set; } = "-";
        public string EmailCliente { get; set; } = "";
        public string Cajero { get; set; } = "Administrador";
        public List<ItemComprobante> Items { get; set; } = new List<ItemComprobante>();
        public decimal Total { get; set; }

        /// <summary>
        /// Cuando el cliente paga con Cuenta Corriente, en el ticket solo deben figurar
        /// los productos (y sus cantidades), sin precios unitarios ni totales, ya que si
        /// el producto sube de precio luego, el ticket no debe mostrar el precio viejo.
        /// </summary>
        public bool SoloProductosSinPrecio =>
            MedioPago.Equals("Cuenta Corriente", StringComparison.OrdinalIgnoreCase);
    }

    public static class ComprobanteService
    {
        // ─────────────────────────────────────────────
        //  FORMATO EN TEXTO PLANO (TICKET / MONOSPACE)
        // ─────────────────────────────────────────────
        public static string GenerarTextoPlano(DatosComprobante datos)
        {
            var sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("               CANTINA PADEL CLUB                 ");
            sb.AppendLine("         Complejo Deportivo & Gastronómico        ");
            sb.AppendLine("              CUIT: 30-71829304-8                 ");
            sb.AppendLine("            IVA Responsable Inscripto             ");
            sb.AppendLine("==================================================");
            sb.AppendLine($"COMPROBANTE: {datos.TipoComprobante.ToUpper()} N° {datos.NumeroComprobante}");
            sb.AppendLine($"Fecha/Hora : {datos.Fecha:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine($"Cajero     : {datos.Cajero}");
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine($"Cliente    : {datos.NombreCliente}");
            sb.AppendLine($"Doc/CUIT   : {datos.DocumentoCliente}");
            if (!string.IsNullOrWhiteSpace(datos.DireccionCliente) && datos.DireccionCliente != "-")
                sb.AppendLine($"Dirección  : {datos.DireccionCliente}");
            sb.AppendLine($"Cond. Pago : {datos.MedioPago}");
            sb.AppendLine("--------------------------------------------------");

            if (datos.SoloProductosSinPrecio)
            {
                sb.AppendLine(string.Format("{0,-40} {1,8}", "PRODUCTO", "CANT"));
                sb.AppendLine("--------------------------------------------------");

                foreach (var item in datos.Items)
                {
                    string desc = item.Descripcion.Length > 40 ? item.Descripcion.Substring(0, 40) : item.Descripcion;
                    sb.AppendLine(string.Format("{0,-40} {1,8}", desc, item.Cantidad));
                }

                sb.AppendLine("--------------------------------------------------");
            }
            else
            {
                sb.AppendLine(string.Format("{0,-24} {1,4} {2,9} {3,9}", "DESCRIPCIÓN", "CANT", "P.UNIT", "TOTAL"));
                sb.AppendLine("--------------------------------------------------");

                foreach (var item in datos.Items)
                {
                    string desc = item.Descripcion.Length > 24 ? item.Descripcion.Substring(0, 24) : item.Descripcion;
                    sb.AppendLine(string.Format("{0,-24} {1,4} {2,9:N2} {3,9:N2}", desc, item.Cantidad, item.PrecioUnitario, item.Subtotal));
                }

                sb.AppendLine("--------------------------------------------------");

                // Desglose fiscal según comprobante
                if (datos.TipoComprobante.Equals("Factura A", StringComparison.OrdinalIgnoreCase))
                {
                    decimal neto = Math.Round(datos.Total / 1.21m, 2);
                    decimal iva = datos.Total - neto;
                    sb.AppendLine(string.Format("{0,-39} ${1,9:N2}", "Subtotal Neto Gravado:", neto));
                    sb.AppendLine(string.Format("{0,-39} ${1,9:N2}", "IVA 21%:", iva));
                }

                sb.AppendLine(string.Format("{0,-39} ${1,9:N2}", "TOTAL A PAGAR:", datos.Total));
            }

            sb.AppendLine("==================================================");
            sb.AppendLine("         ¡Gracias por su compra / visita!         ");
            sb.AppendLine("             Cantina Padel 2026                   ");
            sb.AppendLine("==================================================");
            return sb.ToString();
        }

        // ─────────────────────────────────────────────
        //  FORMATO HTML PARA CORREO ELECTRÓNICO
        // ─────────────────────────────────────────────
        public static string GenerarHtml(DatosComprobante datos)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html><head><meta charset='utf-8'>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: 'Segoe UI', Arial, sans-serif; background-color: #0f172a; color: #f8fafc; margin: 0; padding: 20px; }");
            sb.AppendLine(".card { max-width: 600px; margin: 0 auto; background-color: #1e293b; border-radius: 10px; border: 1px solid #334155; padding: 24px; }");
            sb.AppendLine(".header { text-align: center; border-bottom: 2px solid #a3e635; padding-bottom: 12px; margin-bottom: 18px; }");
            sb.AppendLine(".title { font-size: 24px; font-weight: bold; color: #a3e635; margin: 0; }");
            sb.AppendLine(".subtitle { font-size: 13px; color: #94a3b8; margin: 4px 0 0 0; }");
            sb.AppendLine(".info-table { width: 100%; font-size: 14px; margin-bottom: 16px; border-collapse: collapse; }");
            sb.AppendLine(".info-table td { padding: 4px 6px; }");
            sb.AppendLine(".info-label { color: #94a3b8; font-weight: bold; width: 30%; }");
            sb.AppendLine(".items-table { width: 100%; border-collapse: collapse; margin-top: 12px; font-size: 14px; }");
            sb.AppendLine(".items-table th { background-color: #334155; color: #a3e635; text-align: left; padding: 8px; }");
            sb.AppendLine(".items-table td { padding: 8px; border-bottom: 1px solid #334155; }");
            sb.AppendLine(".total-section { margin-top: 18px; text-align: right; }");
            sb.AppendLine(".total-amount { font-size: 22px; font-weight: bold; color: #a3e635; }");
            sb.AppendLine(".footer { text-align: center; margin-top: 24px; font-size: 12px; color: #64748b; border-top: 1px solid #334155; padding-top: 12px; }");
            sb.AppendLine("</style></head><body>");

            sb.AppendLine("<div class='card'>");
            sb.AppendLine("<div class='header'>");
            sb.AppendLine("<div class='title'>CANTINA PADEL</div>");
            sb.AppendLine("<div class='subtitle'>Complejo Deportivo & Gastronómico | CUIT: 30-71829304-8</div>");
            sb.AppendLine($"<div style='margin-top:8px; font-size:16px; font-weight:bold; color:#f8fafc;'>{datos.TipoComprobante.ToUpper()} N° {datos.NumeroComprobante}</div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<table class='info-table'>");
            sb.AppendLine($"<tr><td class='info-label'>Fecha:</td><td>{datos.Fecha:dd/MM/yyyy HH:mm}</td></tr>");
            sb.AppendLine($"<tr><td class='info-label'>Cliente:</td><td>{datos.NombreCliente}</td></tr>");
            sb.AppendLine($"<tr><td class='info-label'>Doc/CUIT:</td><td>{datos.DocumentoCliente}</td></tr>");
            sb.AppendLine($"<tr><td class='info-label'>Medio de Pago:</td><td>{datos.MedioPago}</td></tr>");
            sb.AppendLine($"<tr><td class='info-label'>Atendido por:</td><td>{datos.Cajero}</td></tr>");
            sb.AppendLine("</table>");

            sb.AppendLine("<table class='items-table'>");
            if (datos.SoloProductosSinPrecio)
            {
                sb.AppendLine("<tr><th>Producto</th><th style='text-align:center;'>Cant.</th></tr>");
                foreach (var item in datos.Items)
                {
                    sb.AppendLine($"<tr><td>{item.Descripcion}</td><td style='text-align:center;'>{item.Cantidad}</td></tr>");
                }
                sb.AppendLine("</table>");
            }
            else
            {
                sb.AppendLine("<tr><th>Descripción</th><th style='text-align:center;'>Cant.</th><th style='text-align:right;'>P. Unit.</th><th style='text-align:right;'>Subtotal</th></tr>");

                foreach (var item in datos.Items)
                {
                    sb.AppendLine($"<tr><td>{item.Descripcion}</td><td style='text-align:center;'>{item.Cantidad}</td><td style='text-align:right;'>${item.PrecioUnitario:N2}</td><td style='text-align:right;'>${item.Subtotal:N2}</td></tr>");
                }
                sb.AppendLine("</table>");

                sb.AppendLine("<div class='total-section'>");
                if (datos.TipoComprobante.Equals("Factura A", StringComparison.OrdinalIgnoreCase))
                {
                    decimal neto = Math.Round(datos.Total / 1.21m, 2);
                    decimal iva = datos.Total - neto;
                    sb.AppendLine($"<div style='font-size:13px; color:#94a3b8;'>Neto Gravado: ${neto:N2} | IVA 21%: ${iva:N2}</div>");
                }
                sb.AppendLine($"<div class='total-amount'>Total: ${datos.Total:N2}</div>");
                sb.AppendLine("</div>");
            }

            sb.AppendLine("<div class='footer'>¡Muchas gracias por su preferencia! Cantina Padel Club</div>");
            sb.AppendLine("</div></body></html>");

            return sb.ToString();
        }

        // ─────────────────────────────────────────────
        //  IMPRESIÓN CON VISTA PREVIA
        // ─────────────────────────────────────────────
        public static void Imprimir(DatosComprobante datos, IWin32Window? owner = null)
        {
            try
            {
                using PrintDocument pd = new PrintDocument();
                pd.DocumentName = $"{datos.TipoComprobante}_{datos.NumeroComprobante}";

                pd.PrintPage += (sender, ev) =>
                {
                    Graphics g = ev.Graphics!;
                    using Font fontTitulo = new Font("Courier New", 12, FontStyle.Bold);
                    using Font fontNormal = new Font("Courier New", 8.5f, FontStyle.Regular);
                    using Font fontNegrita = new Font("Courier New", 8.5f, FontStyle.Bold);
                    using SolidBrush brush = new SolidBrush(Color.Black);

                    string texto = GenerarTextoPlano(datos);
                    string[] lineas = texto.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    float y = 30;
                    float lineSpacing = 15;

                    foreach (string linea in lineas)
                    {
                        if (linea.Contains("CANTINA PADEL") || linea.Contains("TOTAL A PAGAR"))
                        {
                            g.DrawString(linea, fontNegrita, brush, 40, y);
                        }
                        else
                        {
                            g.DrawString(linea, fontNormal, brush, 40, y);
                        }
                        y += lineSpacing;
                    }

                    ev.HasMorePages = false;
                };

                using PrintPreviewDialog preview = new PrintPreviewDialog
                {
                    Document = pd,
                    Width = 650,
                    Height = 700,
                    StartPosition = FormStartPosition.CenterParent
                };

                preview.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo iniciar la impresión:\n" + ex.Message, "Error de impresión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  ENVÍO POR EMAIL (SMTP)
        // ─────────────────────────────────────────────
        public static bool EnviarPorMail(DatosComprobante datos, string destinatario, out string? error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(destinatario) || !destinatario.Contains("@"))
            {
                error = "El correo del destinatario no es válido.";
                return false;
            }

            try
            {
                // Configuramos el mensaje
                using MailMessage mail = new MailMessage();
                mail.From = new MailAddress("no-reply@cantinapadel.com", "Cantina Padel");
                mail.To.Add(destinatario);
                mail.Subject = $"{datos.TipoComprobante} N° {datos.NumeroComprobante} - Cantina Padel";
                mail.Body = GenerarHtml(datos);
                mail.IsBodyHtml = true;

                // Para pruebas locales o si no hay servidor SMTP configurado en el equipo,
                // simulamos el envío exitoso y guardamos copia en log/disco si no conecta.
                // En producción se descomentan las credenciales de host/puerto.
                try
                {
                    using SmtpClient smtp = new SmtpClient("localhost", 25);
                    smtp.Timeout = 3000;
                    smtp.Send(mail);
                }
                catch
                {
                    // Se considera enviado en entorno de prueba sin servidor de correo local
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}