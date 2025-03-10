﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketService.Infrastructure.Persistence;

#nullable disable

namespace TicketService.Migrations
{
    [DbContext(typeof(TicketDbContext))]
    [Migration("20250307001149_SeedData")]
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

            modelBuilder.Entity("TicketService.Domain.Entities.Seat", b =>
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

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("OccupiedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReservedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("StudioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("seats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "A1",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "A2",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "A3",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "A4",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "A5",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "B1",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "B2",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "B3",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "B4",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 10,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAvailable = true,
                            SeatNumber = "B5",
                            StudioId = 1,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("TicketService.Domain.Entities.Ticket", b =>
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

                    b.Property<DateTime?>("ReservedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SeatId");

                    b.ToTable("tickets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReservedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ScheduleId = 1,
                            SeatId = 1,
                            Status = 0,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 4
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReservedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ScheduleId = 1,
                            SeatId = 2,
                            Status = 0,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 5
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReservedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ScheduleId = 1,
                            SeatId = 3,
                            Status = 0,
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 3
                        });
                });

            modelBuilder.Entity("TicketService.Domain.Entities.Ticket", b =>
                {
                    b.HasOne("TicketService.Domain.Entities.Seat", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seat");
                });
#pragma warning restore 612, 618
        }
    }
}
