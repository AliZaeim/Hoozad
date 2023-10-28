using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class NewProductComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NewProductComponent(IStoreService storeService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string guid = string.Empty;
            SepcialProducts specialProducts = new();
            
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                guid = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
                Cart? cart = await _storeService.GetCartByIdAsync(guid);
                if (cart == null)
                {
                    bool ex = Core.Utility.CookieExtensions.RemoveCookie("crt");
                }
                else
                {
                    specialProducts.CartId = cart.Id.ToString();
                }
            }
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.TagsList.Any(a => a.Trim() == "جدید")).ToList();
            specialProducts.Products = products;
            return await Task.FromResult(View("/Pages/Components/_GetNewProduct.cshtml",specialProducts));
        }
    }
}
