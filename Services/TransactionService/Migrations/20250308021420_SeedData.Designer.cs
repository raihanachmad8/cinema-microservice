﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransactionService.Infrastructure.Persistence;

#nullable disable

namespace TransactionService.Migrations
{
    [DbContext(typeof(TransactionDbContext))]
    [Migration("20250308021420_SeedData")]
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

            modelBuilder.Entity("TransactionService.Domain.Entities.Transaction", b =>
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

                    b.Property<int>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("PaymentStatus")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("transactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentMethod = 1,
                            PaymentStatus = 1,
                            TicketId = 1,
                            TotalAmount = 35000m,
                            TransactionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 4
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentMethod = 2,
                            PaymentStatus = 1,
                            TicketId = 2,
                            TotalAmount = 35000m,
                            TransactionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 5
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentMethod = 3,
                            PaymentStatus = 1,
                            TicketId = 2,
                            TotalAmount = 35000m,
                            TransactionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedAt = new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 3
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
