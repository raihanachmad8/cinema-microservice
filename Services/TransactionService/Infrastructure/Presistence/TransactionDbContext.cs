using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.Entities;
using TransactionService.Domain.Enums;

namespace TransactionService.Infrastructure.Persistence
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>().HasQueryFilter(t => t.DeletedAt == null);

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
            // Sample tramsaction
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    TicketId = 1,
                    UserId = 4,
                    PaymentStatus = PaymentStatus.Pending,
                    PaymentMethod = PaymentMethod.CreditCard,
                    TotalAmount = 35000,
                    CreatedAt = DateTime.Parse("2025-03-06"),
                    UpdatedAt = DateTime.Parse("2025-03-06")
                },
                new Transaction
                {
                    Id = 2,
                    TicketId = 2,
                    UserId = 5,
                    PaymentMethod = PaymentMethod.EWallet,
                    PaymentStatus = PaymentStatus.Pending,
                    TotalAmount = 35000,
                    CreatedAt = DateTime.Parse("2025-03-06"),
                    UpdatedAt = DateTime.Parse("2025-03-06")
                },
                new Transaction
                {
                    Id = 3,
                    TicketId = 2,
                    UserId = 3,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    PaymentStatus = PaymentStatus.Pending,
                    TotalAmount = 35000,
                    CreatedAt = DateTime.Parse("2025-03-06"),
                    UpdatedAt = DateTime.Parse("2025-03-06")
                }
            );
        }
    }
}