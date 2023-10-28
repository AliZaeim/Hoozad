using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class SuggestedProductsComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        public SuggestedProductsComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.TagsList.Any(a => a == "پیشنهادی")).ToList();
            return await Task.FromResult(View("/Pages/Components/_GetSuggestedProducts.cshtml",products.ToList()));
        }
    }
}
