namespace Cantina_Padel
{
    partial class FormReportes
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
            pnlHeader = new Panel();
            lblTituloModulo = new Label();
            lblSubtitulo = new Label();
            btnVolver = new Button();
            pnlFiltros = new Panel();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnHoy = new Button();
            btnEsteMes = new Button();
            btnMesAnterior = new Button();
            btnHistorico = new Button();
            btnFiltrar = new Button();
            pnlMenu = new Panel();
            btnMenuVentas = new Button();
            btnMenuCaja = new Button();
            btnMenuCompras = new Button();
            pnlContenedor = new Panel();
            pnlReporteVentas = new Panel();
            pnlHeaderVentas = new Panel();
            lblTituloVentas = new Label();
            lblDescVentas = new Label();
            btnExportarVentasPdf = new Button();
            btnExportarVentasExcel = new Button();
            pnlKpisVentas = new FlowLayoutPanel();
            tabVentas = new TabControl();
            tabVentasDetalle = new TabPage();
            gridVentasDetalle = new DataGridView();
            tabVentasDiferenciacion = new TabPage();
            tlpCantinaPadel = new TableLayoutPanel();
            pnlMitadCantina = new Panel();
            lblHeaderTablaCantina = new Label();
            gridVentasCantina = new DataGridView();
            pnlMitadPadel = new Panel();
            lblHeaderTablaPadel = new Label();
            gridVentasPadel = new DataGridView();
            gridVentasDiferenciacion = new DataGridView();
            tabVentasComprobante = new TabPage();
            gridVentasComprobante = new DataGridView();
            tabVentasMedioPago = new TabPage();
            gridVentasMedioPago = new DataGridView();
            pnlReporteCaja = new Panel();
            pnlHeaderCaja = new Panel();
            lblTituloCaja = new Label();
            lblDescCaja = new Label();
            btnExportarCajaPdf = new Button();
            btnExportarCajaExcel = new Button();
            pnlKpisCaja = new FlowLayoutPanel();
            tabCaja = new TabControl();
            tabCajaSesiones = new TabPage();
            gridCajaSesiones = new DataGridView();
            tabCajaCuentaCorriente = new TabPage();
            gridCajaCuentaCorriente = new DataGridView();
            tabCajaRetiros = new TabPage();
            gridCajaRetiros = new DataGridView();
            pnlReporteCompras = new Panel();
            pnlHeaderCompras = new Panel();
            lblTituloCompras = new Label();
            lblDescCompras = new Label();
            btnExportarComprasPdf = new Button();
            btnExportarComprasExcel = new Button();
            pnlKpisCompras = new FlowLayoutPanel();
            tabCompras = new TabControl();
            tabComprasResumen = new TabPage();
            gridComprasResumen = new DataGridView();
            tabComprasDetalle = new TabPage();
            gridComprasDetalle = new DataGridView();
            pnlDetalleProductoLateral = new Panel();
            pnlHeaderDetalleProd = new Panel();
            lblTituloDetalleProd = new Label();
            lblSubDetalleProd = new Label();
            pnlCuerpoDetalleProd = new Panel();
            lblDetProductoValor = new Label();
            lblDetCodigoValor = new Label();
            lblDetCategoriaValor = new Label();
            lblDetMarcaValor = new Label();
            lblDetProveedorValor = new Label();
            lblDetCantidadValor = new Label();
            lblDetPrecioUnitValor = new Label();
            lblDetSubtotalValor = new Label();
            lblDetPrecioVentaValor = new Label();
            lblDetStockValor = new Label();
            lblDetGananciaValor = new Label();
            lblDetDescripcionValor = new Label();
            lblTituloItemsComp = new Label();
            gridItemsComprobante = new DataGridView();

