using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected readonly ControlEscolarDBContext _controlEscolarDBContext;

        public Repositorio(ControlEscolarDBContext controlEscolarDBContext)
        {
            _controlEscolarDBContext = controlEscolarDBContext;
        }

        public async Task<T?> Actualizar(T entidad)
        {
            _controlEscolarDBContext.Entry(entidad).State = EntityState.Modified;

            await _controlEscolarDBContext.SaveChangesAsync();

            return entidad;
        }

        public async Task<T?> Agregar(T entidad)
        {
            await _controlEscolarDBContext.Set<T>().AddAsync(entidad);

            await _controlEscolarDBContext.SaveChangesAsync();

            return entidad;
        }

        public async Task Eliminar(T entidad)
        {
            _controlEscolarDBContext.Remove(entidad);

            await _controlEscolarDBContext.SaveChangesAsync();
        }

        public async Task<T?> ObtenerPorId(int id)
        {
            return await _controlEscolarDBContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ObtenerTodos()
        {
            return await _controlEscolarDBContext.Set<T>().ToListAsync();
        }
    }
}
