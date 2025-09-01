// <copyright file="Board.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a board entity that is scoped to a tenant and contains columns.
/// </summary>
public class Board : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the unique identifier of the tenant to which this board belongs.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the project to which this board belongs.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the project associated with this board.
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// Gets or sets the name of the board.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of columns associated with this board.
    /// </summary>
    public ICollection<BoardColumn> Columns { get; set; } = [];
}
