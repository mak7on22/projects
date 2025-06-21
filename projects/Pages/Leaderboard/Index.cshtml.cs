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
            var query = _context.GameWinStats
                .GroupBy(s => s.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    Wins = g.Sum(x => x.Wins)
                })
                .OrderByDescending(r => r.Wins)
                .Take(100);

            Rows = await query
                .Join(_context.Users, q => q.UserId, u => u.Id, (q, u) => new Row
                {
                    User = u,
                    Wins = q.Wins
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
