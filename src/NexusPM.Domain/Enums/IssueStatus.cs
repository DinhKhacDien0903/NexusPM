// <copyright file="IssueStatus.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Enums;

/// <summary>
/// Represents the status of an issue in the project management system.
/// </summary>
public enum IssueStatus
{
    /// <summary>
    /// The issue is in the backlog and not yet planned for work.
    /// </summary>
    Backlog = 0,

    /// <summary>
    /// The issue is planned and ready to be worked on.
    /// </summary>
    Todo = 1,

    /// <summary>
    /// The issue is currently being worked on.
    /// </summary>
    InProgress = 2,

    /// <summary>
    /// The issue is under review after development.
    /// </summary>
    InReview = 3,

    /// <summary>
    /// The issue has been completed successfully.
    /// </summary>
    Done = 4,

    /// <summary>
    /// The issue is blocked and cannot proceed.
    /// </summary>
    Blocked = 5,

    /// <summary>
    /// The issue has been canceled and will not be worked on.
    /// </summary>
    Canceled = 6,
}
