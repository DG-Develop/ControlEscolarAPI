using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.PersonalF.Queries
{
    /// <summary>
    /// Consulta para obtener los detalles de un miembro del personal basado en su número de control.
    /// </summary>
    public class ObtenerPersonalPorNoControlQuery : IRequest<ApiResponse<UpdatePersonalDTO>>
    {
        public string NumeroControl { get; set; } = null!;
    }

    /// <summary>
    /// Manejador para la consulta <see cref="ObtenerPersonalPorNoControlQuery"/>.
    /// </summary>
    public class ObtenerPersonalPorNoControlQueryHandler : IRequestHandler<ObtenerPersonalPorNoControlQuery, ApiResponse<UpdatePersonalDTO>>
    {
        private readonly IRepositorioPersonal _repositorioPersonal;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ObtenerPersonalPorNoControlQueryHandler"/>.
        /// </summary>
        /// <param name="repositorioPersonal">El repositorio para acceder a los datos del personal.</param>
        public ObtenerPersonalPorNoControlQueryHandler(IRepositorioPersonal repositorioPersonal)
        {
            _repositorioPersonal = repositorioPersonal;
        }

        /// <summary>
        /// Maneja la lógica para obtener los detalles de un miembro del personal basado en su número de control.
        /// </summary>
        /// <param name="request">La consulta con el número de control del personal.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API con los detalles del personal.</returns>
        public async Task<ApiResponse<UpdatePersonalDTO>> Handle(ObtenerPersonalPorNoControlQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repositorioPersonal.ObtenerPersonalPorNoControl(request.NumeroControl);

            return new ApiResponse<UpdatePersonalDTO>(dto);
        }
    }
}
