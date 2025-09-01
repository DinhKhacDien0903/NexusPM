// <copyright file="BoardConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
        builder.HasOne(x => x.Project).WithMany(p => p.Boards).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
    }
}
