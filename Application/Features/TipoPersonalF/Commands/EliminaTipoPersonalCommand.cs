using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Commands
{
    /// <summary>
    /// Comando para eliminar un tipo de personal.
    /// </summary>
    public class EliminaTipoPersonalCommand : IRequest<ApiResponse<string>>
    {
        public int IdTipoPersonal { get; set; }
    }

    /// <summary>
    /// Manejador para el comando <see cref="EliminaTipoPersonalCommand"/>.
    /// </summary>
    public class EliminaTipoPersonalCommandHandler : IRequestHandler<EliminaTipoPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<TipoPersonal> _repositorio;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EliminaTipoPersonalCommandHandler"/>.
        /// </summary>
        /// <param name="repositorio">El repositorio para acceder a los datos del tipo de personal.</param>
        public EliminaTipoPersonalCommandHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Maneja la lógica para eliminar un tipo de personal.
        /// </summary>
        /// <param name="request">El comando con el identificador del tipo de personal a eliminar.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API indicando el resultado de la operación.</returns>
        public async Task<ApiResponse<string>> Handle(EliminaTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            // Obtiene el tipo de personal por su ID
            TipoPersonal? tipoPersonal = await _repositorio.ObtenerPorId(request.IdTipoPersonal);

            if (tipoPersonal == null)
            {
                throw new ApiException("No se encontró el tipo de personal a eliminar");
            }

            // Elimina el tipo de personal
            await _repositorio.Eliminar(tipoPersonal);

            // Devuelve una respuesta indicando que el registro fue eliminado exitosamente
            return new ApiResponse<string>("¡Registro eliminado exitosamente!", "Eliminado");
        }
    }
}
