using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class BestSellingProductComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        public BestSellingProductComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.TagsList.Any(a => a.Trim() == "پرفروش" || a.Trim() == "پر فروش")).ToList();
            return await Task.FromResult(View("/Pages/Components/_GetBestSellingProduct.cshtml",products.ToList()));
        }
    }
}
