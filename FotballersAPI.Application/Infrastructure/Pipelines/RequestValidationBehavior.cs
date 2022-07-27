using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotballersAPI.Application.Infrastructure.Pipelines
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errors = new List<ValidationFailure>();

            foreach (var validator in _validators)
            {
                var result = await validator.ValidateAsync(context, cancellationToken);
                errors.AddRange(result.Errors);
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}
