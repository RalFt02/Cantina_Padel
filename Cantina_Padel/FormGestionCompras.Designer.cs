namespace Cantina_Padel
{
    partial class FormGestionCompras
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            gridCompras = new DataGridView();
            gridDetalle = new DataGridView();
            gridItems = new DataGridView();
            lblTitulo = new Label();
            lblCompras = new Label();
            lblDetalle = new Label();
            lblProveedor = new Label();
            lblProducto = new Label();
            lblCantidad = new Label();
            lblPrecio = new Label();
            lblTotal = new Label();
            cmbProveedor = new ComboBox();
            cmbProducto = new ComboBox();
            txtCantidad = new TextBox();
            txtPrecio = new TextBox();
            grpNuevaCompra = new GroupBox();
            Agregar_Boton = new Button();
            Quitar_Boton = new Button();
            Registrar_Boton = new Button();
            Limpiar_Boton = new Button();
            Eliminar_Boton = new Button();
            Volver_Boton = new Button();
            ((System.ComponentModel.ISupportInitialize)gridCompras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridDetalle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridItems).BeginInit();
            grpNuevaCompra.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE COMPRAS";
            // 
            // Volver_Boton
            // 
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(1110, 15);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(110, 32);
            Volver_Boton.TabIndex = 1;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // lblCompras
            // 
            lblCompras.AutoSize = true;
            lblCompras.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCompras.ForeColor = Color.FromArgb(163, 230, 53);
            lblCompras.Location = new Point(20, 65);
            lblCompras.Name = "lblCompras";
            lblCompras.TabIndex = 2;
            lblCompras.Text = "Compras registradas:";
            // 
            // gridCompras
            // 
            gridCompras.AllowUserToAddRows = false;
            gridCompras.AllowUserToDeleteRows = false;
            gridCompras.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridCompras.BorderStyle = BorderStyle.None;
            gridCompras.GridColor = Color.FromArgb(51, 65, 85);
            gridCompras.Location = new Point(20, 95);
            gridCompras.MultiSelect = false;
            gridCompras.Name = "gridCompras";
            gridCompras.ReadOnly = true;
            gridCompras.RowHeadersVisible = false;
            gridCompras.RowHeadersWidth = 51;
            gridCompras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCompras.Size = new Size(600, 290);
            gridCompras.TabIndex = 3;
            gridCompras.CellClick += gridCompras_CellClick;
            // 
            // Eliminar_Boton
            // 
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.Location = new Point(20, 395);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(240, 38);
            Eliminar_Boton.TabIndex = 4;
            Eliminar_Boton.Text = "Eliminar compra seleccionada";
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDetalle.ForeColor = Color.FromArgb(163, 230, 53);
            lblDetalle.Location = new Point(20, 445);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.TabIndex = 5;
            lblDetalle.Text = "Detalle de la compra seleccionada:";
            // 
            // gridDetalle
            // 
            gridDetalle.AllowUserToAddRows = false;
            gridDetalle.AllowUserToDeleteRows = false;
            gridDetalle.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridDetalle.BorderStyle = BorderStyle.None;
            gridDetalle.GridColor = Color.FromArgb(51, 65, 85);
            gridDetalle.Location = new Point(20, 475);
            gridDetalle.MultiSelect = false;
            gridDetalle.Name = "gridDetalle";
            gridDetalle.ReadOnly = true;
            gridDetalle.RowHeadersVisible = false;
            gridDetalle.RowHeadersWidth = 51;
            gridDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridDetalle.Size = new Size(600, 185);
            gridDetalle.TabIndex = 6;
            // 
            // grpNuevaCompra
            // 
            grpNuevaCompra.BackColor = Color.FromArgb(30, 41, 59);
            grpNuevaCompra.Controls.Add(lblProveedor);
            grpNuevaCompra.Controls.Add(cmbProveedor);
            grpNuevaCompra.Controls.Add(lblProducto);
            grpNuevaCompra.Controls.Add(cmbProducto);
            grpNuevaCompra.Controls.Add(lblCantidad);
            grpNuevaCompra.Controls.Add(txtCantidad);
            grpNuevaCompra.Controls.Add(lblPrecio);
            grpNuevaCompra.Controls.Add(txtPrecio);
            grpNuevaCompra.Controls.Add(Agregar_Boton);
            grpNuevaCompra.Controls.Add(Quitar_Boton);
            grpNuevaCompra.Controls.Add(gridItems);
            grpNuevaCompra.Controls.Add(lblTotal);
            grpNuevaCompra.Controls.Add(Registrar_Boton);
            grpNuevaCompra.Controls.Add(Limpiar_Boton);
            grpNuevaCompra.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpNuevaCompra.ForeColor = Color.FromArgb(163, 230, 53);
            grpNuevaCompra.Location = new Point(640, 60);
            grpNuevaCompra.Name = "grpNuevaCompra";
            grpNuevaCompra.Size = new Size(580, 600);
            grpNuevaCompra.TabIndex = 7;
            grpNuevaCompra.TabStop = false;
            grpNuevaCompra.Text = "Nueva compra";
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.ForeColor = Color.White;
            lblProveedor.Location = new Point(10, 25);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.TabIndex = 0;
            lblProveedor.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            cmbProveedor.BackColor = Color.FromArgb(51, 65, 85);
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.FlatStyle = FlatStyle.Flat;
            cmbProveedor.ForeColor = Color.White;
            cmbProveedor.Location = new Point(10, 50);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(555, 28);
            cmbProveedor.TabIndex = 1;
            cmbProveedor.SelectedIndexChanged += cmbProveedor_SelectedIndexChanged;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.ForeColor = Color.White;
            lblProducto.Location = new Point(10, 85);
            lblProducto.Name = "lblProducto";
            lblProducto.TabIndex = 2;
            lblProducto.Text = "Producto:";
            // 
            // cmbProducto
            // 
            cmbProducto.BackColor = Color.FromArgb(51, 65, 85);
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.FlatStyle = FlatStyle.Flat;
            cmbProducto.ForeColor = Color.White;
            cmbProducto.Location = new Point(10, 110);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(555, 28);
            cmbProducto.TabIndex = 3;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.ForeColor = Color.White;
            lblCantidad.Location = new Point(10, 148);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.TabIndex = 4;
            lblCantidad.Text = "Cantidad:";
            // 
            // txtCantidad
            // 
            txtCantidad.BackColor = Color.FromArgb(51, 65, 85);
            txtCantidad.ForeColor = Color.White;
            txtCantidad.Location = new Point(10, 173);
            txtCantidad.MaxLength = 6;
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(120, 27);
            txtCantidad.TabIndex = 5;
            txtCantidad.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.ForeColor = Color.White;
            lblPrecio.Location = new Point(145, 148);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.TabIndex = 6;
            lblPrecio.Text = "Costo unitario:";
            // 
            // txtPrecio
            // 
            txtPrecio.BackColor = Color.FromArgb(51, 65, 85);
            txtPrecio.ForeColor = Color.White;
            txtPrecio.Location = new Point(145, 173);
            txtPrecio.MaxLength = 11;
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(140, 27);
            txtPrecio.TabIndex = 7;
            txtPrecio.KeyPress += SoloNumerosDecimal_KeyPress;
            // 
            // Agregar_Boton
            // 
            Agregar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Agregar_Boton.FlatAppearance.BorderSize = 0;
            Agregar_Boton.FlatStyle = FlatStyle.Flat;
            Agregar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Agregar_Boton.ForeColor = Color.Black;
            Agregar_Boton.Location = new Point(300, 168);
            Agregar_Boton.Name = "Agregar_Boton";
            Agregar_Boton.Size = new Size(125, 36);
            Agregar_Boton.TabIndex = 8;
            Agregar_Boton.Text = "Agregar ítem";
            Agregar_Boton.UseVisualStyleBackColor = false;
            Agregar_Boton.Click += Agregar_Boton_Click;
            // 
            // Quitar_Boton
            // 
            Quitar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Quitar_Boton.FlatAppearance.BorderSize = 0;
            Quitar_Boton.FlatStyle = FlatStyle.Flat;
            Quitar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Quitar_Boton.ForeColor = Color.White;
            Quitar_Boton.Location = new Point(440, 168);
            Quitar_Boton.Name = "Quitar_Boton";
            Quitar_Boton.Size = new Size(125, 36);
            Quitar_Boton.TabIndex = 9;
            Quitar_Boton.Text = "Quitar ítem";
            Quitar_Boton.UseVisualStyleBackColor = false;
            Quitar_Boton.Click += Quitar_Boton_Click;
            // 
            // gridItems
            // 
            gridItems.AllowUserToAddRows = false;
            gridItems.AllowUserToDeleteRows = false;
            gridItems.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridItems.BorderStyle = BorderStyle.None;
            gridItems.GridColor = Color.FromArgb(51, 65, 85);
            gridItems.Location = new Point(10, 220);
            gridItems.MultiSelect = false;
            gridItems.Name = "gridItems";
            gridItems.ReadOnly = true;
            gridItems.RowHeadersVisible = false;
            gridItems.RowHeadersWidth = 51;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.Size = new Size(555, 270);
            gridItems.TabIndex = 10;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(163, 230, 53);
            lblTotal.Location = new Point(10, 500);
            lblTotal.Name = "lblTotal";
            lblTotal.TabIndex = 11;
            lblTotal.Text = "Total: $ 0,00";
            // 
            // Registrar_Boton
            // 
            Registrar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Registrar_Boton.FlatAppearance.BorderSize = 0;
            Registrar_Boton.FlatStyle = FlatStyle.Flat;
            Registrar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Registrar_Boton.ForeColor = Color.Black;
            Registrar_Boton.Location = new Point(10, 545);
            Registrar_Boton.Name = "Registrar_Boton";
            Registrar_Boton.Size = new Size(270, 42);
            Registrar_Boton.TabIndex = 12;
            Registrar_Boton.Text = "Registrar compra";
            Registrar_Boton.UseVisualStyleBackColor = false;
            Registrar_Boton.Click += Registrar_Boton_Click;
            // 
            // Limpiar_Boton
            // 
            Limpiar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Limpiar_Boton.FlatAppearance.BorderSize = 0;
            Limpiar_Boton.FlatStyle = FlatStyle.Flat;
            Limpiar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Limpiar_Boton.ForeColor = Color.White;
            Limpiar_Boton.Location = new Point(295, 545);
            Limpiar_Boton.Name = "Limpiar_Boton";
            Limpiar_Boton.Size = new Size(270, 42);
            Limpiar_Boton.TabIndex = 13;
            Limpiar_Boton.Text = "Limpiar";
            Limpiar_Boton.UseVisualStyleBackColor = false;
            Limpiar_Boton.Click += Limpiar_Boton_Click;
            // 
            // FormGestionCompras
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1240, 680);
            Controls.Add(lblTitulo);
            Controls.Add(Volver_Boton);
            Controls.Add(lblCompras);
            Controls.Add(gridCompras);
            Controls.Add(Eliminar_Boton);
            Controls.Add(lblDetalle);
            Controls.Add(gridDetalle);
            Controls.Add(grpNuevaCompra);
            Name = "FormGestionCompras";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Compras";
            ((System.ComponentModel.ISupportInitialize)gridCompras).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridDetalle).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridItems).EndInit();
            grpNuevaCompra.ResumeLayout(false);
            grpNuevaCompra.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gridCompras;
        private DataGridView gridDetalle;
        private DataGridView gridItems;
        private Label lblTitulo;
        private Label lblCompras;
        private Label lblDetalle;
        private Label lblProveedor;
        private Label lblProducto;
        private Label lblCantidad;
        private Label lblPrecio;
        private Label lblTotal;
        private ComboBox cmbProveedor;
        private ComboBox cmbProducto;
        private TextBox txtCantidad;
        private TextBox txtPrecio;
        private GroupBox grpNuevaCompra;
        private Button Agregar_Boton;
        private Button Quitar_Boton;
        private Button Registrar_Boton;
        private Button Limpiar_Boton;
        private Button Eliminar_Boton;
        private Button Volver_Boton;
    }
}
