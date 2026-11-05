namespace Cantina_Padel
{
    partial class FormEnviarMail
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
            lblDestinatario = new Label();
            txtDestinatario = new TextBox();
            lblAsunto = new Label();
            txtAsunto = new TextBox();
            lblVistaPrevia = new Label();
            txtVistaPrevia = new TextBox();
            btnEnviar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.None;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(240, 45);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(380, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "ENVIAR COMPROBANTE POR EMAIL";
            // 
            // lblDestinatario
            // 
            lblDestinatario.Anchor = AnchorStyles.None;
            lblDestinatario.AutoSize = true;
            lblDestinatario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDestinatario.ForeColor = Color.White;
            lblDestinatario.Location = new Point(240, 95);
            lblDestinatario.Name = "lblDestinatario";
            lblDestinatario.Size = new Size(125, 17);
            lblDestinatario.TabIndex = 1;
            lblDestinatario.Text = "Correo Electrónico:";
            // 
            // txtDestinatario
            // 
            txtDestinatario.Anchor = AnchorStyles.None;
            txtDestinatario.BackColor = Color.FromArgb(51, 65, 85);
            txtDestinatario.BorderStyle = BorderStyle.FixedSingle;
            txtDestinatario.Font = new Font("Segoe UI", 10.5F);
            txtDestinatario.ForeColor = Color.White;
            txtDestinatario.Location = new Point(240, 120);
            txtDestinatario.Name = "txtDestinatario";
            txtDestinatario.Size = new Size(800, 27);
            txtDestinatario.TabIndex = 2;
            // 
            // lblAsunto
            // 
            lblAsunto.Anchor = AnchorStyles.None;
            lblAsunto.AutoSize = true;
            lblAsunto.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblAsunto.ForeColor = Color.White;
            lblAsunto.Location = new Point(240, 165);
            lblAsunto.Name = "lblAsunto";
            lblAsunto.Size = new Size(55, 17);
            lblAsunto.TabIndex = 3;
            lblAsunto.Text = "Asunto:";
            // 
            // txtAsunto
            // 
            txtAsunto.Anchor = AnchorStyles.None;
            txtAsunto.BackColor = Color.FromArgb(51, 65, 85);
            txtAsunto.BorderStyle = BorderStyle.FixedSingle;
            txtAsunto.Font = new Font("Segoe UI", 10.5F);
            txtAsunto.ForeColor = Color.White;
            txtAsunto.Location = new Point(240, 190);
            txtAsunto.Name = "txtAsunto";
            txtAsunto.Size = new Size(800, 27);
            txtAsunto.TabIndex = 4;
            // 
            // lblVistaPrevia
            // 
            lblVistaPrevia.Anchor = AnchorStyles.None;
            lblVistaPrevia.AutoSize = true;
            lblVistaPrevia.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblVistaPrevia.ForeColor = Color.White;
            lblVistaPrevia.Location = new Point(240, 235);
            lblVistaPrevia.Name = "lblVistaPrevia";
            lblVistaPrevia.Size = new Size(160, 17);
            lblVistaPrevia.TabIndex = 5;
            lblVistaPrevia.Text = "Contenido / Vista Previa:";
            // 
            // txtVistaPrevia
            // 
            txtVistaPrevia.Anchor = AnchorStyles.None;
            txtVistaPrevia.BackColor = Color.FromArgb(30, 41, 59);
            txtVistaPrevia.BorderStyle = BorderStyle.FixedSingle;
            txtVistaPrevia.Font = new Font("Courier New", 9.5F);
            txtVistaPrevia.ForeColor = Color.FromArgb(226, 232, 240);
            txtVistaPrevia.Location = new Point(240, 260);
            txtVistaPrevia.Multiline = true;
            txtVistaPrevia.Name = "txtVistaPrevia";
            txtVistaPrevia.ReadOnly = true;
            txtVistaPrevia.ScrollBars = ScrollBars.Vertical;
            txtVistaPrevia.Size = new Size(800, 340);
            txtVistaPrevia.TabIndex = 6;
            // 
            // btnEnviar
            // 
            btnEnviar.Anchor = AnchorStyles.None;
            btnEnviar.BackColor = Color.FromArgb(16, 185, 129);
            btnEnviar.Cursor = Cursors.Hand;
            btnEnviar.FlatAppearance.BorderSize = 0;
            btnEnviar.FlatStyle = FlatStyle.Flat;
            btnEnviar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEnviar.ForeColor = Color.White;
            btnEnviar.Location = new Point(240, 620);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(385, 46);
            btnEnviar.TabIndex = 7;
            btnEnviar.Text = "✉ Enviar Correo";
            btnEnviar.UseVisualStyleBackColor = false;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.None;
            btnCancelar.BackColor = Color.FromArgb(71, 85, 105);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(655, 620);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(385, 46);
            btnCancelar.TabIndex = 8;
            btnCancelar.Text = "← Volver";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormEnviarMail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(btnCancelar);
            Controls.Add(btnEnviar);
            Controls.Add(txtVistaPrevia);
            Controls.Add(lblVistaPrevia);
            Controls.Add(txtAsunto);
            Controls.Add(lblAsunto);
            Controls.Add(txtDestinatario);
            Controls.Add(lblDestinatario);
            Controls.Add(lblTitulo);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormEnviarMail";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Enviar Comprobante por Email - Cantina Padel Club";
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private Label lblTitulo;
        private Label lblDestinatario;
        private TextBox txtDestinatario;
        private Label lblAsunto;
        private TextBox txtAsunto;
        private Label lblVistaPrevia;
        private TextBox txtVistaPrevia;
        private Button btnEnviar;
        private Button btnCancelar;
    }
}
