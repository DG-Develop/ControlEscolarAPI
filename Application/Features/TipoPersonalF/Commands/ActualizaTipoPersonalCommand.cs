using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Commands
{
    public class ActualizaTipoPersonalCommand : IRequest<ApiResponse<string>>
    {
        public int IdTipoPersonal { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool TieneSueldo { get; set; }
        public decimal? SueldoMinimo { get; set; }
        public decimal? SueldoMaximo { get; set; }
        public string? IdentificadorDeControl { get; set; }
    }

    public class ActualizaTipoPersonalCommandHandler : IRequestHandler<ActualizaTipoPersonalCommand, ApiResponse<string>>
    {
        private IRepositorio<TipoPersonal> _repositorio;

        public ActualizaTipoPersonalCommandHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ApiResponse<string>> Handle(ActualizaTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            TipoPersonal? tipoPersonal = await _repositorio.ObtenerPorId(request.IdTipoPersonal);

            if(tipoPersonal == null)
            {
                throw new ApiException("No se encontro el tipo de personal a actualizar");
            }

            tipoPersonal.TieneSueldo = request.TieneSueldo;
            tipoPersonal.SueldoMinimo = request.SueldoMinimo ?? 0;
            tipoPersonal.SueldoMaximo = request.SueldoMaximo ?? 0;
            tipoPersonal.Descripcion = request.Descripcion;
            tipoPersonal.IdentificadorDeControl = request.IdentificadorDeControl ?? "";

            await _repositorio.Actualizar(tipoPersonal);


            return new ApiResponse<string>("¡Actualización Completada!", "Actualizado");
        }
    }
}
