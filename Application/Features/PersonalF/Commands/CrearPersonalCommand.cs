using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Application.Utils;
using Domain.Entities;
using Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Features.PersonalF.Commands
{
    /// <summary>
    /// Comando para crear un nuevo miembro del personal.
    /// </summary>
    public class CrearPersonalCommand : IRequest<ApiResponse<string>>
    {
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public int IdTipoPersonal { get; set; }
        public string IdentificadorDeControl { get; set; } = null!;
        public decimal Sueldo { get; set; }
    }

    /// <summary>
    /// Manejador para el comando <see cref="CrearPersonalCommand"/>.
    /// </summary>
    public class CrearPersonalCommandHandler : IRequestHandler<CrearPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorioPersonal _repositorio;
        private readonly ConnectionStringsSettings _connectionStrings;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CrearPersonalCommandHandler"/>.
        /// </summary>
        /// <param name="repositorio">El repositorio para acceder a los datos del personal.</param>
        /// <param name="connectionStrings">Configuración de las cadenas de conexión.</param>
        public CrearPersonalCommandHandler(IRepositorioPersonal repositorio, IOptions<ConnectionStringsSettings> connectionStrings)
        {
            _repositorio = repositorio;
            _connectionStrings = connectionStrings.Value;
        }

        /// <summary>
        /// Maneja la lógica para crear un nuevo miembro del personal.
        /// </summary>
        /// <param name="request">El comando con los datos del nuevo personal.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API indicando el resultado de la operación.</returns>
        public async Task<ApiResponse<string>> Handle(CrearPersonalCommand request, CancellationToken cancellationToken)
        {
            // Verifica si el correo electrónico ya está registrado
            var existeCorreo = await _repositorio.ExisteCorreroRegistrado(request.CorreoElectronico);
            if (existeCorreo)
            {
                throw new ApiException("El correo ya se encuentra registrado");
            }

            // Genera el número de control
            string? numeroControl = await MiembroEscolarUtils.GenerarNumeroControl(request.IdentificadorDeControl, _connectionStrings.DefaultConnection);
            if (string.IsNullOrEmpty(numeroControl))
            {
                throw new ApiException("No se pudo generar el personal, intente más tarde");
            }

            // Crea la nueva entidad de personal
            Personal personal = new Personal()
            {
                Apellidos = request.Apellidos,
                CorreoElectronico = request.CorreoElectronico,
                Estatus = request.Estatus,
                FechaNacimiento = request.FechaNacimiento,
                IdTipoPersonal = request.IdTipoPersonal,
                Nombre = request.Nombre,
                NumeroControl = numeroControl,
                Sueldo = request.Sueldo,
            };

            // Intenta agregar el nuevo personal al repositorio
            try
            {
                await _repositorio.Agregar(personal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            // Devuelve una respuesta indicando que el personal fue creado exitosamente
            return new ApiResponse<string>("¡Personal creado exitosamente!", "Creado");
        }
    }
}
