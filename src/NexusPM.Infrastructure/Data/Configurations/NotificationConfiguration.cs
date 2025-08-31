namespace NexusPM.Infrastructure.Data.Configurations;
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(x => x.Type).HasMaxLength(80).IsRequired();
        builder.Property(x => x.PayloadJson).HasMaxLength(8000).IsRequired();
        builder.HasOne(x => x.Recipient).WithMany().HasForeignKey(x => x.RecipientUserId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
    }
}
