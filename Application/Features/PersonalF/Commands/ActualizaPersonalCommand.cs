using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.PersonalF.Commands
{
    public class ActualizaPersonalCommand : IRequest<ApiResponse<string>>
    {
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string NumeroControl { get; set; } = null!;
        public bool Estatus { get; set; }
        public int IdTipoPersonal { get; set; }
        public decimal Sueldo { get; set; }
        public int IdMiembroEscolar { get; set; }
    }

    public class ActualizaPersonalCommandHandler : IRequestHandler<ActualizaPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<Personal> _repositorio;

        public ActualizaPersonalCommandHandler(IRepositorio<Personal> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ApiResponse<string>> Handle(ActualizaPersonalCommand request, CancellationToken cancellationToken)
        {
            Personal? personal = await _repositorio.ObtenerPorId(request.IdMiembroEscolar);

            if (personal == null)
            {
                throw new ApiException("El personal no fue encontrado.");
            }

            personal.Sueldo = request.Sueldo;
            personal.NumeroControl = request.NumeroControl; 
            personal.Apellidos = request.Apellidos;
            personal.CorreoElectronico = request.CorreoElectronico;
            personal.Estatus = request.Estatus;
            personal.FechaNacimiento = request.FechaNacimiento;
            personal.IdTipoPersonal = request.IdTipoPersonal;
            personal.Nombre = request.Nombre;

            await _repositorio.Actualizar(personal);

            return new ApiResponse<string>("¡El personal fue actualizado correctamente!.", "Actualizado.");
        }
    }
}
