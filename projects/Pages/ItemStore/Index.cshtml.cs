using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projects.Models;
using projects.Servises;

namespace projects.Pages.ItemStore
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ItemStoreService _storeService;
        private readonly IHttpContextAccessor _http;

        public IndexModel(ItemStoreService storeService, IHttpContextAccessor http)
        {
            _storeService = storeService;
            _http = http;
        }

        public List<Item> Items { get; set; } = new();
        public string? Message { get; set; }

        public async Task OnGetAsync()
        {
            Items = await _storeService.GetItemsAsync();
        }

        public async Task<IActionResult> OnPostAsync(Guid itemId)
        {
            var userId = Guid.Parse(_http.HttpContext!.User.FindFirst("sub")!.Value);
            await _storeService.PurchaseItemAsync(userId, itemId);
            Message = "Item purchased!";
            Items = await _storeService.GetItemsAsync();
            return Page();
        }
    }
}
