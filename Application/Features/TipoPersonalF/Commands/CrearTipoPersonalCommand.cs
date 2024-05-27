using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Commands
{
    /// <summary>
    /// Comando para crear un nuevo tipo de personal.
    /// </summary>
    public class CrearTipoPersonalCommand : IRequest<ApiResponse<string>>
    {
        public string Descripcion { get; set; } = null!;
        public bool TieneSueldo { get; set; }
        public decimal? SueldoMinimo { get; set; }
        public decimal? SueldoMaximo { get; set; }
        public string? IdentificadorDeControl { get; set; } 
    }

    /// <summary>
    /// Manejador para el comando <see cref="CrearTipoPersonalCommand"/>.
    /// </summary>
    public class CrearTipoPersonalCommandHandler : IRequestHandler<CrearTipoPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<TipoPersonal> _repositorio;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CrearTipoPersonalCommandHandler"/>.
        /// </summary>
        /// <param name="repositorio">El repositorio para acceder a los datos del tipo de personal.</param>
        public CrearTipoPersonalCommandHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Maneja la lógica para crear un nuevo tipo de personal.
        /// </summary>
        /// <param name="request">El comando con los datos del nuevo tipo de personal.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API indicando el resultado de la operación.</returns>
        public async Task<ApiResponse<string>> Handle(CrearTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            // Crea una nueva instancia de TipoPersonal con los datos del comando
            TipoPersonal tipoPersonal = new TipoPersonal()
            {
                Descripcion = request.Descripcion,
                IdentificadorDeControl = request.IdentificadorDeControl ?? "",
                SueldoMaximo = request.SueldoMaximo ?? 0,
                SueldoMinimo = request.SueldoMinimo ?? 0,
                TieneSueldo = request.TieneSueldo,
            };

            // Agrega el nuevo tipo de personal al repositorio
            await _repositorio.Agregar(tipoPersonal);

            // Devuelve una respuesta indicando que el tipo de personal fue creado correctamente
            return new ApiResponse<string>("¡Tipo personal creado correctamente!", "Creado.");
        }
    }
}
