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

            CargarProductos();
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
        // Carga todos los productos activos (sin filtrar por proveedor).
        private void CargarProductos()
        {
            try
            {
                DataTable dt = GestorCompras.ObtenerProductos();
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
                gridCompras.Columns["total_con_ganancia"].HeaderText = "Total c/ ganancia";

                gridCompras.Columns["id_compra"].Width = 40;
                gridCompras.Columns["fecha"].Width = 130;
                gridCompras.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                gridCompras.Columns["proveedor"].Width = 170;
                gridCompras.Columns["total"].Width = 100;
                gridCompras.Columns["total"].DefaultCellStyle.Format = "C2";
                gridCompras.Columns["total_con_ganancia"].Width = 130;
                gridCompras.Columns["total_con_ganancia"].DefaultCellStyle.Format = "C2";

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
                gridDetalle.Columns["subtotal_con_ganancia"].HeaderText = "Subt. c/ ganancia";

                gridDetalle.Columns["nombre"].Width = 170;
                gridDetalle.Columns["cantidad"].Width = 60;
                gridDetalle.Columns["precio_unitario"].Width = 100;
                gridDetalle.Columns["precio_unitario"].DefaultCellStyle.Format = "C2";
                gridDetalle.Columns["subtotal"].Width = 100;
                gridDetalle.Columns["subtotal"].DefaultCellStyle.Format = "C2";
                gridDetalle.Columns["subtotal_con_ganancia"].Width = 110;
                gridDetalle.Columns["subtotal_con_ganancia"].DefaultCellStyle.Format = "C2";
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
            gridItems.Columns["IdProveedor"].Visible = false;
            gridItems.Columns["PorcentajeGanancia"].Visible = false;
            gridItems.Columns["Nombre"].HeaderText = "Producto";
            gridItems.Columns["Cantidad"].HeaderText = "Cant.";
            gridItems.Columns["PrecioUnitario"].HeaderText = "Costo unit.";
            gridItems.Columns["Subtotal"].HeaderText = "Subtotal";
            gridItems.Columns["SubtotalConGanancia"].HeaderText = "Subt. c/ ganancia";

            gridItems.Columns["Nombre"].Width = 150;
            gridItems.Columns["Cantidad"].Width = 50;
            gridItems.Columns["PrecioUnitario"].Width = 90;
            gridItems.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
            gridItems.Columns["Subtotal"].Width = 95;
            gridItems.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
            gridItems.Columns["SubtotalConGanancia"].Width = 105;
            gridItems.Columns["SubtotalConGanancia"].DefaultCellStyle.Format = "C2";

            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = "Total: " + GestorCompras.CalcularTotal(_lineas).ToString("C2");
            lblTotalGanancia.Text = "Total con ganancia: " + GestorCompras.CalcularTotalConGanancia(_lineas).ToString("C2");
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
                IdProveedor = Convert.ToInt32(fila["id_proveedor"]),
                Nombre = fila["nombre"].ToString() ?? "",
                Cantidad = cantidad,
                PrecioUnitario = precio,
                PorcentajeGanancia = Convert.ToDecimal(fila["porcentaje_ganancia"])
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
            string? error = GestorCompras.ValidarCompra(_lineas);
            if (error != null)
            {
                MostrarAviso(error);
                return;
            }

            int cantidadProveedores = _lineas.Select(l => l.IdProveedor).Distinct().Count();

            var confirm = MessageBox.Show(
                $"¿Registrar la compra por {GestorCompras.CalcularTotal(_lineas):C2}?\n" +
                $"Total con ganancia: {GestorCompras.CalcularTotalConGanancia(_lineas):C2}\n" +
                "Se va a sumar el stock de los productos." +
                (cantidadProveedores > 1
                    ? $"\nLos ítems son de {cantidadProveedores} proveedores: se van a registrar {cantidadProveedores} compras (una por proveedor)."
                    : ""),
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                List<int> ids = GestorCompras.CrearCompra(_idUsuario, _lineas);

                string mensaje = ids.Count == 1
                    ? $"Compra N° {ids[0]} registrada con éxito."
                    : $"Compras N° {string.Join(", ", ids)} registradas con éxito (una por proveedor).";

                MessageBox.Show(mensaje,
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
