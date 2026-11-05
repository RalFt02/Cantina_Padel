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
        private DataView? _dvProveedor;
        private DataView? _dvCategoria;
        private DataView? _dvMarca;
        private DataView? _dvProducto;
        private DataView? _dvUnidad;
        private bool _cambiandoCriterio;

        public FormSubasPrecios(int idUsuario)
        {
            _idUsuario = idUsuario > 0 ? idUsuario : 1;
            InitializeComponent();
            this.BindingContext ??= new BindingContext();
            CargarCombos();
            ConfigurarEventos();
            this.Shown += (s, e) => txtBuscador.Focus();
        }

        private void ConfigurarEventos()
        {
            rbProveedor.CheckedChanged += RadioButton_CheckedChanged;
            rbCategoria.CheckedChanged += RadioButton_CheckedChanged;
            rbMarca.CheckedChanged += RadioButton_CheckedChanged;
            rbProducto.CheckedChanged += RadioButton_CheckedChanged;
            rbUnidad.CheckedChanged += RadioButton_CheckedChanged;
            txtBuscador.TextChanged += txtBuscador_TextChanged;
        }

        private void RadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            cmbProveedor.Enabled = rbProveedor.Checked;
            cmbCategoria.Enabled = rbCategoria.Checked;
            cmbMarca.Enabled     = rbMarca.Checked;
            cmbProducto.Enabled  = rbProducto.Checked;
            cmbUnidad.Enabled    = rbUnidad.Checked;

            if (!_cambiandoCriterio)
            {
                AplicarFiltroBusqueda(permitirAutoCambioCriterio: false);
            }
        }

        private void txtBuscador_TextChanged(object? sender, EventArgs e)
        {
            AplicarFiltroBusqueda(permitirAutoCambioCriterio: true);
        }

        private void AplicarFiltroBusqueda(bool permitirAutoCambioCriterio)
        {
            string query = txtBuscador.Text.Trim();
            string safeQuery = query.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]");
            string filtroGeneral = string.IsNullOrWhiteSpace(safeQuery) ? "" : $"nombre LIKE '%{safeQuery}%'";
            string filtroProducto = string.IsNullOrWhiteSpace(safeQuery)
                ? ""
                : (_dvProducto != null && _dvProducto.Table != null && _dvProducto.Table.Columns.Contains("codigo")
                    ? $"nombre LIKE '%{safeQuery}%' OR codigo LIKE '%{safeQuery}%'"
                    : $"nombre LIKE '%{safeQuery}%'");

            if (!rbProveedor.Checked && _dvProveedor != null) _dvProveedor.RowFilter = "";
            if (!rbCategoria.Checked && _dvCategoria != null) _dvCategoria.RowFilter = "";
            if (!rbMarca.Checked && _dvMarca != null)         _dvMarca.RowFilter = "";
            if (!rbProducto.Checked && _dvProducto != null)   _dvProducto.RowFilter = "";
            if (!rbUnidad.Checked && _dvUnidad != null)       _dvUnidad.RowFilter = "";

            DataView? dvActivo = ObtenerDataViewActivo();
            ComboBox cmbActivo = ObtenerComboActivo();

            if (dvActivo != null)
            {
                dvActivo.RowFilter = rbProducto.Checked ? filtroProducto : filtroGeneral;
                if (cmbActivo.Items.Count > 0)
                {
                    cmbActivo.SelectedIndex = 0;
                    return;
                }
            }

            if (permitirAutoCambioCriterio && !string.IsNullOrWhiteSpace(safeQuery))
            {
                if (IntentarActivarCriterioConCoincidencia(_dvProducto, filtroProducto, rbProducto, cmbProducto)) return;
                if (IntentarActivarCriterioConCoincidencia(_dvCategoria, filtroGeneral, rbCategoria, cmbCategoria)) return;
                if (IntentarActivarCriterioConCoincidencia(_dvMarca, filtroGeneral, rbMarca, cmbMarca)) return;
                if (IntentarActivarCriterioConCoincidencia(_dvProveedor, filtroGeneral, rbProveedor, cmbProveedor)) return;
                if (rbUnidad.Enabled && IntentarActivarCriterioConCoincidencia(_dvUnidad, filtroGeneral, rbUnidad, cmbUnidad)) return;
            }
        }

        private bool IntentarActivarCriterioConCoincidencia(DataView? dv, string filtro, RadioButton rb, ComboBox cmb)
        {
            if (dv == null || !rb.Enabled) return false;

            dv.RowFilter = filtro;
            if (dv.Count > 0)
            {
                _cambiandoCriterio = true;
                DataView? anterior = ObtenerDataViewActivo();
                if (anterior != null && anterior != dv) anterior.RowFilter = "";

                rb.Checked = true;
                _cambiandoCriterio = false;

                if (cmb.Items.Count > 0)
                    cmb.SelectedIndex = 0;
                return true;
            }

            dv.RowFilter = "";
            return false;
        }

        private DataView? ObtenerDataViewActivo()
        {
            if (rbProveedor.Checked) return _dvProveedor;
            if (rbCategoria.Checked) return _dvCategoria;
            if (rbMarca.Checked)     return _dvMarca;
            if (rbProducto.Checked)  return _dvProducto;
            return _dvUnidad;
        }

        private ComboBox ObtenerComboActivo()
        {
            if (rbProveedor.Checked) return cmbProveedor;
            if (rbCategoria.Checked) return cmbCategoria;
            if (rbMarca.Checked)     return cmbMarca;
            if (rbProducto.Checked)  return cmbProducto;
            return cmbUnidad;
        }

        private void CargarCombos()
        {
            try
            {
                using MySqlConnection conn = Conexion.ObtenerConexion();
                conn.Open();

                string qProv = @"SELECT prov.id_proveedor, COALESCE(NULLIF(p.razon_social, ''), CONCAT(p.nombre, ' ', p.apellido)) AS nombre
                                 FROM proveedor prov JOIN persona p ON p.id_persona = prov.id_persona WHERE p.activo = 1 ORDER BY nombre";
                _dvProveedor = LlenarCombo(cmbProveedor, qProv, conn, "id_proveedor", "nombre");

                _dvCategoria = LlenarCombo(cmbCategoria, "SELECT id_categoria, nombre FROM categoria WHERE activo = 1 ORDER BY nombre", conn, "id_categoria", "nombre");

                _dvMarca = LlenarCombo(cmbMarca, "SELECT id_marca, nombre FROM marca WHERE activo = 1 ORDER BY nombre", conn, "id_marca", "nombre");

                _dvProducto = LlenarCombo(cmbProducto, "SELECT id_producto, nombre, codigo FROM producto WHERE activo = 1 AND id_producto NOT IN (998, 999) ORDER BY nombre", conn, "id_producto", "nombre");

                if (SubasPrecios.SoportaUnidad())
                {
                    try
                    {
                        _dvUnidad = LlenarCombo(cmbUnidad, "SELECT id_unidad, nombre FROM unidad WHERE activo = 1 ORDER BY nombre", conn, "id_unidad", "nombre");
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

        private DataView LlenarCombo(ComboBox cmb, string query, MySqlConnection conn, string valueMember, string displayMember)
        {
            using MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataView dv = new DataView(dt);
            cmb.DisplayMember = displayMember;
            cmb.ValueMember = valueMember;
            cmb.DataSource = dv;
            cmb.SelectedIndex = dv.Count > 0 ? 0 : -1;
            return dv;
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
            string txtPct = txtPorcentaje.Text.Trim().Replace(',', '.');
            if (!decimal.TryParse(txtPct, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal porcentaje) || porcentaje <= 0)
            {
                MessageBox.Show("Ingresá un porcentaje de aumento válido y mayor a cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPorcentaje.Focus();
                return;
            }

            string criterio;
            ComboBox cmbActivo;
            if (rbProveedor.Checked)      { criterio = "proveedor"; cmbActivo = cmbProveedor; }
            else if (rbCategoria.Checked) { criterio = "categoría"; cmbActivo = cmbCategoria; }
            else if (rbMarca.Checked)     { criterio = "marca";     cmbActivo = cmbMarca; }
            else if (rbProducto.Checked)  { criterio = "producto";  cmbActivo = cmbProducto; }
            else                          { criterio = "unidad";    cmbActivo = cmbUnidad; }

            if (cmbActivo.SelectedValue == null || cmbActivo.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Debés seleccionar una opción válida del combo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idSeleccionado;
            if (cmbActivo.SelectedValue is DataRowView drv)
            {
                if (string.IsNullOrWhiteSpace(cmbActivo.ValueMember) || !drv.Row.Table.Columns.Contains(cmbActivo.ValueMember) || drv[cmbActivo.ValueMember] == DBNull.Value)
                {
                    MessageBox.Show("Debés seleccionar una opción válida del combo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                idSeleccionado = Convert.ToInt32(drv[cmbActivo.ValueMember]);
            }
            else
            {
                idSeleccionado = Convert.ToInt32(cmbActivo.SelectedValue);
            }

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