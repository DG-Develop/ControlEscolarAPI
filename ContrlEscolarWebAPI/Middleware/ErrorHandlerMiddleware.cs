using Application.Exceptions;
using Application.Responses;
using System.Net;
using System.Text.Json;

namespace ContrlEscolarWebAPI.Middleware
{
    /// <summary>
    /// Middleware para manejar errores globales en la aplicación.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ErrorHandlerMiddleware"/>.
        /// </summary>
        /// <param name="next">El siguiente delegado de middleware en la canalización de solicitud HTTP.</param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoca el middleware para manejar errores en la solicitud HTTP.
        /// </summary>
        /// <param name="context">El contexto de la solicitud HTTP.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var respuesta = context.Response;

                var respuestaModelo = new ApiResponse<string>
                {
                    Exitoso = false,
                    Mensaje = error.Message,
                };

                switch (error)
                {
                    case ApiException e:
                        // Maneja excepciones de la API
                        respuesta.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:
                        // Maneja excepciones de validación
                        respuesta.StatusCode = (int)HttpStatusCode.BadRequest;
                        respuestaModelo.Errores = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        // Maneja excepciones de clave no encontrada
                        respuesta.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // Maneja cualquier otra excepción no específica
                        respuesta.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var resultado = JsonSerializer.Serialize(respuestaModelo);
                respuesta.ContentType = "application/json";
                await respuesta.WriteAsync(resultado);
            }
        }
    }
}
