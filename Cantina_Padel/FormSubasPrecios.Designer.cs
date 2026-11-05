namespace Cantina_Padel
{
    partial class FormSubasPrecios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new System.Windows.Forms.Label();
            grpCriterios = new System.Windows.Forms.GroupBox();
            lblBuscador = new System.Windows.Forms.Label();
            txtBuscador = new System.Windows.Forms.TextBox();
            cmbUnidad = new System.Windows.Forms.ComboBox();
            rbUnidad = new System.Windows.Forms.RadioButton();
            cmbProducto = new System.Windows.Forms.ComboBox();
            rbProducto = new System.Windows.Forms.RadioButton();
            cmbMarca = new System.Windows.Forms.ComboBox();
            rbMarca = new System.Windows.Forms.RadioButton();
            cmbCategoria = new System.Windows.Forms.ComboBox();
            rbCategoria = new System.Windows.Forms.RadioButton();
            cmbProveedor = new System.Windows.Forms.ComboBox();
            rbProveedor = new System.Windows.Forms.RadioButton();
            grpAccion = new System.Windows.Forms.GroupBox();
            lblPorcentajeInfo = new System.Windows.Forms.Label();
            lblPorcentaje = new System.Windows.Forms.Label();
            txtPorcentaje = new System.Windows.Forms.TextBox();
            btnAplicar = new System.Windows.Forms.Button();
            btnCerrar = new System.Windows.Forms.Button();
            grpCriterios.SuspendLayout();
            grpAccion.SuspendLayout();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new System.Drawing.Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(378, 35);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "AUMENTO MASIVO DE PRECIOS";
            //
            // grpCriterios
            //
            grpCriterios.Controls.Add(lblBuscador);
            grpCriterios.Controls.Add(txtBuscador);
            grpCriterios.Controls.Add(cmbUnidad);
            grpCriterios.Controls.Add(rbUnidad);
            grpCriterios.Controls.Add(cmbProducto);
            grpCriterios.Controls.Add(rbProducto);
            grpCriterios.Controls.Add(cmbMarca);
            grpCriterios.Controls.Add(rbMarca);
            grpCriterios.Controls.Add(cmbCategoria);
            grpCriterios.Controls.Add(rbCategoria);
            grpCriterios.Controls.Add(cmbProveedor);
            grpCriterios.Controls.Add(rbProveedor);
            grpCriterios.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            grpCriterios.ForeColor = System.Drawing.Color.FromArgb(163, 230, 53);
            grpCriterios.Location = new System.Drawing.Point(20, 75);
            grpCriterios.Name = "grpCriterios";
            grpCriterios.Size = new System.Drawing.Size(540, 340);
            grpCriterios.TabIndex = 1;
            grpCriterios.TabStop = false;
            grpCriterios.Text = "Criterio de Suba";
            //
            // lblBuscador
            //
            lblBuscador.AutoSize = true;
            lblBuscador.ForeColor = System.Drawing.Color.FromArgb(163, 230, 53);
            lblBuscador.Location = new System.Drawing.Point(20, 28);
            lblBuscador.Name = "lblBuscador";
            lblBuscador.Size = new System.Drawing.Size(445, 21);
            lblBuscador.TabIndex = 0;
            lblBuscador.Text = "🔍 Buscar por nombre (categoría, producto, marca, etc.):";
            //
            // txtBuscador
            //
            txtBuscador.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            txtBuscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtBuscador.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtBuscador.ForeColor = System.Drawing.Color.White;
            txtBuscador.Location = new System.Drawing.Point(20, 53);
            txtBuscador.Name = "txtBuscador";
            txtBuscador.PlaceholderText = "Escribí para filtrar proveedor, categoría, marca, producto...";
            txtBuscador.Size = new System.Drawing.Size(490, 30);
            txtBuscador.TabIndex = 1;
            //
            // cmbUnidad
            //
            cmbUnidad.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbUnidad.Enabled = false;
            cmbUnidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbUnidad.ForeColor = System.Drawing.Color.White;
            cmbUnidad.FormattingEnabled = true;
            cmbUnidad.Location = new System.Drawing.Point(180, 280);
            cmbUnidad.Name = "cmbUnidad";
            cmbUnidad.Size = new System.Drawing.Size(330, 29);
            cmbUnidad.TabIndex = 11;
            //
            // rbUnidad
            //
            rbUnidad.AutoSize = true;
            rbUnidad.ForeColor = System.Drawing.Color.White;
            rbUnidad.Location = new System.Drawing.Point(20, 281);
            rbUnidad.Name = "rbUnidad";
            rbUnidad.Size = new System.Drawing.Size(117, 25);
            rbUnidad.TabIndex = 10;
            rbUnidad.Text = "Por Unidad:";
            rbUnidad.UseVisualStyleBackColor = true;
            //
            // cmbProducto
            //
            cmbProducto.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbProducto.Enabled = false;
            cmbProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbProducto.ForeColor = System.Drawing.Color.White;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new System.Drawing.Point(180, 235);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new System.Drawing.Size(330, 29);
            cmbProducto.TabIndex = 9;
            //
            // rbProducto
            //
            rbProducto.AutoSize = true;
            rbProducto.ForeColor = System.Drawing.Color.White;
            rbProducto.Location = new System.Drawing.Point(20, 236);
            rbProducto.Name = "rbProducto";
            rbProducto.Size = new System.Drawing.Size(133, 25);
            rbProducto.TabIndex = 8;
            rbProducto.Text = "Por Producto:";
            rbProducto.UseVisualStyleBackColor = true;
            //
            // cmbMarca
            //
            cmbMarca.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMarca.Enabled = false;
            cmbMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbMarca.ForeColor = System.Drawing.Color.White;
            cmbMarca.FormattingEnabled = true;
            cmbMarca.Location = new System.Drawing.Point(180, 190);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new System.Drawing.Size(330, 29);
            cmbMarca.TabIndex = 7;
            //
            // rbMarca
            //
            rbMarca.AutoSize = true;
            rbMarca.ForeColor = System.Drawing.Color.White;
            rbMarca.Location = new System.Drawing.Point(20, 191);
            rbMarca.Name = "rbMarca";
            rbMarca.Size = new System.Drawing.Size(111, 25);
            rbMarca.TabIndex = 6;
            rbMarca.Text = "Por Marca:";
            rbMarca.UseVisualStyleBackColor = true;
            //
            // cmbCategoria
            //
            cmbCategoria.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbCategoria.Enabled = false;
            cmbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbCategoria.ForeColor = System.Drawing.Color.White;
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new System.Drawing.Point(180, 145);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new System.Drawing.Size(330, 29);
            cmbCategoria.TabIndex = 5;
            //
            // rbCategoria
            //
            rbCategoria.AutoSize = true;
            rbCategoria.ForeColor = System.Drawing.Color.White;
            rbCategoria.Location = new System.Drawing.Point(20, 146);
            rbCategoria.Name = "rbCategoria";
            rbCategoria.Size = new System.Drawing.Size(135, 25);
            rbCategoria.TabIndex = 4;
            rbCategoria.Text = "Por Categoría:";
            rbCategoria.UseVisualStyleBackColor = true;
            //
            // cmbProveedor
            //
            cmbProveedor.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbProveedor.ForeColor = System.Drawing.Color.White;
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new System.Drawing.Point(180, 100);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new System.Drawing.Size(330, 29);
            cmbProveedor.TabIndex = 3;
            //
            // rbProveedor
            //
            rbProveedor.AutoSize = true;
            rbProveedor.Checked = true;
            rbProveedor.ForeColor = System.Drawing.Color.White;
            rbProveedor.Location = new System.Drawing.Point(20, 101);
            rbProveedor.Name = "rbProveedor";
            rbProveedor.Size = new System.Drawing.Size(141, 25);
            rbProveedor.TabIndex = 2;
            rbProveedor.TabStop = true;
            rbProveedor.Text = "Por Proveedor:";
            rbProveedor.UseVisualStyleBackColor = true;
            //
            // grpAccion
            //
            grpAccion.Controls.Add(lblPorcentajeInfo);
            grpAccion.Controls.Add(lblPorcentaje);
            grpAccion.Controls.Add(txtPorcentaje);
            grpAccion.Controls.Add(btnAplicar);
            grpAccion.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            grpAccion.ForeColor = System.Drawing.Color.FromArgb(163, 230, 53);
            grpAccion.Location = new System.Drawing.Point(20, 430);
            grpAccion.Name = "grpAccion";
            grpAccion.Size = new System.Drawing.Size(540, 160);
            grpAccion.TabIndex = 2;
            grpAccion.TabStop = false;
            grpAccion.Text = "Acción a Ejecutar";
            //
            // lblPorcentajeInfo
            //
            lblPorcentajeInfo.AutoSize = true;
            lblPorcentajeInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblPorcentajeInfo.ForeColor = System.Drawing.Color.Gray;
            lblPorcentajeInfo.Location = new System.Drawing.Point(20, 80);
            lblPorcentajeInfo.Name = "lblPorcentajeInfo";
            lblPorcentajeInfo.Size = new System.Drawing.Size(262, 40);
            lblPorcentajeInfo.TabIndex = 3;
            lblPorcentajeInfo.Text = "* Solo aumentará si el precio \r\nes mayor al actual y hay stock positivo.";
            //
            // lblPorcentaje
            //
            lblPorcentaje.AutoSize = true;
            lblPorcentaje.ForeColor = System.Drawing.Color.White;
            lblPorcentaje.Location = new System.Drawing.Point(20, 40);
            lblPorcentaje.Name = "lblPorcentaje";
            lblPorcentaje.Size = new System.Drawing.Size(130, 21);
            lblPorcentaje.TabIndex = 0;
            lblPorcentaje.Text = "Porcentaje (%):";
            //
            // txtPorcentaje
            //
            txtPorcentaje.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            txtPorcentaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtPorcentaje.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            txtPorcentaje.ForeColor = System.Drawing.Color.White;
            txtPorcentaje.Location = new System.Drawing.Point(155, 35);
            txtPorcentaje.Name = "txtPorcentaje";
            txtPorcentaje.Size = new System.Drawing.Size(120, 34);
            txtPorcentaje.TabIndex = 1;
            txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            txtPorcentaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(SoloNumerosDecimal_KeyPress);
            //
            // btnAplicar
            //
            btnAplicar.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnAplicar.FlatAppearance.BorderSize = 0;
            btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAplicar.ForeColor = System.Drawing.Color.White;
            btnAplicar.Location = new System.Drawing.Point(310, 35);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new System.Drawing.Size(200, 85);
            btnAplicar.TabIndex = 2;
            btnAplicar.Text = "⚡ APLICAR SUBA";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += new System.EventHandler(btnAplicar_Click);
            //
            // btnCerrar
            //
            btnCerrar.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCerrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnCerrar.ForeColor = System.Drawing.Color.White;
            btnCerrar.Location = new System.Drawing.Point(450, 20);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new System.Drawing.Size(110, 32);
            btnCerrar.TabIndex = 3;
            btnCerrar.Text = "← Volver";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += new System.EventHandler(btnCerrar_Click);
            //
            // FormSubasPrecios
            //
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            ClientSize = new System.Drawing.Size(580, 615);
            Controls.Add(btnCerrar);
            Controls.Add(grpAccion);
            Controls.Add(grpCriterios);
            Controls.Add(lblTitulo);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSubasPrecios";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Suba Masiva de Precios";
            grpCriterios.ResumeLayout(false);
            grpCriterios.PerformLayout();
            grpAccion.ResumeLayout(false);
            grpAccion.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpCriterios;
        private System.Windows.Forms.Label lblBuscador;
        private System.Windows.Forms.TextBox txtBuscador;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.RadioButton rbProveedor;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.RadioButton rbCategoria;
        private System.Windows.Forms.ComboBox cmbMarca;
        private System.Windows.Forms.RadioButton rbMarca;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.RadioButton rbProducto;
        private System.Windows.Forms.ComboBox cmbUnidad;
        private System.Windows.Forms.RadioButton rbUnidad;
        private System.Windows.Forms.GroupBox grpAccion;
        private System.Windows.Forms.Label lblPorcentaje;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblPorcentajeInfo;
    }
}