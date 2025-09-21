using MediatR;

namespace NexusPM.Domain.Common;

/// <summary>
/// Represents the base class for all domain events.
/// Implements <see cref="INotification"/> for MediatR event handling.
/// </summary>
public class BaseEvent : INotification
{
}
