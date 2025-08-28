namespace NexusPM.Infrastructure.Data.Configurations;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ColorHex).HasMaxLength(7);
        builder.HasIndex(x => new { x.TenantId, x.Name }).IsUnique();
    }
}
