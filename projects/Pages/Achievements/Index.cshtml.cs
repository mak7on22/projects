using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Pages.Achievements
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IList<Achievement> AllAchievements { get; set; } = new List<Achievement>();
        public IList<Guid> UserAchievementIds { get; set; } = new List<Guid>();

        public IndexModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            AllAchievements = await _context.Achievements.AsNoTracking().ToListAsync();
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr != null)
            {
                var userId = Guid.Parse(userIdStr);
                UserAchievementIds = await _context.UserAchievements
                    .Where(ua => ua.UserId == userId)
                    .Select(ua => ua.AchievementId)
                    .ToListAsync();
            }
        }
    }
}
