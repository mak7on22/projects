using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Servises
{
    public class QuestService
    {
        private readonly ApplicationDbContext _context;

        public QuestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Quest>> GetActiveQuestsAsync()
        {
            return await _context.Quests.ToListAsync();
        }

        public async Task<List<Guid>> GetCompletedQuestIdsAsync(Guid userId)
        {
            return await _context.UserQuests
                .Where(q => q.UserId == userId && q.IsCompleted)
                .Select(q => q.QuestId)
                .ToListAsync();
        }

        public async Task CompleteQuestAsync(Guid userId, Guid questId)
        {
            var uq = await _context.UserQuests
                .FirstOrDefaultAsync(q => q.UserId == userId && q.QuestId == questId);
            if (uq == null)
            {
                uq = new UserQuest
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    QuestId = questId,
                    IsCompleted = true,
                    CompletedAt = DateTime.UtcNow
                };
                _context.UserQuests.Add(uq);
            }
            else if (!uq.IsCompleted)
            {
                uq.IsCompleted = true;
                uq.CompletedAt = DateTime.UtcNow;
            }

            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet != null)
            {
                var quest = await _context.Quests.FirstOrDefaultAsync(q => q.Id == questId);
                if (quest != null)
                {
                    wallet.Balance += quest.RewardCoins;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
