using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositorios
{
    /// <summary>
    /// Clase genérica de repositorio que implementa las operaciones básicas de CRUD (Create, Read, Update, Delete)
    /// para cualquier entidad en el contexto de la base de datos ControlEscolar.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio.</typeparam>
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        /// <summary>
        /// Contexto de la base de datos ControlEscolar.
        /// </summary>
        protected readonly ControlEscolarDBContext _controlEscolarDBContext;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Repositorio{T}"/>.
        /// </summary>
        /// <param name="controlEscolarDBContext">El contexto de la base de datos.</param>
        public Repositorio(ControlEscolarDBContext controlEscolarDBContext)
        {
            _controlEscolarDBContext = controlEscolarDBContext;
        }

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a actualizar.</param>
        /// <returns>La entidad actualizada.</returns>
        public async Task<T?> Actualizar(T entidad)
        {
            _controlEscolarDBContext.Entry(entidad).State = EntityState.Modified;
            await _controlEscolarDBContext.SaveChangesAsync();
            return entidad;
        }

        /// <summary>
        /// Agrega una nueva entidad a la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a agregar.</param>
        /// <returns>La entidad agregada.</returns>
        public async Task<T?> Agregar(T entidad)
        {
            await _controlEscolarDBContext.Set<T>().AddAsync(entidad);
            await _controlEscolarDBContext.SaveChangesAsync();
            return entidad;
        }

        /// <summary>
        /// Elimina una entidad existente de la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task Eliminar(T entidad)
        {
            _controlEscolarDBContext.Remove(entidad);
            await _controlEscolarDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la entidad.</param>
        /// <returns>La entidad encontrada, o null si no se encuentra.</returns>
        public async Task<T?> ObtenerPorId(int id)
        {
            return await _controlEscolarDBContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Obtiene todas las entidades de tipo <typeparamref name="T"/> de la base de datos.
        /// </summary>
        /// <returns>Una lista de solo lectura de todas las entidades.</returns>
        public async Task<IReadOnlyList<T>> ObtenerTodos()
        {
            return await _controlEscolarDBContext.Set<T>().ToListAsync();
        }
    }
}
