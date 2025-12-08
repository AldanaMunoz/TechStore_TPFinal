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
    public partial class FormReporteVendedores : Form
    {
        private readonly ReporteNegocio _reporteNegocio;
        public FormReporteVendedores()
        {
            InitializeComponent();
            _reporteNegocio = new ReporteNegocio();
            ConfigurarFormulario();
        }
        private void FormReporteVendedores_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            ConfigurarFechas();
            GenerarReporte();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Reporte - Ventas por Vendedor";
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
                DataPropertyName = "NombreVendedor",
                HeaderText = "Vendedor",
                Width = 300
            });

            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CantidadVentas",
                HeaderText = "Cantidad de Ventas",
                Width = 150
            });

            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalVentas",
                HeaderText = "Total Ventas",
                Width = 200,
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

                var reporte = _reporteNegocio.ObtenerVentasPorVendedor(fechaDesde, fechaHasta);
                dgvReporte.DataSource = reporte;

                decimal totalGeneral = reporte.Sum(v => v.TotalVentas);
                int totalVentas = reporte.Sum(v => v.CantidadVentas);
                lblTotal.Text = $"Total ventas: {totalVentas} | Monto total: {totalGeneral:C2}";
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
    }
}
