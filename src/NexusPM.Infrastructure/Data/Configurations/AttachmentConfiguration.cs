// <copyright file="AttachmentConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the entity type for <see cref="Attachment"/>.
/// </summary>
public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    /// <summary>
    /// Configures the database schema for the <see cref="Attachment"/> entity.
    /// </summary>
    /// <param name="builder">The builder to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.Property(x => x.FileName)
            .HasMaxLength(260)
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.StorageKey)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(x => x.Issue)
            .WithMany(i => i.Attachments)
            .HasForeignKey(x => x.IssueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
