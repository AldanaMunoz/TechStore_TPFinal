using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace TechStore.Datos
{
    /// <summary>
    /// Implementación genérica del patrón Repository
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TechStoreContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository()
        {
            _context = new TechStoreContext();
            _dbSet = _context.Set<T>();
        }

        public List<T> ObtenerTodos()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los registros: {ex.Message}", ex);
            }
        }

        public T ObtenerPorId(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el registro por ID: {ex.Message}", ex);
            }
        }

        public List<T> Buscar(Expression<Func<T, bool>> predicado)
        {
            try
            {
                return _dbSet.Where(predicado).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar registros: {ex.Message}", ex);
            }
        }

        public bool Agregar(T entidad)
        {
            try
            {
                _dbSet.Add(entidad);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el registro: {ex.Message}", ex);
            }
        }

        public bool Actualizar(T entidad)
        {
            try
            {
                // If entity with same key is already tracked by the context, update its current values
                var entry = _context.Entry(entidad);

                if (entry.State == EntityState.Detached)
                {
                    // Try to get primary key value (convention: property named "Id")
                    var idProp = entidad.GetType().GetProperty("Id");
                    if (idProp != null)
                    {
                        var idVal = idProp.GetValue(entidad);

                        // Look for a local tracked entity with the same key
                        var local = _dbSet.Local.Cast<object>().FirstOrDefault(l =>
                        {
                            var p = l.GetType().GetProperty("Id");
                            return p != null && object.Equals(p.GetValue(l), idVal);
                        });

                        if (local != null)
                        {
                            // Update tracked entity's values from the provided entity
                            _context.Entry(local).CurrentValues.SetValues(entidad);
                            _context.SaveChanges();
                            return true;
                        }
                        else
                        {
                            // Not tracked yet, attach and set modified
                            _dbSet.Attach(entidad);
                            _context.Entry(entidad).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        // No Id property found, fall back to attaching
                        _dbSet.Attach(entidad);
                        _context.Entry(entidad).State = EntityState.Modified;
                    }
                }
                else
                {
                    // Already tracked; just ensure state is Modified
                    entry.State = EntityState.Modified;
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el registro: {ex.Message}", ex);
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                T entidad = ObtenerPorId(id);
                if (entidad != null)
                {
                    return Eliminar(entidad);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el registro: {ex.Message}", ex);
            }
        }

        public bool Eliminar(T entidad)
        {
            try
            {
                _dbSet.Remove(entidad);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el registro: {ex.Message}", ex);
            }
        }

        public bool Existe(Expression<Func<T, bool>> predicado)
        {
            try
            {
                return _dbSet.Any(predicado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar existencia: {ex.Message}", ex);
            }
        }

        public int Contar(Expression<Func<T, bool>> predicado = null)
        {
            try
            {
                if (predicado == null)
                    return _dbSet.Count();
                else
                    return _dbSet.Count(predicado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al contar registros: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
