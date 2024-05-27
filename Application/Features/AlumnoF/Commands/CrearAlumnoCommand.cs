using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.AlumnoF.Commands
{
    public class CrearAlumnoCommand : IRequest<ApiResponse<string>>
    {
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string NumeroControl { get; set; } = null!;
        public bool Estatus { get; set; }
        public int IdTipoPersonal { get; set; }
        public string Grado { get; set; } = null!;
    }

    public class CrearAlumnoCommandHandler : IRequestHandler<CrearAlumnoCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<Alumno> _repositorio;

        public CrearAlumnoCommandHandler(IRepositorio<Alumno> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ApiResponse<string>> Handle(CrearAlumnoCommand request, CancellationToken cancellationToken)
        {
            Alumno alumno = new Alumno()
            {
                Apellidos = request.Apellidos,
                CorreoElectronico = request.CorreoElectronico,
                Estatus = request.Estatus,
                FechaNacimiento = request.FechaNacimiento,
                IdTipoPersonal = request.IdTipoPersonal,
                Nombre = request.Nombre,
                NumeroControl = request.NumeroControl,
                Grado = request.Grado,
            };

            await _repositorio.Agregar(alumno);

            return new ApiResponse<string>("¡Alumno creado exitosamente!", "Creado");
        }
    }
}
