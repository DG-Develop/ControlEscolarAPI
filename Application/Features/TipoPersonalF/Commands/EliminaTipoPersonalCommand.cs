using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Commands
{
    public class EliminaTipoPersonalCommand : IRequest<ApiResponse<string>>
    {
        public int IdTipoPersonal { get; set; }
    }

    public class EliminaTipoPersonalCommandHandler : IRequestHandler<EliminaTipoPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<TipoPersonal> _repositorio;

        public EliminaTipoPersonalCommandHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ApiResponse<string>> Handle(EliminaTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            TipoPersonal? tipoPersonal = await _repositorio.ObtenerPorId(request.IdTipoPersonal);

            if(tipoPersonal == null)
            {
                throw new ApiException("No se encontro el tipo de personal a eliminar");
            }

            await _repositorio.Eliminar(tipoPersonal);

            return new ApiResponse<string>("¡Registro eliminado exitosamente!", "Eliminado");
        }
    }
}
