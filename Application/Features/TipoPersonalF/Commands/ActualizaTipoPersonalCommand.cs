using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Commands
{
    /// <summary>
    /// Comando para actualizar los datos de un tipo de personal.
    /// </summary>
    public class ActualizaTipoPersonalCommand : IRequest<ApiResponse<string>>
    {
        public int IdTipoPersonal { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool TieneSueldo { get; set; }
        public decimal? SueldoMinimo { get; set; }
        public decimal? SueldoMaximo { get; set; }
        public string? IdentificadorDeControl { get; set; }
    }

    /// <summary>
    /// Manejador para el comando <see cref="ActualizaTipoPersonalCommand"/>.
    /// </summary>
    public class ActualizaTipoPersonalCommandHandler : IRequestHandler<ActualizaTipoPersonalCommand, ApiResponse<string>>
    {
        private IRepositorio<TipoPersonal> _repositorio;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ActualizaTipoPersonalCommandHandler"/>.
        /// </summary>
        /// <param name="repositorio">El repositorio para acceder a los datos del tipo de personal.</param>
        public ActualizaTipoPersonalCommandHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Maneja la lógica para actualizar los datos de un tipo de personal.
        /// </summary>
        /// <param name="request">El comando con los datos a actualizar.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API indicando el resultado de la operación.</returns>
        public async Task<ApiResponse<string>> Handle(ActualizaTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            // Obtiene el tipo de personal por su ID
            TipoPersonal? tipoPersonal = await _repositorio.ObtenerPorId(request.IdTipoPersonal);

            if (tipoPersonal == null)
            {
                throw new ApiException("No se encontró el tipo de personal a actualizar");
            }

            // Actualiza los datos del tipo de personal
            tipoPersonal.TieneSueldo = request.TieneSueldo;
            tipoPersonal.SueldoMinimo = request.SueldoMinimo ?? 0;
            tipoPersonal.SueldoMaximo = request.SueldoMaximo ?? 0;
            tipoPersonal.Descripcion = request.Descripcion;
            tipoPersonal.IdentificadorDeControl = request.IdentificadorDeControl ?? "";

            // Guarda los cambios en el repositorio
            await _repositorio.Actualizar(tipoPersonal);

            // Devuelve una respuesta indicando que la actualización fue completada
            return new ApiResponse<string>("¡Actualización Completada!", "Actualizado");
        }
    }
}
