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
            lblTitulo = new Label();

            lblCliente = new Label(); cmbCliente = new ComboBox();
            lblCancha = new Label(); cmbCancha = new ComboBox();
            lblFecha = new Label(); dtpFecha = new DateTimePicker();
            lblHoraInicio = new Label(); txtHoraInicio = new TextBox();
            lblHoraFin = new Label(); txtHoraFin = new TextBox();
            lblEstado = new Label(); lblInfo = new Label();
            Reservar_Boton = new Button(); ReservasFijas_Boton = new Button();
            CancelarReserva_Boton = new Button(); Realquilar_Boton = new Button();
            Limpiar_Boton = new Button(); Volver_Boton = new Button();
            lblOcupados = new Label(); gridOcupados = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)gridOcupados).BeginInit();
            SuspendLayout();

            lblTitulo.AutoSize = true; lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.FromArgb(163,230,53); lblTitulo.Location = new Point(25,18); lblTitulo.Text = "GESTIÓN DE RESERVAS";

            lblCliente.AutoSize = true; lblCliente.ForeColor = Color.White; lblCliente.Location = new Point(25,70); lblCliente.Text = "Cliente:";
            cmbCliente.BackColor = Color.FromArgb(51,65,85); cmbCliente.ForeColor = Color.White; cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList; cmbCliente.Location = new Point(25,95); cmbCliente.Size = new Size(300,28); cmbCliente.TabIndex = 1;

            lblCancha.AutoSize = true; lblCancha.ForeColor = Color.White; lblCancha.Location = new Point(345,70); lblCancha.Text = "Cancha:";
            cmbCancha.BackColor = Color.FromArgb(51,65,85); cmbCancha.ForeColor = Color.White; cmbCancha.DropDownStyle = ComboBoxStyle.DropDownList; cmbCancha.Location = new Point(345,95); cmbCancha.Size = new Size(190,28); cmbCancha.TabIndex = 2;

            lblFecha.AutoSize = true; lblFecha.ForeColor = Color.White; lblFecha.Location = new Point(555,70); lblFecha.Text = "Fecha:";
            dtpFecha.Format = DateTimePickerFormat.Short; dtpFecha.Location = new Point(555,95); dtpFecha.Size = new Size(145,28); dtpFecha.TabIndex = 3;

            lblHoraInicio.AutoSize = true; lblHoraInicio.ForeColor = Color.White; lblHoraInicio.Location = new Point(720,70); lblHoraInicio.Text = "Hora inicio:";
            txtHoraInicio.BackColor = Color.FromArgb(51,65,85); txtHoraInicio.ForeColor = Color.White; txtHoraInicio.Location = new Point(720,95); txtHoraInicio.MaxLength = 5; txtHoraInicio.PlaceholderText = "17:30"; txtHoraInicio.Size = new Size(100,27); txtHoraInicio.TabIndex = 4;

            lblHoraFin.AutoSize = true; lblHoraFin.ForeColor = Color.White; lblHoraFin.Location = new Point(830,70); lblHoraFin.Text = "Hora fin:";
            txtHoraFin.BackColor = Color.FromArgb(51,65,85); txtHoraFin.ForeColor = Color.White; txtHoraFin.Location = new Point(830,95); txtHoraFin.MaxLength = 5; txtHoraFin.PlaceholderText = "18:30"; txtHoraFin.Size = new Size(100,27); txtHoraFin.TabIndex = 5;

            lblInfo.AutoSize = true; lblInfo.ForeColor = Color.LightGray; lblInfo.Location = new Point(25,140); lblInfo.Text = "Rango permitido: 08:00 a 03:00 del día siguiente. Las horas se ingresan manualmente en HH:mm.";
            lblEstado.AutoSize = true; lblEstado.ForeColor = Color.FromArgb(163,230,53); lblEstado.Location = new Point(25,165); lblEstado.Text = "";

            Reservar_Boton.BackColor = Color.FromArgb(163,230,53); Reservar_Boton.FlatStyle = FlatStyle.Flat; Reservar_Boton.ForeColor = Color.Black; Reservar_Boton.Location = new Point(25,195); Reservar_Boton.Size = new Size(180,38); Reservar_Boton.Text = "Reservar"; Reservar_Boton.Click += Reservar_Boton_Click;
            ReservasFijas_Boton.BackColor = Color.FromArgb(59,130,246); ReservasFijas_Boton.FlatStyle = FlatStyle.Flat; ReservasFijas_Boton.ForeColor = Color.White; ReservasFijas_Boton.Location = new Point(220,195); ReservasFijas_Boton.Size = new Size(200,38); ReservasFijas_Boton.Text = "Reservas Fijas"; ReservasFijas_Boton.Click += ReservasFijas_Boton_Click;
            CancelarReserva_Boton.BackColor = Color.FromArgb(239,68,68); CancelarReserva_Boton.FlatStyle = FlatStyle.Flat; CancelarReserva_Boton.ForeColor = Color.White; CancelarReserva_Boton.Location = new Point(25,535); CancelarReserva_Boton.Size = new Size(180,35); CancelarReserva_Boton.Text = "Cancelar reserva"; CancelarReserva_Boton.Click += CancelarReserva_Boton_Click;
            Realquilar_Boton.BackColor = Color.FromArgb(245,158,11); Realquilar_Boton.FlatStyle = FlatStyle.Flat; Realquilar_Boton.ForeColor = Color.Black; Realquilar_Boton.Location = new Point(220,535); Realquilar_Boton.Size = new Size(180,35); Realquilar_Boton.Text = "Realquilar hora"; Realquilar_Boton.Click += Realquilar_Boton_Click;
            Limpiar_Boton.BackColor = Color.FromArgb(71,85,105); Limpiar_Boton.FlatStyle = FlatStyle.Flat; Limpiar_Boton.ForeColor = Color.White; Limpiar_Boton.Location = new Point(435,195); Limpiar_Boton.Size = new Size(150,38); Limpiar_Boton.Text = "Limpiar"; Limpiar_Boton.Click += Limpiar_Boton_Click;

            lblOcupados.AutoSize = true; lblOcupados.Font = new Font("Segoe UI", 10F, FontStyle.Bold); lblOcupados.ForeColor = Color.LightGray; lblOcupados.Location = new Point(25,255); lblOcupados.Text = "Horarios ocupados / bloqueados";
            gridOcupados.BackgroundColor = Color.FromArgb(30,41,59); gridOcupados.BorderStyle = BorderStyle.None; gridOcupados.Location = new Point(25,280); gridOcupados.Name = "gridOcupados"; gridOcupados.ReadOnly = true; gridOcupados.RowHeadersVisible = false; gridOcupados.SelectionMode = DataGridViewSelectionMode.FullRowSelect; gridOcupados.Size = new Size(905,235); gridOcupados.TabIndex = 10;
            gridOcupados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridOcupados.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(51,65,85), ForeColor = Color.FromArgb(163,230,53), Font = new Font("Segoe UI",9F,FontStyle.Bold) };
            gridOcupados.DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(71,71,71), ForeColor = Color.LightGray, SelectionBackColor = Color.FromArgb(100,100,100), SelectionForeColor = Color.White };

            Volver_Boton.BackColor = Color.FromArgb(71,85,105); Volver_Boton.FlatStyle = FlatStyle.Flat; Volver_Boton.ForeColor = Color.White; Volver_Boton.Location = new Point(790,535); Volver_Boton.Size = new Size(140,35); Volver_Boton.Text = "← Volver"; Volver_Boton.Click += Volver_Boton_Click;

            BackColor = Color.FromArgb(15,23,42); ClientSize = new Size(960,590); Controls.AddRange(new Control[] { lblTitulo,lblCliente,cmbCliente,lblCancha,cmbCancha,lblFecha,dtpFecha,lblHoraInicio,txtHoraInicio,lblHoraFin,txtHoraFin,lblInfo,lblEstado,Reservar_Boton,ReservasFijas_Boton,Limpiar_Boton,lblOcupados,gridOcupados,CancelarReserva_Boton,Realquilar_Boton,Volver_Boton }); Name = "FormGestionHorarios"; StartPosition = FormStartPosition.CenterScreen; Text = "Gestión de Reservas";
            ((System.ComponentModel.ISupportInitialize)gridOcupados).EndInit(); ResumeLayout(false); PerformLayout();
        }

        #endregion

        private Label lblTitulo,lblCliente,lblCancha,lblFecha,lblHoraInicio,lblHoraFin,lblEstado,lblInfo,lblOcupados;
        private ComboBox cmbCliente,cmbCancha;
        private DateTimePicker dtpFecha;
        private TextBox txtHoraInicio,txtHoraFin;
        private Button Reservar_Boton,ReservasFijas_Boton,CancelarReserva_Boton,Realquilar_Boton,Limpiar_Boton,Volver_Boton;
        private DataGridView gridOcupados;
    }
}
