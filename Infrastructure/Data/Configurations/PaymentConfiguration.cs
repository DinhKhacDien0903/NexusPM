namespace NexusPM.Infrastructure.Data.Configurations;
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(x => x.Provider).HasMaxLength(40).IsRequired();
        builder.Property(x => x.ProviderRef).HasMaxLength(120).IsRequired();
        builder.HasOne(x => x.Invoice).WithMany(i => i.Payments).HasForeignKey(x => x.InvoiceId).OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(x => x.Amount, owned => owned.MapMoney());
    }
}
