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
    /// Lógica de negocio para la gestión de Productos
    /// </summary>
    public class ProductoNegocio
    {
        private readonly ProductoRepository _productoRepo;
        private readonly Repository<Categoria> _categoriaRepo;

        public ProductoNegocio()
        {
            _productoRepo = new ProductoRepository();
            _categoriaRepo = new Repository<Categoria>();
        }

        // Obtener todos los productos activos con categoría
        public List<Producto> ObtenerProductos()
        {
            try
            {
                return _productoRepo.ObtenerProductosConCategoria();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener productos: {ex.Message}", ex);
            }
        }

        // Obtener producto por ID
        public Producto ObtenerProductoPorId(int id)
        {
            try
            {
                return _productoRepo.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener producto: {ex.Message}", ex);
            }
        }

        // Buscar productos
        public List<Producto> BuscarProductos(string termino)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(termino))
                    return ObtenerProductos();

                return _productoRepo.BuscarPorCodigoONombre(termino);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar productos: {ex.Message}", ex);
            }
        }

        // Obtener productos por categoría
        public List<Producto> ObtenerProductosPorCategoria(int categoriaId)
        {
            try
            {
                return _productoRepo.ObtenerPorCategoria(categoriaId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener productos por categoría: {ex.Message}", ex);
            }
        }

        // Agregar producto con validaciones
        public bool AgregarProducto(Producto producto, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(producto.Codigo))
                {
                    mensaje = "El código del producto es obligatorio.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(producto.Nombre))
                {
                    mensaje = "El nombre del producto es obligatorio.";
                    return false;
                }

                if (producto.PrecioUnitario <= 0)
                {
                    mensaje = "El precio debe ser mayor a cero.";
                    return false;
                }

                // Verificar que el código no exista
                if (_productoRepo.ExisteCodigo(producto.Codigo))
                {
                    mensaje = "Ya existe un producto con ese código.";
                    return false;
                }

                // Agregar producto
                bool resultado = _productoRepo.Agregar(producto);

                if (resultado)
                    mensaje = "Producto agregado exitosamente.";
                else
                    mensaje = "No se pudo agregar el producto.";

                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al agregar producto: {ex.Message}";
                return false;
            }
        }

        // Actualizar producto con validaciones
        public bool ActualizarProducto(Producto producto, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(producto.Codigo))
                {
                    mensaje = "El código del producto es obligatorio.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(producto.Nombre))
                {
                    mensaje = "El nombre del producto es obligatorio.";
                    return false;
                }

                if (producto.PrecioUnitario <= 0)
                {
                    mensaje = "El precio debe ser mayor a cero.";
                    return false;
                }

                // Verificar que el código no exista en otro producto
                if (_productoRepo.ExisteCodigo(producto.Codigo, producto.Id))
                {
                    mensaje = "Ya existe otro producto con ese código.";
                    return false;
                }

                // Actualizar producto
                bool resultado = _productoRepo.Actualizar(producto);

                if (resultado)
                    mensaje = "Producto actualizado exitosamente.";
                else
                    mensaje = "No se pudo actualizar el producto.";

                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al actualizar producto: {ex.Message}";
                return false;
            }
        }

        // Eliminar producto (marcar como inactivo)
        public bool EliminarProducto(int id, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                var producto = _productoRepo.ObtenerPorId(id);

                if (producto == null)
                {
                    mensaje = "Producto no encontrado.";
                    return false;
                }

                // Marcar como inactivo en lugar de eliminar
                producto.Activo = false;
                bool resultado = _productoRepo.Actualizar(producto);

                if (resultado)
                    mensaje = "Producto eliminado exitosamente.";
                else
                    mensaje = "No se pudo eliminar el producto.";

                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al eliminar producto: {ex.Message}";
                return false;
            }
        }

        // Obtener todas las categorías activas
        public List<Categoria> ObtenerCategorias()
        {
            try
            {
                return _categoriaRepo.Buscar(c => c.Activo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener categorías: {ex.Message}", ex);
            }
        }
    }
}