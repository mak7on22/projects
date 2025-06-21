using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Pages.Games
{
    public class StatsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Game? Game { get; set; }
        public List<GameWinStat> Stats { get; set; } = new();

        public StatsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(Guid? id)
        {
            if (id == null)
                return;

            Game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
            if (Game == null)
                return;

            Stats = await _context.GameWinStats
                .Where(s => s.GameId == id)
                .Include(s => s.User)
                .OrderByDescending(s => s.Wins)
                .Take(20)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
