using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Address", "created_at", "deleted_at", "Email", "Name", "Password", "PhoneNumber", "Role", "updated_at" },
                values: new object[,]
                {
                    { 1, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin1@cinema.com", "Admin 1", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin2@cinema.com", "Admin 2", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user1@cinema.com", "User 1", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user2@cinema.com", "User 2", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user3@cinema.com", "User 3", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user4@cinema.com", "User 4", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user5@cinema.com", "User 5", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user6@cinema.com", "User 6", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user7@cinema.com", "User 7", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Jl. Kebon Jeruk 11, Jakarta Selatan", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user8@cinema.com", "User 8", "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", "08123456789", 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
