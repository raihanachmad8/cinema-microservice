using CinemaApp.Domain.Enums;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdentityService.Infrastructure.Persistence;

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
        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
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
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
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
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
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
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
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
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
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
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
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
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Name = "User 5",
                Email = "user5@cinema.com",
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