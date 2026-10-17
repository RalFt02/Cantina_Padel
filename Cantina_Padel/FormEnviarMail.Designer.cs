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
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(346, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "ENVIAR COMPROBANTE POR EMAIL";
            // 
            // lblDestinatario
            // 
            lblDestinatario.AutoSize = true;
            lblDestinatario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDestinatario.ForeColor = Color.White;
            lblDestinatario.Location = new Point(20, 58);
            lblDestinatario.Name = "lblDestinatario";
            lblDestinatario.Size = new Size(135, 20);
            lblDestinatario.TabIndex = 1;
            lblDestinatario.Text = "Correo Electrónico:";
            // 
            // txtDestinatario
            // 
            txtDestinatario.BackColor = Color.FromArgb(51, 65, 85);
            txtDestinatario.BorderStyle = BorderStyle.FixedSingle;
            txtDestinatario.Font = new Font("Segoe UI", 10F);
            txtDestinatario.ForeColor = Color.White;
            txtDestinatario.Location = new Point(20, 82);
            txtDestinatario.Name = "txtDestinatario";
            txtDestinatario.Size = new Size(490, 30);
            txtDestinatario.TabIndex = 2;
            // 
            // lblAsunto
            // 
            lblAsunto.AutoSize = true;
            lblAsunto.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAsunto.ForeColor = Color.White;
            lblAsunto.Location = new Point(20, 122);
            lblAsunto.Name = "lblAsunto";
            lblAsunto.Size = new Size(64, 20);
            lblAsunto.TabIndex = 3;
            lblAsunto.Text = "Asunto:";
            // 
            // txtAsunto
            // 
            txtAsunto.BackColor = Color.FromArgb(51, 65, 85);
            txtAsunto.BorderStyle = BorderStyle.FixedSingle;
            txtAsunto.Font = new Font("Segoe UI", 10F);
            txtAsunto.ForeColor = Color.White;
            txtAsunto.Location = new Point(20, 146);
            txtAsunto.Name = "txtAsunto";
            txtAsunto.Size = new Size(490, 30);
            txtAsunto.TabIndex = 4;
            // 
            // lblVistaPrevia
            // 
            lblVistaPrevia.AutoSize = true;
            lblVistaPrevia.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVistaPrevia.ForeColor = Color.White;
            lblVistaPrevia.Location = new Point(20, 186);
            lblVistaPrevia.Name = "lblVistaPrevia";
            lblVistaPrevia.Size = new Size(183, 20);
            lblVistaPrevia.TabIndex = 5;
            lblVistaPrevia.Text = "Contenido / Vista Previa:";
            // 
            // txtVistaPrevia
            // 
            txtVistaPrevia.BackColor = Color.FromArgb(30, 41, 59);
            txtVistaPrevia.BorderStyle = BorderStyle.FixedSingle;
            txtVistaPrevia.Font = new Font("Courier New", 9F);
            txtVistaPrevia.ForeColor = Color.FromArgb(226, 232, 240);
            txtVistaPrevia.Location = new Point(20, 210);
            txtVistaPrevia.Multiline = true;
            txtVistaPrevia.Name = "txtVistaPrevia";
            txtVistaPrevia.ReadOnly = true;
            txtVistaPrevia.ScrollBars = ScrollBars.Vertical;
            txtVistaPrevia.Size = new Size(490, 250);
            txtVistaPrevia.TabIndex = 6;
            // 
            // btnEnviar
            // 
            btnEnviar.BackColor = Color.FromArgb(16, 185, 129);
            btnEnviar.FlatAppearance.BorderSize = 0;
            btnEnviar.FlatStyle = FlatStyle.Flat;
            btnEnviar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnEnviar.ForeColor = Color.White;
            btnEnviar.Location = new Point(20, 475);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(235, 42);
            btnEnviar.TabIndex = 7;
            btnEnviar.Text = "✉ Enviar Correo";
            btnEnviar.UseVisualStyleBackColor = false;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(71, 85, 105);
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(275, 475);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(235, 42);
            btnCancelar.TabIndex = 8;
            btnCancelar.Text = "Cerrar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormEnviarMail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(530, 535);
            Controls.Add(btnCancelar);
            Controls.Add(btnEnviar);
            Controls.Add(txtVistaPrevia);
            Controls.Add(lblVistaPrevia);
            Controls.Add(txtAsunto);
            Controls.Add(lblAsunto);
            Controls.Add(txtDestinatario);
            Controls.Add(lblDestinatario);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormEnviarMail";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Enviar Comprobante por Email";
            ResumeLayout(false);
            PerformLayout();
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
