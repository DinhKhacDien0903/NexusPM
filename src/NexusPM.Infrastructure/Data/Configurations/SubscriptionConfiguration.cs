namespace NexusPM.Infrastructure.Data.Configurations;
public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasOne(x => x.Plan).WithMany().HasForeignKey(x => x.PlanId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
    }
}
