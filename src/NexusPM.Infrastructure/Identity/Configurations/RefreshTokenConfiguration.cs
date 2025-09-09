// <copyright file="RefreshTokenConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructurbuilder.Identity.Configurations
{
    /// <inheritdoc/>
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasIndex(x => x.TokenHash).IsUnique();
            builder.Property(x => x.TokenHash).HasMaxLength(256);
            builder.Property(x => x.JwtId).HasMaxLength(64);
            builder.Property(x => x.Family).HasMaxLength(64);
        }
    }
}