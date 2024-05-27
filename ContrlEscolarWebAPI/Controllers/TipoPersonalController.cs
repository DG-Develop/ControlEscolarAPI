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

        /// <summary>
        /// Obtiene una lista de tipos de personal, opcionalmente filtrada por descripción.
        /// </summary>
        /// <param name="Descripcion">Filtro para la descripción del tipo de personal.</param>
        /// <returns>Lista de tipos de personal que coinciden con el filtro.</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> ObtenerListadoPersonal([FromQuery] string? Descripcion = null)
        {
            var respuesta = await _mediator.Send(new ObtenerTodoTipoPersonalQuery { Descripcion = Descripcion });

            return Ok(respuesta);
        }

        /// <summary>
        /// Obtiene los detalles de un tipo de personal específico basado en su ID.
        /// </summary>
        /// <param name="id">Identificador del tipo de personal a obtener.</param>
        /// <returns>Detalles del tipo de personal solicitado.</returns>
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


        /// <summary>
        /// Crea un nuevo tipo de personal.
        /// </summary>
        /// <param name="command">Datos del tipo de personal a crear.</param>
        /// <returns>El objeto creado (en este caso un mensaje de confirmación de la creación) junto con el código HTTP 201 (Created).</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> CrearTipoPersonal([FromBody] CrearTipoPersonalCommand command)
        {
            var respuesta = await _mediator.Send(command);

            return Created("", respuesta);
        }

        /// <summary>
        /// Actualiza un tipo de personal existente.
        /// </summary>
        /// <param name="id">Identificador del tipo de personal a actualizar.</param>
        /// <param name="command">Datos actualizados del tipo de personal.</param>
        /// <returns>El mensaje de confirmación de la actualización.</returns>
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

        /// <summary>
        /// Elimina un tipo de personal específico basado en su ID.
        /// </summary>
        /// <param name="id">Identificador del tipo de personal a eliminar.</param>
        /// <returns>Mensaje de confirmación de la eliminación.</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> EliminarTipoPersonal([FromRoute] int id)
        {

            var respuesta = await _mediator.Send(new EliminaTipoPersonalCommand { IdTipoPersonal = id });

            return Ok(respuesta);
        }
    }
}
