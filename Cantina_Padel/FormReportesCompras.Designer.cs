namespace Cantina_Padel
{
    partial class FormReportesCompras
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
            lblReporte = new Label();
            lblDesde = new Label();
            lblHasta = new Label();
            lblProveedor = new Label();
            cmbReporte = new ComboBox();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            cmbProveedor = new ComboBox();
            Generar_Boton = new Button();
            Exportar_Boton = new Button();
            Volver_Boton = new Button();
            gridReporte = new DataGridView();
            lblCantidadCompras = new Label();
            lblUnidades = new Label();
            lblTotal = new Label();
            lblTotalGanancia = new Label();
            lblPromedio = new Label();
            ((System.ComponentModel.ISupportInitialize)gridReporte).BeginInit();
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
            lblTitulo.Text = "REPORTES DE COMPRAS";
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
            // lblReporte
            // 
            lblReporte.AutoSize = true;
            lblReporte.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblReporte.ForeColor = Color.FromArgb(163, 230, 53);
            lblReporte.Location = new Point(20, 65);
            lblReporte.Name = "lblReporte";
            lblReporte.TabIndex = 2;
            lblReporte.Text = "Reporte:";
            // 
            // cmbReporte
            // 
            cmbReporte.BackColor = Color.FromArgb(51, 65, 85);
            cmbReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReporte.FlatStyle = FlatStyle.Flat;
            cmbReporte.ForeColor = Color.White;
            cmbReporte.Location = new Point(20, 90);
            cmbReporte.Name = "cmbReporte";
            cmbReporte.Size = new Size(230, 28);
            cmbReporte.TabIndex = 3;
            cmbReporte.SelectedIndexChanged += cmbReporte_SelectedIndexChanged;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDesde.ForeColor = Color.FromArgb(163, 230, 53);
            lblDesde.Location = new Point(270, 65);
            lblDesde.Name = "lblDesde";
            lblDesde.TabIndex = 4;
            lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(270, 90);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(130, 27);
            dtpDesde.TabIndex = 5;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHasta.ForeColor = Color.FromArgb(163, 230, 53);
            lblHasta.Location = new Point(415, 65);
            lblHasta.Name = "lblHasta";
            lblHasta.TabIndex = 6;
            lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(415, 90);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(130, 27);
            dtpHasta.TabIndex = 7;
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProveedor.ForeColor = Color.FromArgb(163, 230, 53);
            lblProveedor.Location = new Point(560, 65);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.TabIndex = 8;
            lblProveedor.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            cmbProveedor.BackColor = Color.FromArgb(51, 65, 85);
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.FlatStyle = FlatStyle.Flat;
            cmbProveedor.ForeColor = Color.White;
            cmbProveedor.Location = new Point(560, 90);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(240, 28);
            cmbProveedor.TabIndex = 9;
            // 
            // Generar_Boton
            // 
            Generar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Generar_Boton.FlatAppearance.BorderSize = 0;
            Generar_Boton.FlatStyle = FlatStyle.Flat;
            Generar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Generar_Boton.ForeColor = Color.Black;
            Generar_Boton.Location = new Point(820, 85);
            Generar_Boton.Name = "Generar_Boton";
            Generar_Boton.Size = new Size(190, 36);
            Generar_Boton.TabIndex = 10;
            Generar_Boton.Text = "Generar reporte";
            Generar_Boton.UseVisualStyleBackColor = false;
            Generar_Boton.Click += Generar_Boton_Click;
            // 
            // Exportar_Boton
            // 
            Exportar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Exportar_Boton.FlatAppearance.BorderSize = 0;
            Exportar_Boton.FlatStyle = FlatStyle.Flat;
            Exportar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Exportar_Boton.ForeColor = Color.White;
            Exportar_Boton.Location = new Point(1025, 85);
            Exportar_Boton.Name = "Exportar_Boton";
            Exportar_Boton.Size = new Size(195, 36);
            Exportar_Boton.TabIndex = 11;
            Exportar_Boton.Text = "Exportar a CSV";
            Exportar_Boton.UseVisualStyleBackColor = false;
            Exportar_Boton.Click += Exportar_Boton_Click;
            // 
            // gridReporte
            // 
            gridReporte.AllowUserToAddRows = false;
            gridReporte.AllowUserToDeleteRows = false;
            gridReporte.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridReporte.BorderStyle = BorderStyle.None;
            gridReporte.GridColor = Color.FromArgb(51, 65, 85);
            gridReporte.Location = new Point(20, 140);
            gridReporte.MultiSelect = false;
            gridReporte.Name = "gridReporte";
            gridReporte.ReadOnly = true;
            gridReporte.RowHeadersVisible = false;
            gridReporte.RowHeadersWidth = 51;
            gridReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridReporte.Size = new Size(1200, 440);
            gridReporte.TabIndex = 12;
            // 
            // lblCantidadCompras
            // 
            lblCantidadCompras.AutoSize = true;
            lblCantidadCompras.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCantidadCompras.ForeColor = Color.White;
            lblCantidadCompras.Location = new Point(20, 600);
            lblCantidadCompras.Name = "lblCantidadCompras";
            lblCantidadCompras.TabIndex = 13;
            lblCantidadCompras.Text = "Compras: 0";
            // 
            // lblUnidades
            // 
            lblUnidades.AutoSize = true;
            lblUnidades.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblUnidades.ForeColor = Color.White;
            lblUnidades.Location = new Point(200, 600);
            lblUnidades.Name = "lblUnidades";
            lblUnidades.TabIndex = 14;
            lblUnidades.Text = "Unidades: 0";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(163, 230, 53);
            lblTotal.Location = new Point(400, 595);
            lblTotal.Name = "lblTotal";
            lblTotal.TabIndex = 15;
            lblTotal.Text = "Total: $ 0,00";
            // 
            // lblTotalGanancia
            // 
            lblTotalGanancia.AutoSize = true;
            lblTotalGanancia.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalGanancia.ForeColor = Color.White;
            lblTotalGanancia.Location = new Point(700, 600);
            lblTotalGanancia.Name = "lblTotalGanancia";
            lblTotalGanancia.TabIndex = 16;
            lblTotalGanancia.Text = "Total con ganancia: $ 0,00";
            // 
            // lblPromedio
            // 
            lblPromedio.AutoSize = true;
            lblPromedio.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPromedio.ForeColor = Color.White;
            lblPromedio.Location = new Point(20, 635);
            lblPromedio.Name = "lblPromedio";
            lblPromedio.TabIndex = 17;
            lblPromedio.Text = "Promedio por compra: $ 0,00";
            // 
            // FormReportesCompras
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1240, 680);
            Controls.Add(lblTitulo);
            Controls.Add(Volver_Boton);
            Controls.Add(lblReporte);
            Controls.Add(cmbReporte);
            Controls.Add(lblDesde);
            Controls.Add(dtpDesde);
            Controls.Add(lblHasta);
            Controls.Add(dtpHasta);
            Controls.Add(lblProveedor);
            Controls.Add(cmbProveedor);
            Controls.Add(Generar_Boton);
            Controls.Add(Exportar_Boton);
            Controls.Add(gridReporte);
            Controls.Add(lblCantidadCompras);
            Controls.Add(lblUnidades);
            Controls.Add(lblTotal);
            Controls.Add(lblTotalGanancia);
            Controls.Add(lblPromedio);
            Name = "FormReportesCompras";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportes de Compras";
            ((System.ComponentModel.ISupportInitialize)gridReporte).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblReporte;
        private Label lblDesde;
        private Label lblHasta;
        private Label lblProveedor;
        private ComboBox cmbReporte;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private ComboBox cmbProveedor;
        private Button Generar_Boton;
        private Button Exportar_Boton;
        private Button Volver_Boton;
        private DataGridView gridReporte;
        private Label lblCantidadCompras;
        private Label lblUnidades;
        private Label lblTotal;
        private Label lblTotalGanancia;
        private Label lblPromedio;
    }
}
