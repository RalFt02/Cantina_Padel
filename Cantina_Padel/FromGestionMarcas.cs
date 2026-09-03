using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;


namespace Cantina_Padel
{
    public partial class FormGestionMarcas : Form
    {
        // Guarda el ID de la marca seleccionada en la grilla (null = alta nueva).
        // No usamos un TextBox visible para esto, así no aparece ni en tiempo de
        // ejecución ni en el lienzo de diseño de Visual Studio.
        private int? _idSeleccionado;

        public FormGestionMarcas()
        {
            InitializeComponent();
            CargarMarcas();
        }

        // ─────────────────────────────────────────────
        //  READ - Cargar grilla
        // ─────────────────────────────────────────────
        private void CargarMarcas()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = @"
                    SELECT id_marca, nombre, activo
                    FROM Marca
                    ORDER BY id_marca";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gridMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridMarcas.DataSource = dt;

                gridMarcas.Columns["id_marca"].HeaderText = "ID";
                gridMarcas.Columns["nombre"].HeaderText = "Nombre";
                gridMarcas.Columns["activo"].HeaderText = "Activo";

                gridMarcas.Columns["id_marca"].Width = 40;
                gridMarcas.Columns["nombre"].Width = 140;
                gridMarcas.Columns["activo"].Width = 55;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar marcas:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  BOTÓN NUEVO
        // ─────────────────────────────────────────────
        private void Nuevo_Boton_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // ─────────────────────────────────────────────
        //  CREATE / UPDATE - Guardar
        // ─────────────────────────────────────────────
        private void Guardar_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de la marca es obligatorio.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                bool esNuevo = _idSeleccionado == null;

                if (esNuevo)
                    CrearMarca(conn);
                else
                    EditarMarca(conn);

