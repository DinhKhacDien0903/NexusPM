namespace NexusPM.Infrastructure.Data.Configurations;
public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.Property(x => x.FileName).HasMaxLength(260).IsRequired();
        builder.Property(x => x.ContentType).HasMaxLength(120).IsRequired();
        builder.Property(x => x.StorageKey).HasMaxLength(500).IsRequired();
        builder.HasOne(x => x.Issue).WithMany(i => i.Attachments).HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.Cascade);
    }
}
