using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormGestionProductos : Form
    {
        public FormGestionProductos()
        {
            InitializeComponent();
            CargarCombos();
            CargarProductos();
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

                // Categoria
                var daCategoria = new MySqlDataAdapter("SELECT id_categoria, nombre FROM categoria WHERE activo = 1 ORDER BY nombre", conn);
                var dtCategoria = new System.Data.DataTable();
                daCategoria.Fill(dtCategoria);
                cmbCategoria.DataSource    = dtCategoria;
                cmbCategoria.DisplayMember = "nombre";
                cmbCategoria.ValueMember   = "id_categoria";

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

                gridProductos.Columns["id_producto"].HeaderText   = "ID";
                gridProductos.Columns["nombre"].HeaderText         = "Nombre";
                gridProductos.Columns["descripcion"].HeaderText    = "Descripción";
                gridProductos.Columns["precio_venta"].HeaderText   = "Precio";
                gridProductos.Columns["stock"].HeaderText          = "Stock";
                gridProductos.Columns["marca"].HeaderText          = "Marca";
                gridProductos.Columns["categoria"].HeaderText      = "Categoría";
                gridProductos.Columns["proveedor"].HeaderText      = "Proveedor";
                gridProductos.Columns["activo"].HeaderText         = "Activo";

                gridProductos.Columns["id_producto"].Width  = 40;
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


        // ─────────────────────────────────────────────
        //  BOTÓN NUEVO
        // ─────────────────────────────────────────────
        private void Nuevo_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Guardar_Boton.Text = "Crear Producto";
            txtId.Text = "";
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

            bool esNuevo = string.IsNullOrEmpty(txtId.Text);

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();
                using MySqlTransaction tx = conn.BeginTransaction();

                if (esNuevo)
                    CrearProducto(conn, tx, precio, stock);
                else
                    EditarProducto(conn, tx, precio, stock);

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

        private void CrearProducto(MySqlConnection conn, MySqlTransaction tx, decimal precio, int stock)
        {
            string insertProducto = @"
                INSERT INTO producto (nombre, descripcion, precio_venta, stock, activo, id_marca, id_categoria, id_proveedor)
                VALUES (@n, @desc, @p, @s, @a, @idm, @idc, @idp)";

            using MySqlCommand cmd = new MySqlCommand(insertProducto, conn, tx);
            cmd.Parameters.AddWithValue("@n",    txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@desc", string.IsNullOrWhiteSpace(txtDescripcion.Text) ? DBNull.Value : txtDescripcion.Text.Trim());
            cmd.Parameters.AddWithValue("@p",    precio);
            cmd.Parameters.AddWithValue("@s",    stock);
            cmd.Parameters.AddWithValue("@a",    chkActivo.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@idm",  cmbMarca.SelectedValue);
            cmd.Parameters.AddWithValue("@idc",  cmbCategoria.SelectedValue);
            cmd.Parameters.AddWithValue("@idp",  cmbProveedor.SelectedValue);
            cmd.ExecuteNonQuery();
        }

        private void EditarProducto(MySqlConnection conn, MySqlTransaction tx, decimal precio, int stock)
        {
            int id = int.Parse(txtId.Text);

            string updateProducto = @"
                UPDATE producto
                SET nombre        = @n,
                    descripcion   = @desc,
                    precio_venta  = @p,
                    stock         = @s,
                    activo        = @a,
                    id_marca      = @idm,
                    id_categoria  = @idc,
                    id_proveedor  = @idp
                WHERE id_producto = @id";

            using MySqlCommand cmd = new MySqlCommand(updateProducto, conn, tx);
            cmd.Parameters.AddWithValue("@n",    txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@desc", string.IsNullOrWhiteSpace(txtDescripcion.Text) ? DBNull.Value : txtDescripcion.Text.Trim());
            cmd.Parameters.AddWithValue("@p",    precio);
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
            if (string.IsNullOrEmpty(txtId.Text))
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
                cmd.Parameters.AddWithValue("@id", int.Parse(txtId.Text));
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

            txtId.Text          = row.Cells["id_producto"].Value?.ToString() ?? "";
            txtNombre.Text      = row.Cells["nombre"].Value?.ToString() ?? "";
            txtDescripcion.Text = row.Cells["descripcion"].Value?.ToString() ?? "";

            decimal precio = row.Cells["precio_venta"].Value != null
                ? Convert.ToDecimal(row.Cells["precio_venta"].Value)
                : 0m;
            txtPrecio.Text = precio.ToString("0.00");

            txtStock.Text = row.Cells["stock"].Value?.ToString() ?? "0";

            cmbMarca.SelectedValue     = row.Cells["id_marca"].Value;
            cmbCategoria.SelectedValue = row.Cells["id_categoria"].Value;
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
                    $"nombre LIKE '%{filtro}%' OR marca LIKE '%{filtro}%' OR categoria LIKE '%{filtro}%' OR proveedor LIKE '%{filtro}%'";
            }
        }

        // ─────────────────────────────────────────────
        //  UTILIDADES
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            txtId.Text          = "";
            txtNombre.Text      = "";
            txtDescripcion.Text = "";
            txtPrecio.Text      = "0.00";
            txtStock.Text       = "0";
            if (cmbMarca.Items.Count > 0)     cmbMarca.SelectedIndex     = 0;
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;
            chkActivo.Checked   = true;
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
