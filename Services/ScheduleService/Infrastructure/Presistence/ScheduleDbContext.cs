using Microsoft.EntityFrameworkCore;
using ScheduleService.Domain.Entities;

namespace ScheduleService.Infrastructure.Persistence;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options)
    {
    }

    public DbSet<Schedule> Schedules { get; set; } // Change from Schedules to Schedules

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Schedule>()
            .Property(s => s.TicketPrice)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Schedule>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd(); 
        // Global Query Filter for Soft Delete
        
        modelBuilder.Entity<Schedule>().HasQueryFilter(m => m.DeletedAt == null);

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
        // Seed Schedules
        modelBuilder.Entity<Schedule>().HasData(
            new Schedule
            {
                Id = 1,
                StudioId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                MovieId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ShowTime = DateTime.Parse("2025-03-03 10:00:00"),
                TicketPrice = 35000,
                CreatedAt = DateTime.Parse("2025-03-03"),
                UpdatedAt = DateTime.Parse("2025-03-03"),
            },
            new Schedule
            {
                Id = 2,
                StudioId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                MovieId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                ShowTime = DateTime.Parse("2025-03-03 11:00:00"),
                TicketPrice = 30000,
                CreatedAt = DateTime.Parse("2025-03-03"),
                UpdatedAt = DateTime.Parse("2025-03-03"),
            },
            new Schedule
            {
                Id = 3,
                StudioId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                MovieId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                ShowTime = DateTime.Parse("2025-03-03 13:00:00"),
                TicketPrice = 40000,
                CreatedAt = DateTime.Parse("2025-03-03"),
                UpdatedAt = DateTime.Parse("2025-03-03"),
            },
            new Schedule
            {
                Id = 4,
                StudioId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                MovieId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                ShowTime = DateTime.Parse("2025-03-03 12:00:00"),
                TicketPrice = 35000,
                CreatedAt = DateTime.Parse("2025-03-03"),
                UpdatedAt = DateTime.Parse("2025-03-03"),
            },
            new Schedule()
            {
                Id = 5,
                StudioId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                MovieId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                ShowTime = DateTime.Parse("2025-03-03 14:00:00"),
                TicketPrice = 35000,
                CreatedAt = DateTime.Parse("2025-03-03"),
                UpdatedAt = DateTime.Parse("2025-03-03"),
            }
        );
    }
}