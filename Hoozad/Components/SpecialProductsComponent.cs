using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class SpecialProductsComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        public SpecialProductsComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string filterType)
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
            switch (filterType)
            {
                case "پرفروش":
                    {
                        specialProducts.Title = "محصولات پرفروش";
                        products = products.Where(w => w.TagsList.Any(a => a.Trim() == "پرفروش" || a.Trim() == "پر فروش")).ToList();
                        break;
                    }
                case "جدید":
                    {
                        specialProducts.Title = "محصولات جدید";
                        products = products.Where(w => w.TagsList.Any(a => a.Trim() == "جدید")).ToList();
                        break;
                    }
                case "محبوب":
                    {
                        specialProducts.Title = "محصولات محبوب";
                        products = products.Where(w => w.TagsList.Any(a => a.Trim() == "محبوب")).ToList();
                        break;
                    }
                case "برتر":
                    {
                        specialProducts.Title = "محصولات برتر";
                        products = products.Where(w => w.TagsList.Any(a => a.Trim() == "برتر")).ToList();
                        break;
                    }
                case "فصل":
                    {
                        specialProducts.Title = "محصولات فصل";
                        products = products.Where(w => w.TagsList.Any(a => a.Trim() == "فصل")).ToList();
                        break;
                    }
                case "پیشنهادی":
                    {
                        specialProducts.Title = "محصولات پیشنهادی";
                        products = products.Where(w => w.TagsList.Any(a => a.Trim() == "پیشنهادی")).ToList();
                        break;
                    }
                default:
                    break;
            }
            
            specialProducts.Products = products;
            return await Task.FromResult(View("/Pages/Components/_GetSpecialProducts.cshtml", specialProducts));
        }
    }
}
