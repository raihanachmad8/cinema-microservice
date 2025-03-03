using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Address", "created_at", "deleted_at", "Email", "Name", "Password", "PhoneNumber", "Role", "updated_at" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin1@cinema.com", "Admin 1", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin2@cinema.com", "Admin 2", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user1@cinema.com", "User 1", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user2@cinema.com", "User 2", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user3@cinema.com", "User 3", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user4@cinema.com", "User 4", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user5@cinema.com", "User 5", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));
        }
    }
}
