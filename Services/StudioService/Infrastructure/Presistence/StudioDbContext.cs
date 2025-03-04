using Microsoft.EntityFrameworkCore;
using StudioService.Domain.Entities;

namespace StudioService.Infrastructure.Persistence
{
    public class StudioDbContext : DbContext
    {
        public StudioDbContext(DbContextOptions<StudioDbContext> options) : base(options)
        {
        }

        public DbSet<Studio> Studios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Indeks untuk entitas Studio
            modelBuilder.Entity<Studio>()
                .HasIndex(s => s.Name)
                .IsUnique();


            // Global Query Filter untuk Soft Delete
            modelBuilder.Entity<Studio>().HasQueryFilter(s => s.DeletedAt == null);

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
            {
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
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // // Seed Studio
            modelBuilder.Entity<Studio>().HasData(
                new Studio
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Studio 1",
                    Capacity = 100,
                    AdditionalFacilities = "Dolby Surround, Wide Screen",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Studio
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Studio 2",
                    Capacity = 150,
                    AdditionalFacilities = "IMAX, 3D",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Studio
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Studio 3",
                    Capacity = 200,
                    AdditionalFacilities = "Luxury Recliners, Dolby Atmos",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Studio
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "Studio 4",
                    Capacity = 250,
                    AdditionalFacilities = "VIP Seating, 4D Experience",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Studio
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Name = "Studio 5",
                    Capacity = 300,
                    AdditionalFacilities = "Kids Area, Wide Screen",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                }
            );
        }
    }
}