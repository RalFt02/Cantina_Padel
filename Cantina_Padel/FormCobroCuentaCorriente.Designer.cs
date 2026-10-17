namespace Cantina_Padel
{
    partial class FormCobroCuentaCorriente
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
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            lblSaldoTitulo = new Label();
            lblSaldoValor = new Label();
            lblSaldoFavorTitulo = new Label();
            txtSaldoAFavor = new TextBox();
            lblMovimientos = new Label();
            gridMovimientos = new DataGridView();
            grpAbono = new GroupBox();
            lblMonto = new Label();
            txtMonto = new TextBox();
            lblMedioPago = new Label();
            cmbMedioPago = new ComboBox();
            lblComprobante = new Label();
            cmbComprobante = new ComboBox();
            btnRegistrarPago = new Button();
            btnImprimir = new Button();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).BeginInit();
            grpAbono.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(479, 35);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "COBRO Y ESTADO DE CUENTA CORRIENTE";
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.FromArgb(71, 85, 105);
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(810, 15);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(110, 32);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "← Volver";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCliente.ForeColor = Color.White;
            lblCliente.Location = new Point(20, 60);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(160, 21);
            lblCliente.TabIndex = 2;
            lblCliente.Text = "Seleccionar Cliente:";
            // 
            // cmbCliente
            // 
            cmbCliente.BackColor = Color.FromArgb(51, 65, 85);
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.FlatStyle = FlatStyle.Flat;
            cmbCliente.Font = new Font("Segoe UI", 9.5F);
            cmbCliente.ForeColor = Color.White;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(20, 85);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(390, 29);
            cmbCliente.TabIndex = 3;
            cmbCliente.SelectedIndexChanged += cmbCliente_SelectedIndexChanged;
            // 
            // lblSaldoTitulo
            // 
            lblSaldoTitulo.AutoSize = true;
            lblSaldoTitulo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSaldoTitulo.ForeColor = Color.White;
            lblSaldoTitulo.Location = new Point(430, 60);
            lblSaldoTitulo.Name = "lblSaldoTitulo";
            lblSaldoTitulo.Size = new Size(137, 21);
            lblSaldoTitulo.TabIndex = 4;
            lblSaldoTitulo.Text = "Saldo Adeudado:";
            // 
            // lblSaldoValor
            // 
            lblSaldoValor.AutoSize = true;
            lblSaldoValor.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSaldoValor.ForeColor = Color.FromArgb(239, 68, 68);
            lblSaldoValor.Location = new Point(430, 83);
            lblSaldoValor.Name = "lblSaldoValor";
            lblSaldoValor.Size = new Size(88, 32);
            lblSaldoValor.TabIndex = 5;
            lblSaldoValor.Text = "$ 0,00";
            // 
            // lblSaldoFavorTitulo
            // 
            lblSaldoFavorTitulo.AutoSize = true;
            lblSaldoFavorTitulo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSaldoFavorTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblSaldoFavorTitulo.Location = new Point(620, 60);
            lblSaldoFavorTitulo.Name = "lblSaldoFavorTitulo";
            lblSaldoFavorTitulo.Size = new Size(116, 21);
            lblSaldoFavorTitulo.TabIndex = 6;
            lblSaldoFavorTitulo.Text = "Saldo a Favor:";
            // 
            // txtSaldoAFavor
            // 
            txtSaldoAFavor.BackColor = Color.FromArgb(30, 41, 59);
            txtSaldoAFavor.BorderStyle = BorderStyle.FixedSingle;
            txtSaldoAFavor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtSaldoAFavor.ForeColor = Color.FromArgb(163, 230, 53);
            txtSaldoAFavor.Location = new Point(620, 84);
            txtSaldoAFavor.Name = "txtSaldoAFavor";
            txtSaldoAFavor.ReadOnly = true;
            txtSaldoAFavor.Size = new Size(170, 34);
            txtSaldoAFavor.TabIndex = 7;
            txtSaldoAFavor.TabStop = false;
            txtSaldoAFavor.Text = "$ 0,00";
            // 
            // lblMovimientos
            // 
            lblMovimientos.AutoSize = true;
            lblMovimientos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMovimientos.ForeColor = Color.FromArgb(163, 230, 53);
            lblMovimientos.Location = new Point(20, 130);
            lblMovimientos.Name = "lblMovimientos";
            lblMovimientos.Size = new Size(393, 20);
            lblMovimientos.TabIndex = 8;
            lblMovimientos.Text = "Productos adeudados (ordenados de menor a mayor costo):";
            // 
            // gridMovimientos
            // 
            gridMovimientos.AllowUserToAddRows = false;
            gridMovimientos.AllowUserToDeleteRows = false;
            gridMovimientos.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridMovimientos.BorderStyle = BorderStyle.None;
            gridMovimientos.GridColor = Color.FromArgb(51, 65, 85);
            gridMovimientos.Location = new Point(20, 155);
            gridMovimientos.MultiSelect = false;
            gridMovimientos.Name = "gridMovimientos";
            gridMovimientos.ReadOnly = true;
            gridMovimientos.RowHeadersVisible = false;
            gridMovimientos.RowHeadersWidth = 51;
            gridMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridMovimientos.Size = new Size(540, 395);
            gridMovimientos.TabIndex = 9;
            // 
            // grpAbono
            // 
            grpAbono.BackColor = Color.FromArgb(30, 41, 59);
            grpAbono.Controls.Add(btnImprimir);
            grpAbono.Controls.Add(btnRegistrarPago);
            grpAbono.Controls.Add(cmbComprobante);
            grpAbono.Controls.Add(lblComprobante);
            grpAbono.Controls.Add(cmbMedioPago);
            grpAbono.Controls.Add(lblMedioPago);
            grpAbono.Controls.Add(txtMonto);
            grpAbono.Controls.Add(lblMonto);
            grpAbono.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grpAbono.ForeColor = Color.FromArgb(163, 230, 53);
            grpAbono.Location = new Point(580, 145);
            grpAbono.Name = "grpAbono";
            grpAbono.Size = new Size(340, 405);
            grpAbono.TabIndex = 10;
            grpAbono.TabStop = false;
            grpAbono.Text = "Abonar Deuda";
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.ForeColor = Color.White;
            lblMonto.Location = new Point(20, 35);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(160, 21);
            lblMonto.TabIndex = 0;
            lblMonto.Text = "Monto a abonar ($):";
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(51, 65, 85);
            txtMonto.BorderStyle = BorderStyle.FixedSingle;
            txtMonto.Font = new Font("Segoe UI", 11F);
            txtMonto.ForeColor = Color.White;
            txtMonto.Location = new Point(20, 60);
            txtMonto.MaxLength = 10;
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(300, 32);
            txtMonto.TabIndex = 1;
            txtMonto.KeyPress += SoloNumerosDecimal_KeyPress;
            // 
            // lblMedioPago
            // 
            lblMedioPago.AutoSize = true;
            lblMedioPago.ForeColor = Color.White;
            lblMedioPago.Location = new Point(20, 110);
            lblMedioPago.Name = "lblMedioPago";
            lblMedioPago.Size = new Size(128, 21);
            lblMedioPago.TabIndex = 2;
            lblMedioPago.Text = "Medio de cobro:";
            // 
            // cmbMedioPago
            // 
            cmbMedioPago.BackColor = Color.FromArgb(51, 65, 85);
            cmbMedioPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedioPago.FlatStyle = FlatStyle.Flat;
            cmbMedioPago.ForeColor = Color.White;
            cmbMedioPago.FormattingEnabled = true;
            cmbMedioPago.Location = new Point(20, 135);
            cmbMedioPago.Name = "cmbMedioPago";
            cmbMedioPago.Size = new Size(300, 29);
            cmbMedioPago.TabIndex = 3;
            // 
            // lblComprobante
            // 
            lblComprobante.AutoSize = true;
            lblComprobante.ForeColor = Color.White;
            lblComprobante.Location = new Point(20, 185);
            lblComprobante.Name = "lblComprobante";
            lblComprobante.Size = new Size(174, 21);
            lblComprobante.TabIndex = 4;
            lblComprobante.Text = "Tipo de Comprobante:";
            // 
            // cmbComprobante
            // 
            cmbComprobante.BackColor = Color.FromArgb(51, 65, 85);
            cmbComprobante.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbComprobante.FlatStyle = FlatStyle.Flat;
            cmbComprobante.ForeColor = Color.White;
            cmbComprobante.FormattingEnabled = true;
            cmbComprobante.Location = new Point(20, 210);
            cmbComprobante.Name = "cmbComprobante";
            cmbComprobante.Size = new Size(300, 29);
            cmbComprobante.TabIndex = 5;
            // 
            // btnRegistrarPago
            // 
            btnRegistrarPago.BackColor = Color.FromArgb(163, 230, 53);
            btnRegistrarPago.FlatAppearance.BorderSize = 0;
            btnRegistrarPago.FlatStyle = FlatStyle.Flat;
            btnRegistrarPago.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRegistrarPago.ForeColor = Color.Black;
            btnRegistrarPago.Location = new Point(20, 270);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.Size = new Size(300, 48);
            btnRegistrarPago.TabIndex = 6;
            btnRegistrarPago.Text = "✔ Registrar Cobro";
            btnRegistrarPago.UseVisualStyleBackColor = false;
            btnRegistrarPago.Click += btnRegistrarPago_Click;
            // 
            // btnImprimir
            // 
            btnImprimir.BackColor = Color.FromArgb(59, 130, 246);
            btnImprimir.FlatAppearance.BorderSize = 0;
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnImprimir.ForeColor = Color.White;
            btnImprimir.Location = new Point(20, 335);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(300, 42);
            btnImprimir.TabIndex = 7;
            btnImprimir.Text = "🖨 Imprimir Recibo";
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // FormCobroCuentaCorriente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(940, 570);
            Controls.Add(grpAbono);
            Controls.Add(gridMovimientos);
            Controls.Add(lblMovimientos);
            Controls.Add(txtSaldoAFavor);
            Controls.Add(lblSaldoFavorTitulo);
            Controls.Add(lblSaldoValor);
            Controls.Add(lblSaldoTitulo);
            Controls.Add(cmbCliente);
            Controls.Add(lblCliente);
            Controls.Add(btnCerrar);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormCobroCuentaCorriente";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cobro de Cuenta Corriente";
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).EndInit();
            grpAbono.ResumeLayout(false);
            grpAbono.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Button btnCerrar;
        private Label lblCliente;
        private ComboBox cmbCliente;
        private Label lblSaldoTitulo;
        private Label lblSaldoValor;
        private Label lblSaldoFavorTitulo;
        private TextBox txtSaldoAFavor;
        private Label lblMovimientos;
        private DataGridView gridMovimientos;
        private GroupBox grpAbono;
        private Label lblMonto;
        private TextBox txtMonto;
        private Label lblMedioPago;
        private ComboBox cmbMedioPago;
        private Label lblComprobante;
        private ComboBox cmbComprobante;
        private Button btnRegistrarPago;
        private Button btnImprimir;
    }
}
