using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Queries
{
    /// <summary>
    /// Consulta para obtener una lista de todos los tipos de personal.
    /// </summary>
    public class ObtenerTodoTipoPersonalQuery : IRequest<ApiResponse<List<TipoPersonalDTO>>>
    {
        public string? Descripcion { get; set; }
    }

    /// <summary>
    /// Manejador para la consulta <see cref="ObtenerTodoTipoPersonalQuery"/>.
    /// </summary>
    public class ObtenerTodoTipoPersonalQueryHandler : IRequestHandler<ObtenerTodoTipoPersonalQuery, ApiResponse<List<TipoPersonalDTO>>>
    {
        private readonly IRepositorio<TipoPersonal> _repositorio;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ObtenerTodoTipoPersonalQueryHandler"/>.
        /// </summary>
        /// <param name="repositorio">El repositorio para acceder a los datos del tipo de personal.</param>
        public ObtenerTodoTipoPersonalQueryHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Maneja la lógica para obtener una lista de todos los tipos de personal.
        /// </summary>
        /// <param name="request">La consulta con el filtro de descripción opcional.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API con la lista de tipos de personal.</returns>
        public async Task<ApiResponse<List<TipoPersonalDTO>>> Handle(ObtenerTodoTipoPersonalQuery request, CancellationToken cancellationToken)
        {
            // Obtiene la lista de todos los tipos de personal
            IReadOnlyList<TipoPersonal> listadoDelPersonal = await _repositorio.ObtenerTodos();

            // Filtra la lista por descripción si se proporciona
            if (!string.IsNullOrEmpty(request.Descripcion))
            {
                listadoDelPersonal = listadoDelPersonal.Where(listado => listado.Descripcion == request.Descripcion).ToList();
            }

            // Mapea los datos a una lista de DTOs
            List<TipoPersonalDTO> listadoDto = listadoDelPersonal.Select(listado => new TipoPersonalDTO
            {
                Descripcion = listado.Descripcion,
                IdTipoPersonal = listado.IdTipoPersonal,
                IdentificadorDeControl = listado.IdentificadorDeControl,
                SueldoMaximo = listado.SueldoMaximo,
                SueldoMinimo = listado.SueldoMinimo,
            }).ToList();

            // Devuelve una respuesta de la API con la lista de tipos de personal
            return new ApiResponse<List<TipoPersonalDTO>>(listadoDto, "Listado");
        }
    }
}
