﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieService.Infrastructure.Persistence;

#nullable disable

namespace MovieService.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20250304105042_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieService.Domain.Entities.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("movies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A former banker turned criminal tries to survive his life in a New England town.",
                            DurationInMinutes = 120,
                            Genre = 2,
                            Title = "The Shawshank Redemption",
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                            DurationInMinutes = 140,
                            Genre = 1,
                            Title = "The Godfather",
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("33333333-3333-3333-3333-333333333333"),
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "When Batman, Gordon and Harvey Dent launch an assault on the mob, they let the clown out of the box, the Joker, bent on turning Gotham on itself and bringing any heroes down to his level.",
                            DurationInMinutes = 150,
                            Genre = 1,
                            Title = "The Dark Knight",
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("44444444-4444-4444-4444-444444444444"),
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "The lives of two mob hit men, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                            DurationInMinutes = 130,
                            Genre = 2,
                            Title = "Pulp Fiction",
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("55555555-5555-5555-5555-555555555555"),
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                            DurationInMinutes = 170,
                            Genre = 1,
                            Title = "The Lord of the Rings: The Return of the King",
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("66666666-6666-6666-6666-666666666666"),
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A thief who steals corporate secrets through use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                            DurationInMinutes = 140,
                            Genre = 1,
                            Title = "Inception",
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("77777777-7777-7777-7777-777777777777"),
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "When a beautiful stranger leads computer hacker Neo to the underworld, he discovers the shocking truth--the life he knows is the reality he's been trapped in.",
                            DurationInMinutes = 130,
                            Genre = 1,
                            Title = "The Matrix",
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("88888888-8888-8888-8888-888888888888"),
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A young boy learns that he is the man who killed his parents and he vows to find out who did this to him, before becoming the man he never wanted to be.",
                            DurationInMinutes = 150,
                            Genre = 1,
                            Title = "The Dark Knight Rises",
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("99999999-9999-9999-9999-999999999999"),
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "While Frodo and Sam edge closer to Mordor with the help of the shifty Gollum, the divided fellowship makes a stand against Sauron's new ally, Saruman, and his hordes of Isengard.",
                            DurationInMinutes = 170,
                            Genre = 1,
                            Title = "The Lord of the Rings: The Two Towers",
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
