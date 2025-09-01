// <copyright file="MoneyMapping.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Helpers;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusPM.Domain.ValueObjects;

/// <summary>
/// Provides extension methods for mapping Money value objects in Entity Framework configurations.
/// </summary>
public static class MoneyMapping
{
    /// <summary>
    /// Configures the mapping for a Money value object property with appropriate precision and currency handling.
    /// </summary>
    /// <typeparam name="TEntity">The entity type that owns the Money property.</typeparam>
    /// <param name="owned">The owned navigation builder for the Money property.</param>
    /// <returns>The configured owned navigation builder.</returns>
    public static OwnedNavigationBuilder<TEntity, Money> MapMoney<TEntity>(this OwnedNavigationBuilder<TEntity, Money> owned)
where TEntity : class
    {
        owned.Property(p => p.Amount).HasPrecision(18, 2);
        owned.Property(p => p.Currency).HasMaxLength(7);
        return owned;
    }
}
