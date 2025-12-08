using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Datos;
using TechStore.Entidades;

namespace TechStore.Negocio
{
    public class VentaNegocio
    {
        private readonly VentaRepository _ventaRepo;
        private readonly InventarioRepository _inventarioRepo;
        private readonly ClienteRepository _clienteRepo;
        private readonly Repository<DetalleVenta> _detalleRepo;

        public VentaNegocio()
        {
            _ventaRepo = new VentaRepository();
            _inventarioRepo = new InventarioRepository();
            _clienteRepo = new ClienteRepository();
            _detalleRepo = new Repository<DetalleVenta>();
        }

        // Obtener todas las ventas
        public List<Venta> ObtenerVentas()
        {
            try
            {
                return _ventaRepo.ObtenerVentasCompletas();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener ventas: {ex.Message}", ex);
            }
        }

        // Obtener venta con detalles
        public Venta ObtenerVentaConDetalles(int ventaId)
        {
            try
            {
                return _ventaRepo.ObtenerVentaConDetalles(ventaId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener venta: {ex.Message}", ex);
            }
        }

        // Registrar una venta completa
        public bool RegistrarVenta(Venta venta, List<DetalleVenta> detalles, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                // Validaciones básicas
                if (venta.ClienteId <= 0)
                {
                    mensaje = "Debe seleccionar un cliente.";
                    return false;
                }

                if (venta.VendedorId <= 0)
                {
                    mensaje = "Debe seleccionar un vendedor.";
                    return false;
                }

                if (venta.SucursalId <= 0)
                {
                    mensaje = "Debe seleccionar una sucursal.";
                    return false;
                }

                if (detalles == null || detalles.Count == 0)
                {
                    mensaje = "Debe agregar al menos un producto a la venta.";
                    return false;
                }

                // Generar número de factura
                venta.NumeroFactura = _ventaRepo.ObtenerSiguienteNumeroFactura();

                // Verificar stock para todos los productos
                foreach (var detalle in detalles)
                {
                    var inventario = _inventarioRepo.ObtenerStockProductoSucursal(
                        detalle.ProductoId, venta.SucursalId);

                    if (inventario == null || inventario.StockActual < detalle.Cantidad)
                    {
                        mensaje = $"Stock insuficiente para el producto: {detalle.Producto?.Nombre ?? "ID: " + detalle.ProductoId}";
                        return false;
                    }
                }

                // Calcular totales
                venta.Subtotal = detalles.Sum(d => d.Subtotal);

                // Obtener descuento según tipo de cliente
                var cliente = _clienteRepo.ObtenerPorId(venta.ClienteId);
                if (cliente != null && cliente.TipoCliente != null)
                {
                    venta.PorcentajeDescuento = cliente.TipoCliente.PorcentajeDescuento;
                }

                venta.MontoDescuento = venta.Subtotal * (venta.PorcentajeDescuento / 100);
                venta.Total = venta.Subtotal - venta.MontoDescuento;
                venta.FechaVenta = DateTime.Now;
                venta.Estado = "Completada";

                // Registrar la venta
                if (!_ventaRepo.Agregar(venta))
                {
                    mensaje = "No se pudo registrar la venta.";
                    return false;
                }

                // Registrar los detalles y actualizar inventario
                foreach (var detalle in detalles)
                {
                    detalle.VentaId = venta.Id;

                    if (!_detalleRepo.Agregar(detalle))
                    {
                        mensaje = "Error al registrar detalle de venta.";
                        return false;
                    }

                    // Actualizar stock
                    if (!_inventarioRepo.ActualizarStock(detalle.ProductoId, venta.SucursalId, detalle.Cantidad))
                    {
                        mensaje = "Error al actualizar inventario.";
                        return false;
                    }
                }

                mensaje = $"Venta registrada exitosamente. Factura: {venta.NumeroFactura}";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al registrar venta: {ex.Message}";
                return false;
            }
        }

        // Obtener ventas por rango de fechas
        public List<Venta> ObtenerVentasPorFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _ventaRepo.ObtenerPorFechas(fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener ventas por fechas: {ex.Message}", ex);
            }
        }

        // Obtener ventas por cliente
        public List<Venta> ObtenerVentasPorCliente(int clienteId)
        {
            try
            {
                return _ventaRepo.ObtenerPorCliente(clienteId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener ventas del cliente: {ex.Message}", ex);
            }
        }

        // Calcular total de ventas en un período
        public decimal CalcularTotalVentas(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var ventas = ObtenerVentasPorFechas(fechaDesde, fechaHasta);
                return ventas.Where(v => v.Estado == "Completada").Sum(v => v.Total);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al calcular total de ventas: {ex.Message}", ex);
            }
        }
    }
}
