namespace Cantina_Padel
{
    partial class FormGestionHorarios
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
            gridHorarios = new DataGridView();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            grpDatos = new GroupBox();
            lblCancha = new Label();
            txtCancha = new TextBox();
            lblDia = new Label();
            cmbDia = new ComboBox();
            lblHoraInicio = new Label();
            txtHoraInicio = new TextBox();
            lblHoraFin = new Label();
            txtHoraFin = new TextBox();
            chkActivo = new CheckBox();
            Guardar_Boton = new Button();
            Cancelar_Boton = new Button();
            Eliminar_Boton = new Button();
            Nuevo_Boton = new Button();
            Volver_Boton = new Button();
            ((System.ComponentModel.ISupportInitialize)gridHorarios).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();
            // 
            // gridHorarios
            // 
            gridHorarios.AllowUserToAddRows = false;
            gridHorarios.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridHorarios.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridHorarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridHorarios.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridHorarios.DefaultCellStyle = dataGridViewCellStyle2;
            gridHorarios.GridColor = Color.FromArgb(51, 65, 85);
            gridHorarios.Location = new Point(20, 95);
            gridHorarios.Name = "gridHorarios";
            gridHorarios.ReadOnly = true;
            gridHorarios.RowHeadersVisible = false;
            gridHorarios.RowHeadersWidth = 51;
            gridHorarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridHorarios.Size = new Size(400, 370);
            gridHorarios.TabIndex = 3;
            gridHorarios.CellClick += gridHorarios_CellClick;
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
            lblTitulo.Text = "GESTIÓN DE HORARIOS DE JUEGO";
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
            grpDatos.Controls.Add(lblCancha);
            grpDatos.Controls.Add(txtCancha);
            grpDatos.Controls.Add(lblDia);
            grpDatos.Controls.Add(cmbDia);
            grpDatos.Controls.Add(lblHoraInicio);
            grpDatos.Controls.Add(txtHoraInicio);
            grpDatos.Controls.Add(lblHoraFin);
            grpDatos.Controls.Add(txtHoraFin);
            grpDatos.Controls.Add(chkActivo);
            grpDatos.Controls.Add(Guardar_Boton);
            grpDatos.Controls.Add(Cancelar_Boton);
            grpDatos.Controls.Add(Eliminar_Boton);
            grpDatos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Location = new Point(440, 95);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(300, 370);
            grpDatos.TabIndex = 4;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos del Horario";
            // 
            // lblCancha
            // 
            lblCancha.AutoSize = true;
            lblCancha.ForeColor = Color.White;
            lblCancha.Location = new Point(10, 25);
            lblCancha.Name = "lblCancha";
            lblCancha.Size = new Size(97, 20);
            lblCancha.TabIndex = 0;
            lblCancha.Text = "N° de Cancha:";
            // 
            // txtCancha
            // 
            txtCancha.BackColor = Color.FromArgb(51, 65, 85);
            txtCancha.ForeColor = Color.White;
            txtCancha.Location = new Point(10, 47);
            txtCancha.MaxLength = 2;
            txtCancha.Name = "txtCancha";
            txtCancha.Size = new Size(130, 27);
            txtCancha.TabIndex = 1;
            txtCancha.KeyPress += SoloNumeros_KeyPress;
            // 
            // lblDia
            // 
            lblDia.AutoSize = true;
            lblDia.ForeColor = Color.White;
            lblDia.Location = new Point(155, 25);
            lblDia.Name = "lblDia";
            lblDia.Size = new Size(33, 20);
            lblDia.TabIndex = 2;
            lblDia.Text = "Día:";
            // 
            // cmbDia
            // 
            cmbDia.BackColor = Color.FromArgb(51, 65, 85);
            cmbDia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDia.ForeColor = Color.White;
            cmbDia.Items.AddRange(new object[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" });
            cmbDia.Location = new Point(155, 47);
            cmbDia.Name = "cmbDia";
            cmbDia.Size = new Size(135, 28);
            cmbDia.TabIndex = 3;
            // 
            // lblHoraInicio
            // 
            lblHoraInicio.AutoSize = true;
            lblHoraInicio.ForeColor = Color.White;
            lblHoraInicio.Location = new Point(10, 90);
            lblHoraInicio.Name = "lblHoraInicio";
            lblHoraInicio.Size = new Size(140, 20);
            lblHoraInicio.TabIndex = 4;
            lblHoraInicio.Text = "Hora Inicio (HH:mm):";
            // 
            // txtHoraInicio
            // 
            txtHoraInicio.BackColor = Color.FromArgb(51, 65, 85);
            txtHoraInicio.ForeColor = Color.White;
            txtHoraInicio.Location = new Point(10, 112);
            txtHoraInicio.MaxLength = 5;
            txtHoraInicio.Name = "txtHoraInicio";
            txtHoraInicio.PlaceholderText = "08:00";
            txtHoraInicio.Size = new Size(130, 27);
            txtHoraInicio.TabIndex = 5;
            txtHoraInicio.KeyPress += SoloHora_KeyPress;
            // 
            // lblHoraFin
            // 
            lblHoraFin.AutoSize = true;
            lblHoraFin.ForeColor = Color.White;
            lblHoraFin.Location = new Point(155, 90);
            lblHoraFin.Name = "lblHoraFin";
            lblHoraFin.Size = new Size(123, 20);
            lblHoraFin.TabIndex = 6;
            lblHoraFin.Text = "Hora Fin (HH:mm):";
            // 
            // txtHoraFin
            // 
            txtHoraFin.BackColor = Color.FromArgb(51, 65, 85);
            txtHoraFin.ForeColor = Color.White;
            txtHoraFin.Location = new Point(155, 112);
            txtHoraFin.MaxLength = 5;
            txtHoraFin.Name = "txtHoraFin";
            txtHoraFin.PlaceholderText = "09:30";
            txtHoraFin.Size = new Size(135, 27);
            txtHoraFin.TabIndex = 7;
            txtHoraFin.KeyPress += SoloHora_KeyPress;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(10, 155);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(120, 24);
            chkActivo.TabIndex = 8;
            chkActivo.Text = "Horario activo";
            // 
            // Guardar_Boton
            // 
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Location = new Point(10, 195);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(280, 38);
            Guardar_Boton.TabIndex = 9;
            Guardar_Boton.Text = "Crear Horario";
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;
            // 
            // Eliminar_Boton
            // 
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.Location = new Point(10, 245);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(280, 35);
            Eliminar_Boton.TabIndex = 10;
            Eliminar_Boton.Text = "Desactivar Horario";
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(10, 291);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(280, 38);
            Cancelar_Boton.TabIndex = 11;
            Cancelar_Boton.Text = "Limpiar";
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click += Cancelar_Boton_Click;
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
            Nuevo_Boton.Text = "+ Nuevo Horario";
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
            // FormGestionHorarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(780, 500);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridHorarios);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Name = "FormGestionHorarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Horarios de Juego";
            ((System.ComponentModel.ISupportInitialize)gridHorarios).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView     gridHorarios;
        private Label            lblTitulo;
        private Label            lblBuscar;
        private TextBox          txtBuscar;
        private GroupBox         grpDatos;
        private Label            lblCancha;
        private TextBox          txtCancha;
        private Label            lblDia;
        private ComboBox         cmbDia;
        private Label            lblHoraInicio;
        private TextBox          txtHoraInicio;
        private Label            lblHoraFin;
        private TextBox          txtHoraFin;
        private CheckBox         chkActivo;
        private Button           Nuevo_Boton;
        private Button           Guardar_Boton;
        private Button           Eliminar_Boton;
        private Button           Cancelar_Boton;
        private Button           Volver_Boton;
    }
}
