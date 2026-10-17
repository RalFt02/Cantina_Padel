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
            gridVentas = new DataGridView();
            gridDetalle = new DataGridView();
            gridItems = new DataGridView();
            lblTitulo = new Label();
            lblVentas = new Label();
            lblDetalle = new Label();
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
            Quitar_Boton = new Button();
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            lblComprobante = new Label();
            cmbComprobante = new ComboBox();
            lblMedioPago = new Label();
            cmbMedioPago = new ComboBox();
            lblEstadoCaja = new Label();
            lblTotal = new Label();
            grpNuevaVenta = new GroupBox();
            Registrar_Boton = new Button();
            Limpiar_Boton = new Button();
            Eliminar_Boton = new Button();
            btnImprimir = new Button();
            btnEnviarMail = new Button();
            btnCobroCanchas = new Button();
            btnCuentaCorriente = new Button();
            Volver_Boton = new Button();
            ((System.ComponentModel.ISupportInitialize)gridVentas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridDetalle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridItems).BeginInit();
            grpNuevaVenta.SuspendLayout();
            SuspendLayout();
            // 
            // gridVentas
            // 
            gridVentas.AllowUserToAddRows = false;
            gridVentas.AllowUserToDeleteRows = false;
            gridVentas.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridVentas.BorderStyle = BorderStyle.None;
            gridVentas.ColumnHeadersHeight = 29;
            gridVentas.GridColor = Color.FromArgb(51, 65, 85);
            gridVentas.Location = new Point(20, 92);
            gridVentas.MultiSelect = false;
            gridVentas.Name = "gridVentas";
            gridVentas.ReadOnly = true;
            gridVentas.RowHeadersVisible = false;
            gridVentas.RowHeadersWidth = 51;
            gridVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVentas.Size = new Size(600, 275);
            gridVentas.TabIndex = 5;
            gridVentas.CellClick += gridVentas_CellClick;
            // 
            // gridDetalle
            // 
            gridDetalle.AllowUserToAddRows = false;
            gridDetalle.AllowUserToDeleteRows = false;
            gridDetalle.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridDetalle.BorderStyle = BorderStyle.None;
            gridDetalle.ColumnHeadersHeight = 29;
            gridDetalle.GridColor = Color.FromArgb(51, 65, 85);
            gridDetalle.Location = new Point(20, 452);
            gridDetalle.MultiSelect = false;
            gridDetalle.Name = "gridDetalle";
            gridDetalle.ReadOnly = true;
            gridDetalle.RowHeadersVisible = false;
            gridDetalle.RowHeadersWidth = 51;
            gridDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridDetalle.Size = new Size(600, 225);
            gridDetalle.TabIndex = 10;
            // 
            // gridItems
            // 
            gridItems.AllowUserToAddRows = false;
            gridItems.AllowUserToDeleteRows = false;
            gridItems.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridItems.BorderStyle = BorderStyle.None;
            gridItems.ColumnHeadersHeight = 29;
            gridItems.GridColor = Color.FromArgb(51, 65, 85);
            gridItems.Location = new Point(12, 318);
            gridItems.MultiSelect = false;
            gridItems.Name = "gridItems";
            gridItems.ReadOnly = true;
            gridItems.RowHeadersVisible = false;
            gridItems.RowHeadersWidth = 51;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.Size = new Size(573, 185);
            gridItems.TabIndex = 19;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(280, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE VENTAS";
            // 
            // lblVentas
            // 
            lblVentas.AutoSize = true;
            lblVentas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVentas.ForeColor = Color.FromArgb(163, 230, 53);
            lblVentas.Location = new Point(20, 65);
            lblVentas.Name = "lblVentas";
            lblVentas.Size = new Size(142, 20);
            lblVentas.TabIndex = 4;
            lblVentas.Text = "Ventas registradas:";
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDetalle.ForeColor = Color.FromArgb(163, 230, 53);
            lblDetalle.Location = new Point(20, 425);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(234, 20);
            lblDetalle.TabIndex = 9;
            lblDetalle.Text = "Detalle de la venta seleccionada:";
            // 
            // lblBuscador
            // 
            lblBuscador.AutoSize = true;
            lblBuscador.ForeColor = Color.FromArgb(163, 230, 53);
            lblBuscador.Location = new Point(12, 22);
            lblBuscador.Name = "lblBuscador";
            lblBuscador.Size = new Size(379, 20);
            lblBuscador.TabIndex = 0;
            lblBuscador.Text = "🔍 Buscador (Escanear Código de Barras o Nombre):";
            // 
            // txtBuscador
            // 
            txtBuscador.BackColor = Color.FromArgb(51, 65, 85);
            txtBuscador.BorderStyle = BorderStyle.FixedSingle;
            txtBuscador.Font = new Font("Segoe UI", 10.5F);
            txtBuscador.ForeColor = Color.White;
            txtBuscador.Location = new Point(12, 44);
            txtBuscador.Name = "txtBuscador";
            txtBuscador.Size = new Size(573, 31);
            txtBuscador.TabIndex = 1;
            txtBuscador.TextChanged += txtBuscador_TextChanged;
            txtBuscador.KeyDown += txtBuscador_KeyDown;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.ForeColor = Color.White;
            lblProducto.Location = new Point(12, 79);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(159, 20);
            lblProducto.TabIndex = 2;
            lblProducto.Text = "Seleccionar Producto:";
            // 
            // cmbProducto
            // 
            cmbProducto.BackColor = Color.FromArgb(51, 65, 85);
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.FlatStyle = FlatStyle.Flat;
            cmbProducto.ForeColor = Color.White;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(12, 101);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(573, 28);
            cmbProducto.TabIndex = 3;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.ForeColor = Color.White;
            lblCantidad.Location = new Point(12, 137);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(75, 20);
            lblCantidad.TabIndex = 4;
            lblCantidad.Text = "Cantidad:";
            // 
            // txtCantidad
            // 
            txtCantidad.BackColor = Color.FromArgb(51, 65, 85);
            txtCantidad.BorderStyle = BorderStyle.FixedSingle;
            txtCantidad.ForeColor = Color.White;
            txtCantidad.Location = new Point(12, 158);
            txtCantidad.MaxLength = 5;
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(75, 27);
            txtCantidad.TabIndex = 5;
            txtCantidad.Text = "1";
            txtCantidad.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.ForeColor = Color.White;
            lblPrecio.Location = new Point(95, 137);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(100, 20);
            lblPrecio.TabIndex = 6;
            lblPrecio.Text = "Precio Venta:";
            // 
            // txtPrecio
            // 
            txtPrecio.BackColor = Color.FromArgb(30, 41, 59);
            txtPrecio.BorderStyle = BorderStyle.FixedSingle;
            txtPrecio.ForeColor = Color.FromArgb(163, 230, 53);
            txtPrecio.Location = new Point(95, 158);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.ReadOnly = true;
            txtPrecio.Size = new Size(95, 27);
            txtPrecio.TabIndex = 7;
            txtPrecio.TabStop = false;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.ForeColor = Color.White;
            lblStock.Location = new Point(198, 137);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(52, 20);
            lblStock.TabIndex = 8;
            lblStock.Text = "Stock:";
            // 
            // lblStockValor
            // 
            lblStockValor.AutoSize = true;
            lblStockValor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStockValor.ForeColor = Color.FromArgb(148, 163, 184);
            lblStockValor.Location = new Point(198, 162);
            lblStockValor.Name = "lblStockValor";
            lblStockValor.Size = new Size(49, 20);
            lblStockValor.TabIndex = 9;
            lblStockValor.Text = "0 und";
            // 
            // Agregar_Boton
            // 
            Agregar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Agregar_Boton.FlatAppearance.BorderSize = 0;
            Agregar_Boton.FlatStyle = FlatStyle.Flat;
            Agregar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Agregar_Boton.ForeColor = Color.Black;
            Agregar_Boton.Location = new Point(285, 154);
            Agregar_Boton.Name = "Agregar_Boton";
            Agregar_Boton.Size = new Size(140, 34);
            Agregar_Boton.TabIndex = 10;
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
            Quitar_Boton.Location = new Point(435, 154);
            Quitar_Boton.Name = "Quitar_Boton";
            Quitar_Boton.Size = new Size(150, 34);
            Quitar_Boton.TabIndex = 11;
            Quitar_Boton.Text = "Quitar ítem";
            Quitar_Boton.UseVisualStyleBackColor = false;
            Quitar_Boton.Click += Quitar_Boton_Click;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.ForeColor = Color.White;
            lblCliente.Location = new Point(12, 198);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(61, 20);
            lblCliente.TabIndex = 12;
            lblCliente.Text = "Cliente:";
            // 
            // cmbCliente
            // 
            cmbCliente.BackColor = Color.FromArgb(51, 65, 85);
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.FlatStyle = FlatStyle.Flat;
            cmbCliente.ForeColor = Color.White;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(12, 220);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(278, 28);
            cmbCliente.TabIndex = 13;
            // 
            // lblComprobante
            // 
            lblComprobante.AutoSize = true;
            lblComprobante.ForeColor = Color.White;
            lblComprobante.Location = new Point(305, 198);
            lblComprobante.Name = "lblComprobante";
            lblComprobante.Size = new Size(165, 20);
            lblComprobante.TabIndex = 14;
            lblComprobante.Text = "Tipo de Comprobante:";
            // 
            // cmbComprobante
            // 
            cmbComprobante.BackColor = Color.FromArgb(51, 65, 85);
            cmbComprobante.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbComprobante.FlatStyle = FlatStyle.Flat;
            cmbComprobante.ForeColor = Color.White;
            cmbComprobante.FormattingEnabled = true;
            cmbComprobante.Location = new Point(305, 220);
            cmbComprobante.Name = "cmbComprobante";
            cmbComprobante.Size = new Size(280, 28);
            cmbComprobante.TabIndex = 15;
            // 
            // lblMedioPago
            // 
            lblMedioPago.AutoSize = true;
            lblMedioPago.ForeColor = Color.White;
            lblMedioPago.Location = new Point(12, 256);
            lblMedioPago.Name = "lblMedioPago";
            lblMedioPago.Size = new Size(117, 20);
            lblMedioPago.TabIndex = 16;
            lblMedioPago.Text = "Medio de pago:";
            // 
            // cmbMedioPago
            // 
            cmbMedioPago.BackColor = Color.FromArgb(51, 65, 85);
            cmbMedioPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedioPago.FlatStyle = FlatStyle.Flat;
            cmbMedioPago.ForeColor = Color.White;
            cmbMedioPago.FormattingEnabled = true;
            cmbMedioPago.Location = new Point(12, 278);
            cmbMedioPago.Name = "cmbMedioPago";
            cmbMedioPago.Size = new Size(278, 28);
            cmbMedioPago.TabIndex = 17;
            // 
            // lblEstadoCaja
            // 
            lblEstadoCaja.AutoSize = true;
            lblEstadoCaja.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblEstadoCaja.ForeColor = Color.FromArgb(148, 163, 184);
            lblEstadoCaja.Location = new Point(305, 282);
            lblEstadoCaja.Name = "lblEstadoCaja";
            lblEstadoCaja.Size = new Size(144, 20);
            lblEstadoCaja.TabIndex = 18;
            lblEstadoCaja.Text = "● Verificando caja...";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(163, 230, 53);
            lblTotal.Location = new Point(12, 510);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(174, 37);
            lblTotal.TabIndex = 20;
            lblTotal.Text = "Total: $ 0,00";
            // 
            // grpNuevaVenta
            // 
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
            grpNuevaVenta.Controls.Add(Quitar_Boton);
            grpNuevaVenta.Controls.Add(lblCliente);
            grpNuevaVenta.Controls.Add(cmbCliente);
            grpNuevaVenta.Controls.Add(lblComprobante);
            grpNuevaVenta.Controls.Add(cmbComprobante);
            grpNuevaVenta.Controls.Add(lblMedioPago);
            grpNuevaVenta.Controls.Add(cmbMedioPago);
            grpNuevaVenta.Controls.Add(lblEstadoCaja);
            grpNuevaVenta.Controls.Add(gridItems);
            grpNuevaVenta.Controls.Add(lblTotal);
            grpNuevaVenta.Controls.Add(Registrar_Boton);
            grpNuevaVenta.Controls.Add(Limpiar_Boton);
            grpNuevaVenta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpNuevaVenta.ForeColor = Color.FromArgb(163, 230, 53);
            grpNuevaVenta.Location = new Point(640, 65);
            grpNuevaVenta.Name = "grpNuevaVenta";
            grpNuevaVenta.Size = new Size(600, 612);
            grpNuevaVenta.TabIndex = 11;
            grpNuevaVenta.TabStop = false;
            grpNuevaVenta.Text = "Nueva venta de Cantina";
            // 
            // Registrar_Boton
            // 
            Registrar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Registrar_Boton.FlatAppearance.BorderSize = 0;
            Registrar_Boton.FlatStyle = FlatStyle.Flat;
            Registrar_Boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Registrar_Boton.ForeColor = Color.Black;
            Registrar_Boton.Location = new Point(12, 552);
            Registrar_Boton.Name = "Registrar_Boton";
            Registrar_Boton.Size = new Size(280, 48);
            Registrar_Boton.TabIndex = 21;
            Registrar_Boton.Text = "✔ Registrar venta";
            Registrar_Boton.UseVisualStyleBackColor = false;
            Registrar_Boton.Click += Registrar_Boton_Click;
            // 
            // Limpiar_Boton
            // 
            Limpiar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Limpiar_Boton.FlatAppearance.BorderSize = 0;
            Limpiar_Boton.FlatStyle = FlatStyle.Flat;
            Limpiar_Boton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Limpiar_Boton.ForeColor = Color.White;
            Limpiar_Boton.Location = new Point(305, 552);
            Limpiar_Boton.Name = "Limpiar_Boton";
            Limpiar_Boton.Size = new Size(280, 48);
            Limpiar_Boton.TabIndex = 22;
            Limpiar_Boton.Text = "Limpiar Carrito";
            Limpiar_Boton.UseVisualStyleBackColor = false;
            Limpiar_Boton.Click += Limpiar_Boton_Click;
            // 
            // Eliminar_Boton
            // 
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.Location = new Point(20, 377);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(185, 36);
            Eliminar_Boton.TabIndex = 6;
            Eliminar_Boton.Text = "✖ Anular Venta";
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;
            // 
            // btnImprimir
            // 
            btnImprimir.BackColor = Color.FromArgb(59, 130, 246);
            btnImprimir.FlatAppearance.BorderSize = 0;
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnImprimir.ForeColor = Color.White;
            btnImprimir.Location = new Point(215, 377);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(195, 36);
            btnImprimir.TabIndex = 7;
            btnImprimir.Text = "🖨 Imprimir Comprobante";
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // btnEnviarMail
            // 
            btnEnviarMail.BackColor = Color.FromArgb(16, 185, 129);
            btnEnviarMail.FlatAppearance.BorderSize = 0;
            btnEnviarMail.FlatStyle = FlatStyle.Flat;
            btnEnviarMail.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnEnviarMail.ForeColor = Color.White;
            btnEnviarMail.Location = new Point(420, 377);
            btnEnviarMail.Name = "btnEnviarMail";
            btnEnviarMail.Size = new Size(200, 36);
            btnEnviarMail.TabIndex = 8;
            btnEnviarMail.Text = "✉ Enviar por Email";
            btnEnviarMail.UseVisualStyleBackColor = false;
            btnEnviarMail.Click += btnEnviarMail_Click;
            // 
            // btnCobroCanchas
            // 
            btnCobroCanchas.BackColor = Color.FromArgb(14, 165, 233);
            btnCobroCanchas.FlatAppearance.BorderSize = 0;
            btnCobroCanchas.FlatStyle = FlatStyle.Flat;
            btnCobroCanchas.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCobroCanchas.ForeColor = Color.White;
            btnCobroCanchas.Location = new Point(570, 14);
            btnCobroCanchas.Name = "btnCobroCanchas";
            btnCobroCanchas.Size = new Size(185, 36);
            btnCobroCanchas.TabIndex = 1;
            btnCobroCanchas.Text = "🎾 Cobro de Canchas";
            btnCobroCanchas.UseVisualStyleBackColor = false;
            btnCobroCanchas.Click += btnCobroCanchas_Click;
            // 
            // btnCuentaCorriente
            // 
            btnCuentaCorriente.BackColor = Color.FromArgb(245, 158, 11);
            btnCuentaCorriente.FlatAppearance.BorderSize = 0;
            btnCuentaCorriente.FlatStyle = FlatStyle.Flat;
            btnCuentaCorriente.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCuentaCorriente.ForeColor = Color.Black;
            btnCuentaCorriente.Location = new Point(770, 14);
            btnCuentaCorriente.Name = "btnCuentaCorriente";
            btnCuentaCorriente.Size = new Size(185, 36);
            btnCuentaCorriente.TabIndex = 2;
            btnCuentaCorriente.Text = "💳 Cuenta Corriente";
            btnCuentaCorriente.UseVisualStyleBackColor = false;
            btnCuentaCorriente.Click += btnCuentaCorriente_Click;
            // 
            // Volver_Boton
            // 
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(1125, 14);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(115, 36);
            Volver_Boton.TabIndex = 3;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // FormGestionVentas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1260, 695);
            Controls.Add(btnCobroCanchas);
            Controls.Add(btnCuentaCorriente);
            Controls.Add(lblTitulo);
            Controls.Add(Volver_Boton);
            Controls.Add(lblVentas);
            Controls.Add(gridVentas);
            Controls.Add(Eliminar_Boton);
            Controls.Add(btnImprimir);
            Controls.Add(btnEnviarMail);
            Controls.Add(lblDetalle);
            Controls.Add(gridDetalle);
            Controls.Add(grpNuevaVenta);
            Name = "FormGestionVentas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Ventas";
            ((System.ComponentModel.ISupportInitialize)gridVentas).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridDetalle).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridItems).EndInit();
            grpNuevaVenta.ResumeLayout(false);
            grpNuevaVenta.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gridVentas;
        private DataGridView gridDetalle;
        private DataGridView gridItems;
        private Label lblTitulo;
        private Label lblVentas;
        private Label lblDetalle;
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
        private Label lblCliente;
        private ComboBox cmbCliente;
        private Label lblComprobante;
        private ComboBox cmbComprobante;
        private Label lblMedioPago;
        private ComboBox cmbMedioPago;
        private Label lblEstadoCaja;
        private Label lblTotal;
        private GroupBox grpNuevaVenta;
        private Button Agregar_Boton;
        private Button Quitar_Boton;
        private Button Registrar_Boton;
        private Button Limpiar_Boton;
        private Button Eliminar_Boton;
        private Button btnImprimir;
        private Button btnEnviarMail;
        private Button btnCobroCanchas;
        private Button btnCuentaCorriente;
        private Button Volver_Boton;
    }
}
