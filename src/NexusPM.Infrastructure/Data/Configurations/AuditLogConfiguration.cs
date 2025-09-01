// <copyright file="AuditLogConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.Property(x => x.ResourceType).HasMaxLength(60).IsRequired();
        builder.Property(x => x.Action).HasMaxLength(40).IsRequired();
        builder.Property(x => x.OldValuesJson).HasColumnType("jsonb");
        builder.Property(x => x.NewValuesJson).HasColumnType("jsonb");
    }
}
