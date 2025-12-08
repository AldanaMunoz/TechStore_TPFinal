using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Datos;
using TechStore.Entidades;

namespace TechStore.Negocio
{
    public class InventarioNegocio
    {
        private readonly InventarioRepository _inventarioRepo;

        public InventarioNegocio()
        {
            _inventarioRepo = new InventarioRepository();
        }

        // Obtener inventario por sucursal
        public List<Inventario> ObtenerInventarioPorSucursal(int sucursalId)
        {
            try
            {
                return _inventarioRepo.ObtenerPorSucursal(sucursalId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener inventario: {ex.Message}", ex);
            }
        }

        // Obtener inventario por producto
        public List<Inventario> ObtenerInventarioPorProducto(int productoId)
        {
            try
            {
                return _inventarioRepo.ObtenerPorProducto(productoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener inventario del producto: {ex.Message}", ex);
            }
        }

        // Obtener stock disponible
        public int ObtenerStockDisponible(int productoId, int sucursalId)
        {
            try
            {
                var inventario = _inventarioRepo.ObtenerStockProductoSucursal(productoId, sucursalId);
                return inventario?.StockActual ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener stock: {ex.Message}", ex);
            }
        }

        // Actualizar stock manualmente
        public bool ActualizarStock(int productoId, int sucursalId, int nuevoStock, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                if (nuevoStock < 0)
                {
                    mensaje = "El stock no puede ser negativo.";
                    return false;
                }

                var inventario = _inventarioRepo.ObtenerStockProductoSucursal(productoId, sucursalId);

                if (inventario == null)
                {
                    mensaje = "No se encontró el registro de inventario.";
                    return false;
                }

                inventario.StockActual = nuevoStock;
                inventario.UltimaActualizacion = DateTime.Now;

                bool resultado = _inventarioRepo.Actualizar(inventario);

                if (resultado)
                    mensaje = "Stock actualizado exitosamente.";
                else
                    mensaje = "No se pudo actualizar el stock.";

                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al actualizar stock: {ex.Message}";
                return false;
            }
        }

        // Obtener productos con stock bajo
        public List<Inventario> ObtenerProductosStockBajo(int sucursalId)
        {
            try
            {
                return _inventarioRepo.ObtenerProductosStockBajo(sucursalId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener productos con stock bajo: {ex.Message}", ex);
            }
        }
    }
}
