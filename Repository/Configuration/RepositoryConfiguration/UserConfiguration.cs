using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration.RepositoryConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder
                .Property(a => a.Name)
                .HasMaxLength(30);

            builder
                .Property(a => a.UserName)
                .HasMaxLength(30);

            builder.HasMany(a => a.Notifications);

            builder
                .Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(320);

            builder
                .Property(a => a.PasswordHash)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder
                .Property(a => a.Birthdate)
                .IsRequired();

            builder
                .Property(a => a.CountryCode)
                .HasMaxLength(2);

            builder
                .Property(a => a.Bio)
                .HasMaxLength(140);

            SeedUsers(builder);
        }

        private void SeedUsers(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = Guid.Parse("3a0475b0-8474-4637-b1f0-6814329a37f9"),
                    UserName = "nelson",
                    NormalizedUserName = "NELSON",
                    AvatarUrl = "https://www.incimages.com/uploaded_files/image/1920x1080/getty_481292845_77896.jpg",
                    Name = "Nelson",
                    Email = "nelson@gmail.com",
                    NormalizedEmail = "NELSON@GMAIL.COM",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    PhoneNumber = "7871111111",
                    PasswordHash = "AQAAAAEAACcQAAAAEAag7JjXUhYWH/OFXPi4v1XPdrX2cFHNcHCAEigYylpqUOEQHIcI57q+ilkvyU+L8w==",
                    ConcurrencyStamp = "e2a3ada8-5f97-419c-b90e-ae05acc29c97",
                    SecurityStamp = "872be4ae-8530-40f2-9bfe-c409bd07896e"

                },
                new User()
                {
                    Id = Guid.Parse("6e2cc9fa-e5b7-4b19-99bb-591aa2a33c09"),
                    UserName = "alexis",
                    NormalizedUserName = "ALEXIS",
                    AvatarUrl = "https://cdn.hswstatic.com/gif/play/0b7f4e9b-f59c-4024-9f06-b3dc12850ab7-1920-1080.jpg",
                    Email = "alexis@gmail.com",
                    NormalizedEmail = "ALEXIS@GMAIL.COM",
                    Name = "Alexis",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    PhoneNumber = "7872222222",
                    PasswordHash = "AQAAAAEAACcQAAAAEGUkrPsFaxUDFIU1Pw8p5rHY6f47nBIMIppjR4vCyHeQ/72m33kGBTSruy12sej5kg==",
                    ConcurrencyStamp = "12693c27-b5fe-47d0-89ff-d08521edeb08",
                    SecurityStamp = "872be4ae-8530-40f2-9bfe-c409bd07896e",

                },
                new User()
                {
                    Id = Guid.Parse("0b726657-e034-467b-af14-09090b097af6"),
                    UserName = "david",
                    NormalizedUserName = "DAVID",
                    AvatarUrl = "https://cdn2.psychologytoday.com/assets/styles/manual_crop_1_91_1_1528x800/public/field_blog_entry_images/2018-09/shutterstock_648907024.jpg?itok=7lrLYx-B",
                    Name = "David",
                    Email = "david@gmail.com",
                    NormalizedEmail = "DAVID@GMAIL.COM",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    PhoneNumber = "7873333333",
                    PasswordHash = "AQAAAAEAACcQAAAAEIDA5eiTMWosnLgOv3iG54H7IPhVDjTYMkWsAmWeFt1vAnVn2fjRbSrwx1xXaf5K/Q==",
                    ConcurrencyStamp = "29f00cd2-e8eb-4c54-8458-55ca73424f7e",
                    SecurityStamp = "872be4ae-8530-40f2-9bfe-c409bd07896e"

                },
                new User()
                {
                    Id = Guid.Parse("ca6324b7-d5f7-4276-bb8c-40df9eee6898"),
                    UserName = "pedro",
                    NormalizedUserName = "PEDRO",
                    AvatarUrl = "https://globalnews.ca/wp-content/uploads/2017/05/oldestmanthumb.jpg?quality=85&strip=all",
                    Name = "Pedro",
                    Email = "pedro@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "PEDRO@GMAIL.COM",
                    LockoutEnabled = false,
                    PhoneNumber = "7875555555",
                    PasswordHash = "AQAAAAEAACcQAAAAELW8+BSOWFr1xbTaIfZqTZz/3LtCq89nuatjaxKdUuzJgwUy+5bpBogK68yQ7z0kLQ==",
                    ConcurrencyStamp = "496f0a29-857b-4cb1-aadf-57be6add9796",
                    SecurityStamp = "872be4ae-8530-40f2-9bfe-c409bd07896e"

                },
                new User()
                {
                    Id = Guid.Parse("bd122f8d-004a-4391-bf01-c620a9bc8f70"),
                    UserName = "juan",
                    NormalizedUserName = "JUAN",
                    AvatarUrl = "https://caricom.org/wp-content/uploads/Floyd-Morris-Remake-1024x879-1.jpg",
                    Name = "Juan",
                    Email = "juan@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "JUAN@GMAIL.COM",
                    LockoutEnabled = false,
                    PhoneNumber = "7876666666",
                    PasswordHash = "AQAAAAEAACcQAAAAEE7iIzDGRvgzmgg45O48+5Z98MKs3J6smxrvaoJak2XPC5ZJ0ObdRSEbJYsex0uHRg==",
                    ConcurrencyStamp = "c605f80f-89d9-4470-bf61-6abc03f8c8d0",
                    SecurityStamp = "872be4ae-8530-40f2-9bfe-c409bd07896e"
                },
                new User()
                {
                    Id = Guid.Parse("52d93506-877e-4ae7-8969-1c227aa4f96c"),
                    UserName = "nathan",
                    NormalizedUserName = "NATHAN",
                    CustomerId = "cus_NW5y3MHFNsTpXu",
                    EmailConfirmed = true,
                    AvatarUrl = "https://www.happierhuman.com/wp-content/uploads/2022/07/glass-half-full-type-persons-lessons-learned.jpg",
                    Name = "Nathan",
                    Email = "nathan@gmail.com",
                    NormalizedEmail = "NATHAN@GMAIL.COM",
                    LockoutEnabled = false,
                    PhoneNumber = "7877777777",
                    PasswordHash = "AQAAAAEAACcQAAAAEIX5wEEGad1NTQttO2DEtwkYMxWYrtICa9oki94ZlRbCbwqdwvJfORwkfCQMVzZjxg==",
                    ConcurrencyStamp = "4758d4bd-b083-465d-9ddd-13e61f05e4b8",
                    SecurityStamp = "872be4ae-8530-40f2-9bfe-c409bd07896e"
                },
                new User()
                {
                    Id = Guid.Parse("0904be49-2b83-4767-b71b-d5d4dc8341d5"),
                    UserName = "jorge",
                    NormalizedUserName = "JORGE",
                    AvatarUrl = "https://images.pexels.com/photos/220453/pexels-photo-220453.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
                    Name = "Jorge",
                    Email = "jorge@gmail.com",
                    NormalizedEmail = "JORGE@GMAIL.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "7878888888",
                    PasswordHash = "AQAAAAEAACcQAAAAENTenQb8BbnhrAJ0Pu5UcEoDhsQ+XdQdtjAQnWrZNYeqCL4N90ORO1LTA5oQ9Rd3/A==",
                    ConcurrencyStamp = "2393a202-7f6b-4852-af61-63fc8a5f7425",
                    SecurityStamp = "872be4ae-8530-40f2-9bfe-c409bd07896e"

                }
            );
        }
    }
}
