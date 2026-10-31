namespace Cantina_Padel
{
    partial class FormGestionCaja
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblTitulo = new Label();
            lblEstadoCaja = new Label();
            lblCajero = new Label();
            lblApertura = new Label();
            lblMontoInicio = new Label();
            lblDisponible = new Label();
            grpAbrir = new GroupBox();
            lblMontoInicioLbl = new Label();
            txtMontoInicio = new TextBox();
            Abrir_Boton = new Button();
            grpCerrar = new GroupBox();
            lblMontoCierreLbl = new Label();
            lblMontoCierre = new Label();
            lblTotalExtraccionesLbl = new Label();
            lblTotalExtracciones = new Label();
            Cerrar_Boton = new Button();
            grpRetiro = new GroupBox();
            grpFacturacion = new GroupBox();
            lblFactEfectivoTitulo = new Label();
            lblFactEfectivoValor = new Label();
            lblFactDebitoTitulo = new Label();
            lblFactDebitoValor = new Label();
            lblFactCreditoTitulo = new Label();
            lblFactCreditoValor = new Label();
            lblFactBilleteraTitulo = new Label();
            lblFactBilleteraValor = new Label();
            lblMontoRetiroLbl = new Label();
            txtMontoRetiro = new TextBox();
            lblMotivoLbl = new Label();
            txtMotivo = new TextBox();
            Retiro_Boton = new Button();
            lblRetiros = new Label();
            gridRetiros = new DataGridView();
            Volver_Boton = new Button();
            grpAbrir.SuspendLayout();
            grpCerrar.SuspendLayout();
            grpRetiro.SuspendLayout();
            grpFacturacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridRetiros).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(246, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE CAJA";
            // 
            // lblEstadoCaja
            // 
            lblEstadoCaja.AutoSize = true;
            lblEstadoCaja.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEstadoCaja.ForeColor = Color.IndianRed;
            lblEstadoCaja.Location = new Point(20, 55);
            lblEstadoCaja.Name = "lblEstadoCaja";
            lblEstadoCaja.Size = new Size(165, 25);
            lblEstadoCaja.TabIndex = 1;
            lblEstadoCaja.Text = "● CAJA CERRADA";
            // 
            // lblCajero
            // 
            lblCajero.AutoSize = true;
            lblCajero.ForeColor = Color.LightGray;
            lblCajero.Location = new Point(220, 55);
            lblCajero.Name = "lblCajero";
            lblCajero.Size = new Size(0, 20);
            lblCajero.TabIndex = 2;
            // 
            // lblApertura
            // 
            lblApertura.AutoSize = true;
            lblApertura.ForeColor = Color.LightGray;
            lblApertura.Location = new Point(420, 55);
            lblApertura.Name = "lblApertura";
            lblApertura.Size = new Size(0, 20);
            lblApertura.TabIndex = 3;
            // 
            // lblMontoInicio
            // 
            lblMontoInicio.AutoSize = true;
            lblMontoInicio.ForeColor = Color.LightGray;
            lblMontoInicio.Location = new Point(650, 55);
            lblMontoInicio.Name = "lblMontoInicio";
            lblMontoInicio.Size = new Size(0, 20);
            lblMontoInicio.TabIndex = 4;
            // 
            // lblDisponible
            // 
            lblDisponible.AutoSize = true;
            lblDisponible.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDisponible.ForeColor = Color.FromArgb(163, 230, 53);
            lblDisponible.Location = new Point(850, 55);
            lblDisponible.Name = "lblDisponible";
            lblDisponible.Size = new Size(0, 23);
            lblDisponible.TabIndex = 5;
            // 
            // grpAbrir
            // 
            grpAbrir.BackColor = Color.FromArgb(30, 41, 59);
            grpAbrir.Controls.Add(lblMontoInicioLbl);
            grpAbrir.Controls.Add(txtMontoInicio);
            grpAbrir.Controls.Add(Abrir_Boton);
            grpAbrir.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpAbrir.ForeColor = Color.FromArgb(163, 230, 53);
            grpAbrir.Location = new Point(20, 90);
            grpAbrir.Name = "grpAbrir";
            grpAbrir.Size = new Size(185, 165);
            grpAbrir.TabIndex = 6;
            grpAbrir.TabStop = false;
            grpAbrir.Text = "Abrir Caja";
            // 
            // lblMontoInicioLbl
            // 
            lblMontoInicioLbl.AutoSize = true;
            lblMontoInicioLbl.ForeColor = Color.White;
            lblMontoInicioLbl.Location = new Point(10, 20);
            lblMontoInicioLbl.Name = "lblMontoInicioLbl";
            lblMontoInicioLbl.Size = new Size(126, 20);
            lblMontoInicioLbl.TabIndex = 0;
            lblMontoInicioLbl.Text = "Monto inicio ($):";
            // 
            // txtMontoInicio
            // 
            txtMontoInicio.BackColor = Color.FromArgb(51, 65, 85);
            txtMontoInicio.ForeColor = Color.White;
            txtMontoInicio.Location = new Point(10, 45);
            txtMontoInicio.Name = "txtMontoInicio";
            txtMontoInicio.PlaceholderText = "0.00";
            txtMontoInicio.Size = new Size(160, 27);
            txtMontoInicio.TabIndex = 1;
            // 
            // Abrir_Boton
            // 
            Abrir_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Abrir_Boton.FlatAppearance.BorderSize = 0;
            Abrir_Boton.FlatStyle = FlatStyle.Flat;
            Abrir_Boton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            Abrir_Boton.ForeColor = Color.Black;
            Abrir_Boton.Location = new Point(10, 78);
            Abrir_Boton.Name = "Abrir_Boton";
            Abrir_Boton.Size = new Size(160, 26);
            Abrir_Boton.TabIndex = 2;
            Abrir_Boton.Text = "Abrir Caja";
            Abrir_Boton.UseVisualStyleBackColor = false;
            Abrir_Boton.Click += Abrir_Boton_Click;
            // 
            // grpCerrar
            // 
            grpCerrar.BackColor = Color.FromArgb(30, 41, 59);
            grpCerrar.Controls.Add(lblMontoCierreLbl);
            grpCerrar.Controls.Add(lblMontoCierre);
            grpCerrar.Controls.Add(lblTotalExtraccionesLbl);
            grpCerrar.Controls.Add(lblTotalExtracciones);
            grpCerrar.Controls.Add(Cerrar_Boton);
            grpCerrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpCerrar.ForeColor = Color.FromArgb(163, 230, 53);
            grpCerrar.Location = new Point(220, 90);
            grpCerrar.Name = "grpCerrar";
            grpCerrar.Size = new Size(185, 165);
            grpCerrar.TabIndex = 7;
            grpCerrar.TabStop = false;
            grpCerrar.Text = "Cerrar Caja";
            // 
            // lblMontoCierreLbl
            // 
            lblMontoCierreLbl.AutoSize = true;
            lblMontoCierreLbl.ForeColor = Color.White;
            lblMontoCierreLbl.Location = new Point(10, 20);
            lblMontoCierreLbl.Name = "lblMontoCierreLbl";
            lblMontoCierreLbl.Size = new Size(128, 20);
            lblMontoCierreLbl.TabIndex = 0;
            lblMontoCierreLbl.Text = "Monto cierre ($):";
            // 
            // lblMontoCierre
            // 
            lblMontoCierre.BackColor = Color.FromArgb(51, 65, 85);
            lblMontoCierre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMontoCierre.ForeColor = Color.Gray;
            lblMontoCierre.Location = new Point(10, 45);
            lblMontoCierre.Name = "lblMontoCierre";
            lblMontoCierre.Padding = new Padding(6, 0, 0, 0);
            lblMontoCierre.Size = new Size(160, 27);
            lblMontoCierre.TabIndex = 1;
            lblMontoCierre.Text = "Cerrada";
            lblMontoCierre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalExtraccionesLbl
            // 
            lblTotalExtraccionesLbl.AutoSize = true;
            lblTotalExtraccionesLbl.ForeColor = Color.White;
            lblTotalExtraccionesLbl.Location = new Point(10, 76);
            lblTotalExtraccionesLbl.Name = "lblTotalExtraccionesLbl";
            lblTotalExtraccionesLbl.Size = new Size(160, 20);
            lblTotalExtraccionesLbl.TabIndex = 3;
            lblTotalExtraccionesLbl.Text = "Total extracciones ($):";
            // 
            // lblTotalExtracciones
            // 
            lblTotalExtracciones.BackColor = Color.FromArgb(51, 65, 85);
            lblTotalExtracciones.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalExtracciones.ForeColor = Color.Gray;
            lblTotalExtracciones.Location = new Point(10, 98);
            lblTotalExtracciones.Name = "lblTotalExtracciones";
            lblTotalExtracciones.Padding = new Padding(6, 0, 0, 0);
            lblTotalExtracciones.Size = new Size(160, 27);
            lblTotalExtracciones.TabIndex = 4;
            lblTotalExtracciones.Text = "Cerrada";
            lblTotalExtracciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Cerrar_Boton
            // 
            Cerrar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Cerrar_Boton.FlatAppearance.BorderSize = 0;
            Cerrar_Boton.FlatStyle = FlatStyle.Flat;
            Cerrar_Boton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            Cerrar_Boton.ForeColor = Color.White;
            Cerrar_Boton.Location = new Point(10, 130);
            Cerrar_Boton.Name = "Cerrar_Boton";
            Cerrar_Boton.Size = new Size(160, 26);
            Cerrar_Boton.TabIndex = 2;
            Cerrar_Boton.Text = "Cerrar Caja";
            Cerrar_Boton.UseVisualStyleBackColor = false;
            Cerrar_Boton.Click += Cerrar_Boton_Click;
            // 
            // grpRetiro
            // 
            grpRetiro.BackColor = Color.FromArgb(30, 41, 59);
            grpRetiro.Controls.Add(lblMontoRetiroLbl);
            grpRetiro.Controls.Add(txtMontoRetiro);
            grpRetiro.Controls.Add(lblMotivoLbl);
            grpRetiro.Controls.Add(txtMotivo);
            grpRetiro.Controls.Add(Retiro_Boton);
            grpRetiro.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpRetiro.ForeColor = Color.FromArgb(163, 230, 53);
            grpRetiro.Location = new Point(415, 90);
            grpRetiro.Name = "grpRetiro";
            grpRetiro.Size = new Size(315, 165);
            grpRetiro.TabIndex = 8;
            grpRetiro.TabStop = false;
            grpRetiro.Text = "Retiro de Efectivo";
            // 
            // lblMontoRetiroLbl
            // 
            lblMontoRetiroLbl.AutoSize = true;
            lblMontoRetiroLbl.ForeColor = Color.White;
            lblMontoRetiroLbl.Location = new Point(10, 28);
            lblMontoRetiroLbl.Name = "lblMontoRetiroLbl";
            lblMontoRetiroLbl.Size = new Size(85, 20);
            lblMontoRetiroLbl.TabIndex = 0;
            lblMontoRetiroLbl.Text = "Monto ($):";
            // 
            // txtMontoRetiro
            // 
            txtMontoRetiro.BackColor = Color.FromArgb(51, 65, 85);
            txtMontoRetiro.ForeColor = Color.White;
            txtMontoRetiro.Location = new Point(10, 50);
            txtMontoRetiro.Name = "txtMontoRetiro";
            txtMontoRetiro.PlaceholderText = "0.00";
            txtMontoRetiro.Size = new Size(95, 27);
            txtMontoRetiro.TabIndex = 1;
            // 
            // lblMotivoLbl
            // 
            lblMotivoLbl.AutoSize = true;
            lblMotivoLbl.ForeColor = Color.White;
            lblMotivoLbl.Location = new Point(115, 28);
            lblMotivoLbl.Name = "lblMotivoLbl";
            lblMotivoLbl.Size = new Size(63, 20);
            lblMotivoLbl.TabIndex = 2;
            lblMotivoLbl.Text = "Motivo:";
            // 
            // txtMotivo
            // 
            txtMotivo.BackColor = Color.FromArgb(51, 65, 85);
            txtMotivo.ForeColor = Color.White;
            txtMotivo.Location = new Point(115, 50);
            txtMotivo.Name = "txtMotivo";
            txtMotivo.PlaceholderText = "Ej:Extraccion de Cliente";
            txtMotivo.Size = new Size(185, 27);
            txtMotivo.TabIndex = 3;
            // 
            // Retiro_Boton
            // 
            Retiro_Boton.BackColor = Color.FromArgb(245, 158, 11);
            Retiro_Boton.FlatAppearance.BorderSize = 0;
            Retiro_Boton.FlatStyle = FlatStyle.Flat;
            Retiro_Boton.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            Retiro_Boton.ForeColor = Color.Black;
            Retiro_Boton.Location = new Point(10, 95);
            Retiro_Boton.Name = "Retiro_Boton";
            Retiro_Boton.Size = new Size(290, 36);
            Retiro_Boton.TabIndex = 4;
            Retiro_Boton.Text = "Registrar Retiro";
            Retiro_Boton.UseVisualStyleBackColor = false;
            Retiro_Boton.Click += Retiro_Boton_Click;
                        // 
            // grpFacturacion
            // 
            grpFacturacion.BackColor = Color.FromArgb(30, 41, 59);
            grpFacturacion.Controls.Add(lblFactEfectivoTitulo);
            grpFacturacion.Controls.Add(lblFactEfectivoValor);
            grpFacturacion.Controls.Add(lblFactDebitoTitulo);
            grpFacturacion.Controls.Add(lblFactDebitoValor);
            grpFacturacion.Controls.Add(lblFactCreditoTitulo);
            grpFacturacion.Controls.Add(lblFactCreditoValor);
            grpFacturacion.Controls.Add(lblFactBilleteraTitulo);
            grpFacturacion.Controls.Add(lblFactBilleteraValor);
            grpFacturacion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpFacturacion.ForeColor = Color.FromArgb(163, 230, 53);
            grpFacturacion.Location = new Point(740, 90);
            grpFacturacion.Name = "grpFacturacion";
            grpFacturacion.Size = new Size(340, 165);
            grpFacturacion.TabIndex = 9;
            grpFacturacion.TabStop = false;
            grpFacturacion.Text = "Facturación por Medio de Pago";
            // 
            // lblFactEfectivoTitulo
            // 
            lblFactEfectivoTitulo.AutoSize = true;
            lblFactEfectivoTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblFactEfectivoTitulo.ForeColor = Color.White;
            lblFactEfectivoTitulo.Location = new Point(12, 28);
            lblFactEfectivoTitulo.Name = "lblFactEfectivoTitulo";
            lblFactEfectivoTitulo.Size = new Size(65, 19);
            lblFactEfectivoTitulo.TabIndex = 0;
            lblFactEfectivoTitulo.Text = "Efectivo:";
            // 
            // lblFactEfectivoValor
            // 
            lblFactEfectivoValor.AutoSize = true;
            lblFactEfectivoValor.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblFactEfectivoValor.ForeColor = Color.FromArgb(163, 230, 53);
            lblFactEfectivoValor.Location = new Point(12, 48);
            lblFactEfectivoValor.Name = "lblFactEfectivoValor";
            lblFactEfectivoValor.Size = new Size(59, 25);
            lblFactEfectivoValor.TabIndex = 1;
            lblFactEfectivoValor.Text = "$ 0,00";
            // 
            // lblFactDebitoTitulo
            // 
            lblFactDebitoTitulo.AutoSize = true;
            lblFactDebitoTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblFactDebitoTitulo.ForeColor = Color.White;
            lblFactDebitoTitulo.Location = new Point(175, 28);
            lblFactDebitoTitulo.Name = "lblFactDebitoTitulo";
            lblFactDebitoTitulo.Size = new Size(71, 19);
            lblFactDebitoTitulo.TabIndex = 2;
            lblFactDebitoTitulo.Text = "T. Débito:";
            // 
            // lblFactDebitoValor
            // 
            lblFactDebitoValor.AutoSize = true;
            lblFactDebitoValor.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblFactDebitoValor.ForeColor = Color.FromArgb(96, 165, 250);
            lblFactDebitoValor.Location = new Point(175, 48);
            lblFactDebitoValor.Name = "lblFactDebitoValor";
            lblFactDebitoValor.Size = new Size(59, 25);
            lblFactDebitoValor.TabIndex = 3;
            lblFactDebitoValor.Text = "$ 0,00";
            // 
            // lblFactCreditoTitulo
            // 
            lblFactCreditoTitulo.AutoSize = true;
            lblFactCreditoTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblFactCreditoTitulo.ForeColor = Color.White;
            lblFactCreditoTitulo.Location = new Point(12, 85);
            lblFactCreditoTitulo.Name = "lblFactCreditoTitulo";
            lblFactCreditoTitulo.Size = new Size(74, 19);
            lblFactCreditoTitulo.TabIndex = 4;
            lblFactCreditoTitulo.Text = "T. Crédito:";
            // 
            // lblFactCreditoValor
            // 
            lblFactCreditoValor.AutoSize = true;
            lblFactCreditoValor.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblFactCreditoValor.ForeColor = Color.FromArgb(192, 132, 252);
            lblFactCreditoValor.Location = new Point(12, 105);
            lblFactCreditoValor.Name = "lblFactCreditoValor";
            lblFactCreditoValor.Size = new Size(59, 25);
            lblFactCreditoValor.TabIndex = 5;
            lblFactCreditoValor.Text = "$ 0,00";
            // 
            // lblFactBilleteraTitulo
            // 
            lblFactBilleteraTitulo.AutoSize = true;
            lblFactBilleteraTitulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblFactBilleteraTitulo.ForeColor = Color.White;
            lblFactBilleteraTitulo.Location = new Point(175, 85);
            lblFactBilleteraTitulo.Name = "lblFactBilleteraTitulo";
            lblFactBilleteraTitulo.Size = new Size(118, 19);
            lblFactBilleteraTitulo.TabIndex = 6;
            lblFactBilleteraTitulo.Text = "Billetera Virtual:";
            // 
            // lblFactBilleteraValor
            // 
            lblFactBilleteraValor.AutoSize = true;
            lblFactBilleteraValor.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblFactBilleteraValor.ForeColor = Color.FromArgb(250, 204, 21);
            lblFactBilleteraValor.Location = new Point(175, 105);
            lblFactBilleteraValor.Name = "lblFactBilleteraValor";
            lblFactBilleteraValor.Size = new Size(59, 25);
            lblFactBilleteraValor.TabIndex = 7;
            lblFactBilleteraValor.Text = "$ 0,00";
            // 
            // lblRetiros
            // 
            lblRetiros.AutoSize = true;
            lblRetiros.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRetiros.ForeColor = Color.LightGray;
            lblRetiros.Location = new Point(20, 270);
            lblRetiros.Name = "lblRetiros";
            lblRetiros.Size = new Size(216, 23);
            lblRetiros.TabIndex = 11;
            lblRetiros.Text = "Retiros de la sesión actual";
            // 
            // gridRetiros
            // 
            gridRetiros.AllowUserToAddRows = false;
            gridRetiros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridRetiros.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridRetiros.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridRetiros.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridRetiros.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridRetiros.DefaultCellStyle = dataGridViewCellStyle2;
            gridRetiros.GridColor = Color.FromArgb(51, 65, 85);
            gridRetiros.Location = new Point(20, 295);
            gridRetiros.Name = "gridRetiros";
            gridRetiros.ReadOnly = true;
            gridRetiros.RowHeadersVisible = false;
            gridRetiros.RowHeadersWidth = 51;
            gridRetiros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridRetiros.Size = new Size(1060, 325);
            gridRetiros.TabIndex = 12;
            // 
            // Volver_Boton
            // 
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(960, 638);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(120, 32);
            Volver_Boton.TabIndex = 13;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // FormGestionCaja
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1100, 680);
            Controls.Add(lblTitulo);
            Controls.Add(lblEstadoCaja);
            Controls.Add(lblCajero);
            Controls.Add(lblApertura);
            Controls.Add(lblMontoInicio);
            Controls.Add(lblDisponible);
            Controls.Add(grpAbrir);
            Controls.Add(grpCerrar);
            Controls.Add(grpRetiro);
            Controls.Add(grpFacturacion);
            Controls.Add(lblRetiros);
            Controls.Add(gridRetiros);
            Controls.Add(Volver_Boton);
            Name = "FormGestionCaja";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Caja";
            grpAbrir.ResumeLayout(false);
            grpAbrir.PerformLayout();
            grpCerrar.ResumeLayout(false);
            grpCerrar.PerformLayout();
            grpRetiro.ResumeLayout(false);
            grpRetiro.PerformLayout();
            grpFacturacion.ResumeLayout(false);
            grpFacturacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridRetiros).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label        lblTitulo, lblEstadoCaja, lblCajero, lblApertura;
        private Label        lblMontoInicio, lblDisponible;
        private GroupBox     grpAbrir, grpCerrar, grpRetiro, grpFacturacion;
        private Label        lblFactEfectivoTitulo, lblFactEfectivoValor;
        private Label        lblFactDebitoTitulo, lblFactDebitoValor;
        private Label        lblFactCreditoTitulo, lblFactCreditoValor;
        private Label        lblFactBilleteraTitulo, lblFactBilleteraValor;
        private Label        lblMontoInicioLbl, lblMontoCierreLbl, lblMontoRetiroLbl, lblMotivoLbl, lblMontoCierre, lblTotalExtraccionesLbl, lblTotalExtracciones;
        private TextBox      txtMontoInicio, txtMontoRetiro, txtMotivo;
        private Button       Abrir_Boton, Cerrar_Boton, Retiro_Boton, Volver_Boton;
        private Label        lblRetiros;
        private DataGridView gridRetiros;
    }
}
