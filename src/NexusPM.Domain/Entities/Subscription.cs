// <copyright file="Subscription.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a subscription to a plan for a tenant, including billing periods and trial information.
/// </summary>
public class Subscription : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the plan identifier for this subscription.
    /// </summary>
    public Guid PlanId { get; set; }

    /// <summary>
    /// Gets or sets the plan navigation property.
    /// </summary>
    public Plan? Plan { get; set; }

    /// <summary>
    /// Gets or sets the number of seats included in the subscription. Defaults to 1.
    /// </summary>
    public int Seats { get; set; } = 1;

    /// <summary>
    /// Gets or sets the current status of the subscription. Defaults to Active.
    /// </summary>
    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;

    /// <summary>
    /// Gets or sets the UTC date when the subscription started. Defaults to current UTC time.
    /// </summary>
    public DateTime StartedOnUtc { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the UTC date when the current billing period started. Defaults to current UTC time.
    /// </summary>
    public DateTime CurrentPeriodStartUtc { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the UTC date when the current billing period ends. Defaults to one month from now.
    /// </summary>
    public DateTime CurrentPeriodEndUtc { get; set; } = DateTime.UtcNow.AddMonths(1);

    /// <summary>
    /// Gets or sets the UTC date when the trial period ends, if applicable.
    /// </summary>
    public DateTime? TrialEndsUtc { get; set; }
}
