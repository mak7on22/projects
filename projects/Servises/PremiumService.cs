using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Servises
{
    public class PremiumService
    {
        private readonly ApplicationDbContext _context;

        public PremiumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPremiumAsync(Guid userId, decimal amount)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null)
            {
                wallet = new Wallet { UserId = userId };
                _context.Wallets.Add(wallet);
            }

            wallet.PremiumBalance += amount;

            var tx = new PremiumTransaction
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Amount = amount,
                CreatedAt = DateTime.UtcNow
            };
            _context.PremiumTransactions.Add(tx);

            await _context.SaveChangesAsync();
        }
    }
}
