namespace Cantina_Padel
{
    partial class FormGestionCanchas
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            gridCanchas = new DataGridView();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            grpDatos = new GroupBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            chkActivo = new CheckBox();
            Guardar_Boton = new Button();
            Cancelar_Boton = new Button();
            Eliminar_Boton = new Button();
            Nuevo_Boton = new Button();
            Volver_Boton = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)gridCanchas).BeginInit();
            grpDatos.SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            SuspendLayout();
            // 
            // gridCanchas
            // 
            gridCanchas.AllowUserToAddRows = false;
            gridCanchas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridCanchas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCanchas.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridCanchas.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            gridCanchas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            gridCanchas.ColumnHeadersHeight = 32;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.White;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            gridCanchas.DefaultCellStyle = dataGridViewCellStyle6;
            gridCanchas.GridColor = Color.FromArgb(51, 65, 85);
            gridCanchas.Location = new Point(20, 95);
            gridCanchas.Name = "gridCanchas";
            gridCanchas.ReadOnly = true;
            gridCanchas.RowHeadersVisible = false;
            gridCanchas.RowHeadersWidth = 51;
            gridCanchas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCanchas.Size = new Size(760, 605);
            gridCanchas.TabIndex = 3;
            gridCanchas.CellClick += gridCanchas_CellClick;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(270, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE CANCHAS";
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
            grpDatos.Controls.Add(label1);
            grpDatos.Controls.Add(textBox1);
            grpDatos.Controls.Add(lblNombre);
            grpDatos.Controls.Add(txtNombre);
            grpDatos.Controls.Add(lblEstado);
            grpDatos.Controls.Add(cmbEstado);
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
            grpDatos.Text = "Datos de la Cancha";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(15, 45);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(58, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(15, 70);
            txtNombre.MaxLength = 30;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(430, 24);
            txtNombre.TabIndex = 1;
            txtNombre.KeyPress += NombreCancha_KeyPress;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.ForeColor = Color.White;
            lblEstado.Location = new Point(15, 125);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(50, 15);
            lblEstado.TabIndex = 2;
            lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.BackColor = Color.FromArgb(51, 65, 85);
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.ForeColor = Color.White;
            cmbEstado.Items.AddRange(new object[] { "Activo", "Fuera de Servicio", "En Reparación" });
            cmbEstado.Location = new Point(15, 150);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(430, 24);
            cmbEstado.TabIndex = 3;
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(15, 205);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 9;
            label1.Text = "Precio ($):";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(51, 65, 85);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(15, 228);
            textBox1.MaxLength = 30;
            textBox1.Name = "textBox1";
            textBox1.Text = "0.00";
            textBox1.Size = new Size(200, 24);
            textBox1.TabIndex = 8;
            textBox1.KeyPress += Precio_KeyPress;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(15, 275);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(105, 19);
            chkActivo.TabIndex = 4;
            chkActivo.Text = "Cancha activa";
            // 
            // Guardar_Boton
            // 
            Guardar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.Cursor = Cursors.Hand;
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Location = new Point(15, 425);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(430, 44);
            Guardar_Boton.TabIndex = 5;
            Guardar_Boton.Text = "Crear Cancha";
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;
            // 
            // Eliminar_Boton
            // 
            Eliminar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.Cursor = Cursors.Hand;
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.Location = new Point(15, 482);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(430, 44);
            Eliminar_Boton.TabIndex = 7;
            Eliminar_Boton.Text = "Desactivar Cancha";
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
            Cancelar_Boton.Location = new Point(15, 538);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(430, 44);
            Cancelar_Boton.TabIndex = 6;
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
            Nuevo_Boton.Text = "+ Nueva Cancha";
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
            // FormGestionCanchas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridCanchas);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormGestionCanchas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Canchas - Cantina Padel Club";
            ((System.ComponentModel.ISupportInitialize)gridCanchas).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private DataGridView     gridCanchas;
        private Label            lblTitulo;
        private Label            lblBuscar;
        private TextBox          txtBuscar;
        private GroupBox         grpDatos;
        private Label            lblNombre;
        private TextBox          txtNombre;
        private Label            lblEstado;
        private ComboBox         cmbEstado;
        private CheckBox         chkActivo;
        private Button           Nuevo_Boton;
        private Button           Guardar_Boton;
        private Button           Eliminar_Boton;
        private Button           Cancelar_Boton;
        private Button           Volver_Boton;
        private Label label1;
        private TextBox textBox1;
    }
}
