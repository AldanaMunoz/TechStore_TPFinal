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
    public partial class FormReporteSucursales : Form
    {
        private readonly ReporteNegocio _reporteNegocio;
        public FormReporteSucursales()
        {
            InitializeComponent();
            _reporteNegocio = new ReporteNegocio();
            ConfigurarFormulario();
        }
        private void FormReporteSucursales_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            ConfigurarFechas();
            GenerarReporte();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Reporte - Ventas por Sucursal";
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
                DataPropertyName = "NombreSucursal",
                HeaderText = "Sucursal",
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

                var reporte = _reporteNegocio.ObtenerVentasPorSucursal(fechaDesde, fechaHasta);
                dgvReporte.DataSource = reporte;

                decimal totalGeneral = reporte.Sum(s => s.TotalVentas);
                int totalVentas = reporte.Sum(s => s.CantidadVentas);
                lblTotal.Text = $"Total ventas: {totalVentas} | Monto total: {totalGeneral:C2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
