namespace Application.Interfaces
{
    public interface IRepositorio<T> where T : class
    {
        Task<IReadOnlyList<T>> ObtenerTodos();
        Task<T?> ObtenerPorId(int id);
        Task<T?> Agregar(T entidad);
        Task<T?> Actualizar(T entidad);
        Task Eliminar(T entidad);
    }
}
