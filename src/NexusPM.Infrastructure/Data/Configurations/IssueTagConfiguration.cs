// <copyright file="IssueTagConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class IssueTagConfiguration : IEntityTypeConfiguration<IssueTag>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<IssueTag> builder)
    {
        builder.HasKey(x => new { x.TenantId, x.IssueId, x.TagId });
        builder.HasOne(x => x.Issue).WithMany(i => i.Tags).HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Tag).WithMany(t => t.Issues).HasForeignKey(x => x.TagId).OnDelete(DeleteBehavior.Cascade);
    }
}
