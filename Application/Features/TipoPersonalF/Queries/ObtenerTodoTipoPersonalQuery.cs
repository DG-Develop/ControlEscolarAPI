using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Queries
{
    public class ObtenerTodoTipoPersonalQuery : IRequest<ApiResponse<List<TipoPersonalDTO>>>
    {
        public string? Descripcion { get; set; }
    }

    public class ObtenerTodoTipoPersonalQueryHandler : IRequestHandler<ObtenerTodoTipoPersonalQuery, ApiResponse<List<TipoPersonalDTO>>>
    {
        private readonly IRepositorio<TipoPersonal> _repositorio;

        public ObtenerTodoTipoPersonalQueryHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ApiResponse<List<TipoPersonalDTO>>> Handle(ObtenerTodoTipoPersonalQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<TipoPersonal> listadoDelPersonal = await _repositorio.ObtenerTodos();

            if (string.IsNullOrEmpty(request.Descripcion))
            {
                listadoDelPersonal.Where(listado => listado.Descripcion == request.Descripcion); 
            }

            List<TipoPersonalDTO> listadoDto = listadoDelPersonal.Select(listado => new TipoPersonalDTO
            {
                Descripcion = listado.Descripcion,
                IdTipoPersonal = listado.IdTipoPersonal,
                IdentificadorDeControl = listado.IdentificadorDeControl,
                SueldoMaximo = listado.SueldoMaximo,
                SueldoMinimo = listado.SueldoMinimo,

            }).ToList();

            return new ApiResponse<List<TipoPersonalDTO>>(listadoDto, "Listado");
        }
    }
}
