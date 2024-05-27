using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Queries
{
    public class ObtenerTipoPersonalPorIdCommand : IRequest<ApiResponse<UpdateTipoPersonalDTO>>
    {
        public int IdTipoPersonal { get; set; }
    }

    public class ObtenerTipoPersonalPorIdCommandHandler : IRequestHandler<ObtenerTipoPersonalPorIdCommand, ApiResponse<UpdateTipoPersonalDTO>>
    {
        private readonly IRepositorio<TipoPersonal> _repositorio;

        public ObtenerTipoPersonalPorIdCommandHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ApiResponse<UpdateTipoPersonalDTO>> Handle(ObtenerTipoPersonalPorIdCommand request, CancellationToken cancellationToken)
        {
            TipoPersonal? tipoPersonal = await _repositorio.ObtenerPorId(request.IdTipoPersonal);

            if (tipoPersonal == null) 
            {
                throw new KeyNotFoundException("Tipo Personal no encontrado");
            }

            UpdateTipoPersonalDTO dto = new UpdateTipoPersonalDTO()
            {
                 Descripcion = tipoPersonal.Descripcion,
                 IdentificadorDeControl = tipoPersonal.IdentificadorDeControl,
                 SueldoMaximo = tipoPersonal.SueldoMaximo,
                 SueldoMinimo  = tipoPersonal.SueldoMinimo,
                 IdTipoPersonal = tipoPersonal.IdTipoPersonal,
                 TieneSueldo = tipoPersonal.TieneSueldo,
            };

            return new ApiResponse<UpdateTipoPersonalDTO>(dto, "Listado.");
        }
    }
}
