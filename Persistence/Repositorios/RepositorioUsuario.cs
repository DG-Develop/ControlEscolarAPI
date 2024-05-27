using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositorios
{
    internal class RepositorioUsuario : Repositorio<Usuario>, IRepositorioUsuario
    {

        public RepositorioUsuario(ControlEscolarDBContext controlEscolarDBContext) : base(controlEscolarDBContext)
        {
        }

        public async Task<Usuario?> ObtenerUsuarioPorCorreoYPassword(string Correo, string Password)
        {
            return await _controlEscolarDBContext.Set<Usuario>()
                .Where(usuario => usuario.CorreoElectronico == Correo && usuario.Password == Password)
                .FirstOrDefaultAsync();
        }
    }
}
