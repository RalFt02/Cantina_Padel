using System.Data;

namespace Cantina_Padel
{
    public partial class FormGestionCaja : Form
    {
        private int? _idSesionActual;
        private readonly int _idUsuario;   // usuario logueado
        private const int ID_CAJA = 1;     // número físico de caja

        public FormGestionCaja(int idUsuario)
        {
            _idUsuario = idUsuario;
            InitializeComponent();
            RefrescarEstadoCaja();
        }

        // ═══════════════════════════════════════════════════
        //  MÉTODOS DE ESTADO
        // ═══════════════════════════════════════════════════

        /// <summary>Consulta si hay caja abierta y actualiza toda la UI.</summary>
        private void RefrescarEstadoCaja()
        {
            DataRow? sesion = CajaService.ObtenerSesionAbierta(ID_CAJA);

            if (sesion != null)
            {
                _idSesionActual = Convert.ToInt32(sesion["id_sesion"]);
                MostrarCajaAbierta(sesion);
            }
            else
            {
                _idSesionActual = null;
                MostrarCajaCerrada();
            }

            CargarGrillaSesiones();
        }

        /// <summary>Actualiza los controles para mostrar que la caja está abierta.</summary>
        private void MostrarCajaAbierta(DataRow sesion)
        {
            lblEstadoCaja.Text      = "● CAJA ABIERTA";
            lblEstadoCaja.ForeColor = Color.FromArgb(163, 230, 53);
            lblCajero.Text          = "Cajero: " + sesion["cajero"].ToString();
            lblApertura.Text        = "Apertura: " +
                Convert.ToDateTime(sesion["fecha_apertura"]).ToString("dd/MM/yyyy HH:mm");
            lblMontoInicio.Text     = "Inicio: $" +
                Convert.ToDecimal(sesion["monto_inicio"]).ToString("N2");

            ActualizarDisponible();

            Abrir_Boton.Enabled   = false;
            Cerrar_Boton.Enabled  = true;
            Retiro_Boton.Enabled  = true;

            CargarGrillaRetiros();
        }

        /// <summary>Actualiza los controles para mostrar que la caja está cerrada.</summary>
        private void MostrarCajaCerrada()
        {
            lblEstadoCaja.Text      = "● CAJA CERRADA";
            lblEstadoCaja.ForeColor = Color.IndianRed;
            lblCajero.Text          = "";
            lblApertura.Text        = "";
            lblMontoInicio.Text     = "";
            lblDisponible.Text      = "";

            Abrir_Boton.Enabled   = true;
            Cerrar_Boton.Enabled  = false;
            Retiro_Boton.Enabled  = false;

            gridRetiros.DataSource = null;
        }

        /// <summary>Calcula y muestra el efectivo disponible en caja.</summary>
        private void ActualizarDisponible()
        {
            if (_idSesionActual == null) return;

            decimal inicio   = ObtenerMontoInicio();
            decimal retiros  = CajaService.ObtenerTotalRetiros(_idSesionActual.Value);
            decimal disponible = CajaService.CalcularDisponible(inicio, 0, retiros);

            lblDisponible.Text      = $"Disponible: ${disponible:N2}";
            lblDisponible.ForeColor = disponible >= 0
                ? Color.FromArgb(163, 230, 53)
                : Color.IndianRed;
        }

        private decimal ObtenerMontoInicio()
        {
            if (_idSesionActual == null) return 0;
            DataRow? sesion = CajaService.ObtenerSesionAbierta(ID_CAJA);
            return sesion != null ? Convert.ToDecimal(sesion["monto_inicio"]) : 0;
        }

        // ═══════════════════════════════════════════════════
        //  MÉTODOS DE GRILLA
        // ═══════════════════════════════════════════════════

        private void CargarGrillaSesiones()
        {
            gridSesiones.DataSource = CajaService.ObtenerTodasLasSesiones();
        }

        private void CargarGrillaRetiros()
        {
            if (_idSesionActual == null) return;
            gridRetiros.DataSource = CajaService.ObtenerRetirosDeSesion(_idSesionActual.Value);
        }

        // ═══════════════════════════════════════════════════
        //  ACCIONES — ABRIR CAJA
        // ═══════════════════════════════════════════════════

        private void Abrir_Boton_Click(object sender, EventArgs e)
        {
            AbrirCaja();
        }

        private void AbrirCaja()
        {
            if (!CajaService.TryParseMonto(txtMontoInicio.Text, out decimal monto)
                || !CajaService.MontoInicioEsValido(monto))
            {
                MessageBox.Show("Ingresá un monto de inicio válido mayor a $0.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoInicio.Focus();
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Abrís la caja con ${monto:N2} de inicio?",
                "Confirmar apertura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                CajaService.AbrirCaja(ID_CAJA, monto, _idUsuario);
                txtMontoInicio.Clear();
                RefrescarEstadoCaja();
                MessageBox.Show("Caja abierta correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la caja:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═══════════════════════════════════════════════════
        //  ACCIONES — CERRAR CAJA
        // ═══════════════════════════════════════════════════

        private void Cerrar_Boton_Click(object sender, EventArgs e)
        {
            CerrarCaja();
        }

        private void CerrarCaja()
        {
            if (_idSesionActual == null) return;

            if (!CajaService.TryParseMonto(txtMontoCierre.Text, out decimal monto))
            {
                MessageBox.Show("Ingresá el monto de cierre.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoCierre.Focus();
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Cerrás la caja con ${monto:N2}?",
                "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                CajaService.CerrarCaja(_idSesionActual.Value, monto);
                txtMontoCierre.Clear();
                RefrescarEstadoCaja();
                MessageBox.Show("Caja cerrada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar la caja:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═══════════════════════════════════════════════════
        //  ACCIONES — RETIRO DE EFECTIVO
        // ═══════════════════════════════════════════════════

        private void Retiro_Boton_Click(object sender, EventArgs e)
        {
            RegistrarRetiro();
        }

        private void RegistrarRetiro()
        {
            if (_idSesionActual == null) return;

            if (!CajaService.TryParseMonto(txtMontoRetiro.Text, out decimal monto)
                || !CajaService.MontoRetiroEsValido(monto))
            {
                MessageBox.Show("Ingresá un monto de retiro válido mayor a $0.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoRetiro.Focus();
                return;
            }

            if (!CajaService.MotivoEsValido(txtMotivo.Text))
            {
                MessageBox.Show("Ingresá el motivo del retiro.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMotivo.Focus();
                return;
            }

            decimal inicio     = ObtenerMontoInicio();
            decimal retiros    = CajaService.ObtenerTotalRetiros(_idSesionActual.Value);
            decimal disponible = CajaService.CalcularDisponible(inicio, 0, retiros);

            if (!CajaService.RetiroNoCierraEnNegativo(monto, disponible))
            {
                MessageBox.Show(
                    $"No hay suficiente efectivo.\nDisponible: ${disponible:N2}",
                    "Saldo insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Registrás un retiro de ${monto:N2}?\nMotivo: {txtMotivo.Text}",
                "Confirmar retiro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                CajaService.RegistrarRetiro(
                    _idSesionActual.Value, monto, txtMotivo.Text, _idUsuario);

                txtMontoRetiro.Clear();
                txtMotivo.Clear();
                ActualizarDisponible();
                CargarGrillaRetiros();
                MessageBox.Show("Retiro registrado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el retiro:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Volver_Boton_Click(object sender, EventArgs e) => this.Close();
    }
}
