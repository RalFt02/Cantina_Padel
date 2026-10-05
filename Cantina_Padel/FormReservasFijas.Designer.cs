namespace Cantina_Padel
{
    partial class FormReservasFijas
    {
        private System.ComponentModel.IContainer components=null;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private void InitializeComponent()
        {
            lblTitulo=new Label(); lblCliente=new Label(); cmbCliente=new ComboBox(); lblCancha=new Label(); cmbCancha=new ComboBox(); lblDia=new Label(); cmbDia=new ComboBox(); lblPeriodicidad=new Label(); cmbPeriodicidad=new ComboBox(); lblDesde=new Label(); dtpDesde=new DateTimePicker(); lblHasta=new Label(); dtpHasta=new DateTimePicker(); lblHoraInicio=new Label(); txtHoraInicio=new TextBox(); lblHoraFin=new Label(); txtHoraFin=new TextBox(); lblInfo=new Label(); CrearFija_Boton=new Button(); Cancelar_Boton=new Button(); Realquilar_Boton=new Button(); Volver_Boton=new Button(); gridFijas=new DataGridView(); ((System.ComponentModel.ISupportInitialize)gridFijas).BeginInit(); SuspendLayout();

            lblTitulo.AutoSize=true;lblTitulo.Font=new Font("Segoe UI",18F,FontStyle.Bold);lblTitulo.ForeColor=Color.FromArgb(163,230,53);lblTitulo.Location=new Point(20,15);lblTitulo.Text="RESERVAS FIJAS";
            lblCliente.AutoSize=true;lblCliente.ForeColor=Color.White;lblCliente.Location=new Point(20,65);lblCliente.Text="Cliente:";cmbCliente.BackColor=Color.FromArgb(51,65,85);cmbCliente.ForeColor=Color.White;cmbCliente.DropDownStyle=ComboBoxStyle.DropDownList;cmbCliente.Location=new Point(20,88);cmbCliente.Size=new Size(260,28);
            lblCancha.AutoSize=true;lblCancha.ForeColor=Color.White;lblCancha.Location=new Point(300,65);lblCancha.Text="Cancha:";cmbCancha.BackColor=Color.FromArgb(51,65,85);cmbCancha.ForeColor=Color.White;cmbCancha.DropDownStyle=ComboBoxStyle.DropDownList;cmbCancha.Location=new Point(300,88);cmbCancha.Size=new Size(180,28);
            lblDia.AutoSize=true;lblDia.ForeColor=Color.White;lblDia.Location=new Point(500,65);lblDia.Text="Día:";cmbDia.BackColor=Color.FromArgb(51,65,85);cmbDia.ForeColor=Color.White;cmbDia.DropDownStyle=ComboBoxStyle.DropDownList;cmbDia.Items.AddRange(new object[]{"Lunes","Martes","Miércoles","Jueves","Viernes","Sábado","Domingo"});cmbDia.Location=new Point(500,88);cmbDia.Size=new Size(140,28);cmbDia.SelectedIndex=0;cmbDia.SelectedIndexChanged+=cmbDia_SelectedIndexChanged;
            lblPeriodicidad.AutoSize=true;lblPeriodicidad.ForeColor=Color.White;lblPeriodicidad.Location=new Point(660,65);lblPeriodicidad.Text="Periodicidad:";cmbPeriodicidad.BackColor=Color.FromArgb(51,65,85);cmbPeriodicidad.ForeColor=Color.White;cmbPeriodicidad.DropDownStyle=ComboBoxStyle.DropDownList;cmbPeriodicidad.Items.AddRange(new object[]{"Mensual","Anual"});cmbPeriodicidad.Location=new Point(660,88);cmbPeriodicidad.Size=new Size(130,28);cmbPeriodicidad.SelectedIndex=1;cmbPeriodicidad.SelectedIndexChanged+=cmbPeriodicidad_SelectedIndexChanged;

            lblDesde.AutoSize=true;lblDesde.ForeColor=Color.White;lblDesde.Location=new Point(20,130);lblDesde.Text="Desde:";dtpDesde.Format=DateTimePickerFormat.Short;dtpDesde.Location=new Point(20,153);dtpDesde.Size=new Size(140,28);
            lblHasta.AutoSize=true;lblHasta.ForeColor=Color.White;lblHasta.Location=new Point(180,130);lblHasta.Text="Hasta:";dtpHasta.Format=DateTimePickerFormat.Short;dtpHasta.Location=new Point(180,153);dtpHasta.Size=new Size(140,28);
            lblHoraInicio.AutoSize=true;lblHoraInicio.ForeColor=Color.White;lblHoraInicio.Location=new Point(340,130);lblHoraInicio.Text="Hora inicio:";txtHoraInicio.BackColor=Color.FromArgb(51,65,85);txtHoraInicio.ForeColor=Color.White;txtHoraInicio.MaxLength=5;txtHoraInicio.PlaceholderText="17:30";txtHoraInicio.Location=new Point(340,153);txtHoraInicio.Size=new Size(120,27);txtHoraInicio.KeyPress+=SoloHora_KeyPress;
            lblHoraFin.AutoSize=true;lblHoraFin.ForeColor=Color.White;lblHoraFin.Location=new Point(475,130);lblHoraFin.Text="Hora fin:";txtHoraFin.BackColor=Color.FromArgb(51,65,85);txtHoraFin.ForeColor=Color.White;txtHoraFin.MaxLength=5;txtHoraFin.PlaceholderText="18:30";txtHoraFin.Location=new Point(475,153);txtHoraFin.Size=new Size(120,27);txtHoraFin.KeyPress+=SoloHora_KeyPress;
            lblInfo.AutoSize=true;lblInfo.ForeColor=Color.LightGray;lblInfo.Location=new Point(610,130);lblInfo.Text="08:00 a 03:00. Máximo 1 año.";

            CrearFija_Boton.BackColor=Color.FromArgb(163,230,53);CrearFija_Boton.FlatStyle=FlatStyle.Flat;CrearFija_Boton.ForeColor=Color.Black;CrearFija_Boton.Location=new Point(610,153);CrearFija_Boton.Size=new Size(130,38);CrearFija_Boton.Text="Crear fija";CrearFija_Boton.Click+=CrearFija_Boton_Click;
            Cancelar_Boton.BackColor=Color.FromArgb(239,68,68);Cancelar_Boton.FlatStyle=FlatStyle.Flat;Cancelar_Boton.ForeColor=Color.White;Cancelar_Boton.Location=new Point(750,153);Cancelar_Boton.Size=new Size(130,38);Cancelar_Boton.Text="Cancelar";Cancelar_Boton.Click+=Cancelar_Boton_Click;

            gridFijas.BackgroundColor=Color.FromArgb(30,41,59);gridFijas.BorderStyle=BorderStyle.None;gridFijas.ColumnHeadersDefaultCellStyle=new DataGridViewCellStyle{BackColor=Color.FromArgb(51,65,85),ForeColor=Color.FromArgb(163,230,53),Font=new Font("Segoe UI",9F,FontStyle.Bold)};gridFijas.DefaultCellStyle=new DataGridViewCellStyle{BackColor=Color.FromArgb(30,41,59),ForeColor=Color.White,SelectionBackColor=Color.FromArgb(163,230,53),SelectionForeColor=Color.Black};gridFijas.Location=new Point(20,225);gridFijas.ReadOnly=true;gridFijas.RowHeadersVisible=false;gridFijas.SelectionMode=DataGridViewSelectionMode.FullRowSelect;gridFijas.Size=new Size(860,300);
            Realquilar_Boton.BackColor=Color.FromArgb(245,158,11);Realquilar_Boton.FlatStyle=FlatStyle.Flat;Realquilar_Boton.ForeColor=Color.Black;Realquilar_Boton.Location=new Point(20,540);Realquilar_Boton.Size=new Size(200,35);Realquilar_Boton.Text="Realquilar seleccionada";Realquilar_Boton.Click+=Realquilar_Boton_Click;
            Volver_Boton.BackColor=Color.FromArgb(71,85,105);Volver_Boton.FlatStyle=FlatStyle.Flat;Volver_Boton.ForeColor=Color.White;Volver_Boton.Location=new Point(760,540);Volver_Boton.Size=new Size(120,35);Volver_Boton.Text="← Volver";Volver_Boton.Click+=Volver_Boton_Click;

            BackColor=Color.FromArgb(15,23,42);ClientSize=new Size(900,590);Controls.AddRange(new Control[]{lblTitulo,lblCliente,cmbCliente,lblCancha,cmbCancha,lblDia,cmbDia,lblPeriodicidad,cmbPeriodicidad,lblDesde,dtpDesde,lblHasta,dtpHasta,lblHoraInicio,txtHoraInicio,lblHoraFin,txtHoraFin,lblInfo,CrearFija_Boton,Cancelar_Boton,gridFijas,Realquilar_Boton,Volver_Boton});Name="FormReservasFijas";StartPosition=FormStartPosition.CenterParent;Text="Reservas Fijas";((System.ComponentModel.ISupportInitialize)gridFijas).EndInit();ResumeLayout(false);PerformLayout();
        }
        private Label lblTitulo,lblCliente,lblCancha,lblDia,lblPeriodicidad,lblDesde,lblHasta,lblHoraInicio,lblHoraFin,lblInfo;
        private ComboBox cmbCliente,cmbCancha,cmbDia,cmbPeriodicidad;
        private DateTimePicker dtpDesde,dtpHasta;
        private TextBox txtHoraInicio,txtHoraFin;
        private Button CrearFija_Boton,Cancelar_Boton,Realquilar_Boton,Volver_Boton;
        private DataGridView gridFijas;
    }
}
