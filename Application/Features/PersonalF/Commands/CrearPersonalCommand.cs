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

    public class CrearPersonalCommandHandler : IRequestHandler<CrearPersonalCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<Personal> _repositorio;
        private readonly ConnectionStringsSettings _connectionStrings;

        public CrearPersonalCommandHandler(IRepositorio<Personal> repositorio, IOptions<ConnectionStringsSettings> connectionStrings)
        {
            _repositorio = repositorio;
            _connectionStrings = connectionStrings.Value;
        }

        public async Task<ApiResponse<string>> Handle(CrearPersonalCommand request, CancellationToken cancellationToken)
        {

            string? numeroControl = await MiembroEscolarUtils.GenerarNumeroControl(request.IdentificadorDeControl, _connectionStrings.DefaultConnection);

            if(string.IsNullOrEmpty(numeroControl))
            {
                throw new ApiException("No se pudo generar al personal intente más tarde");
            }

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

            try
            {
            await _repositorio.Agregar(personal);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                throw;
            }


            return new ApiResponse<string>("¡Personal creado exitosamente!", "Creado");
        }
    }
}
