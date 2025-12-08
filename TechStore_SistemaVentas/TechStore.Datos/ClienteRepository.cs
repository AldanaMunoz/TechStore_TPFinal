using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TechStore.Entidades;

namespace TechStore.Datos
{
    public class ClienteRepository : Repository<Cliente>
    {
        // Obtener clientes con tipo de cliente cargado
        public List<Cliente> ObtenerClientesConTipo()
        {
            return _context.Clientes
                .Include(c => c.TipoCliente)
                .Where(c => c.Activo)
                .OrderBy(c => c.RazonSocial)
                .ToList();
        }

        // Buscar cliente por CUIT
        public Cliente ObtenerPorCUIT(string cuit)
        {
            return _dbSet
                .Include(c => c.TipoCliente)
                .FirstOrDefault(c => c.CUIT == cuit);
        }

        // Buscar clientes por nombre
        public List<Cliente> BuscarPorNombre(string nombre)
        {
            nombre = nombre.ToLower();
            return _dbSet
                .Include(c => c.TipoCliente)
                .Where(c => c.Activo && c.RazonSocial.ToLower().Contains(nombre))
                .ToList();
        }

        // Obtener clientes por tipo
        public List<Cliente> ObtenerPorTipo(int tipoClienteId)
        {
            return _dbSet
                .Include(c => c.TipoCliente)
                .Where(c => c.TipoClienteId == tipoClienteId && c.Activo)
                .ToList();
        }
    }
}