using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using MediatR;

namespace Application.Features.AlumnoF.Queries
{
    public  class PaginaVwAlumnoQuery : IRequest<PaginacionResponse<List<VwAlumno>>>
    {
        public int TotalPagina { get; set; }
        public int NumeroPagina { get; set; }
        public string? NumeroControl { get; set; }
    }

    public class PaginaVwAlumnoQueryHandler : IRequestHandler<PaginaVwAlumnoQuery, PaginacionResponse<List<VwAlumno>>>
    {
        private readonly IRepositorioVwAlumno _repositorioVwAlumno;

        public PaginaVwAlumnoQueryHandler(IRepositorioVwAlumno repositorioVwAlumno)
        {
            _repositorioVwAlumno = repositorioVwAlumno;
        }

        public async Task<PaginacionResponse<List<VwAlumno>>> Handle(PaginaVwAlumnoQuery request, CancellationToken cancellationToken)
        {
            var listado = await _repositorioVwAlumno.ObtenerPaginacion(request.NumeroPagina, request.TotalPagina, request.NumeroControl);

            return new PaginacionResponse<List<VwAlumno>>(listado, request.NumeroPagina, request.TotalPagina);
        }
    }
}
