using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductDetailsModel(IStoreService storeService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public Product Pproduct { get; set; } = new();
        [BindProperty]
        public List<ProductGroup> ProductGroups { get; set; } = new();
        [BindProperty]
        public SiteInfo SiteInfo { get; set; } = new();
        public string? CartId { get; set; }
        public ProductGroup ProductGroup { get; set; } = new();
        public List<Product> ComponentProducts { get; set; } = new();
        public List<(int Height, int Price, int NetPrice, int DiscountVal, int DiscountPercent)> ProductPricesFinance { get; set; } = new();
        public async Task<IActionResult> OnGet(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound("محصول مشخص نشده است !");
            }
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);

            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                CartId = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
            }
            Pproduct = await _storeService.GetProductByCodeAsync(code);
            ProductGroups = await _storeService.GetProductGroupsAsync();
            SiteInfo = await _storeService.GetLastSiteInfoAsync();
            if (Pproduct != null)
            {
                if (Pproduct.Views.HasValue)
                {
                    Pproduct.Views += 1;
                }
                else
                {
                    Pproduct.Views = 1;
                }
                _storeService.UpdateProduct(Pproduct);
                await _storeService.SaveChangesAsync();
                ProductGroup = await _storeService.GetProductGroupAsync(Pproduct.ProductGroupId.GetValueOrDefault());
            }
            foreach (var item in Pproduct!.ProductComponents.ToList())
            {
                Product product = await _storeService.GetProductByCodeAsync(item.ProductCode!);
                if (product != null)
                {
                    ComponentProducts.Add(product);
                }

            }
            ProductPricesFinance = await _storeService.GetProductPricesFinanceAsync(code);
            return Page();
        }
        

    }
}
