// <copyright file="TagConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ColorHex).HasMaxLength(7);
        builder.HasIndex(x => new { x.TenantId, x.Name }).IsUnique();
    }
}
