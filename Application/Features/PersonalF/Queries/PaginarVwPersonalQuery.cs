using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using MediatR;

namespace Application.Features.PersonalF.Queries
{
    /// <summary>
    /// Consulta para paginar los registros de la vista del personal.
    /// </summary>
    public class PaginarVwPersonalQuery : IRequest<PaginacionResponse<List<VwPersonal>>>
    {
        public int TotalPagina { get; set; }
        public int NumeroPagina { get; set; }
        public string? NumeroControl { get; set; }
    }

    /// <summary>
    /// Manejador para la consulta <see cref="PaginarVwPersonalQuery"/>.
    /// </summary>
    public class PaginaVwPersonalQueryHandler : IRequestHandler<PaginarVwPersonalQuery, PaginacionResponse<List<VwPersonal>>>
    {
        private readonly IRepositorioVwPersonal _repositorioVwPersonal;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PaginaVwPersonalQueryHandler"/>.
        /// </summary>
        /// <param name="repositorioVwPersonal">El repositorio para acceder a los datos de la vista del personal.</param>
        public PaginaVwPersonalQueryHandler(IRepositorioVwPersonal repositorioVwPersonal)
        {
            _repositorioVwPersonal = repositorioVwPersonal;
        }

        /// <summary>
        /// Maneja la lógica para paginar los registros de la vista del personal.
        /// </summary>
        /// <param name="request">La consulta con los parámetros de paginación y filtro.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta paginada de la API con los registros de la vista del personal.</returns>
        public async Task<PaginacionResponse<List<VwPersonal>>> Handle(PaginarVwPersonalQuery request, CancellationToken cancellationToken)
        {
            // Obtiene los registros paginados de la vista del personal
            var listado = await _repositorioVwPersonal.ObtenerPaginacion(request.NumeroPagina, request.TotalPagina, request.NumeroControl);

            return new PaginacionResponse<List<VwPersonal>>(listado, request.NumeroPagina, request.TotalPagina);
        }
    }
}
