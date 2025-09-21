using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace NexusPM.Application.Common.Behaviours;

/// <summary>
/// Logs MediatR requests with user and request information.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
public class LoggingBehaviour<TRequest>(ILogger<LoggingBehaviour<TRequest>> logger)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    /// <summary>
    /// The logger action for requests.
    /// </summary>
    private static readonly Action<ILogger, string, string, string, TRequest, Exception?> LogRequest =
        LoggerMessage.Define<string, string, string, TRequest>(
            LogLevel.Information,
            new EventId(0, nameof(LoggingBehaviour<TRequest>)),
            "NexusPM Request: {Name} {UserId} {UserName} {@Request}");

    /// <summary>
    /// The logger instance.
    /// </summary>
    private readonly ILogger<LoggingBehaviour<TRequest>> logger = logger;

    /// <summary>
    /// Processes the request and logs its details.
    /// </summary>
    /// <param name="request">The request instance.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        // TODO: get user info
        // var userId = _user.Id ?? string.Empty;
        // string? userName = string.Empty;
        // if (!string.IsNullOrEmpty(userId))
        // {
        //    userName = await _identityService.GetUserNameAsync(userId);
        // }
        await Task.Delay(1, cancellationToken);
        LogRequest(this.logger, requestName, "FakeUserId", "FakeUserName", request, null);
    }
}
