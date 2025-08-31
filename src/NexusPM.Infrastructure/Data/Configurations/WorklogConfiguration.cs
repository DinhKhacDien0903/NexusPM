namespace NexusPM.Infrastructure.Data.Configurations;

public class WorklogConfiguration : IEntityTypeConfiguration<Worklog>
{
    public void Configure(EntityTypeBuilder<Worklog> builder)
    {
        builder.Property(x => x.Note).HasMaxLength(240);
        builder.HasOne(x => x.Issue).WithMany(i => i.Worklogs).HasForeignKey(x => x.IssueId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.InvoiceLine).WithMany().HasForeignKey(x => x.InvoiceLineId).OnDelete(DeleteBehavior.Restrict);

        // Owned Money (nullable)
        builder.OwnsOne(x => x.HourlyRate, owned => owned.MapMoney()).Navigation(x => x.HourlyRate).IsRequired(false);
    }
}
