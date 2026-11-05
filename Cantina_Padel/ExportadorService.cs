using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cantina_Padel
{
    /// <summary>
    /// Servicio de exportación a formatos estándar de oficina:
    /// - Excel (.xlsx nativo OpenXML y .csv) con soporte multi-hoja, métricas y formato financiero.
    /// - PDF (.pdf vectorial estándar 1.4) con orientación horizontal, KPIs, encabezados,
    ///   paginación automática de múltiples páginas y desgloses.
    /// No depende de paquetes NuGet externos ni de licencias de terceros.
    /// </summary>
    public static class ExportadorService
    {
        // ═══════════════════════════════════════════════════
        //  EXPORTACIÓN A EXCEL (.XLSX / .CSV)
        // ═══════════════════════════════════════════════════

        public static void ExportarAExcel(string rutaArchivo,
                                          string tituloReporte,
                                          string periodo,
                                          Dictionary<string, string>? kpis,
                                          DataTable tablaPrincipal,
                                          DataTable? tablaSecundaria = null,
                                          string nombreHojaPrincipal = "Reporte Principal",
                                          string? nombreHojaSecundaria = "Resumen")
        {
            if (rutaArchivo.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                ExportarACsv(rutaArchivo, tituloReporte, periodo, kpis, tablaPrincipal);
                return;
            }

            using var fileStream = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None);
            using var archive = new ZipArchive(fileStream, ZipArchiveMode.Create);

            bool tieneHoja2 = tablaSecundaria != null && tablaSecundaria.Rows.Count > 0;
            string hoja1Nombre = LimpiarNombreHoja(nombreHojaPrincipal);
            string hoja2Nombre = LimpiarNombreHoja(nombreHojaSecundaria ?? "Resumen");

            // 1. [Content_Types].xml
            string extraSheetType = tieneHoja2 ? @"<Override PartName=""/xl/worksheets/sheet2.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>" : "";
            string typesXml = string.Format(
                @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types"">
  <Default Extension=""rels"" ContentType=""application/vnd.openxmlformats-package.relationships+xml""/>
  <Default Extension=""xml"" ContentType=""application/xml""/>
  <Override PartName=""/xl/workbook.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml""/>
  <Override PartName=""/xl/worksheets/sheet1.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>
  {0}
  <Override PartName=""/xl/styles.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml""/>
</Types>", extraSheetType);
            EscribirEntradaZip(archive, "[Content_Types].xml", typesXml);

            // 2. _rels/.rels
            const string rootRels = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"" Target=""xl/workbook.xml""/>
</Relationships>";
            EscribirEntradaZip(archive, "_rels/.rels", rootRels);

            // 3. xl/_rels/workbook.xml.rels
            string wbRelsXml;
            if (tieneHoja2)
            {
                wbRelsXml = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet1.xml""/>
  <Relationship Id=""rId2"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet2.xml""/>
  <Relationship Id=""rId3"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles"" Target=""styles.xml""/>
</Relationships>";
            }
            else
            {
                wbRelsXml = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
  <Relationship Id=""rId1"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""worksheets/sheet1.xml""/>
  <Relationship Id=""rId2"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles"" Target=""styles.xml""/>
</Relationships>";
            }
            EscribirEntradaZip(archive, "xl/_rels/workbook.xml.rels", wbRelsXml);

            // 4. xl/workbook.xml
            string extraSheetTag = tieneHoja2 ? string.Format(@"<sheet name=""{0}"" sheetId=""2"" r:id=""rId2""/>", EscaparXml(hoja2Nombre)) : "";
            string wbXml = string.Format(
                @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<workbook xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"" xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships"">
  <sheets>
    <sheet name=""{0}"" sheetId=""1"" r:id=""rId1""/>
    {1}
  </sheets>
</workbook>", EscaparXml(hoja1Nombre), extraSheetTag);
            EscribirEntradaZip(archive, "xl/workbook.xml", wbXml);

            // 5. xl/styles.xml
            string stylesXml = ObtenerStylesXml();
            EscribirEntradaZip(archive, "xl/styles.xml", stylesXml);

            // 6. xl/worksheets/sheet1.xml
            string sheet1Xml = GenerarHojaXml(tituloReporte, periodo, kpis, tablaPrincipal);
            EscribirEntradaZip(archive, "xl/worksheets/sheet1.xml", sheet1Xml);

            // 7. xl/worksheets/sheet2.xml (si existe)
            if (tieneHoja2 && tablaSecundaria != null)
            {
                string sheet2Xml = GenerarHojaXml($"{tituloReporte} - {hoja2Nombre}", periodo, null, tablaSecundaria);
                EscribirEntradaZip(archive, "xl/worksheets/sheet2.xml", sheet2Xml);
            }
        }

        private static string ObtenerStylesXml()
        {
            return @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<styleSheet xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">
  <numFmts count=""2"">
    <numFmt numFmtId=""164"" formatCode=""$#,##0.00""/>
    <numFmt numFmtId=""165"" formatCode=""0.00%""/>
  </numFmts>
  <fonts count=""6"">
    <font><name val=""Segoe UI""/><sz val=""10""/></font>
    <font><b/><name val=""Segoe UI""/><sz val=""14""/><color rgb=""FF0F172A""/></font>
    <font><i/><name val=""Segoe UI""/><sz val=""9""/><color rgb=""FF64748B""/></font>
    <font><b/><name val=""Segoe UI""/><sz val=""10""/><color rgb=""FFFFFFFF""/></font>
    <font><b/><name val=""Segoe UI""/><sz val=""10""/><color rgb=""FF0F172A""/></font>
    <font><b/><name val=""Segoe UI""/><sz val=""11""/><color rgb=""FF15803D""/></font>
  </fonts>
  <fills count=""5"">
    <fill><patternFill patternType=""none""/></fill>
    <fill><patternFill patternType=""gray125""/></fill>
    <fill><patternFill patternType=""solid""><fgColor rgb=""FF1E293B""/></patternFill></fill>
    <fill><patternFill patternType=""solid""><fgColor rgb=""FFF1F5F9""/></patternFill></fill>
    <fill><patternFill patternType=""solid""><fgColor rgb=""FFE2E8F0""/></patternFill></fill>
  </fills>
  <borders count=""3"">
    <border><left/><right/><top/><bottom/></border>
    <border>
      <left style=""thin""><color rgb=""FFCBD5E1""/></left>
      <right style=""thin""><color rgb=""FFCBD5E1""/></right>
      <top style=""thin""><color rgb=""FFCBD5E1""/></top>
      <bottom style=""thin""><color rgb=""FFCBD5E1""/></bottom>
    </border>
    <border>
      <left/><right/>
      <top style=""thin""><color rgb=""FF0F172A""/></top>
      <bottom style=""double""><color rgb=""FF0F172A""/></bottom>
    </border>
  </borders>
  <cellStyleXfs count=""1""><xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""0""/></cellStyleXfs>
  <cellXfs count=""9"">
    <xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""0"" xfId=""0""/>
    <xf numFmtId=""0"" fontId=""1"" fillId=""0"" borderId=""0"" xfId=""0"" applyFont=""1""/>
    <xf numFmtId=""0"" fontId=""2"" fillId=""0"" borderId=""0"" xfId=""0"" applyFont=""1""/>
    <xf numFmtId=""0"" fontId=""3"" fillId=""2"" borderId=""1"" xfId=""0"" applyFont=""1"" applyFill=""1"" applyBorder=""1""/>
    <xf numFmtId=""0"" fontId=""0"" fillId=""0"" borderId=""1"" xfId=""0"" applyBorder=""1""/>
    <xf numFmtId=""164"" fontId=""0"" fillId=""0"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyBorder=""1""/>
    <xf numFmtId=""0"" fontId=""4"" fillId=""3"" borderId=""1"" xfId=""0"" applyFont=""1"" applyFill=""1"" applyBorder=""1""/>
    <xf numFmtId=""164"" fontId=""5"" fillId=""3"" borderId=""1"" xfId=""0"" applyNumberFormat=""1"" applyFont=""1"" applyFill=""1"" applyBorder=""1""/>
    <xf numFmtId=""164"" fontId=""4"" fillId=""4"" borderId=""2"" xfId=""0"" applyNumberFormat=""1"" applyFont=""1"" applyFill=""1"" applyBorder=""1""/>
  </cellXfs>
</styleSheet>";
        }

        private static string GenerarHojaXml(string titulo, string periodo, Dictionary<string, string>? kpis, DataTable tabla)
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>");
            sb.AppendLine(@"<worksheet xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">");

            sb.AppendLine("  <cols>");
            int colCount = Math.Max(tabla.Columns.Count, 4);
            for (int c = 1; c <= colCount; c++)
            {
                double width = 16.0;
                if (c - 1 < tabla.Columns.Count)
                {
                    string colName = tabla.Columns[c - 1].ColumnName;
                    if (colName.Contains("Cliente") || colName.Contains("Proveedor") || colName.Contains("Producto") || colName.Contains("Motivo"))
                        width = 28.0;
                    else if (colName.Contains("Fecha") || colName.Contains("Apertura") || colName.Contains("Cierre"))
                        width = 20.0;
                    else if (colName.Contains("($)") || colName.Contains("Total") || colName.Contains("Monto"))
                        width = 17.0;
                }
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture, @"    <col min=""{0}"" max=""{0}"" width=""{1:0.0}"" customWidth=""1""/>", c, width));
            }
            sb.AppendLine("  </cols>");

            sb.AppendLine("  <sheetData>");
            int rowIndex = 1;

            sb.AppendLine(string.Format(@"    <row r=""{0}"">", rowIndex));
            sb.AppendLine(string.Format(@"      <c r=""A{0}"" s=""1"" t=""inlineStr""><is><t>CANTINA PADEL CLUB - {1}</t></is></c>", rowIndex, EscaparXml(titulo)));
            sb.AppendLine("    </row>");
            rowIndex++;

            sb.AppendLine(string.Format(@"    <row r=""{0}"">", rowIndex));
            sb.AppendLine(string.Format(@"      <c r=""A{0}"" s=""2"" t=""inlineStr""><is><t>Período: {1} | Generado el {2:dd/MM/yyyy HH:mm:ss}</t></is></c>", rowIndex, EscaparXml(periodo), DateTime.Now));
            sb.AppendLine("    </row>");
            rowIndex += 2;

            if (kpis != null && kpis.Count > 0)
            {
                sb.AppendLine(string.Format(@"    <row r=""{0}"">", rowIndex));
                sb.AppendLine(string.Format(@"      <c r=""A{0}"" s=""4"" t=""inlineStr""><is><t>RESUMEN OPERATIVO / INDICADORES CLAVE (KPIs)</t></is></c>", rowIndex));
                sb.AppendLine("    </row>");
                rowIndex++;

                foreach (var kpi in kpis)
                {
                    sb.AppendLine(string.Format(@"    <row r=""{0}"">", rowIndex));
                    sb.AppendLine(string.Format(@"      <c r=""A{0}"" s=""6"" t=""inlineStr""><is><t>{1}</t></is></c>", rowIndex, EscaparXml(kpi.Key)));

                    if (decimal.TryParse(kpi.Value.Replace("$", "").Replace("%", "").Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out decimal valKpi) ||
                        decimal.TryParse(kpi.Value.Replace("$", "").Replace("%", "").Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out valKpi))
                    {
                        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, @"      <c r=""B{0}"" s=""7""><v>{1}</v></c>", rowIndex, valKpi.ToString(CultureInfo.InvariantCulture)));
                    }
                    else
                    {
                        sb.AppendLine(string.Format(@"      <c r=""B{0}"" s=""7"" t=""inlineStr""><is><t>{1}</t></is></c>", rowIndex, EscaparXml(kpi.Value)));
                    }

                    sb.AppendLine("    </row>");
                    rowIndex++;
                }
                rowIndex++;
            }

            int headerRowIndex = rowIndex;
            sb.AppendLine(string.Format(@"    <row r=""{0}"">", headerRowIndex));
            for (int c = 0; c < tabla.Columns.Count; c++)
            {
                string colLetter = ObtenerLetraColumna(c + 1);
                string colHeader = tabla.Columns[c].ColumnName;
                sb.AppendLine(string.Format(@"      <c r=""{0}{1}"" s=""3"" t=""inlineStr""><is><t>{2}</t></is></c>", colLetter, headerRowIndex, EscaparXml(colHeader)));
            }
            sb.AppendLine("    </row>");
            rowIndex++;

            foreach (DataRow row in tabla.Rows)
            {
                sb.AppendLine(string.Format(@"    <row r=""{0}"">", rowIndex));
                for (int c = 0; c < tabla.Columns.Count; c++)
                {
                    string colLetter = ObtenerLetraColumna(c + 1);
                    object val = row[c];
                    string colName = tabla.Columns[c].ColumnName;

                    if (val == DBNull.Value || val == null)
                    {
                        sb.AppendLine(string.Format(@"      <c r=""{0}{1}"" s=""4"" t=""inlineStr""><is><t>-</t></is></c>", colLetter, rowIndex));
                    }
                    else if (EsColumnaMonedaONumero(colName, val))
                    {
                        decimal dVal = Convert.ToDecimal(val);
                        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, @"      <c r=""{0}{1}"" s=""5""><v>{2}</v></c>", colLetter, rowIndex, dVal.ToString(CultureInfo.InvariantCulture)));
                    }
                    else if (val is DateTime dtVal)
                    {
                        sb.AppendLine(string.Format(@"      <c r=""{0}{1}"" s=""4"" t=""inlineStr""><is><t>{2:dd/MM/yyyy HH:mm}</t></is></c>", colLetter, rowIndex, dtVal));
                    }
                    else
                    {
                        string strVal = val.ToString() ?? "";
                        sb.AppendLine(string.Format(@"      <c r=""{0}{1}"" s=""4"" t=""inlineStr""><is><t>{2}</t></is></c>", colLetter, rowIndex, EscaparXml(strVal)));
                    }
                }
                sb.AppendLine("    </row>");
                rowIndex++;
            }

            if (tabla.Rows.Count > 0)
            {
                sb.AppendLine(string.Format(@"    <row r=""{0}"">", rowIndex));
                for (int c = 0; c < tabla.Columns.Count; c++)
                {
                    string colLetter = ObtenerLetraColumna(c + 1);
                    string colName = tabla.Columns[c].ColumnName;

                    if (c == 0)
                    {
                        sb.AppendLine(string.Format(@"      <c r=""{0}{1}"" s=""8"" t=""inlineStr""><is><t>TOTALES GENERALES</t></is></c>", colLetter, rowIndex));
                    }
                    else if (EsColumnaSumable(colName))
                    {
                        decimal totalCol = 0;
                        foreach (DataRow r in tabla.Rows)
                        {
                            if (r[c] != DBNull.Value && decimal.TryParse(r[c].ToString(), out decimal num))
                                totalCol += num;
                        }
                        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, @"      <c r=""{0}{1}"" s=""8""><v>{2}</v></c>", colLetter, rowIndex, totalCol.ToString(CultureInfo.InvariantCulture)));
                    }
                    else
                    {
                        sb.AppendLine(string.Format(@"      <c r=""{0}{1}"" s=""8"" t=""inlineStr""><is><t></t></is></c>", colLetter, rowIndex));
                    }
                }
                sb.AppendLine("    </row>");
            }

            sb.AppendLine("  </sheetData>");
            sb.AppendLine("</worksheet>");
            return sb.ToString();
        }

        private static void ExportarACsv(string rutaArchivo,
                                         string titulo,
                                         string periodo,
                                         Dictionary<string, string>? kpis,
                                         DataTable tabla)
        {
            using var sw = new StreamWriter(rutaArchivo, false, new UTF8Encoding(true));

            sw.WriteLine(string.Format(@"""CANTINA PADEL CLUB - {0}""", titulo));
            sw.WriteLine(string.Format(@"""Período: {0} | Generado: {1:dd/MM/yyyy HH:mm:ss}""", periodo, DateTime.Now));
            sw.WriteLine();

            if (kpis != null && kpis.Count > 0)
            {
                sw.WriteLine(@"""RESUMEN / KPIS"";""VALOR""");
                foreach (var kvp in kpis)
                {
                    sw.WriteLine(string.Format(@"""{0}"";""{1}""", kvp.Key, kvp.Value));
                }
                sw.WriteLine();
            }

            var colHeaders = tabla.Columns.Cast<DataColumn>().Select(c => $"\"{c.ColumnName}\"");
            sw.WriteLine(string.Join(";", colHeaders));

            var arCulture = CultureInfo.GetCultureInfo("es-AR");
            foreach (DataRow row in tabla.Rows)
            {
                var rowVals = tabla.Columns.Cast<DataColumn>().Select(col =>
                {
                    object val = row[col];
                    if (val is null or DBNull) return string.Empty;

                    return val switch
                    {
                        DateTime dt => $"\"{dt:dd/MM/yyyy HH:mm}\"",
                        decimal or double or float => Convert.ToDecimal(val).ToString("N2", arCulture),
                        _ => $"\"{val.ToString()?.Replace("\"", "\"\"")}\""
                    };
                });

                sw.WriteLine(string.Join(";", rowVals));
            }
        }

        public static void ExportarAPdf(string rutaArchivo,
                                        string tituloReporte,
                                        string periodo,
                                        Dictionary<string, string>? kpis,
                                        DataTable tablaPrincipal,
                                        DataTable? tablaSecundaria = null,
                                        string? tituloSecundaria = "Resumen Diferenciado")
        {
            var pdf = new GeneradorPdfDocumento(tituloReporte, periodo, kpis, tablaPrincipal, tablaSecundaria, tituloSecundaria);
            byte[] pdfBytes = pdf.ConstruirBytes();
            File.WriteAllBytes(rutaArchivo, pdfBytes);
        }

        private class GeneradorPdfDocumento
        {
            private const float PAGE_WIDTH = 841.89f;
            private const float PAGE_HEIGHT = 595.28f;
            private const float MARGIN_LEFT = 36f;
            private const float MARGIN_RIGHT = 36f;
            private const float MARGIN_TOP = 36f;
            private const float MARGIN_BOTTOM = 36f;
            private const float CONTENT_WIDTH = PAGE_WIDTH - MARGIN_LEFT - MARGIN_RIGHT;

            private readonly string _titulo;
            private readonly string _periodo;
            private readonly Dictionary<string, string>? _kpis;
            private readonly DataTable _tablaPrincipal;
            private readonly DataTable? _tablaSecundaria;
            private readonly string? _tituloSecundaria;

            private readonly List<string> _paginasStream = new List<string>();
            private StringBuilder _streamActual = new StringBuilder();
            private float _currentY;
            private int _paginaNumero = 1;

            public GeneradorPdfDocumento(string titulo, string periodo, Dictionary<string, string>? kpis,
                                         DataTable tablaPrincipal, DataTable? tablaSecundaria, string? tituloSecundaria)
            {
                _titulo = titulo;
                _periodo = periodo;
                _kpis = kpis;
                _tablaPrincipal = tablaPrincipal;
                _tablaSecundaria = tablaSecundaria;
                _tituloSecundaria = tituloSecundaria;
            }

            public byte[] ConstruirBytes()
            {
                IniciarNuevaPagina(true);
                DibujarEncabezadoPagina1();

                if (_kpis != null && _kpis.Count > 0)
                {
                    DibujarTarjetasKpi();
                }

                DibujarTabla(_tablaPrincipal, true);

                if (_tablaSecundaria != null && _tablaSecundaria.Rows.Count > 0)
                {
                    if (_currentY < 180)
                    {
                        FinalizarPaginaActual();
                        IniciarNuevaPagina(false);
                    }
                    else
                    {
                        _currentY -= 20;
                    }

                    DibujarTituloSeccion(_tituloSecundaria ?? "Diferenciación Cancha vs. Cantina");
                    DibujarTabla(_tablaSecundaria, false);
                }

                FinalizarPaginaActual();
                return EnsamblarEstructuraPdf();
            }

            private void IniciarNuevaPagina(bool esPrimera)
            {
                _streamActual = new StringBuilder();
                _currentY = PAGE_HEIGHT - MARGIN_TOP;

                if (!esPrimera)
                {
                    _streamActual.AppendLine("q");
                    _streamActual.AppendLine("BT /F2 9 Tf 0.12 0.16 0.23 rg");
                    _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", MARGIN_LEFT, _currentY - 10, EscaparPdf(string.Format("CANTINA PADEL CLUB - {0} (Continuación)", _titulo))));
                    _streamActual.AppendLine("BT /F1 8 Tf 0.39 0.45 0.55 rg");
                    _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", PAGE_WIDTH - MARGIN_RIGHT - 180, _currentY - 10, EscaparPdf(string.Format("Período: {0}", _periodo))));
                    _streamActual.AppendLine("Q");

                    _streamActual.AppendLine("0.8 0.84 0.88 RG 0.75 w");
                    _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} m {2:0.##} {1:0.##} l S", MARGIN_LEFT, _currentY - 16, PAGE_WIDTH - MARGIN_RIGHT));

                    _currentY -= 30;
                }
            }

            private void FinalizarPaginaActual()
            {
                _streamActual.AppendLine("q");
                _streamActual.AppendLine("0.8 0.84 0.88 RG 0.5 w");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} m {2:0.##} {1:0.##} l S", MARGIN_LEFT, MARGIN_BOTTOM + 12, PAGE_WIDTH - MARGIN_RIGHT));

                _streamActual.AppendLine("BT /F1 7.5 Tf 0.39 0.45 0.55 rg");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", MARGIN_LEFT, MARGIN_BOTTOM, EscaparPdf("Cantina Padel Club | Sistema de Gestión y Reportes Financieros")));

                string strPagina = string.Format("Página {0}", _paginaNumero);
                _streamActual.AppendLine("BT /F2 7.5 Tf 0.39 0.45 0.55 rg");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", PAGE_WIDTH - MARGIN_RIGHT - 60, MARGIN_BOTTOM, EscaparPdf(strPagina)));
                _streamActual.AppendLine("Q");

                _paginasStream.Add(_streamActual.ToString());
                _paginaNumero++;
            }

            private void DibujarEncabezadoPagina1()
            {
                _streamActual.AppendLine("q");

                _streamActual.AppendLine("BT /F2 16 Tf 0.06 0.09 0.16 rg");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", MARGIN_LEFT, _currentY - 14, EscaparPdf("CANTINA PADEL CLUB")));

                _streamActual.AppendLine("BT /F1 8.5 Tf 0.39 0.45 0.55 rg");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", MARGIN_LEFT, _currentY - 26, EscaparPdf("Complejo Deportivo & Gastronómico | CUIT: 30-71829304-8")));

                _streamActual.AppendLine("BT /F2 12.5 Tf 0.12 0.16 0.23 rg");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", MARGIN_LEFT, _currentY - 44, EscaparPdf(_titulo.ToUpper())));

                _streamActual.AppendLine("BT /F1 8 Tf 0.39 0.45 0.55 rg");
                string meta = string.Format("Período Analizado: {0}  |  Emitido el: {1:dd/MM/yyyy HH:mm:ss}  |  Usuario: Administrador", _periodo, DateTime.Now);
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", MARGIN_LEFT, _currentY - 58, EscaparPdf(meta)));

                _streamActual.AppendLine("0.64 0.90 0.21 rg");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} {2:0.##} 3.5 re f", MARGIN_LEFT, _currentY - 65, CONTENT_WIDTH));

                _streamActual.AppendLine("Q");
                _currentY -= 76;
            }

            private void DibujarTarjetasKpi()
            {
                if (_kpis == null || _kpis.Count == 0) return;

                int cant = Math.Min(_kpis.Count, 6);
                float gap = 8f;
                float cardW = (CONTENT_WIDTH - (cant - 1) * gap) / cant;
                float cardH = 38f;

                int idx = 0;
                foreach (var kpi in _kpis.Take(cant))
                {
                    float x = MARGIN_LEFT + idx * (cardW + gap);
                    float y = _currentY - cardH;

                    _streamActual.AppendLine("q");
                    _streamActual.AppendLine("0.96 0.97 0.98 rg 0.8 0.84 0.88 RG 0.5 w");
                    _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} {2:0.##} {3:0.##} re B", x, y, cardW, cardH));

                    _streamActual.AppendLine("BT /F1 6.5 Tf 0.39 0.45 0.55 rg");
                    string kpiTitle = kpi.Key.Length > 24 ? kpi.Key.Substring(0, 22) + ".." : kpi.Key;
                    _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", x + 6, y + cardH - 12, EscaparPdf(kpiTitle.ToUpper())));

                    _streamActual.AppendLine("BT /F2 9.5 Tf 0.06 0.09 0.16 rg");
                    string kpiVal = kpi.Value.Length > 18 ? kpi.Value.Substring(0, 16) + ".." : kpi.Value;
                    _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", x + 6, y + 8, EscaparPdf(kpiVal)));

                    _streamActual.AppendLine("Q");
                    idx++;
                }

                _currentY -= (cardH + 14);
            }

            private void DibujarTituloSeccion(string titulo)
            {
                _streamActual.AppendLine("q");
                _streamActual.AppendLine("BT /F2 10.5 Tf 0.12 0.16 0.23 rg");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", MARGIN_LEFT, _currentY - 10, EscaparPdf(titulo)));
                _streamActual.AppendLine("0.8 0.84 0.88 RG 0.75 w");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} m {2:0.##} {1:0.##} l S", MARGIN_LEFT, _currentY - 14, PAGE_WIDTH - MARGIN_RIGHT));
                _streamActual.AppendLine("Q");
                _currentY -= 22;
            }

            private void DibujarTabla(DataTable tabla, bool esPrincipal)
            {
                if (tabla.Columns.Count == 0) return;

                int numCols = tabla.Columns.Count;
                float[] colWidths = CalcularAnchosColumnas(tabla);
                float headerHeight = 18f;
                float rowHeight = 14f;

                DibujarEncabezadoColumnas(tabla, colWidths, headerHeight);

                int rowIdx = 0;
                foreach (DataRow row in tabla.Rows)
                {
                    if (_currentY - rowHeight < MARGIN_BOTTOM + 25)
                    {
                        FinalizarPaginaActual();
                        IniciarNuevaPagina(false);
                        DibujarEncabezadoColumnas(tabla, colWidths, headerHeight);
                    }

                    _streamActual.AppendLine("q");
                    float yFila = _currentY - rowHeight;

                    if (rowIdx % 2 == 1)
                    {
                        _streamActual.AppendLine("0.97 0.98 0.99 rg");
                        _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} {2:0.##} {3:0.##} re f", MARGIN_LEFT, yFila, CONTENT_WIDTH, rowHeight));
                    }

                    _streamActual.AppendLine("0.90 0.92 0.95 RG 0.4 w");
                    _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} m {2:0.##} {1:0.##} l S", MARGIN_LEFT, yFila, PAGE_WIDTH - MARGIN_RIGHT));

                    float curX = MARGIN_LEFT;
                    for (int c = 0; c < numCols; c++)
                    {
                        float cw = colWidths[c];
                        string colName = tabla.Columns[c].ColumnName;
                        object val = row[c];
                        string texto = FormatearValorCelda(val, colName);

                        bool esNumero = EsColumnaMonedaONumero(colName, val);

                        int maxChars = Math.Max(4, (int)(cw / 4.8f));
                        if (texto.Length > maxChars) texto = texto.Substring(0, maxChars - 2) + "..";

                        _streamActual.AppendLine("BT /F1 7 Tf 0.12 0.16 0.23 rg");
                        if (esNumero)
                        {
                            float textoAnchoAprox = texto.Length * 4.2f;
                            float posDerecha = Math.Max(curX + 2, curX + cw - textoAnchoAprox - 4);
                            _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", posDerecha, yFila + 4, EscaparPdf(texto)));
                        }
                        else
                        {
                            _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", curX + 4, yFila + 4, EscaparPdf(texto)));
                        }

                        curX += cw;
                    }

                    _streamActual.AppendLine("Q");
                    _currentY -= rowHeight;
                    rowIdx++;
                }

                if (esPrincipal && tabla.Rows.Count > 0)
                {
                    if (_currentY - 18 < MARGIN_BOTTOM + 25)
                    {
                        FinalizarPaginaActual();
                        IniciarNuevaPagina(false);
                    }

                    DibujarFilaTotales(tabla, colWidths, 18f);
                }
            }

            private void DibujarEncabezadoColumnas(DataTable tabla, float[] colWidths, float headerHeight)
            {
                _streamActual.AppendLine("q");
                float yHeader = _currentY - headerHeight;

                _streamActual.AppendLine("0.12 0.16 0.23 rg");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} {2:0.##} {3:0.##} re f", MARGIN_LEFT, yHeader, CONTENT_WIDTH, headerHeight));

                float curX = MARGIN_LEFT;
                for (int c = 0; c < tabla.Columns.Count; c++)
                {
                    float cw = colWidths[c];
                    string colName = tabla.Columns[c].ColumnName;
                    int maxChars = Math.Max(4, (int)(cw / 5.2f));
                    string headerText = colName.Length > maxChars ? colName.Substring(0, maxChars - 2) + ".." : colName;

                    _streamActual.AppendLine("BT /F2 7.5 Tf 1 1 1 rg");
                    _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", curX + 4, yHeader + 5, EscaparPdf(headerText)));
                    curX += cw;
                }

                _streamActual.AppendLine("Q");
                _currentY -= headerHeight;
            }

            private void DibujarFilaTotales(DataTable tabla, float[] colWidths, float rowHeight)
            {
                _streamActual.AppendLine("q");
                float yTot = _currentY - rowHeight;

                _streamActual.AppendLine("0.88 0.91 0.94 rg");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} {2:0.##} {3:0.##} re f", MARGIN_LEFT, yTot, CONTENT_WIDTH, rowHeight));

                _streamActual.AppendLine("0.06 0.09 0.16 RG 1 w");
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} m {2:0.##} {1:0.##} l S", MARGIN_LEFT, yTot + rowHeight, PAGE_WIDTH - MARGIN_RIGHT));
                _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} m {2:0.##} {1:0.##} l S", MARGIN_LEFT, yTot, PAGE_WIDTH - MARGIN_RIGHT));

                float curX = MARGIN_LEFT;
                for (int c = 0; c < tabla.Columns.Count; c++)
                {
                    float cw = colWidths[c];
                    string colName = tabla.Columns[c].ColumnName;

                    if (c == 0)
                    {
                        _streamActual.AppendLine("BT /F2 7.5 Tf 0.06 0.09 0.16 rg");
                        _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", curX + 4, yTot + 5, EscaparPdf("TOTALES")));
                    }
                    else if (EsColumnaSumable(colName))
                    {
                        decimal sum = 0;
                        foreach (DataRow r in tabla.Rows)
                        {
                            if (r[c] != DBNull.Value && decimal.TryParse(r[c].ToString(), out decimal n)) sum += n;
                        }
                        string textTot = string.Format("${0:N2}", sum);
                        float textoAnchoAprox = textTot.Length * 4.6f;
                        float posDer = Math.Max(curX + 2, curX + cw - textoAnchoAprox - 4);

                        _streamActual.AppendLine("BT /F2 7.5 Tf 0.06 0.09 0.16 rg");
                        _streamActual.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:0.##} {1:0.##} Td ({2}) Tj ET", posDer, yTot + 5, EscaparPdf(textTot)));
                    }

                    curX += cw;
                }

                _streamActual.AppendLine("Q");
                _currentY -= rowHeight;
            }

            private float[] CalcularAnchosColumnas(DataTable tabla)
            {
                int numCols = tabla.Columns.Count;
                float[] widths = new float[numCols];
                float totalPeso = 0;
                float[] pesos = new float[numCols];

                for (int i = 0; i < numCols; i++)
                {
                    string col = tabla.Columns[i].ColumnName;
                    if (col.Contains("Cliente") || col.Contains("Proveedor") || col.Contains("Producto") || col.Contains("Motivo"))
                        pesos[i] = 2.2f;
                    else if (col.Contains("Fecha") || col.Contains("Apertura") || col.Contains("Cierre"))
                        pesos[i] = 1.7f;
                    else if (col.Contains("Comprobante") || col.Contains("Medio"))
                        pesos[i] = 1.5f;
                    else if (col.Contains("($)") || col.Contains("Total") || col.Contains("Monto"))
                        pesos[i] = 1.4f;
                    else if (col.Contains("ID") || col.Contains("N°") || col.Contains("Cant"))
                        pesos[i] = 0.9f;
                    else
                        pesos[i] = 1.2f;

                    totalPeso += pesos[i];
                }

                for (int i = 0; i < numCols; i++)
                {
                    widths[i] = (pesos[i] / totalPeso) * CONTENT_WIDTH;
                }

                return widths;
            }

            private string FormatearValorCelda(object val, string colName)
            {
                if (val == DBNull.Value || val == null) return "-";
                if (val is DateTime dt) return dt.ToString("dd/MM/yyyy HH:mm");
                if (colName.Contains("($)") || colName.Contains("Total") || colName.Contains("Monto") || colName.Contains("Costo") || colName.Contains("Ganancia") || colName.Contains("Ingresos"))
                {
                    if (decimal.TryParse(val.ToString(), out decimal d)) return string.Format("${0:N2}", d);
                }
                if (colName.Contains("(%)") || colName.Contains("Margen") || colName.Contains("Participación"))
                {
                    if (decimal.TryParse(val.ToString(), out decimal p)) return string.Format("{0:N2}%", p);
                }
                return val.ToString() ?? "";
            }

            private byte[] EnsamblarEstructuraPdf()
            {
                var body = new List<byte>();
                var offsets = new Dictionary<int, long>();

                void AgregarObjeto(int num, byte[] content)
                {
                    offsets[num] = body.Count;
                    byte[] header = Encoding.Latin1.GetBytes($"{num} 0 obj\n");
                    body.AddRange(header);
                    body.AddRange(content);
                    body.AddRange(Encoding.Latin1.GetBytes("\nendobj\n"));
                }

                // Cabecera PDF con salto de línea
                byte[] pdfHeader = Encoding.Latin1.GetBytes("%PDF-1.4\n");
                body.AddRange(pdfHeader);

                int objNum = 1;
                int catalogObjId = objNum++;
                int pagesObjId = objNum++;
                int fontF1ObjId = objNum++;
                int fontF2ObjId = objNum++;

                int cantPaginas = _paginasStream.Count;
                var pageObjIds = new List<int>();
                var contentObjIds = new List<int>();

                for (int i = 0; i < cantPaginas; i++)
                {
                    pageObjIds.Add(objNum++);
                    contentObjIds.Add(objNum++);
                }

                // Catálogo
                AgregarObjeto(catalogObjId, Encoding.Latin1.GetBytes($"<< /Type /Catalog /Pages {pagesObjId} 0 R >>"));

                // Páginas
                string kidsStr = string.Join(" ", pageObjIds.Select(id => $"{id} 0 R"));
                AgregarObjeto(pagesObjId, Encoding.Latin1.GetBytes($"<< /Type /Pages /Kids [{kidsStr}] /Count {cantPaginas} >>"));

                // Fuentes
                AgregarObjeto(fontF1ObjId, Encoding.Latin1.GetBytes("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>"));
                AgregarObjeto(fontF2ObjId, Encoding.Latin1.GetBytes("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>"));

                // Páginas y Streams de contenido
                for (int i = 0; i < cantPaginas; i++)
                {
                    int pId = pageObjIds[i];
                    int cId = contentObjIds[i];
                    byte[] streamBytes = Encoding.Latin1.GetBytes(_paginasStream[i]);

                    string pageDict = string.Format(CultureInfo.InvariantCulture,
                        "<< /Type /Page /Parent {0} 0 R /MediaBox [0 0 {1:0.##} {2:0.##}] /Resources << /Font << /F1 {3} 0 R /F2 {4} 0 R >> >> /Contents {5} 0 R >>",
                        pagesObjId, PAGE_WIDTH, PAGE_HEIGHT, fontF1ObjId, fontF2ObjId, cId);
                    AgregarObjeto(pId, Encoding.Latin1.GetBytes(pageDict));

                    var fullStream = new List<byte>();
                    fullStream.AddRange(Encoding.Latin1.GetBytes($"<< /Length {streamBytes.Length} >>\nstream\n"));
                    fullStream.AddRange(streamBytes);
                    fullStream.AddRange(Encoding.Latin1.GetBytes("\nendstream"));
                    AgregarObjeto(cId, fullStream.ToArray());
                }

                // Tabla de referencias cruzadas (XREF)
                long xrefPos = body.Count;
                int totalObjetos = offsets.Count;
                var sbXref = new StringBuilder();

                sbXref.Append($"xref\n0 {totalObjetos + 1}\n");
                // Entrada del objeto 0 (exactamente 20 bytes con \r\n: 10 + 1 + 5 + 1 + 1 + 2 = 20)
                sbXref.Append("0000000000 65535 f \r\n");

                for (int i = 1; i <= totalObjetos; i++)
                {
                    sbXref.Append(string.Format(CultureInfo.InvariantCulture, "{0:0000000000} 00000 n \r\n", offsets[i]));
                }

                // Trailer y cierre de archivo
                sbXref.Append($"trailer\n<< /Size {totalObjetos + 1} /Root {catalogObjId} 0 R >>\n");
                sbXref.Append($"startxref\n{xrefPos}\n%%EOF\n");

                body.AddRange(Encoding.Latin1.GetBytes(sbXref.ToString()));
                return body.ToArray();
            }
        }

        private static void EscribirEntradaZip(ZipArchive archive, string nombreEntrada, string contenido)
        {
            ZipArchiveEntry entry = archive.CreateEntry(nombreEntrada, CompressionLevel.Optimal);
            using var writer = new StreamWriter(entry.Open(), Encoding.UTF8);
            writer.Write(contenido);
        }

        private static string ObtenerLetraColumna(int colIndex)
        {
            string resultado = "";
            while (colIndex > 0)
            {
                int mod = (colIndex - 1) % 26;
                resultado = (char)('A' + mod) + resultado;
                colIndex = (colIndex - mod) / 26;
            }
            return resultado;
        }

        private static bool EsColumnaMonedaONumero(string colName, object val)
        {
            if (val is decimal || val is double || val is float || val is int || val is long) return true;
            if (colName.Contains("($)") || colName.Contains("Total") || colName.Contains("Monto") || colName.Contains("Costo") || colName.Contains("Ganancia") || colName.Contains("Subtotal") || colName.Contains("Ingresos"))
                return decimal.TryParse(val?.ToString(), out _);
            return false;
        }

        private static bool EsColumnaSumable(string colName)
        {
            return colName.Contains("($)") || colName.Contains("Total") || colName.Contains("Monto") || colName.Contains("Costo") || colName.Contains("Ganancia") || colName.Contains("Subtotal") || colName.Contains("Ingresos") || colName.Contains("Ventas") || colName.Contains("Retiros");
        }

        private static string EscaparXml(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;

            return texto.Replace("&", "&amp;")
                        .Replace("<", "&lt;")
                        .Replace(">", "&gt;")
                        .Replace("\"", "&quot;")
                        .Replace("'", "&apos;");
        }

        private static string LimpiarNombreHoja(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return "Hoja1";

            const string prohibidos = @":\/?*[]";

            string limpia = new string(nombre.Where(c => !prohibidos.Contains(c)).ToArray()).Trim('\'');

            if (limpia.Length > 31)
            {
                limpia = limpia.Substring(0, 31).TrimEnd('\'');
            }

            return string.IsNullOrWhiteSpace(limpia) ? "Hoja1" : limpia;
        }

        private static string EscaparPdf(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "";
            var sb = new StringBuilder();

            foreach (char c in texto)
            {
                switch (c)
                {
                    case '\\':
                        sb.Append(@"\\");
                        break;
                    case '(':
                        sb.Append(@"\(");
                        break;
                    case ')':
                        sb.Append(@"\)");
                        break;
                    default:
                        if (c >= 32 && c <= 126)
                        {
                            sb.Append(c);
                        }
                        else
                        {
                            int code = (int)c;
                            if (code <= 255)
                            {
                                sb.Append('\\');
                                sb.Append(Convert.ToString(code, 8).PadLeft(3, '0'));
                            }
                            else
                            {
                                sb.Append('?');
                            }
                        }
                        break;
                }
            }

            return sb.ToString();
        }
    }
}
