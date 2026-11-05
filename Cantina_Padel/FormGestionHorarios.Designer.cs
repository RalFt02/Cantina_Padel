namespace Cantina_Padel
{
    partial class FormGestionHorarios
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
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
            this.BindingContext = new System.Windows.Forms.BindingContext();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(25, 16);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(300, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE RESERVAS";
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
            Volver_Boton.Location = new Point(1125, 16);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(130, 36);
            Volver_Boton.TabIndex = 14;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCliente.ForeColor = Color.White;
            lblCliente.Location = new Point(25, 68);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(50, 15);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "Cliente:";
            // 
            // cmbCliente
            // 
            cmbCliente.BackColor = Color.FromArgb(51, 65, 85);
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.ForeColor = Color.White;
            cmbCliente.Location = new Point(25, 90);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(380, 25);
            cmbCliente.TabIndex = 1;
            // 
            // lblCancha
            // 
            lblCancha.AutoSize = true;
            lblCancha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCancha.ForeColor = Color.White;
            lblCancha.Location = new Point(430, 68);
            lblCancha.Name = "lblCancha";
            lblCancha.Size = new Size(52, 15);
            lblCancha.TabIndex = 2;
            lblCancha.Text = "Cancha:";
            // 
            // cmbCancha
            // 
            cmbCancha.BackColor = Color.FromArgb(51, 65, 85);
            cmbCancha.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCancha.ForeColor = Color.White;
            cmbCancha.Location = new Point(430, 90);
            cmbCancha.Name = "cmbCancha";
            cmbCancha.Size = new Size(250, 25);
            cmbCancha.TabIndex = 2;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFecha.ForeColor = Color.White;
            lblFecha.Location = new Point(705, 68);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(42, 15);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(705, 90);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(200, 24);
            dtpFecha.TabIndex = 3;
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHorario.ForeColor = Color.White;
            lblHorario.Location = new Point(930, 68);
            lblHorario.Name = "lblHorario";
            lblHorario.Size = new Size(55, 15);
            lblHorario.TabIndex = 4;
            lblHorario.Text = "Horario:";
            // 
            // cmbHorario
            // 
            cmbHorario.BackColor = Color.FromArgb(51, 65, 85);
            cmbHorario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHorario.ForeColor = Color.White;
            cmbHorario.Location = new Point(930, 90);
            cmbHorario.MaxDropDownItems = 10;
            cmbHorario.Name = "cmbHorario";
            cmbHorario.Size = new Size(325, 25);
            cmbHorario.TabIndex = 4;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.LightGray;
            lblInfo.Location = new Point(25, 130);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(540, 15);
            lblInfo.TabIndex = 6;
            lblInfo.Text = "Turnos de 1 hora, de 08:00 a 03:00 del día siguiente. Los turnos ya reservados no aparecen en la lista.";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEstado.ForeColor = Color.FromArgb(163, 230, 53);
            lblEstado.Location = new Point(25, 152);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(0, 17);
            lblEstado.TabIndex = 7;
            // 
            // Reservar_Boton
            // 
            Reservar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Reservar_Boton.Cursor = Cursors.Hand;
            Reservar_Boton.FlatAppearance.BorderSize = 0;
            Reservar_Boton.FlatStyle = FlatStyle.Flat;
            Reservar_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Reservar_Boton.ForeColor = Color.Black;
            Reservar_Boton.Location = new Point(25, 180);
            Reservar_Boton.Name = "Reservar_Boton";
            Reservar_Boton.Size = new Size(200, 40);
            Reservar_Boton.TabIndex = 8;
            Reservar_Boton.Text = "Reservar Turno";
            Reservar_Boton.UseVisualStyleBackColor = false;
            Reservar_Boton.Click += Reservar_Boton_Click;
            // 
            // ReservasFijas_Boton
            // 
            ReservasFijas_Boton.BackColor = Color.FromArgb(59, 130, 246);
            ReservasFijas_Boton.Cursor = Cursors.Hand;
            ReservasFijas_Boton.FlatAppearance.BorderSize = 0;
            ReservasFijas_Boton.FlatStyle = FlatStyle.Flat;
            ReservasFijas_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            ReservasFijas_Boton.ForeColor = Color.White;
            ReservasFijas_Boton.Location = new Point(240, 180);
            ReservasFijas_Boton.Name = "ReservasFijas_Boton";
            ReservasFijas_Boton.Size = new Size(210, 40);
            ReservasFijas_Boton.TabIndex = 9;
            ReservasFijas_Boton.Text = "Reservas Fijas";
            ReservasFijas_Boton.UseVisualStyleBackColor = false;
            ReservasFijas_Boton.Click += ReservasFijas_Boton_Click;
            // 
            // Limpiar_Boton
            // 
            Limpiar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Limpiar_Boton.Cursor = Cursors.Hand;
            Limpiar_Boton.FlatAppearance.BorderSize = 0;
            Limpiar_Boton.FlatStyle = FlatStyle.Flat;
            Limpiar_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Limpiar_Boton.ForeColor = Color.White;
            Limpiar_Boton.Location = new Point(465, 180);
            Limpiar_Boton.Name = "Limpiar_Boton";
            Limpiar_Boton.Size = new Size(160, 40);
            Limpiar_Boton.TabIndex = 10;
            Limpiar_Boton.Text = "Limpiar";
            Limpiar_Boton.UseVisualStyleBackColor = false;
            Limpiar_Boton.Click += Limpiar_Boton_Click;
            // 
            // lblOcupados
            // 
            lblOcupados.AutoSize = true;
            lblOcupados.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOcupados.ForeColor = Color.FromArgb(163, 230, 53);
            lblOcupados.Location = new Point(25, 238);
            lblOcupados.Name = "lblOcupados";
            lblOcupados.Size = new Size(235, 19);
            lblOcupados.TabIndex = 11;
            lblOcupados.Text = "Horarios ocupados / bloqueados:";
            // 
            // gridOcupados
            // 
            gridOcupados.AllowUserToAddRows = false;
            gridOcupados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridOcupados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridOcupados.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridOcupados.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            gridOcupados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridOcupados.ColumnHeadersHeight = 32;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            gridOcupados.DefaultCellStyle = dataGridViewCellStyle2;
            gridOcupados.GridColor = Color.FromArgb(51, 65, 85);
            gridOcupados.Location = new Point(25, 265);
            gridOcupados.Name = "gridOcupados";
            gridOcupados.ReadOnly = true;
            gridOcupados.RowHeadersVisible = false;
            gridOcupados.RowHeadersWidth = 51;
            gridOcupados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridOcupados.Size = new Size(1230, 380);
            gridOcupados.TabIndex = 10;
            // 
            // CancelarReserva_Boton
            // 
            CancelarReserva_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CancelarReserva_Boton.BackColor = Color.FromArgb(239, 68, 68);
            CancelarReserva_Boton.Cursor = Cursors.Hand;
            CancelarReserva_Boton.FlatAppearance.BorderSize = 0;
            CancelarReserva_Boton.FlatStyle = FlatStyle.Flat;
            CancelarReserva_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            CancelarReserva_Boton.ForeColor = Color.White;
            CancelarReserva_Boton.Location = new Point(25, 660);
            CancelarReserva_Boton.Name = "CancelarReserva_Boton";
            CancelarReserva_Boton.Size = new Size(210, 40);
            CancelarReserva_Boton.TabIndex = 12;
            CancelarReserva_Boton.Text = "Cancelar reserva";
            CancelarReserva_Boton.UseVisualStyleBackColor = false;
            CancelarReserva_Boton.Click += CancelarReserva_Boton_Click;
            // 
            // Realquilar_Boton
            // 
            Realquilar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Realquilar_Boton.BackColor = Color.FromArgb(245, 158, 11);
            Realquilar_Boton.Cursor = Cursors.Hand;
            Realquilar_Boton.FlatAppearance.BorderSize = 0;
            Realquilar_Boton.FlatStyle = FlatStyle.Flat;
            Realquilar_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Realquilar_Boton.ForeColor = Color.Black;
            Realquilar_Boton.Location = new Point(250, 660);
            Realquilar_Boton.Name = "Realquilar_Boton";
            Realquilar_Boton.Size = new Size(210, 40);
            Realquilar_Boton.TabIndex = 13;
            Realquilar_Boton.Text = "Realquilar hora";
            Realquilar_Boton.UseVisualStyleBackColor = false;
            Realquilar_Boton.Click += Realquilar_Boton_Click;
            // 
            // FormGestionHorarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
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
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormGestionHorarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Reservas - Cantina Padel Club";
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
