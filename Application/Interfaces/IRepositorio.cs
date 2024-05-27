namespace Application.Interfaces
{
    /// <summary>
    /// Interfaz genérica de repositorio que define las operaciones básicas de CRUD (Create, Read, Update, Delete)
    /// para cualquier entidad en el contexto de la base de datos.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que maneja el repositorio.</typeparam>
    public interface IRepositorio<T> where T : class
    {
        /// <summary>
        /// Obtiene todas las entidades de tipo <typeparamref name="T"/> de la base de datos.
        /// </summary>
        /// <returns>Una lista de solo lectura de todas las entidades.</returns>
        Task<IReadOnlyList<T>> ObtenerTodos();

        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la entidad.</param>
        /// <returns>La entidad encontrada, o null si no se encuentra.</returns>
        Task<T?> ObtenerPorId(int id);

        /// <summary>
        /// Agrega una nueva entidad a la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a agregar.</param>
        /// <returns>La entidad agregada.</returns>
        Task<T?> Agregar(T entidad);

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a actualizar.</param>
        /// <returns>La entidad actualizada.</returns>
        Task<T?> Actualizar(T entidad);

        /// <summary>
        /// Elimina una entidad existente de la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task Eliminar(T entidad);
    }
}
