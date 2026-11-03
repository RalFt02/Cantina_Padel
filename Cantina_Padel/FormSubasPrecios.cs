using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Cantina_Padel
{
    public partial class FormSubasPrecios : Form
    {
        private readonly int _idUsuario;

        public FormSubasPrecios(int idUsuario)
        {
            _idUsuario = idUsuario > 0 ? idUsuario : 1;
            InitializeComponent();
            ConfigurarEventos();
            CargarCombos();
        }

        private void ConfigurarEventos()
        {
            rbProveedor.CheckedChanged += RadioButton_CheckedChanged;
            rbCategoria.CheckedChanged += RadioButton_CheckedChanged;
            rbMarca.CheckedChanged += RadioButton_CheckedChanged;
            rbProducto.CheckedChanged += RadioButton_CheckedChanged;
            rbUnidad.CheckedChanged += RadioButton_CheckedChanged;
        }

        private void RadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            cmbProveedor.Enabled = rbProveedor.Checked;
            cmbCategoria.Enabled = rbCategoria.Checked;
            cmbMarca.Enabled     = rbMarca.Checked;
            cmbProducto.Enabled  = rbProducto.Checked;
            cmbUnidad.Enabled    = rbUnidad.Checked;
        }

        private void CargarCombos()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                // 1. Proveedores
                string qProv = @"SELECT prov.id_proveedor, COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido)) AS nombre 
                                 FROM proveedor prov JOIN persona p ON p.id_persona = prov.id_persona WHERE p.activo = 1";
                LlenarCombo(cmbProveedor, qProv, conn, "id_proveedor", "nombre");

                // 2. Categorías
                LlenarCombo(cmbCategoria, "SELECT id_categoria, nombre FROM categoria WHERE activo = 1", conn, "id_categoria", "nombre");

                // 3. Marcas
                LlenarCombo(cmbMarca, "SELECT id_marca, nombre FROM marca WHERE activo = 1", conn, "id_marca", "nombre");

                // 4. Productos
                LlenarCombo(cmbProducto, "SELECT id_producto, nombre FROM producto WHERE activo = 1", conn, "id_producto", "nombre");

                // 5. Unidades: solo si la BD tiene la tabla 'unidad' y la columna producto.id_unidad
                if (SubasPrecios.SoportaUnidad())
                {
                    try
                    {
                        LlenarCombo(cmbUnidad, "SELECT id_unidad, nombre FROM unidad WHERE activo = 1", conn, "id_unidad", "nombre");
                    }
                    catch { DeshabilitarUnidad(); }
                }
                else
                {
                    DeshabilitarUnidad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los combos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeshabilitarUnidad()
        {
            cmbUnidad.Enabled = false;
            rbUnidad.Enabled = false;
            rbUnidad.Text = "Por Unidad (no disponible)";
            rbUnidad.AutoSize = true;
        }

        private void LlenarCombo(ComboBox cmb, string query, MySqlConnection conn, string valueMember, string displayMember)
        {
            using MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmb.DataSource = dt;
            cmb.ValueMember = valueMember;
            cmb.DisplayMember = displayMember;
            cmb.SelectedIndex = dt.Rows.Count > 0 ? 0 : -1;
        }

        private void SoloNumerosDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;
            if (char.IsControl(e.KeyChar)) return;
            if ((e.KeyChar == '.' || e.KeyChar == ',') && !txt.Text.Contains('.') && !txt.Text.Contains(',')) return;
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            // Validar porcentaje
            string txtPct = txtPorcentaje.Text.Trim().Replace(',', '.');
            if (!decimal.TryParse(txtPct, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal porcentaje) || porcentaje <= 0)
            {
                MessageBox.Show("Ingresá un porcentaje de aumento válido y mayor a cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPorcentaje.Focus();
                return;
            }

            // Determinar el criterio elegido
            string criterio;
            ComboBox cmbActivo;
            if (rbProveedor.Checked)      { criterio = "proveedor"; cmbActivo = cmbProveedor; }
            else if (rbCategoria.Checked) { criterio = "categoría"; cmbActivo = cmbCategoria; }
            else if (rbMarca.Checked)     { criterio = "marca";     cmbActivo = cmbMarca; }
            else if (rbProducto.Checked)  { criterio = "producto";  cmbActivo = cmbProducto; }
            else                          { criterio = "unidad";    cmbActivo = cmbUnidad; }

            if (cmbActivo.SelectedValue == null)
            {
                MessageBox.Show("Debés seleccionar una opción válida del combo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idSeleccionado = Convert.ToInt32(cmbActivo.SelectedValue);

            DialogResult conf = MessageBox.Show(
                $"¿Aplicar un aumento del {porcentaje}% a los productos del {criterio} \"{cmbActivo.Text}\"?\n\n" +
                "Solo se actualizarán los productos con stock > 0 y cuyo nuevo precio sea superior al actual.",
                "Confirmar Aumento Masivo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (conf != DialogResult.Yes) return;

            try
            {
                var subas = new SubasPrecios();
                int afectados;

                if (rbProveedor.Checked)      afectados = subas.SubirPorProveedor(idSeleccionado, porcentaje, _idUsuario);
                else if (rbCategoria.Checked) afectados = subas.SubirPorCategoria(idSeleccionado, porcentaje, _idUsuario);
                else if (rbMarca.Checked)     afectados = subas.SubirPorMarca(idSeleccionado, porcentaje, _idUsuario);
                else if (rbProducto.Checked)  afectados = subas.SubirPorProducto(idSeleccionado, porcentaje, _idUsuario);
                else                          afectados = subas.SubirPorUnidad(idSeleccionado, porcentaje, _idUsuario);

                MessageBox.Show(
                    afectados > 0
                        ? $"¡Aumento aplicado con éxito!\n\nSe actualizaron los precios de {afectados} producto(s)."
                        : "No se actualizó ningún producto.\n\nNinguno cumplía la condición (stock > 0 y precio nuevo superior al actual).",
                    "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPorcentaje.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al procesar la suba de precios:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}