// <copyright file="TenantConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the Tenant entity, defining database schema and relationships.
/// </summary>
public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(160).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(80).IsRequired();
        builder.Property(x => x.Domain).HasMaxLength(200);
        builder.HasIndex(x => x.Slug).IsUnique();
    }
}
