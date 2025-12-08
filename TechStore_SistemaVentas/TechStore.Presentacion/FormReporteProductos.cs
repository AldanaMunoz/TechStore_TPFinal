using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechStore.Negocio;


namespace TechStore.Presentacion
{
    public partial class FormReporteProductos : Form
    {
        private readonly ReporteNegocio _reporteNegocio;

        public FormReporteProductos()
        {
            InitializeComponent();
            _reporteNegocio = new ReporteNegocio();
            ConfigurarFormulario();
        }
        private void FormReporteProductos_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            ConfigurarFechas();
            GenerarReporte();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Reporte - Productos Más Vendidos";
        }

        private void ConfigurarFechas()
        {
            dtpDesde.Value = DateTime.Now.AddMonths(-1);
            dtpHasta.Value = DateTime.Now;
        }
        private void ConfigurarDataGridView()
        {
            dgvReporte.AutoGenerateColumns = false;
            dgvReporte.ReadOnly = true;
            dgvReporte.AllowUserToAddRows = false;

            dgvReporte.Columns.Clear();

            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductoId",
                HeaderText = "ID",
                Width = 60
            });

            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreProducto",
                HeaderText = "Producto",
                Width = 400
            });

            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CantidadVendida",
                HeaderText = "Cantidad Vendida",
                Width = 120
            });

            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalVentas",
                HeaderText = "Total Ventas",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
        }
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }
        private void GenerarReporte()
        {
            try
            {
                DateTime fechaDesde = dtpDesde.Value.Date;
                DateTime fechaHasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

                var reporte = _reporteNegocio.ObtenerProductosMasVendidos(fechaDesde, fechaHasta, 20);
                dgvReporte.DataSource = reporte;

                decimal totalGeneral = reporte.Sum(p => p.TotalVentas);
                lblTotal.Text = $"Total productos: {reporte.Count} | Monto total: {totalGeneral:C2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void FormReporteProductos_Load_1(object sender, EventArgs e)
        {

        }
    }
}
