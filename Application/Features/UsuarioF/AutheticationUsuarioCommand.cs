using Application.Exceptions;
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
    /// <summary>
    /// Comando para autenticar un usuario.
    /// </summary>
    public class AuthenticationUsuarioCommand : IRequest<ApiResponse<UsuarioModel>>
    {
        public string CorreoElectronico { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    /// <summary>
    /// Manejador para el comando <see cref="AuthenticationUsuarioCommand"/>.
    /// </summary>
    public class AuthenticationUsuarioCommandHandler : IRequestHandler<AuthenticationUsuarioCommand, ApiResponse<UsuarioModel>>
    {
        private readonly IRepositorioUsuario _repositorio;
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AuthenticationUsuarioCommandHandler"/>.
        /// </summary>
        /// <param name="repositorio">El repositorio para acceder a los datos del usuario.</param>
        /// <param name="jwtSettings">Configuración de JWT.</param>
        public AuthenticationUsuarioCommandHandler(IRepositorioUsuario repositorio, IOptions<JwtSettings> jwtSettings)
        {
            _repositorio = repositorio;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Maneja la lógica para autenticar un usuario.
        /// </summary>
        /// <param name="request">El comando con las credenciales del usuario.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API con los datos del usuario autenticado.</returns>
        public async Task<ApiResponse<UsuarioModel>> Handle(AuthenticationUsuarioCommand request, CancellationToken cancellationToken)
        {
            // Obtiene el usuario por su correo y contraseña
            Usuario? usuario = await _repositorio.ObtenerUsuarioPorCorreoYPassword(request.CorreoElectronico, request.Password);

            if (usuario == null)
            {
                throw new ApiException("Credenciales incorrectas o Usuario no existente");
            }

            // Crea las reclamaciones (claims) para el token JWT
            var claims = new List<Claim>
        {
            new Claim("username", usuario.CorreoElectronico),
            new Claim("role", usuario.TipoUsuario),
        };

            // Crea un token de seguridad
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

            // Firma el token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Crea el modelo de usuario autenticado con el token
            UsuarioModel usuarioAuth = new UsuarioModel
            {
                AccessToken = tokenString,
                Id = usuario.IdUsuario,
                Role = usuario.TipoUsuario,
                Username = usuario.CorreoElectronico,
            };

            // Devuelve una respuesta de la API con los datos del usuario autenticado
            return new ApiResponse<UsuarioModel>(usuarioAuth, "Login Completado");
        }
    }
}
