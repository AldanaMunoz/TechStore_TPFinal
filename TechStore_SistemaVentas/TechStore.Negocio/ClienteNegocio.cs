using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Datos;
using TechStore.Entidades;

namespace TechStore.Negocio
{
    public class ClienteNegocio
    {
        private readonly ClienteRepository _clienteRepo;
        private readonly Repository<TipoCliente> _tipoClienteRepo;

        public ClienteNegocio()
        {
            _clienteRepo = new ClienteRepository();
            _tipoClienteRepo = new Repository<TipoCliente>();
        }

        // Obtener todos los clientes activos
        public List<Cliente> ObtenerClientes()
        {
            try
            {
                return _clienteRepo.ObtenerClientesConTipo();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener clientes: {ex.Message}", ex);
            }
        }

        // Obtener cliente por ID
        public Cliente ObtenerClientePorId(int id)
        {
            try
            {
                return _clienteRepo.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener cliente: {ex.Message}", ex);
            }
        }

        // Buscar clientes por nombre
        public List<Cliente> BuscarClientes(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    return ObtenerClientes();

                return _clienteRepo.BuscarPorNombre(nombre);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar clientes: {ex.Message}", ex);
            }
        }

        // Agregar cliente con validaciones
        public bool AgregarCliente(Cliente cliente, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(cliente.RazonSocial))
                {
                    mensaje = "La razón social es obligatoria.";
                    return false;
                }

                // Verificar CUIT duplicado si se proporciona
                if (!string.IsNullOrWhiteSpace(cliente.CUIT))
                {
                    var clienteExistente = _clienteRepo.ObtenerPorCUIT(cliente.CUIT);
                    if (clienteExistente != null)
                    {
                        mensaje = "Ya existe un cliente con ese CUIT.";
                        return false;
                    }
                }

                bool resultado = _clienteRepo.Agregar(cliente);

                if (resultado)
                    mensaje = "Cliente agregado exitosamente.";
                else
                    mensaje = "No se pudo agregar el cliente.";

                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al agregar cliente: {ex.Message}";
                return false;
            }
        }

        // Actualizar cliente
        public bool ActualizarCliente(Cliente cliente, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(cliente.RazonSocial))
                {
                    mensaje = "La razón social es obligatoria.";
                    return false;
                }

                bool resultado = _clienteRepo.Actualizar(cliente);

                if (resultado)
                    mensaje = "Cliente actualizado exitosamente.";
                else
                    mensaje = "No se pudo actualizar el cliente.";

                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al actualizar cliente: {ex.Message}";
                return false;
            }
        }

        // Eliminar cliente (marcar como inactivo)
        public bool EliminarCliente(int id, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                var cliente = _clienteRepo.ObtenerPorId(id);

                if (cliente == null)
                {
                    mensaje = "Cliente no encontrado.";
                    return false;
                }

                cliente.Activo = false;
                bool resultado = _clienteRepo.Actualizar(cliente);

                if (resultado)
                    mensaje = "Cliente eliminado exitosamente.";
                else
                    mensaje = "No se pudo eliminar el cliente.";

                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al eliminar cliente: {ex.Message}";
                return false;
            }
        }

        // Obtener tipos de cliente
        public List<TipoCliente> ObtenerTiposCliente()
        {
            try
            {
                return _tipoClienteRepo.ObtenerTodos();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener tipos de cliente: {ex.Message}", ex);
            }
        }
    }
}