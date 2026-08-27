namespace Cantina_Padel
{
    partial class FormGestionProductos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            gridProductos     = new DataGridView();
            lblTitulo         = new Label();
            lblBuscar         = new Label();
            txtBuscar         = new TextBox();
            grpDatos          = new GroupBox();
            lblId             = new Label();
            txtId             = new TextBox();
            lblNombre         = new Label();
            txtNombre         = new TextBox();
            lblDescripcion    = new Label();
            txtDescripcion    = new TextBox();
            lblPrecio         = new Label();
            txtPrecio         = new TextBox();
            lblStock          = new Label();
            txtStock          = new TextBox();
            lblMarca          = new Label();
            cmbMarca          = new ComboBox();
            lblCategoria      = new Label();
            cmbCategoria      = new ComboBox();
            lblProveedor      = new Label();
            cmbProveedor      = new ComboBox();
            chkActivo         = new CheckBox();
            Nuevo_Boton       = new Button();
            Guardar_Boton     = new Button();
            Eliminar_Boton    = new Button();
            Cancelar_Boton    = new Button();
            Volver_Boton      = new Button();

            ((System.ComponentModel.ISupportInitialize)gridProductos).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();

            // ── Formulario ──────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(15, 23, 42);
            ClientSize          = new Size(1100, 700);
            Name                = "FormGestionProductos";
            Text                = "Gestión de Productos";
            StartPosition       = FormStartPosition.CenterScreen;

            // ── Título ──────────────────────────────────────────
            lblTitulo.Text      = "GESTIÓN DE PRODUCTOS";
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Font      = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location  = new Point(20, 15);
            lblTitulo.Size      = new Size(400, 35);
            lblTitulo.AutoSize  = true;

            // ── Buscar ──────────────────────────────────────────
            lblBuscar.Text      = "Buscar:";
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location  = new Point(20, 60);
            lblBuscar.AutoSize  = true;

            txtBuscar.Location  = new Point(80, 57);
            txtBuscar.Size      = new Size(250, 27);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // ── Grilla ──────────────────────────────────────────
            gridProductos.Location                  = new Point(20, 95);
            gridProductos.Size                      = new Size(580, 560);
            gridProductos.BackgroundColor           = Color.FromArgb(30, 41, 59);
            gridProductos.ForeColor                 = Color.White;
            gridProductos.GridColor                 = Color.FromArgb(51, 65, 85);
            gridProductos.BorderStyle               = BorderStyle.None;
            gridProductos.RowHeadersVisible         = false;
            gridProductos.AllowUserToAddRows        = false;
            gridProductos.ReadOnly                  = true;
            gridProductos.SelectionMode             = DataGridViewSelectionMode.FullRowSelect;
            gridProductos.AutoSizeColumnsMode       = DataGridViewAutoSizeColumnsMode.None;
            gridProductos.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(51, 65, 85);
            gridProductos.ColumnHeadersDefaultCellStyle.ForeColor  = Color.FromArgb(163, 230, 53);
            gridProductos.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridProductos.DefaultCellStyle.BackColor               = Color.FromArgb(30, 41, 59);
            gridProductos.DefaultCellStyle.ForeColor               = Color.White;
            gridProductos.DefaultCellStyle.SelectionBackColor      = Color.FromArgb(163, 230, 53);
            gridProductos.DefaultCellStyle.SelectionForeColor      = Color.Black;
            gridProductos.CellClick += gridProductos_CellClick;

            // ── Panel Datos ─────────────────────────────────────
            grpDatos.Text      = "Datos del Producto";
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.Location  = new Point(620, 95);
            grpDatos.Size      = new Size(460, 560);
            grpDatos.BackColor = Color.FromArgb(30, 41, 59);

            // ID (oculto visualmente)
            lblId.Text      = "ID:";
            lblId.ForeColor = Color.Gray;
            lblId.Location  = new Point(10, 28);
            lblId.AutoSize  = true;
            txtId.Location  = new Point(60, 25);
            txtId.Size      = new Size(60, 27);
            txtId.ReadOnly  = true;
            txtId.BackColor = Color.FromArgb(15, 23, 42);
            txtId.ForeColor = Color.Gray;

            // Nombre
            lblNombre.Text      = "Nombre:";
            lblNombre.ForeColor = Color.White;
            lblNombre.Location  = new Point(10, 68);
            lblNombre.AutoSize  = true;
            txtNombre.Location  = new Point(10, 90);
            txtNombre.Size      = new Size(440, 27);
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;

            // Descripción (multilínea)
            lblDescripcion.Text      = "Descripción:";
            lblDescripcion.ForeColor = Color.White;
            lblDescripcion.Location  = new Point(10, 128);
            lblDescripcion.AutoSize  = true;
            txtDescripcion.Location    = new Point(10, 150);
            txtDescripcion.Size        = new Size(440, 70);
            txtDescripcion.Multiline   = true;
            txtDescripcion.ScrollBars  = ScrollBars.Vertical;
            txtDescripcion.BackColor   = Color.FromArgb(51, 65, 85);
            txtDescripcion.ForeColor   = Color.White;

            // Precio / Stock
            lblPrecio.Text      = "Precio de Venta ($):";
            lblPrecio.ForeColor = Color.White;
            lblPrecio.Location  = new Point(10, 238);
            lblPrecio.AutoSize  = true;
            txtPrecio.Location  = new Point(10, 260);
            txtPrecio.Size      = new Size(205, 27);
            txtPrecio.Text      = "0.00";
            txtPrecio.BackColor = Color.FromArgb(51, 65, 85);
            txtPrecio.ForeColor = Color.White;

            lblStock.Text      = "Stock:";
            lblStock.ForeColor = Color.White;
            lblStock.Location  = new Point(235, 238);
            lblStock.AutoSize  = true;
            txtStock.Location  = new Point(235, 260);
            txtStock.Size      = new Size(215, 27);
            txtStock.Text      = "0";
            txtStock.BackColor = Color.FromArgb(51, 65, 85);
            txtStock.ForeColor = Color.White;

            // Marca / Categoría
            lblMarca.Text      = "Marca:";
            lblMarca.ForeColor = Color.White;
            lblMarca.Location  = new Point(10, 298);
            lblMarca.AutoSize  = true;
            cmbMarca.Location      = new Point(10, 320);
            cmbMarca.Size          = new Size(205, 28);
            cmbMarca.BackColor     = Color.FromArgb(51, 65, 85);
            cmbMarca.ForeColor     = Color.White;
            cmbMarca.DropDownStyle = ComboBoxStyle.DropDownList;

            lblCategoria.Text      = "Categoría:";
            lblCategoria.ForeColor = Color.White;
            lblCategoria.Location  = new Point(235, 298);
            lblCategoria.AutoSize  = true;
            cmbCategoria.Location      = new Point(235, 320);
            cmbCategoria.Size          = new Size(215, 28);
            cmbCategoria.BackColor     = Color.FromArgb(51, 65, 85);
            cmbCategoria.ForeColor     = Color.White;
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;

            // Proveedor
            lblProveedor.Text      = "Proveedor:";
            lblProveedor.ForeColor = Color.White;
            lblProveedor.Location  = new Point(10, 358);
            lblProveedor.AutoSize  = true;
            cmbProveedor.Location      = new Point(10, 380);
            cmbProveedor.Size          = new Size(440, 28);
            cmbProveedor.BackColor     = Color.FromArgb(51, 65, 85);
            cmbProveedor.ForeColor     = Color.White;
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;

            // Activo
            chkActivo.Text      = "Producto activo";
            chkActivo.ForeColor = Color.White;
            chkActivo.Location  = new Point(10, 420);
            chkActivo.Checked   = true;
            chkActivo.AutoSize  = true;

            // ── Botones del panel ───────────────────────────────
            Guardar_Boton.Text             = "Crear Producto";
            Guardar_Boton.Location         = new Point(10, 458);
            Guardar_Boton.Size             = new Size(215, 38);
            Guardar_Boton.BackColor        = Color.FromArgb(163, 230, 53);
            Guardar_Boton.ForeColor        = Color.Black;
            Guardar_Boton.Font             = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.FlatStyle        = FlatStyle.Flat;
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click           += Guardar_Boton_Click;

            Cancelar_Boton.Text            = "Limpiar";
            Cancelar_Boton.Location        = new Point(235, 458);
            Cancelar_Boton.Size            = new Size(215, 38);
            Cancelar_Boton.BackColor       = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.ForeColor       = Color.White;
            Cancelar_Boton.FlatStyle       = FlatStyle.Flat;
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click          += Cancelar_Boton_Click;

            Eliminar_Boton.Text            = "Desactivar Producto";
            Eliminar_Boton.Location        = new Point(10, 506);
            Eliminar_Boton.Size            = new Size(440, 35);
            Eliminar_Boton.BackColor       = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.ForeColor       = Color.White;
            Eliminar_Boton.FlatStyle       = FlatStyle.Flat;
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click          += Eliminar_Boton_Click;

            grpDatos.Controls.AddRange(new Control[]
            {
                lblId, txtId,
                lblNombre, txtNombre,
                lblDescripcion, txtDescripcion,
                lblPrecio, txtPrecio,
                lblStock, txtStock,
                lblMarca, cmbMarca,
                lblCategoria, cmbCategoria,
                lblProveedor, cmbProveedor,
                chkActivo,
                Guardar_Boton, Cancelar_Boton,
                Eliminar_Boton
            });

            // ── Botones externos ────────────────────────────────
            Nuevo_Boton.Text           = "+ Nuevo Producto";
            Nuevo_Boton.Location       = new Point(350, 55);
            Nuevo_Boton.Size           = new Size(160, 32);
            Nuevo_Boton.BackColor      = Color.FromArgb(163, 230, 53);
            Nuevo_Boton.ForeColor      = Color.Black;
            Nuevo_Boton.Font           = new Font("Segoe UI", 9F, FontStyle.Bold);
            Nuevo_Boton.FlatStyle      = FlatStyle.Flat;
            Nuevo_Boton.FlatAppearance.BorderSize = 0;
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click         += Nuevo_Boton_Click;

            Volver_Boton.Text          = "← Volver";
            Volver_Boton.Location      = new Point(970, 55);
            Volver_Boton.Size          = new Size(110, 32);
            Volver_Boton.BackColor     = Color.FromArgb(71, 85, 105);
            Volver_Boton.ForeColor     = Color.White;
            Volver_Boton.FlatStyle     = FlatStyle.Flat;
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click        += Volver_Boton_Click;

            Controls.AddRange(new Control[]
            {
                lblTitulo,
                lblBuscar, txtBuscar,
                gridProductos,
                grpDatos,
                Nuevo_Boton,
                Volver_Boton
            });

            ((System.ComponentModel.ISupportInitialize)gridProductos).EndInit();
            grpDatos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView     gridProductos;
        private Label            lblTitulo;
        private Label            lblBuscar;
        private TextBox          txtBuscar;
        private GroupBox         grpDatos;
        private Label            lblId;
        private TextBox          txtId;
        private Label            lblNombre;
        private TextBox          txtNombre;
        private Label            lblDescripcion;
        private TextBox          txtDescripcion;
        private Label            lblPrecio;
        private TextBox          txtPrecio;
        private Label            lblStock;
        private TextBox          txtStock;
        private Label            lblMarca;
        private ComboBox         cmbMarca;
        private Label            lblCategoria;
        private ComboBox         cmbCategoria;
        private Label            lblProveedor;
        private ComboBox         cmbProveedor;
        private CheckBox         chkActivo;
        private Button           Nuevo_Boton;
        private Button           Guardar_Boton;
        private Button           Eliminar_Boton;
        private Button           Cancelar_Boton;
        private Button           Volver_Boton;
    }
}
