using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.PersonalF.Commands
{
    public class EliminarPersonalCommand : IRequest<ApiResponse<string>>
    {
        public string NumeroControl { get; set; } = null!;
    }

    public class EliminarPersonalCommandHanlder : IRequestHandler<EliminarPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<MiembroEscolar> _repositorio;
        private readonly IRepositorioPersonal _repositorioPersonal;

        public EliminarPersonalCommandHanlder(IRepositorio<MiembroEscolar> repositorio, IRepositorioPersonal repositorioPersonal)
        {
            _repositorio = repositorio;
            _repositorioPersonal = repositorioPersonal;
        }

        public async Task<ApiResponse<string>> Handle(EliminarPersonalCommand request, CancellationToken cancellationToken)
        {
            var personalDto = await _repositorioPersonal.ObtenerPersonalPorNoControl(request.NumeroControl);

            MiembroEscolar? personal = await _repositorio.ObtenerPorId(personalDto.IdMiembroEscolar);

            if (personal == null)
            {
                throw new ApiException("El personal no fue encontrado.");
            }

            await _repositorio.Eliminar(personal);

            return new ApiResponse<string>("¡Personal eliminado correctamente!", "Eliminado");
        }
    }
}
