namespace Cantina_Padel
{
    partial class FormCobroCanchas
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
            btnRefrescar = new Button();
            lblReservas = new Label();
            gridReservas = new DataGridView();
            grpCobro = new GroupBox();
            lblTotal = new Label();
            btnEnviarAVentas = new Button();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)gridReservas).BeginInit();
            grpCobro.SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(390, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "COBRO DE RESERVAS DE CANCHAS";
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.BackColor = Color.FromArgb(71, 85, 105);
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(1130, 15);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(130, 36);
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
            lblCliente.Location = new Point(20, 62);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(125, 17);
            lblCliente.TabIndex = 2;
            lblCliente.Text = "Filtrar por Cliente:";
            // 
            // cmbCliente
            // 
            cmbCliente.BackColor = Color.FromArgb(51, 65, 85);
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.FlatStyle = FlatStyle.Flat;
            cmbCliente.Font = new Font("Segoe UI", 10F);
            cmbCliente.ForeColor = Color.White;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(20, 86);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(620, 26);
            cmbCliente.TabIndex = 3;
            cmbCliente.SelectedIndexChanged += cmbCliente_SelectedIndexChanged;
            // 
            // btnRefrescar
            // 
            btnRefrescar.BackColor = Color.FromArgb(51, 65, 85);
            btnRefrescar.Cursor = Cursors.Hand;
            btnRefrescar.FlatAppearance.BorderSize = 0;
            btnRefrescar.FlatStyle = FlatStyle.Flat;
            btnRefrescar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefrescar.ForeColor = Color.White;
            btnRefrescar.Location = new Point(655, 83);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(135, 32);
            btnRefrescar.TabIndex = 4;
            btnRefrescar.Text = "🔄 Refrescar";
            btnRefrescar.UseVisualStyleBackColor = false;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // lblReservas
            // 
            lblReservas.AutoSize = true;
            lblReservas.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblReservas.ForeColor = Color.FromArgb(163, 230, 53);
            lblReservas.Location = new Point(20, 128);
            lblReservas.Name = "lblReservas";
            lblReservas.Size = new Size(275, 17);
            lblReservas.TabIndex = 5;
            lblReservas.Text = "Reservas pendientes de cobro (seleccioná):";
            // 
            // gridReservas
            // 
            gridReservas.AllowUserToAddRows = false;
            gridReservas.AllowUserToDeleteRows = false;
            gridReservas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridReservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridReservas.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridReservas.BorderStyle = BorderStyle.None;
            gridReservas.GridColor = Color.FromArgb(51, 65, 85);
            gridReservas.Location = new Point(20, 152);
            gridReservas.MultiSelect = true;
            gridReservas.Name = "gridReservas";
            gridReservas.ReadOnly = true;
            gridReservas.RowHeadersVisible = false;
            gridReservas.RowHeadersWidth = 51;
            gridReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridReservas.Size = new Size(810, 545);
            gridReservas.TabIndex = 6;
            gridReservas.SelectionChanged += gridReservas_SelectionChanged;
            // 
            // grpCobro
            // 
            grpCobro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grpCobro.BackColor = Color.FromArgb(30, 41, 59);
            grpCobro.Controls.Add(btnEnviarAVentas);
            grpCobro.Controls.Add(lblTotal);
            grpCobro.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpCobro.ForeColor = Color.FromArgb(163, 230, 53);
            grpCobro.Location = new Point(850, 145);
            grpCobro.Name = "grpCobro";
            grpCobro.Size = new Size(410, 240);
            grpCobro.TabIndex = 7;
            grpCobro.TabStop = false;
            grpCobro.Text = "Acción de Cobro";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(163, 230, 53);
            lblTotal.Location = new Point(25, 45);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(165, 32);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Total: $ 0,00";
            // 
            // btnEnviarAVentas
            // 
            btnEnviarAVentas.BackColor = Color.FromArgb(163, 230, 53);
            btnEnviarAVentas.Cursor = Cursors.Hand;
            btnEnviarAVentas.FlatAppearance.BorderSize = 0;
            btnEnviarAVentas.FlatStyle = FlatStyle.Flat;
            btnEnviarAVentas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnEnviarAVentas.ForeColor = Color.Black;
            btnEnviarAVentas.Location = new Point(25, 130);
            btnEnviarAVentas.Name = "btnEnviarAVentas";
            btnEnviarAVentas.Size = new Size(360, 60);
            btnEnviarAVentas.TabIndex = 5;
            btnEnviarAVentas.Text = "🛒 Enviar Turno(s) a Ventas";
            btnEnviarAVentas.UseVisualStyleBackColor = false;
            btnEnviarAVentas.Click += btnEnviarAVentas_Click;
            // 
            // FormCobroCanchas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(grpCobro);
            Controls.Add(gridReservas);
            Controls.Add(lblReservas);
            Controls.Add(btnRefrescar);
            Controls.Add(cmbCliente);
            Controls.Add(lblCliente);
            Controls.Add(btnCerrar);
            Controls.Add(lblTitulo);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormCobroCanchas";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cobro de Reservas de Canchas - Cantina Padel Club";
            ((System.ComponentModel.ISupportInitialize)gridReservas).EndInit();
            grpCobro.ResumeLayout(false);
            grpCobro.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private Label lblTitulo;
        private Button btnCerrar;
        private Label lblCliente;
        private ComboBox cmbCliente;
        private Button btnRefrescar;
        private Label lblReservas;
        private DataGridView gridReservas;
        private GroupBox grpCobro;
        private Label lblTotal;
        private Button btnEnviarAVentas;
    }
}
