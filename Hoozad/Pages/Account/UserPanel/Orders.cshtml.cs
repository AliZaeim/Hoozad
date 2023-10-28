using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account.UserPanel
{
    [Authorize]
    public class OrdersModel : PageModel
    {
        
        private readonly IStoreService _storeService;
        public OrdersModel(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public List<CartItem> CartItems { get;set; } = new();
        
        public async Task OnGetAsync()
        {
            CartItems = await _storeService.GetOrdersCartItemsForLogin(User.Identity!.Name!);

        }
    }
}
