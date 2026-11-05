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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            gridProductos = new DataGridView();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            grpDatos = new GroupBox();
            textBox1 = new TextBox();
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
            Subas_Boton = new Button();
            ((System.ComponentModel.ISupportInitialize)gridProductos).BeginInit();
            grpDatos.SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            SuspendLayout();
            // 
            // gridProductos
            // 
            gridProductos.AllowUserToAddRows = false;
            gridProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridProductos.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridProductos.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridProductos.ColumnHeadersHeight = 32;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridProductos.DefaultCellStyle = dataGridViewCellStyle2;
            gridProductos.GridColor = Color.FromArgb(51, 65, 85);
            gridProductos.Location = new Point(20, 95);
            gridProductos.Name = "gridProductos";
            gridProductos.ReadOnly = true;
            gridProductos.RowHeadersVisible = false;
            gridProductos.RowHeadersWidth = 51;
            gridProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridProductos.Size = new Size(760, 605);
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
            lblTitulo.Size = new Size(300, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE PRODUCTOS";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(20, 60);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(48, 15);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(51, 65, 85);
            txtBuscar.ForeColor = Color.White;
            txtBuscar.Location = new Point(80, 56);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(280, 24);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // grpDatos
            // 
            grpDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
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
            grpDatos.Location = new Point(800, 95);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(460, 605);
            grpDatos.TabIndex = 4;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos del Producto";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(51, 65, 85);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(10, 303);
            textBox1.MaxLength = 10;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(205, 24);
            textBox1.TabIndex = 10;
            textBox1.Text = "0.00";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(10, 283);
            label1.Name = "label1";
            label1.Size = new Size(95, 15);
            label1.TabIndex = 20;
            label1.Text = "% de Ganancia";
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.ForeColor = Color.Gray;
            lblCodigo.Location = new Point(10, 22);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(110, 15);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código de barras:";
            // 
            // txtCodigo
            // 
            txtCodigo.BackColor = Color.FromArgb(15, 23, 42);
            txtCodigo.Font = new Font("Consolas", 10F);
            txtCodigo.ForeColor = Color.Gray;
            txtCodigo.Location = new Point(10, 42);
            txtCodigo.MaxLength = 13;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(250, 24);
            txtCodigo.TabIndex = 1;
            txtCodigo.Text = "Dejar vacio para automatico";
            txtCodigo.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(10, 72);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(58, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(10, 92);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(440, 24);
            txtNombre.TabIndex = 3;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.ForeColor = Color.White;
            lblDescripcion.Location = new Point(10, 128);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(78, 15);
            lblDescripcion.TabIndex = 4;
            lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.BackColor = Color.FromArgb(51, 65, 85);
            txtDescripcion.ForeColor = Color.White;
            txtDescripcion.Location = new Point(10, 148);
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
            lblPrecio.Location = new Point(10, 226);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(125, 15);
            lblPrecio.TabIndex = 6;
            lblPrecio.Text = "Precio de Venta ($):";
            // 
            // txtPrecio
            // 
            txtPrecio.BackColor = Color.FromArgb(51, 65, 85);
            txtPrecio.ForeColor = Color.White;
            txtPrecio.Location = new Point(10, 246);
            txtPrecio.MaxLength = 10;
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(205, 24);
            txtPrecio.TabIndex = 7;
            txtPrecio.Text = "0.00";
            txtPrecio.KeyPress += SoloNumerosDecimal_KeyPress;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.ForeColor = Color.White;
            lblStock.Location = new Point(235, 226);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(44, 15);
            lblStock.TabIndex = 8;
            lblStock.Text = "Stock:";
            // 
            // txtStock
            // 
            txtStock.BackColor = Color.FromArgb(51, 65, 85);
            txtStock.ForeColor = Color.White;
            txtStock.Location = new Point(235, 246);
            txtStock.MaxLength = 10;
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(215, 24);
            txtStock.TabIndex = 9;
            txtStock.Text = "0";
            txtStock.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.ForeColor = Color.White;
            lblMarca.Location = new Point(10, 342);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(47, 15);
            lblMarca.TabIndex = 10;
            lblMarca.Text = "Marca:";
            // 
            // cmbMarca
            // 
            cmbMarca.BackColor = Color.FromArgb(51, 65, 85);
            cmbMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarca.ForeColor = Color.White;
            cmbMarca.Location = new Point(10, 362);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new Size(205, 24);
            cmbMarca.TabIndex = 11;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.ForeColor = Color.White;
            lblCategoria.Location = new Point(235, 342);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(68, 15);
            lblCategoria.TabIndex = 12;
            lblCategoria.Text = "Categoría:";
            // 
            // cmbCategoria
            // 
            cmbCategoria.BackColor = Color.FromArgb(51, 65, 85);
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.ForeColor = Color.White;
            cmbCategoria.Location = new Point(235, 362);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(215, 24);
            cmbCategoria.TabIndex = 13;
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.ForeColor = Color.White;
            lblProveedor.Location = new Point(10, 402);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(72, 15);
            lblProveedor.TabIndex = 14;
            lblProveedor.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            cmbProveedor.BackColor = Color.FromArgb(51, 65, 85);
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.ForeColor = Color.White;
            cmbProveedor.Location = new Point(10, 422);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(440, 24);
            cmbProveedor.TabIndex = 15;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(10, 460);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(120, 19);
            chkActivo.TabIndex = 16;
            chkActivo.Text = "Producto activo";
            // 
            // Guardar_Boton
            // 
            Guardar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.Cursor = Cursors.Hand;
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Location = new Point(10, 498);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(215, 40);
            Guardar_Boton.TabIndex = 17;
            Guardar_Boton.Text = "Crear Producto";
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;
            // 
            // Eliminar_Boton
            // 
            Eliminar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.Cursor = Cursors.Hand;
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.Location = new Point(235, 498);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(215, 40);
            Eliminar_Boton.TabIndex = 19;
            Eliminar_Boton.Text = "Desactivar Producto";
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.Cursor = Cursors.Hand;
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(10, 548);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(440, 40);
            Cancelar_Boton.TabIndex = 18;
            Cancelar_Boton.Text = "Limpiar";
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click += Cancelar_Boton_Click;
            // 
            // Nuevo_Boton
            // 
            Nuevo_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Nuevo_Boton.Cursor = Cursors.Hand;
            Nuevo_Boton.FlatAppearance.BorderSize = 0;
            Nuevo_Boton.FlatStyle = FlatStyle.Flat;
            Nuevo_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Nuevo_Boton.ForeColor = Color.Black;
            Nuevo_Boton.Location = new Point(380, 53);
            Nuevo_Boton.Name = "Nuevo_Boton";
            Nuevo_Boton.Size = new Size(165, 32);
            Nuevo_Boton.TabIndex = 5;
            Nuevo_Boton.Text = "+ Nuevo Producto";
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click += Nuevo_Boton_Click;
            // 
            // Subas_Boton
            // 
            Subas_Boton.BackColor = Color.FromArgb(245, 158, 11);
            Subas_Boton.Cursor = Cursors.Hand;
            Subas_Boton.FlatAppearance.BorderSize = 0;
            Subas_Boton.FlatStyle = FlatStyle.Flat;
            Subas_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Subas_Boton.ForeColor = Color.Black;
            Subas_Boton.Location = new Point(560, 53);
            Subas_Boton.Name = "Subas_Boton";
            Subas_Boton.Size = new Size(190, 32);
            Subas_Boton.TabIndex = 7;
            Subas_Boton.Text = "📈 Subas de Precios";
            Subas_Boton.UseVisualStyleBackColor = false;
            Subas_Boton.Click += Subas_Boton_Click;
            // 
            // Volver_Boton
            // 
            Volver_Boton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.Cursor = Cursors.Hand;
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(1130, 53);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(130, 32);
            Volver_Boton.TabIndex = 6;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // FormGestionProductos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridProductos);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Controls.Add(Subas_Boton);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormGestionProductos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Productos - Cantina Padel Club";
            ((System.ComponentModel.ISupportInitialize)gridProductos).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
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
        private Button           Subas_Boton;
        private Label label1;
        private TextBox textBox1;
    }
}
