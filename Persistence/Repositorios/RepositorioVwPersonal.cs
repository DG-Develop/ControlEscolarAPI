using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositorios
{
    public class RepositorioVwPersonal : IRepositorioVwPersonal
    {
        private readonly ControlEscolarDBContext _controlEscolarDBContext;

        public RepositorioVwPersonal(ControlEscolarDBContext controlEscolarDBContext)
        {
            _controlEscolarDBContext = controlEscolarDBContext;
        }

        public async Task<List<VwPersonal>> ObtenerPaginacion(int NumeroPagina, int TotalPagina, string? NumeroControl)
        {
            var query = _controlEscolarDBContext.VwPersonal.AsQueryable();

            if (!string.IsNullOrEmpty(NumeroControl))
            {
                 query = query.Where(vwP => vwP.NumeroControl.ToLower().Contains(NumeroControl.ToLower()));
            }

            var listado = await query
                .Skip((NumeroPagina - 1) * TotalPagina)
                .Take(TotalPagina)
                .ToListAsync();

            return listado;
        }
    }
}
