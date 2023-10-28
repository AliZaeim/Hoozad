using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class ProductCategoryComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        public ProductCategoryComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var groups = await _storeService.GetProductGroupsAsync();
            groups = groups.Where(w => w.IsActive).ToList();
            return await Task.FromResult(View("/Pages/Components/_GetProductCategory.cshtml",groups));
        }
    }
}
