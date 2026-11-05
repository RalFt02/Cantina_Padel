using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace Cantina_Padel
{
    public partial class FormGestionMarcas : Form
    {
        private int? _idSeleccionado;

        public FormGestionMarcas()
        {
            InitializeComponent();
            this.BindingContext ??= new BindingContext();
            gridMarcas.AutoGenerateColumns = true;
            gridMarcas.BindingContext = this.BindingContext;
            gridMarcas.DataBindingComplete += (_, _) => FormatearColumnasMarcas();
            this.Load += (_, _) => FormatearColumnasMarcas();
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

                this.BindingContext ??= new BindingContext();
                gridMarcas.AutoGenerateColumns = true;
                gridMarcas.BindingContext = this.BindingContext;
                gridMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridMarcas.DataSource = dt;

                FormatearColumnasMarcas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasMarcas()
        {
            if (gridMarcas == null || gridMarcas.Columns.Count == 0) return;

            if (gridMarcas.Columns["id_marca"] is DataGridViewColumn colId)
            {
                colId.HeaderText = "ID";
                colId.FillWeight = 18;
            }
            if (gridMarcas.Columns["nombre"] is DataGridViewColumn colNom)
            {
                colNom.HeaderText = "Nombre";
                colNom.FillWeight = 62;
            }
            if (gridMarcas.Columns["activo"] is DataGridViewColumn colAct)
            {
                colAct.HeaderText = "Activo";
                colAct.FillWeight = 20;
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

                bool exito = esNuevo
                    ? CrearMarca(conn)
                    : EditarMarca(conn);

                if (!exito) return;

                MessageBox.Show("Operación realizada con éxito.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarMarcas();
                LimpiarFormulario();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Ya existe una marca registrada con ese nombre.",
                    "Marca duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CrearMarca(MySqlConnection conn)
        {
            string check = "SELECT COUNT(*) FROM Marca WHERE nombre = @n";
            using (MySqlCommand cmdCheck = new MySqlCommand(check, conn))
            {
                cmdCheck.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
                long count = Convert.ToInt64(cmdCheck.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Ya existe una marca con ese nombre.",
                        "Marca ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            string query = @"
                INSERT INTO Marca (nombre, activo)
                VALUES (@n, @a)";

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@a", chkActivo.Checked ? 1 : 0);
            cmd.ExecuteNonQuery();
            return true;
        }

        private bool EditarMarca(MySqlConnection conn)
        {
            int id = _idSeleccionado!.Value;

            string check = "SELECT COUNT(*) FROM Marca WHERE nombre = @n AND id_marca <> @id";
            using (MySqlCommand cmdCheck = new MySqlCommand(check, conn))
            {
                cmdCheck.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
                cmdCheck.Parameters.AddWithValue("@id", id);
                long count = Convert.ToInt64(cmdCheck.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Ya existe otra marca con ese nombre.",
                        "Marca ya existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            string query = @"
                UPDATE Marca
                SET nombre = @n,
                    activo = @a
                WHERE id_marca = @id";

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@a", chkActivo.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            return true;
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
            catch (Exception ex)
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
            if (e.RowIndex < 0 || e.RowIndex >= gridMarcas.Rows.Count) return;
            var row = gridMarcas.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var idVal = gridMarcas.Columns.Contains("id_marca") ? row.Cells["id_marca"]?.Value : (row.Cells.Count > 0 ? row.Cells[0]?.Value : null);
            if (idVal == null || idVal == DBNull.Value) return;

            _idSeleccionado = Convert.ToInt32(idVal);
            txtNombre.Text = gridMarcas.Columns.Contains("nombre") ? (row.Cells["nombre"]?.Value?.ToString() ?? "") : "";
            var actVal = gridMarcas.Columns.Contains("activo") ? row.Cells["activo"]?.Value : null;
            chkActivo.Checked = actVal != null && actVal != DBNull.Value && Convert.ToBoolean(actVal);
            Guardar_Boton.Text = "Guardar Cambios";
        }

        // ─────────────────────────────────────────────
        //  Buscar en tiempo real
        // ─────────────────────────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (gridMarcas.DataSource is DataTable dt)
            {
                string filtro = txtBuscar.Text.Replace("'", "''");
                dt.DefaultView.RowFilter = $"nombre LIKE '%{filtro}%'";
            }
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
            this.BindingContext = new System.Windows.Forms.BindingContext();
            // 
            // gridMarcas
            // 
            gridMarcas.AllowUserToAddRows = false;
            gridMarcas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            gridMarcas.ColumnHeadersHeight = 32;
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
            gridMarcas.Size = new Size(760, 605);
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
            lblTitulo.Size = new Size(260, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE MARCAS";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(20, 60);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(48, 15);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(51, 65, 85);
            txtBuscar.ForeColor = Color.White;
            txtBuscar.Location = new Point(80, 56);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(280, 24);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // grpDatos
            // 
            grpDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpDatos.BackColor = Color.FromArgb(30, 41, 59);
            grpDatos.Controls.Add(lblNombre);
            grpDatos.Controls.Add(txtNombre);
            grpDatos.Controls.Add(chkActivo);
            grpDatos.Controls.Add(Guardar_Boton);
            grpDatos.Controls.Add(Cancelar_Boton);
            grpDatos.Controls.Add(Eliminar_Boton);
            grpDatos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDatos.ForeColor = Color.FromArgb(163, 230, 53);
            grpDatos.Location = new Point(800, 95);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(460, 605);
            grpDatos.TabIndex = 4;
            grpDatos.TabStop = false;
            grpDatos.Text = "Datos de la Marca";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(15, 55);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(68, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre: *";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(51, 65, 85);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(15, 80);
            txtNombre.MaxLength = 30;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(430, 24);
            txtNombre.TabIndex = 3;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.ForeColor = Color.White;
            chkActivo.Location = new Point(15, 130);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(100, 19);
            chkActivo.TabIndex = 4;
            chkActivo.Text = "Marca activa";
            // 
            // Guardar_Boton
            // 
            Guardar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Guardar_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Guardar_Boton.Cursor = Cursors.Hand;
            Guardar_Boton.FlatAppearance.BorderSize = 0;
            Guardar_Boton.FlatStyle = FlatStyle.Flat;
            Guardar_Boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Guardar_Boton.ForeColor = Color.Black;
            Guardar_Boton.Location = new Point(15, 425);
            Guardar_Boton.Name = "Guardar_Boton";
            Guardar_Boton.Size = new Size(430, 44);
            Guardar_Boton.TabIndex = 5;
            Guardar_Boton.Text = "Crear Marca";
            Guardar_Boton.UseVisualStyleBackColor = false;
            Guardar_Boton.Click += Guardar_Boton_Click;
            // 
            // Eliminar_Boton
            // 
            Eliminar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Eliminar_Boton.BackColor = Color.FromArgb(239, 68, 68);
            Eliminar_Boton.Cursor = Cursors.Hand;
            Eliminar_Boton.FlatAppearance.BorderSize = 0;
            Eliminar_Boton.FlatStyle = FlatStyle.Flat;
            Eliminar_Boton.ForeColor = Color.White;
            Eliminar_Boton.Location = new Point(15, 482);
            Eliminar_Boton.Name = "Eliminar_Boton";
            Eliminar_Boton.Size = new Size(430, 44);
            Eliminar_Boton.TabIndex = 7;
            Eliminar_Boton.Text = "Desactivar";
            Eliminar_Boton.UseVisualStyleBackColor = false;
            Eliminar_Boton.Click += Eliminar_Boton_Click;
            // 
            // Cancelar_Boton
            // 
            Cancelar_Boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Cancelar_Boton.BackColor = Color.FromArgb(71, 85, 105);
            Cancelar_Boton.Cursor = Cursors.Hand;
            Cancelar_Boton.FlatAppearance.BorderSize = 0;
            Cancelar_Boton.FlatStyle = FlatStyle.Flat;
            Cancelar_Boton.ForeColor = Color.White;
            Cancelar_Boton.Location = new Point(15, 538);
            Cancelar_Boton.Name = "Cancelar_Boton";
            Cancelar_Boton.Size = new Size(430, 44);
            Cancelar_Boton.TabIndex = 6;
            Cancelar_Boton.Text = "Limpiar";
            Cancelar_Boton.UseVisualStyleBackColor = false;
            Cancelar_Boton.Click += Cancelar_Boton_Click;
            // 
            // Nuevo_Boton
            // 
            Nuevo_Boton.BackColor = Color.FromArgb(163, 230, 53);
            Nuevo_Boton.Cursor = Cursors.Hand;
            Nuevo_Boton.FlatAppearance.BorderSize = 0;
            Nuevo_Boton.FlatStyle = FlatStyle.Flat;
            Nuevo_Boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Nuevo_Boton.ForeColor = Color.Black;
            Nuevo_Boton.Location = new Point(380, 53);
            Nuevo_Boton.Name = "Nuevo_Boton";
            Nuevo_Boton.Size = new Size(160, 32);
            Nuevo_Boton.TabIndex = 5;
            Nuevo_Boton.Text = "+ Nueva Marca";
            Nuevo_Boton.UseVisualStyleBackColor = false;
            Nuevo_Boton.Click += Nuevo_Boton_Click;
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
            Volver_Boton.Location = new Point(1130, 53);
            Volver_Boton.Name = "Volver_Boton";
            Volver_Boton.Size = new Size(130, 32);
            Volver_Boton.TabIndex = 6;
            Volver_Boton.Text = "← Volver";
            Volver_Boton.UseVisualStyleBackColor = false;
            Volver_Boton.Click += Volver_Boton_Click;
            // 
            // FormGestionMarcas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(gridMarcas);
            Controls.Add(grpDatos);
            Controls.Add(Nuevo_Boton);
            Controls.Add(Volver_Boton);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormGestionMarcas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Marcas - Cantina Padel Club";
            ((System.ComponentModel.ISupportInitialize)gridMarcas).EndInit();
            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
            WindowState = FormWindowState.Maximized;
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