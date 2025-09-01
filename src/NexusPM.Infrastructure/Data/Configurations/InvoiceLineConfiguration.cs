// <copyright file="InvoiceLineConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.Property(x => x.Description).HasMaxLength(240).IsRequired();
        builder.HasOne(x => x.Invoice).WithMany(i => i.Lines).HasForeignKey(x => x.InvoiceId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Issue).WithMany().HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.SetNull);

        builder.OwnsOne(x => x.UnitPricePerHour, owned => owned.MapMoney());
        builder.OwnsOne(x => x.LineTotal, owned => owned.MapMoney());
    }
}
