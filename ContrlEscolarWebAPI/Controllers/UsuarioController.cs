﻿using Application.Features.UsuarioF;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContrlEscolarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Maneja la autenticación de un usuario devuelve un modelo de usuario con el 
        /// accessToken generado con JWT si las credenciales son válidas.
        /// </summary>
        /// <param name="command">Credenciales de autenticación del usuario.</param>
        /// <returns>Devuelve un modelo de usuario.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationUsuarioCommand command)
        {
            var respuesta = await _mediator.Send(command);

            return Ok(respuesta);
        }
    }
}
