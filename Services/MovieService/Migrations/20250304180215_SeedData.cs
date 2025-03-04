using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieService.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "movies",
                columns: new[] { "Id", "created_at", "deleted_at", "Description", "DurationInMinutes", "Genre", "Title", "updated_at" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "A former banker turned criminal tries to survive his life in a New England town.", 120, 2, "The Shawshank Redemption", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", 140, 1, "The Godfather", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "When Batman, Gordon and Harvey Dent launch an assault on the mob, they let the clown out of the box, the Joker, bent on turning Gotham on itself and bringing any heroes down to his level.", 150, 1, "The Dark Knight", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "The lives of two mob hit men, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", 130, 2, "Pulp Fiction", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", 170, 1, "The Lord of the Rings: The Return of the King", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "A thief who steals corporate secrets through use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.", 140, 1, "Inception", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "When a beautiful stranger leads computer hacker Neo to the underworld, he discovers the shocking truth--the life he knows is the reality he's been trapped in.", 130, 1, "The Matrix", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "A young boy learns that he is the man who killed his parents and he vows to find out who did this to him, before becoming the man he never wanted to be.", 150, 1, "The Dark Knight Rises", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "While Frodo and Sam edge closer to Mordor with the help of the shifty Gollum, the divided fellowship makes a stand against Sauron's new ally, Saruman, and his hordes of Isengard.", 170, 1, "The Lord of the Rings: The Two Towers", new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "movies",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));
        }
    }
}
