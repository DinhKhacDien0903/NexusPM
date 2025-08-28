namespace NexusPM.Infrastructure.Data.Configurations;
public class SprintConfiguration : IEntityTypeConfiguration<Sprint>
{
    public void Configure(EntityTypeBuilder<Sprint> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
        builder.HasOne(x => x.Project).WithMany(p => p.Sprints).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
    }
}
