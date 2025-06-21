using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Servises
{
    public class AchievementService
    {
        private readonly ApplicationDbContext _context;

        public AchievementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CheckAchievementsAsync(Guid userId)
        {
            // total wins across games
            var totalWins = await _context.GameWinStats
                .Where(s => s.UserId == userId)
                .SumAsync(s => s.Wins);

            var achieved = await _context.UserAchievements
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.AchievementId)
                .ToListAsync();

            var newAchievements = await _context.Achievements
                .Where(a => a.RequiredWins <= totalWins && !achieved.Contains(a.Id))
                .ToListAsync();

            foreach (var ach in newAchievements)
            {
                _context.UserAchievements.Add(new UserAchievement
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    AchievementId = ach.Id,
                    AchievedAt = DateTime.UtcNow
                });
            }

            if (newAchievements.Count > 0)
                await _context.SaveChangesAsync();
        }
    }
}
