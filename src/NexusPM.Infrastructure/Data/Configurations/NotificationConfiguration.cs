// <copyright file="NotificationConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the entity type for <see cref="Notification"/>.
/// </summary>
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="Notification"/> entity.
    /// </summary>
    /// <param name="builder">The builder to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(x => x.Type)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.PayloadJson)
            .HasMaxLength(8000)
            .IsRequired();

        builder.HasOne(x => x.Recipient)
            .WithMany()
            .HasForeignKey(x => x.RecipientUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
