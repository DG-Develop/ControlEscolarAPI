using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositorios
{
    public class RepositorioVwAlumno : IRepositorioVwAlumno
    {
        private readonly ControlEscolarDBContext _controlEscolarDBContext;

        public RepositorioVwAlumno(ControlEscolarDBContext controlEscolarDBContext) 
        {
            _controlEscolarDBContext = controlEscolarDBContext;
        }

        public async Task<List<VwAlumno>> ObtenerPaginacion(int NumeroPagina, int TotalPagina, string? NumeroControl)
        {
            var query = _controlEscolarDBContext.VwAlumno.AsQueryable();

            if (!string.IsNullOrEmpty(NumeroControl))
            {
                query = query.Where(vwP => vwP.NumeroControl == NumeroControl);
            }

            var listado = await query
                .Skip((NumeroPagina - 1) * TotalPagina)
                .Take(TotalPagina)
                .ToListAsync();

            return listado;
        }
    }
}
