// <copyright file="SubscriptionConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasOne(x => x.Plan).WithMany().HasForeignKey(x => x.PlanId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
    }
}
