using NexusPM.Domain.Entities;

namespace NexusPM.Domain.Events;

/// <summary>
/// Represents an event that is triggered when a new user signs up.
/// </summary>
public class SignedUpEvent(AppUser user)
    : BaseEvent
{
    /// <summary>
    /// Gets the user who has signed up.
    /// </summary>
    public AppUser User { get; } = user;
}