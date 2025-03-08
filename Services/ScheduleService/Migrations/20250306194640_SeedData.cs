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
                columns: new[] { "Id", "created_at", "deleted_at", "EndDatetime", "MovieId", "StartDatetime", "StudioId", "TicketPrice", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 3, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 3, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 35000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 3, 3, 13, 20, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 3, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), 2, 30000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 3, 3, 15, 30, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 3, 3, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, 40000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 3, 3, 14, 10, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 3, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 35000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 3, 3, 15, 50, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2025, 3, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), 3, 35000m, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
