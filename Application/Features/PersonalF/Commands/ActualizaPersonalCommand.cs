using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.PersonalF.Commands
{

    /// <summary>
    /// Comando para actualizar los datos de un miembro del personal.
    /// </summary>
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

    /// <summary>
    /// Manejador para el comando <see cref="ActualizaPersonalCommand"/>.
    /// </summary>
    public class ActualizaPersonalCommandHandler : IRequestHandler<ActualizaPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<Personal> _repositorio;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ActualizaPersonalCommandHandler"/>.
        /// </summary>
        /// <param name="repositorio">El repositorio para acceder a los datos del personal.</param>
        public ActualizaPersonalCommandHandler(IRepositorio<Personal> repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Maneja la lógica para actualizar los datos de un miembro del personal.
        /// </summary>
        /// <param name="request">El comando con los datos a actualizar.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API indicando el resultado de la operación.</returns>
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

            return new ApiResponse<string>("¡El personal fue actualizado correctamente!", "Actualizado.");
        }
    }
}
