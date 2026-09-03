namespace Cantina_Padel
{
    partial class FormGestionProveedores
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
            gridProveedores = new DataGridView();
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
            lblObservaciones = new Label();
            txtObservaciones = new TextBox();
            chkActivo = new CheckBox();
            Guardar_Boton = new Button();
            Cancelar_Boton = new Button();
            Eliminar_Boton = new Button();
            Nuevo_Boton = new Button();
            Volver_Boton = new Button();
            ((System.ComponentModel.ISupportInitialize)gridProveedores).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();
            // 
            // gridProveedores
            // 
            gridProveedores.AllowUserToAddRows = false;
            gridProveedores.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridProveedores.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridProveedores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridProveedores.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridProveedores.DefaultCellStyle = dataGridViewCellStyle2;
            gridProveedores.GridColor = Color.FromArgb(51, 65, 85);
            gridProveedores.Location = new Point(20, 95);
            gridProveedores.Name = "gridProveedores";
            gridProveedores.ReadOnly = true;
            gridProveedores.RowHeadersVisible = false;
            gridProveedores.RowHeadersWidth = 51;
            gridProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridProveedores.Size = new Size(580, 520);
            gridProveedores.TabIndex = 3;
            gridProveedores.CellClick += gridProveedores_CellClick;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(367, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE PROVEEDORES";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(20, 60);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(55, 20);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(80, 57);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(250, 27);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // grpDatos
            // 
            grpDatos.BackColor = Color.FromArgb(30, 41, 59);
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
            grpDatos.Controls.Add(lblObservaciones);
            grpDatos.Controls.Add(txtObservaciones);
            grpDatos.Controls.Add(chkActivo);
            grpDatos.Controls.Add(Guardar_Boton);
            grpDatos.Controls.Add(Cancelar_Boton);
            grpDatos.Controls.Add(Eliminar_Boton);
            grpDatos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Location = new Point(620, 95);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(460, 520);
            grpDatos.TabIndex = 4;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos del Proveedor";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(10, 25);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(71, 20);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(10, 50);
            txtNombre.MaxLength = 20;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(205, 27);
            txtNombre.TabIndex = 3;
            txtNombre.KeyPress += SoloLetras_KeyPress;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.ForeColor = Color.White;
            lblApellido.Location = new Point(235, 25);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(71, 20);
            lblApellido.TabIndex = 4;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.BackColor = Color.FromArgb(51, 65, 85);
            txtApellido.ForeColor = Color.White;
            txtApellido.Location = new Point(235, 50);
            txtApellido.MaxLength = 20;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(205, 27);
            txtApellido.TabIndex = 5;
            txtApellido.KeyPress += SoloLetras_KeyPress;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.ForeColor = Color.White;
            lblTelefono.Location = new Point(10, 85);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(74, 20);
            lblTelefono.TabIndex = 6;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.BackColor = Color.FromArgb(51, 65, 85);
            txtTelefono.ForeColor = Color.White;
            txtTelefono.Location = new Point(10, 110);
            txtTelefono.MaxLength = 10;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(205, 27);
            txtTelefono.TabIndex = 7;
            txtTelefono.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.ForeColor = Color.White;
            lblDni.Location = new Point(235, 85);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(41, 20);
            lblDni.TabIndex = 8;
            lblDni.Text = "DNI:";
            // 
            // txtDni
            // 
            txtDni.BackColor = Color.FromArgb(51, 65, 85);
            txtDni.ForeColor = Color.White;
            txtDni.Location = new Point(235, 110);
            txtDni.MaxLength = 9;
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(205, 27);
            txtDni.TabIndex = 9;
            txtDni.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.ForeColor = Color.White;
            lblEmail.Location = new Point(10, 140);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(51, 20);
            lblEmail.TabIndex = 10;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(51, 65, 85);
            txtEmail.ForeColor = Color.White;
            txtEmail.Location = new Point(10, 161);
            txtEmail.MaxLength = 30;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(205, 27);
            txtEmail.TabIndex = 11;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.ForeColor = Color.White;
            lblDireccion.Location = new Point(235, 140);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(78, 20);
            lblDireccion.TabIndex = 12;
            lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.BackColor = Color.FromArgb(51, 65, 85);
            txtDireccion.ForeColor = Color.White;
            txtDireccion.Location = new Point(235, 161);
            txtDireccion.MaxLength = 40;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(205, 27);
            txtDireccion.TabIndex = 13;
            // 
            // lblCuit
            // 
            lblCuit.AutoSize = true;
            lblCuit.ForeColor = Color.White;
            lblCuit.Location = new Point(10, 190);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(47, 20);
            lblCuit.TabIndex = 14;
            lblCuit.Text = "CUIT:";
            // 
            // txtCuit
            // 
            txtCuit.BackColor = Color.FromArgb(51, 65, 85);
            txtCuit.ForeColor = Color.White;
            txtCuit.Location = new Point(10, 215);
            txtCuit.MaxLength = 12;
            txtCuit.Name = "txtCuit";
            txtCuit.Size = new Size(205, 27);
            txtCuit.TabIndex = 15;
            txtCuit.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.ForeColor = Color.White;
            lblRazonSocial.Location = new Point(235, 190);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(100, 20);
            lblRazonSocial.TabIndex = 16;
            lblRazonSocial.Text = "Razón Social:";
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.BackColor = Color.FromArgb(51, 65, 85);
            txtRazonSocial.ForeColor = Color.White;
            txtRazonSocial.Location = new Point(235, 215);
            txtRazonSocial.MaxLength = 40;
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(205, 27);
            txtRazonSocial.TabIndex = 17;
            // 
            // lblObservaciones
            // 
            lblObservaciones.AutoSize = true;
            lblObservaciones.ForeColor = Color.White;
            lblObservaciones.Location = new Point(10, 256);
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.Size = new Size(115, 20);
            lblObservaciones.TabIndex = 18;
            lblObservaciones.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            txtObservaciones.BackColor = Color.FromArgb(51, 65, 85);
            txtObservaciones.ForeColor = Color.White;
            txtObservaciones.Location = new Point(10, 287);
            txtObservaciones.MaxLength = 100;
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(430, 60);
            txtObservaciones.TabIndex = 19;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(10, 357);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(149, 24);
            chkActivo.TabIndex = 20;
            chkActivo.Text = "Proveedor activo";
            // 
            // Guardar_Boton
            // 
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Location = new Point(10, 400);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(215, 38);
            Guardar_Boton.TabIndex = 21;
            Guardar_Boton.Text = "Crear Proveedor";
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(239, 450);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(215, 38);
            Cancelar_Boton.TabIndex = 22;
            Cancelar_Boton.Text = "Limpiar";
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click += Cancelar_Boton_Click;
            // 
            // Eliminar_Boton
            // 
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.Location = new Point(239, 400);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(215, 38);
            Eliminar_Boton.TabIndex = 23;
            Eliminar_Boton.Text = "Desactivar Proveedor";
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;
            // 
            // Nuevo_Boton
            // 
            Nuevo_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Nuevo_Boton.FlatAppearance.BorderSize = 0;
            Nuevo_Boton.FlatStyle = FlatStyle.Flat;
            Nuevo_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Nuevo_Boton.ForeColor = Color.Black;
            Nuevo_Boton.Location = new Point(350, 55);
            Nuevo_Boton.Name = "Nuevo_Boton";
            Nuevo_Boton.Size = new Size(150, 32);
            Nuevo_Boton.TabIndex = 5;
            Nuevo_Boton.Text = "+ Nuevo Proveedor";
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click += Nuevo_Boton_Click;
            // 
            // Volver_Boton
            // 
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(970, 55);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(110, 32);
            Volver_Boton.TabIndex = 6;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // FormGestionProveedores
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1100, 650);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridProveedores);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Name = "FormGestionProveedores";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Proveedores";
            ((System.ComponentModel.ISupportInitialize)gridProveedores).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView     gridProveedores;
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
        private Label            lblObservaciones;
        private TextBox          txtObservaciones;
        private CheckBox         chkActivo;
        private Button           Nuevo_Boton;
        private Button           Guardar_Boton;
        private Button           Eliminar_Boton;
        private Button           Cancelar_Boton;
        private Button           Volver_Boton;
    }
}
