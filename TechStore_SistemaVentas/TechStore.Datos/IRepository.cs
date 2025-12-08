using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TechStore.Datos
{
    /// <summary>
    /// Interfaz genérica para el patrón Repository
    /// Define las operaciones CRUD básicas
    /// </summary>
    public interface IRepository<T> where T : class
    {
        // Obtener todos los registros
        List<T> ObtenerTodos();

        // Obtener un registro por ID
        T ObtenerPorId(int id);

        // Buscar registros con condición
        List<T> Buscar(Expression<Func<T, bool>> predicado);

        // Agregar un nuevo registro
        bool Agregar(T entidad);

        // Actualizar un registro existente
        bool Actualizar(T entidad);

        // Eliminar un registro
        bool Eliminar(int id);

        // Eliminar un registro (por entidad)
        bool Eliminar(T entidad);

        // Verificar si existe un registro
        bool Existe(Expression<Func<T, bool>> predicado);

        // Contar registros
        int Contar(Expression<Func<T, bool>> predicado = null);
    }
}