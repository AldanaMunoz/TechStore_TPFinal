using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TechStore.Entidades;

namespace TechStore.Datos
{
    public class InventarioRepository : Repository<Inventario>
    {
        // Obtener inventario por sucursal
        public List<Inventario> ObtenerPorSucursal(int sucursalId)
        {
            return _context.Inventarios
                .Include(i => i.Producto)
                .Include(i => i.Producto.Categoria)
                .Include(i => i.Sucursal)
                .Where(i => i.SucursalId == sucursalId && i.Producto.Activo)
                .OrderBy(i => i.Producto.Nombre)
                .ToList();
        }

        // Obtener inventario por producto
        public List<Inventario> ObtenerPorProducto(int productoId)
        {
            return _context.Inventarios
                .Include(i => i.Sucursal)
                .Where(i => i.ProductoId == productoId)
                .ToList();
        }

        // Obtener stock de un producto en una sucursal específica
        public Inventario ObtenerStockProductoSucursal(int productoId, int sucursalId)
        {
            return _context.Inventarios
                .Include(i => i.Producto)
                .Include(i => i.Sucursal)
                .FirstOrDefault(i => i.ProductoId == productoId && i.SucursalId == sucursalId);
        }

        // Obtener productos con stock bajo
        public List<Inventario> ObtenerProductosStockBajo(int sucursalId)
        {
            return _context.Inventarios
                .Include(i => i.Producto)
                .Include(i => i.Producto.Categoria)
                .Where(i => i.SucursalId == sucursalId &&
                            i.StockActual <= i.Producto.StockMinimo)
                .ToList();
        }

        // Actualizar stock después de una venta
        public bool ActualizarStock(int productoId, int sucursalId, int cantidad)
        {
            var inventario = ObtenerStockProductoSucursal(productoId, sucursalId);

            if (inventario == null)
                return false;

            if (inventario.StockActual < cantidad)
                return false;

            inventario.StockActual -= cantidad;
            inventario.UltimaActualizacion = System.DateTime.Now;

            return Actualizar(inventario);
        }
    }
}
