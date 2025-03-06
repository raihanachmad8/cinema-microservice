using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudioService.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "studios",
                columns: new[] { "Id", "AdditionalFacilities", "Capacity", "created_at", "deleted_at", "Name", "updated_at" },
                values: new object[,]
                {
                    { 1, "Dolby Surround, Wide Screen", 50, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 1", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "IMAX, 3D", 80, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 2", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Luxury Recliners, Dolby Atmos", 50, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 3", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "VIP Seating, 4D Experience", 40, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 4", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Kids Area, Wide Screen", 80, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 5", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Wide Screen, Luxury Recliners", 60, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 6", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
