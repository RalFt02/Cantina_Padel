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
            lblTitulo        = new Label();
            lblEstadoCaja    = new Label();
            lblCajero        = new Label();
            lblApertura      = new Label();
            lblMontoInicio   = new Label();
            lblDisponible    = new Label();

            grpAbrir         = new GroupBox();
            lblMontoInicioLbl = new Label();
            txtMontoInicio   = new TextBox();
            Abrir_Boton      = new Button();

            grpCerrar        = new GroupBox();
            lblMontoCierreLbl = new Label();
            txtMontoCierre   = new TextBox();
            Cerrar_Boton     = new Button();

            grpRetiro        = new GroupBox();
            lblMontoRetiroLbl = new Label();
            txtMontoRetiro   = new TextBox();
            lblMotivoLbl     = new Label();
            txtMotivo        = new TextBox();
            Retiro_Boton     = new Button();

            lblSesiones      = new Label();
            gridSesiones     = new DataGridView();
            lblRetiros       = new Label();
            gridRetiros      = new DataGridView();
            Volver_Boton     = new Button();

            ((System.ComponentModel.ISupportInitialize)gridSesiones).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridRetiros).BeginInit();
            grpAbrir.SuspendLayout();
            grpCerrar.SuspendLayout();
            grpRetiro.SuspendLayout();
            SuspendLayout();

            // ── Formulario ──────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(15, 23, 42);
            ClientSize          = new Size(1100, 680);
            Name                = "FormGestionCaja";
            Text                = "Gestión de Caja";
            StartPosition       = FormStartPosition.CenterScreen;

            // ── Título ──────────────────────────────────────────
            lblTitulo.Text      = "GESTIÓN DE CAJA";
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Font      = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location  = new Point(20, 15);
            lblTitulo.AutoSize  = true;

            // ── Estado ──────────────────────────────────────────
            lblEstadoCaja.Text      = "● CAJA CERRADA";
            lblEstadoCaja.ForeColor = Color.IndianRed;
            lblEstadoCaja.Font      = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEstadoCaja.Location  = new Point(20, 55);
            lblEstadoCaja.AutoSize  = true;

            lblCajero.Text      = "";
            lblCajero.ForeColor = Color.LightGray;
            lblCajero.Location  = new Point(220, 55);
            lblCajero.AutoSize  = true;

            lblApertura.Text      = "";
            lblApertura.ForeColor = Color.LightGray;
            lblApertura.Location  = new Point(420, 55);
            lblApertura.AutoSize  = true;

            lblMontoInicio.Text      = "";
            lblMontoInicio.ForeColor = Color.LightGray;
            lblMontoInicio.Location  = new Point(650, 55);
            lblMontoInicio.AutoSize  = true;

            lblDisponible.Text      = "";
            lblDisponible.ForeColor = Color.FromArgb(163, 230, 53);
            lblDisponible.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDisponible.Location  = new Point(850, 55);
            lblDisponible.AutoSize  = true;

            // ── GroupBox Abrir ───────────────────────────────────
            grpAbrir.Text      = "Abrir Caja";
            grpAbrir.ForeColor = Color.FromArgb(163, 230, 53);
            grpAbrir.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpAbrir.BackColor = Color.FromArgb(30, 41, 59);
            grpAbrir.Location  = new Point(20, 90);
            grpAbrir.Size      = new Size(230, 110);

            lblMontoInicioLbl.Text      = "Monto inicio ($):";
            lblMontoInicioLbl.ForeColor = Color.White;
            lblMontoInicioLbl.Location  = new Point(10, 28);
            lblMontoInicioLbl.AutoSize  = true;
            txtMontoInicio.Location     = new Point(10, 50);
            txtMontoInicio.Size         = new Size(200, 27);
            txtMontoInicio.BackColor    = Color.FromArgb(51, 65, 85);
            txtMontoInicio.ForeColor    = Color.White;
            txtMontoInicio.PlaceholderText = "0.00";
            Abrir_Boton.Text            = "Abrir Caja";
            Abrir_Boton.Location        = new Point(10, 83);
            Abrir_Boton.Size            = new Size(200, 18);
            Abrir_Boton.BackColor       = Color.FromArgb(163, 230, 53);
            Abrir_Boton.ForeColor       = Color.Black;
            Abrir_Boton.FlatStyle       = FlatStyle.Flat;
            Abrir_Boton.FlatAppearance.BorderSize = 0;
            Abrir_Boton.Font            = new Font("Segoe UI", 8F, FontStyle.Bold);
            Abrir_Boton.UseVisualStyleBackColor = false;
            Abrir_Boton.Click          += Abrir_Boton_Click;
            grpAbrir.Controls.AddRange(new Control[] { lblMontoInicioLbl, txtMontoInicio, Abrir_Boton });

            // ── GroupBox Cerrar ──────────────────────────────────
            grpCerrar.Text      = "Cerrar Caja";
            grpCerrar.ForeColor = Color.FromArgb(163, 230, 53);
            grpCerrar.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpCerrar.BackColor = Color.FromArgb(30, 41, 59);
            grpCerrar.Location  = new Point(265, 90);
            grpCerrar.Size      = new Size(230, 110);

            lblMontoCierreLbl.Text      = "Monto cierre ($):";
            lblMontoCierreLbl.ForeColor = Color.White;
            lblMontoCierreLbl.Location  = new Point(10, 28);
            lblMontoCierreLbl.AutoSize  = true;
            txtMontoCierre.Location     = new Point(10, 50);
            txtMontoCierre.Size         = new Size(200, 27);
            txtMontoCierre.BackColor    = Color.FromArgb(51, 65, 85);
            txtMontoCierre.ForeColor    = Color.White;
            txtMontoCierre.PlaceholderText = "0.00";
            Cerrar_Boton.Text           = "Cerrar Caja";
            Cerrar_Boton.Location       = new Point(10, 83);
            Cerrar_Boton.Size           = new Size(200, 18);
            Cerrar_Boton.BackColor      = Color.FromArgb(239, 68, 68);
            Cerrar_Boton.ForeColor      = Color.White;
            Cerrar_Boton.FlatStyle      = FlatStyle.Flat;
            Cerrar_Boton.FlatAppearance.BorderSize = 0;
            Cerrar_Boton.Font           = new Font("Segoe UI", 8F, FontStyle.Bold);
            Cerrar_Boton.UseVisualStyleBackColor = false;
            Cerrar_Boton.Click         += Cerrar_Boton_Click;
            grpCerrar.Controls.AddRange(new Control[] { lblMontoCierreLbl, txtMontoCierre, Cerrar_Boton });

            // ── GroupBox Retiro ──────────────────────────────────
            grpRetiro.Text      = "Retiro de Efectivo";
            grpRetiro.ForeColor = Color.FromArgb(163, 230, 53);
            grpRetiro.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpRetiro.BackColor = Color.FromArgb(30, 41, 59);
            grpRetiro.Location  = new Point(510, 90);
            grpRetiro.Size      = new Size(560, 110);

            lblMontoRetiroLbl.Text      = "Monto ($):";
            lblMontoRetiroLbl.ForeColor = Color.White;
            lblMontoRetiroLbl.Location  = new Point(10, 28);
            lblMontoRetiroLbl.AutoSize  = true;
            txtMontoRetiro.Location     = new Point(10, 50);
            txtMontoRetiro.Size         = new Size(130, 27);
            txtMontoRetiro.BackColor    = Color.FromArgb(51, 65, 85);
            txtMontoRetiro.ForeColor    = Color.White;
            txtMontoRetiro.PlaceholderText = "0.00";

            lblMotivoLbl.Text      = "Motivo:";
            lblMotivoLbl.ForeColor = Color.White;
            lblMotivoLbl.Location  = new Point(155, 28);
            lblMotivoLbl.AutoSize  = true;
            txtMotivo.Location     = new Point(155, 50);
            txtMotivo.Size         = new Size(270, 27);
            txtMotivo.BackColor    = Color.FromArgb(51, 65, 85);
            txtMotivo.ForeColor    = Color.White;
            txtMotivo.PlaceholderText = "Ej: Pago proveedor";

            Retiro_Boton.Text           = "Registrar Retiro";
            Retiro_Boton.Location       = new Point(440, 45);
            Retiro_Boton.Size           = new Size(110, 35);
            Retiro_Boton.BackColor      = Color.FromArgb(245, 158, 11);
            Retiro_Boton.ForeColor      = Color.Black;
            Retiro_Boton.FlatStyle      = FlatStyle.Flat;
            Retiro_Boton.FlatAppearance.BorderSize = 0;
            Retiro_Boton.Font           = new Font("Segoe UI", 8F, FontStyle.Bold);
            Retiro_Boton.UseVisualStyleBackColor = false;
            Retiro_Boton.Click         += Retiro_Boton_Click;
            grpRetiro.Controls.AddRange(new Control[] { lblMontoRetiroLbl, txtMontoRetiro, lblMotivoLbl, txtMotivo, Retiro_Boton });

            // ── Grilla Sesiones ──────────────────────────────────
            lblSesiones.Text      = "Registro de sesiones";
            lblSesiones.ForeColor = Color.LightGray;
            lblSesiones.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSesiones.Location  = new Point(20, 215);
            lblSesiones.AutoSize  = true;

            gridSesiones.Location            = new Point(20, 240);
            gridSesiones.Size                = new Size(1060, 180);
            gridSesiones.BackgroundColor     = Color.FromArgb(30, 41, 59);
            gridSesiones.ForeColor           = Color.White;
            gridSesiones.GridColor           = Color.FromArgb(51, 65, 85);
            gridSesiones.BorderStyle         = BorderStyle.None;
            gridSesiones.RowHeadersVisible   = false;
            gridSesiones.AllowUserToAddRows  = false;
            gridSesiones.ReadOnly            = true;
            gridSesiones.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            gridSesiones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridSesiones.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(51, 65, 85);
            gridSesiones.ColumnHeadersDefaultCellStyle.ForeColor  = Color.FromArgb(163, 230, 53);
            gridSesiones.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridSesiones.DefaultCellStyle.BackColor               = Color.FromArgb(30, 41, 59);
            gridSesiones.DefaultCellStyle.ForeColor               = Color.White;
            gridSesiones.DefaultCellStyle.SelectionBackColor      = Color.FromArgb(163, 230, 53);
            gridSesiones.DefaultCellStyle.SelectionForeColor      = Color.Black;

            // ── Grilla Retiros ───────────────────────────────────
            lblRetiros.Text      = "Retiros de la sesión actual";
            lblRetiros.ForeColor = Color.LightGray;
            lblRetiros.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRetiros.Location  = new Point(20, 435);
            lblRetiros.AutoSize  = true;

            gridRetiros.Location            = new Point(20, 460);
            gridRetiros.Size                = new Size(1060, 170);
            gridRetiros.BackgroundColor     = Color.FromArgb(30, 41, 59);
            gridRetiros.ForeColor           = Color.White;
            gridRetiros.GridColor           = Color.FromArgb(51, 65, 85);
            gridRetiros.BorderStyle         = BorderStyle.None;
            gridRetiros.RowHeadersVisible   = false;
            gridRetiros.AllowUserToAddRows  = false;
            gridRetiros.ReadOnly            = true;
            gridRetiros.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            gridRetiros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridRetiros.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(51, 65, 85);
            gridRetiros.ColumnHeadersDefaultCellStyle.ForeColor  = Color.FromArgb(163, 230, 53);
            gridRetiros.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridRetiros.DefaultCellStyle.BackColor               = Color.FromArgb(30, 41, 59);
            gridRetiros.DefaultCellStyle.ForeColor               = Color.White;
            gridRetiros.DefaultCellStyle.SelectionBackColor      = Color.FromArgb(163, 230, 53);
            gridRetiros.DefaultCellStyle.SelectionForeColor      = Color.Black;

            // ── Volver ───────────────────────────────────────────
            Volver_Boton.Text           = "← Volver";
            Volver_Boton.Location       = new Point(960, 638);
            Volver_Boton.Size           = new Size(120, 32);
            Volver_Boton.BackColor      = Color.FromArgb(71, 85, 105);
            Volver_Boton.ForeColor      = Color.White;
            Volver_Boton.FlatStyle      = FlatStyle.Flat;
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click         += Volver_Boton_Click;

            Controls.AddRange(new Control[]
            {
                lblTitulo,
                lblEstadoCaja, lblCajero, lblApertura, lblMontoInicio, lblDisponible,
                grpAbrir, grpCerrar, grpRetiro,
                lblSesiones, gridSesiones,
                lblRetiros, gridRetiros,
                Volver_Boton
            });

            ((System.ComponentModel.ISupportInitialize)gridSesiones).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridRetiros).EndInit();
            grpAbrir.ResumeLayout(false);
            grpCerrar.ResumeLayout(false);
            grpRetiro.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private Label        lblTitulo, lblEstadoCaja, lblCajero, lblApertura;
        private Label        lblMontoInicio, lblDisponible;
        private GroupBox     grpAbrir, grpCerrar, grpRetiro;
        private Label        lblMontoInicioLbl, lblMontoCierreLbl, lblMontoRetiroLbl, lblMotivoLbl;
        private TextBox      txtMontoInicio, txtMontoCierre, txtMontoRetiro, txtMotivo;
        private Button       Abrir_Boton, Cerrar_Boton, Retiro_Boton, Volver_Boton;
        private Label        lblSesiones, lblRetiros;
        private DataGridView gridSesiones, gridRetiros;
    }
}
