using Application.DTOs;

namespace Application.Interfaces
{
    public interface IRepositorioPersonal
    {
        Task<UpdatePersonalDTO> ObtenerPersonalPorNoControl(string NumeroControl);
    }
}
