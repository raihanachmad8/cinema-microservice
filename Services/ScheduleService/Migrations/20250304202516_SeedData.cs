using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScheduleService.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "schedules",
                columns: new[] { "Id", "created_at", "deleted_at", "MovieId", "ShowTime", "StudioId", "TicketPrice", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 3, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), 35000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 3, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222"), 30000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 3, 3, 13, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222"), 40000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 3, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), 35000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 3, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333"), 35000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "schedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "schedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "schedules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "schedules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "schedules",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
