using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where  TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var contexto = new ValidationContext<TRequest>(request);
                var resultadosValidacion = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(contexto, cancellationToken)));
                var fallos = resultadosValidacion.SelectMany(respuesta => respuesta.Errors).Where(fallo => fallo != null).ToList();
                if(fallos.Count != 0)
                {
                    throw new ValidationException(fallos);
                }
            }
            
            return await next();
        }
    }
}
