using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projects.Servises;

namespace projects.Pages.Quests
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly QuestService _questService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(QuestService questService, IHttpContextAccessor httpContextAccessor)
        {
            _questService = questService;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<projects.Models.Quest> Quests { get; set; } = new();
        public List<Guid> CompletedQuestIds { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("sub")!.Value);
            Quests = await _questService.GetActiveQuestsAsync();
            CompletedQuestIds = await _questService.GetCompletedQuestIdsAsync(userId);
        }

        public async Task<IActionResult> OnPostAsync(Guid questId)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("sub")!.Value);
            await _questService.CompleteQuestAsync(userId, questId);
            return RedirectToPage();
        }
    }
}
