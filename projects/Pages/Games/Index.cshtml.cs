using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projects.Models;

namespace projects.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<Game> Games { get; set; } = new List<Game>();

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Games = await _context.Games.AsNoTracking().ToListAsync();
        }
    }
}
