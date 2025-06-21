using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace projects.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<PremiumTransaction> PremiumTransactions => Set<PremiumTransaction>();
        public DbSet<GameWinStat> GameWinStats => Set<GameWinStat>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.UserId);

            builder.Entity<Match>()
                .HasOne(m => m.Player1)
                .WithMany(u => u.Matches)
                .HasForeignKey(m => m.Player1Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Match>()
                .HasOne(m => m.Player2)
                .WithMany()
                .HasForeignKey(m => m.Player2Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Match>()
                .HasOne(m => m.Winner)
                .WithMany()
                .HasForeignKey(m => m.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PremiumTransaction>()
                .HasKey(t => t.Id);

            builder.Entity<PremiumTransaction>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<GameWinStat>()
                .HasIndex(s => new { s.GameId, s.UserId })
                .IsUnique();

            builder.Entity<GameWinStat>()
                .HasOne(s => s.Game)
                .WithMany()
                .HasForeignKey(s => s.GameId);

            builder.Entity<GameWinStat>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);
        }
    }
}
