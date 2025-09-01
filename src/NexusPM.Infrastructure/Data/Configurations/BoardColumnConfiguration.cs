// <copyright file="BoardColumnConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <inheritdoc/>
public class BoardColumnConfiguration : IEntityTypeConfiguration<BoardColumn>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<BoardColumn> builder)
    {
        builder.Property(x => x.Order).IsRequired();
        builder.Property(x => x.WipLimitNote).HasMaxLength(80);
        builder.HasOne(x => x.Board).WithMany(bd => bd.Columns).HasForeignKey(x => x.BoardId).OnDelete(DeleteBehavior.Cascade);
    }
}
