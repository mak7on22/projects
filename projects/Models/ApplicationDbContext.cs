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
        public DbSet<Achievement> Achievements => Set<Achievement>();
        public DbSet<UserAchievement> UserAchievements => Set<UserAchievement>();
        public DbSet<Quest> Quests => Set<Quest>();
        public DbSet<UserQuest> UserQuests => Set<UserQuest>();

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

            builder.Entity<UserAchievement>()
                .HasKey(ua => ua.Id);

            builder.Entity<UserAchievement>()
                .HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId);

            builder.Entity<UserAchievement>()
                .HasOne(ua => ua.Achievement)
                .WithMany(a => a.UserAchievements)
                .HasForeignKey(ua => ua.AchievementId);

            builder.Entity<UserQuest>()
                .HasKey(uq => uq.Id);

            builder.Entity<UserQuest>()
                .HasOne(uq => uq.User)
                .WithMany()
                .HasForeignKey(uq => uq.UserId);

            builder.Entity<UserQuest>()
                .HasOne(uq => uq.Quest)
                .WithMany(q => q.UserQuests)
                .HasForeignKey(uq => uq.QuestId);
        }
    }
}
