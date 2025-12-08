using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Datos;
using TechStore.Entidades;

namespace TechStore.Negocio
{
    /// <summary>
    /// Lógica de negocio para reportes y estadísticas
    /// </summary>
    public class ReporteNegocio
    {
        private readonly VentaRepository _ventaRepo;
        private readonly Repository<DetalleVenta> _detalleRepo;

        public ReporteNegocio()
        {
            _ventaRepo = new VentaRepository();
            _detalleRepo = new Repository<DetalleVenta>();
        }

        // Productos más vendidos en un período
        public List<ProductoMasVendido> ObtenerProductosMasVendidos(DateTime fechaDesde, DateTime fechaHasta, int top = 10)
        {
            try
            {
                var ventas = _ventaRepo.ObtenerPorFechas(fechaDesde, fechaHasta);
                var ventasIds = ventas.Select(v => v.Id).ToList();

                var productosVendidos = _detalleRepo
                    .Buscar(d => ventasIds.Contains(d.VentaId))
                    .GroupBy(d => new { d.ProductoId, d.Producto.Nombre })
                    .Select(g => new ProductoMasVendido
                    {
                        ProductoId = g.Key.ProductoId,
                        NombreProducto = g.Key.Nombre,
                        CantidadVendida = g.Sum(d => d.Cantidad),
                        TotalVentas = g.Sum(d => d.Subtotal)
                    })
                    .OrderByDescending(p => p.CantidadVendida)
                    .Take(top)
                    .ToList();

                return productosVendidos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener productos más vendidos: {ex.Message}", ex);
            }
        }

        // Ventas por vendedor
        public List<VentasPorVendedor> ObtenerVentasPorVendedor(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var ventas = _ventaRepo.ObtenerPorFechas(fechaDesde, fechaHasta)
                    .Where(v => v.Estado == "Completada")
                    .GroupBy(v => new { v.VendedorId, v.Vendedor.Nombre, v.Vendedor.Apellido })
                    .Select(g => new VentasPorVendedor
                    {
                        VendedorId = g.Key.VendedorId,
                        NombreVendedor = $"{g.Key.Nombre} {g.Key.Apellido}",
                        CantidadVentas = g.Count(),
                        TotalVentas = g.Sum(v => v.Total)
                    })
                    .OrderByDescending(v => v.TotalVentas)
                    .ToList();

                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener ventas por vendedor: {ex.Message}", ex);
            }
        }

        // Ventas por sucursal
        public List<VentasPorSucursal> ObtenerVentasPorSucursal(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var ventas = _ventaRepo.ObtenerPorFechas(fechaDesde, fechaHasta)
                    .Where(v => v.Estado == "Completada")
                    .GroupBy(v => new { v.SucursalId, v.Sucursal.Nombre })
                    .Select(g => new VentasPorSucursal
                    {
                        SucursalId = g.Key.SucursalId,
                        NombreSucursal = g.Key.Nombre,
                        CantidadVentas = g.Count(),
                        TotalVentas = g.Sum(v => v.Total)
                    })
                    .OrderByDescending(v => v.TotalVentas)
                    .ToList();

                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener ventas por sucursal: {ex.Message}", ex);
            }
        }
    }

    // Clases auxiliares para reportes
    public class ProductoMasVendido
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int CantidadVendida { get; set; }
        public decimal TotalVentas { get; set; }
    }

    public class VentasPorVendedor
    {
        public int VendedorId { get; set; }
        public string NombreVendedor { get; set; }
        public int CantidadVentas { get; set; }
        public decimal TotalVentas { get; set; }
    }

    public class VentasPorSucursal
    {
        public int SucursalId { get; set; }
        public string NombreSucursal { get; set; }
        public int CantidadVentas { get; set; }
        public decimal TotalVentas { get; set; }
    }
}
