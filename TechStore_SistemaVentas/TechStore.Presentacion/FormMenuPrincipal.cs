using System;
using System.Drawing;
using System.Windows.Forms;

namespace TechStore.Presentacion
{
    public partial class FormMenuPrincipal : Form
    {
        private Panel panelBienvenida;

        public FormMenuPrincipal()
        {
            InitializeComponent();
            ConfigurarMenus();
        }

        private void ConfigurarMenus()
        {
            this.Text = "TechStore - Sistema de Gestión de Ventas";
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 700);
            this.IsMdiContainer = true;
            this.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormProductos());
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormClientes());
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormInventario());
        }

        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormNuevaVenta());
        }

        private void consultarVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormConsultarVentas());
        }

        private void productosMasVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormReporteProductos());
        }

        private void ventasPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormReporteVendedores());
        }

        private void ventasPorSucursalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormReporteSucursales());
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea salir del sistema?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void AbrirFormularioHijo(Form formularioHijo)
        {
            if (panelBienvenida != null && this.Controls.Contains(panelBienvenida))
            {
                this.Controls.Remove(panelBienvenida);
                panelBienvenida.Dispose();
                panelBienvenida = null;
            }

            foreach (Form form in this.MdiChildren)
            {
                form.Close();
            }

            formularioHijo.MdiParent = this;
            formularioHijo.WindowState = FormWindowState.Maximized;
            formularioHijo.Show();
        }

        private void FormMenuPrincipal_Load(object sender, EventArgs e)
        {
            MostrarPanelBienvenida();
        }

        private void MostrarPanelBienvenida()
        {
            panelBienvenida = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            Label lblTitulo = new Label
            {
                Text = "TechStore S.A.",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 66, 91),
                AutoSize = true
            };

            Label lblSubtitulo = new Label
            {
                Text = "Sistema de Gestión de Ventas",
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true
            };

            Label lblInstrucciones = new Label
            {
                Text = "Seleccione una opción del menú para comenzar",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(150, 150, 150),
                AutoSize = true
            };


            panelBienvenida.Resize += (s, ev) =>
            {
                lblTitulo.Location = new Point(
                    (panelBienvenida.Width - lblTitulo.Width) / 2,
                    (panelBienvenida.Height - lblTitulo.Height) / 2 - 60
                );
                lblSubtitulo.Location = new Point(
                    (panelBienvenida.Width - lblSubtitulo.Width) / 2,
                    lblTitulo.Bottom + 20
                );
                lblInstrucciones.Location = new Point(
                    (panelBienvenida.Width - lblInstrucciones.Width) / 2,
                    lblSubtitulo.Bottom + 40
                );
            };

            panelBienvenida.Controls.Add(lblTitulo);
            panelBienvenida.Controls.Add(lblSubtitulo);
            panelBienvenida.Controls.Add(lblInstrucciones);

            this.Controls.Add(panelBienvenida);
            panelBienvenida.BringToFront();
        }
    }
}