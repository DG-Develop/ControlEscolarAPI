using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRepositorioUsuario
    {
        Task<Usuario?> ObtenerUsuarioPorCorreo(string Correo);
    }
}
