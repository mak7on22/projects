using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Pages.Profile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public User? UserInfo { get; set; }
        public int WinsCount { get; set; }

        public IndexModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return;

            var guid = Guid.Parse(userId);
            UserInfo = await _context.Users
                .Include(u => u.Wallet)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == guid);

            if (UserInfo != null)
            {
                WinsCount = await _context.Matches.CountAsync(m => m.WinnerId == guid);
            }
        }
    }
}
