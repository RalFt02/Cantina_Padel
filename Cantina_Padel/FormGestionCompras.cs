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
            this.BindingContext ??= new BindingContext();
            _idUsuario = idUsuario > 0 ? idUsuario : 1;

            ConfigurarGrilla(gridCompras);
            ConfigurarGrilla(gridDetalle);
            ConfigurarGrilla(gridItems);

            gridCompras.DataBindingComplete += (_, _) => FormatearColumnasCompras();
            gridDetalle.DataBindingComplete += (_, _) => FormatearColumnasDetalle();
            gridItems.DataBindingComplete += (_, _) => FormatearColumnasItems();
            this.Load += (_, _) =>
            {
                FormatearColumnasCompras();
                FormatearColumnasDetalle();
                FormatearColumnasItems();
            };

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
            if (combo.SelectedValue == null || combo.SelectedValue == DBNull.Value)
                return null;
            if (combo.SelectedValue is DataRowView drv)
            {
                if (!string.IsNullOrWhiteSpace(combo.ValueMember) && drv.Row.Table.Columns.Contains(combo.ValueMember) && drv[combo.ValueMember] != DBNull.Value)
                    return Convert.ToInt32(drv[combo.ValueMember]);
                return null;
            }
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
                this.BindingContext ??= new BindingContext();
                gridCompras.AutoGenerateColumns = true;
                gridCompras.BindingContext = this.BindingContext;
                gridCompras.DataSource = GestorCompras.ObtenerCompras();

                FormatearColumnasCompras();

                _idCompraSeleccionada = null;
                gridDetalle.DataSource = null;
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al cargar compras:\n" + ex.Message);
            }
        }

        private void FormatearColumnasCompras()
        {
            if (gridCompras == null || gridCompras.Columns.Count == 0) return;

            if (gridCompras.Columns["id_compra"] is DataGridViewColumn colId)
            {
                colId.HeaderText = "ID";
                colId.Width = 40;
            }
            if (gridCompras.Columns["fecha"] is DataGridViewColumn colFec)
            {
                colFec.HeaderText = "Fecha";
                colFec.Width = 130;
                colFec.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (gridCompras.Columns["proveedor"] is DataGridViewColumn colProv)
            {
                colProv.HeaderText = "Proveedor";
                colProv.Width = 170;
            }
            if (gridCompras.Columns["total"] is DataGridViewColumn colTot)
            {
                colTot.HeaderText = "Total";
                colTot.Width = 100;
                colTot.DefaultCellStyle.Format = "C2";
            }
            if (gridCompras.Columns["total_con_ganancia"] is DataGridViewColumn colTotGan)
            {
                colTotGan.HeaderText = "Total c/ ganancia";
                colTotGan.Width = 130;
                colTotGan.DefaultCellStyle.Format = "C2";
            }
        }

        private void CargarDetalle(int idCompra)
        {
            try
            {
                this.BindingContext ??= new BindingContext();
                gridDetalle.AutoGenerateColumns = true;
                gridDetalle.BindingContext = this.BindingContext;
                gridDetalle.DataSource = GestorCompras.ObtenerDetalleCompra(idCompra);

                FormatearColumnasDetalle();
            }
            catch (MySqlException ex)
            {
                MostrarError("Error al cargar el detalle:\n" + ex.Message);
            }
        }

        private void FormatearColumnasDetalle()
        {
            if (gridDetalle == null || gridDetalle.Columns.Count == 0) return;

            if (gridDetalle.Columns["nombre"] is DataGridViewColumn colNom)
            {
                colNom.HeaderText = "Producto";
                colNom.Width = 170;
            }
            if (gridDetalle.Columns["cantidad"] is DataGridViewColumn colCant)
            {
                colCant.HeaderText = "Cant.";
                colCant.Width = 60;
            }
            if (gridDetalle.Columns["precio_unitario"] is DataGridViewColumn colPre)
            {
                colPre.HeaderText = "Costo unit.";
                colPre.Width = 100;
                colPre.DefaultCellStyle.Format = "C2";
            }
            if (gridDetalle.Columns["subtotal"] is DataGridViewColumn colSub)
            {
                colSub.HeaderText = "Subtotal";
                colSub.Width = 100;
                colSub.DefaultCellStyle.Format = "C2";
            }
            if (gridDetalle.Columns["subtotal_con_ganancia"] is DataGridViewColumn colSubGan)
            {
                colSubGan.HeaderText = "Subt. c/ ganancia";
                colSubGan.Width = 110;
                colSubGan.DefaultCellStyle.Format = "C2";
            }
        }

        // Redibuja la grilla de ítems de la compra en armado y actualiza el total.
        private void RefrescarItems()
        {
            this.BindingContext ??= new BindingContext();
            gridItems.AutoGenerateColumns = true;
            gridItems.BindingContext = this.BindingContext;
            gridItems.DataSource = null;
            gridItems.DataSource = new System.ComponentModel.BindingList<LineaCompra>(_lineas);

            FormatearColumnasItems();
            ActualizarTotal();
        }

        private void FormatearColumnasItems()
        {
            if (gridItems == null || gridItems.Columns.Count == 0) return;

            if (gridItems.Columns["IdProducto"] is DataGridViewColumn colIdP) colIdP.Visible = false;
            if (gridItems.Columns["IdProveedor"] is DataGridViewColumn colIdPr) colIdPr.Visible = false;
            if (gridItems.Columns["PorcentajeGanancia"] is DataGridViewColumn colPg) colPg.Visible = false;

            if (gridItems.Columns["Nombre"] is DataGridViewColumn colNom)
            {
                colNom.HeaderText = "Producto";
                colNom.Width = 150;
            }
            if (gridItems.Columns["Cantidad"] is DataGridViewColumn colCant)
            {
                colCant.HeaderText = "Cant.";
                colCant.Width = 50;
            }
            if (gridItems.Columns["PrecioUnitario"] is DataGridViewColumn colPre)
            {
                colPre.HeaderText = "Costo unit.";
                colPre.Width = 90;
                colPre.DefaultCellStyle.Format = "C2";
            }
            if (gridItems.Columns["Subtotal"] is DataGridViewColumn colSub)
            {
                colSub.HeaderText = "Subtotal";
                colSub.Width = 95;
                colSub.DefaultCellStyle.Format = "C2";
            }
            if (gridItems.Columns["SubtotalConGanancia"] is DataGridViewColumn colSubGan)
            {
                colSubGan.HeaderText = "Subt. c/ ganancia";
                colSubGan.Width = 105;
                colSubGan.DefaultCellStyle.Format = "C2";
            }
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

            if (cmbProducto.SelectedItem is not DataRowView fila)
            {
                MostrarAviso("Seleccioná un producto válido.");
                return;
            }

            LineaCompra nueva = new LineaCompra
            {
                IdProducto = idProducto.Value,
                IdProveedor = fila.Row.Table.Columns.Contains("id_proveedor") && fila["id_proveedor"] != DBNull.Value ? Convert.ToInt32(fila["id_proveedor"]) : 1,
                Nombre = fila.Row.Table.Columns.Contains("nombre") ? (fila["nombre"]?.ToString() ?? "") : "",
                Cantidad = cantidad,
                PrecioUnitario = precio,
                PorcentajeGanancia = fila.Row.Table.Columns.Contains("porcentaje_ganancia") && fila["porcentaje_ganancia"] != DBNull.Value ? Convert.ToDecimal(fila["porcentaje_ganancia"]) : 0m
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
            if (indiceFila < 0 || indiceFila >= gridCompras.Rows.Count) return;
            var fila = gridCompras.Rows[indiceFila];
            if (fila.IsNewRow) return;

            var idVal = gridCompras.Columns.Contains("id_compra") ? fila.Cells["id_compra"]?.Value : (fila.Cells.Count > 0 ? fila.Cells[0]?.Value : null);
            if (idVal == null || idVal == DBNull.Value) return;

            _idCompraSeleccionada = Convert.ToInt32(idVal);
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
            this.BindingContext ??= new BindingContext();
            grid.AutoGenerateColumns = true;
            grid.BindingContext = this.BindingContext;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
