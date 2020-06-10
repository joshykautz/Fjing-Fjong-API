using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FjingFjongApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .Property(p => p.Rating)
                .HasDefaultValue(1000);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.MatchesOne)
                .WithOne(m => m.PlayerOne)
                .HasForeignKey(m => m.PlayerOneId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.MatchesTwo)
                .WithOne(m => m.PlayerTwo)
                .HasForeignKey(m => m.PlayerTwoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.MatchesThree)
                .WithOne(m => m.PlayerThree)
                .HasForeignKey(m => m.PlayerThreeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.MatchesFour)
                .WithOne(m => m.PlayerFour)
                .HasForeignKey(m => m.PlayerFourId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            DateTime now = DateTime.Now;

            foreach (EntityEntry changedEntity in ChangeTracker.Entries())
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        changedEntity.Property("Created").CurrentValue = now;
                        changedEntity.Property("Updated").CurrentValue = now;
                        break;

                    case EntityState.Modified:
                        changedEntity.Property("Updated").CurrentValue = now;
                        break;
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
