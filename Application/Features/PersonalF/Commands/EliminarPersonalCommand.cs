using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.PersonalF.Commands
{
    /// <summary>
    /// Comando para eliminar un miembro del personal.
    /// </summary>
    public class EliminarPersonalCommand : IRequest<ApiResponse<string>>
    {
        public string NumeroControl { get; set; } = null!;
    }

    /// <summary>
    /// Manejador para el comando <see cref="EliminarPersonalCommand"/>.
    /// </summary>
    public class EliminarPersonalCommandHandler : IRequestHandler<EliminarPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<MiembroEscolar> _repositorio;
        private readonly IRepositorioPersonal _repositorioPersonal;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EliminarPersonalCommandHandler"/>.
        /// </summary>
        /// <param name="repositorio">El repositorio para acceder a los datos del miembro escolar.</param>
        /// <param name="repositorioPersonal">El repositorio para acceder a los datos del personal.</param>
        public EliminarPersonalCommandHandler(IRepositorio<MiembroEscolar> repositorio, IRepositorioPersonal repositorioPersonal)
        {
            _repositorio = repositorio;
            _repositorioPersonal = repositorioPersonal;
        }

        /// <summary>
        /// Maneja la lógica para eliminar un miembro del personal.
        /// </summary>
        /// <param name="request">El comando con los datos del personal a eliminar.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API indicando el resultado de la operación.</returns>
        public async Task<ApiResponse<string>> Handle(EliminarPersonalCommand request, CancellationToken cancellationToken)
        {
            // Obtiene el DTO del personal por su número de control
            var personalDto = await _repositorioPersonal.ObtenerPersonalPorNoControl(request.NumeroControl);

            // Obtiene la entidad del miembro escolar por su ID
            MiembroEscolar? personal = await _repositorio.ObtenerPorId(personalDto.IdMiembroEscolar);

            if (personal == null)
            {
                throw new ApiException("El personal no fue encontrado.");
            }

            // Elimina el miembro escolar
            await _repositorio.Eliminar(personal);

            // Devuelve una respuesta indicando que el personal fue eliminado correctamente
            return new ApiResponse<string>("¡Personal eliminado correctamente!", "Eliminado");
        }
    }
}
