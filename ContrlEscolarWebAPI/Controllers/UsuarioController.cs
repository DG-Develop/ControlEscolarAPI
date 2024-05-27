using Application.Features.UsuarioF;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationUsuarioCommand command)
        {
            var respuesta = await _mediator.Send(command);

            return Ok(respuesta);
        }
    }
}
