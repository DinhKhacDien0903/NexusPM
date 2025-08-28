namespace NexusPM.Infrastructure.Data.Configurations;
public class IssueTagConfiguration : IEntityTypeConfiguration<IssueTag>
{
    public void Configure(EntityTypeBuilder<IssueTag> builder)
    {
        builder.HasKey(x => new { x.TenantId, x.IssueId, x.TagId });
        builder.HasOne(x => x.Issue).WithMany(i => i.Tags).HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Tag).WithMany(t => t.Issues).HasForeignKey(x => x.TagId).OnDelete(DeleteBehavior.Cascade);
    }
}
