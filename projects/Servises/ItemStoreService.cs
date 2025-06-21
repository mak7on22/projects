using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Servises
{
    public class ItemStoreService
    {
        private readonly ApplicationDbContext _context;

        public ItemStoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            return await _context.Items.AsNoTracking().ToListAsync();
        }

        public async Task PurchaseItemAsync(Guid userId, Guid itemId)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null) throw new InvalidOperationException("Item not found");

            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null) throw new InvalidOperationException("Wallet not found");

            if (item.IsPremium)
            {
                if (wallet.PremiumBalance < item.Price) throw new InvalidOperationException("Not enough premium coins");
                wallet.PremiumBalance -= item.Price;
            }
            else
            {
                if (wallet.Balance < item.Price) throw new InvalidOperationException("Not enough coins");
                wallet.Balance -= item.Price;
            }

            _context.UserItems.Add(new UserItem
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ItemId = itemId,
                PurchasedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }
    }
}
