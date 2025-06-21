using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projects.Models;
using projects.Servises;

namespace projects.Pages.Exchange
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CurrencyExchangeService _exchangeService;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public decimal Coins { get; set; } = 100;
        public string? Message { get; set; }

        public IndexModel(CurrencyExchangeService exchangeService, UserManager<User> userManager)
        {
            _exchangeService = exchangeService;
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

            try
            {
                var premium = await _exchangeService.ExchangeAsync(Guid.Parse(userIdStr), Coins);
                Message = $"Received {premium} premium coins";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Page();
        }
    }
}
