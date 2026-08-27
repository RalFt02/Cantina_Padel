namespace Cantina_Padel
{
    partial class FormGestionUsuarios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            gridUsuarios       = new DataGridView();
            lblTitulo          = new Label();
            lblBuscar          = new Label();
            txtBuscar          = new TextBox();
            grpDatos           = new GroupBox();
            lblId              = new Label();
            txtId              = new TextBox();
            lblUsername        = new Label();
            txtUsername        = new TextBox();
            lblNombre          = new Label();
            txtNombre          = new TextBox();
            lblPassword        = new Label();
            txtPassword        = new TextBox();
            lblRol             = new Label();
            cmbRol             = new ComboBox();
            chkActivo          = new CheckBox();
            Nuevo_Boton        = new Button();
            Guardar_Boton      = new Button();
            Eliminar_Boton     = new Button();
            CambiarPass_Boton  = new Button();
            Cancelar_Boton     = new Button();
            Volver_Boton       = new Button();
            lblPasswordHint    = new Label();

            ((System.ComponentModel.ISupportInitialize)gridUsuarios).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();

            // ── Formulario ──────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(15, 23, 42);
            ClientSize          = new Size(1000, 600);
            Name                = "FormGestionUsuarios";
            Text                = "Gestión de Usuarios";
            StartPosition       = FormStartPosition.CenterScreen;

            // ── Título ──────────────────────────────────────────
            lblTitulo.Text      = "GESTIÓN DE USUARIOS";
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Font      = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location  = new Point(20, 15);
            lblTitulo.Size      = new Size(400, 35);
            lblTitulo.AutoSize  = true;

            // ── Buscar ──────────────────────────────────────────
            lblBuscar.Text      = "Buscar:";
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location  = new Point(20, 60);
            lblBuscar.AutoSize  = true;

            txtBuscar.Location  = new Point(80, 57);
            txtBuscar.Size      = new Size(250, 27);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // ── Grilla ──────────────────────────────────────────
            gridUsuarios.Location                  = new Point(20, 95);
            gridUsuarios.Size                      = new Size(580, 460);
            gridUsuarios.BackgroundColor           = Color.FromArgb(30, 41, 59);
            gridUsuarios.ForeColor                 = Color.White;
            gridUsuarios.GridColor                 = Color.FromArgb(51, 65, 85);
            gridUsuarios.BorderStyle               = BorderStyle.None;
            gridUsuarios.RowHeadersVisible         = false;
            gridUsuarios.AllowUserToAddRows        = false;
            gridUsuarios.ReadOnly                  = true;
            gridUsuarios.SelectionMode             = DataGridViewSelectionMode.FullRowSelect;
            gridUsuarios.AutoSizeColumnsMode       = DataGridViewAutoSizeColumnsMode.None;
            gridUsuarios.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(51, 65, 85);
            gridUsuarios.ColumnHeadersDefaultCellStyle.ForeColor  = Color.FromArgb(163, 230, 53);
            gridUsuarios.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridUsuarios.DefaultCellStyle.BackColor               = Color.FromArgb(30, 41, 59);
            gridUsuarios.DefaultCellStyle.ForeColor               = Color.White;
            gridUsuarios.DefaultCellStyle.SelectionBackColor      = Color.FromArgb(163, 230, 53);
            gridUsuarios.DefaultCellStyle.SelectionForeColor      = Color.Black;
            gridUsuarios.CellClick += gridUsuarios_CellClick;

            // ── Panel Datos ─────────────────────────────────────
            grpDatos.Text      = "Datos del Usuario";
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.Location  = new Point(620, 95);
            grpDatos.Size      = new Size(360, 460);
            grpDatos.BackColor = Color.FromArgb(30, 41, 59);

            // ID (oculto visualmente)
            lblId.Text      = "ID:";
            lblId.ForeColor = Color.Gray;
            lblId.Location  = new Point(10, 28);
            lblId.AutoSize  = true;
            txtId.Location  = new Point(60, 25);
            txtId.Size      = new Size(60, 27);
            txtId.ReadOnly  = true;
            txtId.BackColor = Color.FromArgb(15, 23, 42);
            txtId.ForeColor = Color.Gray;

            // Username
            lblUsername.Text      = "Usuario:";
            lblUsername.ForeColor = Color.White;
            lblUsername.Location  = new Point(10, 68);
            lblUsername.AutoSize  = true;
            txtUsername.Location  = new Point(10, 90);
            txtUsername.Size      = new Size(330, 27);
            txtUsername.BackColor = Color.FromArgb(51, 65, 85);
            txtUsername.ForeColor = Color.White;

            // Nombre completo
            lblNombre.Text      = "Nombre completo:";
            lblNombre.ForeColor = Color.White;
            lblNombre.Location  = new Point(10, 128);
            lblNombre.AutoSize  = true;
            txtNombre.Location  = new Point(10, 150);
            txtNombre.Size      = new Size(330, 27);
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;

            // Contraseña
            lblPassword.Text      = "Contraseña:";
            lblPassword.ForeColor = Color.White;
            lblPassword.Location  = new Point(10, 188);
            lblPassword.AutoSize  = true;
            txtPassword.Location       = new Point(10, 210);
            txtPassword.Size           = new Size(330, 27);
            txtPassword.PasswordChar   = '*';
            txtPassword.BackColor      = Color.FromArgb(51, 65, 85);
            txtPassword.ForeColor      = Color.White;

            lblPasswordHint.Text      = "(Dejar vacío para no cambiar al editar)";
            lblPasswordHint.ForeColor = Color.Gray;
            lblPasswordHint.Font      = new Font("Segoe UI", 7.5F);
            lblPasswordHint.Location  = new Point(10, 240);
            lblPasswordHint.AutoSize  = true;

            // Rol
            lblRol.Text      = "Rol:";
            lblRol.ForeColor = Color.White;
            lblRol.Location  = new Point(10, 262);
            lblRol.AutoSize  = true;
            cmbRol.Location         = new Point(10, 284);
            cmbRol.Size             = new Size(200, 28);
            cmbRol.BackColor        = Color.FromArgb(51, 65, 85);
            cmbRol.ForeColor        = Color.White;
            cmbRol.DropDownStyle    = ComboBoxStyle.DropDownList;
            cmbRol.Items.AddRange(new object[] { "Admin", "Cajero" });
            cmbRol.SelectedIndex    = 1;

            // Activo
            chkActivo.Text      = "Cuenta activa";
            chkActivo.ForeColor = Color.White;
            chkActivo.Location  = new Point(10, 325);
            chkActivo.Checked   = true;
            chkActivo.AutoSize  = true;

            // ── Botones del panel ───────────────────────────────
            Guardar_Boton.Text             = "Crear Usuario";
            Guardar_Boton.Location         = new Point(10, 365);
            Guardar_Boton.Size             = new Size(155, 38);
            Guardar_Boton.BackColor        = Color.FromArgb(163, 230, 53);
            Guardar_Boton.ForeColor        = Color.Black;
            Guardar_Boton.Font             = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.FlatStyle        = FlatStyle.Flat;
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click           += Guardar_Boton_Click;

            Cancelar_Boton.Text            = "Limpiar";
            Cancelar_Boton.Location        = new Point(175, 365);
            Cancelar_Boton.Size            = new Size(155, 38);
            Cancelar_Boton.BackColor       = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.ForeColor       = Color.White;
            Cancelar_Boton.FlatStyle       = FlatStyle.Flat;
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click          += Cancelar_Boton_Click;

            CambiarPass_Boton.Text         = "Cambiar Contraseña";
            CambiarPass_Boton.Location     = new Point(10, 413);
            CambiarPass_Boton.Size         = new Size(200, 35);
            CambiarPass_Boton.BackColor    = Color.FromArgb(59, 130, 246);
            CambiarPass_Boton.ForeColor    = Color.White;
            CambiarPass_Boton.FlatStyle    = FlatStyle.Flat;
            CambiarPass_Boton.FlatAppearance.BorderSize = 0;
            CambiarPass_Boton.UseVisualStyleBackColor = false;
            CambiarPass_Boton.Click       += CambiarPass_Boton_Click;

            Eliminar_Boton.Text            = "Desactivar";
            Eliminar_Boton.Location        = new Point(220, 413);
            Eliminar_Boton.Size            = new Size(130, 35);
            Eliminar_Boton.BackColor       = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.ForeColor       = Color.White;
            Eliminar_Boton.FlatStyle       = FlatStyle.Flat;
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click          += Eliminar_Boton_Click;

            grpDatos.Controls.AddRange(new Control[]
            {
                lblId, txtId,
                lblUsername, txtUsername,
                lblNombre, txtNombre,
                lblPassword, txtPassword, lblPasswordHint,
                lblRol, cmbRol,
                chkActivo,
                Guardar_Boton, Cancelar_Boton,
                CambiarPass_Boton, Eliminar_Boton
            });

            // ── Botones externos ────────────────────────────────
            Nuevo_Boton.Text           = "+ Nuevo Usuario";
            Nuevo_Boton.Location       = new Point(350, 55);
            Nuevo_Boton.Size           = new Size(150, 32);
            Nuevo_Boton.BackColor      = Color.FromArgb(163, 230, 53);
            Nuevo_Boton.ForeColor      = Color.Black;
            Nuevo_Boton.Font           = new Font("Segoe UI", 9F, FontStyle.Bold);
            Nuevo_Boton.FlatStyle      = FlatStyle.Flat;
            Nuevo_Boton.FlatAppearance.BorderSize = 0;
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click         += Nuevo_Boton_Click;

            Volver_Boton.Text          = "← Volver";
            Volver_Boton.Location      = new Point(870, 55);
            Volver_Boton.Size          = new Size(110, 32);
            Volver_Boton.BackColor     = Color.FromArgb(71, 85, 105);
            Volver_Boton.ForeColor     = Color.White;
            Volver_Boton.FlatStyle     = FlatStyle.Flat;
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click        += Volver_Boton_Click;

            Controls.AddRange(new Control[]
            {
                lblTitulo,
                lblBuscar, txtBuscar,
                gridUsuarios,
                grpDatos,
                Nuevo_Boton,
                Volver_Boton
            });

            ((System.ComponentModel.ISupportInitialize)gridUsuarios).EndInit();
            grpDatos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView     gridUsuarios;
        private Label            lblTitulo;
        private Label            lblBuscar;
        private TextBox          txtBuscar;
        private GroupBox         grpDatos;
        private Label            lblId;
        private TextBox          txtId;
        private Label            lblUsername;
        private TextBox          txtUsername;
        private Label            lblNombre;
        private TextBox          txtNombre;
        private Label            lblPassword;
        private TextBox          txtPassword;
        private Label            lblPasswordHint;
        private Label            lblRol;
        private ComboBox         cmbRol;
        private CheckBox         chkActivo;
        private Button           Nuevo_Boton;
        private Button           Guardar_Boton;
        private Button           Eliminar_Boton;
        private Button           CambiarPass_Boton;
        private Button           Cancelar_Boton;
        private Button           Volver_Boton;
    }
}
