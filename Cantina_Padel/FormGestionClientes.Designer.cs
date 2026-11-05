namespace Cantina_Padel
{
    partial class FormGestionClientes
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            gridClientes = new DataGridView();
            lblCondicionFiscal = new Label();
            cmbCondicionFiscal = new ComboBox();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            grpDatos = new GroupBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblDni = new Label();
            txtDni = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            lblCuit = new Label();
            txtCuit = new TextBox();
            lblRazonSocial = new Label();
            txtRazonSocial = new TextBox();
            chkActivo = new CheckBox();
            Guardar_Boton = new Button();
            Cancelar_Boton = new Button();
            Eliminar_Boton = new Button();
            Nuevo_Boton = new Button();
            Volver_Boton = new Button();
            ((System.ComponentModel.ISupportInitialize)gridClientes).BeginInit();
            grpDatos.SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            SuspendLayout();
            // 
            // gridClientes
            // 
            gridClientes.AllowUserToAddRows = false;
            gridClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridClientes.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridClientes.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            gridClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            gridClientes.ColumnHeadersHeight = 32;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            gridClientes.DefaultCellStyle = dataGridViewCellStyle4;
            gridClientes.GridColor = Color.FromArgb(51, 65, 85);
            gridClientes.Location = new Point(20, 95);
            gridClientes.Name = "gridClientes";
            gridClientes.ReadOnly = true;
            gridClientes.RowHeadersVisible = false;
            gridClientes.RowHeadersWidth = 51;
            gridClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridClientes.Size = new Size(760, 605);
            gridClientes.TabIndex = 3;
            gridClientes.CellClick += gridClientes_CellClick;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(260, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE CLIENTES";
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
            grpDatos.Controls.Add(lblCondicionFiscal);
            grpDatos.Controls.Add(cmbCondicionFiscal);
            grpDatos.Controls.Add(lblNombre);
            grpDatos.Controls.Add(txtNombre);
            grpDatos.Controls.Add(lblApellido);
            grpDatos.Controls.Add(txtApellido);
            grpDatos.Controls.Add(lblTelefono);
            grpDatos.Controls.Add(txtTelefono);
            grpDatos.Controls.Add(lblDni);
            grpDatos.Controls.Add(txtDni);
            grpDatos.Controls.Add(lblEmail);
            grpDatos.Controls.Add(txtEmail);
            grpDatos.Controls.Add(lblDireccion);
            grpDatos.Controls.Add(txtDireccion);
            grpDatos.Controls.Add(lblCuit);
            grpDatos.Controls.Add(txtCuit);
            grpDatos.Controls.Add(lblRazonSocial);
            grpDatos.Controls.Add(txtRazonSocial);
            grpDatos.Controls.Add(chkActivo);
            grpDatos.Controls.Add(Guardar_Boton);
            grpDatos.Controls.Add(Cancelar_Boton);
            grpDatos.Controls.Add(Eliminar_Boton);
            grpDatos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Location = new Point(800, 95);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(460, 605);
            grpDatos.TabIndex = 4;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos del Cliente";
            // 
            // lblCondicionFiscal
            // 
            lblCondicionFiscal.AutoSize = true;
            lblCondicionFiscal.ForeColor = Color.White;
            lblCondicionFiscal.Location = new Point(10, 26);
            lblCondicionFiscal.Name = "lblCondicionFiscal";
            lblCondicionFiscal.Size = new Size(135, 15);
            lblCondicionFiscal.TabIndex = 0;
            lblCondicionFiscal.Text = "Condición Fiscal / IVA:";
            // 
            // cmbCondicionFiscal
            // 
            cmbCondicionFiscal.BackColor = Color.FromArgb(51, 65, 85);
            cmbCondicionFiscal.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCondicionFiscal.FlatStyle = FlatStyle.Flat;
            cmbCondicionFiscal.ForeColor = Color.White;
            cmbCondicionFiscal.FormattingEnabled = true;
            cmbCondicionFiscal.Items.AddRange(new object[] {
            "Consumidor Final",
            "Monotributista",
            "Responsable Inscripto"});
            cmbCondicionFiscal.Location = new Point(10, 48);
            cmbCondicionFiscal.Name = "cmbCondicionFiscal";
            cmbCondicionFiscal.Size = new Size(435, 24);
            cmbCondicionFiscal.TabIndex = 1;
            cmbCondicionFiscal.SelectedIndexChanged += cmbCondicionFiscal_SelectedIndexChanged;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(10, 88);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(58, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(10, 110);
            txtNombre.MaxLength = 30;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(205, 24);
            txtNombre.TabIndex = 3;
            txtNombre.KeyPress += SoloLetras_KeyPress;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.ForeColor = Color.White;
            lblApellido.Location = new Point(235, 88);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(58, 15);
            lblApellido.TabIndex = 4;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.BackColor = Color.FromArgb(51, 65, 85);
            txtApellido.ForeColor = Color.White;
            txtApellido.Location = new Point(235, 110);
            txtApellido.MaxLength = 30;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(210, 24);
            txtApellido.TabIndex = 5;
            txtApellido.KeyPress += SoloLetras_KeyPress;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.ForeColor = Color.White;
            lblTelefono.Location = new Point(10, 150);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(60, 15);
            lblTelefono.TabIndex = 6;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.BackColor = Color.FromArgb(51, 65, 85);
            txtTelefono.ForeColor = Color.White;
            txtTelefono.Location = new Point(10, 172);
            txtTelefono.MaxLength = 10;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(205, 24);
            txtTelefono.TabIndex = 7;
            txtTelefono.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.ForeColor = Color.White;
            lblDni.Location = new Point(235, 150);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(34, 15);
            lblDni.TabIndex = 8;
            lblDni.Text = "DNI:";
            // 
            // txtDni
            // 
            txtDni.BackColor = Color.FromArgb(51, 65, 85);
            txtDni.ForeColor = Color.White;
            txtDni.Location = new Point(235, 172);
            txtDni.MaxLength = 9;
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(210, 24);
            txtDni.TabIndex = 9;
            txtDni.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.ForeColor = Color.White;
            lblEmail.Location = new Point(10, 212);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(42, 15);
            lblEmail.TabIndex = 10;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(51, 65, 85);
            txtEmail.ForeColor = Color.White;
            txtEmail.Location = new Point(10, 234);
            txtEmail.MaxLength = 30;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(205, 24);
            txtEmail.TabIndex = 11;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.ForeColor = Color.White;
            lblDireccion.Location = new Point(235, 212);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(65, 15);
            lblDireccion.TabIndex = 12;
            lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.BackColor = Color.FromArgb(51, 65, 85);
            txtDireccion.ForeColor = Color.White;
            txtDireccion.Location = new Point(235, 234);
            txtDireccion.MaxLength = 50;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(210, 24);
            txtDireccion.TabIndex = 13;
            // 
            // lblCuit
            // 
            lblCuit.AutoSize = true;
            lblCuit.ForeColor = Color.White;
            lblCuit.Location = new Point(10, 274);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(38, 15);
            lblCuit.TabIndex = 14;
            lblCuit.Text = "CUIT:";
            // 
            // txtCuit
            // 
            txtCuit.BackColor = Color.FromArgb(51, 65, 85);
            txtCuit.ForeColor = Color.White;
            txtCuit.Location = new Point(10, 296);
            txtCuit.MaxLength = 11;
            txtCuit.Name = "txtCuit";
            txtCuit.Size = new Size(205, 24);
            txtCuit.TabIndex = 15;
            txtCuit.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.ForeColor = Color.White;
            lblRazonSocial.Location = new Point(235, 274);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(85, 15);
            lblRazonSocial.TabIndex = 16;
            lblRazonSocial.Text = "Razón Social:";
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.BackColor = Color.FromArgb(51, 65, 85);
            txtRazonSocial.ForeColor = Color.White;
            txtRazonSocial.Location = new Point(235, 296);
            txtRazonSocial.MaxLength = 50;
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(210, 24);
            txtRazonSocial.TabIndex = 17;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(10, 340);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(105, 19);
            chkActivo.TabIndex = 20;
            chkActivo.Text = "Cliente activo";
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
            Guardar_Boton.Location = new Point(10, 475);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(212, 48);
            Guardar_Boton.TabIndex = 21;
            Guardar_Boton.Text = "Crear Cliente";
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
            Eliminar_Boton.Location = new Point(235, 475);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(212, 48);
            Eliminar_Boton.TabIndex = 23;
            Eliminar_Boton.Text = "Desactivar Cliente";
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.Cursor = Cursors.Hand;
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(10, 538);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(437, 45);
            Cancelar_Boton.TabIndex = 22;
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
            Nuevo_Boton.Text = "+ Nuevo Cliente";
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
            // FormGestionClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridClientes);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormGestionClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Clientes - Cantina Padel Club";
            ((System.ComponentModel.ISupportInitialize)gridClientes).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private DataGridView     gridClientes;
        private Label            lblTitulo;
        private Label            lblBuscar;
        private TextBox          txtBuscar;
        private GroupBox         grpDatos;
        private Label            lblNombre;
        private TextBox          txtNombre;
        private Label            lblApellido;
        private TextBox          txtApellido;
        private Label            lblTelefono;
        private TextBox          txtTelefono;
        private Label            lblDni;
        private TextBox          txtDni;
        private Label            lblEmail;
        private TextBox          txtEmail;
        private Label            lblDireccion;
        private TextBox          txtDireccion;
        private Label            lblCuit;
        private TextBox          txtCuit;
        private Label            lblRazonSocial;
        private TextBox          txtRazonSocial;
        private Label            lblCondicionFiscal;
        private ComboBox         cmbCondicionFiscal;
        private CheckBox         chkActivo;
        private Button           Nuevo_Boton;
        private Button           Guardar_Boton;
        private Button           Eliminar_Boton;
        private Button           Cancelar_Boton;
        private Button           Volver_Boton;
    }
}