            pnlHeader.SuspendLayout();
            this.BindingContext = new System.Windows.Forms.BindingContext();
            pnlFiltros.SuspendLayout();
            pnlMenu.SuspendLayout();
            pnlContenedor.SuspendLayout();
            pnlReporteVentas.SuspendLayout();
            pnlHeaderVentas.SuspendLayout();
            tabVentas.SuspendLayout();
            tabVentasDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridVentasDetalle).BeginInit();
            tabVentasDiferenciacion.SuspendLayout();
            tlpCantinaPadel.SuspendLayout();
            pnlMitadCantina.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridVentasCantina).BeginInit();
            pnlMitadPadel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridVentasPadel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridVentasDiferenciacion).BeginInit();
            tabVentasComprobante.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridVentasComprobante).BeginInit();
            tabVentasMedioPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridVentasMedioPago).BeginInit();
            pnlReporteCaja.SuspendLayout();
            pnlHeaderCaja.SuspendLayout();
            tabCaja.SuspendLayout();
            tabCajaSesiones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCajaSesiones).BeginInit();
            tabCajaCuentaCorriente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCajaCuentaCorriente).BeginInit();
            tabCajaRetiros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCajaRetiros).BeginInit();
            pnlReporteCompras.SuspendLayout();
            pnlHeaderCompras.SuspendLayout();
            tabCompras.SuspendLayout();
            tabComprasResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridComprasResumen).BeginInit();
            tabComprasDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridComprasDetalle).BeginInit();
            pnlDetalleProductoLateral.SuspendLayout();
            pnlHeaderDetalleProd.SuspendLayout();
            pnlCuerpoDetalleProd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridItemsComprobante).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            pnlHeader.Controls.Add(btnVolver);
            pnlHeader.Controls.Add(lblSubtitulo);
            pnlHeader.Controls.Add(lblTituloModulo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 10, 20, 10);
            pnlHeader.Size = new Size(1280, 68);
            pnlHeader.TabIndex = 0;
            // 
            // lblTituloModulo
            // 
            lblTituloModulo.AutoSize = true;
            lblTituloModulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTituloModulo.ForeColor = Color.FromArgb(163, 230, 53);
            lblTituloModulo.Location = new Point(18, 9);
            lblTituloModulo.Name = "lblTituloModulo";
            lblTituloModulo.Size = new Size(410, 28);
            lblTituloModulo.TabIndex = 0;
            lblTituloModulo.Text = "MÓDULO DE REPORTES Y ESTADÍSTICAS";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 9F);
            lblSubtitulo.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitulo.Location = new Point(20, 39);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(510, 15);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Cantina Padel Club | Horas y Facturación de Pádel vs. Cantina | Caja y Compras";
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.BackColor = Color.FromArgb(51, 65, 85);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(1110, 14);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(150, 40);
            btnVolver.TabIndex = 2;
            btnVolver.Text = "← Volver al Inicio";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(24, 34, 53);
            pnlFiltros.Controls.Add(btnHistorico);
            pnlFiltros.Controls.Add(btnMesAnterior);
            pnlFiltros.Controls.Add(btnEsteMes);
            pnlFiltros.Controls.Add(btnHoy);
            pnlFiltros.Controls.Add(btnFiltrar);
            pnlFiltros.Controls.Add(dtpHasta);
            pnlFiltros.Controls.Add(lblHasta);
            pnlFiltros.Controls.Add(dtpDesde);
            pnlFiltros.Controls.Add(lblDesde);
            pnlFiltros.Dock = DockStyle.Top;
            pnlFiltros.Location = new Point(0, 68);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Padding = new Padding(15, 8, 15, 8);
            pnlFiltros.Size = new Size(1280, 56);
            pnlFiltros.TabIndex = 1;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDesde.ForeColor = Color.FromArgb(203, 213, 225);
            lblDesde.Location = new Point(18, 19);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(78, 15);
            lblDesde.TabIndex = 0;
            lblDesde.Text = "Fecha Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.CalendarFont = new Font("Segoe UI", 9.5F);
            dtpDesde.Font = new Font("Segoe UI", 9.5F);
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(102, 14);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(125, 24);
            dtpDesde.TabIndex = 1;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHasta.ForeColor = Color.FromArgb(203, 213, 225);
            lblHasta.Location = new Point(245, 19);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(74, 15);
            lblHasta.TabIndex = 2;
            lblHasta.Text = "Fecha Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.CalendarFont = new Font("Segoe UI", 9.5F);
            dtpHasta.Font = new Font("Segoe UI", 9.5F);
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(325, 14);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(125, 24);
            dtpHasta.TabIndex = 3;
            // 
            // btnHoy
            // 
            btnHoy.BackColor = Color.FromArgb(51, 65, 85);
            btnHoy.Cursor = Cursors.Hand;
            btnHoy.FlatAppearance.BorderSize = 0;
            btnHoy.FlatStyle = FlatStyle.Flat;
            btnHoy.Font = new Font("Segoe UI", 8.5F);
            btnHoy.ForeColor = Color.White;
            btnHoy.Location = new Point(468, 13);
            btnHoy.Name = "btnHoy";
            btnHoy.Size = new Size(65, 28);
            btnHoy.TabIndex = 4;
            btnHoy.Text = "Hoy";
            btnHoy.UseVisualStyleBackColor = false;
            btnHoy.Click += btnHoy_Click;
            // 
            // btnEsteMes
            // 
            btnEsteMes.BackColor = Color.FromArgb(51, 65, 85);
            btnEsteMes.Cursor = Cursors.Hand;
            btnEsteMes.FlatAppearance.BorderSize = 0;
            btnEsteMes.FlatStyle = FlatStyle.Flat;
            btnEsteMes.Font = new Font("Segoe UI", 8.5F);
            btnEsteMes.ForeColor = Color.White;
            btnEsteMes.Location = new Point(539, 13);
            btnEsteMes.Name = "btnEsteMes";
            btnEsteMes.Size = new Size(80, 28);
            btnEsteMes.TabIndex = 5;
            btnEsteMes.Text = "Este Mes";
            btnEsteMes.UseVisualStyleBackColor = false;
            btnEsteMes.Click += btnEsteMes_Click;
            // 
            // btnMesAnterior
            // 
            btnMesAnterior.BackColor = Color.FromArgb(51, 65, 85);
            btnMesAnterior.Cursor = Cursors.Hand;
            btnMesAnterior.FlatAppearance.BorderSize = 0;
            btnMesAnterior.FlatStyle = FlatStyle.Flat;
            btnMesAnterior.Font = new Font("Segoe UI", 8.5F);
            btnMesAnterior.ForeColor = Color.White;
            btnMesAnterior.Location = new Point(625, 13);
            btnMesAnterior.Name = "btnMesAnterior";
            btnMesAnterior.Size = new Size(95, 28);
            btnMesAnterior.TabIndex = 6;
            btnMesAnterior.Text = "Mes Anterior";
            btnMesAnterior.UseVisualStyleBackColor = false;
            btnMesAnterior.Click += btnMesAnterior_Click;
            // 
            // btnHistorico
            // 
            btnHistorico.BackColor = Color.FromArgb(51, 65, 85);
            btnHistorico.Cursor = Cursors.Hand;
            btnHistorico.FlatAppearance.BorderSize = 0;
            btnHistorico.FlatStyle = FlatStyle.Flat;
            btnHistorico.Font = new Font("Segoe UI", 8.5F);
            btnHistorico.ForeColor = Color.White;
            btnHistorico.Location = new Point(726, 13);
            btnHistorico.Name = "btnHistorico";
            btnHistorico.Size = new Size(110, 28);
            btnHistorico.TabIndex = 7;
            btnHistorico.Text = "Histórico (Todo)";
            btnHistorico.UseVisualStyleBackColor = false;
            btnHistorico.Click += btnHistorico_Click;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFiltrar.BackColor = Color.FromArgb(163, 230, 53);
            btnFiltrar.Cursor = Cursors.Hand;
            btnFiltrar.FlatAppearance.BorderSize = 0;
            btnFiltrar.FlatStyle = FlatStyle.Flat;
            btnFiltrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnFiltrar.ForeColor = Color.Black;
            btnFiltrar.Location = new Point(1090, 11);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(170, 32);
            btnFiltrar.TabIndex = 8;
            btnFiltrar.Text = "🔍 Actualizar Reportes";
            btnFiltrar.UseVisualStyleBackColor = false;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(15, 23, 42);
            pnlMenu.Controls.Add(btnMenuCompras);
            pnlMenu.Controls.Add(btnMenuCaja);
            pnlMenu.Controls.Add(btnMenuVentas);
            pnlMenu.Dock = DockStyle.Top;
            pnlMenu.Location = new Point(0, 124);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Padding = new Padding(15, 6, 15, 6);
            pnlMenu.Size = new Size(1280, 50);
            pnlMenu.TabIndex = 2;
            // 
            // btnMenuVentas
            // 
            btnMenuVentas.BackColor = Color.FromArgb(163, 230, 53);
            btnMenuVentas.Cursor = Cursors.Hand;
            btnMenuVentas.FlatAppearance.BorderSize = 0;
            btnMenuVentas.FlatStyle = FlatStyle.Flat;
            btnMenuVentas.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnMenuVentas.ForeColor = Color.Black;
            btnMenuVentas.Location = new Point(18, 6);
            btnMenuVentas.Name = "btnMenuVentas";
            btnMenuVentas.Size = new Size(240, 38);
            btnMenuVentas.TabIndex = 0;
            btnMenuVentas.Text = "📊 Reportes de ventas";
            btnMenuVentas.UseVisualStyleBackColor = false;
            btnMenuVentas.Click += btnMenuVentas_Click;
            // 
            // btnMenuCaja
            // 
            btnMenuCaja.BackColor = Color.FromArgb(51, 65, 85);
            btnMenuCaja.Cursor = Cursors.Hand;
            btnMenuCaja.FlatAppearance.BorderSize = 0;
            btnMenuCaja.FlatStyle = FlatStyle.Flat;
            btnMenuCaja.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnMenuCaja.ForeColor = Color.White;
            btnMenuCaja.Location = new Point(270, 6);
            btnMenuCaja.Name = "btnMenuCaja";
            btnMenuCaja.Size = new Size(240, 38);
            btnMenuCaja.TabIndex = 1;
            btnMenuCaja.Text = "💵 Reportes de caja";
            btnMenuCaja.UseVisualStyleBackColor = false;
            btnMenuCaja.Click += btnMenuCaja_Click;
            // 
            // btnMenuCompras
            // 
            btnMenuCompras.BackColor = Color.FromArgb(51, 65, 85);
            btnMenuCompras.Cursor = Cursors.Hand;
            btnMenuCompras.FlatAppearance.BorderSize = 0;
            btnMenuCompras.FlatStyle = FlatStyle.Flat;
            btnMenuCompras.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnMenuCompras.ForeColor = Color.White;
            btnMenuCompras.Location = new Point(522, 6);
            btnMenuCompras.Name = "btnMenuCompras";
            btnMenuCompras.Size = new Size(240, 38);
            btnMenuCompras.TabIndex = 2;
            btnMenuCompras.Text = "📦 Reportes de compras";
            btnMenuCompras.UseVisualStyleBackColor = false;
            btnMenuCompras.Click += btnMenuCompras_Click;
            // 
            // pnlContenedor
            // 
            pnlContenedor.BackColor = Color.FromArgb(15, 23, 42);
            pnlContenedor.Controls.Add(pnlReporteVentas);
            pnlContenedor.Controls.Add(pnlReporteCaja);
            pnlContenedor.Controls.Add(pnlReporteCompras);
            pnlContenedor.Controls.Add(pnlDetalleProductoLateral);
            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.Location = new Point(0, 174);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Padding = new Padding(15, 5, 15, 15);
            pnlContenedor.Size = new Size(1280, 546);
            pnlContenedor.TabIndex = 3;
            // 
            // pnlDetalleProductoLateral (Ventana pequeña en un costado para ver detalles de producto)
            // 
            pnlDetalleProductoLateral.BackColor = Color.FromArgb(24, 34, 53);
            pnlDetalleProductoLateral.Controls.Add(pnlCuerpoDetalleProd);
            pnlDetalleProductoLateral.Controls.Add(pnlHeaderDetalleProd);
            pnlDetalleProductoLateral.Dock = DockStyle.Right;
            pnlDetalleProductoLateral.Location = new Point(960, 5);
            pnlDetalleProductoLateral.Name = "pnlDetalleProductoLateral";
            pnlDetalleProductoLateral.Padding = new Padding(6, 0, 0, 0);
            pnlDetalleProductoLateral.Size = new Size(305, 526);
            pnlDetalleProductoLateral.TabIndex = 4;
            // 
            // pnlHeaderDetalleProd
            // 
            pnlHeaderDetalleProd.BackColor = Color.FromArgb(51, 65, 85);
            pnlHeaderDetalleProd.Controls.Add(lblSubDetalleProd);
            pnlHeaderDetalleProd.Controls.Add(lblTituloDetalleProd);
            pnlHeaderDetalleProd.Dock = DockStyle.Top;
            pnlHeaderDetalleProd.Location = new Point(6, 0);
            pnlHeaderDetalleProd.Name = "pnlHeaderDetalleProd";
            pnlHeaderDetalleProd.Padding = new Padding(10, 6, 10, 6);
            pnlHeaderDetalleProd.Size = new Size(299, 52);
            pnlHeaderDetalleProd.TabIndex = 0;
            // 
            // lblTituloDetalleProd
            // 
            lblTituloDetalleProd.AutoSize = true;
            lblTituloDetalleProd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTituloDetalleProd.ForeColor = Color.FromArgb(163, 230, 53);
            lblTituloDetalleProd.Location = new Point(8, 6);
            lblTituloDetalleProd.Name = "lblTituloDetalleProd";
            lblTituloDetalleProd.Size = new Size(195, 19);
            lblTituloDetalleProd.TabIndex = 0;
            lblTituloDetalleProd.Text = "🔎 DETALLE DE PRODUCTO";
            // 
            // lblSubDetalleProd
            // 
            lblSubDetalleProd.AutoSize = true;
            lblSubDetalleProd.Font = new Font("Segoe UI", 8F);
            lblSubDetalleProd.ForeColor = Color.FromArgb(203, 213, 225);
            lblSubDetalleProd.Location = new Point(9, 27);
            lblSubDetalleProd.Name = "lblSubDetalleProd";
            lblSubDetalleProd.Size = new Size(220, 13);
            lblSubDetalleProd.TabIndex = 1;
            lblSubDetalleProd.Text = "Seleccione una fila para ver sus productos";
            // 
            // pnlCuerpoDetalleProd
            // 
            pnlCuerpoDetalleProd.BackColor = Color.FromArgb(30, 41, 59);
            pnlCuerpoDetalleProd.Controls.Add(gridItemsComprobante);
            pnlCuerpoDetalleProd.Controls.Add(lblTituloItemsComp);
            pnlCuerpoDetalleProd.Controls.Add(lblDetDescripcionValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetGananciaValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetStockValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetPrecioVentaValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetSubtotalValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetPrecioUnitValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetCantidadValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetProveedorValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetMarcaValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetCategoriaValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetCodigoValor);
            pnlCuerpoDetalleProd.Controls.Add(lblDetProductoValor);
            pnlCuerpoDetalleProd.Dock = DockStyle.Fill;
            pnlCuerpoDetalleProd.Location = new Point(6, 52);
            pnlCuerpoDetalleProd.Name = "pnlCuerpoDetalleProd";
            pnlCuerpoDetalleProd.Padding = new Padding(10);
            pnlCuerpoDetalleProd.Size = new Size(299, 474);
            pnlCuerpoDetalleProd.TabIndex = 1;
            // 
            // Controles de Ficha de Producto en Ventana Lateral
            // 
            lblDetProductoValor.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDetProductoValor.ForeColor = Color.FromArgb(163, 230, 53);
            lblDetProductoValor.Location = new Point(10, 8);
            lblDetProductoValor.Size = new Size(279, 20);
            lblDetProductoValor.Text = "Producto: -";
            lblDetProductoValor.AutoEllipsis = true;

            lblDetCodigoValor.Font = new Font("Segoe UI", 8.5F);
            lblDetCodigoValor.ForeColor = Color.White;
            lblDetCodigoValor.Location = new Point(10, 30);
            lblDetCodigoValor.Size = new Size(279, 18);
            lblDetCodigoValor.Text = "Código: -";

            lblDetCategoriaValor.Font = new Font("Segoe UI", 8.5F);
            lblDetCategoriaValor.ForeColor = Color.White;
            lblDetCategoriaValor.Location = new Point(10, 50);
            lblDetCategoriaValor.Size = new Size(279, 18);
            lblDetCategoriaValor.Text = "Categoría: -";

            lblDetMarcaValor.Font = new Font("Segoe UI", 8.5F);
            lblDetMarcaValor.ForeColor = Color.White;
            lblDetMarcaValor.Location = new Point(10, 70);
            lblDetMarcaValor.Size = new Size(279, 18);
            lblDetMarcaValor.Text = "Marca: -";

            lblDetProveedorValor.Font = new Font("Segoe UI", 8.5F);
            lblDetProveedorValor.ForeColor = Color.White;
            lblDetProveedorValor.Location = new Point(10, 90);
            lblDetProveedorValor.Size = new Size(279, 18);
            lblDetProveedorValor.Text = "Proveedor: -";
            lblDetProveedorValor.AutoEllipsis = true;

            lblDetCantidadValor.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDetCantidadValor.ForeColor = Color.FromArgb(56, 189, 248);
            lblDetCantidadValor.Location = new Point(10, 112);
            lblDetCantidadValor.Size = new Size(279, 18);
            lblDetCantidadValor.Text = "Cantidad en operación: -";

            lblDetPrecioUnitValor.Font = new Font("Segoe UI", 8.5F);
            lblDetPrecioUnitValor.ForeColor = Color.White;
            lblDetPrecioUnitValor.Location = new Point(10, 132);
            lblDetPrecioUnitValor.Size = new Size(279, 18);
            lblDetPrecioUnitValor.Text = "Precio / Costo Unit.: -";

            lblDetSubtotalValor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDetSubtotalValor.ForeColor = Color.FromArgb(250, 204, 21);
            lblDetSubtotalValor.Location = new Point(10, 152);
            lblDetSubtotalValor.Size = new Size(279, 18);
            lblDetSubtotalValor.Text = "Subtotal: -";

            lblDetPrecioVentaValor.Font = new Font("Segoe UI", 8.5F);
            lblDetPrecioVentaValor.ForeColor = Color.FromArgb(203, 213, 225);
            lblDetPrecioVentaValor.Location = new Point(10, 174);
            lblDetPrecioVentaValor.Size = new Size(279, 18);
            lblDetPrecioVentaValor.Text = "Precio Venta Actual: -";

            lblDetStockValor.Font = new Font("Segoe UI", 8.5F);
            lblDetStockValor.ForeColor = Color.FromArgb(203, 213, 225);
            lblDetStockValor.Location = new Point(10, 194);
            lblDetStockValor.Size = new Size(279, 18);
            lblDetStockValor.Text = "Stock Disponible: -";

            lblDetGananciaValor.Font = new Font("Segoe UI", 8.5F);
            lblDetGananciaValor.ForeColor = Color.FromArgb(203, 213, 225);
            lblDetGananciaValor.Location = new Point(10, 214);
            lblDetGananciaValor.Size = new Size(279, 18);
            lblDetGananciaValor.Text = "% Ganancia Rubro: -";

            lblDetDescripcionValor.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            lblDetDescripcionValor.ForeColor = Color.FromArgb(148, 163, 184);
            lblDetDescripcionValor.Location = new Point(10, 234);
            lblDetDescripcionValor.Size = new Size(279, 32);
            lblDetDescripcionValor.Text = "Detalle: -";
            lblDetDescripcionValor.AutoEllipsis = true;

            lblTituloItemsComp.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblTituloItemsComp.ForeColor = Color.FromArgb(163, 230, 53);
            lblTituloItemsComp.Location = new Point(10, 268);
            lblTituloItemsComp.Size = new Size(279, 18);
            lblTituloItemsComp.Text = "Ítems del comprobante (clic para ver):";

            gridItemsComprobante.AllowUserToAddRows = false;
            gridItemsComprobante.AllowUserToDeleteRows = false;
            gridItemsComprobante.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridItemsComprobante.BackgroundColor = Color.FromArgb(15, 23, 42);
            gridItemsComprobante.BorderStyle = BorderStyle.None;
            gridItemsComprobante.Location = new Point(10, 288);
            gridItemsComprobante.Name = "gridItemsComprobante";
            gridItemsComprobante.ReadOnly = true;
            gridItemsComprobante.RowHeadersVisible = false;
            gridItemsComprobante.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItemsComprobante.Size = new Size(279, 176);
            gridItemsComprobante.TabIndex = 12;
            // 
            // pnlReporteVentas
            // 
            pnlReporteVentas.Controls.Add(tabVentas);
            pnlReporteVentas.Controls.Add(pnlKpisVentas);
            pnlReporteVentas.Controls.Add(pnlHeaderVentas);
            pnlReporteVentas.Dock = DockStyle.Fill;
            pnlReporteVentas.Location = new Point(15, 5);
            pnlReporteVentas.Name = "pnlReporteVentas";
            pnlReporteVentas.Size = new Size(945, 526);
            pnlReporteVentas.TabIndex = 0;
            // 
            // pnlHeaderVentas
            // 
            pnlHeaderVentas.BackColor = Color.FromArgb(30, 41, 59);
            pnlHeaderVentas.Controls.Add(btnExportarVentasPdf);
            pnlHeaderVentas.Controls.Add(btnExportarVentasExcel);
            pnlHeaderVentas.Controls.Add(lblDescVentas);
            pnlHeaderVentas.Controls.Add(lblTituloVentas);
            pnlHeaderVentas.Dock = DockStyle.Top;
            pnlHeaderVentas.Location = new Point(0, 0);
            pnlHeaderVentas.Name = "pnlHeaderVentas";
            pnlHeaderVentas.Padding = new Padding(15, 10, 15, 10);
            pnlHeaderVentas.Size = new Size(945, 58);
            pnlHeaderVentas.TabIndex = 0;
            // 
            // lblTituloVentas
            // 
            lblTituloVentas.AutoSize = true;
            lblTituloVentas.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblTituloVentas.ForeColor = Color.FromArgb(163, 230, 53);
            lblTituloVentas.Location = new Point(12, 8);
            lblTituloVentas.Name = "lblTituloVentas";
            lblTituloVentas.Size = new Size(160, 23);
            lblTituloVentas.TabIndex = 0;
            lblTituloVentas.Text = "Reportes de ventas";
            // 
            // lblDescVentas
            // 
            lblDescVentas.AutoSize = true;
            lblDescVentas.Font = new Font("Segoe UI", 8.5F);
            lblDescVentas.ForeColor = Color.FromArgb(148, 163, 184);
            lblDescVentas.Location = new Point(14, 32);
            lblDescVentas.Name = "lblDescVentas";
            lblDescVentas.Size = new Size(460, 15);
            lblDescVentas.TabIndex = 1;
            lblDescVentas.Text = "Cantidad de horas de pádel y facturación del día | División Cantina vs. Pádel";
            // 
            // btnExportarVentasPdf
            // 
            btnExportarVentasPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarVentasPdf.BackColor = Color.FromArgb(239, 68, 68);
            btnExportarVentasPdf.Cursor = Cursors.Hand;
            btnExportarVentasPdf.FlatAppearance.BorderSize = 0;
            btnExportarVentasPdf.FlatStyle = FlatStyle.Flat;
            btnExportarVentasPdf.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportarVentasPdf.ForeColor = Color.White;
            btnExportarVentasPdf.Location = new Point(790, 12);
            btnExportarVentasPdf.Name = "btnExportarVentasPdf";
            btnExportarVentasPdf.Size = new Size(140, 34);
            btnExportarVentasPdf.TabIndex = 3;
            btnExportarVentasPdf.Text = "📕 Exportar a PDF";
            btnExportarVentasPdf.UseVisualStyleBackColor = false;
            btnExportarVentasPdf.Click += btnExportarVentasPdf_Click;
            // 
            // btnExportarVentasExcel
            // 
            btnExportarVentasExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarVentasExcel.BackColor = Color.FromArgb(34, 197, 94);
            btnExportarVentasExcel.Cursor = Cursors.Hand;
            btnExportarVentasExcel.FlatAppearance.BorderSize = 0;
            btnExportarVentasExcel.FlatStyle = FlatStyle.Flat;
            btnExportarVentasExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportarVentasExcel.ForeColor = Color.White;
            btnExportarVentasExcel.Location = new Point(635, 12);
            btnExportarVentasExcel.Name = "btnExportarVentasExcel";
            btnExportarVentasExcel.Size = new Size(145, 34);
            btnExportarVentasExcel.TabIndex = 2;
            btnExportarVentasExcel.Text = "📗 Exportar a Excel";
            btnExportarVentasExcel.UseVisualStyleBackColor = false;
            btnExportarVentasExcel.Click += btnExportarVentasExcel_Click;
            // 
            // pnlKpisVentas
            // 
            pnlKpisVentas.BackColor = Color.FromArgb(15, 23, 42);
            pnlKpisVentas.Dock = DockStyle.Top;
            pnlKpisVentas.Location = new Point(0, 58);
            pnlKpisVentas.Name = "pnlKpisVentas";
            pnlKpisVentas.Padding = new Padding(0, 6, 0, 6);
            pnlKpisVentas.Size = new Size(945, 72);
            pnlKpisVentas.TabIndex = 1;
            // 
            // tabVentas
            // 
            tabVentas.Controls.Add(tabVentasDetalle);
            tabVentas.Controls.Add(tabVentasDiferenciacion);
            tabVentas.Controls.Add(tabVentasComprobante);
            tabVentas.Controls.Add(tabVentasMedioPago);
            tabVentas.Dock = DockStyle.Fill;
            tabVentas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tabVentas.Location = new Point(0, 130);
            tabVentas.Name = "tabVentas";
            tabVentas.SelectedIndex = 0;
            tabVentas.Size = new Size(945, 396);
            tabVentas.TabIndex = 2;
            // 
            // tabVentasDetalle
            // 
            tabVentasDetalle.BackColor = Color.FromArgb(15, 23, 42);
            tabVentasDetalle.Controls.Add(gridVentasDetalle);
            tabVentasDetalle.Location = new Point(4, 24);
            tabVentasDetalle.Name = "tabVentasDetalle";
            tabVentasDetalle.Padding = new Padding(3);
            tabVentasDetalle.Size = new Size(937, 368);
            tabVentasDetalle.TabIndex = 0;
            tabVentasDetalle.Text = "📋 Listado de Ventas";
            // 
            // gridVentasDetalle
            // 
            gridVentasDetalle.AllowUserToAddRows = false;
            gridVentasDetalle.AllowUserToDeleteRows = false;
            gridVentasDetalle.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridVentasDetalle.BorderStyle = BorderStyle.None;
            gridVentasDetalle.Dock = DockStyle.Fill;
            gridVentasDetalle.Location = new Point(3, 3);
            gridVentasDetalle.Name = "gridVentasDetalle";
            gridVentasDetalle.ReadOnly = true;
            gridVentasDetalle.RowHeadersVisible = false;
            gridVentasDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVentasDetalle.Size = new Size(931, 362);
            gridVentasDetalle.TabIndex = 0;
            // 
            // tabVentasDiferenciacion (Dividido en dos tablas lado a lado: Cantina y Pádel)
            // 
            tabVentasDiferenciacion.BackColor = Color.FromArgb(15, 23, 42);
            tabVentasDiferenciacion.Controls.Add(tlpCantinaPadel);
            tabVentasDiferenciacion.Location = new Point(4, 24);
            tabVentasDiferenciacion.Name = "tabVentasDiferenciacion";
            tabVentasDiferenciacion.Padding = new Padding(3);
            tabVentasDiferenciacion.Size = new Size(937, 368);
            tabVentasDiferenciacion.TabIndex = 1;
            tabVentasDiferenciacion.Text = "🍔 Cantina vs. 🎾 Pádel";
            // 
            // tlpCantinaPadel
            // 
            tlpCantinaPadel.ColumnCount = 2;
            tlpCantinaPadel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCantinaPadel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCantinaPadel.Controls.Add(pnlMitadCantina, 0, 0);
            tlpCantinaPadel.Controls.Add(pnlMitadPadel, 1, 0);
            tlpCantinaPadel.Dock = DockStyle.Fill;
            tlpCantinaPadel.Location = new Point(3, 3);
            tlpCantinaPadel.Name = "tlpCantinaPadel";
            tlpCantinaPadel.RowCount = 1;
            tlpCantinaPadel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCantinaPadel.Size = new Size(931, 362);
            tlpCantinaPadel.TabIndex = 0;
            // 
            // pnlMitadCantina
            // 
            pnlMitadCantina.BackColor = Color.FromArgb(24, 34, 53);
            pnlMitadCantina.Controls.Add(gridVentasCantina);
            pnlMitadCantina.Controls.Add(lblHeaderTablaCantina);
            pnlMitadCantina.Dock = DockStyle.Fill;
            pnlMitadCantina.Location = new Point(3, 3);
            pnlMitadCantina.Name = "pnlMitadCantina";
            pnlMitadCantina.Padding = new Padding(2);
            pnlMitadCantina.Size = new Size(459, 356);
            pnlMitadCantina.TabIndex = 0;
            // 
            // lblHeaderTablaCantina
            // 
            lblHeaderTablaCantina.BackColor = Color.FromArgb(30, 58, 95);
            lblHeaderTablaCantina.Dock = DockStyle.Top;
            lblHeaderTablaCantina.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHeaderTablaCantina.ForeColor = Color.FromArgb(56, 189, 248);
            lblHeaderTablaCantina.Location = new Point(2, 2);
            lblHeaderTablaCantina.Name = "lblHeaderTablaCantina";
            lblHeaderTablaCantina.Padding = new Padding(8, 0, 0, 0);
            lblHeaderTablaCantina.Size = new Size(455, 28);
            lblHeaderTablaCantina.TabIndex = 0;
            lblHeaderTablaCantina.Text = "🍔 TABLA CANTINA (Productos)";
            lblHeaderTablaCantina.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gridVentasCantina
            // 
            gridVentasCantina.AllowUserToAddRows = false;
            gridVentasCantina.AllowUserToDeleteRows = false;
            gridVentasCantina.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridVentasCantina.BorderStyle = BorderStyle.None;
            gridVentasCantina.Dock = DockStyle.Fill;
            gridVentasCantina.Location = new Point(2, 30);
            gridVentasCantina.Name = "gridVentasCantina";
            gridVentasCantina.ReadOnly = true;
            gridVentasCantina.RowHeadersVisible = false;
            gridVentasCantina.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVentasCantina.Size = new Size(455, 324);
            gridVentasCantina.TabIndex = 1;
            // 
            // pnlMitadPadel
            // 
            pnlMitadPadel.BackColor = Color.FromArgb(24, 34, 53);
            pnlMitadPadel.Controls.Add(gridVentasPadel);
            pnlMitadPadel.Controls.Add(lblHeaderTablaPadel);
            pnlMitadPadel.Dock = DockStyle.Fill;
            pnlMitadPadel.Location = new Point(468, 3);
            pnlMitadPadel.Name = "pnlMitadPadel";
            pnlMitadPadel.Padding = new Padding(2);
            pnlMitadPadel.Size = new Size(460, 356);
            pnlMitadPadel.TabIndex = 1;
            // 
            // lblHeaderTablaPadel
            // 
            lblHeaderTablaPadel.BackColor = Color.FromArgb(36, 59, 45);
            lblHeaderTablaPadel.Dock = DockStyle.Top;
            lblHeaderTablaPadel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHeaderTablaPadel.ForeColor = Color.FromArgb(163, 230, 53);
            lblHeaderTablaPadel.Location = new Point(2, 2);
            lblHeaderTablaPadel.Name = "lblHeaderTablaPadel";
            lblHeaderTablaPadel.Padding = new Padding(8, 0, 0, 0);
            lblHeaderTablaPadel.Size = new Size(456, 28);
            lblHeaderTablaPadel.TabIndex = 0;
            lblHeaderTablaPadel.Text = "🎾 TABLA PÁDEL (Horas y Facturación)";
            lblHeaderTablaPadel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gridVentasPadel
            // 
            gridVentasPadel.AllowUserToAddRows = false;
            gridVentasPadel.AllowUserToDeleteRows = false;
            gridVentasPadel.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridVentasPadel.BorderStyle = BorderStyle.None;
            gridVentasPadel.Dock = DockStyle.Fill;
            gridVentasPadel.Location = new Point(2, 30);
            gridVentasPadel.Name = "gridVentasPadel";
            gridVentasPadel.ReadOnly = true;
            gridVentasPadel.RowHeadersVisible = false;
            gridVentasPadel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVentasPadel.Size = new Size(456, 324);
            gridVentasPadel.TabIndex = 1;
            // 
            // tabVentasComprobante
            // 
            tabVentasComprobante.BackColor = Color.FromArgb(15, 23, 42);
            tabVentasComprobante.Controls.Add(gridVentasComprobante);
            tabVentasComprobante.Location = new Point(4, 24);
            tabVentasComprobante.Name = "tabVentasComprobante";
            tabVentasComprobante.Padding = new Padding(3);
            tabVentasComprobante.Size = new Size(937, 368);
            tabVentasComprobante.TabIndex = 2;
            tabVentasComprobante.Text = "🧾 Por Tipo de Comprobante";
            // 
            // gridVentasComprobante
            // 
            gridVentasComprobante.AllowUserToAddRows = false;
            gridVentasComprobante.AllowUserToDeleteRows = false;
            gridVentasComprobante.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridVentasComprobante.BorderStyle = BorderStyle.None;
            gridVentasComprobante.Dock = DockStyle.Fill;
            gridVentasComprobante.Location = new Point(3, 3);
            gridVentasComprobante.Name = "gridVentasComprobante";
            gridVentasComprobante.ReadOnly = true;
            gridVentasComprobante.RowHeadersVisible = false;
            gridVentasComprobante.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVentasComprobante.Size = new Size(931, 362);
            gridVentasComprobante.TabIndex = 0;
            // 
            // tabVentasMedioPago
            // 
            tabVentasMedioPago.BackColor = Color.FromArgb(15, 23, 42);
            tabVentasMedioPago.Controls.Add(gridVentasMedioPago);
            tabVentasMedioPago.Location = new Point(4, 24);
            tabVentasMedioPago.Name = "tabVentasMedioPago";
            tabVentasMedioPago.Padding = new Padding(3);
            tabVentasMedioPago.Size = new Size(937, 368);
            tabVentasMedioPago.TabIndex = 3;
            tabVentasMedioPago.Text = "💳 Por Medio de Pago";
            // 
            // gridVentasMedioPago
            // 
            gridVentasMedioPago.AllowUserToAddRows = false;
            gridVentasMedioPago.AllowUserToDeleteRows = false;
            gridVentasMedioPago.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridVentasMedioPago.BorderStyle = BorderStyle.None;
            gridVentasMedioPago.Dock = DockStyle.Fill;
            gridVentasMedioPago.Location = new Point(3, 3);
            gridVentasMedioPago.Name = "gridVentasMedioPago";
            gridVentasMedioPago.ReadOnly = true;
            gridVentasMedioPago.RowHeadersVisible = false;
            gridVentasMedioPago.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridVentasMedioPago.Size = new Size(931, 362);
            gridVentasMedioPago.TabIndex = 0;
            // 
            // pnlReporteCaja
            // 
            pnlReporteCaja.Controls.Add(tabCaja);
            pnlReporteCaja.Controls.Add(pnlKpisCaja);
            pnlReporteCaja.Controls.Add(pnlHeaderCaja);
            pnlReporteCaja.Dock = DockStyle.Fill;
            pnlReporteCaja.Location = new Point(15, 5);
            pnlReporteCaja.Name = "pnlReporteCaja";
            pnlReporteCaja.Size = new Size(945, 526);
            pnlReporteCaja.TabIndex = 1;
            pnlReporteCaja.Visible = false;
            // 
            // pnlHeaderCaja
            // 
            pnlHeaderCaja.BackColor = Color.FromArgb(30, 41, 59);
            pnlHeaderCaja.Controls.Add(btnExportarCajaPdf);
            pnlHeaderCaja.Controls.Add(btnExportarCajaExcel);
            pnlHeaderCaja.Controls.Add(lblDescCaja);
            pnlHeaderCaja.Controls.Add(lblTituloCaja);
            pnlHeaderCaja.Dock = DockStyle.Top;
            pnlHeaderCaja.Location = new Point(0, 0);
            pnlHeaderCaja.Name = "pnlHeaderCaja";
            pnlHeaderCaja.Padding = new Padding(15, 10, 15, 10);
            pnlHeaderCaja.Size = new Size(945, 58);
            pnlHeaderCaja.TabIndex = 0;
            // 
            // lblTituloCaja
            // 
            lblTituloCaja.AutoSize = true;
            lblTituloCaja.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblTituloCaja.ForeColor = Color.FromArgb(163, 230, 53);
            lblTituloCaja.Location = new Point(12, 8);
            lblTituloCaja.Name = "lblTituloCaja";
            lblTituloCaja.Size = new Size(141, 23);
            lblTituloCaja.TabIndex = 0;
            lblTituloCaja.Text = "Reportes de caja";
            // 
            // lblDescCaja
            // 
            lblDescCaja.AutoSize = true;
            lblDescCaja.Font = new Font("Segoe UI", 8.5F);
            lblDescCaja.ForeColor = Color.FromArgb(148, 163, 184);
            lblDescCaja.Location = new Point(14, 32);
            lblDescCaja.Name = "lblDescCaja";
            lblDescCaja.Size = new Size(495, 15);
            lblDescCaja.TabIndex = 1;
            lblDescCaja.Text = "Sesiones de caja, desglose por medio de pago (Efectivo, Tarjetas, Billetera) y Fiado en Cta. Cte.";
            // 
            // btnExportarCajaPdf
            // 
            btnExportarCajaPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarCajaPdf.BackColor = Color.FromArgb(239, 68, 68);
            btnExportarCajaPdf.Cursor = Cursors.Hand;
            btnExportarCajaPdf.FlatAppearance.BorderSize = 0;
            btnExportarCajaPdf.FlatStyle = FlatStyle.Flat;
            btnExportarCajaPdf.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportarCajaPdf.ForeColor = Color.White;
            btnExportarCajaPdf.Location = new Point(790, 12);
            btnExportarCajaPdf.Name = "btnExportarCajaPdf";
            btnExportarCajaPdf.Size = new Size(140, 34);
            btnExportarCajaPdf.TabIndex = 3;
            btnExportarCajaPdf.Text = "📕 Exportar a PDF";
            btnExportarCajaPdf.UseVisualStyleBackColor = false;
            btnExportarCajaPdf.Click += btnExportarCajaPdf_Click;
            // 
            // btnExportarCajaExcel
            // 
            btnExportarCajaExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarCajaExcel.BackColor = Color.FromArgb(34, 197, 94);
            btnExportarCajaExcel.Cursor = Cursors.Hand;
            btnExportarCajaExcel.FlatAppearance.BorderSize = 0;
            btnExportarCajaExcel.FlatStyle = FlatStyle.Flat;
            btnExportarCajaExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportarCajaExcel.ForeColor = Color.White;
            btnExportarCajaExcel.Location = new Point(635, 12);
            btnExportarCajaExcel.Name = "btnExportarCajaExcel";
            btnExportarCajaExcel.Size = new Size(145, 34);
            btnExportarCajaExcel.TabIndex = 2;
            btnExportarCajaExcel.Text = "📗 Exportar a Excel";
            btnExportarCajaExcel.UseVisualStyleBackColor = false;
            btnExportarCajaExcel.Click += btnExportarCajaExcel_Click;
            // 
            // pnlKpisCaja
            // 
            pnlKpisCaja.BackColor = Color.FromArgb(15, 23, 42);
            pnlKpisCaja.Dock = DockStyle.Top;
            pnlKpisCaja.Location = new Point(0, 58);
            pnlKpisCaja.Name = "pnlKpisCaja";
            pnlKpisCaja.Padding = new Padding(0, 6, 0, 6);
            pnlKpisCaja.Size = new Size(945, 72);
            pnlKpisCaja.TabIndex = 1;
            // 
            // tabCaja
            // 
            tabCaja.Controls.Add(tabCajaSesiones);
            tabCaja.Controls.Add(tabCajaCuentaCorriente);
            tabCaja.Controls.Add(tabCajaRetiros);
            tabCaja.Dock = DockStyle.Fill;
            tabCaja.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tabCaja.Location = new Point(0, 130);
            tabCaja.Name = "tabCaja";
            tabCaja.SelectedIndex = 0;
            tabCaja.Size = new Size(945, 396);
            tabCaja.TabIndex = 2;
            // 
            // tabCajaSesiones
            // 
            tabCajaSesiones.BackColor = Color.FromArgb(15, 23, 42);
            tabCajaSesiones.Controls.Add(gridCajaSesiones);
            tabCajaSesiones.Location = new Point(4, 24);
            tabCajaSesiones.Name = "tabCajaSesiones";
            tabCajaSesiones.Padding = new Padding(3);
            tabCajaSesiones.Size = new Size(937, 368);
            tabCajaSesiones.TabIndex = 0;
            tabCajaSesiones.Text = "📋 Sesiones de Caja";
            // 
            // gridCajaSesiones
            // 
            gridCajaSesiones.AllowUserToAddRows = false;
            gridCajaSesiones.AllowUserToDeleteRows = false;
            gridCajaSesiones.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridCajaSesiones.BorderStyle = BorderStyle.None;
            gridCajaSesiones.Dock = DockStyle.Fill;
            gridCajaSesiones.Location = new Point(3, 3);
            gridCajaSesiones.Name = "gridCajaSesiones";
            gridCajaSesiones.ReadOnly = true;
            gridCajaSesiones.RowHeadersVisible = false;
            gridCajaSesiones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCajaSesiones.Size = new Size(931, 362);
            gridCajaSesiones.TabIndex = 0;
            // 
            // tabCajaCuentaCorriente
            // 
            tabCajaCuentaCorriente.BackColor = Color.FromArgb(15, 23, 42);
            tabCajaCuentaCorriente.Controls.Add(gridCajaCuentaCorriente);
            tabCajaCuentaCorriente.Location = new Point(4, 24);
            tabCajaCuentaCorriente.Name = "tabCajaCuentaCorriente";
            tabCajaCuentaCorriente.Padding = new Padding(3);
            tabCajaCuentaCorriente.Size = new Size(937, 368);
            tabCajaCuentaCorriente.TabIndex = 1;
            tabCajaCuentaCorriente.Text = "📒 Dinero Fiado en Cta. Cte. (Lo que se debe)";
            // 
            // gridCajaCuentaCorriente
            // 
            gridCajaCuentaCorriente.AllowUserToAddRows = false;
            gridCajaCuentaCorriente.AllowUserToDeleteRows = false;
            gridCajaCuentaCorriente.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridCajaCuentaCorriente.BorderStyle = BorderStyle.None;
            gridCajaCuentaCorriente.Dock = DockStyle.Fill;
            gridCajaCuentaCorriente.Location = new Point(3, 3);
            gridCajaCuentaCorriente.Name = "gridCajaCuentaCorriente";
            gridCajaCuentaCorriente.ReadOnly = true;
            gridCajaCuentaCorriente.RowHeadersVisible = false;
            gridCajaCuentaCorriente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCajaCuentaCorriente.Size = new Size(931, 362);
            gridCajaCuentaCorriente.TabIndex = 0;
            // 
            // tabCajaRetiros
            // 
            tabCajaRetiros.BackColor = Color.FromArgb(15, 23, 42);
            tabCajaRetiros.Controls.Add(gridCajaRetiros);
            tabCajaRetiros.Location = new Point(4, 24);
            tabCajaRetiros.Name = "tabCajaRetiros";
            tabCajaRetiros.Padding = new Padding(3);
            tabCajaRetiros.Size = new Size(937, 368);
            tabCajaRetiros.TabIndex = 2;
            tabCajaRetiros.Text = "💸 Retiros de Efectivo";
            // 
            // gridCajaRetiros
            // 
            gridCajaRetiros.AllowUserToAddRows = false;
            gridCajaRetiros.AllowUserToDeleteRows = false;
            gridCajaRetiros.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridCajaRetiros.BorderStyle = BorderStyle.None;
            gridCajaRetiros.Dock = DockStyle.Fill;
            gridCajaRetiros.Location = new Point(3, 3);
            gridCajaRetiros.Name = "gridCajaRetiros";
            gridCajaRetiros.ReadOnly = true;
            gridCajaRetiros.RowHeadersVisible = false;
            gridCajaRetiros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCajaRetiros.Size = new Size(931, 362);
            gridCajaRetiros.TabIndex = 0;
            // 
            // pnlReporteCompras
            // 
            pnlReporteCompras.Controls.Add(tabCompras);
            pnlReporteCompras.Controls.Add(pnlKpisCompras);
            pnlReporteCompras.Controls.Add(pnlHeaderCompras);
            pnlReporteCompras.Dock = DockStyle.Fill;
            pnlReporteCompras.Location = new Point(15, 5);
            pnlReporteCompras.Name = "pnlReporteCompras";
            pnlReporteCompras.Size = new Size(945, 526);
            pnlReporteCompras.TabIndex = 2;
            pnlReporteCompras.Visible = false;
            // 
            // pnlHeaderCompras
            // 
            pnlHeaderCompras.BackColor = Color.FromArgb(30, 41, 59);
            pnlHeaderCompras.Controls.Add(btnExportarComprasPdf);
            pnlHeaderCompras.Controls.Add(btnExportarComprasExcel);
            pnlHeaderCompras.Controls.Add(lblDescCompras);
            pnlHeaderCompras.Controls.Add(lblTituloCompras);
            pnlHeaderCompras.Dock = DockStyle.Top;
            pnlHeaderCompras.Location = new Point(0, 0);
            pnlHeaderCompras.Name = "pnlHeaderCompras";
            pnlHeaderCompras.Padding = new Padding(15, 10, 15, 10);
            pnlHeaderCompras.Size = new Size(945, 58);
            pnlHeaderCompras.TabIndex = 0;
            // 
            // lblTituloCompras
            // 
            lblTituloCompras.AutoSize = true;
            lblTituloCompras.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblTituloCompras.ForeColor = Color.FromArgb(163, 230, 53);
            lblTituloCompras.Location = new Point(12, 8);
            lblTituloCompras.Name = "lblTituloCompras";
            lblTituloCompras.Size = new Size(177, 23);
            lblTituloCompras.TabIndex = 0;
            lblTituloCompras.Text = "Reportes de compras";
            // 
            // lblDescCompras
            // 
            lblDescCompras.AutoSize = true;
            lblDescCompras.Font = new Font("Segoe UI", 8.5F);
            lblDescCompras.ForeColor = Color.FromArgb(148, 163, 184);
            lblDescCompras.Location = new Point(14, 32);
            lblDescCompras.Name = "lblDescCompras";
            lblDescCompras.Size = new Size(420, 15);
            lblDescCompras.TabIndex = 1;
            lblDescCompras.Text = "Historial de compras a proveedores, productos adquiridos y rentabilidad proyectada";
            // 
            // btnExportarComprasPdf
            // 
            btnExportarComprasPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarComprasPdf.BackColor = Color.FromArgb(239, 68, 68);
            btnExportarComprasPdf.Cursor = Cursors.Hand;
            btnExportarComprasPdf.FlatAppearance.BorderSize = 0;
            btnExportarComprasPdf.FlatStyle = FlatStyle.Flat;
            btnExportarComprasPdf.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportarComprasPdf.ForeColor = Color.White;
            btnExportarComprasPdf.Location = new Point(790, 12);
            btnExportarComprasPdf.Name = "btnExportarComprasPdf";
            btnExportarComprasPdf.Size = new Size(140, 34);
            btnExportarComprasPdf.TabIndex = 3;
            btnExportarComprasPdf.Text = "📕 Exportar a PDF";
            btnExportarComprasPdf.UseVisualStyleBackColor = false;
            btnExportarComprasPdf.Click += btnExportarComprasPdf_Click;
            // 
            // btnExportarComprasExcel
            // 
            btnExportarComprasExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarComprasExcel.BackColor = Color.FromArgb(34, 197, 94);
            btnExportarComprasExcel.Cursor = Cursors.Hand;
            btnExportarComprasExcel.FlatAppearance.BorderSize = 0;
            btnExportarComprasExcel.FlatStyle = FlatStyle.Flat;
            btnExportarComprasExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportarComprasExcel.ForeColor = Color.White;
            btnExportarComprasExcel.Location = new Point(635, 12);
            btnExportarComprasExcel.Name = "btnExportarComprasExcel";
            btnExportarComprasExcel.Size = new Size(145, 34);
            btnExportarComprasExcel.TabIndex = 2;
            btnExportarComprasExcel.Text = "📗 Exportar a Excel";
            btnExportarComprasExcel.UseVisualStyleBackColor = false;
            btnExportarComprasExcel.Click += btnExportarComprasExcel_Click;
            // 
            // pnlKpisCompras
            // 
            pnlKpisCompras.BackColor = Color.FromArgb(15, 23, 42);
            pnlKpisCompras.Dock = DockStyle.Top;
            pnlKpisCompras.Location = new Point(0, 58);
            pnlKpisCompras.Name = "pnlKpisCompras";
            pnlKpisCompras.Padding = new Padding(0, 6, 0, 6);
            pnlKpisCompras.Size = new Size(945, 72);
            pnlKpisCompras.TabIndex = 1;
            // 
            // tabCompras
            // 
            tabCompras.Controls.Add(tabComprasResumen);
            tabCompras.Controls.Add(tabComprasDetalle);
            tabCompras.Dock = DockStyle.Fill;
            tabCompras.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tabCompras.Location = new Point(0, 130);
            tabCompras.Name = "tabCompras";
            tabCompras.SelectedIndex = 0;
            tabCompras.Size = new Size(945, 396);
            tabCompras.TabIndex = 2;
            // 
            // tabComprasResumen
            // 
            tabComprasResumen.BackColor = Color.FromArgb(15, 23, 42);
            tabComprasResumen.Controls.Add(gridComprasResumen);
            tabComprasResumen.Location = new Point(4, 24);
            tabComprasResumen.Name = "tabComprasResumen";
            tabComprasResumen.Padding = new Padding(3);
            tabComprasResumen.Size = new Size(937, 368);
            tabComprasResumen.TabIndex = 0;
            tabComprasResumen.Text = "📋 Listado de Compras";
            // 
            // gridComprasResumen
            // 
            gridComprasResumen.AllowUserToAddRows = false;
            gridComprasResumen.AllowUserToDeleteRows = false;
            gridComprasResumen.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridComprasResumen.BorderStyle = BorderStyle.None;
            gridComprasResumen.Dock = DockStyle.Fill;
            gridComprasResumen.Location = new Point(3, 3);
            gridComprasResumen.Name = "gridComprasResumen";
            gridComprasResumen.ReadOnly = true;
            gridComprasResumen.RowHeadersVisible = false;
            gridComprasResumen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridComprasResumen.Size = new Size(931, 362);
            gridComprasResumen.TabIndex = 0;
            // 
            // tabComprasDetalle
            // 
            tabComprasDetalle.BackColor = Color.FromArgb(15, 23, 42);
            tabComprasDetalle.Controls.Add(gridComprasDetalle);
            tabComprasDetalle.Location = new Point(4, 24);
            tabComprasDetalle.Name = "tabComprasDetalle";
            tabComprasDetalle.Padding = new Padding(3);
            tabComprasDetalle.Size = new Size(937, 368);
            tabComprasDetalle.TabIndex = 1;
            tabComprasDetalle.Text = "📦 Detalle de Ítems Comprados";
            // 
            // gridComprasDetalle
            // 
            gridComprasDetalle.AllowUserToAddRows = false;
            gridComprasDetalle.AllowUserToDeleteRows = false;
            gridComprasDetalle.BackgroundColor = Color.FromArgb(30, 41, 59);
            gridComprasDetalle.BorderStyle = BorderStyle.None;
            gridComprasDetalle.Dock = DockStyle.Fill;
            gridComprasDetalle.Location = new Point(3, 3);
            gridComprasDetalle.Name = "gridComprasDetalle";
            gridComprasDetalle.ReadOnly = true;
            gridComprasDetalle.RowHeadersVisible = false;
            gridComprasDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridComprasDetalle.Size = new Size(931, 362);
            gridComprasDetalle.TabIndex = 0;
            // 
            // FormReportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlContenedor);
            Controls.Add(pnlMenu);
            Controls.Add(pnlFiltros);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MinimumSize = new Size(1150, 680);
            Name = "FormReportes";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Módulo de Reportes - Cantina Padel Club";
            Load += FormReportes_Load;

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            pnlMenu.ResumeLayout(false);
            pnlContenedor.ResumeLayout(false);
            pnlReporteVentas.ResumeLayout(false);
            pnlHeaderVentas.ResumeLayout(false);
            pnlHeaderVentas.PerformLayout();
            tabVentas.ResumeLayout(false);
            tabVentasDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridVentasDetalle).EndInit();
            tabVentasDiferenciacion.ResumeLayout(false);
            tlpCantinaPadel.ResumeLayout(false);
            pnlMitadCantina.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridVentasCantina).EndInit();
            pnlMitadPadel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridVentasPadel).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridVentasDiferenciacion).EndInit();
            tabVentasComprobante.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridVentasComprobante).EndInit();
            tabVentasMedioPago.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridVentasMedioPago).EndInit();
            pnlReporteCaja.ResumeLayout(false);
            pnlHeaderCaja.ResumeLayout(false);
            pnlHeaderCaja.PerformLayout();
            tabCaja.ResumeLayout(false);
            tabCajaSesiones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridCajaSesiones).EndInit();
            tabCajaCuentaCorriente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridCajaCuentaCorriente).EndInit();
            tabCajaRetiros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridCajaRetiros).EndInit();
            pnlReporteCompras.ResumeLayout(false);
            pnlHeaderCompras.ResumeLayout(false);
            pnlHeaderCompras.PerformLayout();
            tabCompras.ResumeLayout(false);
            tabComprasResumen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridComprasResumen).EndInit();
            tabComprasDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridComprasDetalle).EndInit();
            pnlDetalleProductoLateral.ResumeLayout(false);
            pnlHeaderDetalleProd.ResumeLayout(false);
            pnlHeaderDetalleProd.PerformLayout();
            pnlCuerpoDetalleProd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridItemsComprobante).EndInit();
            ResumeLayout(false);
                    WindowState = FormWindowState.Maximized;
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTituloModulo;
        private Label lblSubtitulo;
        private Button btnVolver;
        private Panel pnlFiltros;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnHoy;
        private Button btnEsteMes;
        private Button btnMesAnterior;
        private Button btnHistorico;
        private Button btnFiltrar;
        private Panel pnlMenu;
        private Button btnMenuVentas;
        private Button btnMenuCaja;
        private Button btnMenuCompras;
        private Panel pnlContenedor;
        private Panel pnlReporteVentas;
        private Panel pnlHeaderVentas;
        private Label lblTituloVentas;
        private Label lblDescVentas;
        private Button btnExportarVentasPdf;
        private Button btnExportarVentasExcel;
        private FlowLayoutPanel pnlKpisVentas;
        private TabControl tabVentas;
        private TabPage tabVentasDetalle;
        private DataGridView gridVentasDetalle;
        private TabPage tabVentasDiferenciacion;
        private TableLayoutPanel tlpCantinaPadel;
        private Panel pnlMitadCantina;
        private Label lblHeaderTablaCantina;
        private DataGridView gridVentasCantina;
        private Panel pnlMitadPadel;
        private Label lblHeaderTablaPadel;
        private DataGridView gridVentasPadel;
        private DataGridView gridVentasDiferenciacion;
        private TabPage tabVentasComprobante;
        private DataGridView gridVentasComprobante;
        private TabPage tabVentasMedioPago;
        private DataGridView gridVentasMedioPago;
        private Panel pnlReporteCaja;
        private Panel pnlHeaderCaja;
        private Label lblTituloCaja;
        private Label lblDescCaja;
        private Button btnExportarCajaPdf;
        private Button btnExportarCajaExcel;
        private FlowLayoutPanel pnlKpisCaja;
        private TabControl tabCaja;
        private TabPage tabCajaSesiones;
        private DataGridView gridCajaSesiones;
        private TabPage tabCajaCuentaCorriente;
        private DataGridView gridCajaCuentaCorriente;
        private TabPage tabCajaRetiros;
        private DataGridView gridCajaRetiros;
        private Panel pnlReporteCompras;
        private Panel pnlHeaderCompras;
        private Label lblTituloCompras;
        private Label lblDescCompras;
        private Button btnExportarComprasPdf;
        private Button btnExportarComprasExcel;
        private FlowLayoutPanel pnlKpisCompras;
        private TabControl tabCompras;
        private TabPage tabComprasResumen;
        private DataGridView gridComprasResumen;
        private TabPage tabComprasDetalle;
        private DataGridView gridComprasDetalle;
        private Panel pnlDetalleProductoLateral;
        private Panel pnlHeaderDetalleProd;
        private Label lblTituloDetalleProd;
        private Label lblSubDetalleProd;
        private Panel pnlCuerpoDetalleProd;
        private Label lblDetProductoValor;
        private Label lblDetCodigoValor;
        private Label lblDetCategoriaValor;
        private Label lblDetMarcaValor;
        private Label lblDetProveedorValor;
        private Label lblDetCantidadValor;
        private Label lblDetPrecioUnitValor;
        private Label lblDetSubtotalValor;
        private Label lblDetPrecioVentaValor;
        private Label lblDetStockValor;
        private Label lblDetGananciaValor;
        private Label lblDetDescripcionValor;
        private Label lblTituloItemsComp;
        private DataGridView gridItemsComprobante;
    }
}
