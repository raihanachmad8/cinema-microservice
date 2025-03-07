using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketService.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "seats",
                columns: new[] { "Id", "created_at", "deleted_at", "IsAvailable", "OccupiedAt", "ReservedAt", "SeatNumber", "StudioId", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "A1", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "A2", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "A3", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "A4", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "A5", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "B1", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "B2", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "B3", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "B4", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, "B5", 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "tickets",
                columns: new[] { "Id", "created_at", "deleted_at", "ReservedAt", "ScheduleId", "SeatId", "Status", "updated_at", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 0, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 2, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 0, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 3, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 0, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "seats",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
