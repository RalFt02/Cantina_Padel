using System.Data;
using System.Drawing;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionCompras : Form
    {
        // Ítems de la compra que se está armando (todavía no se guardó en la base).
        private readonly List<LineaCompra> _lineas = new List<LineaCompra>();

        // ID de la compra seleccionada en la grilla de compras (null = ninguna).
        private int? _idCompraSeleccionada;

        // Usuario que registra la compra. Igual que en reservas, todavía no hay
        // sesión global, así que por defecto se usa el usuario 1.
        private readonly int _idUsuario;

        public FormGestionCompras() : this(1)
        {
        }

        public FormGestionCompras(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;

            ConfigurarGrilla(gridCompras);
            ConfigurarGrilla(gridDetalle);
            ConfigurarGrilla(gridItems);

            CargarProveedores();
            CargarCompras();
            RefrescarItems();
        }

        // ─────────────────────────────────────────────
        //  VALIDACIÓN DE TECLADO
        // ─────────────────────────────────────────────
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        // Dígitos, un único separador decimal (punto o coma) y teclas de control.
        private void SoloNumerosDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;
            if (char.IsControl(e.KeyChar)) return;

            if ((e.KeyChar == '.' || e.KeyChar == ',') && !txt.Text.Contains('.') && !txt.Text.Contains(','))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // ─────────────────────────────────────────────
        //  LECTURA DE CONTROLES
        // ─────────────────────────────────────────────

        // Devuelve el valor seleccionado de un combo como int, o null si no hay selección válida.
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

        // Acepta tanto "12.50" como "12,50".
        private bool LeerPrecio(out decimal precio)
        {
            string texto = txtPrecio.Text.Trim().Replace(',', '.');
            return decimal.TryParse(texto, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out precio);
        }

        // ─────────────────────────────────────────────
        //  CARGA DE COMBOS Y GRILLAS
        // ─────────────────────────────────────────────
        private void CargarProveedores()
        {
            try
            {
                DataTable dt = GestorCompras.ObtenerProveedores();
                cmbProveedor.DisplayMember = "nombre_proveedor";
                cmbProveedor.ValueMember = "id_proveedor";
                cmbProveedor.DataSource = dt;
                CargarProductos();
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al cargar proveedores:\n" + ex.Message);
            }
        }

        // Carga los productos del proveedor elegido (cada producto tiene un único proveedor).
        private void CargarProductos()
        {
            try
            {
                int? idProveedor = ObtenerIdSeleccionado(cmbProveedor);
                if (idProveedor == null)
                {
                    cmbProducto.DataSource = null;
                    return;
                }

                DataTable dt = GestorCompras.ObtenerProductosDeProveedor(idProveedor.Value);
                cmbProducto.DisplayMember = "descripcion";
                cmbProducto.ValueMember = "id_producto";
                cmbProducto.DataSource = dt;
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al cargar productos:\n" + ex.Message);
            }
        }

        private void CargarCompras()
        {
            try
            {
                gridCompras.DataSource = GestorCompras.ObtenerCompras();

                gridCompras.Columns["id_compra"].HeaderText = "ID";
                gridCompras.Columns["fecha"].HeaderText = "Fecha";
                gridCompras.Columns["proveedor"].HeaderText = "Proveedor";
                gridCompras.Columns["total"].HeaderText = "Total";

                gridCompras.Columns["id_compra"].Width = 40;
                gridCompras.Columns["fecha"].Width = 130;
                gridCompras.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                gridCompras.Columns["proveedor"].Width = 250;
                gridCompras.Columns["total"].Width = 100;
                gridCompras.Columns["total"].DefaultCellStyle.Format = "C2";

                _idCompraSeleccionada = null;
                gridDetalle.DataSource = null;
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al cargar compras:\n" + ex.Message);
            }
        }

        private void CargarDetalle(int idCompra)
        {
            try
            {
                gridDetalle.DataSource = GestorCompras.ObtenerDetalleCompra(idCompra);

                gridDetalle.Columns["nombre"].HeaderText = "Producto";
                gridDetalle.Columns["cantidad"].HeaderText = "Cant.";
                gridDetalle.Columns["precio_unitario"].HeaderText = "Costo unit.";
                gridDetalle.Columns["subtotal"].HeaderText = "Subtotal";

                gridDetalle.Columns["nombre"].Width = 230;
                gridDetalle.Columns["cantidad"].Width = 60;
                gridDetalle.Columns["precio_unitario"].Width = 100;
                gridDetalle.Columns["precio_unitario"].DefaultCellStyle.Format = "C2";
                gridDetalle.Columns["subtotal"].Width = 100;
                gridDetalle.Columns["subtotal"].DefaultCellStyle.Format = "C2";
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al cargar el detalle:\n" + ex.Message);
            }
        }

        // Redibuja la grilla de ítems de la compra en armado y actualiza el total.
        private void RefrescarItems()
        {
            gridItems.DataSource = null;
            gridItems.DataSource = new System.ComponentModel.BindingList<LineaCompra>(_lineas);

            gridItems.Columns["IdProducto"].Visible = false;
            gridItems.Columns["Nombre"].HeaderText = "Producto";
            gridItems.Columns["Cantidad"].HeaderText = "Cant.";
            gridItems.Columns["PrecioUnitario"].HeaderText = "Costo unit.";
            gridItems.Columns["Subtotal"].HeaderText = "Subtotal";

            gridItems.Columns["Nombre"].Width = 220;
            gridItems.Columns["Cantidad"].Width = 55;
            gridItems.Columns["PrecioUnitario"].Width = 100;
            gridItems.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
            gridItems.Columns["Subtotal"].Width = 100;
            gridItems.Columns["Subtotal"].DefaultCellStyle.Format = "C2";

            ActualizarTotal();

            // Con ítems cargados no se puede cambiar de proveedor (los productos son de uno solo).
            cmbProveedor.Enabled = _lineas.Count == 0;
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = "Total: " + GestorCompras.CalcularTotal(_lineas).ToString("C2");
        }

        // ─────────────────────────────────────────────
        //  ALTA DE COMPRA
        // ─────────────────────────────────────────────

        // Toma producto, cantidad y costo de los controles y lo suma a la lista.
        private void AgregarItem()
        {
            int? idProducto = ObtenerIdSeleccionado(cmbProducto);
            if (idProducto == null)
            {
                MostrarAviso("Seleccioná un producto.");
                return;
            }

            if (!LeerCantidad(out int cantidad))
            {
                MostrarAviso("Ingresá una cantidad entera válida.");
                return;
            }

            if (!LeerPrecio(out decimal precio))
            {
                MostrarAviso("Ingresá un costo unitario válido.");
                return;
            }

            var fila = (DataRowView)cmbProducto.SelectedItem!;
            LineaCompra nueva = new LineaCompra
            {
                IdProducto = idProducto.Value,
                Nombre = fila["nombre"].ToString() ?? "",
                Cantidad = cantidad,
                PrecioUnitario = precio
            };

            string? error = GestorCompras.AgregarLinea(_lineas, nueva);
            if (error != null)
            {
                MostrarAviso(error);
                return;
            }

            RefrescarItems();
            LimpiarCamposItem();
        }

        private void QuitarItem()
        {
            if (gridItems.CurrentRow == null || gridItems.CurrentRow.DataBoundItem is not LineaCompra linea)
            {
                MostrarAviso("Seleccioná un ítem de la lista para quitarlo.");
                return;
            }

            GestorCompras.QuitarLinea(_lineas, linea.IdProducto);
            RefrescarItems();
        }

        private void RegistrarCompra()
        {
            int? idProveedor = ObtenerIdSeleccionado(cmbProveedor);

            string? error = GestorCompras.ValidarCompra(idProveedor ?? 0, _lineas);
            if (error != null)
            {
                MostrarAviso(error);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Registrar la compra por {GestorCompras.CalcularTotal(_lineas):C2}?\nSe va a sumar el stock de los productos.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                int idCompra = GestorCompras.CrearCompra(idProveedor!.Value, _idUsuario, _lineas);

                MessageBox.Show($"Compra N° {idCompra} registrada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarNuevaCompra();
                CargarCompras();
                CargarProductos(); // para que se vea el stock actualizado
            }
            catch (ArgumentException ex)
            {
                MostrarAviso(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MostrarAviso(ex.Message);
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al registrar la compra:\n" + ex.Message);
            }
        }

        // ─────────────────────────────────────────────
        //  ELIMINACIÓN DE COMPRA
        // ─────────────────────────────────────────────
        private void EliminarCompraSeleccionada()
        {
            if (_idCompraSeleccionada == null)
            {
                MostrarAviso("Seleccioná una compra de la lista.");
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Eliminar la compra N° {_idCompraSeleccionada}?\nSe va a descontar del stock lo que había sumado y se borra de la base.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                GestorCompras.EliminarCompra(_idCompraSeleccionada.Value);

                MessageBox.Show("Compra eliminada.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarCompras();
                CargarProductos();
            }
            catch (InvalidOperationException ex)
            {
                MostrarAviso(ex.Message);
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al eliminar la compra:\n" + ex.Message);
            }
        }

        private void SeleccionarCompra(int indiceFila)
        {
            if (indiceFila < 0) return;
            var fila = gridCompras.Rows[indiceFila];

            _idCompraSeleccionada = Convert.ToInt32(fila.Cells["id_compra"].Value);
            CargarDetalle(_idCompraSeleccionada.Value);
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarCamposItem()
        {
            txtCantidad.Text = "";
            txtPrecio.Text = "";
        }

        private void LimpiarNuevaCompra()
        {
            _lineas.Clear();
            LimpiarCamposItem();
            RefrescarItems();
        }

        private void MostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Mismo estilo oscuro que el resto de los formularios.
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

        // ─────────────────────────────────────────────
        //  EVENTOS (solo delegan en los métodos de arriba)
        // ─────────────────────────────────────────────
        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void gridCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarCompra(e.RowIndex);
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
            RegistrarCompra();
        }

        private void Limpiar_Boton_Click(object sender, EventArgs e)
        {
            LimpiarNuevaCompra();
        }

        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            EliminarCompraSeleccionada();
        }

        private void Volver_Boton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
