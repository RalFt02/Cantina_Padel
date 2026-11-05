namespace Cantina_Padel
{
    partial class FormCancelarVenta
    {
        private System.ComponentModel.IContainer? components = null;

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
            gridVentas = new DataGridView();
            lblDetalle = new Label();
            gridDetalle = new DataGridView();
            lblMotivo = new Label();
            txtMotivo = new TextBox();
            btnCancelarVenta = new Button();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)gridVentas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridDetalle).BeginInit();
            SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "CANCELACIÓN DE VENTAS";
            // 
            // gridVentas
            // 
            gridVentas.AllowUserToAddRows = false;
            gridVentas.AllowUserToDeleteRows = false;
            gridVentas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gridVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridVentas.BackgroundColor = Color.FromArgb(15, 23, 42);
            gridVentas.BorderStyle = BorderStyle.None;
            gridVentas.ColumnHeadersHeight = 32;
            gridVentas.GridColor = Color.FromArgb(51, 65, 85);
            gridVentas.Location = new Point(20, 60);
            gridVentas.MultiSelect = false;
            gridVentas.Name = "gridVentas";
            gridVentas.ReadOnly = true;
            gridVentas.RowHeadersVisible = false;
            gridVentas.RowHeadersWidth = 51;
            gridVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVentas.Size = new Size(1240, 320);
            gridVentas.TabIndex = 1;
            gridVentas.SelectionChanged += gridVentas_SelectionChanged;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDetalle.ForeColor = Color.FromArgb(163, 230, 53);
            lblDetalle.Location = new Point(20, 392);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.TabIndex = 2;
            lblDetalle.Text = "Detalle de la venta seleccionada:";
            // 
            // gridDetalle
            // 
            gridDetalle.AllowUserToAddRows = false;
            gridDetalle.AllowUserToDeleteRows = false;
            gridDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridDetalle.BackgroundColor = Color.FromArgb(15, 23, 42);
            gridDetalle.BorderStyle = BorderStyle.None;
            gridDetalle.ColumnHeadersHeight = 32;
            gridDetalle.GridColor = Color.FromArgb(51, 65, 85);
            gridDetalle.Location = new Point(20, 418);
            gridDetalle.Name = "gridDetalle";
            gridDetalle.ReadOnly = true;
            gridDetalle.RowHeadersVisible = false;
            gridDetalle.RowHeadersWidth = 51;
            gridDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridDetalle.Size = new Size(1240, 210);
            gridDetalle.TabIndex = 3;
            // 
            // lblMotivo
            // 
            lblMotivo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblMotivo.AutoSize = true;
            lblMotivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMotivo.ForeColor = Color.White;
            lblMotivo.Location = new Point(20, 642);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.TabIndex = 4;
            lblMotivo.Text = "Motivo de la cancelación:";
            // 
            // txtMotivo
            // 
            txtMotivo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMotivo.BackColor = Color.FromArgb(51, 65, 85);
            txtMotivo.BorderStyle = BorderStyle.FixedSingle;
            txtMotivo.Font = new Font("Segoe UI", 10F);
            txtMotivo.ForeColor = Color.White;
            txtMotivo.Location = new Point(20, 666);
            txtMotivo.MaxLength = 255;
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(820, 30);
            txtMotivo.TabIndex = 5;
            // 
            // btnCancelarVenta
            // 
            btnCancelarVenta.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelarVenta.BackColor = Color.FromArgb(239, 68, 68);
            btnCancelarVenta.Cursor = Cursors.Hand;
            btnCancelarVenta.FlatAppearance.BorderSize = 0;
            btnCancelarVenta.FlatStyle = FlatStyle.Flat;
            btnCancelarVenta.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelarVenta.ForeColor = Color.White;
            btnCancelarVenta.Location = new Point(860, 658);
            btnCancelarVenta.Name = "btnCancelarVenta";
            btnCancelarVenta.Size = new Size(240, 42);
            btnCancelarVenta.TabIndex = 6;
            btnCancelarVenta.Text = "🚫 Cancelar venta";
            btnCancelarVenta.UseVisualStyleBackColor = false;
            btnCancelarVenta.Click += btnCancelarVenta_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCerrar.BackColor = Color.FromArgb(71, 85, 105);
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(1115, 658);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(145, 42);
            btnCerrar.TabIndex = 7;
            btnCerrar.Text = "← Volver";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // FormCancelarVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(lblTitulo);
            Controls.Add(gridVentas);
            Controls.Add(lblDetalle);
            Controls.Add(gridDetalle);
            Controls.Add(lblMotivo);
            Controls.Add(txtMotivo);
            Controls.Add(btnCancelarVenta);
            Controls.Add(btnCerrar);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormCancelarVenta";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cancelación de Ventas - Cantina Padel Club";
            ((System.ComponentModel.ISupportInitialize)gridVentas).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private Label lblTitulo;
        private DataGridView gridVentas;
        private Label lblDetalle;
        private DataGridView gridDetalle;
        private Label lblMotivo;
        private TextBox txtMotivo;
        private Button btnCancelarVenta;
        private Button btnCerrar;
    }
}
