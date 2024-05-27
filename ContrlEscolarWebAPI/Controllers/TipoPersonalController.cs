using Application.Features.TipoPersonalF.Commands;
using Application.Features.TipoPersonalF.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContrlEscolarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TipoPersonalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TipoPersonalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> ObtenerListadoPersonal([FromQuery] string? Descripcion = null)
        {
            var respuesta = await _mediator.Send(new ObtenerTodoTipoPersonalQuery { Descripcion = Descripcion });

            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> ObtenerTipoPersonalPorId([FromRoute] int id)
        {
            var respuesta = await _mediator.Send(new ObtenerTipoPersonalPorIdCommand
            {
                IdTipoPersonal = id
            });

            return Ok(respuesta);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> CrearTipoPersonal([FromBody] CrearTipoPersonalCommand command)
        {
            var respuesta = await _mediator.Send(command);

            return Created("", respuesta);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> ActualizaTipoPersonal([FromRoute] int id, [FromBody] ActualizaTipoPersonalCommand command)
        {
            if(id != command.IdTipoPersonal)
            {
                return BadRequest("Las identificacciones no coinciden");
            }

            var respuesta = await _mediator.Send(command);

            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> EliminarTipoPersonal([FromRoute] int id)
        {

            var respuesta = await _mediator.Send(new EliminaTipoPersonalCommand { IdTipoPersonal = id });

            return Ok(respuesta);
        }
    }
}
