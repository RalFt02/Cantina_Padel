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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            gridUsuarios = new DataGridView();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            grpDatos = new GroupBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblPasswordHint = new Label();
            lblRol = new Label();
            cmbRol = new ComboBox();
            chkActivo = new CheckBox();
            Guardar_Boton = new Button();
            Cancelar_Boton = new Button();
            CambiarPass_Boton = new Button();
            Eliminar_Boton = new Button();
            Nuevo_Boton = new Button();
            Volver_Boton = new Button();
            ((System.ComponentModel.ISupportInitialize)gridUsuarios).BeginInit();
            grpDatos.SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            SuspendLayout();
            // 
            // gridUsuarios
            // 
            gridUsuarios.AllowUserToAddRows = false;
            gridUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridUsuarios.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridUsuarios.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridUsuarios.ColumnHeadersHeight = 32;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            gridUsuarios.GridColor = Color.FromArgb(51, 65, 85);
            gridUsuarios.Location = new Point(20, 95);
            gridUsuarios.Name = "gridUsuarios";
            gridUsuarios.ReadOnly = true;
            gridUsuarios.RowHeadersVisible = false;
            gridUsuarios.RowHeadersWidth = 51;
            gridUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridUsuarios.Size = new Size(760, 605);
            gridUsuarios.TabIndex = 3;
            gridUsuarios.CellClick += gridUsuarios_CellClick;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(275, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE USUARIOS";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(20, 60);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(48, 15);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(51, 65, 85);
            txtBuscar.ForeColor = Color.White;
            txtBuscar.Location = new Point(80, 56);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(280, 24);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // grpDatos
            // 
            grpDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpDatos.BackColor = Color.FromArgb(30, 41, 59);
            grpDatos.Controls.Add(lblUsername);
            grpDatos.Controls.Add(txtUsername);
            grpDatos.Controls.Add(lblNombre);
            grpDatos.Controls.Add(txtNombre);
            grpDatos.Controls.Add(lblPassword);
            grpDatos.Controls.Add(txtPassword);
            grpDatos.Controls.Add(lblPasswordHint);
            grpDatos.Controls.Add(lblRol);
            grpDatos.Controls.Add(cmbRol);
            grpDatos.Controls.Add(chkActivo);
            grpDatos.Controls.Add(Guardar_Boton);
            grpDatos.Controls.Add(Cancelar_Boton);
            grpDatos.Controls.Add(CambiarPass_Boton);
            grpDatos.Controls.Add(Eliminar_Boton);
            grpDatos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Location = new Point(800, 95);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(460, 605);
            grpDatos.TabIndex = 4;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos del Usuario";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.ForeColor = Color.White;
            lblUsername.Location = new Point(15, 45);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(56, 15);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Usuario:";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(51, 65, 85);
            txtUsername.ForeColor = Color.White;
            txtUsername.Location = new Point(15, 68);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(430, 24);
            txtUsername.TabIndex = 3;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(15, 115);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(120, 15);
            lblNombre.TabIndex = 4;
            lblNombre.Text = "Nombre completo:";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(15, 138);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(430, 24);
            txtNombre.TabIndex = 5;
            txtNombre.KeyPress += SoloLetras_KeyPress;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = Color.White;
            lblPassword.Location = new Point(15, 185);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(78, 15);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(51, 65, 85);
            txtPassword.ForeColor = Color.White;
            txtPassword.Location = new Point(15, 208);
            txtPassword.MaxLength = 50;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(430, 24);
            txtPassword.TabIndex = 7;
            // 
            // lblPasswordHint
            // 
            lblPasswordHint.AutoSize = true;
            lblPasswordHint.Font = new Font("Segoe UI", 8F);
            lblPasswordHint.ForeColor = Color.Gray;
            lblPasswordHint.Location = new Point(15, 238);
            lblPasswordHint.Name = "lblPasswordHint";
            lblPasswordHint.Size = new Size(210, 13);
            lblPasswordHint.TabIndex = 8;
            lblPasswordHint.Text = "(Dejar vacío para no cambiar al editar)";
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.ForeColor = Color.White;
            lblRol.Location = new Point(15, 270);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(30, 15);
            lblRol.TabIndex = 9;
            lblRol.Text = "Rol:";
            // 
            // cmbRol
            // 
            cmbRol.BackColor = Color.FromArgb(51, 65, 85);
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.ForeColor = Color.White;
            cmbRol.Items.AddRange(new object[] { "Admin", "Cajero" });
            cmbRol.Location = new Point(15, 293);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(280, 24);
            cmbRol.TabIndex = 10;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(15, 340);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(105, 19);
            chkActivo.TabIndex = 11;
            chkActivo.Text = "Cuenta activa";
            // 
            // Guardar_Boton
            // 
            Guardar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.Cursor = Cursors.Hand;
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Location = new Point(15, 480);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(210, 44);
            Guardar_Boton.TabIndex = 12;
            Guardar_Boton.Text = "Crear Usuario";
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;
            // 
            // Eliminar_Boton
            // 
            Eliminar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.Cursor = Cursors.Hand;
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.Location = new Point(235, 480);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(210, 44);
            Eliminar_Boton.TabIndex = 15;
            Eliminar_Boton.Text = "Desactivar";
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;
            // 
            // CambiarPass_Boton
            // 
            CambiarPass_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CambiarPass_Boton.BackColor = Color.FromArgb(59, 130, 246);
            CambiarPass_Boton.Cursor = Cursors.Hand;
            CambiarPass_Boton.FlatAppearance.BorderSize = 0;
            CambiarPass_Boton.FlatStyle = FlatStyle.Flat;
            CambiarPass_Boton.ForeColor = Color.White;
            CambiarPass_Boton.Location = new Point(15, 538);
            CambiarPass_Boton.Name = "CambiarPass_Boton";
            CambiarPass_Boton.Size = new Size(210, 44);
            CambiarPass_Boton.TabIndex = 14;
            CambiarPass_Boton.Text = "Cambiar Contraseña";
            CambiarPass_Boton.UseVisualStyleBackColor = false;
            CambiarPass_Boton.Click += CambiarPass_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.Cursor = Cursors.Hand;
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(235, 538);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(210, 44);
            Cancelar_Boton.TabIndex = 13;
            Cancelar_Boton.Text = "Limpiar";
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click += Cancelar_Boton_Click;
            // 
            // Nuevo_Boton
            // 
            Nuevo_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Nuevo_Boton.Cursor = Cursors.Hand;
            Nuevo_Boton.FlatAppearance.BorderSize = 0;
            Nuevo_Boton.FlatStyle = FlatStyle.Flat;
            Nuevo_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Nuevo_Boton.ForeColor = Color.Black;
            Nuevo_Boton.Location = new Point(380, 53);
            Nuevo_Boton.Name = "Nuevo_Boton";
            Nuevo_Boton.Size = new Size(160, 32);
            Nuevo_Boton.TabIndex = 5;
            Nuevo_Boton.Text = "+ Nuevo Usuario";
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click += Nuevo_Boton_Click;
            // 
            // Volver_Boton
            // 
            Volver_Boton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.Cursor = Cursors.Hand;
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(1130, 53);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(130, 32);
            Volver_Boton.TabIndex = 6;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // FormGestionUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridUsuarios);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormGestionUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Usuarios - Cantina Padel Club";
            ((System.ComponentModel.ISupportInitialize)gridUsuarios).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private DataGridView     gridUsuarios;
        private Label            lblTitulo;
        private Label            lblBuscar;
        private TextBox          txtBuscar;
        private GroupBox         grpDatos;
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
