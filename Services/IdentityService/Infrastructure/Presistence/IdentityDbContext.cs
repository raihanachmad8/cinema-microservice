using CinemaApp.Domain.Enums;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdentityService.Infrastructure.Persistence
{
    public class IdentityDBContext : DbContext
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Indeks untuk entitas User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Global Query Filter untuk Soft Delete
            modelBuilder.Entity<User>().HasQueryFilter(u => u.DeletedAt == null);
            base.OnModelCreating(modelBuilder);

            // Seeder
            SeedData(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.State = EntityState.Modified; // Soft delete
                }
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users with integer Ids
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, // Update to int values
                    Name = "Admin 1",
                    Email = "admin1@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.Admin,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                },
                new User
                {
                    Id = 2, // Update to int values
                    Name = "Admin 2",
                    Email = "admin2@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.Admin,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                },
                new User
                {
                    Id = 3, // Update to int values
                    Name = "User 1",
                    Email = "user1@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.User,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                },
                new User
                {
                    Id = 4, // Update to int values
                    Name = "User 2",
                    Email = "user2@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.User,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                },
                new User
                {
                    Id = 5, // Update to int values
                    Name = "User 3",
                    Email = "user3@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.User,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                },
                new User
                {
                    Id = 6,
                    Name = "User 4",
                    Email = "user4@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.User,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                },
                new User
                {
                    Id = 7,
                    Name = "User 5",
                    Email = "user5@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.User,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                },
                new User
                {
                    Id = 8,
                    Name = "User 6",
                    Email = "user6@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.User,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                },
                new User
                {
                    Id = 9,
                    Name = "User 7",
                    Email = "user7@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.User,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                },
                new User
                {
                    Id = 10,
                    Name = "User 8",
                    Email = "user8@cinema.com",
                    Password =
                        "$argon2id$v=19$m=65536,t=3,p=1$xBq496riUmc7v0hRVp4s/A$NIwkenkHFcqEJ+21ejwCercJSYl9T08DXuHq3wrOUAw", // Password: Password@123
                    PhoneNumber = "08123456789",
                    Address = "Jl. Kebon Jeruk 11, Jakarta Selatan",
                    Role = Role.User,
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03")
                }
            );
        }
    }
}