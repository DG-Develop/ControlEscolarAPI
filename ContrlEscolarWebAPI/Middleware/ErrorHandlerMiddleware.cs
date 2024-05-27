using Application.Exceptions;
using Application.Responses;
using System.Net;
using System.Text.Json;

namespace ContrlEscolarWebAPI.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {

                var respuesta = context.Response;

                var respuestaModelo = new ApiResponse<string>()
                {
                    Exitoso = false,
                    Mensaje = error.Message,
                };

                switch(error)
                {
                    case ApiException e:
                        respuesta.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest); 
                        break;
                    case ValidationException e:
                        respuesta.StatusCode= Convert.ToInt32(HttpStatusCode.BadRequest);
                        respuestaModelo.Errores = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        respuesta.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                        break;
                    default:
                        respuesta.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                        break;
                }

                var resultado = JsonSerializer.Serialize(respuestaModelo);

                respuesta.ContentType = "application/json";

                await respuesta.WriteAsync(resultado);
            }
        }
    }
}
