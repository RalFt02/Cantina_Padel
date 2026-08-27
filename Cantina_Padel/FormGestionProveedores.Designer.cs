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
            gridProveedores    = new DataGridView();
            lblTitulo          = new Label();
            lblBuscar          = new Label();
            txtBuscar          = new TextBox();
            grpDatos           = new GroupBox();
            lblId              = new Label();
            txtId              = new TextBox();
            lblNombre          = new Label();
            txtNombre          = new TextBox();
            lblApellido        = new Label();
            txtApellido        = new TextBox();
            lblTelefono        = new Label();
            txtTelefono        = new TextBox();
            lblDni             = new Label();
            txtDni             = new TextBox();
            lblEmail           = new Label();
            txtEmail           = new TextBox();
            lblDireccion       = new Label();
            txtDireccion       = new TextBox();
            lblCuit            = new Label();
            txtCuit            = new TextBox();
            lblRazonSocial     = new Label();
            txtRazonSocial     = new TextBox();
            lblObservaciones   = new Label();
            txtObservaciones   = new TextBox();
            chkActivo          = new CheckBox();
            Nuevo_Boton        = new Button();
            Guardar_Boton      = new Button();
            Eliminar_Boton     = new Button();
            Cancelar_Boton     = new Button();
            Volver_Boton       = new Button();

            ((System.ComponentModel.ISupportInitialize)gridProveedores).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();

            // ── Formulario ──────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(15, 23, 42);
            ClientSize          = new Size(1100, 650);
            Name                = "FormGestionProveedores";
            Text                = "Gestión de Proveedores";
            StartPosition       = FormStartPosition.CenterScreen;

            // ── Título ──────────────────────────────────────────
            lblTitulo.Text      = "GESTIÓN DE PROVEEDORES";
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Font      = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location  = new Point(20, 15);
            lblTitulo.Size      = new Size(400, 35);
            lblTitulo.AutoSize  = true;

            // ── Buscar ──────────────────────────────────────────
            lblBuscar.Text      = "Buscar:";
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location  = new Point(20, 60);
            lblBuscar.AutoSize  = true;

            txtBuscar.Location  = new Point(80, 57);
            txtBuscar.Size      = new Size(250, 27);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // ── Grilla ──────────────────────────────────────────
            gridProveedores.Location                  = new Point(20, 95);
            gridProveedores.Size                      = new Size(580, 520);
            gridProveedores.BackgroundColor           = Color.FromArgb(30, 41, 59);
            gridProveedores.ForeColor                 = Color.White;
            gridProveedores.GridColor                 = Color.FromArgb(51, 65, 85);
            gridProveedores.BorderStyle               = BorderStyle.None;
            gridProveedores.RowHeadersVisible         = false;
            gridProveedores.AllowUserToAddRows        = false;
            gridProveedores.ReadOnly                  = true;
            gridProveedores.SelectionMode             = DataGridViewSelectionMode.FullRowSelect;
            gridProveedores.AutoSizeColumnsMode       = DataGridViewAutoSizeColumnsMode.None;
            gridProveedores.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(51, 65, 85);
            gridProveedores.ColumnHeadersDefaultCellStyle.ForeColor  = Color.FromArgb(163, 230, 53);
            gridProveedores.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridProveedores.DefaultCellStyle.BackColor               = Color.FromArgb(30, 41, 59);
            gridProveedores.DefaultCellStyle.ForeColor               = Color.White;
            gridProveedores.DefaultCellStyle.SelectionBackColor      = Color.FromArgb(163, 230, 53);
            gridProveedores.DefaultCellStyle.SelectionForeColor      = Color.Black;
            gridProveedores.CellClick += gridProveedores_CellClick;

            // ── Panel Datos ─────────────────────────────────────
            grpDatos.Text      = "Datos del Proveedor";
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.Location  = new Point(620, 95);
            grpDatos.Size      = new Size(460, 520);
            grpDatos.BackColor = Color.FromArgb(30, 41, 59);

            // ID (oculto visualmente)
            lblId.Text      = "ID:";
            lblId.ForeColor = Color.Gray;
            lblId.Location  = new Point(10, 28);
            lblId.AutoSize  = true;
            txtId.Location  = new Point(60, 25);
            txtId.Size      = new Size(60, 27);
            txtId.ReadOnly  = true;
            txtId.BackColor = Color.FromArgb(15, 23, 42);
            txtId.ForeColor = Color.Gray;

            // ── Columna izquierda: x=10 / Columna derecha: x=235 (ancho 205 c/u) ──

            // Nombre / Apellido
            lblNombre.Text      = "Nombre:";
            lblNombre.ForeColor = Color.White;
            lblNombre.Location  = new Point(10, 68);
            lblNombre.AutoSize  = true;
            txtNombre.Location  = new Point(10, 90);
            txtNombre.Size      = new Size(205, 27);
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;

            lblApellido.Text      = "Apellido:";
            lblApellido.ForeColor = Color.White;
            lblApellido.Location  = new Point(235, 68);
            lblApellido.AutoSize  = true;
            txtApellido.Location  = new Point(235, 90);
            txtApellido.Size      = new Size(205, 27);
            txtApellido.BackColor = Color.FromArgb(51, 65, 85);
            txtApellido.ForeColor = Color.White;

            // Teléfono / DNI
            lblTelefono.Text      = "Teléfono:";
            lblTelefono.ForeColor = Color.White;
            lblTelefono.Location  = new Point(10, 128);
            lblTelefono.AutoSize  = true;
            txtTelefono.Location  = new Point(10, 150);
            txtTelefono.Size      = new Size(205, 27);
            txtTelefono.BackColor = Color.FromArgb(51, 65, 85);
            txtTelefono.ForeColor = Color.White;

            lblDni.Text      = "DNI:";
            lblDni.ForeColor = Color.White;
            lblDni.Location  = new Point(235, 128);
            lblDni.AutoSize  = true;
            txtDni.Location  = new Point(235, 150);
            txtDni.Size      = new Size(205, 27);
            txtDni.BackColor = Color.FromArgb(51, 65, 85);
            txtDni.ForeColor = Color.White;

            // Email / Dirección
            lblEmail.Text      = "Email:";
            lblEmail.ForeColor = Color.White;
            lblEmail.Location  = new Point(10, 188);
            lblEmail.AutoSize  = true;
            txtEmail.Location  = new Point(10, 210);
            txtEmail.Size      = new Size(205, 27);
            txtEmail.BackColor = Color.FromArgb(51, 65, 85);
            txtEmail.ForeColor = Color.White;

            lblDireccion.Text      = "Dirección:";
            lblDireccion.ForeColor = Color.White;
            lblDireccion.Location  = new Point(235, 188);
            lblDireccion.AutoSize  = true;
            txtDireccion.Location  = new Point(235, 210);
            txtDireccion.Size      = new Size(205, 27);
            txtDireccion.BackColor = Color.FromArgb(51, 65, 85);
            txtDireccion.ForeColor = Color.White;

            // CUIT / Razón Social
            lblCuit.Text      = "CUIT:";
            lblCuit.ForeColor = Color.White;
            lblCuit.Location  = new Point(10, 248);
            lblCuit.AutoSize  = true;
            txtCuit.Location  = new Point(10, 270);
            txtCuit.Size      = new Size(205, 27);
            txtCuit.BackColor = Color.FromArgb(51, 65, 85);
            txtCuit.ForeColor = Color.White;

            lblRazonSocial.Text      = "Razón Social:";
            lblRazonSocial.ForeColor = Color.White;
            lblRazonSocial.Location  = new Point(235, 248);
            lblRazonSocial.AutoSize  = true;
            txtRazonSocial.Location  = new Point(235, 270);
            txtRazonSocial.Size      = new Size(205, 27);
            txtRazonSocial.BackColor = Color.FromArgb(51, 65, 85);
            txtRazonSocial.ForeColor = Color.White;

            // Observaciones (propias del proveedor)
            lblObservaciones.Text      = "Observaciones:";
            lblObservaciones.ForeColor = Color.White;
            lblObservaciones.Location  = new Point(10, 308);
            lblObservaciones.AutoSize  = true;
            txtObservaciones.Location  = new Point(10, 330);
            txtObservaciones.Size      = new Size(430, 60);
            txtObservaciones.Multiline = true;
            txtObservaciones.BackColor = Color.FromArgb(51, 65, 85);
            txtObservaciones.ForeColor = Color.White;

            // Activo
            chkActivo.Text      = "Proveedor activo";
            chkActivo.ForeColor = Color.White;
            chkActivo.Location  = new Point(10, 400);
            chkActivo.Checked   = true;
            chkActivo.AutoSize  = true;

            // ── Botones del panel ───────────────────────────────
            Guardar_Boton.Text             = "Crear Proveedor";
            Guardar_Boton.Location         = new Point(10, 438);
            Guardar_Boton.Size             = new Size(215, 38);
            Guardar_Boton.BackColor        = Color.FromArgb(163, 230, 53);
            Guardar_Boton.ForeColor        = Color.Black;
            Guardar_Boton.Font             = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.FlatStyle        = FlatStyle.Flat;
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click           += Guardar_Boton_Click;

            Cancelar_Boton.Text            = "Limpiar";
            Cancelar_Boton.Location        = new Point(235, 438);
            Cancelar_Boton.Size            = new Size(205, 38);
            Cancelar_Boton.BackColor       = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.ForeColor       = Color.White;
            Cancelar_Boton.FlatStyle       = FlatStyle.Flat;
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click          += Cancelar_Boton_Click;

            Eliminar_Boton.Text            = "Desactivar Proveedor";
            Eliminar_Boton.Location        = new Point(10, 486);
            Eliminar_Boton.Size            = new Size(430, 35);
            Eliminar_Boton.BackColor       = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.ForeColor       = Color.White;
            Eliminar_Boton.FlatStyle       = FlatStyle.Flat;
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click          += Eliminar_Boton_Click;

            grpDatos.Controls.AddRange(new Control[]
            {
                lblId, txtId,
                lblNombre, txtNombre,
                lblApellido, txtApellido,
                lblTelefono, txtTelefono,
                lblDni, txtDni,
                lblEmail, txtEmail,
                lblDireccion, txtDireccion,
                lblCuit, txtCuit,
                lblRazonSocial, txtRazonSocial,
                lblObservaciones, txtObservaciones,
                chkActivo,
                Guardar_Boton, Cancelar_Boton,
                Eliminar_Boton
            });

            // ── Botones externos ────────────────────────────────
            Nuevo_Boton.Text           = "+ Nuevo Proveedor";
            Nuevo_Boton.Location       = new Point(350, 55);
            Nuevo_Boton.Size           = new Size(150, 32);
            Nuevo_Boton.BackColor      = Color.FromArgb(163, 230, 53);
            Nuevo_Boton.ForeColor      = Color.Black;
            Nuevo_Boton.Font           = new Font("Segoe UI", 9F, FontStyle.Bold);
            Nuevo_Boton.FlatStyle      = FlatStyle.Flat;
            Nuevo_Boton.FlatAppearance.BorderSize = 0;
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click         += Nuevo_Boton_Click;

            Volver_Boton.Text          = "← Volver";
            Volver_Boton.Location      = new Point(970, 55);
            Volver_Boton.Size          = new Size(110, 32);
            Volver_Boton.BackColor     = Color.FromArgb(71, 85, 105);
            Volver_Boton.ForeColor     = Color.White;
            Volver_Boton.FlatStyle     = FlatStyle.Flat;
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click        += Volver_Boton_Click;

            Controls.AddRange(new Control[]
            {
                lblTitulo,
                lblBuscar, txtBuscar,
                gridProveedores,
                grpDatos,
                Nuevo_Boton,
                Volver_Boton
            });

            ((System.ComponentModel.ISupportInitialize)gridProveedores).EndInit();
            grpDatos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView     gridProveedores;
        private Label            lblTitulo;
        private Label            lblBuscar;
        private TextBox          txtBuscar;
        private GroupBox         grpDatos;
        private Label            lblId;
        private TextBox          txtId;
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
