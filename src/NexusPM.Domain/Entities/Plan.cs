// <copyright file="Plan.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a subscription plan with pricing and feature limits.
/// </summary>
public class Plan : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique code identifier for the plan.
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// Gets or sets the display name of the plan.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the optional description of the plan.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the monthly price per seat. Defaults to $0 USD.
    /// </summary>
    public Money PricePerSeatMonthly { get; set; } = new Money(0, "USD");

    /// <summary>
    /// Gets or sets the yearly price per seat, if applicable.
    /// </summary>
    public Money? PricePerSeatYearly { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of seats allowed, if any.
    /// </summary>
    public int? MaxSeats { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of projects allowed, if any.
    /// </summary>
    public int? MaxProjects { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of active issues allowed, if any.
    /// </summary>
    public int? MaxActiveIssues { get; set; }
}
