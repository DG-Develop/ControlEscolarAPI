using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using MediatR;

namespace Application.Features.PersonalF.Queries
{
    public class PaginarVwPersonalQuery : IRequest<PaginacionResponse<List<VwPersonal>>>
    {
        public int TotalPagina { get; set; }
        public int NumeroPagina { get; set; }
        public string? NumeroControl { get; set; }
    }

    public class PaginaVwPersonalQueryHandler : IRequestHandler<PaginarVwPersonalQuery, PaginacionResponse<List<VwPersonal>>>
    {
        private readonly IRepositorioVwPersonal _repositorioVwPersonal;

        public PaginaVwPersonalQueryHandler(IRepositorioVwPersonal repositorioVwPersonal)
        {
            _repositorioVwPersonal = repositorioVwPersonal;
        }

        public async Task<PaginacionResponse<List<VwPersonal>>> Handle(PaginarVwPersonalQuery request, CancellationToken cancellationToken)
        {
            var listado = await _repositorioVwPersonal.ObtenerPaginacion(request.NumeroPagina, request.TotalPagina, request.NumeroControl);

            return new PaginacionResponse<List<VwPersonal>>(listado, request.NumeroPagina, request.TotalPagina);
        }
    }
}
