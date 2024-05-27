using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.AlumnoF.Commands
{
    public class ActualizaAlumnoCommand : IRequest<ApiResponse<string>>
    {
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string NumeroControl { get; set; } = null!;
        public bool Estatus { get; set; }
        public int IdTipoPersonal { get; set; }
        public string Grado { get; set; } = null!;
        public int IdMiembroEscolar { get; set; }
    }

    public class ActualizaAlumnoCommandHandler : IRequestHandler<ActualizaAlumnoCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<Alumno> _repositorio;

        public ActualizaAlumnoCommandHandler(IRepositorio<Alumno> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ApiResponse<string>> Handle(ActualizaAlumnoCommand request, CancellationToken cancellationToken)
        {
            Alumno? alumno = await _repositorio.ObtenerPorId(request.IdMiembroEscolar);

            if (alumno == null)
            {
                throw new ApiException("El alumno no fue encontrado.");
            }

            alumno.Grado = request.Grado;
            alumno.NumeroControl = request.NumeroControl;
            alumno.Apellidos = request.Apellidos;
            alumno.CorreoElectronico = request.CorreoElectronico;
            alumno.Estatus = request.Estatus;
            alumno.FechaNacimiento = request.FechaNacimiento;
            alumno.IdTipoPersonal = request.IdTipoPersonal;
            alumno.Nombre = request.Nombre;

            await _repositorio.Actualizar(alumno);

            return new ApiResponse<string>("¡El alumno fue actualizado correctamente!.", "Actualizado.");
        }
    }
}
