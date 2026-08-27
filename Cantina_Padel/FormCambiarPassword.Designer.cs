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

            // Formulario
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(15, 23, 42);
            ClientSize          = new Size(400, 300);
            FormBorderStyle     = FormBorderStyle.FixedDialog;
            MaximizeBox         = false;
            MinimizeBox         = false;
            Name                = "FormCambiarPassword";
            Text                = "Cambiar Contraseña";
            StartPosition       = FormStartPosition.CenterParent;

            // Título
            lblTitulo.Text      = "CAMBIAR CONTRASEÑA";
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Font      = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitulo.Location  = new Point(20, 20);
            lblTitulo.AutoSize  = true;

            // Usuario
            lblUsuario.Text      = "Usuario:";
            lblUsuario.ForeColor = Color.Gray;
            lblUsuario.Location  = new Point(20, 70);
            lblUsuario.AutoSize  = true;

            lblUsuarioValor.Text      = "";
            lblUsuarioValor.ForeColor = Color.White;
            lblUsuarioValor.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsuarioValor.Location  = new Point(90, 70);
            lblUsuarioValor.AutoSize  = true;

            // Nueva contraseña
            lblNueva.Text      = "Nueva contraseña:";
            lblNueva.ForeColor = Color.White;
            lblNueva.Location  = new Point(20, 110);
            lblNueva.AutoSize  = true;

            txtNueva.Location     = new Point(20, 133);
            txtNueva.Size         = new Size(350, 27);
            txtNueva.PasswordChar = '*';
            txtNueva.BackColor    = Color.FromArgb(51, 65, 85);
            txtNueva.ForeColor    = Color.White;

            // Confirmar
            lblConfirmar.Text      = "Confirmar contraseña:";
            lblConfirmar.ForeColor = Color.White;
            lblConfirmar.Location  = new Point(20, 172);
            lblConfirmar.AutoSize  = true;

            txtConfirmar.Location     = new Point(20, 195);
            txtConfirmar.Size         = new Size(350, 27);
            txtConfirmar.PasswordChar = '*';
            txtConfirmar.BackColor    = Color.FromArgb(51, 65, 85);
            txtConfirmar.ForeColor    = Color.White;

            // Botón guardar
            Guardar_Boton.Text                         = "Actualizar";
            Guardar_Boton.Location                     = new Point(20, 240);
            Guardar_Boton.Size                         = new Size(160, 38);
            Guardar_Boton.BackColor                    = Color.FromArgb(163, 230, 53);
            Guardar_Boton.ForeColor                    = Color.Black;
            Guardar_Boton.Font                         = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.FlatStyle                    = FlatStyle.Flat;
            Guardar_Boton.FlatAppearance.BorderSize    = 0;
            Guardar_Boton.UseVisualStyleBackColor      = false;
            Guardar_Boton.Click                       += Guardar_Boton_Click;

            // Botón cancelar
            Cancelar_Boton.Text                        = "Cancelar";
            Cancelar_Boton.Location                    = new Point(210, 240);
            Cancelar_Boton.Size                        = new Size(160, 38);
            Cancelar_Boton.BackColor                   = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.ForeColor                   = Color.White;
            Cancelar_Boton.FlatStyle                   = FlatStyle.Flat;
            Cancelar_Boton.FlatAppearance.BorderSize   = 0;
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
