namespace Cantina_Padel
{
    partial class FormGestionVentas
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
            lblTitulo = new Label();
            btnCobroCanchas = new Button();
            btnCuentaCorriente = new Button();
            btnCancelarVenta = new Button();
            Volver_Boton = new Button();
            grpCarrito = new GroupBox();
            gridItems = new DataGridView();
            Quitar_Boton = new Button();
            btnEnviarMail = new Button();
            lblTotal = new Label();
            btnImprimir = new Button();
            grpNuevaVenta = new GroupBox();
            lblBuscador = new Label();
            txtBuscador = new TextBox();
            lblProducto = new Label();
            cmbProducto = new ComboBox();
            lblCantidad = new Label();
            txtCantidad = new TextBox();
            lblPrecio = new Label();
            txtPrecio = new TextBox();
            lblStock = new Label();
            lblStockValor = new Label();
            Agregar_Boton = new Button();
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            lblComprobante = new Label();
            cmbComprobante = new ComboBox();
            lblMedioPago = new Label();
            cmbMedioPago = new ComboBox();
            lblEstadoCaja = new Label();
            Registrar_Boton = new Button();
            Limpiar_Boton = new Button();
            grpCarrito.SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            ((System.ComponentModel.ISupportInitialize)gridItems).BeginInit();
            grpNuevaVenta.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(340, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "CAJA - COBRO DE PRODUCTOS";
            // 
            // btnCobroCanchas
            // 
            btnCobroCanchas.BackColor = Color.FromArgb(14, 165, 233);
            btnCobroCanchas.Cursor = Cursors.Hand;
            btnCobroCanchas.FlatAppearance.BorderSize = 0;
            btnCobroCanchas.FlatStyle = FlatStyle.Flat;
            btnCobroCanchas.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCobroCanchas.ForeColor = Color.White;
            btnCobroCanchas.Location = new Point(450, 14);
            btnCobroCanchas.Name = "btnCobroCanchas";
            btnCobroCanchas.Size = new Size(189, 36);
            btnCobroCanchas.TabIndex = 1;
            btnCobroCanchas.Text = "🎾 Cobro de Canchas";
            btnCobroCanchas.UseVisualStyleBackColor = false;
            btnCobroCanchas.Click += btnCobroCanchas_Click;
            // 
            // btnCuentaCorriente
            // 
            btnCuentaCorriente.BackColor = Color.FromArgb(245, 158, 11);
            btnCuentaCorriente.Cursor = Cursors.Hand;
            btnCuentaCorriente.FlatAppearance.BorderSize = 0;
            btnCuentaCorriente.FlatStyle = FlatStyle.Flat;
            btnCuentaCorriente.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCuentaCorriente.ForeColor = Color.Black;
            btnCuentaCorriente.Location = new Point(653, 14);
            btnCuentaCorriente.Name = "btnCuentaCorriente";
            btnCuentaCorriente.Size = new Size(175, 36);
            btnCuentaCorriente.TabIndex = 2;
            btnCuentaCorriente.Text = "💳 Cuenta Corriente";
            btnCuentaCorriente.UseVisualStyleBackColor = false;
            btnCuentaCorriente.Click += btnCuentaCorriente_Click;
            // 
            // btnCancelarVenta
            // 
            btnCancelarVenta.BackColor = Color.FromArgb(239, 68, 68);
            btnCancelarVenta.Cursor = Cursors.Hand;
            btnCancelarVenta.FlatAppearance.BorderSize = 0;
            btnCancelarVenta.FlatStyle = FlatStyle.Flat;
            btnCancelarVenta.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelarVenta.ForeColor = Color.White;
            btnCancelarVenta.Location = new Point(840, 14);
            btnCancelarVenta.Name = "btnCancelarVenta";
            btnCancelarVenta.Size = new Size(175, 36);
            btnCancelarVenta.TabIndex = 6;
            btnCancelarVenta.Text = "🚫 Cancelar Venta";
            btnCancelarVenta.UseVisualStyleBackColor = false;
            btnCancelarVenta.Click += btnCancelarVenta_Click;
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
            Volver_Boton.Location = new Point(1130, 14);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(130, 36);
            Volver_Boton.TabIndex = 3;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // grpCarrito
            // 
            grpCarrito.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpCarrito.BackColor = Color.FromArgb(30, 41, 59);
            grpCarrito.Controls.Add(gridItems);
            grpCarrito.Controls.Add(Quitar_Boton);
            grpCarrito.Controls.Add(btnEnviarMail);
            grpCarrito.Controls.Add(lblTotal);
            grpCarrito.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpCarrito.ForeColor = Color.FromArgb(163, 230, 53);
            grpCarrito.Location = new Point(20, 65);
            grpCarrito.Name = "grpCarrito";
            grpCarrito.Size = new Size(610, 635);
            grpCarrito.TabIndex = 4;
            grpCarrito.TabStop = false;
            grpCarrito.Text = "Carrito de Compras (Productos en la Venta)";
            // 
            // gridItems
            // 
            gridItems.AllowUserToAddRows = false;
            gridItems.AllowUserToDeleteRows = false;
            gridItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridItems.BackgroundColor = Color.FromArgb(15, 23, 42);
            gridItems.BorderStyle = BorderStyle.None;
            gridItems.ColumnHeadersHeight = 32;
            gridItems.GridColor = Color.FromArgb(51, 65, 85);
            gridItems.Location = new Point(15, 28);
            gridItems.MultiSelect = false;
            gridItems.Name = "gridItems";
            gridItems.ReadOnly = true;
            gridItems.RowHeadersVisible = false;
            gridItems.RowHeadersWidth = 51;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.Size = new Size(580, 455);
            gridItems.TabIndex = 0;
            // 
            // Quitar_Boton
            // 
            Quitar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Quitar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Quitar_Boton.Cursor = Cursors.Hand;
            Quitar_Boton.FlatAppearance.BorderSize = 0;
            Quitar_Boton.FlatStyle = FlatStyle.Flat;
            Quitar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Quitar_Boton.ForeColor = Color.White;
            Quitar_Boton.Location = new Point(15, 498);
            Quitar_Boton.Name = "Quitar_Boton";
            Quitar_Boton.Size = new Size(280, 40);
            Quitar_Boton.TabIndex = 1;
            Quitar_Boton.Text = "🗑 Quitar Ítem";
            Quitar_Boton.UseVisualStyleBackColor = false;
            Quitar_Boton.Click += Quitar_Boton_Click;
            // 
            // btnEnviarMail
            // 
            btnEnviarMail.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnviarMail.BackColor = Color.FromArgb(16, 185, 129);
            btnEnviarMail.Cursor = Cursors.Hand;
            btnEnviarMail.FlatAppearance.BorderSize = 0;
            btnEnviarMail.FlatStyle = FlatStyle.Flat;
            btnEnviarMail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEnviarMail.ForeColor = Color.White;
            btnEnviarMail.Location = new Point(315, 498);
            btnEnviarMail.Name = "btnEnviarMail";
            btnEnviarMail.Size = new Size(280, 40);
            btnEnviarMail.TabIndex = 3;
            btnEnviarMail.Text = "✉ Enviar por Email";
            btnEnviarMail.UseVisualStyleBackColor = false;
            btnEnviarMail.Click += btnEnviarMail_Click;
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(163, 230, 53);
            lblTotal.Location = new Point(15, 560);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(345, 32);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "TOTAL A PAGAR: $ 0,00";
            // 
            // btnImprimir
            // 
            btnImprimir.BackColor = Color.FromArgb(59, 130, 246);
            btnImprimir.FlatAppearance.BorderSize = 0;
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnImprimir.ForeColor = Color.White;
            btnImprimir.Location = new Point(190, 498);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(185, 38);
            btnImprimir.TabIndex = 2;
            btnImprimir.Text = "🖨 Imprimir Comp.";
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Visible = false;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // grpNuevaVenta
            // 
            grpNuevaVenta.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpNuevaVenta.BackColor = Color.FromArgb(30, 41, 59);
            grpNuevaVenta.Controls.Add(lblBuscador);
            grpNuevaVenta.Controls.Add(txtBuscador);
            grpNuevaVenta.Controls.Add(lblProducto);
            grpNuevaVenta.Controls.Add(cmbProducto);
            grpNuevaVenta.Controls.Add(lblCantidad);
            grpNuevaVenta.Controls.Add(txtCantidad);
            grpNuevaVenta.Controls.Add(lblPrecio);
            grpNuevaVenta.Controls.Add(txtPrecio);
            grpNuevaVenta.Controls.Add(lblStock);
            grpNuevaVenta.Controls.Add(lblStockValor);
            grpNuevaVenta.Controls.Add(Agregar_Boton);
            grpNuevaVenta.Controls.Add(lblCliente);
            grpNuevaVenta.Controls.Add(cmbCliente);
            grpNuevaVenta.Controls.Add(lblComprobante);
            grpNuevaVenta.Controls.Add(cmbComprobante);
            grpNuevaVenta.Controls.Add(lblMedioPago);
            grpNuevaVenta.Controls.Add(cmbMedioPago);
            grpNuevaVenta.Controls.Add(lblEstadoCaja);
            grpNuevaVenta.Controls.Add(Registrar_Boton);
            grpNuevaVenta.Controls.Add(Limpiar_Boton);
            grpNuevaVenta.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpNuevaVenta.ForeColor = Color.FromArgb(163, 230, 53);
            grpNuevaVenta.Location = new Point(650, 65);
            grpNuevaVenta.Name = "grpNuevaVenta";
            grpNuevaVenta.Size = new Size(610, 635);
            grpNuevaVenta.TabIndex = 5;
            grpNuevaVenta.TabStop = false;
            grpNuevaVenta.Text = "Caja - Cobro de Productos";
            // 
            // lblBuscador
            // 
            lblBuscador.AutoSize = true;
            lblBuscador.ForeColor = Color.FromArgb(163, 230, 53);
            lblBuscador.Location = new Point(20, 28);
            lblBuscador.Name = "lblBuscador";
            lblBuscador.Size = new Size(340, 17);
            lblBuscador.TabIndex = 0;
            lblBuscador.Text = "🔍 Buscador (Escanear Código de Barras o Nombre):";
            // 
            // txtBuscador
            // 
            txtBuscador.BackColor = Color.FromArgb(51, 65, 85);
            txtBuscador.Font = new Font("Segoe UI", 10.5F);
            txtBuscador.ForeColor = Color.White;
            txtBuscador.Location = new Point(20, 52);
            txtBuscador.Name = "txtBuscador";
            txtBuscador.PlaceholderText = "Escanear código de barras o escribir producto...";
            txtBuscador.Size = new Size(570, 26);
            txtBuscador.TabIndex = 1;
            txtBuscador.TextChanged += txtBuscador_TextChanged;
            txtBuscador.KeyDown += txtBuscador_KeyDown;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.ForeColor = Color.White;
            lblProducto.Location = new Point(20, 96);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(290, 17);
            lblProducto.TabIndex = 2;
            lblProducto.Text = "Producto (Último escaneado/seleccionado):";
            // 
            // cmbProducto
            // 
            cmbProducto.BackColor = Color.FromArgb(51, 65, 85);
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.FlatStyle = FlatStyle.Flat;
            cmbProducto.Font = new Font("Segoe UI", 10F);
            cmbProducto.ForeColor = Color.White;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(20, 120);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(570, 25);
            cmbProducto.TabIndex = 3;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.ForeColor = Color.White;
            lblCantidad.Location = new Point(20, 165);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(45, 17);
            lblCantidad.TabIndex = 4;
            lblCantidad.Text = "Cant.:";
            // 
            // txtCantidad
            // 
            txtCantidad.BackColor = Color.FromArgb(51, 65, 85);
            txtCantidad.Font = new Font("Segoe UI", 10F);
            txtCantidad.ForeColor = Color.White;
            txtCantidad.Location = new Point(20, 189);
            txtCantidad.MaxLength = 5;
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(95, 25);
            txtCantidad.TabIndex = 5;
            txtCantidad.Text = "1";
            txtCantidad.TextAlign = HorizontalAlignment.Center;
            txtCantidad.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.ForeColor = Color.White;
            lblPrecio.Location = new Point(135, 165);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(72, 17);
            lblPrecio.TabIndex = 6;
            lblPrecio.Text = "Precio ($):";
            // 
            // txtPrecio
            // 
            txtPrecio.BackColor = Color.FromArgb(30, 41, 59);
            txtPrecio.BorderStyle = BorderStyle.FixedSingle;
            txtPrecio.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtPrecio.ForeColor = Color.FromArgb(163, 230, 53);
            txtPrecio.Location = new Point(135, 189);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.ReadOnly = true;
            txtPrecio.Size = new Size(125, 25);
            txtPrecio.TabIndex = 7;
            txtPrecio.TabStop = false;
            txtPrecio.Text = "0.00";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.ForeColor = Color.White;
            lblStock.Location = new Point(280, 165);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(47, 17);
            lblStock.TabIndex = 8;
            lblStock.Text = "Stock:";
            // 
            // lblStockValor
            // 
            lblStockValor.AutoSize = true;
            lblStockValor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStockValor.ForeColor = Color.FromArgb(163, 230, 53);
            lblStockValor.Location = new Point(280, 192);
            lblStockValor.Name = "lblStockValor";
            lblStockValor.Size = new Size(48, 19);
            lblStockValor.TabIndex = 9;
            lblStockValor.Text = "0 und";
            // 
            // Agregar_Boton
            // 
            Agregar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Agregar_Boton.Cursor = Cursors.Hand;
            Agregar_Boton.FlatAppearance.BorderSize = 0;
            Agregar_Boton.FlatStyle = FlatStyle.Flat;
            Agregar_Boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Agregar_Boton.ForeColor = Color.Black;
            Agregar_Boton.Location = new Point(375, 180);
            Agregar_Boton.Name = "Agregar_Boton";
            Agregar_Boton.Size = new Size(215, 42);
            Agregar_Boton.TabIndex = 10;
            Agregar_Boton.Text = "➕ Agregar al Carrito";
            Agregar_Boton.UseVisualStyleBackColor = false;
            Agregar_Boton.Click += Agregar_Boton_Click;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.ForeColor = Color.White;
            lblCliente.Location = new Point(20, 238);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(56, 17);
            lblCliente.TabIndex = 11;
            lblCliente.Text = "Cliente:";
            // 
            // cmbCliente
            // 
            cmbCliente.BackColor = Color.FromArgb(51, 65, 85);
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.FlatStyle = FlatStyle.Flat;
            cmbCliente.Font = new Font("Segoe UI", 10F);
            cmbCliente.ForeColor = Color.White;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(20, 262);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(570, 25);
            cmbCliente.TabIndex = 12;
            cmbCliente.SelectedIndexChanged += cmbCliente_SelectedIndexChanged;
            // 
            // lblComprobante
            // 
            lblComprobante.AutoSize = true;
            lblComprobante.ForeColor = Color.White;
            lblComprobante.Location = new Point(20, 310);
            lblComprobante.Name = "lblComprobante";
            lblComprobante.Size = new Size(148, 17);
            lblComprobante.TabIndex = 13;
            lblComprobante.Text = "Tipo de Comprobante:";
            // 
            // cmbComprobante
            // 
            cmbComprobante.BackColor = Color.FromArgb(51, 65, 85);
            cmbComprobante.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbComprobante.FlatStyle = FlatStyle.Flat;
            cmbComprobante.Font = new Font("Segoe UI", 10F);
            cmbComprobante.ForeColor = Color.White;
            cmbComprobante.FormattingEnabled = true;
            cmbComprobante.Location = new Point(20, 334);
            cmbComprobante.Name = "cmbComprobante";
            cmbComprobante.Size = new Size(275, 25);
            cmbComprobante.TabIndex = 14;
            // 
            // lblMedioPago
            // 
            lblMedioPago.AutoSize = true;
            lblMedioPago.ForeColor = Color.White;
            lblMedioPago.Location = new Point(315, 310);
            lblMedioPago.Name = "lblMedioPago";
            lblMedioPago.Size = new Size(112, 17);
            lblMedioPago.TabIndex = 15;
            lblMedioPago.Text = "Medio de Cobro:";
            // 
            // cmbMedioPago
            // 
            cmbMedioPago.BackColor = Color.FromArgb(51, 65, 85);
            cmbMedioPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedioPago.FlatStyle = FlatStyle.Flat;
            cmbMedioPago.Font = new Font("Segoe UI", 10F);
            cmbMedioPago.ForeColor = Color.White;
            cmbMedioPago.FormattingEnabled = true;
            cmbMedioPago.Location = new Point(315, 334);
            cmbMedioPago.Name = "cmbMedioPago";
            cmbMedioPago.Size = new Size(275, 25);
            cmbMedioPago.TabIndex = 16;
            // 
            // lblEstadoCaja
            // 
            lblEstadoCaja.AutoSize = true;
            lblEstadoCaja.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEstadoCaja.ForeColor = Color.FromArgb(148, 163, 184);
            lblEstadoCaja.Location = new Point(20, 390);
            lblEstadoCaja.Name = "lblEstadoCaja";
            lblEstadoCaja.Size = new Size(135, 17);
            lblEstadoCaja.TabIndex = 17;
            lblEstadoCaja.Text = "● Verificando caja...";
            // 
            // Registrar_Boton
            // 
            Registrar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Registrar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Registrar_Boton.Cursor = Cursors.Hand;
            Registrar_Boton.FlatAppearance.BorderSize = 0;
            Registrar_Boton.FlatStyle = FlatStyle.Flat;
            Registrar_Boton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            Registrar_Boton.ForeColor = Color.Black;
            Registrar_Boton.Location = new Point(20, 550);
            Registrar_Boton.Name = "Registrar_Boton";
            Registrar_Boton.Size = new Size(340, 58);
            Registrar_Boton.TabIndex = 18;
            Registrar_Boton.Text = "✔ Registrar Cobro / Venta";
            Registrar_Boton.UseVisualStyleBackColor = false;
            Registrar_Boton.Click += Registrar_Boton_Click;
            // 
            // Limpiar_Boton
            // 
            Limpiar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Limpiar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Limpiar_Boton.Cursor = Cursors.Hand;
            Limpiar_Boton.FlatAppearance.BorderSize = 0;
            Limpiar_Boton.FlatStyle = FlatStyle.Flat;
            Limpiar_Boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Limpiar_Boton.ForeColor = Color.White;
            Limpiar_Boton.Location = new Point(390, 550);
            Limpiar_Boton.Name = "Limpiar_Boton";
            Limpiar_Boton.Size = new Size(200, 58);
            Limpiar_Boton.TabIndex = 19;
            Limpiar_Boton.Text = "Limpiar Carrito";
            Limpiar_Boton.UseVisualStyleBackColor = false;
            Limpiar_Boton.Click += Limpiar_Boton_Click;
            // 
            // FormGestionVentas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(btnCobroCanchas);
            Controls.Add(btnCuentaCorriente);
            Controls.Add(btnCancelarVenta);
            Controls.Add(lblTitulo);
            Controls.Add(Volver_Boton);
            Controls.Add(grpCarrito);
            Controls.Add(grpNuevaVenta);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormGestionVentas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Ventas y Caja - Cantina Padel Club";
            grpCarrito.ResumeLayout(false);
            grpCarrito.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridItems).EndInit();
            grpNuevaVenta.ResumeLayout(false);
            grpNuevaVenta.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private Label lblTitulo;
        private Button btnCobroCanchas;
        private Button btnCuentaCorriente;
        private Button btnCancelarVenta;
        private Button Volver_Boton;
        private GroupBox grpCarrito;
        private DataGridView gridItems;
        private Button Quitar_Boton;
        private Button btnImprimir;
        private Button btnEnviarMail;
        private Label lblTotal;
        private GroupBox grpNuevaVenta;
        private Label lblBuscador;
        private TextBox txtBuscador;
        private Label lblProducto;
        private ComboBox cmbProducto;
        private Label lblCantidad;
        private TextBox txtCantidad;
        private Label lblPrecio;
        private TextBox txtPrecio;
        private Label lblStock;
        private Label lblStockValor;
        private Button Agregar_Boton;
        private Label lblCliente;
        private ComboBox cmbCliente;
        private Label lblComprobante;
        private ComboBox cmbComprobante;
        private Label lblMedioPago;
        private ComboBox cmbMedioPago;
        private Label lblEstadoCaja;
        private Button Registrar_Boton;
        private Button Limpiar_Boton;
    }
}
