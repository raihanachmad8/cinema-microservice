using Microsoft.EntityFrameworkCore;
using TicketService.Domain.Entities;
using TicketService.Domain.Enums;

namespace TicketService.Infrastructure.Persistence
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global Query Filter for Soft Delete
            modelBuilder.Entity<Ticket>().HasQueryFilter(t => t.DeletedAt == null);
            modelBuilder.Entity<Seat>().HasQueryFilter(s => s.DeletedAt == null);

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
            // Sample seats for a studio
            modelBuilder.Entity<Seat>().HasData(
                new Seat { Id = 1, StudioId = 1, SeatNumber = "A1", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") },
                new Seat { Id = 2, StudioId = 1, SeatNumber = "A2", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") },
                new Seat { Id = 3, StudioId = 1, SeatNumber = "A3", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") },
                new Seat { Id = 4, StudioId = 1, SeatNumber = "A4", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") },
                new Seat { Id = 5, StudioId = 1, SeatNumber = "A5", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") },
                new Seat { Id = 6, StudioId = 1, SeatNumber = "B1", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") },
                new Seat { Id = 7, StudioId = 1, SeatNumber = "B2", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") },
                new Seat { Id = 8, StudioId = 1, SeatNumber = "B3", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") },
                new Seat { Id = 9, StudioId = 1, SeatNumber = "B4", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") },
                new Seat { Id = 10, StudioId = 1, SeatNumber = "B5", IsAvailable = true, CreatedAt = DateTime.Parse("2025-03-06"), UpdatedAt = DateTime.Parse("2025-03-06") }
            );

            // Sample tickets for reserved seats
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    ScheduleId = 1,
                    UserId = 4,
                    SeatId = 1,
                    Status = TicketStatus.Reserved,
                    ReservedAt = DateTime.Parse("2025-03-06"),
                    CreatedAt = DateTime.Parse("2025-03-06"),
                    UpdatedAt = DateTime.Parse("2025-03-06")
                },
                new Ticket
                {
                    Id = 2,
                    ScheduleId = 1,
                    UserId = 5,
                    SeatId = 2,
                    Status = TicketStatus.Reserved,
                    ReservedAt = DateTime.Parse("2025-03-06"),
                    CreatedAt = DateTime.Parse("2025-03-06"),
                    UpdatedAt = DateTime.Parse("2025-03-06")
                },
                new Ticket
                {
                    Id = 3,
                    ScheduleId = 1,
                    UserId = 3,
                    SeatId = 3,
                    Status = TicketStatus.Reserved,
                    ReservedAt = DateTime.Parse("2025-03-06"),
                    CreatedAt = DateTime.Parse("2025-03-06"),
                    UpdatedAt = DateTime.Parse("2025-03-06")
                }
            );
        }
    }
}
