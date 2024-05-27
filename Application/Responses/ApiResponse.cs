namespace Application.Responses
{
    /// <summary>
    /// Clase genérica para manejar las respuestas de la API.
    /// </summary>
    /// <typeparam name="T">Tipo de datos que contiene el resultado de la respuesta de la API.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool Exitoso { get; set; }

        /// <summary>
        /// Mensaje descriptivo de la respuesta.
        /// </summary>
        public string Mensaje { get; set; } = null!;

        /// <summary>
        /// Lista de errores asociados con la respuesta.
        /// </summary>
        public List<string> Errores { get; set; } = new List<string>();

        /// <summary>
        /// Resultado de la operación de la API.
        /// </summary>
        public T? Resultado { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ApiResponse{T}"/>.
        /// </summary>
        public ApiResponse() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ApiResponse{T}"/> con un resultado exitoso y un mensaje opcional.
        /// </summary>
        /// <param name="resultado">El resultado de la operación.</param>
        /// <param name="mensaje">El mensaje descriptivo de la operación. Por defecto es una cadena vacía.</param>
        public ApiResponse(T resultado, string mensaje = "")
        {
            Exitoso = true;
            Mensaje = mensaje;
            Resultado = resultado;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ApiResponse{T}"/> con un mensaje de error.
        /// </summary>
        /// <param name="mensaje">El mensaje descriptivo del error.</param>
        public ApiResponse(string mensaje)
        {
            Exitoso = false;
            Mensaje = mensaje;
        }
    }
}
