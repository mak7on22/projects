using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Servises
{
    public class MatchService
    {
        private readonly ApplicationDbContext _context;
        private const decimal WinReward = 10m;

        public MatchService(ApplicationDbContext context)
        {
            _context = context;
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

            await _context.SaveChangesAsync();
        }
    }
}
