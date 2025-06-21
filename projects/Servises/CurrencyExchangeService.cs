using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Servises
{
    /// <summary>
    /// Handles converting regular coins into premium coins with a daily limit.
    /// </summary>
    public class CurrencyExchangeService
    {
        private readonly ApplicationDbContext _context;
        private const decimal ExchangeRate = 100m; // 100 coins -> 1 premium
        private const decimal DailyLimit = 1000m; // max coins per day

        public CurrencyExchangeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> ExchangeAsync(Guid userId, decimal coins)
        {
            if (coins <= 0) throw new ArgumentException("Amount must be positive", nameof(coins));
            if (coins % ExchangeRate != 0) throw new InvalidOperationException($"Amount must be multiple of {ExchangeRate}");
            var today = DateTime.UtcNow.Date;
            var spentToday = await _context.ExchangeTransactions
                .Where(t => t.UserId == userId && t.CreatedAt >= today)
                .SumAsync(t => t.CoinsSpent);
            if (spentToday + coins > DailyLimit)
                throw new InvalidOperationException("Daily exchange limit exceeded");

            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null || wallet.Balance < coins)
                throw new InvalidOperationException("Not enough coins");

            wallet.Balance -= coins;
            var premium = coins / ExchangeRate;
            wallet.PremiumBalance += premium;

            _context.ExchangeTransactions.Add(new ExchangeTransaction
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CoinsSpent = coins,
                PremiumReceived = premium,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return premium;
        }
    }
}
