using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Se han producido uno o varios errores de validación")
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> fallos) : this()
        {
            foreach (var fallo in fallos)
            {
                Errors.Add(fallo.ErrorMessage);
            }
        }
    }
}
