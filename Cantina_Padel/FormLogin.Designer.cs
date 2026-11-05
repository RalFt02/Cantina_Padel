namespace Cantina_Padel
{
    partial class Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            Iniciar_Sesion = new Button();
            Contra_Campo = new TextBox();
            Usuario_Campo = new TextBox();
            Imagen = new PictureBox();
            Contra_Text = new Label();
            Usuario_Text = new Label();
            ((System.ComponentModel.ISupportInitialize)Imagen).BeginInit();
            SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            // 
            // Iniciar_Sesion
            // 
            Iniciar_Sesion.Anchor = AnchorStyles.None;
            Iniciar_Sesion.BackColor = Color.FromArgb(163, 230, 53);
            Iniciar_Sesion.Cursor = Cursors.Hand;
            Iniciar_Sesion.FlatAppearance.BorderSize = 0;
            Iniciar_Sesion.FlatStyle = FlatStyle.Flat;
            Iniciar_Sesion.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            Iniciar_Sesion.ForeColor = Color.Black;
            Iniciar_Sesion.Location = new Point(546, 595);
            Iniciar_Sesion.Name = "Iniciar_Sesion";
            Iniciar_Sesion.Size = new Size(188, 56);
            Iniciar_Sesion.TabIndex = 0;
            Iniciar_Sesion.Text = "Ingresar";
            Iniciar_Sesion.UseVisualStyleBackColor = false;
            Iniciar_Sesion.Click += Iniciar_Sesion_Click;
            // 
            // Contra_Campo
            // 
            Contra_Campo.Anchor = AnchorStyles.None;
            Contra_Campo.BackColor = Color.FromArgb(30, 41, 59);
            Contra_Campo.BorderStyle = BorderStyle.FixedSingle;
            Contra_Campo.Font = new Font("Segoe UI", 11F);
            Contra_Campo.ForeColor = Color.White;
            Contra_Campo.Location = new Point(393, 520);
            Contra_Campo.MaxLength = 40;
            Contra_Campo.Name = "Contra_Campo";
            Contra_Campo.PasswordChar = '*';
            Contra_Campo.Size = new Size(494, 30);
            Contra_Campo.TabIndex = 1;
            // 
            // Usuario_Campo
            // 
            Usuario_Campo.Anchor = AnchorStyles.None;
            Usuario_Campo.BackColor = Color.FromArgb(30, 41, 59);
            Usuario_Campo.BorderStyle = BorderStyle.FixedSingle;
            Usuario_Campo.Font = new Font("Segoe UI", 11F);
            Usuario_Campo.ForeColor = Color.White;
            Usuario_Campo.Location = new Point(393, 430);
            Usuario_Campo.MaxLength = 30;
            Usuario_Campo.Name = "Usuario_Campo";
            Usuario_Campo.Size = new Size(494, 30);
            Usuario_Campo.TabIndex = 2;
            // 
            // Imagen
            // 
            Imagen.Anchor = AnchorStyles.None;
            Imagen.Image = (Image)resources.GetObject("Imagen.Image");
            Imagen.Location = new Point(393, 82);
            Imagen.Name = "Imagen";
            Imagen.Size = new Size(494, 270);
            Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            Imagen.TabIndex = 3;
            Imagen.TabStop = false;
            // 
            // Contra_Text
            // 
            Contra_Text.Anchor = AnchorStyles.None;
            Contra_Text.AutoSize = true;
            Contra_Text.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Contra_Text.ForeColor = Color.White;
            Contra_Text.Location = new Point(393, 492);
            Contra_Text.Name = "Contra_Text";
            Contra_Text.Size = new Size(85, 19);
            Contra_Text.TabIndex = 4;
            Contra_Text.Text = "Contraseña";
            Contra_Text.Click += label1_Click;
            // 
            // Usuario_Text
            // 
            Usuario_Text.Anchor = AnchorStyles.None;
            Usuario_Text.AutoSize = true;
            Usuario_Text.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Usuario_Text.ForeColor = Color.White;
            Usuario_Text.Location = new Point(393, 402);
            Usuario_Text.Name = "Usuario_Text";
            Usuario_Text.Size = new Size(60, 19);
            Usuario_Text.TabIndex = 5;
            Usuario_Text.Text = "Usuario";
            // 
            // Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(Usuario_Text);
            Controls.Add(Contra_Text);
            Controls.Add(Imagen);
            Controls.Add(Usuario_Campo);
            Controls.Add(Contra_Campo);
            Controls.Add(Iniciar_Sesion);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "Principal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar Sesión - Cantina Padel Club";
            ((System.ComponentModel.ISupportInitialize)Imagen).EndInit();
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private Button Iniciar_Sesion;
        private TextBox Contra_Campo;
        private TextBox Usuario_Campo;
        private PictureBox Imagen;
        private Label Contra_Text;
        private Label Usuario_Text;
    }
}
