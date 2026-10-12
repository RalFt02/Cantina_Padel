namespace Cantina_Padel
{
    partial class FormGestionHorarios
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            lblTitulo = new Label();
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            lblCancha = new Label();
            cmbCancha = new ComboBox();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblHorario = new Label();
            cmbHorario = new ComboBox();
            lblEstado = new Label();
            lblInfo = new Label();
            Reservar_Boton = new Button();
            ReservasFijas_Boton = new Button();
            CancelarReserva_Boton = new Button();
            Realquilar_Boton = new Button();
            Limpiar_Boton = new Button();
            Volver_Boton = new Button();
            lblOcupados = new Label();
            gridOcupados = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)gridOcupados).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(25, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(346, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE RESERVAS";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.ForeColor = Color.White;
            lblCliente.Location = new Point(25, 70);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(58, 20);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "Cliente:";
            // 
            // cmbCliente
            // 
            cmbCliente.BackColor = Color.FromArgb(51, 65, 85);
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.ForeColor = Color.White;
            cmbCliente.Location = new Point(25, 95);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(300, 28);
            cmbCliente.TabIndex = 1;
            // 
            // lblCancha
            // 
            lblCancha.AutoSize = true;
            lblCancha.ForeColor = Color.White;
            lblCancha.Location = new Point(345, 70);
            lblCancha.Name = "lblCancha";
            lblCancha.Size = new Size(60, 20);
            lblCancha.TabIndex = 2;
            lblCancha.Text = "Cancha:";
            // 
            // cmbCancha
            // 
            cmbCancha.BackColor = Color.FromArgb(51, 65, 85);
            cmbCancha.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCancha.ForeColor = Color.White;
            cmbCancha.Location = new Point(345, 95);
            cmbCancha.Name = "cmbCancha";
            cmbCancha.Size = new Size(190, 28);
            cmbCancha.TabIndex = 2;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.ForeColor = Color.White;
            lblFecha.Location = new Point(555, 70);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(50, 20);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(555, 95);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(145, 27);
            dtpFecha.TabIndex = 3;
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.ForeColor = Color.White;
            lblHorario.Location = new Point(720, 70);
            lblHorario.Name = "lblHorario";
            lblHorario.Size = new Size(65, 20);
            lblHorario.TabIndex = 4;
            lblHorario.Text = "Horario:";
            // 
            // cmbHorario
            // 
            cmbHorario.BackColor = Color.FromArgb(51, 65, 85);
            cmbHorario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHorario.ForeColor = Color.White;
            cmbHorario.Location = new Point(720, 95);
            cmbHorario.MaxDropDownItems = 10;
            cmbHorario.Name = "cmbHorario";
            cmbHorario.Size = new Size(210, 28);
            cmbHorario.TabIndex = 4;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.ForeColor = Color.FromArgb(163, 230, 53);
            lblEstado.Location = new Point(25, 165);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(0, 20);
            lblEstado.TabIndex = 7;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.LightGray;
            lblInfo.Location = new Point(25, 140);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(648, 20);
            lblInfo.TabIndex = 6;
            lblInfo.Text = "Turnos de 1 hora, de 08:00 a 03:00 del día siguiente. Los turnos ya reservados no aparecen en la lista.";
            // 
            // Reservar_Boton
            // 
            Reservar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Reservar_Boton.FlatStyle = FlatStyle.Flat;
            Reservar_Boton.ForeColor = Color.Black;
            Reservar_Boton.Location = new Point(25, 195);
            Reservar_Boton.Name = "Reservar_Boton";
            Reservar_Boton.Size = new Size(180, 38);
            Reservar_Boton.TabIndex = 8;
            Reservar_Boton.Text = "Reservar";
            Reservar_Boton.UseVisualStyleBackColor = false;
            Reservar_Boton.Click += Reservar_Boton_Click;
            // 
            // ReservasFijas_Boton
            // 
            ReservasFijas_Boton.BackColor = Color.FromArgb(59, 130, 246);
            ReservasFijas_Boton.FlatStyle = FlatStyle.Flat;
            ReservasFijas_Boton.ForeColor = Color.White;
            ReservasFijas_Boton.Location = new Point(220, 195);
            ReservasFijas_Boton.Name = "ReservasFijas_Boton";
            ReservasFijas_Boton.Size = new Size(200, 38);
            ReservasFijas_Boton.TabIndex = 9;
            ReservasFijas_Boton.Text = "Reservas Fijas";
            ReservasFijas_Boton.UseVisualStyleBackColor = false;
            ReservasFijas_Boton.Click += ReservasFijas_Boton_Click;
            // 
            // CancelarReserva_Boton
            // 
            CancelarReserva_Boton.BackColor = Color.FromArgb(239, 68, 68);
            CancelarReserva_Boton.FlatStyle = FlatStyle.Flat;
            CancelarReserva_Boton.ForeColor = Color.White;
            CancelarReserva_Boton.Location = new Point(25, 535);
            CancelarReserva_Boton.Name = "CancelarReserva_Boton";
            CancelarReserva_Boton.Size = new Size(180, 35);
            CancelarReserva_Boton.TabIndex = 12;
            CancelarReserva_Boton.Text = "Cancelar reserva";
            CancelarReserva_Boton.UseVisualStyleBackColor = false;
            CancelarReserva_Boton.Click += CancelarReserva_Boton_Click;
            // 
            // Realquilar_Boton
            // 
            Realquilar_Boton.BackColor = Color.FromArgb(245, 158, 11);
            Realquilar_Boton.FlatStyle = FlatStyle.Flat;
            Realquilar_Boton.ForeColor = Color.Black;
            Realquilar_Boton.Location = new Point(220, 535);
            Realquilar_Boton.Name = "Realquilar_Boton";
            Realquilar_Boton.Size = new Size(180, 35);
            Realquilar_Boton.TabIndex = 13;
            Realquilar_Boton.Text = "Realquilar hora";
            Realquilar_Boton.UseVisualStyleBackColor = false;
            Realquilar_Boton.Click += Realquilar_Boton_Click;
            // 
            // Limpiar_Boton
            // 
            Limpiar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Limpiar_Boton.FlatStyle = FlatStyle.Flat;
            Limpiar_Boton.ForeColor = Color.White;
            Limpiar_Boton.Location = new Point(435, 195);
            Limpiar_Boton.Name = "Limpiar_Boton";
            Limpiar_Boton.Size = new Size(150, 38);
            Limpiar_Boton.TabIndex = 10;
            Limpiar_Boton.Text = "Limpiar";
            Limpiar_Boton.UseVisualStyleBackColor = false;
            Limpiar_Boton.Click += Limpiar_Boton_Click;
            // 
            // Volver_Boton
            // 
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(790, 535);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(140, 35);
            Volver_Boton.TabIndex = 14;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // lblOcupados
            // 
            lblOcupados.AutoSize = true;
            lblOcupados.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOcupados.ForeColor = Color.LightGray;
            lblOcupados.Location = new Point(25, 255);
            lblOcupados.Name = "lblOcupados";
            lblOcupados.Size = new Size(270, 23);
            lblOcupados.TabIndex = 11;
            lblOcupados.Text = "Horarios ocupados / bloqueados";
            // 
            // gridOcupados
            // 
            gridOcupados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridOcupados.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridOcupados.BorderStyle = BorderStyle.None;
            gridOcupados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridOcupados.ColumnHeadersHeight = 29;
            gridOcupados.Location = new Point(25, 280);
            gridOcupados.Name = "gridOcupados";
            gridOcupados.ReadOnly = true;
            gridOcupados.RowHeadersVisible = false;
            gridOcupados.RowHeadersWidth = 51;
            gridOcupados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridOcupados.Size = new Size(905, 235);
            gridOcupados.TabIndex = 10;
            // 
            // FormGestionHorarios
            // 
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(960, 590);
            Controls.Add(lblTitulo);
            Controls.Add(lblCliente);
            Controls.Add(cmbCliente);
            Controls.Add(lblCancha);
            Controls.Add(cmbCancha);
            Controls.Add(lblFecha);
            Controls.Add(dtpFecha);
            Controls.Add(lblHorario);
            Controls.Add(cmbHorario);
            Controls.Add(lblInfo);
            Controls.Add(lblEstado);
            Controls.Add(Reservar_Boton);
            Controls.Add(ReservasFijas_Boton);
            Controls.Add(Limpiar_Boton);
            Controls.Add(lblOcupados);
            Controls.Add(gridOcupados);
            Controls.Add(CancelarReserva_Boton);
            Controls.Add(Realquilar_Boton);
            Controls.Add(Volver_Boton);
            Name = "FormGestionHorarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Reservas";
            ((System.ComponentModel.ISupportInitialize)gridOcupados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo,lblCliente,lblCancha,lblFecha,lblHorario,lblEstado,lblInfo,lblOcupados;
        private ComboBox cmbCliente,cmbCancha,cmbHorario;
        private DateTimePicker dtpFecha;
        private Button Reservar_Boton,ReservasFijas_Boton,CancelarReserva_Boton,Realquilar_Boton,Limpiar_Boton,Volver_Boton;
        private DataGridView gridOcupados;
    }
}
