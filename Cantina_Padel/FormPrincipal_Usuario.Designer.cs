namespace Cantina_Padel
{
    partial class FormPrincipal_Usuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal_Usuario));
            pictureBox1 = new PictureBox();
            Clientes_Boton = new Button();
            Caja_Boton = new Button();
            Ventas_Boton = new Button();
            Reservas_Boton = new Button();
            Volver_Menu = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(413, 93);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(494, 270);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Clientes_Boton
            // 
            Clientes_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Clientes_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Clientes_Boton.ForeColor = Color.Black;
            Clientes_Boton.Location = new Point(100, 472);
            Clientes_Boton.Name = "Clientes_Boton";
            Clientes_Boton.Size = new Size(216, 64);
            Clientes_Boton.TabIndex = 1;
            Clientes_Boton.Text = "Clientes";
            Clientes_Boton.UseVisualStyleBackColor = false;
            Clientes_Boton.Click += Clientes_Boton_Click;
            // 
            // Caja_Boton
            // 
            Caja_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Caja_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Caja_Boton.ForeColor = Color.Black;
            Caja_Boton.Location = new Point(400, 472);
            Caja_Boton.Name = "Caja_Boton";
            Caja_Boton.Size = new Size(216, 64);
            Caja_Boton.TabIndex = 2;
            Caja_Boton.Text = "Caja";
            Caja_Boton.UseVisualStyleBackColor = false;
            // 
            // Ventas_Boton
            // 
            Ventas_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Ventas_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Ventas_Boton.ForeColor = Color.Black;
            Ventas_Boton.Location = new Point(700, 472);
            Ventas_Boton.Name = "Ventas_Boton";
            Ventas_Boton.Size = new Size(216, 64);
            Ventas_Boton.TabIndex = 3;
            Ventas_Boton.Text = "Ventas";
            Ventas_Boton.UseVisualStyleBackColor = false;
            // 
            // Reservas_Boton
            // 
            Reservas_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Reservas_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Reservas_Boton.ForeColor = Color.Black;
            Reservas_Boton.Location = new Point(1000, 472);
            Reservas_Boton.Name = "Reservas_Boton";
            Reservas_Boton.Size = new Size(216, 64);
            Reservas_Boton.TabIndex = 5;
            Reservas_Boton.Text = "Reservas";
            Reservas_Boton.UseVisualStyleBackColor = false;
            // 
            // Volver_Menu
            // 
            Volver_Menu.BackColor = Color.FromArgb(163, 230, 53);
            Volver_Menu.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            Volver_Menu.ForeColor = Color.Black;
            Volver_Menu.Location = new Point(19, 19);
            Volver_Menu.Name = "Volver_Menu";
            Volver_Menu.Size = new Size(216, 56);
            Volver_Menu.TabIndex = 6;
            Volver_Menu.Text = "Volver al Menu";
            Volver_Menu.UseVisualStyleBackColor = false;
            Volver_Menu.Click += Volver_Menu_Click;
            // 
            // FormPrincipal_Usuario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(Volver_Menu);
            Controls.Add(Reservas_Boton);
            Controls.Add(Ventas_Boton);
            Controls.Add(Caja_Boton);
            Controls.Add(Clientes_Boton);
            Controls.Add(pictureBox1);
            ForeColor = Color.Transparent;
            Name = "FormPrincipal_Usuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            TransparencyKey = Color.Transparent;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button Clientes_Boton;
        private Button Caja_Boton;
        private Button Ventas_Boton;
        private Button Reservas_Boton;
        private Button Volver_Menu;
    }
}