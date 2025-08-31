namespace NexusPM.Infrastructure.Data.Configurations;
public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.Property(x => x.ResourceType).HasMaxLength(60).IsRequired();
        builder.Property(x => x.Action).HasMaxLength(40).IsRequired();
        builder.Property(x => x.OldValuesJson).HasColumnType("jsonb");
        builder.Property(x => x.NewValuesJson).HasColumnType("jsonb");
    }
}
