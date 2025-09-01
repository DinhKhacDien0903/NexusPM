// <copyright file="BoardColumn.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a column on a board, which is used to organize and manage issues.
/// </summary>
public class BoardColumn : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the unique identifier of the tenant that owns this column.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the board to which this column belongs.
    /// </summary>
    public Guid BoardId { get; set; }

    /// <summary>
    /// Gets or sets the board entity associated with this column.
    /// </summary>
    public Board? Board { get; set; }

    /// <summary>
    /// Gets or sets the status of issues in this column.
    /// </summary>
    public IssueStatus Status { get; set; } = IssueStatus.Todo;

    /// <summary>
    /// Gets or sets the order of this column in the board.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Gets or sets the note for the Work In Progress (WIP) limit of this column.
    /// </summary>
    public string? WipLimitNote { get; set; }
}
