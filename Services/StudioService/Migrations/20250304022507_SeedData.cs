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
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Dolby Surround, Wide Screen", 100, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 1", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "IMAX, 3D", 150, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 2", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Luxury Recliners, Dolby Atmos", 200, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 3", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "VIP Seating, 4D Experience", 250, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 4", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Kids Area, Wide Screen", 300, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Studio 5", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "studios",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));
        }
    }
}
