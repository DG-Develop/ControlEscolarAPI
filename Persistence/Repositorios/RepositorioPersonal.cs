using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositorios
{
    public class RepositorioPersonal : Repositorio<Personal>, IRepositorioPersonal
    {
        public RepositorioPersonal(ControlEscolarDBContext controlEscolarDBContext) : base(controlEscolarDBContext)
        {
        }

        public async Task<UpdatePersonalDTO> ObtenerPersonalPorNoControl(string NumeroControl)
        {
            var respuesta = await _controlEscolarDBContext.Personales.Where(p => p.NumeroControl == NumeroControl)
                .Select(p => new UpdatePersonalDTO
                {
                    NumeroControl = p.NumeroControl,
                    Apellidos = p.Apellidos,
                    CorreoElectronico = p.CorreoElectronico,
                    Estatus = p.Estatus,
                    FechaNacimiento = p.FechaNacimiento,
                    IdMiembroEscolar = p.IdMiembroEscolar,
                    IdTipoPersonal = p.IdTipoPersonal,
                    Nombre = p.Nombre,
                    Sueldo = p.Sueldo,
          
                    
                }).FirstOrDefaultAsync();

            if (respuesta == null)
            {
                throw new ApiException("No se encontro al personal");
            }

            return respuesta;
        }
    }
}
