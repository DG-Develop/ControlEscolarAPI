using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRepositorioPersonal: IRepositorio<Personal>
    {
        Task<UpdatePersonalDTO> ObtenerPersonalPorNoControl(string NumeroControl);
        Task<bool> ExisteCorreroRegistrado(string CorreoElectronico);
    }
}
