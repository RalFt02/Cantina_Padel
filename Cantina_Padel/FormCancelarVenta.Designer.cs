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
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
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
            gridVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridVentas.BackgroundColor = Color.FromArgb(15, 23, 42);
            gridVentas.BorderStyle = BorderStyle.None;
            gridVentas.ColumnHeadersHeight = 30;
            gridVentas.GridColor = Color.FromArgb(51, 65, 85);
            gridVentas.Location = new Point(20, 60);
            gridVentas.MultiSelect = false;
            gridVentas.Name = "gridVentas";
            gridVentas.ReadOnly = true;
            gridVentas.RowHeadersVisible = false;
            gridVentas.RowHeadersWidth = 51;
            gridVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVentas.Size = new Size(960, 270);
            gridVentas.TabIndex = 1;
            gridVentas.SelectionChanged += gridVentas_SelectionChanged;
            //
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDetalle.ForeColor = Color.White;
            lblDetalle.Location = new Point(20, 340);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.TabIndex = 2;
            lblDetalle.Text = "Detalle de la venta seleccionada:";
            //
            // gridDetalle
            //
            gridDetalle.AllowUserToAddRows = false;
            gridDetalle.AllowUserToDeleteRows = false;
            gridDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridDetalle.BackgroundColor = Color.FromArgb(15, 23, 42);
            gridDetalle.BorderStyle = BorderStyle.None;
            gridDetalle.ColumnHeadersHeight = 30;
            gridDetalle.GridColor = Color.FromArgb(51, 65, 85);
            gridDetalle.Location = new Point(20, 368);
            gridDetalle.Name = "gridDetalle";
            gridDetalle.ReadOnly = true;
            gridDetalle.RowHeadersVisible = false;
            gridDetalle.RowHeadersWidth = 51;
            gridDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridDetalle.Size = new Size(960, 150);
            gridDetalle.TabIndex = 3;
            //
            // lblMotivo
            //
            lblMotivo.AutoSize = true;
            lblMotivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMotivo.ForeColor = Color.White;
            lblMotivo.Location = new Point(20, 535);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.TabIndex = 4;
            lblMotivo.Text = "Motivo de la cancelación:";
            //
            // txtMotivo
            //
            txtMotivo.BackColor = Color.FromArgb(51, 65, 85);
            txtMotivo.BorderStyle = BorderStyle.FixedSingle;
            txtMotivo.Font = new Font("Segoe UI", 10F);
            txtMotivo.ForeColor = Color.White;
            txtMotivo.Location = new Point(20, 562);
            txtMotivo.MaxLength = 255;
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(560, 30);
            txtMotivo.TabIndex = 5;
            //
            // btnCancelarVenta
            //
            btnCancelarVenta.BackColor = Color.FromArgb(239, 68, 68);
            btnCancelarVenta.FlatAppearance.BorderSize = 0;
            btnCancelarVenta.FlatStyle = FlatStyle.Flat;
            btnCancelarVenta.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelarVenta.ForeColor = Color.White;
            btnCancelarVenta.Location = new Point(600, 556);
            btnCancelarVenta.Name = "btnCancelarVenta";
            btnCancelarVenta.Size = new Size(220, 40);
            btnCancelarVenta.TabIndex = 6;
            btnCancelarVenta.Text = "🚫 Cancelar venta";
            btnCancelarVenta.UseVisualStyleBackColor = false;
            btnCancelarVenta.Click += btnCancelarVenta_Click;
            //
            // btnCerrar
            //
            btnCerrar.BackColor = Color.FromArgb(71, 85, 105);
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(840, 556);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(140, 40);
            btnCerrar.TabIndex = 7;
            btnCerrar.Text = "← Volver";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            //
            // FormCancelarVenta
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1000, 620);
            Controls.Add(lblTitulo);
            Controls.Add(gridVentas);
            Controls.Add(lblDetalle);
            Controls.Add(gridDetalle);
            Controls.Add(lblMotivo);
            Controls.Add(txtMotivo);
            Controls.Add(btnCancelarVenta);
            Controls.Add(btnCerrar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormCancelarVenta";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cancelación de Ventas";
            ((System.ComponentModel.ISupportInitialize)gridVentas).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
