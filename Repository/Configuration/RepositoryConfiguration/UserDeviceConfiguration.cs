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
    public class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice>
    {
        public void Configure(EntityTypeBuilder<UserDevice> builder)
        {
            builder.ToTable("UserDevices");

            builder.HasKey(ud => ud.Id);

            builder
                .HasOne(a => a.User)
                .WithMany(x=>x.Devices)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(ud => ud.DeviceId)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(ud => ud.RefreshToken)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(ud => ud.Platform)
             .HasConversion<string>();

            builder.Property(ud => ud.CreatedAt)
                .IsRequired();

            builder.Property(ud => ud.LastUsedAt)
                .IsRequired();

            SeedUserDevices(builder);

     
        }

        private void SeedUserDevices(EntityTypeBuilder<UserDevice> builder)
        {
            builder.HasData(
                 new UserDevice
                 {
                     Id = Guid.Parse("1d7d1bc4-4c7b-4f40-9ad1-1d16f7c6b162"),
                     UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                     DeviceId = "SOMEID",
                     RefreshToken = "RefreshToken1",
                     UserAgent = "UserAgent1",
                     CreatedAt = new DateTime(2023, 2, 22, 10, 30, 0),
                     Platform = Platform.IOS,
                     LastUsedAt = new DateTime(2023, 2, 22, 10, 30, 0),
                     Model = "SOMEMODEL",
                     Revoked = true,
                 },
                 new UserDevice
                 {
                     Id = Guid.Parse("2a3e3f6d-9fdd-4b07-9cb9-6a0a1d7c9363"),
                     UserId = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                     DeviceId = "SOMEID2",
                     RefreshToken = "RefreshToken2",
                     UserAgent = "UserAgent2",
                     Platform = Platform.Android,
                     CreatedAt = new DateTime(2023, 2, 22, 10, 30, 0),
                     LastUsedAt = new DateTime(2023, 2, 22, 10, 30, 0),
                     Model = "SOMEMODEL",
                     Revoked = true
                 }
             );
        }
    }
}
