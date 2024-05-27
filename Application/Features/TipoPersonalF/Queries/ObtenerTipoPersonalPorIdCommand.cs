using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.TipoPersonalF.Queries
{
    /// <summary>
    /// Comando para obtener los datos de un tipo de personal por su identificador.
    /// </summary>
    public class ObtenerTipoPersonalPorIdCommand : IRequest<ApiResponse<UpdateTipoPersonalDTO>>
    {
        public int IdTipoPersonal { get; set; }
    }

    /// <summary>
    /// Manejador para el comando <see cref="ObtenerTipoPersonalPorIdCommand"/>.
    /// </summary>
    public class ObtenerTipoPersonalPorIdCommandHandler : IRequestHandler<ObtenerTipoPersonalPorIdCommand, ApiResponse<UpdateTipoPersonalDTO>>
    {
        private readonly IRepositorio<TipoPersonal> _repositorio;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ObtenerTipoPersonalPorIdCommandHandler"/>.
        /// </summary>
        /// <param name="repositorio">El repositorio para acceder a los datos del tipo de personal.</param>
        public ObtenerTipoPersonalPorIdCommandHandler(IRepositorio<TipoPersonal> repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Maneja la lógica para obtener los datos de un tipo de personal por su identificador.
        /// </summary>
        /// <param name="request">El comando con el identificador del tipo de personal.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API con los datos del tipo de personal.</returns>
        public async Task<ApiResponse<UpdateTipoPersonalDTO>> Handle(ObtenerTipoPersonalPorIdCommand request, CancellationToken cancellationToken)
        {
            // Obtiene el tipo de personal por su ID
            TipoPersonal? tipoPersonal = await _repositorio.ObtenerPorId(request.IdTipoPersonal);

            if (tipoPersonal == null)
            {
                throw new KeyNotFoundException("Tipo Personal no encontrado");
            }

            // Mapea los datos del tipo de personal a un DTO
            UpdateTipoPersonalDTO dto = new UpdateTipoPersonalDTO()
            {
                Descripcion = tipoPersonal.Descripcion,
                IdentificadorDeControl = tipoPersonal.IdentificadorDeControl,
                SueldoMaximo = tipoPersonal.SueldoMaximo,
                SueldoMinimo = tipoPersonal.SueldoMinimo,
                IdTipoPersonal = tipoPersonal.IdTipoPersonal,
                TieneSueldo = tipoPersonal.TieneSueldo,
            };

            // Devuelve una respuesta de la API con los datos del tipo de personal
            return new ApiResponse<UpdateTipoPersonalDTO>(dto, "Listado.");
        }
    }
}
