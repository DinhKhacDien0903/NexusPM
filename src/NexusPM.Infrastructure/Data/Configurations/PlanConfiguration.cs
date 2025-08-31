namespace NexusPM.Infrastructure.Data.Configurations;
public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.Property(x => x.Code).HasMaxLength(60).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(160).IsRequired();
        builder.HasIndex(x => x.Code).IsUnique();

        builder.OwnsOne(x => x.PricePerSeatMonthly, owned => owned.MapMoney());
        builder.OwnsOne(x => x.PricePerSeatYearly, owned => owned.MapMoney()).Navigation(x => x.PricePerSeatYearly).IsRequired(false);
    }
}