                MessageBox.Show("Operación realizada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarMarcas();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearMarca(MySqlConnection conn)
        {
            string check = "SELECT COUNT(*) FROM Marca WHERE nombre = @n";
            using (MySqlCommand cmdCheck = new MySqlCommand(check, conn))
            {
                cmdCheck.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
                long count = (long)cmdCheck.ExecuteScalar()!;
                if (count > 0)
                {
                    MessageBox.Show("Ya existe una marca con ese nombre.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string query = @"
                INSERT INTO Marca (nombre, activo)
                VALUES (@n, @a)";

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@a", chkActivo.Checked ? 1 : 0);
            cmd.ExecuteNonQuery();
        }

        private void EditarMarca(MySqlConnection conn)
        {
            string query = @"
                UPDATE Marca
                SET nombre = @n,
                    activo = @a
                WHERE id_marca = @id";

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@a", chkActivo.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
            cmd.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────
        //  DELETE - Baja lógica
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == null)
            {
                MessageBox.Show("Seleccioná una marca de la lista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Desactivar la marca '{txtNombre.Text}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string query = "UPDATE Marca SET activo = 0 WHERE id_marca = @id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idSeleccionado!.Value);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Marca desactivada.", "Listo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarMarcas();
                LimpiarFormulario();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  Seleccionar fila en grilla
        // ─────────────────────────────────────────────
        private void gridMarcas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = gridMarcas.Rows[e.RowIndex];

            _idSeleccionado = Convert.ToInt32(row.Cells["id_marca"].Value);
            txtNombre.Text = row.Cells["nombre"].Value?.ToString() ?? "";
            chkActivo.Checked = Convert.ToBoolean(row.Cells["activo"].Value);
            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  Buscar en tiempo real
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridMarcas.DataSource is DataTable dt)
                dt.DefaultView.RowFilter = $"nombre LIKE '%{txtBuscar.Text}%'";
        }

        // ─────────────────────────────────────────────
        //  Utilidades
        // ─────────────────────────────────────────────
        private void LimpiarFormulario()
        {
            _idSeleccionado = null;
            txtNombre.Text = "";
            chkActivo.Checked = true;
            Guardar_Boton.Text = "Crear Marca";
        }

        private void Cancelar_Boton_Click(object sender, EventArgs e) => LimpiarFormulario();

        private void Volver_Boton_Click(object sender, EventArgs e) => this.Close();

        // ─────────────────────────────────────────────
        //  DISEÑADOR DE COMPONENTES
        // ─────────────────────────────────────────────
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
            gridMarcas = new DataGridView();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            grpDatos = new GroupBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            chkActivo = new CheckBox();
            Guardar_Boton = new Button();
            Cancelar_Boton = new Button();
            Eliminar_Boton = new Button();
            Nuevo_Boton = new Button();
            Volver_Boton = new Button();
            ((System.ComponentModel.ISupportInitialize)gridMarcas).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();
            // 
            // gridMarcas
            // 
            gridMarcas.AllowUserToAddRows = false;
            gridMarcas.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridMarcas.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridMarcas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridMarcas.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(163, 230, 53);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridMarcas.DefaultCellStyle = dataGridViewCellStyle2;
            gridMarcas.GridColor = Color.FromArgb(51, 65, 85);
            gridMarcas.Location = new Point(20, 95);
            gridMarcas.Name = "gridMarcas";
            gridMarcas.ReadOnly = true;
            gridMarcas.RowHeadersVisible = false;
            gridMarcas.RowHeadersWidth = 51;
            gridMarcas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridMarcas.Size = new Size(580, 490);
            gridMarcas.TabIndex = 3;
            gridMarcas.CellClick += gridMarcas_CellClick;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(293, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE MARCAS";
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
            txtBuscar.BackColor = Color.FromArgb(51, 65, 85);
            txtBuscar.ForeColor = Color.White;
            txtBuscar.Location = new Point(80, 57);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(240, 27);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // grpDatos
            // 
            grpDatos.BackColor = Color.FromArgb(30, 41, 59);
            grpDatos.Controls.Add(lblNombre);
            grpDatos.Controls.Add(txtNombre);
            grpDatos.Controls.Add(chkActivo);
            grpDatos.Controls.Add(Guardar_Boton);
            grpDatos.Controls.Add(Cancelar_Boton);
            grpDatos.Controls.Add(Eliminar_Boton);
            grpDatos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Location = new Point(620, 95);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(380, 280);
            grpDatos.TabIndex = 4;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos de la Marca";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(10, 65);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(82, 20);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre: *";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(10, 87);
            txtNombre.MaxLength = 30;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(350, 27);
            txtNombre.TabIndex = 3;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(10, 125);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(119, 24);
            chkActivo.TabIndex = 4;
            chkActivo.Text = "Marca activa";
            // 
            // Guardar_Boton
            // 
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Location = new Point(10, 165);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(165, 38);
            Guardar_Boton.TabIndex = 5;
            Guardar_Boton.Text = "Crear Marca";
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(191, 220);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(175, 38);
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
            Eliminar_Boton.Location = new Point(191, 165);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(175, 38);
            Eliminar_Boton.TabIndex = 7;
            Eliminar_Boton.Text = "Desactivar";
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
            Nuevo_Boton.Location = new Point(340, 55);
            Nuevo_Boton.Name = "Nuevo_Boton";
            Nuevo_Boton.Size = new Size(150, 32);
            Nuevo_Boton.TabIndex = 5;
            Nuevo_Boton.Text = "+ Nueva Marca";
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click += Nuevo_Boton_Click;
            // 
            // Volver_Boton
            // 
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.Location = new Point(890, 55);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(110, 32);
            Volver_Boton.TabIndex = 6;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // FormGestionMarcas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1020, 620);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridMarcas);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Name = "FormGestionMarcas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Marcas";
            ((System.ComponentModel.ISupportInitialize)gridMarcas).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gridMarcas;
        private Label lblTitulo;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private GroupBox grpDatos;
        private Label lblNombre;
        private TextBox txtNombre;
        private CheckBox chkActivo;
        private Button Nuevo_Boton;
        private Button Guardar_Boton;
        private Button Eliminar_Boton;
        private Button Cancelar_Boton;
        private Button Volver_Boton;
    }
}