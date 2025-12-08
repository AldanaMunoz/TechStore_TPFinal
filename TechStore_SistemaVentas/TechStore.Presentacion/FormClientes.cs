using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechStore.Entidades;
using TechStore.Negocio;

namespace TechStore.Presentacion
{
    public partial class FormClientes : Form
    {
        private readonly ClienteNegocio _clienteNegocio;
        private List<Cliente> _listaClientes;
        private bool _modoEdicion = false;
        private bool _suspendirSelection = false;
        private int _prevSelectedRowIndex = -1;
        private int _editingClientId = -1;

        public FormClientes()
        {
            InitializeComponent();
            _clienteNegocio = new ClienteNegocio();
            ConfigurarFormulario();
        }
        private void FormClientes_Load(object sender, EventArgs e)
        {
            CargarTiposCliente();
            CargarClientes();
            ConfigurarDataGridView();
            BloquearControles();
        }
        private void ConfigurarFormulario()
        {
            this.Text = "Gestión de Clientes";

            numLimiteCredito.DecimalPlaces = 2;
            numLimiteCredito.Maximum = 999999999;
            numLimiteCredito.ThousandsSeparator = true;
        }
        private void ConfigurarDataGridView()
        {
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;

            dgvClientes.Columns.Clear();

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RazonSocial",
                HeaderText = "Razón Social",
                Width = 250
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CUIT",
                HeaderText = "CUIT",
                Width = 120
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TipoCliente.Nombre",
                HeaderText = "Tipo Cliente",
                Width = 120
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Telefono",
                HeaderText = "Teléfono",
                Width = 120
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 200
            });

