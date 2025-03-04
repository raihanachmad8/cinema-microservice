﻿// <auto-generated />
using System;
using IdentityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IdentityService.Migrations
{
    [DbContext(typeof(IdentityDBContext))]
    partial class IdentityDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IdentityService.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin1@cinema.com",
                            Name = "Admin 1",
                            Password = "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw",
                            PhoneNumber = "08123456789",
                            Role = 2,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin2@cinema.com",
                            Name = "Admin 2",
                            Password = "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw",
                            PhoneNumber = "08123456789",
                            Role = 2,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("33333333-3333-3333-3333-333333333333"),
                            Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user1@cinema.com",
                            Name = "User 1",
                            Password = "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw",
                            PhoneNumber = "08123456789",
                            Role = 1,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("44444444-4444-4444-4444-444444444444"),
                            Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user2@cinema.com",
                            Name = "User 2",
                            Password = "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw",
                            PhoneNumber = "08123456789",
                            Role = 1,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("55555555-5555-5555-5555-555555555555"),
                            Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user3@cinema.com",
                            Name = "User 3",
                            Password = "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw",
                            PhoneNumber = "08123456789",
                            Role = 1,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("66666666-6666-6666-6666-666666666666"),
                            Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user4@cinema.com",
                            Name = "User 4",
                            Password = "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw",
                            PhoneNumber = "08123456789",
                            Role = 1,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("77777777-7777-7777-7777-777777777777"),
                            Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                            CreatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user5@cinema.com",
                            Name = "User 5",
                            Password = "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw",
                            PhoneNumber = "08123456789",
                            Role = 1,
                            UpdatedAt = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
