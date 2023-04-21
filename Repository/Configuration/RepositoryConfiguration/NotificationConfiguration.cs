using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration.RepositoryConfiguration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            // Primary key
            builder.HasKey(e => e.Id);

            // Relationships
            builder.HasOne(e => e.User)
                .WithMany(e => e.Notifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Sender)
                .WithMany()
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            // Columns

            builder.Property(e => e.Type)
                .HasColumnName("notification_type")
                .HasConversion<string>()
                .IsRequired();


            builder.Property(e => e.Timestamp)
                .IsRequired()
                .HasMaxLength(30);



            builder.Property(e => e.IsRead)
                .IsRequired()
                .HasDefaultValue(false);

            SeedNotifications(builder);
        }


        private void SeedNotifications(EntityTypeBuilder<Notification> builder)
        {
            builder.HasData(new List<Notification>
            {
                   new Notification
                {
                    Id = Guid.Parse("9e3e0349-f334-4260-bc8a-96a80ea58bb2"),
                    UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                    Type = Notification.NotificationType.Messages,
                    Message = "You have a new message.",
                    SenderId = Guid.Parse("6e2cc9fa-e5b7-4b19-99bb-591aa2a33c09"),
                    Timestamp = new DateTime(2023, 2, 15, 10, 30, 0)
                 },
                new Notification
                {
                    Id = Guid.Parse("bb58bfa5-7119-4512-a33e-d8f7aa7fe2c4"),
                    UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                    Type = Notification.NotificationType.FriendRequest,
                    Message = "You have a new friend request.",
                    SenderId = Guid.Parse("0b726657-e034-467b-af14-09090b097af6"),
                    Timestamp = new DateTime(2023, 2, 17, 10, 30, 0)
                },
                new Notification
                {
                    Id = Guid.Parse("a20b5caa-21ab-454a-9808-d418a8feda97"),
                    UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                    Type = Notification.NotificationType.Mention,
                    Message = "You were mentioned in a post.",
                    SenderId = Guid.Parse("bd122f8d-004a-4391-bf01-c620a9bc8f70"),
                    Timestamp = new DateTime(2023, 2, 18, 10, 30, 0)
                },
                  new Notification
                {
                    Id = Guid.Parse("033fa656-cef5-468f-a36b-2dca896eabf3"),
                    UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                    Type = Notification.NotificationType.Favorite,
                    Message = "Likes your post",
                    SenderId = Guid.Parse("0904be49-2b83-4767-b71b-d5d4dc8341d5"),
                    Timestamp = new DateTime(2023, 2, 18, 10, 30, 0)
                },
                     new Notification
                {
                    Id = Guid.Parse("7456d18e-8ce5-4882-b0ff-3d928a52a3bf"),
                    UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                    Type = Notification.NotificationType.Shared,
                    Message = "Shared your post",
                    SenderId = Guid.Parse("0904be49-2b83-4767-b71b-d5d4dc8341d5"),
                    Timestamp = new DateTime(2023, 2, 20, 10, 30, 0)
                },
                        new Notification
                {
                    Id = Guid.Parse("2ecf3634-262e-4a62-a309-50f1462dbe08"),
                    UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                    Type = Notification.NotificationType.Favorite,
                    Message = "Likes your post",
                    SenderId = Guid.Parse("0904be49-2b83-4767-b71b-d5d4dc8341d5"),
                    Timestamp = new DateTime(2023, 2, 22, 10, 30, 0)
                },
                           new Notification
                {
                    Id = Guid.Parse("6e17b80a-b267-45d9-913c-1a8561382b22"),
                    UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                    Type = Notification.NotificationType.Favorite,
                    Message = "Likes your post",
                    SenderId = Guid.Parse("0904be49-2b83-4767-b71b-d5d4dc8341d5"),
                    Timestamp = new DateTime(2023, 2, 22, 10, 30, 0)
                },
                              new Notification
                {
                    Id = Guid.Parse("52de628e-0562-43e3-a084-089766e5a357"),
                    UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                    Type = Notification.NotificationType.Favorite,
                    Message = "Likes your post",
                    SenderId = Guid.Parse("0904be49-2b83-4767-b71b-d5d4dc8341d5"),
                    Timestamp = new DateTime(2023, 2, 22, 10, 30, 0)
                },
            });
        }

    }
}

