// <copyright file="CommentConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(x => x.Body).IsRequired();
        builder.HasOne(x => x.Issue).WithMany(i => i.Comments).HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Author).WithMany().HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
    }
}
