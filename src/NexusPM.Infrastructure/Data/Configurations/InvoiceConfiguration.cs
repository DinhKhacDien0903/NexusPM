// <copyright file="InvoiceConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(x => x.Number).HasMaxLength(40).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.Number }).IsUnique();

        builder.OwnsOne(x => x.Subtotal, owned => owned.MapMoney());
        builder.OwnsOne(x => x.Tax, owned => owned.MapMoney());
        builder.OwnsOne(x => x.Total, owned => owned.MapMoney());
    }
}
