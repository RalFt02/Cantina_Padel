namespace Cantina_Padel
{
    partial class FormReservasFijas
    {
        private System.ComponentModel.IContainer components=null;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblTitulo = new Label();
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            lblCancha = new Label();
            cmbCancha = new ComboBox();
            lblDia = new Label();
            cmbDia = new ComboBox();
            lblPeriodicidad = new Label();
            cmbPeriodicidad = new ComboBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            lblHorario = new Label();
            cmbHorario = new ComboBox();
            lblInfo = new Label();
            CrearFija_Boton = new Button();
            Cancelar_Boton = new Button();
            Realquilar_Boton = new Button();
            Volver_Boton = new Button();
            gridFijas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)gridFijas).BeginInit();
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
            lblTitulo.Size = new Size(210, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "RESERVAS FIJAS";
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
            Volver_Boton.TabIndex = 22;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCliente.ForeColor = Color.White;
            lblCliente.Location = new Point(25, 65);
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
            cmbCliente.Location = new Point(25, 86);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(360, 25);
            cmbCliente.TabIndex = 2;
            // 
            // lblCancha
            // 
            lblCancha.AutoSize = true;
            lblCancha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCancha.ForeColor = Color.White;
            lblCancha.Location = new Point(410, 65);
            lblCancha.Name = "lblCancha";
            lblCancha.Size = new Size(52, 15);
            lblCancha.TabIndex = 3;
            lblCancha.Text = "Cancha:";
            // 
            // cmbCancha
            // 
            cmbCancha.BackColor = Color.FromArgb(51, 65, 85);
            cmbCancha.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCancha.ForeColor = Color.White;
            cmbCancha.Location = new Point(410, 86);
            cmbCancha.Name = "cmbCancha";
            cmbCancha.Size = new Size(250, 25);
            cmbCancha.TabIndex = 4;
            // 
            // lblDia
            // 
            lblDia.AutoSize = true;
            lblDia.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDia.ForeColor = Color.White;
            lblDia.Location = new Point(685, 65);
            lblDia.Name = "lblDia";
            lblDia.Size = new Size(30, 15);
            lblDia.TabIndex = 5;
            lblDia.Text = "Día:";
            // 
            // cmbDia
            // 
            cmbDia.BackColor = Color.FromArgb(51, 65, 85);
            cmbDia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDia.ForeColor = Color.White;
            cmbDia.Items.AddRange(new object[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" });
            cmbDia.Location = new Point(685, 86);
            cmbDia.Name = "cmbDia";
            cmbDia.Size = new Size(220, 25);
            cmbDia.TabIndex = 6;
            cmbDia.SelectedIndexChanged += cmbDia_SelectedIndexChanged;
            // 
            // lblPeriodicidad
            // 
            lblPeriodicidad.AutoSize = true;
            lblPeriodicidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPeriodicidad.ForeColor = Color.White;
            lblPeriodicidad.Location = new Point(930, 65);
            lblPeriodicidad.Name = "lblPeriodicidad";
            lblPeriodicidad.Size = new Size(82, 15);
            lblPeriodicidad.TabIndex = 7;
            lblPeriodicidad.Text = "Periodicidad:";
            // 
            // cmbPeriodicidad
            // 
            cmbPeriodicidad.BackColor = Color.FromArgb(51, 65, 85);
            cmbPeriodicidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPeriodicidad.ForeColor = Color.White;
            cmbPeriodicidad.Items.AddRange(new object[] { "Mensual", "Anual" });
            cmbPeriodicidad.Location = new Point(930, 86);
            cmbPeriodicidad.Name = "cmbPeriodicidad";
            cmbPeriodicidad.Size = new Size(220, 25);
            cmbPeriodicidad.TabIndex = 8;
            cmbPeriodicidad.SelectedIndexChanged += cmbPeriodicidad_SelectedIndexChanged;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDesde.ForeColor = Color.White;
            lblDesde.Location = new Point(25, 130);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(45, 15);
            lblDesde.TabIndex = 9;
            lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(25, 152);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(175, 24);
            dtpDesde.TabIndex = 10;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHasta.ForeColor = Color.White;
            lblHasta.Location = new Point(220, 130);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(42, 15);
            lblHasta.TabIndex = 11;
            lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(220, 152);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(175, 24);
            dtpHasta.TabIndex = 12;
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHorario.ForeColor = Color.White;
            lblHorario.Location = new Point(415, 130);
            lblHorario.Name = "lblHorario";
            lblHorario.Size = new Size(55, 15);
            lblHorario.TabIndex = 13;
            lblHorario.Text = "Horario:";
            // 
            // cmbHorario
            // 
            cmbHorario.BackColor = Color.FromArgb(51, 65, 85);
            cmbHorario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHorario.ForeColor = Color.White;
            cmbHorario.Location = new Point(415, 152);
            cmbHorario.MaxDropDownItems = 10;
            cmbHorario.Name = "cmbHorario";
            cmbHorario.Size = new Size(280, 25);
            cmbHorario.TabIndex = 14;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.LightGray;
            lblInfo.Location = new Point(720, 130);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(165, 15);
            lblInfo.TabIndex = 17;
            lblInfo.Text = "08:00 a 03:00. Máximo 1 año.";
            // 
            // CrearFija_Boton
            // 
            CrearFija_Boton.BackColor = Color.FromArgb(163, 230, 53);
            CrearFija_Boton.Cursor = Cursors.Hand;
            CrearFija_Boton.FlatAppearance.BorderSize = 0;
            CrearFija_Boton.FlatStyle = FlatStyle.Flat;
            CrearFija_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            CrearFija_Boton.ForeColor = Color.Black;
            CrearFija_Boton.Location = new Point(720, 148);
            CrearFija_Boton.Name = "CrearFija_Boton";
            CrearFija_Boton.Size = new Size(190, 38);
            CrearFija_Boton.TabIndex = 18;
            CrearFija_Boton.Text = "Crear reserva fija";
            CrearFija_Boton.UseVisualStyleBackColor = false;
            CrearFija_Boton.Click += CrearFija_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Cancelar_Boton.Cursor = Cursors.Hand;
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(925, 148);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(190, 38);
            Cancelar_Boton.TabIndex = 19;
            Cancelar_Boton.Text = "Cancelar";
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click += Cancelar_Boton_Click;
            // 
            // gridFijas
            // 
            gridFijas.AllowUserToAddRows = false;
            gridFijas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridFijas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridFijas.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridFijas.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(163, 230, 53);
            gridFijas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridFijas.ColumnHeadersHeight = 32;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            gridFijas.DefaultCellStyle = dataGridViewCellStyle2;
            gridFijas.GridColor = Color.FromArgb(51, 65, 85);
            gridFijas.Location = new Point(25, 205);
            gridFijas.Name = "gridFijas";
            gridFijas.ReadOnly = true;
            gridFijas.RowHeadersVisible = false;
            gridFijas.RowHeadersWidth = 51;
            gridFijas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridFijas.Size = new Size(1230, 440);
            gridFijas.TabIndex = 20;
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
            Realquilar_Boton.Location = new Point(25, 660);
            Realquilar_Boton.Name = "Realquilar_Boton";
            Realquilar_Boton.Size = new Size(230, 40);
            Realquilar_Boton.TabIndex = 21;
            Realquilar_Boton.Text = "Realquilar seleccionada";
            Realquilar_Boton.UseVisualStyleBackColor = false;
            Realquilar_Boton.Click += Realquilar_Boton_Click;
            // 
            // FormReservasFijas
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
            Controls.Add(lblDia);
            Controls.Add(cmbDia);
            Controls.Add(lblPeriodicidad);
            Controls.Add(cmbPeriodicidad);
            Controls.Add(lblDesde);
            Controls.Add(dtpDesde);
            Controls.Add(lblHasta);
            Controls.Add(dtpHasta);
            Controls.Add(lblHorario);
            Controls.Add(cmbHorario);
            Controls.Add(lblInfo);
            Controls.Add(CrearFija_Boton);
            Controls.Add(Cancelar_Boton);
            Controls.Add(gridFijas);
            Controls.Add(Realquilar_Boton);
            Controls.Add(Volver_Boton);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormReservasFijas";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reservas Fijas - Cantina Padel Club";
            ((System.ComponentModel.ISupportInitialize)gridFijas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblTitulo,lblCliente,lblCancha,lblDia,lblPeriodicidad,lblDesde,lblHasta,lblHorario,lblInfo;
        private ComboBox cmbCliente,cmbCancha,cmbDia,cmbPeriodicidad,cmbHorario;
        private DateTimePicker dtpDesde,dtpHasta;
        private Button CrearFija_Boton,Cancelar_Boton,Realquilar_Boton,Volver_Boton;
        private DataGridView gridFijas;
    }
}
