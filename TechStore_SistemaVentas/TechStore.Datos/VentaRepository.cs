using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TechStore.Entidades;

namespace TechStore.Datos
{
    public class VentaRepository : Repository<Venta>
    {
        public List<Venta> ObtenerVentasCompletas()
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Cliente.TipoCliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Sucursal)
                .Include(v => v.MetodoPago)
                .Include(v => v.Detalles)
                .OrderByDescending(v => v.FechaVenta)
                .ToList();
        }

        public Venta ObtenerVentaConDetalles(int ventaId)
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Cliente.TipoCliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Sucursal)
                .Include(v => v.MetodoPago)
                .Include(v => v.Detalles.Select(d => d.Producto))
                .FirstOrDefault(v => v.Id == ventaId);
        }

        public List<Venta> ObtenerPorFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Cliente.TipoCliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Sucursal)
                .Include(v => v.MetodoPago)
                .Include(v => v.Detalles.Select(d => d.Producto))
                .Where(v => v.FechaVenta >= fechaDesde && v.FechaVenta <= fechaHasta)
                .OrderByDescending(v => v.FechaVenta)
                .ToList();
        }

        public List<Venta> ObtenerPorCliente(int clienteId)
        {
            return _context.Ventas
                .Include(v => v.Vendedor)
                .Include(v => v.Sucursal)
                .Include(v => v.MetodoPago)
                .Where(v => v.ClienteId == clienteId)
                .OrderByDescending(v => v.FechaVenta)
                .ToList();
        }

        public List<Venta> ObtenerPorVendedor(int vendedorId, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var query = _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Sucursal)
                .Where(v => v.VendedorId == vendedorId);

            if (fechaDesde.HasValue)
                query = query.Where(v => v.FechaVenta >= fechaDesde.Value);

            if (fechaHasta.HasValue)
                query = query.Where(v => v.FechaVenta <= fechaHasta.Value);

            return query.OrderByDescending(v => v.FechaVenta).ToList();
        }

        public string ObtenerSiguienteNumeroFactura()
        {
            var ultimaVenta = _dbSet
                .OrderByDescending(v => v.Id)
                .FirstOrDefault();

            if (ultimaVenta == null)
                return "FAC-00001";

            // Extraer el número de la última factura
            string ultimoNumero = ultimaVenta.NumeroFactura.Replace("FAC-", "");
            int numero = int.Parse(ultimoNumero) + 1;

            return $"FAC-{numero:D5}";
        }
    }
}