            dgvClientes.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Activo",
                HeaderText = "Activo",
                Width = 60
            });

            this.dgvClientes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvClientes_CellFormatting);
        }

        private void dgvClientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                var col = dgvClientes.Columns[e.ColumnIndex];
                if (col != null && col.DataPropertyName == "TipoCliente.Nombre")
                {
                    var row = dgvClientes.Rows[e.RowIndex];
                    var cliente = row.DataBoundItem as Cliente;
                    if (cliente != null)
                    {
                        e.Value = cliente.TipoCliente != null ? cliente.TipoCliente.Nombre : string.Empty;
                        e.FormattingApplied = true;
                    }
                }
            }
            catch { }
        }

        private void CargarTiposCliente()
        {
            try
            {
                var tipos = _clienteNegocio.ObtenerTiposCliente();
                cmbTipoCliente.DataSource = tipos;
                cmbTipoCliente.DisplayMember = "Nombre";
                cmbTipoCliente.ValueMember = "Id";
                cmbTipoCliente.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de cliente: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarClientes()
        {
            try
            {
                _listaClientes = _clienteNegocio.ObtenerClientes();
                dgvClientes.DataSource = _listaClientes;
                lblTotal.Text = $"Total de clientes: {_listaClientes.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string termino = txtBuscar.Text.Trim();

                if (!string.IsNullOrEmpty(termino))
                {
                    _listaClientes = _clienteNegocio.BuscarClientes(termino);
                }
                else
                {
                    _listaClientes = _clienteNegocio.ObtenerClientes();
                }

                dgvClientes.DataSource = null;
                dgvClientes.DataSource = _listaClientes;
                lblTotal.Text = $"Clientes encontrados: {_listaClientes.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la búsqueda: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarClientes();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            _modoEdicion = false;
            _editingClientId = -1;
            LimpiarCampos();
            DesbloquearControles();
            txtRazonSocial.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                var cliente = new Cliente
                {
                    RazonSocial = txtRazonSocial.Text.Trim(),
                    CUIT = txtCUIT.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    TipoClienteId = (int)cmbTipoCliente.SelectedValue,
                    LimiteCredito = numLimiteCredito.Value,
                    Activo = chkActivo.Checked
                };

                string mensaje;
                bool resultado;

                if (_modoEdicion)
                {
                    cliente.Id = int.Parse(txtId.Text);
                    cliente.SaldoActual = decimal.Parse(txtSaldoActual.Text);
                    _editingClientId = cliente.Id;
                    resultado = _clienteNegocio.ActualizarCliente(cliente, out mensaje);
                }
                else
                {
                    resultado = _clienteNegocio.AgregarCliente(cliente, out mensaje);
                }

                MessageBox.Show(mensaje, resultado ? "Éxito" : "Error",
                    MessageBoxButtons.OK,
                    resultado ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (resultado)
                {
                    _modoEdicion = false;
                    CargarClientes();

                    if (_editingClientId > 0 && _listaClientes != null)
                    {
                        int idx = _listaClientes.FindIndex(c => c.Id == _editingClientId);
                        if (idx >= 0 && idx < dgvClientes.Rows.Count)
                        {
                            _suspendirSelection = true;
                            dgvClientes.ClearSelection();
                            dgvClientes.CurrentCell = dgvClientes.Rows[idx].Cells[0];
                            dgvClientes.Rows[idx].Selected = true;
                            _prevSelectedRowIndex = idx;
                            _suspendirSelection = false;
                        }
                    }

                    _editingClientId = -1;
                    LimpiarCampos();
                    BloquearControles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un cliente de la lista.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _modoEdicion = true;
            var cliente = (Cliente)dgvClientes.CurrentRow.DataBoundItem;
            _editingClientId = cliente.Id;
            CargarDatosCliente(cliente);
            DesbloquearControles();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un cliente de la lista.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cliente = (Cliente)dgvClientes.CurrentRow.DataBoundItem;

            var resultado = MessageBox.Show(
                $"¿Está seguro de eliminar el cliente '{cliente.RazonSocial}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    string mensaje;
                    bool exito = _clienteNegocio.EliminarCliente(cliente.Id, out mensaje);

                    MessageBox.Show(mensaje, exito ? "Éxito" : "Error",
                        MessageBoxButtons.OK,
                        exito ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                    if (exito)
                    {
                        CargarClientes();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            BloquearControles();
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditar_Click(sender, e);
            }
        }
        private void CargarDatosCliente(Cliente cliente)
        {
            txtId.Text = cliente.Id.ToString();
            txtRazonSocial.Text = cliente.RazonSocial;
            txtCUIT.Text = cliente.CUIT;
            txtDireccion.Text = cliente.Direccion;
            txtTelefono.Text = cliente.Telefono;
            txtEmail.Text = cliente.Email;
            cmbTipoCliente.SelectedValue = cliente.TipoClienteId;
            numLimiteCredito.Value = cliente.LimiteCredito;
            txtSaldoActual.Text = cliente.SaldoActual.ToString("N2");
            chkActivo.Checked = cliente.Activo;
        }
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtRazonSocial.Clear();
            txtCUIT.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            cmbTipoCliente.SelectedIndex = -1;
            numLimiteCredito.Value = 0;
            txtSaldoActual.Text = "0.00";
            chkActivo.Checked = true;
        }
        private void BloquearControles()
        {
            txtRazonSocial.Enabled = false;
            txtCUIT.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            cmbTipoCliente.Enabled = false;
            numLimiteCredito.Enabled = false;
            chkActivo.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }
        
        private void DesbloquearControles()
        {
            txtRazonSocial.Enabled = true;
            txtCUIT.Enabled = true;
            txtDireccion.Enabled = true;
            txtTelefono.Enabled = true;
            txtEmail.Enabled = true;
            cmbTipoCliente.Enabled = true;
            numLimiteCredito.Enabled = true;
            chkActivo.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
            {
                MessageBox.Show("La razón social es obligatoria.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRazonSocial.Focus();
                return false;
            }
            if (cmbTipoCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de cliente.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoCliente.Focus();
                return false;
            }
            return true;
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar_Click(sender, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (_suspendirSelection)
            {
                _prevSelectedRowIndex = dgvClientes.CurrentRow != null ? dgvClientes.CurrentRow.Index : -1;
                return;
            }

            int newIndex = dgvClientes.CurrentRow != null ? dgvClientes.CurrentRow.Index : -1;

            if (_modoEdicion && newIndex != _prevSelectedRowIndex)
            {
                var result = MessageBox.Show(
                    "Está editando un cliente. Si cambia de selección se cancelará la edición actual.\n¿Desea continuar y cancelar la edición?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _modoEdicion = false;
                    if (newIndex >= 0 && dgvClientes.CurrentRow.DataBoundItem is Cliente c)
                    {
                        _suspendirSelection = true;
                        CargarDatosCliente(c);
                        BloquearControles();
                        _suspendirSelection = false;
                        _prevSelectedRowIndex = newIndex;
                    }
                    else
                    {
                        LimpiarCampos();
                        _prevSelectedRowIndex = -1;
                    }
                }
                else
                {
                    _suspendirSelection = true;
                    if (_prevSelectedRowIndex >= 0 && _prevSelectedRowIndex < dgvClientes.Rows.Count)
                    {
                        dgvClientes.ClearSelection();
                        dgvClientes.CurrentCell = dgvClientes.Rows[_prevSelectedRowIndex].Cells[0];
                        dgvClientes.Rows[_prevSelectedRowIndex].Selected = true;
                    }
                    else
                    {
                        dgvClientes.ClearSelection();
                    }
                    _suspendirSelection = false;
                }

                return;
            }

            if (dgvClientes.CurrentRow != null && dgvClientes.CurrentRow.DataBoundItem is Cliente)
            {
                var cliente = (Cliente)dgvClientes.CurrentRow.DataBoundItem;
                if (_editingClientId > 0 && cliente.Id == _editingClientId)
                {
                    CargarDatosCliente(cliente);
                    _prevSelectedRowIndex = dgvClientes.CurrentRow.Index;
                    _editingClientId = -1;
                    return;
                }
                CargarDatosCliente(cliente);
                _prevSelectedRowIndex = dgvClientes.CurrentRow.Index;
            }
            else
            {
                _prevSelectedRowIndex = -1;
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
