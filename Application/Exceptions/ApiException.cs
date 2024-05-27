using System.Globalization;

namespace Application.Exceptions
{
    /// <summary>
    /// Clase personalizada para manejar excepciones específicas de la API.
    /// Hereda de la clase base <see cref="Exception"/>.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ApiException"/> sin mensaje.
        /// </summary>
        public ApiException() : base() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ApiException"/> con un mensaje especificado.
        /// </summary>
        /// <param name="mensaje">El mensaje que describe el error.</param>
        public ApiException(string mensaje) : base(mensaje) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ApiException"/> con un mensaje especificado y argumentos.
        /// </summary>
        /// <param name="mensaje">El mensaje que describe el error.</param>
        /// <param name="args">Una matriz de objetos que contiene cero o más argumentos de los que se puede formatear el mensaje.</param>
        public ApiException(string mensaje, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, mensaje, args)) { }
    }
}
