namespace Cantina_Padel
{
    partial class FormGestionCategorias
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
            gridCategorias = new DataGridView();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            grpDatos = new GroupBox();
            lblId = new Label();
            txtId = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            chkActivo = new CheckBox();
            Guardar_Boton = new Button();
            Cancelar_Boton = new Button();
            Eliminar_Boton = new Button();
            Nuevo_Boton = new Button();
            Volver_Boton = new Button();
            ((System.ComponentModel.ISupportInitialize)gridCategorias).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();
            // 
            // gridCategorias
            // 
            gridCategorias.AllowUserToAddRows = false;
            gridCategorias.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridCategorias.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridCategorias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridCategorias.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridCategorias.DefaultCellStyle = dataGridViewCellStyle2;
            gridCategorias.GridColor = Color.FromArgb(51, 65, 85);
            gridCategorias.Location = new Point(20, 95);
            gridCategorias.Name = "gridCategorias";
            gridCategorias.ReadOnly = true;
            gridCategorias.RowHeadersVisible = false;
            gridCategorias.RowHeadersWidth = 51;
            gridCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCategorias.Size = new Size(400, 360);
            gridCategorias.TabIndex = 3;
            gridCategorias.CellClick += gridCategorias_CellClick;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(343, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE CATEGORÍAS";
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
            grpDatos.Controls.Add(lblId);
            grpDatos.Controls.Add(txtId);
            grpDatos.Controls.Add(lblNombre);
            grpDatos.Controls.Add(txtNombre);
            grpDatos.Controls.Add(chkActivo);
            grpDatos.Controls.Add(Guardar_Boton);
            grpDatos.Controls.Add(Cancelar_Boton);
            grpDatos.Controls.Add(Eliminar_Boton);
            grpDatos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Location = new Point(440, 95);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(300, 360);
            grpDatos.TabIndex = 4;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos de la Categoría";
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.ForeColor = Color.Gray;
            lblId.Location = new Point(10, 28);
            lblId.Name = "lblId";
            lblId.Size = new Size(29, 20);
            lblId.TabIndex = 0;
            lblId.Text = "ID:";
            // 
            // txtId
            // 
            txtId.BackColor = Color.FromArgb(15, 23, 42);
            txtId.ForeColor = Color.Gray;
            txtId.Location = new Point(60, 25);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(60, 27);
            txtId.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(10, 68);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(71, 20);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(10, 90);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(280, 27);
            txtNombre.TabIndex = 3;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(10, 130);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(143, 24);
            chkActivo.TabIndex = 4;
            chkActivo.Text = "Categoría activa";
            // 
            // Guardar_Boton
            // 
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Location = new Point(10, 170);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(280, 38);
            Guardar_Boton.TabIndex = 5;
            Guardar_Boton.Text = "Crear Categoría";
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(10, 216);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(280, 38);
            Cancelar_Boton.TabIndex = 6;
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
            Eliminar_Boton.Location = new Point(10, 262);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(280, 35);
            Eliminar_Boton.TabIndex = 7;
            Eliminar_Boton.Text = "Desactivar Categoría";
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
            Nuevo_Boton.Location = new Point(348, 54);
            Nuevo_Boton.Name = "Nuevo_Boton";
            Nuevo_Boton.Size = new Size(160, 32);
            Nuevo_Boton.TabIndex = 5;
            Nuevo_Boton.Text = "+ Nueva Categoría";
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click += Nuevo_Boton_Click;
            // 
            // Volver_Boton
            // 
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(650, 55);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(110, 32);
            Volver_Boton.TabIndex = 6;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // FormGestionCategorias
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(780, 500);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridCategorias);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Name = "FormGestionCategorias";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Categorías";
            ((System.ComponentModel.ISupportInitialize)gridCategorias).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView     gridCategorias;
        private Label            lblTitulo;
        private Label            lblBuscar;
        private TextBox          txtBuscar;
        private GroupBox         grpDatos;
        private Label            lblId;
        private TextBox          txtId;
        private Label            lblNombre;
        private TextBox          txtNombre;
        private CheckBox         chkActivo;
        private Button           Nuevo_Boton;
        private Button           Guardar_Boton;
        private Button           Eliminar_Boton;
        private Button           Cancelar_Boton;
        private Button           Volver_Boton;
    }
}
