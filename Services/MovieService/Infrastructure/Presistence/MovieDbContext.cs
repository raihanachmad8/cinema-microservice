using Microsoft.EntityFrameworkCore;
using MovieService.Domain.Entities;
using MovieService.Domain.Enums;

namespace MovieService.Infrastructure.Persistence
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        { }

        public DbSet<Movie> Movies { get; set; } // Change from Movies to Movies

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Index for Movie entity
            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.Title)
                .IsUnique();

            // Global Query Filter for Soft Delete
            modelBuilder.Entity<Movie>().HasQueryFilter(m => m.DeletedAt == null);

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
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "The Shawshank Redemption",
                    Genre = Genre.Drama,
                    DurationInMinutes = 120,
                    Description = "A former banker turned criminal tries to survive his life in a New England town.",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Movie
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "The Godfather",
                    Genre = Genre.Action,
                    DurationInMinutes = 140,
                    Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Movie
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "The Dark Knight",
                    Genre = Genre.Action,
                    DurationInMinutes = 150,
                    Description = "When Batman, Gordon and Harvey Dent launch an assault on the mob, they let the clown out of the box, the Joker, bent on turning Gotham on itself and bringing any heroes down to his level.",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Movie
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Title = "Pulp Fiction",
                    Genre = Genre.Drama,
                    DurationInMinutes = 130,
                    Description = "The lives of two mob hit men, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Movie
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Title = "The Lord of the Rings: The Return of the King",
                    Genre = Genre.Action,
                    DurationInMinutes = 170,
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Movie
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Title = "Inception",
                    Genre = Genre.Action,
                    DurationInMinutes = 140,
                    Description = "A thief who steals corporate secrets through use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Movie
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    Title = "The Matrix",
                    Genre = Genre.Action,
                    DurationInMinutes = 130,
                    Description = "When a beautiful stranger leads computer hacker Neo to the underworld, he discovers the shocking truth--the life he knows is the reality he's been trapped in.",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Movie
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    Title = "The Dark Knight Rises",
                    Genre = Genre.Action,
                    DurationInMinutes = 150,
                    Description = "A young boy learns that he is the man who killed his parents and he vows to find out who did this to him, before becoming the man he never wanted to be.",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                },
                new Movie
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Title = "The Lord of the Rings: The Two Towers",
                    Genre = Genre.Action,
                    DurationInMinutes = 170,
                    Description = "While Frodo and Sam edge closer to Mordor with the help of the shifty Gollum, the divided fellowship makes a stand against Sauron's new ally, Saruman, and his hordes of Isengard.",
                    CreatedAt = DateTime.Parse("2025-03-03"),
                    UpdatedAt = DateTime.Parse("2025-03-03"),
                }
            );
        }
    }
}