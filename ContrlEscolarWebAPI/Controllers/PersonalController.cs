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

        /// <summary>
        /// Pagina los registros de personal.
        /// </summary>
        /// <param name="TotalPagina">Número de registros por página.</param>
        /// <param name="NumeroPagina">Número de la página a obtener.</param>
        /// <param name="NumeroControl">Número de control del personal para filtrar los resultados.</param>
        /// <returns>Lista paginada de registros del personal.</returns>
        [HttpGet("paginar-personal")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> PaginarPersonal([FromQuery] int TotalPagina = 10, [FromQuery] int NumeroPagina = 1, [FromQuery] string? NumeroControl = null)
        {
            var respuesta = await _mediator.Send(new PaginarVwPersonalQuery { NumeroControl = NumeroControl, NumeroPagina = NumeroPagina, TotalPagina = TotalPagina });

            return Ok(respuesta);
        }

        /// <summary>
        /// Obtiene los detalles de un personal específico basado en su número de control.
        /// </summary>
        /// <param name="NumeroControl">Número de control del personal.</param>
        /// <returns>Detalles del personal solicitado.</returns>
        [HttpGet("{NumeroControl}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> ObtenerPersonalPorNumeroControl([FromRoute] string NumeroControl)
        {
            var respuesta = await _mediator.Send(new ObtenerPersonalPorNoControlQuery { NumeroControl = NumeroControl });

            return Ok(respuesta);
        }

        /// <summary>
        /// Crea un nuevo registro de personal.
        /// </summary>
        /// <param name="command">Datos del personal a crear.</param>
        /// <returns>El objeto creado (en este caso un mensaje de confirmación de la creación) junto con el código HTTP 201 (Created).</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> CrearPersonal([FromBody] CrearPersonalCommand command)
        {
            var respuesta = await _mediator.Send(command);

            return Created("", respuesta);
        }

        /// <summary>
        /// Actualiza un registro de personal existente.
        /// </summary>
        /// <param name="id">Identificador del miembro escolar a actualizar.</param>
        /// <param name="command">Datos actualizados del personal.</param>
        /// <returns>El mensaje de confirmación de la actualización.</returns>
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

        /// <summary>
        /// Elimina un registro de personal específico basado en su número de control.
        /// </summary>
        /// <param name="NumeroControl">Número de control del personal a eliminar.</param>
        /// <returns>Mensaje de confirmación de la eliminación.</returns>
        [HttpDelete("{NumeroControl}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> EliminarPersonal([FromRoute] string NumeroControl)
        {

            var respuesta = await _mediator.Send(new EliminarPersonalCommand { NumeroControl = NumeroControl });

            return Ok(respuesta);
        }
    }
}
