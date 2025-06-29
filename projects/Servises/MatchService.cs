using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Servises
{
    public class MatchService
    {
        private readonly ApplicationDbContext _context;
        private readonly AchievementService _achievementService;
        private const decimal WinReward = 10m;

        public MatchService(ApplicationDbContext context, AchievementService achievementService)
        {
            _context = context;
            _achievementService = achievementService;
        }

        public async Task RecordMatchResultAsync(Guid matchId, Guid winnerId)
        {
            var match = await _context.Matches.Include(m => m.Game).FirstOrDefaultAsync(m => m.Id == matchId);
            if (match == null)
                throw new InvalidOperationException("Match not found");

            match.WinnerId = winnerId;
            match.EndedAt = DateTime.UtcNow;

            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == winnerId);
            if (wallet != null)
            {
                wallet.Balance += WinReward;
            }

            var stat = await _context.GameWinStats.FirstOrDefaultAsync(s => s.GameId == match.GameId && s.UserId == winnerId);
            if (stat == null)
            {
                stat = new GameWinStat
                {
                    Id = Guid.NewGuid(),
                    GameId = match.GameId,
                    UserId = winnerId,
                    Wins = 1
                };
                _context.GameWinStats.Add(stat);
            }
            else
            {
                stat.Wins += 1;
            }

            await _context.SaveChangesAsync();

            await _achievementService.CheckAchievementsAsync(winnerId);
        }
    }
}
