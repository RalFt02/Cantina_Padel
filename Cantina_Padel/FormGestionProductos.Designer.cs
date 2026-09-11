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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            gridProductos = new DataGridView();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            grpDatos = new GroupBox();
            label1 = new Label();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblPrecio = new Label();
            txtPrecio = new TextBox();
            lblStock = new Label();
            txtStock = new TextBox();
            lblMarca = new Label();
            cmbMarca = new ComboBox();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblProveedor = new Label();
            cmbProveedor = new ComboBox();
            chkActivo = new CheckBox();
            Guardar_Boton = new Button();
            Cancelar_Boton = new Button();
            Eliminar_Boton = new Button();
            Nuevo_Boton = new Button();
            Volver_Boton = new Button();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)gridProductos).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();
            // 
            // gridProductos
            // 
            gridProductos.AllowUserToAddRows = false;
            gridProductos.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridProductos.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            gridProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            gridProductos.ColumnHeadersHeight = 29;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            gridProductos.DefaultCellStyle = dataGridViewCellStyle4;
            gridProductos.GridColor = Color.FromArgb(51, 65, 85);
            gridProductos.Location = new Point(20, 95);
            gridProductos.Name = "gridProductos";
            gridProductos.ReadOnly = true;
            gridProductos.RowHeadersVisible = false;
            gridProductos.RowHeadersWidth = 51;
            gridProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridProductos.Size = new Size(580, 560);
            gridProductos.TabIndex = 3;
            gridProductos.CellClick += gridProductos_CellClick;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(341, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE PRODUCTOS";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(20, 60);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(55, 20);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(80, 57);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(250, 27);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // grpDatos
            // 
            grpDatos.BackColor = Color.FromArgb(30, 41, 59);
            grpDatos.Controls.Add(textBox1);
            grpDatos.Controls.Add(label1);
            grpDatos.Controls.Add(lblCodigo);
            grpDatos.Controls.Add(txtCodigo);
            grpDatos.Controls.Add(lblNombre);
            grpDatos.Controls.Add(txtNombre);
            grpDatos.Controls.Add(lblDescripcion);
            grpDatos.Controls.Add(txtDescripcion);
            grpDatos.Controls.Add(lblPrecio);
            grpDatos.Controls.Add(txtPrecio);
            grpDatos.Controls.Add(lblStock);
            grpDatos.Controls.Add(txtStock);
            grpDatos.Controls.Add(lblMarca);
            grpDatos.Controls.Add(cmbMarca);
            grpDatos.Controls.Add(lblCategoria);
            grpDatos.Controls.Add(cmbCategoria);
            grpDatos.Controls.Add(lblProveedor);
            grpDatos.Controls.Add(cmbProveedor);
            grpDatos.Controls.Add(chkActivo);
            grpDatos.Controls.Add(Guardar_Boton);
            grpDatos.Controls.Add(Cancelar_Boton);
            grpDatos.Controls.Add(Eliminar_Boton);
            grpDatos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Location = new Point(620, 95);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(460, 560);
            grpDatos.TabIndex = 4;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos del Producto";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(10, 281);
            label1.Name = "label1";
            label1.Size = new Size(111, 20);
            label1.TabIndex = 20;
            label1.Text = "% de Ganancia";
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.ForeColor = Color.Gray;
            lblCodigo.Location = new Point(10, 20);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(131, 20);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código de barras:";
            // 
            // txtCodigo
            // 
            txtCodigo.BackColor = Color.FromArgb(15, 23, 42);
            txtCodigo.Font = new Font("Consolas", 10F);
            txtCodigo.ForeColor = Color.Gray;
            txtCodigo.Location = new Point(10, 40);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.ReadOnly = true;
            txtCodigo.Size = new Size(250, 27);
            txtCodigo.TabIndex = 1;
            txtCodigo.Text = "(se genera al guardar)";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(10, 68);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(71, 20);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(10, 90);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(440, 27);
            txtNombre.TabIndex = 3;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.ForeColor = Color.White;
            lblDescripcion.Location = new Point(10, 128);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(94, 20);
            lblDescripcion.TabIndex = 4;
            lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.BackColor = Color.FromArgb(51, 65, 85);
            txtDescripcion.ForeColor = Color.White;
            txtDescripcion.Location = new Point(10, 150);
            txtDescripcion.MaxLength = 200;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Size = new Size(440, 70);
            txtDescripcion.TabIndex = 5;
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.ForeColor = Color.White;
            lblPrecio.Location = new Point(10, 225);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(146, 20);
            lblPrecio.TabIndex = 6;
            lblPrecio.Text = "Precio de Venta ($):";
            // 
            // txtPrecio
            // 
            txtPrecio.BackColor = Color.FromArgb(51, 65, 85);
            txtPrecio.ForeColor = Color.White;
            txtPrecio.Location = new Point(10, 248);
            txtPrecio.MaxLength = 10;
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(205, 27);
            txtPrecio.TabIndex = 7;
            txtPrecio.Text = "0.00";
            txtPrecio.KeyPress += SoloNumerosDecimal_KeyPress;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.ForeColor = Color.White;
            lblStock.Location = new Point(235, 225);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(52, 20);
            lblStock.TabIndex = 8;
            lblStock.Text = "Stock:";
            // 
            // txtStock
            // 
            txtStock.BackColor = Color.FromArgb(51, 65, 85);
            txtStock.ForeColor = Color.White;
            txtStock.Location = new Point(235, 248);
            txtStock.MaxLength = 10;
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(215, 27);
            txtStock.TabIndex = 9;
            txtStock.Text = "0";
            txtStock.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.ForeColor = Color.White;
            lblMarca.Location = new Point(10, 301);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(56, 20);
            lblMarca.TabIndex = 10;
            lblMarca.Text = "Marca:";
            // 
            // cmbMarca
            // 
            cmbMarca.BackColor = Color.FromArgb(51, 65, 85);
            cmbMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarca.ForeColor = Color.White;
            cmbMarca.Location = new Point(10, 323);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new Size(205, 28);
            cmbMarca.TabIndex = 11;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.ForeColor = Color.White;
            lblCategoria.Location = new Point(235, 301);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(80, 20);
            lblCategoria.TabIndex = 12;
            lblCategoria.Text = "Categoría:";
            // 
            // cmbCategoria
            // 
            cmbCategoria.BackColor = Color.FromArgb(51, 65, 85);
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.ForeColor = Color.White;
            cmbCategoria.Location = new Point(235, 323);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(215, 28);
            cmbCategoria.TabIndex = 13;
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.ForeColor = Color.White;
            lblProveedor.Location = new Point(10, 361);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(86, 20);
            lblProveedor.TabIndex = 14;
            lblProveedor.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            cmbProveedor.BackColor = Color.FromArgb(51, 65, 85);
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.ForeColor = Color.White;
            cmbProveedor.Location = new Point(10, 385);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(440, 28);
            cmbProveedor.TabIndex = 15;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(10, 427);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(141, 24);
            chkActivo.TabIndex = 16;
            chkActivo.Text = "Producto activo";
            // 
            // Guardar_Boton
            // 
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Location = new Point(10, 460);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(215, 38);
            Guardar_Boton.TabIndex = 17;
            Guardar_Boton.Text = "Crear Producto";
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(239, 510);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(215, 38);
            Cancelar_Boton.TabIndex = 18;
            Cancelar_Boton.Text = "Limpiar";
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click += Cancelar_Boton_Click;
            // 
            // Eliminar_Boton
            // 
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.Location = new Point(239, 460);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(215, 38);
            Eliminar_Boton.TabIndex = 19;
            Eliminar_Boton.Text = "Desactivar Producto";
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;
            // 
            // Nuevo_Boton
            // 
            Nuevo_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Nuevo_Boton.FlatAppearance.BorderSize = 0;
            Nuevo_Boton.FlatStyle = FlatStyle.Flat;
            Nuevo_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Nuevo_Boton.ForeColor = Color.Black;
            Nuevo_Boton.Location = new Point(350, 55);
            Nuevo_Boton.Name = "Nuevo_Boton";
            Nuevo_Boton.Size = new Size(160, 32);
            Nuevo_Boton.TabIndex = 5;
            Nuevo_Boton.Text = "+ Nuevo Producto";
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click += Nuevo_Boton_Click;
            // 
            // Volver_Boton
            // 
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(970, 55);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(110, 32);
            Volver_Boton.TabIndex = 6;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(51, 65, 85);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(121, 279);
            textBox1.MaxLength = 10;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(40, 27);
            textBox1.TabIndex = 21;
            textBox1.Text = "0.00";
            // 
            // FormGestionProductos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1100, 700);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridProductos);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Name = "FormGestionProductos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Productos";
            ((System.ComponentModel.ISupportInitialize)gridProductos).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView     gridProductos;
        private Label            lblTitulo;
        private Label            lblBuscar;
        private TextBox          txtBuscar;
        private GroupBox         grpDatos;
        private Label            lblCodigo;
        private TextBox          txtCodigo;
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
        private Label label1;
        private TextBox textBox1;
    }
}
