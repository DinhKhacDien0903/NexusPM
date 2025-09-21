using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPM.Domain.Common;

/// <summary>
/// Represents the base class for all entities, providing domain event support.
/// </summary>
public abstract class BaseEntity
{
    private readonly List<BaseEvent> domainEvents = [];

    /// <summary>
    /// Gets the collection of domain events associated with this entity.
    /// </summary>
    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => this.domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the entity.
    /// </summary>
    /// <param name="eventItem">The domain event to add.</param>
    public void AddDomainEvent(BaseEvent eventItem) => this.domainEvents.Add(eventItem);

    /// <summary>
    /// Removes a domain event from the entity.
    /// </summary>
    /// <param name="eventItem">The domain event to remove.</param>
    public void RemoveDomainEvent(BaseEvent eventItem) => this.domainEvents.Remove(eventItem);

    /// <summary>
    /// Clears all domain events from the entity.
    /// </summary>
    public void ClearDomainEvents() => this.domainEvents.Clear();
}
