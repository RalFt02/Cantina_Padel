using MySql.Data.MySqlClient;
using System.Drawing;

namespace Cantina_Padel
{
    public partial class FormGestionProductos : Form
    {
        // Guarda el ID del producto seleccionado en la grilla (null = alta nueva).
        // No usamos un TextBox visible para esto, así no aparece ni en tiempo de
        // ejecución ni en el lienzo de diseño de Visual Studio.
        private int? _idSeleccionado;
        private decimal _precioBase;
        private decimal _porcentajeCategoriaAnterior;
        private bool _actualizandoFormulario;
        private bool _precioEditadoManualmente;

        public FormGestionProductos()
        {
            InitializeComponent();
            txtPrecio.TextChanged += txtPrecio_TextChanged;
            CargarCombos();
            CargarProductos();
        }

        // Permite dígitos, un único punto decimal y teclas de control (backspace, etc.)
        private void SoloNumerosDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;
            if (char.IsControl(e.KeyChar)) return;

            if (e.KeyChar == '.' && !txt.Text.Contains('.'))
                return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // Permite solo dígitos y teclas de control (backspace, etc.)
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // ─────────────────────────────────────────────
        //  CARGAR COMBOS (MARCA / CATEGORIA / PROVEEDOR)
        // ─────────────────────────────────────────────
        private void CargarCombos()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                // Marca
                var daMarca = new MySqlDataAdapter("SELECT id_marca, nombre FROM marca WHERE activo = 1 ORDER BY nombre", conn);
                var dtMarca = new System.Data.DataTable();
                daMarca.Fill(dtMarca);
                cmbMarca.DataSource    = dtMarca;
                cmbMarca.DisplayMember = "nombre";
                cmbMarca.ValueMember   = "id_marca";

                // Categoria: se muestra solamente el nombre.
                var daCategoria = new MySqlDataAdapter("SELECT id_categoria, nombre, porcentaje_ganancia FROM categoria WHERE activo = 1 ORDER BY nombre", conn);
                var dtCategoria = new System.Data.DataTable();
                daCategoria.Fill(dtCategoria);

                cmbCategoria.DataSource    = dtCategoria;
                cmbCategoria.DisplayMember = "nombre";
                cmbCategoria.ValueMember   = "id_categoria";
                cmbCategoria.SelectedIndexChanged += cmbCategoria_SelectedIndexChanged;
                ActualizarPorcentajeGanancia();

