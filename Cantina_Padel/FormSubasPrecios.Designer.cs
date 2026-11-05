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
            this.BindingContext = new System.Windows.Forms.BindingContext();
            grpAccion.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.FromArgb(163, 230, 53);
            lblTitulo.Location = new System.Drawing.Point(240, 45);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(340, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "AUMENTO MASIVO DE PRECIOS";
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = System.Windows.Forms.AnchorStyles.None;
            btnCerrar.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCerrar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnCerrar.ForeColor = System.Drawing.Color.White;
            btnCerrar.Location = new System.Drawing.Point(910, 45);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new System.Drawing.Size(130, 36);
            btnCerrar.TabIndex = 3;
            btnCerrar.Text = "← Volver";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += new System.EventHandler(btnCerrar_Click);
            // 
            // grpCriterios
            // 
            grpCriterios.Anchor = System.Windows.Forms.AnchorStyles.None;
            grpCriterios.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
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
            grpCriterios.Location = new System.Drawing.Point(240, 100);
            grpCriterios.Name = "grpCriterios";
            grpCriterios.Size = new System.Drawing.Size(800, 370);
            grpCriterios.TabIndex = 1;
            grpCriterios.TabStop = false;
            grpCriterios.Text = "Criterio de Suba";
            // 
            // lblBuscador
            // 
            lblBuscador.AutoSize = true;
            lblBuscador.ForeColor = System.Drawing.Color.FromArgb(163, 230, 53);
            lblBuscador.Location = new System.Drawing.Point(25, 30);
            lblBuscador.Name = "lblBuscador";
            lblBuscador.Size = new System.Drawing.Size(375, 17);
            lblBuscador.TabIndex = 0;
            lblBuscador.Text = "🔍 Buscar por nombre (categoría, producto, marca, etc.):";
            // 
            // txtBuscador
            // 
            txtBuscador.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            txtBuscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtBuscador.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtBuscador.ForeColor = System.Drawing.Color.White;
            txtBuscador.Location = new System.Drawing.Point(25, 55);
            txtBuscador.Name = "txtBuscador";
            txtBuscador.PlaceholderText = "Escribí para filtrar proveedor, categoría, marca, producto...";
            txtBuscador.Size = new System.Drawing.Size(750, 26);
            txtBuscador.TabIndex = 1;
            // 
            // rbProveedor
            // 
            rbProveedor.AutoSize = true;
            rbProveedor.Checked = true;
            rbProveedor.ForeColor = System.Drawing.Color.White;
            rbProveedor.Location = new System.Drawing.Point(25, 108);
            rbProveedor.Name = "rbProveedor";
            rbProveedor.Size = new System.Drawing.Size(118, 21);
            rbProveedor.TabIndex = 2;
            rbProveedor.TabStop = true;
            rbProveedor.Text = "Por Proveedor:";
            rbProveedor.UseVisualStyleBackColor = true;
            // 
            // cmbProveedor
            // 
            cmbProveedor.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbProveedor.ForeColor = System.Drawing.Color.White;
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new System.Drawing.Point(200, 105);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new System.Drawing.Size(575, 25);
            cmbProveedor.TabIndex = 3;
            // 
            // rbCategoria
            // 
            rbCategoria.AutoSize = true;
            rbCategoria.ForeColor = System.Drawing.Color.White;
            rbCategoria.Location = new System.Drawing.Point(25, 158);
            rbCategoria.Name = "rbCategoria";
            rbCategoria.Size = new System.Drawing.Size(114, 21);
            rbCategoria.TabIndex = 4;
            rbCategoria.Text = "Por Categoría:";
            rbCategoria.UseVisualStyleBackColor = true;
            // 
            // cmbCategoria
            // 
            cmbCategoria.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbCategoria.Enabled = false;
            cmbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbCategoria.ForeColor = System.Drawing.Color.White;
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new System.Drawing.Point(200, 155);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new System.Drawing.Size(575, 25);
            cmbCategoria.TabIndex = 5;
            // 
            // rbMarca
            // 
            rbMarca.AutoSize = true;
            rbMarca.ForeColor = System.Drawing.Color.White;
            rbMarca.Location = new System.Drawing.Point(25, 208);
            rbMarca.Name = "rbMarca";
            rbMarca.Size = new System.Drawing.Size(95, 21);
            rbMarca.TabIndex = 6;
            rbMarca.Text = "Por Marca:";
            rbMarca.UseVisualStyleBackColor = true;
            // 
            // cmbMarca
            // 
            cmbMarca.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMarca.Enabled = false;
            cmbMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbMarca.ForeColor = System.Drawing.Color.White;
            cmbMarca.FormattingEnabled = true;
            cmbMarca.Location = new System.Drawing.Point(200, 205);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new System.Drawing.Size(575, 25);
            cmbMarca.TabIndex = 7;
            // 
            // rbProducto
            // 
            rbProducto.AutoSize = true;
            rbProducto.ForeColor = System.Drawing.Color.White;
            rbProducto.Location = new System.Drawing.Point(25, 258);
            rbProducto.Name = "rbProducto";
            rbProducto.Size = new System.Drawing.Size(112, 21);
            rbProducto.TabIndex = 8;
            rbProducto.Text = "Por Producto:";
            rbProducto.UseVisualStyleBackColor = true;
            // 
            // cmbProducto
            // 
            cmbProducto.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbProducto.Enabled = false;
            cmbProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbProducto.ForeColor = System.Drawing.Color.White;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new System.Drawing.Point(200, 255);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new System.Drawing.Size(575, 25);
            cmbProducto.TabIndex = 9;
            // 
            // rbUnidad
            // 
            rbUnidad.AutoSize = true;
            rbUnidad.ForeColor = System.Drawing.Color.White;
            rbUnidad.Location = new System.Drawing.Point(25, 308);
            rbUnidad.Name = "rbUnidad";
            rbUnidad.Size = new System.Drawing.Size(98, 21);
            rbUnidad.TabIndex = 10;
            rbUnidad.Text = "Por Unidad:";
            rbUnidad.UseVisualStyleBackColor = true;
            // 
            // cmbUnidad
            // 
            cmbUnidad.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            cmbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbUnidad.Enabled = false;
            cmbUnidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbUnidad.ForeColor = System.Drawing.Color.White;
            cmbUnidad.FormattingEnabled = true;
            cmbUnidad.Location = new System.Drawing.Point(200, 305);
            cmbUnidad.Name = "cmbUnidad";
            cmbUnidad.Size = new System.Drawing.Size(575, 25);
            cmbUnidad.TabIndex = 11;
            // 
            // grpAccion
            // 
            grpAccion.Anchor = System.Windows.Forms.AnchorStyles.None;
            grpAccion.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            grpAccion.Controls.Add(lblPorcentajeInfo);
            grpAccion.Controls.Add(lblPorcentaje);
            grpAccion.Controls.Add(txtPorcentaje);
            grpAccion.Controls.Add(btnAplicar);
            grpAccion.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            grpAccion.ForeColor = System.Drawing.Color.FromArgb(163, 230, 53);
            grpAccion.Location = new System.Drawing.Point(240, 490);
            grpAccion.Name = "grpAccion";
            grpAccion.Size = new System.Drawing.Size(800, 175);
            grpAccion.TabIndex = 2;
            grpAccion.TabStop = false;
            grpAccion.Text = "Acción a Ejecutar";
            // 
            // lblPorcentaje
            // 
            lblPorcentaje.AutoSize = true;
            lblPorcentaje.ForeColor = System.Drawing.Color.White;
            lblPorcentaje.Location = new System.Drawing.Point(25, 45);
            lblPorcentaje.Name = "lblPorcentaje";
            lblPorcentaje.Size = new System.Drawing.Size(110, 17);
            lblPorcentaje.TabIndex = 0;
            lblPorcentaje.Text = "Porcentaje (%):";
            // 
            // txtPorcentaje
            // 
            txtPorcentaje.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            txtPorcentaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtPorcentaje.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            txtPorcentaje.ForeColor = System.Drawing.Color.White;
            txtPorcentaje.Location = new System.Drawing.Point(160, 40);
            txtPorcentaje.Name = "txtPorcentaje";
            txtPorcentaje.Size = new System.Drawing.Size(180, 29);
            txtPorcentaje.TabIndex = 1;
            txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            txtPorcentaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(SoloNumerosDecimal_KeyPress);
            // 
            // lblPorcentajeInfo
            // 
            lblPorcentajeInfo.AutoSize = true;
            lblPorcentajeInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblPorcentajeInfo.ForeColor = System.Drawing.Color.Gray;
            lblPorcentajeInfo.Location = new System.Drawing.Point(25, 92);
            lblPorcentajeInfo.Name = "lblPorcentajeInfo";
            lblPorcentajeInfo.Size = new System.Drawing.Size(340, 30);
            lblPorcentajeInfo.TabIndex = 3;
            lblPorcentajeInfo.Text = "* Solo aumentará si el precio es mayor al actual y hay stock positivo.";
            // 
            // btnAplicar
            // 
            btnAplicar.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAplicar.FlatAppearance.BorderSize = 0;
            btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAplicar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnAplicar.ForeColor = System.Drawing.Color.White;
            btnAplicar.Location = new System.Drawing.Point(480, 40);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new System.Drawing.Size(295, 95);
            btnAplicar.TabIndex = 2;
            btnAplicar.Text = "⚡ APLICAR SUBA";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += new System.EventHandler(btnAplicar_Click);
            // 
            // FormSubasPrecios
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            ClientSize = new System.Drawing.Size(1280, 720);
            Controls.Add(btnCerrar);
            Controls.Add(grpAccion);
            Controls.Add(grpCriterios);
            Controls.Add(lblTitulo);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            ForeColor = System.Drawing.Color.White;
            MinimumSize = new System.Drawing.Size(1150, 680);
            Name = "FormSubasPrecios";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Suba Masiva de Precios - Cantina Padel Club";
            grpCriterios.ResumeLayout(false);
            grpCriterios.PerformLayout();
            grpAccion.ResumeLayout(false);
            grpAccion.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
                    WindowState = FormWindowState.Maximized;
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
