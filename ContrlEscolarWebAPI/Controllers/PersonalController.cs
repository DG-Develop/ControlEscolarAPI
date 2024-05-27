using Application.Features.PersonalF.Commands;
using Application.Features.PersonalF.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContrlEscolarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("paginar-personal")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> PaginarPersonal([FromQuery] int TotalPagina = 10, [FromQuery] int NumeroPagina = 1, [FromQuery] string? NumeroControl = null)
        {
            var respuesta = await _mediator.Send(new PaginarVwPersonalQuery { NumeroControl = NumeroControl, NumeroPagina = NumeroPagina, TotalPagina = TotalPagina });

            return Ok(respuesta);
        }

        [HttpGet("{NumeroControl}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> ObtenerPersonalPorNumeroControl([FromRoute] string NumeroControl)
        {
            var respuesta = await _mediator.Send(new ObtenerPersonalPorNoControlQuery { NumeroControl = NumeroControl });

            return Ok(respuesta);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> CrearPersonal([FromBody] CrearPersonalCommand command)
        {
            var respuesta = await _mediator.Send(command);

            return Created("", respuesta);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> ActualizarPersonal([FromRoute] int id, [FromBody] ActualizaPersonalCommand command)
        {
            if(id != command.IdMiembroEscolar)
            {
                return BadRequest("Los identificadores no coinciden");
            }
            var respuesta = await _mediator.Send(command);

            return Ok(respuesta);
        }

        [HttpDelete("{NumeroControl}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> EliminarPersonal([FromRoute] string NumeroControl)
        {

            var respuesta = await _mediator.Send(new EliminarPersonalCommand { NumeroControl = NumeroControl });

            return Ok(respuesta);
        }
    }
}