                // Proveedor (nombre a mostrar: razon social si tiene, si no nombre y apellido)
                string queryProv = @"
                    SELECT prov.id_proveedor,
                           COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido)) AS nombre_proveedor
                    FROM proveedor prov
                    JOIN persona p ON p.id_persona = prov.id_persona
                    WHERE p.activo = 1
                    ORDER BY nombre_proveedor";
                var daProv = new MySqlDataAdapter(queryProv, conn);
                var dtProv = new System.Data.DataTable();
                daProv.Fill(dtProv);
                cmbProveedor.DataSource    = dtProv;
                cmbProveedor.DisplayMember = "nombre_proveedor";
                cmbProveedor.ValueMember   = "id_proveedor";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar los combos:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  CARGAR GRILLA
        // ─────────────────────────────────────────────
        private void CargarProductos()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = @"
                    SELECT p.id_producto,
                           p.codigo,
                           p.nombre,
                           p.descripcion,
                           p.precio_venta,
                           p.stock,
                           m.nombre AS marca,
                           c.nombre AS categoria,
                           COALESCE(NULLIF(per.razon_social, ''), CONCAT(per.nombre, ' ', per.apellido)) AS proveedor,
                           p.activo,
                           p.id_marca,
                           p.id_categoria,
                           p.id_proveedor
                    FROM producto p
                    JOIN marca m ON m.id_marca = p.id_marca
                    JOIN categoria c ON c.id_categoria = p.id_categoria
                    JOIN proveedor prov ON prov.id_proveedor = p.id_proveedor
                    JOIN persona per ON per.id_persona = prov.id_persona
                    ORDER BY p.id_producto";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                gridProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridProductos.DataSource = dt;

                // Las columnas se auto-generan recién acá; a veces no heredan bien
                // el ForeColor blanco definido en el diseñador, así que lo forzamos
                // columna por columna para que el texto no se vea negro.
                gridProductos.RowsDefaultCellStyle.ForeColor = Color.White;
                gridProductos.RowsDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
                foreach (DataGridViewColumn col in gridProductos.Columns)
                {
                    col.DefaultCellStyle.ForeColor = Color.White;
                    col.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
                    col.DefaultCellStyle.SelectionForeColor = Color.Black;
                    col.DefaultCellStyle.SelectionBackColor = Color.FromArgb(163, 230, 53);
                }

                gridProductos.Columns["id_producto"].HeaderText   = "ID";
                gridProductos.Columns["codigo"].HeaderText         = "Código";
                gridProductos.Columns["nombre"].HeaderText         = "Nombre";
                gridProductos.Columns["descripcion"].HeaderText    = "Descripción";
                gridProductos.Columns["precio_venta"].HeaderText   = "Precio";
                gridProductos.Columns["stock"].HeaderText          = "Stock";
                gridProductos.Columns["marca"].HeaderText          = "Marca";
                gridProductos.Columns["categoria"].HeaderText      = "Categoría";
                gridProductos.Columns["proveedor"].HeaderText      = "Proveedor";
                gridProductos.Columns["activo"].HeaderText         = "Activo";

                gridProductos.Columns["id_producto"].Width  = 40;
                gridProductos.Columns["codigo"].Width         = 110;
                gridProductos.Columns["nombre"].Width         = 130;
                gridProductos.Columns["descripcion"].Width    = 160;
                gridProductos.Columns["precio_venta"].Width   = 90;
                gridProductos.Columns["precio_venta"].DefaultCellStyle.Format = "C2";
                gridProductos.Columns["stock"].Width           = 60;
                gridProductos.Columns["marca"].Width            = 100;
                gridProductos.Columns["categoria"].Width        = 100;
                gridProductos.Columns["proveedor"].Width        = 130;
                gridProductos.Columns["activo"].Width            = 55;

                // Los IDs de FK viajan ocultos, solo se usan para preseleccionar los combos al editar
                gridProductos.Columns["id_marca"].Visible     = false;
                gridProductos.Columns["id_categoria"].Visible = false;
                gridProductos.Columns["id_proveedor"].Visible = false;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar productos:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal nuevoPorcentaje = ObtenerPorcentajeGananciaCategoria();

            // Si se está editando un producto y el usuario no modificó manualmente
            // el precio, recuperamos el precio base desde el precio de venta actual
            // usando el porcentaje anterior. Así, cambiar de categoría no acumula
            // porcentajes sobre porcentajes.
            if (!_actualizandoFormulario)
            {
                if (_precioEditadoManualmente)
                {
                    // El usuario acaba de escribir un precio base.
                    _precioBase = ObtenerPrecioMostrado();
                }
                else
                {
                    // El precio mostrado ya incluye el porcentaje anterior.
                    // Lo revertimos para obtener el precio base antes de aplicar
                    // el porcentaje de la nueva categoría.
                    decimal factorAnterior = 1m + (_porcentajeCategoriaAnterior / 100m);
                    if (factorAnterior > 0m)
                    {
                        _precioBase = ObtenerPrecioMostrado() / factorAnterior;
                    }
                }

                MostrarPrecioConGanancia(_precioBase, nuevoPorcentaje);
                _precioEditadoManualmente = false;
            }

            _porcentajeCategoriaAnterior = nuevoPorcentaje;
            ActualizarPorcentajeGanancia();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            if (!_actualizandoFormulario)
            {
                _precioEditadoManualmente = true;
            }
        }

        private decimal ObtenerPrecioMostrado()
        {
            return decimal.TryParse(txtPrecio.Text, out decimal precio) ? precio : 0m;
        }

        private void MostrarPrecioConGanancia(decimal precioBase, decimal porcentajeGanancia)
        {
            _actualizandoFormulario = true;
            txtPrecio.Text = (precioBase + (precioBase * porcentajeGanancia / 100m)).ToString("0.00");
            _actualizandoFormulario = false;
        }

        private void ActualizarPorcentajeGanancia()
        {
            textBox1.Text = ObtenerPorcentajeGananciaCategoria().ToString("0.##");
        }

        private decimal ObtenerPorcentajeGananciaCategoria()
        {
            if (cmbCategoria.SelectedItem is System.Data.DataRowView fila &&
                fila["porcentaje_ganancia"] != DBNull.Value)
            {
                return Convert.ToDecimal(fila["porcentaje_ganancia"]);
            }

            return 0m;
        }

        // ─────────────────────────────────────────────
        //  BOTÓN NUEVO
        // ─────────────────────────────────────────────
        private void Nuevo_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Guardar_Boton.Text = "Crear Producto";
        }

        // ─────────────────────────────────────────────
        //  BOTÓN GUARDAR (CREAR / EDITAR)
        // ─────────────────────────────────────────────
        private void Guardar_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                cmbMarca.SelectedValue == null ||
                cmbCategoria.SelectedValue == null ||
                cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Completá Nombre, Marca, Categoría y Proveedor (son obligatorios).",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio < 0)
            {
                MessageBox.Show("El precio de venta debe ser un número válido.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("El stock debe ser un número entero válido.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal porcentajeGanancia = ObtenerPorcentajeGananciaCategoria();
            decimal precioBase = _precioEditadoManualmente ? precio : _precioBase;

            string codigoIngresado = txtCodigo.Text.Trim();
            if (!string.IsNullOrWhiteSpace(codigoIngresado) &&
                (codigoIngresado.Length < 6 || codigoIngresado.Length > 13 || !codigoIngresado.All(char.IsDigit)))
            {
                MessageBox.Show("El código de barras debe contener entre 6 y 13 dígitos.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esNuevo = _idSeleccionado == null;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();
                using MySqlTransaction tx = conn.BeginTransaction();

                if (esNuevo)
                    CrearProducto(conn, tx, precioBase, stock, porcentajeGanancia, codigoIngresado);
                else
                    EditarProducto(conn, tx, precioBase, stock, porcentajeGanancia, codigoIngresado);

                tx.Commit();
                MessageBox.Show("Operación realizada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductos();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearProducto(MySqlConnection conn, MySqlTransaction tx, decimal precio, int stock, decimal porcentajeGanancia, string codigoIngresado)
        {
            string codigo = string.IsNullOrWhiteSpace(codigoIngresado)
                ? GenerarCodigoEan13Unico(conn, tx)
                : codigoIngresado;

            string insertProducto = @"
                INSERT INTO producto (codigo, nombre, descripcion, precio_venta, stock, activo, id_marca, id_categoria, id_proveedor)
                VALUES (@cod, @n, @desc, @p, @s, @a, @idm, @idc, @idp)";

            using MySqlCommand cmd = new MySqlCommand(insertProducto, conn, tx);
            cmd.Parameters.AddWithValue("@cod",  codigo);
            cmd.Parameters.AddWithValue("@n",    txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@desc", string.IsNullOrWhiteSpace(txtDescripcion.Text) ? DBNull.Value : txtDescripcion.Text.Trim());
            cmd.Parameters.AddWithValue("@p",    precio + (precio / 100 * porcentajeGanancia));
            cmd.Parameters.AddWithValue("@s",    stock);
            cmd.Parameters.AddWithValue("@a",    chkActivo.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@idm",  cmbMarca.SelectedValue);
            cmd.Parameters.AddWithValue("@idc",  cmbCategoria.SelectedValue);
            cmd.Parameters.AddWithValue("@idp",  cmbProveedor.SelectedValue);
            cmd.ExecuteNonQuery();

            txtCodigo.Text = codigo;
        }

        // ─────────────────────────────────────────────
        //  Generador de código de barras (EAN-13 válido)
        // ─────────────────────────────────────────────
        // Arma un código de 13 dígitos: prefijo 779 (rango GS1 Argentina) +
        // 9 dígitos aleatorios + dígito verificador calculado. Verifica contra
        // la base que no exista otro producto con el mismo código antes de
        // devolverlo (reintenta si hay colisión, algo prácticamente imposible
        // con 9 dígitos aleatorios, pero mejor curarse en salud).
        private string GenerarCodigoEan13Unico(MySqlConnection conn, MySqlTransaction tx)
        {
            var rnd = new Random();

            while (true)
            {
                string cuerpo = "779" + rnd.Next(0, 999_999_999).ToString("D9");

                int suma = 0;
                for (int i = 0; i < cuerpo.Length; i++)
                {
                    int digito = cuerpo[i] - '0';
                    suma += (i % 2 == 0) ? digito : digito * 3;
                }
                int verificador = (10 - (suma % 10)) % 10;
                string codigo = cuerpo + verificador;

                string check = "SELECT COUNT(*) FROM producto WHERE codigo = @c";
                using MySqlCommand cmd = new MySqlCommand(check, conn, tx);
                cmd.Parameters.AddWithValue("@c", codigo);
                long count = (long)cmd.ExecuteScalar()!;

                if (count == 0) return codigo;
            }
        }

        private void EditarProducto(MySqlConnection conn, MySqlTransaction tx, decimal precio, int stock, decimal porcentajeGanancia, string codigoIngresado)
        {
            int id = _idSeleccionado!.Value;

            string updateProducto = @"
                UPDATE producto
                SET codigo        = @cod,
                    nombre        = @n,
                    descripcion   = @desc,
                    precio_venta  = @p,
                    stock         = @s,
                    activo        = @a,
                    id_marca      = @idm,
                    id_categoria  = @idc,
                    id_proveedor  = @idp
                WHERE id_producto = @id";

            using MySqlCommand cmd = new MySqlCommand(updateProducto, conn, tx);
            cmd.Parameters.AddWithValue("@cod",  codigoIngresado);
            cmd.Parameters.AddWithValue("@n",    txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@desc", string.IsNullOrWhiteSpace(txtDescripcion.Text) ? DBNull.Value : txtDescripcion.Text.Trim());
            cmd.Parameters.AddWithValue("@p",    precio + (precio * porcentajeGanancia / 100m));
            cmd.Parameters.AddWithValue("@s",    stock);
            cmd.Parameters.AddWithValue("@a",    chkActivo.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@idm",  cmbMarca.SelectedValue);
            cmd.Parameters.AddWithValue("@idc",  cmbCategoria.SelectedValue);
            cmd.Parameters.AddWithValue("@idp",  cmbProveedor.SelectedValue);
            cmd.Parameters.AddWithValue("@id",   id);
            cmd.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────
        //  BOTÓN ELIMINAR (BAJA LÓGICA)
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un producto de la lista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Desactivar el producto '{txtNombre.Text}'?\n(No se eliminará permanentemente)",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "UPDATE producto SET activo = 0 WHERE id_producto = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Producto desactivado.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductos();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  SELECCIONAR FILA EN GRILLA
        // ─────────────────────────────────────────────
        private void gridProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = gridProductos.Rows[e.RowIndex];

            _idSeleccionado      = Convert.ToInt32(row.Cells["id_producto"].Value);
            txtCodigo.Text      = row.Cells["codigo"].Value?.ToString() ?? "";
            txtNombre.Text      = row.Cells["nombre"].Value?.ToString() ?? "";
            txtDescripcion.Text = row.Cells["descripcion"].Value?.ToString() ?? "";

            decimal precioVenta = row.Cells["precio_venta"].Value != null
                ? Convert.ToDecimal(row.Cells["precio_venta"].Value)
                : 0m;
            txtStock.Text = row.Cells["stock"].Value?.ToString() ?? "0";

            cmbMarca.SelectedValue     = row.Cells["id_marca"].Value;

            _actualizandoFormulario = true;
            cmbCategoria.SelectedValue = row.Cells["id_categoria"].Value;
            _actualizandoFormulario = false;

            _porcentajeCategoriaAnterior = ObtenerPorcentajeGananciaCategoria();
            decimal factor = 1m + (_porcentajeCategoriaAnterior / 100m);
            _precioBase = factor > 0m ? precioVenta / factor : precioVenta;
            _precioEditadoManualmente = false;
            MostrarPrecioConGanancia(_precioBase, _porcentajeCategoriaAnterior);
            ActualizarPorcentajeGanancia();
            cmbProveedor.SelectedValue = row.Cells["id_proveedor"].Value;

            chkActivo.Checked = Convert.ToBoolean(row.Cells["activo"].Value);
            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  BUSCAR
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridProductos.DataSource is System.Data.DataTable dt)
            {
                string filtro = txtBuscar.Text.Replace("'", "''");
                dt.DefaultView.RowFilter =
                    $"nombre LIKE '%{filtro}%' OR codigo LIKE '%{filtro}%' OR marca LIKE '%{filtro}%' OR categoria LIKE '%{filtro}%' OR proveedor LIKE '%{filtro}%'";
            }
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            _actualizandoFormulario = true;
            _idSeleccionado     = null;
            _precioBase         = 0m;
            _precioEditadoManualmente = false;
            txtCodigo.Text      = "";
            txtNombre.Text      = "";
            txtDescripcion.Text = "";
            txtPrecio.Text      = "0.00";
            txtStock.Text       = "0";
            if (cmbMarca.Items.Count > 0)     cmbMarca.SelectedIndex     = 0;
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;
            chkActivo.Checked   = true;
            _actualizandoFormulario = false;
            _porcentajeCategoriaAnterior = ObtenerPorcentajeGananciaCategoria();
            ActualizarPorcentajeGanancia();
            Guardar_Boton.Text  = "Crear Producto";
        }

        private void Cancelar_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void Volver_Boton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
