using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionVentas : Form
    {
        private readonly List<LineaVenta> _lineas = new List<LineaVenta>();
        private int? _idVentaSeleccionada;
        private readonly int _idUsuario;
        private int? _idSesionCajaActual;
        private DatosComprobante? _ultimoComprobante;
        private DataTable? _dtProductos;
        private DataView? _dvProductos;

        public FormGestionVentas() : this(SesionActual.HayUsuario ? SesionActual.IdUsuario : 1)
        {
        }

        public FormGestionVentas(int idUsuario, int? idSesion = null)
        {
            _idUsuario = idUsuario > 0 ? idUsuario : 1;
            _idSesionCajaActual = idSesion ?? GestorVentas.ObtenerIdSesionCajaAbierta(_idUsuario);

            if (!_idSesionCajaActual.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Load += (s, e) => this.Close();
                return;
            }

            InitializeComponent();

            // Solo el administrador puede cancelar ventas; ocultar botón para cajeros
            bool esAdmin = SesionActual.EsAdmin;
            btnCancelarVenta.Visible = esAdmin;
            btnCancelarVenta.Enabled = esAdmin;

            // El botón de impresión manual se quita de Caja (se mantiene el desplegable de comprobante)
            btnImprimir.Visible = false;

            ConfigurarGrilla(gridItems);

            CargarMediosDePago();
            CargarComprobantes();
            CargarClientes();
            CargarProductos();
            ActualizarEstadoCaja();
            RefrescarItems();

            this.Shown += (s, e) => txtBuscador.Focus();
        }

        // ─────────────────────────────────────────────
        //  CONFIGURACIÓN Y CARGA DE COMBOS
        // ─────────────────────────────────────────────

        private void ConfigurarGrilla(DataGridView grid)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 65, 85);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(163, 230, 53);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            grid.DefaultCellStyle.ForeColor = Color.White;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(163, 230, 53);
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void CargarMediosDePago()
        {
            cmbMedioPago.Items.Clear();
            cmbMedioPago.Items.Add("Efectivo");
            cmbMedioPago.Items.Add("Tarjeta de Débito");
            cmbMedioPago.Items.Add("Tarjeta de Crédito");
            cmbMedioPago.Items.Add("Billetera Virtual");
            cmbMedioPago.Items.Add("Cuenta Corriente");
            cmbMedioPago.SelectedIndex = 0;
        }

        private void CargarComprobantes()
        {
            cmbComprobante.Items.Clear();
            cmbComprobante.Items.Add("Ticket");
            cmbComprobante.Items.Add("Factura B");
            cmbComprobante.Items.Add("Factura C");
            cmbComprobante.Items.Add("Factura A");
            cmbComprobante.SelectedIndex = 0;
        }

        private void ActualizarEstadoCaja()
        {
            _idSesionCajaActual = GestorVentas.ObtenerIdSesionCajaAbierta(_idUsuario);

            if (_idSesionCajaActual.HasValue)
            {
                lblEstadoCaja.Text = $"● Caja abierta";
                lblEstadoCaja.ForeColor = Color.FromArgb(163, 230, 53);
            }
            else
            {
                lblEstadoCaja.Text = "● Sin caja abierta (Se requiere abrir caja para operar)";
                lblEstadoCaja.ForeColor = Color.FromArgb(239, 68, 68);
            }
        }

        private void cmbCliente_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarComprobantesSegunCliente();
        }

        private string ObtenerFacturaSeleccionadaOSegunCliente()
        {
            string seleccionado = cmbComprobante.SelectedItem?.ToString() ?? "";
            if (seleccionado.StartsWith("Factura", StringComparison.OrdinalIgnoreCase))
            {
                return seleccionado;
            }

            string condicion = "Consumidor Final";
            string razonSocial = "";

            if (cmbCliente.SelectedItem is DataRowView drv)
            {
                if (drv.Row.Table.Columns.Contains("condicion_fiscal") && drv["condicion_fiscal"] != DBNull.Value)
                    condicion = drv["condicion_fiscal"].ToString() ?? "Consumidor Final";
                if (drv.Row.Table.Columns.Contains("razon_social") && drv["razon_social"] != DBNull.Value)
                    razonSocial = drv["razon_social"].ToString() ?? "";
            }

            int? idCliente = ObtenerIdSeleccionado(cmbCliente);
            if (!idCliente.HasValue || idCliente.Value == 9999)
            {
                condicion = "Consumidor Final";
            }
            else if (string.IsNullOrWhiteSpace(condicion) && !string.IsNullOrWhiteSpace(razonSocial))
            {
                condicion = "Responsable Inscripto";
            }

            if (condicion.Equals("Responsable Inscripto", StringComparison.OrdinalIgnoreCase) || !string.IsNullOrWhiteSpace(razonSocial))
            {
                return "Factura A";
            }

            return "Factura B";
        }

        private void ActualizarComprobantesSegunCliente()
        {
            string condicion = "Consumidor Final";
            string razonSocial = "";

            if (cmbCliente.SelectedItem is DataRowView drv)
            {
                if (drv.Row.Table.Columns.Contains("condicion_fiscal") && drv["condicion_fiscal"] != DBNull.Value)
                    condicion = drv["condicion_fiscal"].ToString() ?? "Consumidor Final";
                if (drv.Row.Table.Columns.Contains("razon_social") && drv["razon_social"] != DBNull.Value)
                    razonSocial = drv["razon_social"].ToString() ?? "";
            }

            int? idCliente = ObtenerIdSeleccionado(cmbCliente);
            if (!idCliente.HasValue || idCliente.Value == 9999)
            {
                condicion = "Consumidor Final";
            }
            else if (string.IsNullOrWhiteSpace(condicion) && !string.IsNullOrWhiteSpace(razonSocial))
            {
                condicion = "Responsable Inscripto";
            }

            string comprobanteActual = cmbComprobante.SelectedItem?.ToString() ?? "";
            cmbComprobante.Items.Clear();

            if (condicion.Equals("Responsable Inscripto", StringComparison.OrdinalIgnoreCase) || !string.IsNullOrWhiteSpace(razonSocial))
            {
                cmbComprobante.Items.Add("Factura A");
                cmbComprobante.Items.Add("Factura B");
                cmbComprobante.Items.Add("Factura C");
                cmbComprobante.Items.Add("Ticket");
            }
            else if (condicion.Equals("Monotributista", StringComparison.OrdinalIgnoreCase))
            {
                cmbComprobante.Items.Add("Factura B");
                cmbComprobante.Items.Add("Factura C");
                cmbComprobante.Items.Add("Factura A");
                cmbComprobante.Items.Add("Ticket");
            }
            else // Consumidor Final
            {
                cmbComprobante.Items.Add("Ticket");
                cmbComprobante.Items.Add("Factura B");
            }

            int idx = cmbComprobante.Items.IndexOf(comprobanteActual);
            cmbComprobante.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private void CargarClientes()
        {
            try
            {
                // Incluye Consumidor Final como primera opción
                DataTable dt = GestorVentas.ObtenerClientes(incluirConsumidorFinal: true);
                cmbCliente.DisplayMember = "nombre_completo";
                cmbCliente.ValueMember = "id_cliente";
                cmbCliente.DataSource = dt;
                if (cmbCliente.Items.Count > 0)
                    cmbCliente.SelectedIndex = 0;
                ActualizarComprobantesSegunCliente();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar clientes:\n" + ex.Message);
            }
        }

        private void CargarProductos()
        {
            try
            {
                _dtProductos = GestorVentas.ObtenerProductos();
                _dvProductos = new DataView(_dtProductos);

                cmbProducto.DisplayMember = "nombre";
                cmbProducto.ValueMember = "id_producto";
                cmbProducto.DataSource = _dvProductos;

                if (cmbProducto.Items.Count > 0)
                {
                    cmbProducto.SelectedIndex = 0;
                    ActualizarPrecioDesdeProducto();
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar productos:\n" + ex.Message);
            }
        }

        // El precio no se ingresa manualmente: se extrae del producto cargado
        private void ActualizarPrecioDesdeProducto()
        {
            if (cmbProducto.SelectedItem is DataRowView fila)
            {
                decimal precio = Convert.ToDecimal(fila["precio_venta"]);
                int stock = Convert.ToInt32(fila["stock"]);

                txtPrecio.Text = precio.ToString("0.00", CultureInfo.InvariantCulture);
                lblStockValor.Text = $"{stock} und";
                lblStockValor.ForeColor = stock > 0 ? Color.FromArgb(163, 230, 53) : Color.FromArgb(239, 68, 68);

                if (string.IsNullOrWhiteSpace(txtCantidad.Text) || txtCantidad.Text == "0")
                {
                    txtCantidad.Text = "1";
                }
            }
        }

        private void cmbProducto_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarPrecioDesdeProducto();
        }

        // ─────────────────────────────────────────────
        //  BUSCADOR POR CÓDIGO DE BARRAS Y NOMBRE (LIKE)
        // ─────────────────────────────────────────────

        private void txtBuscador_TextChanged(object? sender, EventArgs e)
        {
            if (_dtProductos == null || _dvProductos == null) return;

            string query = txtBuscador.Text.Trim();

            // Si el lector de barras ingresó un código numérico completo (ej. EAN 8 a 13)
            if (query.Length >= 8 && query.All(char.IsDigit))
            {
                DataRow? prodEncontrado = _dtProductos.AsEnumerable()
                    .FirstOrDefault(r => r.Table.Columns.Contains("codigo") && string.Equals(r["codigo"]?.ToString(), query, StringComparison.OrdinalIgnoreCase));

                if (prodEncontrado != null)
                {
                    AgregarProductoPorDataRow(prodEncontrado);
                    txtBuscador.Clear();
                    _dvProductos.RowFilter = "";
                    txtBuscador.Focus();
                    return;
                }
            }

            // Filtrado dinámico LIKE en el desplegable de productos (ubicado debajo del buscador)
            if (string.IsNullOrWhiteSpace(query))
            {
                _dvProductos.RowFilter = "";
            }
            else
            {
                // Escapar caracteres para RowFilter
                string safeQuery = query.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]");
                // Filtra para que en el desplegable de productos SOLO aparezcan coincidencias LIKE por nombre o código
                _dvProductos.RowFilter = $"nombre LIKE '%{safeQuery}%' OR codigo LIKE '%{safeQuery}%'";
            }

            // Si hay productos coincidentes en el desplegable, seleccionar el primero y actualizar precio/stock
            if (cmbProducto.Items.Count > 0)
            {
                cmbProducto.SelectedIndex = 0;
                ActualizarPrecioDesdeProducto();
            }
            else
            {
                txtPrecio.Text = "0.00";
                lblStockValor.Text = "0 und";
                lblStockValor.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }

        private void txtBuscador_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string query = txtBuscador.Text.Trim();
                if (string.IsNullOrEmpty(query) || _dtProductos == null) return;

                // Buscar por código de barras primero
                DataRow? prodEncontrado = _dtProductos.AsEnumerable()
                    .FirstOrDefault(r => r.Table.Columns.Contains("codigo") && string.Equals(r["codigo"]?.ToString(), query, StringComparison.OrdinalIgnoreCase));

                // Si no coincide por código pero hay producto seleccionado o filtrado en el desplegable de abajo
                if (prodEncontrado == null && _dvProductos != null && _dvProductos.Count > 0)
                {
                    if (cmbProducto.SelectedItem is DataRowView drv)
                    {
                        prodEncontrado = drv.Row;
                    }
                    else
                    {
                        prodEncontrado = _dvProductos[0].Row;
                    }
                }

                if (prodEncontrado != null)
                {
                    AgregarProductoPorDataRow(prodEncontrado);
                    txtBuscador.Clear();
                    if (_dvProductos != null) _dvProductos.RowFilter = "";
                    txtBuscador.Focus();
                }
                else
                {
                    SystemSounds.Exclamation.Play();
                    MostrarAviso($"Producto '{query}' no encontrado.");
                    txtBuscador.SelectAll();
                }
            }
        }

        private void AgregarProductoPorDataRow(DataRow fila)
        {
            int idProducto = Convert.ToInt32(fila["id_producto"]);
            string nombre = fila["nombre"].ToString() ?? "";
            decimal precio = Convert.ToDecimal(fila["precio_venta"]);
            int stockDisponible = fila.Table.Columns.Contains("stock") && fila["stock"] != DBNull.Value ? Convert.ToInt32(fila["stock"]) : 0;

            // Leer primero la cantidad ingresada en la casilla de cantidad (ej. si puso 5, toma 5)
            int cantAAgregar = 1;
            if (LeerCantidad(out int cantIngresada) && cantIngresada > 0)
            {
                cantAAgregar = cantIngresada;
            }

            // Verificar si ya está en el carrito (se eliminó restricción de stock para permitir stock negativo)
            var existente = _lineas.FirstOrDefault(l => l.IdProducto == idProducto);
            if (existente != null)
            {
                existente.Cantidad += cantAAgregar;
            }
            else
            {
                _lineas.Add(new LineaVenta
                {
                    IdProducto = idProducto,
                    Nombre = nombre,
                    Cantidad = cantAAgregar,
                    PrecioUnitario = precio,
                    StockDisponible = stockDisponible
                });
            }

            // Mostrar el último producto escaneado / seleccionado en el control de abajo
            if (_dvProductos != null) _dvProductos.RowFilter = "";
            cmbProducto.SelectedValue = idProducto;
            ActualizarPrecioDesdeProducto();

            // Restablecer la casilla de cantidad a 1 para el próximo escaneo
            txtCantidad.Text = "1";

            SystemSounds.Beep.Play();
            RefrescarItems(idProducto);
        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private int? ObtenerIdSeleccionado(ComboBox combo)
        {
            if (combo.SelectedValue == null || combo.SelectedValue is DataRowView)
                return null;
            return Convert.ToInt32(combo.SelectedValue);
        }

        private bool LeerCantidad(out int cantidad)
        {
            return int.TryParse(txtCantidad.Text.Trim(), out cantidad);
        }

        // ─────────────────────────────────────────────
        //  CARRITO
        // ─────────────────────────────────────────────

        private void RefrescarItems(int? idProductoSeleccionado = null)
        {
            gridItems.DataSource = null;
            gridItems.DataSource = new BindingList<LineaVenta>(_lineas);

            if (gridItems.Columns.Contains("Nombre"))
            {
                gridItems.Columns["IdProducto"].Visible = false;
                gridItems.Columns["StockDisponible"].Visible = false;
                if (gridItems.Columns.Contains("IdReserva"))
                    gridItems.Columns["IdReserva"].Visible = false;

                gridItems.Columns["Nombre"].HeaderText = "Producto";
                gridItems.Columns["Cantidad"].HeaderText = "Cant.";
                gridItems.Columns["PrecioUnitario"].HeaderText = "Precio unit.";
                gridItems.Columns["Subtotal"].HeaderText = "Subtotal";

                gridItems.Columns["Nombre"].Width = 260;
                gridItems.Columns["Cantidad"].Width = 65;
                gridItems.Columns["PrecioUnitario"].Width = 115;
                gridItems.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
                gridItems.Columns["Subtotal"].Width = 115;
                gridItems.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
            }

            // En los productos agregados, seleccionar automáticamente el último producto escaneado / agregado
            if (gridItems.Rows.Count > 0)
            {
                int filaASeleccionar = -1;
                if (idProductoSeleccionado.HasValue)
                {
                    for (int i = 0; i < gridItems.Rows.Count; i++)
                    {
                        if (gridItems.Rows[i].DataBoundItem is LineaVenta lv && lv.IdProducto == idProductoSeleccionado.Value)
                        {
                            filaASeleccionar = i;
                            break;
                        }
                    }
                }

                if (filaASeleccionar == -1)
                {
                    filaASeleccionar = gridItems.Rows.Count - 1;
                }

                gridItems.ClearSelection();
                gridItems.Rows[filaASeleccionar].Selected = true;
                if (gridItems.Columns.Contains("Nombre"))
                    gridItems.CurrentCell = gridItems.Rows[filaASeleccionar].Cells["Nombre"];
                gridItems.FirstDisplayedScrollingRowIndex = filaASeleccionar;
            }

            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = "TOTAL A PAGAR: " + GestorVentas.CalcularTotal(_lineas).ToString("C2");
        }

        private void AgregarItem()
        {
            int? idProducto = ObtenerIdSeleccionado(cmbProducto);
            if (idProducto == null)
            {
                MostrarAviso("Seleccioná un producto del desplegable.");
                return;
            }

            if (!LeerCantidad(out int cantidad) || cantidad <= 0)
            {
                MostrarAviso("Ingresá una cantidad entera mayor a 0.");
                return;
            }

            var fila = (DataRowView)cmbProducto.SelectedItem!;
            int stockDisponible = Convert.ToInt32(fila["stock"]);
            decimal precio = Convert.ToDecimal(fila["precio_venta"]);

            LineaVenta nueva = new LineaVenta
            {
                IdProducto = idProducto.Value,
                Nombre = fila["nombre"].ToString() ?? "",
                Cantidad = cantidad,
                PrecioUnitario = precio,
                StockDisponible = stockDisponible
            };

            string? error = GestorVentas.AgregarLinea(_lineas, nueva);
            if (error != null)
            {
                MostrarAviso(error);
                return;
            }

            RefrescarItems(idProducto.Value);
            LimpiarCamposItem();
            txtBuscador.Focus();
        }

        private void QuitarItem()
        {
            if (gridItems.CurrentRow == null || gridItems.CurrentRow.DataBoundItem is not LineaVenta linea)
            {
                MostrarAviso("Seleccioná un ítem del carrito para quitarlo.");
                return;
            }

            GestorVentas.QuitarLinea(_lineas, linea.IdProducto);
            RefrescarItems();
        }

        // ─────────────────────────────────────────────
        //  REGISTRAR VENTA (TICKET AUTOMÁTICO + AVISO DE FACTURA)
        // ─────────────────────────────────────────────

        private void RegistrarVenta()
        {
            string? medioPago = cmbMedioPago.SelectedItem?.ToString();
            int? idCliente = ObtenerIdSeleccionado(cmbCliente);

            if (!idCliente.HasValue || idCliente.Value <= 0)
            {
                MostrarAviso("Debe seleccionar un cliente.");
                return;
            }

            // Validación de Cuenta Corriente: no permitida para Consumidor Final
            if (string.Equals(medioPago, "Cuenta Corriente", StringComparison.OrdinalIgnoreCase) && idCliente.Value == 9999)
            {
                MostrarAviso("Las ventas en Cuenta Corriente requieren un cliente registrado (no Consumidor Final).");
                return;
            }

            ActualizarEstadoCaja();
            if (!_idSesionCajaActual.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? error = GestorVentas.ValidarVenta(_lineas, medioPago, idCliente);
            if (error != null)
            {
                MostrarAviso(error);
                return;
            }

            decimal total = GestorVentas.CalcularTotal(_lineas);
            string clienteNombre = cmbCliente.Text;
            string facturaDisponible = ObtenerFacturaSeleccionadaOSegunCliente();

            DialogResult confirmacion = MessageBox.Show(
                $"¿Confirmar venta por {total:C2}?\n" +
                $"Cliente: {clienteNombre}\n" +
                $"Medio de Pago: {medioPago}\n" +
                $"Ítems: {_lineas.Count}\n" +
                "Se descontará el stock de los productos vendidos.",
                "Confirmar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            // Aviso consultando si desea factura o no; si acepta emite la factura correspondiente (A/B/C), si no emite Ticket
            DialogResult deseaFactura = MessageBox.Show(
                $"¿Desea emitir factura ({facturaDisponible}) para esta compra?\n\nSeleccione 'Sí' para emitir {facturaDisponible} o 'No' para emitir Ticket.",
                "Comprobante de Venta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            string tipoComprobante = (deseaFactura == DialogResult.Yes)
                ? facturaDisponible
                : "Ticket";

            try
            {
                int idVenta = GestorVentas.RegistrarVenta(
                    _idUsuario, idCliente.Value, _idSesionCajaActual.Value, medioPago!, tipoComprobante, _lineas);

                _ultimoComprobante = GestorVentas.ObtenerDatosComprobante(idVenta);

                // Generación automática del Ticket / Comprobante al completar la compra
                ComprobanteService.Imprimir(_ultimoComprobante, this);

                LimpiarNuevaVenta();
                CargarProductos();
                txtBuscador.Focus();
            }
            catch (Exception ex)
            {
                MostrarError("Error al registrar la venta:\n" + ex.Message);
            }
        }

        // ─────────────────────────────────────────────
        //  BOTONES DE IMPRESIÓN Y EMAIL
        // ─────────────────────────────────────────────

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (_ultimoComprobante == null)
            {
                MostrarAviso("No hay un comprobante de venta reciente para imprimir.");
                return;
            }

            ComprobanteService.Imprimir(_ultimoComprobante, this);
        }

        private void btnEnviarMail_Click(object sender, EventArgs e)
        {
            if (_ultimoComprobante == null)
            {
                MostrarAviso("No hay un comprobante de venta reciente para enviar por email.");
                return;
            }

            FormEnviarMail f = new FormEnviarMail(_ultimoComprobante);
            f.ShowDialog(this);
        }

        // ─────────────────────────────────────────────
        //  BOTONES MÓDULOS EXTRA: CANCHAS Y CTA CTE
        // ─────────────────────────────────────────────

        private void btnCobroCanchas_Click(object sender, EventArgs e)
        {
            ActualizarEstadoCaja();
            if (!_idSesionCajaActual.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using FormCobroCanchas formCanchas = new FormCobroCanchas(_idUsuario);
            if (formCanchas.ShowDialog(this) == DialogResult.OK && formCanchas.TurnosParaVenta.Count > 0)
            {
                int agregados = 0;
                foreach (var turno in formCanchas.TurnosParaVenta)
                {
                    if (_lineas.Any(l => l.IdReserva == turno.IdReserva))
                    {
                        MostrarAviso($"El turno {turno.Cancha} ({turno.Horario}) ya está cargado en el carrito.");
                        continue;
                    }

                    string nombreItem = $"Alquiler Cancha: {turno.Cancha} ({turno.Horario})";
                    _lineas.Add(new LineaVenta
                    {
                        IdProducto = 999,
                        Nombre = nombreItem,
                        Cantidad = 1,
                        PrecioUnitario = turno.Precio,
                        StockDisponible = 9999,
                        IdReserva = turno.IdReserva
                    });

                    if (turno.IdCliente > 0 && turno.IdCliente != 9999)
                    {
                        cmbCliente.SelectedValue = turno.IdCliente;
                    }

                    agregados++;
                }

                if (agregados > 0)
                {
                    RefrescarItems(999);
                    MostrarAviso($"{agregados} turno(s) de cancha agregado(s) a los productos de la venta. Cobrar seleccionando medio de pago y confirmando la venta.");
                }
            }
        }

        private void btnCuentaCorriente_Click(object sender, EventArgs e)
        {
            ActualizarEstadoCaja();
            if (!_idSesionCajaActual.HasValue)
            {
                MessageBox.Show("Debe abrir una caja para poder operar", "Caja requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? idCliente = ObtenerIdSeleccionado(cmbCliente);
            if (idCliente == 9999) idCliente = null;

            FormCobroCuentaCorriente formCC = new FormCobroCuentaCorriente(_idUsuario, idCliente);
            formCC.ShowDialog(this);
        }

        private void btnCancelarVenta_Click(object sender, EventArgs e)
        {
            AbrirCancelacionDeVentas();
        }

        private void AbrirCancelacionDeVentas()
        {
            if (!SesionActual.EsAdmin)
            {
                MostrarAviso("Solo el administrador puede cancelar ventas.");
                return;
            }

            using FormCancelarVenta formCancelar = new FormCancelarVenta(_idUsuario);
            formCancelar.ShowDialog(this);

            // La cancelación devuelve stock y cambia los totales de caja
            CargarProductos();
            ActualizarEstadoCaja();
        }

        private void LimpiarCamposItem()
        {
            txtCantidad.Text = "1";
            ActualizarPrecioDesdeProducto();
        }

        private void LimpiarNuevaVenta()
        {
            _lineas.Clear();
            LimpiarCamposItem();
            RefrescarItems();
            txtBuscador.Clear();
            if (_dvProductos != null) _dvProductos.RowFilter = "";
            if (cmbCliente.Items.Count > 0) cmbCliente.SelectedIndex = 0;
            if (cmbMedioPago.Items.Count > 0) cmbMedioPago.SelectedIndex = 0;
            if (cmbComprobante.Items.Count > 0) cmbComprobante.SelectedIndex = 0;
        }

        private void MostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Agregar_Boton_Click(object sender, EventArgs e)
        {
            AgregarItem();
        }

        private void Quitar_Boton_Click(object sender, EventArgs e)
        {
            QuitarItem();
        }

        private void Registrar_Boton_Click(object sender, EventArgs e)
        {
            RegistrarVenta();
        }

        private void Limpiar_Boton_Click(object sender, EventArgs e)
        {
            if (_lineas.Count > 0)
            {
                DialogResult confirm = MessageBox.Show(
                    "Hay productos cargados en el carrito.\n¿Está seguro de que desea limpiar la venta actual y vaciar el carrito?",
                    "Confirmar limpieza", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;
            }
            LimpiarNuevaVenta();
        }

        private void Volver_Boton_Click(object sender, EventArgs e)
        {
            if (_lineas.Count > 0)
            {
                MessageBox.Show(
                    "Hay productos cargados en la venta actual. Debe terminar la compra o limpiar el carrito antes de salir.",
                    "Compra en curso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
        }
    }
}
