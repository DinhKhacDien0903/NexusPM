// <copyright file="UserTenantConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class UserTenantConfiguration : IEntityTypeConfiguration<UserTenant>
{

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<UserTenant> b)
    {
        b.HasIndex(x => new { x.TenantId, x.UserId }).IsUnique();
        b.HasOne(x => x.Tenant).WithMany(t => t.UserTenants).HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Cascade);
        b.HasOne(x => x.User).WithMany(u => u.Tenants).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
