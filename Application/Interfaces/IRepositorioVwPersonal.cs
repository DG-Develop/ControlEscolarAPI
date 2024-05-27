using Domain.Models;

namespace Application.Interfaces
{
    public interface IRepositorioVwPersonal
    {
        Task<List<VwPersonal>> ObtenerPaginacion(int NumeroPagina, int TotalPagina, string? NumeroControl);
    }
}
