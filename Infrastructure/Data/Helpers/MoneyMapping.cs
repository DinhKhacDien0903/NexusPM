namespace NexusPM.Infrastructure.Data.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusPM.Domain.ValueObjects;

public static class MoneyMapping
{
    public static OwnedNavigationBuilder<TEntity, Money> MapMoney<TEntity>(this OwnedNavigationBuilder<TEntity, Money> owned)
where TEntity : class
    {
        owned.Property(p => p.Amount).HasPrecision(18, 2);
        owned.Property(p => p.Currency).HasMaxLength(7);
        return owned;
    }
}
