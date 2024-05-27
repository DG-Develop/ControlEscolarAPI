using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRepositorioUsuario
    {
        Task<Usuario?> ObtenerUsuarioPorCorreoYPassword(string Correo, string Password);
    }
}
