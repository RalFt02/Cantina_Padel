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

                bool esNuevo = string.IsNullOrEmpty(txtId.Text);

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
            cmd.Parameters.AddWithValue("@id", int.Parse(txtId.Text));
            cmd.ExecuteNonQuery();
        }

        // ─────────────────────────────────────────────
        //  DELETE - Baja lógica
        // ─────────────────────────────────────────────
        private void Eliminar_Boton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
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
                cmd.Parameters.AddWithValue("@id", int.Parse(txtId.Text));
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

            txtId.Text = row.Cells["id_marca"].Value?.ToString() ?? "";
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
            txtId.Text = "";
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
            gridMarcas = new DataGridView();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            grpDatos = new GroupBox();
            lblId = new Label();
            txtId = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            chkActivo = new CheckBox();
            Nuevo_Boton = new Button();
            Guardar_Boton = new Button();
            Eliminar_Boton = new Button();
            Cancelar_Boton = new Button();
            Volver_Boton = new Button();

            ((System.ComponentModel.ISupportInitialize)gridMarcas).BeginInit();
            grpDatos.SuspendLayout();
            SuspendLayout();

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1020, 620);
            Name = "FormGestionMarcas";
            Text = "Gestión de Marcas";
            StartPosition = FormStartPosition.CenterScreen;

            lblTitulo.Text = "GESTIÓN DE MARCAS";
            lblTitulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.AutoSize = true;

            lblBuscar.Text = "Buscar:";
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(20, 60);
            lblBuscar.AutoSize = true;

            txtBuscar.Location = new Point(80, 57);
            txtBuscar.Size = new Size(240, 27);
            txtBuscar.BackColor = Color.FromArgb(51, 65, 85);
            txtBuscar.ForeColor = Color.White;
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            gridMarcas.Location = new Point(20, 95);
            gridMarcas.Size = new Size(580, 490);
            gridMarcas.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridMarcas.ForeColor = Color.White;
            gridMarcas.GridColor = Color.FromArgb(51, 65, 85);
            gridMarcas.BorderStyle = BorderStyle.None;
            gridMarcas.RowHeadersVisible = false;
            gridMarcas.AllowUserToAddRows = false;
            gridMarcas.ReadOnly = true;
            gridMarcas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            gridMarcas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 65, 85);
            gridMarcas.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(163, 230, 53);
            gridMarcas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridMarcas.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            gridMarcas.DefaultCellStyle.ForeColor = Color.White;
            gridMarcas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(163, 230, 53);
            gridMarcas.DefaultCellStyle.SelectionForeColor = Color.Black;
            gridMarcas.CellClick += gridMarcas_CellClick;

            grpDatos.Text = "Datos de la Marca";
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.Location = new Point(620, 95);
            grpDatos.Size = new Size(380, 280);
            grpDatos.BackColor = Color.FromArgb(30, 41, 59);

            lblId.Text = "ID:";
            lblId.ForeColor = Color.Gray;
            lblId.Location = new Point(10, 28);
            lblId.AutoSize = true;
            txtId.Location = new Point(40, 25);
            txtId.Size = new Size(60, 27);
            txtId.ReadOnly = true;
            txtId.BackColor = Color.FromArgb(15, 23, 42);
            txtId.ForeColor = Color.Gray;

            lblNombre.Text = "Nombre: *";
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(10, 65);
            lblNombre.AutoSize = true;
            txtNombre.Location = new Point(10, 87);
            txtNombre.Size = new Size(350, 27);
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;

            chkActivo.Text = "Marca activa";
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(10, 125);
            chkActivo.Checked = true;
            chkActivo.AutoSize = true;

            Guardar_Boton.Text = "Crear Marca";
            Guardar_Boton.Location = new Point(10, 165);
            Guardar_Boton.Size = new Size(165, 38);
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;

            Cancelar_Boton.Text = "Limpiar";
            Cancelar_Boton.Location = new Point(185, 165);
            Cancelar_Boton.Size = new Size(175, 38);
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click += Cancelar_Boton_Click;

            Eliminar_Boton.Text = "Desactivar";
            Eliminar_Boton.Location = new Point(10, 213);
            Eliminar_Boton.Size = new Size(350, 38);
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;

            grpDatos.Controls.AddRange(new Control[]
            {
                lblId, txtId,
                lblNombre, txtNombre,
                chkActivo,
                Guardar_Boton, Cancelar_Boton,
                Eliminar_Boton
            });

            Nuevo_Boton.Text = "+ Nueva Marca";
            Nuevo_Boton.Location = new Point(340, 55);
            Nuevo_Boton.Size = new Size(150, 32);
            Nuevo_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Nuevo_Boton.ForeColor = Color.Black;
            Nuevo_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Nuevo_Boton.FlatStyle = FlatStyle.Flat;
            Nuevo_Boton.FlatAppearance.BorderSize = 0;
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click += Nuevo_Boton_Click;

            Volver_Boton.Text = "← Volver";
            Volver_Boton.Location = new Point(890, 55);
            Volver_Boton.Size = new Size(110, 32);
            Volver_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Volver_Boton.ForeColor = Color.White;
            Volver_Boton.FlatStyle = FlatStyle.Flat;
            Volver_Boton.FlatAppearance.BorderSize = 0;
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;

            Controls.AddRange(new Control[]
            {
                lblTitulo,
                lblBuscar, txtBuscar,
                gridMarcas,
                grpDatos,
                Nuevo_Boton,
                Volver_Boton
            });

            ((System.ComponentModel.ISupportInitialize)gridMarcas).EndInit();
            grpDatos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gridMarcas;
        private Label lblTitulo;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private GroupBox grpDatos;
        private Label lblId;
        private TextBox txtId;
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