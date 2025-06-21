using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projects.Models;
using projects.Servises;

namespace projects.Pages.Shop
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly PremiumService _premiumService;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public decimal Amount { get; set; } = 100;

        public string? Message { get; set; }

        public IndexModel(PremiumService premiumService, UserManager<User> userManager)
        {
            _premiumService = premiumService;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdStr = _userManager.GetUserId(User);
            if (userIdStr == null)
                return RedirectToPage();

            await _premiumService.AddPremiumAsync(Guid.Parse(userIdStr), Amount);
            Message = $"Added {Amount} coins";
            return Page();
        }
    }
}
