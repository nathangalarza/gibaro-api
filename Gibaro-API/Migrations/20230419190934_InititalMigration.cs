using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gibaro_API.Migrations
{
    public partial class InititalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFullyRegistered = table.Column<bool>(type: "bit", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDateLocked = table.Column<bool>(type: "bit", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: true),
                    LeavingAppReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsProffesional = table.Column<bool>(type: "bit", nullable: false),
                    Notification = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    notification_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", maxLength: 30, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUsedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDevices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "Bio", "BirthDateLocked", "Birthdate", "ConcurrencyStamp", "CountryCode", "CreatedAt", "CustomerId", "DeletedAt", "Email", "EmailConfirmed", "HeaderUrl", "IsFullyRegistered", "IsProffesional", "LeavingAppReason", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Notification", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0904be49-2b83-4767-b71b-d5d4dc8341d5"), 0, "https://images.pexels.com/photos/220453/pexels-photo-220453.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2393a202-7f6b-4852-af61-63fc8a5f7425", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "jorge@gmail.com", true, null, false, false, null, false, null, "Jorge", "JORGE@GMAIL.COM", "JORGE", false, "AQAAAAEAACcQAAAAENTenQb8BbnhrAJ0Pu5UcEoDhsQ+XdQdtjAQnWrZNYeqCL4N90ORO1LTA5oQ9Rd3/A==", "7878888888", false, null, null, "872be4ae-8530-40f2-9bfe-c409bd07896e", false, "jorge" },
                    { new Guid("0b726657-e034-467b-af14-09090b097af6"), 0, "https://cdn2.psychologytoday.com/assets/styles/manual_crop_1_91_1_1528x800/public/field_blog_entry_images/2018-09/shutterstock_648907024.jpg?itok=7lrLYx-B", null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "29f00cd2-e8eb-4c54-8458-55ca73424f7e", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "david@gmail.com", true, null, false, false, null, false, null, "David", "DAVID@GMAIL.COM", "DAVID", false, "AQAAAAEAACcQAAAAEIDA5eiTMWosnLgOv3iG54H7IPhVDjTYMkWsAmWeFt1vAnVn2fjRbSrwx1xXaf5K/Q==", "7873333333", false, null, null, "872be4ae-8530-40f2-9bfe-c409bd07896e", false, "david" },
                    { new Guid("3a0475b0-8474-4637-b1f0-6814329a37f9"), 0, "https://www.incimages.com/uploaded_files/image/1920x1080/getty_481292845_77896.jpg", null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e2a3ada8-5f97-419c-b90e-ae05acc29c97", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "nelson@gmail.com", true, null, false, false, null, false, null, "Nelson", "NELSON@GMAIL.COM", "NELSON", false, "AQAAAAEAACcQAAAAEAag7JjXUhYWH/OFXPi4v1XPdrX2cFHNcHCAEigYylpqUOEQHIcI57q+ilkvyU+L8w==", "7871111111", false, null, null, "872be4ae-8530-40f2-9bfe-c409bd07896e", false, "nelson" },
                    { new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c"), 0, "https://www.happierhuman.com/wp-content/uploads/2022/07/glass-half-full-type-persons-lessons-learned.jpg", null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4758d4bd-b083-465d-9ddd-13e61f05e4b8", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cus_NW5y3MHFNsTpXu", null, "nathan@gmail.com", true, null, false, false, null, false, null, "Nathan", "NATHAN@GMAIL.COM", "NATHAN", false, "AQAAAAEAACcQAAAAEIX5wEEGad1NTQttO2DEtwkYMxWYrtICa9oki94ZlRbCbwqdwvJfORwkfCQMVzZjxg==", "7877777777", false, null, null, "872be4ae-8530-40f2-9bfe-c409bd07896e", false, "nathan" },
                    { new Guid("6e2cc9fa-e5b7-4b19-99bb-591aa2a33c09"), 0, "https://cdn.hswstatic.com/gif/play/0b7f4e9b-f59c-4024-9f06-b3dc12850ab7-1920-1080.jpg", null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12693c27-b5fe-47d0-89ff-d08521edeb08", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "alexis@gmail.com", true, null, false, false, null, false, null, "Alexis", "ALEXIS@GMAIL.COM", "ALEXIS", false, "AQAAAAEAACcQAAAAEGUkrPsFaxUDFIU1Pw8p5rHY6f47nBIMIppjR4vCyHeQ/72m33kGBTSruy12sej5kg==", "7872222222", false, null, null, "872be4ae-8530-40f2-9bfe-c409bd07896e", false, "alexis" },
                    { new Guid("bd122f8d-004a-4391-bf01-c620a9bc8f70"), 0, "https://caricom.org/wp-content/uploads/Floyd-Morris-Remake-1024x879-1.jpg", null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "c605f80f-89d9-4470-bf61-6abc03f8c8d0", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "juan@gmail.com", true, null, false, false, null, false, null, "Juan", "JUAN@GMAIL.COM", "JUAN", false, "AQAAAAEAACcQAAAAEE7iIzDGRvgzmgg45O48+5Z98MKs3J6smxrvaoJak2XPC5ZJ0ObdRSEbJYsex0uHRg==", "7876666666", false, null, null, "872be4ae-8530-40f2-9bfe-c409bd07896e", false, "juan" },
                    { new Guid("ca6324b7-d5f7-4276-bb8c-40df9eee6898"), 0, "https://globalnews.ca/wp-content/uploads/2017/05/oldestmanthumb.jpg?quality=85&strip=all", null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "496f0a29-857b-4cb1-aadf-57be6add9796", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "pedro@gmail.com", true, null, false, false, null, false, null, "Pedro", "PEDRO@GMAIL.COM", "PEDRO", false, "AQAAAAEAACcQAAAAELW8+BSOWFr1xbTaIfZqTZz/3LtCq89nuatjaxKdUuzJgwUy+5bpBogK68yQ7z0kLQ==", "7875555555", false, null, null, "872be4ae-8530-40f2-9bfe-c409bd07896e", false, "pedro" }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "ActionUrl", "Message", "SenderId", "Timestamp", "notification_type", "UserId" },
                values: new object[,]
                {
                    { new Guid("033fa656-cef5-468f-a36b-2dca896eabf3"), null, "Likes your post", new Guid("0904be49-2b83-4767-b71b-d5d4dc8341d5"), new DateTime(2023, 2, 18, 10, 30, 0, 0, DateTimeKind.Unspecified), "Favorite", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") },
                    { new Guid("2ecf3634-262e-4a62-a309-50f1462dbe08"), null, "Likes your post", new Guid("0904be49-2b83-4767-b71b-d5d4dc8341d5"), new DateTime(2023, 2, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), "Favorite", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") },
                    { new Guid("52de628e-0562-43e3-a084-089766e5a357"), null, "Likes your post", new Guid("0904be49-2b83-4767-b71b-d5d4dc8341d5"), new DateTime(2023, 2, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), "Favorite", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") },
                    { new Guid("6e17b80a-b267-45d9-913c-1a8561382b22"), null, "Likes your post", new Guid("0904be49-2b83-4767-b71b-d5d4dc8341d5"), new DateTime(2023, 2, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), "Favorite", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") },
                    { new Guid("7456d18e-8ce5-4882-b0ff-3d928a52a3bf"), null, "Shared your post", new Guid("0904be49-2b83-4767-b71b-d5d4dc8341d5"), new DateTime(2023, 2, 20, 10, 30, 0, 0, DateTimeKind.Unspecified), "Shared", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") },
                    { new Guid("9e3e0349-f334-4260-bc8a-96a80ea58bb2"), null, "You have a new message.", new Guid("6e2cc9fa-e5b7-4b19-99bb-591aa2a33c09"), new DateTime(2023, 2, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), "Messages", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") },
                    { new Guid("a20b5caa-21ab-454a-9808-d418a8feda97"), null, "You were mentioned in a post.", new Guid("bd122f8d-004a-4391-bf01-c620a9bc8f70"), new DateTime(2023, 2, 18, 10, 30, 0, 0, DateTimeKind.Unspecified), "Mention", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") },
                    { new Guid("bb58bfa5-7119-4512-a33e-d8f7aa7fe2c4"), null, "You have a new friend request.", new Guid("0b726657-e034-467b-af14-09090b097af6"), new DateTime(2023, 2, 17, 10, 30, 0, 0, DateTimeKind.Unspecified), "FriendRequest", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") }
                });

            migrationBuilder.InsertData(
                table: "UserDevices",
                columns: new[] { "Id", "CreatedAt", "DeviceId", "LastUsedAt", "Model", "Platform", "RefreshToken", "Revoked", "UserAgent", "UserId" },
                values: new object[,]
                {
                    { new Guid("1d7d1bc4-4c7b-4f40-9ad1-1d16f7c6b162"), new DateTime(2023, 2, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), "SOMEID", new DateTime(2023, 2, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), "SOMEMODEL", "IOS", "RefreshToken1", true, "UserAgent1", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") },
                    { new Guid("2a3e3f6d-9fdd-4b07-9cb9-6a0a1d7c9363"), new DateTime(2023, 2, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), "SOMEID2", new DateTime(2023, 2, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), "SOMEMODEL", "Android", "RefreshToken2", true, "UserAgent2", new Guid("52d93506-877e-4ae7-8969-1c227aa4f96c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_UserId",
                table: "UserDevices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserDevices");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
