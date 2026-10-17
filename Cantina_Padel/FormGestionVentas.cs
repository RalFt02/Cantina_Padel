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

            ConfigurarGrilla(gridVentas);
            ConfigurarGrilla(gridDetalle);
            ConfigurarGrilla(gridItems);

            CargarMediosDePago();
            CargarComprobantes();
            CargarClientes();
            CargarProductos();
            ActualizarEstadoCaja();
            CargarVentas();
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
                lblEstadoCaja.Text = $"● Caja abierta (Sesión #{_idSesionCajaActual.Value})";
                lblEstadoCaja.ForeColor = Color.FromArgb(163, 230, 53);
            }
            else
            {
                lblEstadoCaja.Text = "● Sin caja abierta (Se requiere abrir caja para operar)";
                lblEstadoCaja.ForeColor = Color.FromArgb(239, 68, 68);
            }
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

                cmbProducto.DisplayMember = "descripcion";
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
            int stockDisponible = Convert.ToInt32(fila["stock"]);

            if (stockDisponible <= 0)
            {
                MostrarAviso($"El producto '{nombre}' no tiene stock disponible.");
                return;
            }

            // Seleccionar en combo para sincronizar la vista
            cmbProducto.SelectedValue = idProducto;

            // Leer primero la cantidad ingresada en la casilla de cantidad (ej. si puso 5, toma 5)
            int cantAAgregar = 1;
            if (LeerCantidad(out int cantIngresada) && cantIngresada > 0)
            {
                cantAAgregar = cantIngresada;
            }

            // Verificar si ya está en el carrito
            var existente = _lineas.FirstOrDefault(l => l.IdProducto == idProducto);
            if (existente != null)
            {
                if (existente.Cantidad + cantAAgregar > stockDisponible)
                {
                    MostrarAviso($"No hay suficiente stock de '{nombre}'. Stock disponible: {stockDisponible}, en carrito: {existente.Cantidad}, intentás sumar: {cantAAgregar}.");
                    return;
                }
                existente.Cantidad += cantAAgregar;
            }
            else
            {
                if (cantAAgregar > stockDisponible)
                {
                    MostrarAviso($"Stock insuficiente para '{nombre}'. Hay {stockDisponible} unidades disponibles e intentás agregar {cantAAgregar}.");
                    return;
                }

                _lineas.Add(new LineaVenta
                {
                    IdProducto = idProducto,
                    Nombre = nombre,
                    Cantidad = cantAAgregar,
                    PrecioUnitario = precio,
                    StockDisponible = stockDisponible
                });
            }

            // Restablecer la casilla de cantidad a 1 para el próximo escaneo
            txtCantidad.Text = "1";

            SystemSounds.Beep.Play();
            RefrescarItems();
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
        //  CARGA DE VENTAS Y DETALLE
        // ─────────────────────────────────────────────

        private void CargarVentas()
        {
            try
            {
                gridVentas.DataSource = GestorVentas.ObtenerVentas();

                if (gridVentas.Columns.Contains("id_venta"))
                {
                    gridVentas.Columns["id_venta"].HeaderText = "ID";
                    gridVentas.Columns["fecha"].HeaderText = "Fecha";
                    gridVentas.Columns["comprobante"].HeaderText = "Tipo";
                    gridVentas.Columns["numero"].HeaderText = "N° Comp.";
                    gridVentas.Columns["cliente"].HeaderText = "Cliente";
                    gridVentas.Columns["medio_pago"].HeaderText = "Pago";
                    gridVentas.Columns["total"].HeaderText = "Total";
                    gridVentas.Columns["cajero"].HeaderText = "Cajero";

                    gridVentas.Columns["id_venta"].Width = 40;
                    gridVentas.Columns["fecha"].Width = 110;
                    gridVentas.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    gridVentas.Columns["comprobante"].Width = 75;
                    gridVentas.Columns["numero"].Width = 115;
                    gridVentas.Columns["cliente"].Width = 140;
                    gridVentas.Columns["medio_pago"].Width = 90;
                    gridVentas.Columns["total"].Width = 85;
                    gridVentas.Columns["total"].DefaultCellStyle.Format = "C2";
                    gridVentas.Columns["cajero"].Width = 80;
                }

                _idVentaSeleccionada = null;
                gridDetalle.DataSource = null;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar ventas:\n" + ex.Message);
            }
        }

        private void CargarDetalle(int idVenta)
        {
            try
            {
                gridDetalle.DataSource = GestorVentas.ObtenerDetalleVenta(idVenta);

                if (gridDetalle.Columns.Contains("producto"))
                {
                    gridDetalle.Columns["producto"].HeaderText = "Producto / Concepto";
                    gridDetalle.Columns["cantidad"].HeaderText = "Cant.";
                    gridDetalle.Columns["precio_unitario"].HeaderText = "Precio unit.";
                    gridDetalle.Columns["subtotal"].HeaderText = "Subtotal";

                    gridDetalle.Columns["producto"].Width = 230;
                    gridDetalle.Columns["cantidad"].Width = 60;
                    gridDetalle.Columns["precio_unitario"].Width = 110;
                    gridDetalle.Columns["precio_unitario"].DefaultCellStyle.Format = "C2";
                    gridDetalle.Columns["subtotal"].Width = 110;
                    gridDetalle.Columns["subtotal"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar detalle:\n" + ex.Message);
            }
        }

        // ─────────────────────────────────────────────
        //  CARRITO
        // ─────────────────────────────────────────────

        private void RefrescarItems()
        {
            gridItems.DataSource = null;
            gridItems.DataSource = new BindingList<LineaVenta>(_lineas);

            if (gridItems.Columns.Contains("Nombre"))
            {
                gridItems.Columns["IdProducto"].Visible = false;
                gridItems.Columns["StockDisponible"].Visible = false;

                gridItems.Columns["Nombre"].HeaderText = "Producto";
                gridItems.Columns["Cantidad"].HeaderText = "Cant.";
                gridItems.Columns["PrecioUnitario"].HeaderText = "Precio unit.";
                gridItems.Columns["Subtotal"].HeaderText = "Subtotal";

                gridItems.Columns["Nombre"].Width = 240;
                gridItems.Columns["Cantidad"].Width = 60;
                gridItems.Columns["PrecioUnitario"].Width = 100;
                gridItems.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
                gridItems.Columns["Subtotal"].Width = 110;
                gridItems.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
            }

            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = "Total: " + GestorVentas.CalcularTotal(_lineas).ToString("C2");
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

            RefrescarItems();
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
        //  REGISTRAR VENTA
        // ─────────────────────────────────────────────

        private void RegistrarVenta()
        {
            string? medioPago = cmbMedioPago.SelectedItem?.ToString();
            string? tipoComprobante = cmbComprobante.SelectedItem?.ToString() ?? "Ticket";
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

            DialogResult confirmacion = MessageBox.Show(
                $"¿Confirmar venta por {total:C2}?\n" +
                $"Comprobante: {tipoComprobante}\n" +
                $"Cliente: {clienteNombre}\n" +
                $"Medio de Pago: {medioPago}\n" +
                $"Ítems: {_lineas.Count}\n" +
                "Se descontará el stock de los productos vendidos.",
                "Confirmar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                int idVenta = GestorVentas.RegistrarVenta(
                    _idUsuario, idCliente.Value, _idSesionCajaActual.Value, medioPago!, tipoComprobante, _lineas);

                _ultimoComprobante = GestorVentas.ObtenerDatosComprobante(idVenta);

                DialogResult drPost = MessageBox.Show(
                    $"¡Venta N° {idVenta} ({tipoComprobante}) registrada con éxito!\n\n" +
                    "¿Deseás imprimir el comprobante ahora?\n(Hacé clic en 'No' si preferís enviarlo por email o continuar)",
                    "Venta completada",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Information);

                if (drPost == DialogResult.Yes)
                {
                    ComprobanteService.Imprimir(_ultimoComprobante, this);
                }

                LimpiarNuevaVenta();
                CargarVentas();
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
            if (!_idVentaSeleccionada.HasValue)
            {
                if (_ultimoComprobante != null)
                {
                    ComprobanteService.Imprimir(_ultimoComprobante, this);
                    return;
                }
                MostrarAviso("Seleccioná una venta de la lista para imprimir su comprobante.");
                return;
            }

            DatosComprobante datos = GestorVentas.ObtenerDatosComprobante(_idVentaSeleccionada.Value);
            ComprobanteService.Imprimir(datos, this);
        }

        private void btnEnviarMail_Click(object sender, EventArgs e)
        {
            DatosComprobante? datos = null;

            if (_idVentaSeleccionada.HasValue)
            {
                datos = GestorVentas.ObtenerDatosComprobante(_idVentaSeleccionada.Value);
            }
            else if (_ultimoComprobante != null)
            {
                datos = _ultimoComprobante;
            }

            if (datos == null)
            {
                MostrarAviso("Seleccioná una venta de la lista para enviarla por email.");
                return;
            }

            FormEnviarMail f = new FormEnviarMail(datos);
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

            FormCobroCanchas formCanchas = new FormCobroCanchas(_idUsuario);
            formCanchas.ShowDialog(this);
            CargarVentas();
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
            CargarVentas();
        }

        // ─────────────────────────────────────────────
        //  ANULACIÓN DE VENTA
        // ─────────────────────────────────────────────

        private void EliminarVentaSeleccionada()
        {
            if (_idVentaSeleccionada == null)
            {
                MostrarAviso("Seleccioná una venta de la lista para anularla.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"¿Estás seguro de anular la venta N° {_idVentaSeleccionada}?\n" +
                "Se reintegrará el stock de los productos vendidos y se eliminará el registro.",
                "Confirmar anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                GestorVentas.EliminarVenta(_idVentaSeleccionada.Value);

                MessageBox.Show("Venta anulada con éxito.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarVentas();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MostrarError("Error al anular la venta:\n" + ex.Message);
            }
        }

        private void SeleccionarVenta(int indiceFila)
        {
            if (indiceFila < 0 || indiceFila >= gridVentas.Rows.Count) return;
            var fila = gridVentas.Rows[indiceFila];

            _idVentaSeleccionada = Convert.ToInt32(fila.Cells["id_venta"].Value);
            CargarDetalle(_idVentaSeleccionada.Value);
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

        private void gridVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarVenta(e.RowIndex);
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
            LimpiarNuevaVenta();
        }

        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            EliminarVentaSeleccionada();
        }

        private void Volver_Boton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
