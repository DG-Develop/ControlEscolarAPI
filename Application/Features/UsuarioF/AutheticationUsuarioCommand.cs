using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Domain.Models;
using Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Features.UsuarioF
{
    public class AuthenticationUsuarioCommand : IRequest<ApiResponse<UsuarioModel>>
    {
        public string CorreoElectronico { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class AuthenticationUsuarioCommandHandler : IRequestHandler<AuthenticationUsuarioCommand, ApiResponse<UsuarioModel>>
    {
        private readonly IRepositorioUsuario _repositorio;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationUsuarioCommandHandler(IRepositorioUsuario repositorio, IOptions<JwtSettings> jwtSettings)
        {
            _repositorio = repositorio;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ApiResponse<UsuarioModel>> Handle(AuthenticationUsuarioCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _repositorio.ObtenerUsuarioPorCorreo(request.CorreoElectronico);

            if (usuario == null)
            {
                throw new KeyNotFoundException($"El usuario no fue econtrado");
            }

            // * Agregó contenido al Json Web Token (JWT)
            var claims = new List<Claim>()
            {
                new Claim("username", usuario.CorreoElectronico),
                new Claim("role", usuario.TipoUsuario),
            };


            // * Creo un token de seguridad
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                    SecurityAlgorithms.HmacSha256
                )
            );


            // * Firmo el token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            UsuarioModel usuarioAuth = new UsuarioModel()
            {
                AccessToken = tokenString,
                Id = usuario.IdUsuario,
                Role = usuario.TipoUsuario,
                Username = usuario.CorreoElectronico,
            };

            return new ApiResponse<UsuarioModel>(usuarioAuth, "Login Completado");
        }
    }
}
