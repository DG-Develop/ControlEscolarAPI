using Domain.Models;

namespace Application.Interfaces
{
    public interface IRepositorioVwAlumno
    {
        Task<List<VwAlumno>> ObtenerPaginacion(int NumeroPagina, int TotalPagina, string? NumeroControl);
    }
}
