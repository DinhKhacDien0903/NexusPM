namespace NexusPM.Infrastructure.Data.Configurations;
public class BoardColumnConfiguration : IEntityTypeConfiguration<BoardColumn>
{
    public void Configure(EntityTypeBuilder<BoardColumn> builder)
    {
        builder.Property(x => x.Order).IsRequired();
        builder.Property(x => x.WipLimitNote).HasMaxLength(80);
        builder.HasOne(x => x.Board).WithMany(bd => bd.Columns).HasForeignKey(x => x.BoardId).OnDelete(DeleteBehavior.Cascade);
    }
}
