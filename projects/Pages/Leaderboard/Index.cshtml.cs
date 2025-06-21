using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Pages.Leaderboard
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Row> Rows { get; set; } = new();

        public class Row
        {
            public User User { get; set; } = null!;
            public int Wins { get; set; }
        }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Rows = await _context.GameWinStats
                .Include(s => s.User)
                .GroupBy(s => new { s.UserId, s.User })
                .Select(g => new Row
                {
                    User = g.Key.User,
                    Wins = g.Sum(x => x.Wins)
                })
                .OrderByDescending(r => r.Wins)
                .Take(100)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
