﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScheduleService.Infrastructure.Persistence;

#nullable disable

namespace ScheduleService.Migrations
{
    [DbContext(typeof(ScheduleDbContext))]
    [Migration("20250306194640_SeedData")]
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

            modelBuilder.Entity("ScheduleService.Domain.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_at");

                    b.Property<DateTime>("EndDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudioId")
                        .HasColumnType("int");

                    b.Property<decimal>("TicketPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("schedules");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDatetime = new DateTime(2025, 3, 3, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 1,
                            StartDatetime = new DateTime(2025, 3, 3, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            StudioId = 1,
                            TicketPrice = 35000m,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDatetime = new DateTime(2025, 3, 3, 13, 20, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 2,
                            StartDatetime = new DateTime(2025, 3, 3, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            StudioId = 2,
                            TicketPrice = 30000m,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDatetime = new DateTime(2025, 3, 3, 15, 30, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 3,
                            StartDatetime = new DateTime(2025, 3, 3, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            StudioId = 2,
                            TicketPrice = 40000m,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDatetime = new DateTime(2025, 3, 3, 14, 10, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 4,
                            StartDatetime = new DateTime(2025, 3, 3, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            StudioId = 1,
                            TicketPrice = 35000m,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDatetime = new DateTime(2025, 3, 3, 15, 50, 0, 0, DateTimeKind.Unspecified),
                            MovieId = 6,
                            StartDatetime = new DateTime(2025, 3, 3, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            StudioId = 3,
                            TicketPrice = 35000m,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
