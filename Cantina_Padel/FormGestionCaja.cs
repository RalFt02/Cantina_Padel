using System.Data;

namespace Cantina_Padel
{
    public partial class FormGestionCaja : Form
    {
        private int? _idSesionActual;
        private decimal _montoInicioActual; // monto de inicio de la sesión del usuario logueado
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

        /// <summary>Consulta si el usuario logueado tiene una caja abierta y actualiza toda la UI.</summary>
        private void RefrescarEstadoCaja()
        {
            DataRow? sesion = CajaService.ObtenerSesionAbierta(ID_CAJA, _idUsuario);

            if (sesion != null)
            {
                // La caja abierta es del usuario logueado
                _idSesionActual = Convert.ToInt32(sesion["id_sesion"]);
                MostrarCajaAbierta(sesion);
            }
            else
            {
                _idSesionActual = null;

                // ¿La tiene abierta otro usuario? Entonces se muestra bloqueada.
                DataRow? deOtro = CajaService.ObtenerSesionAbiertaDeCaja(ID_CAJA);
                if (deOtro != null)
                    MostrarCajaDeOtroUsuario(deOtro);
                else
                    MostrarCajaCerrada();
            }
        }

        /// <summary>
        /// La caja está abierta por otro usuario: se muestra quién y se bloquea
        /// abrir, cerrar y retirar. Solo quien la abrió puede cerrarla.
        /// </summary>
        private void MostrarCajaDeOtroUsuario(DataRow sesion)
        {
            _montoInicioActual      = 0;

            lblEstadoCaja.Text      = "● CAJA EN USO"; // corto para no pisar el label del cajero
            lblEstadoCaja.ForeColor = Color.FromArgb(245, 158, 11);
            lblCajero.Text          = "Cajero: " + sesion["cajero"].ToString();
            lblApertura.Text        = "Apertura: " +
                Convert.ToDateTime(sesion["fecha_apertura"]).ToString("dd/MM/yyyy HH:mm");
            lblMontoInicio.Text     = "";
            lblDisponible.Text      = "";
            LimpiarMontoCierre();

            Abrir_Boton.Enabled   = false;
            Cerrar_Boton.Enabled  = false;
            Retiro_Boton.Enabled  = false;

            gridRetiros.DataSource = null;
        }

        /// <summary>Actualiza los controles para mostrar que la caja está abierta.</summary>
        private void MostrarCajaAbierta(DataRow sesion)
        {
            _montoInicioActual      = Convert.ToDecimal(sesion["monto_inicio"]);

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
            _montoInicioActual      = 0;

            lblEstadoCaja.Text      = "● CAJA CERRADA";
            lblEstadoCaja.ForeColor = Color.IndianRed;
            lblCajero.Text          = "";
            lblApertura.Text        = "";
            lblMontoInicio.Text     = "";
            lblDisponible.Text      = "";
            LimpiarMontoCierre();

            Abrir_Boton.Enabled   = true;
            Cerrar_Boton.Enabled  = false;
            Retiro_Boton.Enabled  = false;

            gridRetiros.DataSource = null;
        }

        /// <summary>Calcula y muestra el efectivo disponible en caja.</summary>
        private void ActualizarDisponible()
        {
            if (_idSesionActual == null) return;

            decimal disponible = CajaService.CalcularEfectivoEsperado(
                _idSesionActual.Value, ObtenerMontoInicio());

            lblDisponible.Text      = $"Disponible: ${disponible:N2}";
            lblDisponible.ForeColor = disponible >= 0
                ? Color.FromArgb(163, 230, 53)
                : Color.IndianRed;

            lblMontoCierre.Text      = $"$ {disponible:N2}";
            lblMontoCierre.ForeColor = Color.White;

            decimal extracciones = CajaService.ObtenerTotalRetiros(_idSesionActual.Value);
            lblTotalExtracciones.Text      = $"$ {extracciones:N2}";
            lblTotalExtracciones.ForeColor = Color.White;
        }

        /// <summary>Deja el monto de cierre en su estado vacío ("Cerrada").</summary>
        private void LimpiarMontoCierre()
        {
            lblMontoCierre.Text      = "Cerrada";
            lblMontoCierre.ForeColor = Color.Gray;

            lblTotalExtracciones.Text      = "Cerrada";
            lblTotalExtracciones.ForeColor = Color.Gray;
        }

        private decimal ObtenerMontoInicio()
        {
            // Usa el valor guardado al cargar la sesión del usuario logueado (sin consultar de nuevo la base)
            return _idSesionActual == null ? 0 : _montoInicioActual;
        }

        // ═══════════════════════════════════════════════════
        //  MÉTODOS DE GRILLA
        // ═══════════════════════════════════════════════════

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

            decimal esperado = CajaService.CalcularEfectivoEsperado(
                _idSesionActual.Value, ObtenerMontoInicio());

            var confirm = MessageBox.Show(
                $"La caja se cerrará con ${esperado:N2} (inicio + ventas en efectivo - retiros).\n¿Confirmás el cierre?",
                "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                // Solo puede cerrarla el mismo usuario que la abrió
                decimal cierre = CajaService.CerrarCaja(_idSesionActual.Value, _idUsuario);
                RefrescarEstadoCaja();
                MessageBox.Show($"Caja cerrada con ${cierre:N2}.", "Éxito",
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

            decimal disponible = CajaService.CalcularEfectivoEsperado(
                _idSesionActual.Value, ObtenerMontoInicio());

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
