// <copyright file="IssueConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the Issue entity, defining database schema and relationships.
/// </summary>
public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.Property(x => x.Key).HasMaxLength(30).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(240).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.Key }).IsUnique();
        builder.HasIndex(x => new { x.TenantId, x.ProjectId, x.Number }).IsUnique();

        builder.HasOne(x => x.Project).WithMany(p => p.Issues).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Sprint).WithMany().HasForeignKey(x => x.SprintId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.Assignee).WithMany().HasForeignKey(x => x.AssigneeId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.Reporter).WithMany().HasForeignKey(x => x.ReporterId).OnDelete(DeleteBehavior.SetNull);
    }
}
