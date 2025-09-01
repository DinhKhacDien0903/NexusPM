// <copyright file="SubscriptionStatus.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Enums;

/// <summary>
/// Represents the status of a subscription.
/// </summary>
public enum SubscriptionStatus
{
    /// <summary>
    /// The subscription is in a trial period.
    /// </summary>
    Trialing = 0,

    /// <summary>
    /// The subscription is active.
    /// </summary>
    Active = 1,

    /// <summary>
    /// The subscription payment is past due.
    /// </summary>
    PastDue = 2,

    /// <summary>
    /// The subscription has been canceled.
    /// </summary>
    Canceled = 3,

    /// <summary>
    /// The subscription is incomplete.
    /// </summary>
    Incomplete = 4,
}
