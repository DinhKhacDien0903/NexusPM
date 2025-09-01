// <copyright file="SprintConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class SprintConfiguration : IEntityTypeConfiguration<Sprint>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Sprint> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
        builder.HasOne(x => x.Project).WithMany(p => p.Sprints).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
    }
}
