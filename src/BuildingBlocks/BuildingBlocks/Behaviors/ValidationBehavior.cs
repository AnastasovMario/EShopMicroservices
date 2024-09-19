using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    //In this class we implement IPipeline behaviour interface and checks for all validation errors in the upcoming request
    public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> //checks for all the validation errors in the upcoming request
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            //Checks for validation errors and completes when all the validators are ran through
            var validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            //we get the errors
            var failures =
                validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next(); // very crucial for continuing the pipeline
        }
    }
}
