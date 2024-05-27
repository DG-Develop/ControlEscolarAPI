namespace Application.Responses
{
    /// <summary>
    /// Clase genérica para manejar las respuestas paginadas de la API.
    /// Hereda de <see cref="ApiResponse{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de datos que contiene el resultado de la respuesta de la API.</typeparam>
    public class PaginacionResponse<T> : ApiResponse<T>
    {
        /// <summary>
        /// Número de la página actual.
        /// </summary>
        public int NumeroPagina { get; set; }

        /// <summary>
        /// Tamaño de la página (cantidad de registros por página).
        /// </summary>
        public int TamanioPagina { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PaginacionResponse{T}"/> con el resultado, número de página y tamaño de página especificados.
        /// </summary>
        /// <param name="resultado">El resultado de la operación.</param>
        /// <param name="numeroPagina">El número de la página actual.</param>
        /// <param name="tamanioPagina">El tamaño de la página (cantidad de registros por página).</param>
        public PaginacionResponse(T resultado, int numeroPagina, int tamanioPagina)
        {
            NumeroPagina = numeroPagina;
            TamanioPagina = tamanioPagina;
            Resultado = resultado;
            Mensaje = "";
            Exitoso = true;
            Errores = new List<string>();
        }
    }
}
