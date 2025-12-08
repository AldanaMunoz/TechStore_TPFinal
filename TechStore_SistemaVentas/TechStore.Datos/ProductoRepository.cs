using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TechStore.Entidades;

namespace TechStore.Datos
{
    /// <summary>
    /// Repositorio específico para Productos con métodos adicionales
    /// </summary>
    public class ProductoRepository : Repository<Producto>
    {
        // Obtener productos con su categoría cargada
        public List<Producto> ObtenerProductosConCategoria()
        {
            return _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.Activo)
                .ToList();
        }

        // Obtener productos por categoría
        public List<Producto> ObtenerPorCategoria(int categoriaId)
        {
            return _dbSet
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaId && p.Activo)
                .ToList();
        }

        // Buscar productos por código o nombre
        public List<Producto> BuscarPorCodigoONombre(string termino)
        {
            termino = termino.ToLower();
            return _dbSet
                .Include(p => p.Categoria)
                .Where(p => p.Activo &&
                       (p.Codigo.ToLower().Contains(termino) ||
                        p.Nombre.ToLower().Contains(termino)))
                .ToList();
        }

        // Verificar si un código ya existe
        public bool ExisteCodigo(string codigo, int? idExcluir = null)
        {
            if (idExcluir.HasValue)
            {
                return _dbSet.Any(p => p.Codigo == codigo && p.Id != idExcluir.Value);
            }
            return _dbSet.Any(p => p.Codigo == codigo);
        }
    }
}
