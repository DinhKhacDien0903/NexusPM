// <copyright file="IssueType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Enums;

/// <summary>
/// Represents the type of an issue in the system.
/// </summary>
public enum IssueType
{
    /// <summary>
    /// A user story, representing a feature or functionality.
    /// </summary>
    Story = 0,

    /// <summary>
    /// A task, representing a unit of work.
    /// </summary>
    Task = 1,

    /// <summary>
    /// A bug, representing a defect or issue in the system.
    /// </summary>
    Bug = 2,

    /// <summary>
    /// An epic, representing a large body of work that can be broken down into smaller tasks or stories.
    /// </summary>
    Epic = 3,

    /// <summary>
    /// A sub-task, representing a smaller unit of work within a task.
    /// </summary>
    SubTask = 4,
}
