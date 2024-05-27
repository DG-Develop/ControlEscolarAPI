using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Commands
{
    public class CrearTipoPersonalCommand : IRequest<ApiResponse<string>>
    {
        public string Descripcion { get; set; } = null!;
        public bool TieneSueldo { get; set; }
        public decimal? SueldoMinimo { get; set; }
        public decimal? SueldoMaximo { get; set; }
        public string? IdentificadorDeControl { get; set; } 
    }

    public class CrearTipoPersonalCommandHandler : IRequestHandler<CrearTipoPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<TipoPersonal> _repositorio;

        public CrearTipoPersonalCommandHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ApiResponse<string>> Handle(CrearTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            TipoPersonal tipoPersonal = new TipoPersonal()
            {
                Descripcion = request.Descripcion,
                IdentificadorDeControl = request.IdentificadorDeControl ?? "",
                SueldoMaximo = request.SueldoMaximo ?? 0,
                SueldoMinimo = request.SueldoMinimo ?? 0,
                TieneSueldo = request.TieneSueldo,
            };

            await _repositorio.Agregar(tipoPersonal);

            return new ApiResponse<string>("¡Tipo personal creado correctamente!", "Creado.");
        }
    }
}
