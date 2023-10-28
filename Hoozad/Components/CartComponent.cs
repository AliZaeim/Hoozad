using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class CartComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartComponent(IStoreService storeService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {             
            Cart? cart = null;
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                string? cartId = Core.Utility.CookieExtensions.ReadCookie("crt");
                if (cartId != null)
                {
                    cart = await _storeService.GetCartByIdAsync(cartId);
                }
            }
            return await Task.FromResult(View("/Pages/Components/_GetCart.cshtml", cart));
        }
    }
}

