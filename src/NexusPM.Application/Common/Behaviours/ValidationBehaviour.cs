using MediatR;
using ValidationException = NexusPM.Application.Common.Exceptions.ValidationException;

namespace NexusPM.Application.Common.Behaviours;

/// <summary>
/// Pipeline behavior that validates requests using registered <see cref="IValidator{TRequest}"/>s.
/// Throws a <see cref="ValidationException"/> if validation fails.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="ValidationBehaviour{TRequest, TResponse}"/> class.
/// </remarks>
public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    /// <summary>
    /// Handles the request by validating it before passing to the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The request instance.</param>
    /// <param name="next">The next handler delegate.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The response from the next handler.</returns>
    /// <exception cref="ValidationException">Thrown if validation fails.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken)));

            var failures = validationResults
                .Where(vr => vr.Errors.Count != 0)
                .SelectMany(vr => vr.Errors)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }

        return await next(cancellationToken);
    }
}