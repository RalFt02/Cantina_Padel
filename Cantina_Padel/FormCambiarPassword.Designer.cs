namespace Cantina_Padel
{
    partial class FormCambiarPassword
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
            lblTitulo        = new Label();
            lblUsuario       = new Label();
            lblUsuarioValor  = new Label();
            lblNueva         = new Label();
            txtNueva         = new TextBox();
            lblConfirmar     = new Label();
            txtConfirmar     = new TextBox();
            Guardar_Boton    = new Button();
            Cancelar_Boton   = new Button();

            SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();

            // Formulario
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(15, 23, 42);
            ClientSize          = new Size(1280, 720);
            Font                = new Font("Segoe UI", 9F);
            ForeColor           = Color.White;
            MinimumSize         = new Size(1150, 680);
            Name                = "FormCambiarPassword";
            Text                = "Cambiar Contraseña - Cantina Padel Club";
            StartPosition       = FormStartPosition.CenterParent;

            // Título
            lblTitulo.Anchor    = AnchorStyles.None;
            lblTitulo.Text      = "CAMBIAR CONTRASEÑA";
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Font      = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location  = new Point(415, 160);
            lblTitulo.AutoSize  = true;

            // Usuario
            lblUsuario.Anchor    = AnchorStyles.None;
            lblUsuario.Text      = "Usuario:";
            lblUsuario.ForeColor = Color.Gray;
            lblUsuario.Font      = new Font("Segoe UI", 10F);
            lblUsuario.Location  = new Point(415, 220);
            lblUsuario.AutoSize  = true;

            lblUsuarioValor.Anchor    = AnchorStyles.None;
            lblUsuarioValor.Text      = "";
            lblUsuarioValor.ForeColor = Color.White;
            lblUsuarioValor.Font      = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblUsuarioValor.Location  = new Point(495, 220);
            lblUsuarioValor.AutoSize  = true;

            // Nueva contraseña
            lblNueva.Anchor    = AnchorStyles.None;
            lblNueva.Text      = "Nueva contraseña:";
            lblNueva.ForeColor = Color.White;
            lblNueva.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNueva.Location  = new Point(415, 270);
            lblNueva.AutoSize  = true;

            txtNueva.Anchor       = AnchorStyles.None;
            txtNueva.Location     = new Point(415, 298);
            txtNueva.Size         = new Size(450, 28);
            txtNueva.Font         = new Font("Segoe UI", 10.5F);
            txtNueva.PasswordChar = '*';
            txtNueva.BackColor    = Color.FromArgb(51, 65, 85);
            txtNueva.ForeColor    = Color.White;

            // Confirmar
            lblConfirmar.Anchor    = AnchorStyles.None;
            lblConfirmar.Text      = "Confirmar contraseña:";
            lblConfirmar.ForeColor = Color.White;
            lblConfirmar.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirmar.Location  = new Point(415, 350);
            lblConfirmar.AutoSize  = true;

            txtConfirmar.Anchor       = AnchorStyles.None;
            txtConfirmar.Location     = new Point(415, 378);
            txtConfirmar.Size         = new Size(450, 28);
            txtConfirmar.Font         = new Font("Segoe UI", 10.5F);
            txtConfirmar.PasswordChar = '*';
            txtConfirmar.BackColor    = Color.FromArgb(51, 65, 85);
            txtConfirmar.ForeColor    = Color.White;

            // Botón guardar
            Guardar_Boton.Anchor                       = AnchorStyles.None;
            Guardar_Boton.Text                         = "Actualizar";
            Guardar_Boton.Location                     = new Point(415, 445);
            Guardar_Boton.Size                         = new Size(215, 44);
            Guardar_Boton.BackColor                    = Color.FromArgb(163, 230, 53);
            Guardar_Boton.ForeColor                    = Color.Black;
            Guardar_Boton.Font                         = new Font("Segoe UI", 10F, FontStyle.Bold);
            Guardar_Boton.FlatStyle                    = FlatStyle.Flat;
            Guardar_Boton.FlatAppearance.BorderSize    = 0;
            Guardar_Boton.Cursor                       = Cursors.Hand;
            Guardar_Boton.UseVisualStyleBackColor      = false;
            Guardar_Boton.Click                       += Guardar_Boton_Click;

            // Botón cancelar
            Cancelar_Boton.Anchor                      = AnchorStyles.None;
            Cancelar_Boton.Text                        = "← Volver";
            Cancelar_Boton.Location                    = new Point(650, 445);
            Cancelar_Boton.Size                        = new Size(215, 44);
            Cancelar_Boton.BackColor                   = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.ForeColor                   = Color.White;
            Cancelar_Boton.Font                        = new Font("Segoe UI", 10F, FontStyle.Bold);
            Cancelar_Boton.FlatStyle                   = FlatStyle.Flat;
            Cancelar_Boton.FlatAppearance.BorderSize   = 0;
            Cancelar_Boton.Cursor                      = Cursors.Hand;
            Cancelar_Boton.UseVisualStyleBackColor     = false;
            Cancelar_Boton.Click                      += Cancelar_Boton_Click;

            Controls.AddRange(new Control[]
            {
                lblTitulo,
                lblUsuario, lblUsuarioValor,
                lblNueva, txtNueva,
                lblConfirmar, txtConfirmar,
                Guardar_Boton, Cancelar_Boton
            });

            ResumeLayout(false);
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private Label    lblTitulo;
        private Label    lblUsuario;
        private Label    lblUsuarioValor;
        private Label    lblNueva;
        private TextBox  txtNueva;
        private Label    lblConfirmar;
        private TextBox  txtConfirmar;
        private Button   Guardar_Boton;
        private Button   Cancelar_Boton;
    }
}
