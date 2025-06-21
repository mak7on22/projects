using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        [Microsoft.AspNetCore.Mvc.BindProperty]
        public ProfileInput Input { get; set; } = new();

        public class ProfileInput
        {
            public string? DisplayName { get; set; }
            public string? AvatarUrl { get; set; }
        }

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
                Input.DisplayName = UserInfo.DisplayName;
                Input.AvatarUrl = UserInfo.AvatarUrl;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToPage();

            user.DisplayName = Input.DisplayName;
            user.AvatarUrl = Input.AvatarUrl;
            await _userManager.UpdateAsync(user);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostConfirmEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && !user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToPage();
        }
    }
}
