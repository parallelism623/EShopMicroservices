using BuildingBlocks.Exceptions;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.PipelineBehaviors;
public class ValidationPipelineBehavior<TRequest, TResponse> (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var results = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var errors = results
            .Where(x => x.Errors.Any())
            .SelectMany(x => x.Errors)
            .ToList();

        if(errors.Any())
        {
            throw new ValidationErrorException(errors.FirstOrDefault()!.ErrorMessage);
        }

        return await next();
    }
}
