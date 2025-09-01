// <copyright file="UserSessionConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructurbuilder.Identity.Configurations
{
    /// <inheritdoc/>
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("UserSessions");
            builder.HasIndex(x => new { x.UserId, x.SessionId }).IsUnique();
            builder.Property(x => x.SessionId).HasMaxLength(64);
        }
    }
}