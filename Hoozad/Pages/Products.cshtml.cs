using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using RestSharp;
using System.Security.Policy;

namespace Web.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductsModel(IStoreService storeService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public List<ProductGroup> ProductGroups { get; set; } = new();
        [BindProperty]
        public List<Product> Products { get; set; } = new();
        [BindProperty]
        public SiteInfo SiteInfo { get; set; } = new();
        [BindProperty]
        public ProductGroup ProductGroup { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public string? CartId { get; set; }
        public List<IGrouping<string, string>> GroupingTopTags { get; set; } = new();
        public List<IGrouping<ProductPrice, ProductPrice>> ProductPrices { get; set; } = new();
        public List<ProductColor> ProductColors { get; set; } = new();
        public string? PageSubTitle { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; } = string.Empty;
        public async Task OnGet(int? gId, int? pageId = 1, string tag = "", string color = "", string search = "", int? height = null)
        {
            Products = await _storeService.GetProductsAsync();
            Products = Products.Where(w => w.IsActive).ToList();
            ProductGroups = await _storeService.GetProductGroupsAsync();
            SiteInfo = await _storeService.GetLastSiteInfoAsync();
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);

            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                CartId = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
            }
            if (Products != null)
            {
                ProductColors = Products.SelectMany(x => x.ProductColors).ToList();

                List<ProductPrice> sizes = Products.SelectMany(x => x.ProductPrices).ToList();
                ProductPrices = sizes.GroupBy(g => g).OrderByDescending(r => r.Count()).ToList();
                Tags = Products.SelectMany(x => x.TagsList).ToList();
                GroupingTopTags = Tags.GroupBy(g => g.Trim()).OrderByDescending(r => r.Count()).ToList();
            }
            if (gId != null)
            {
                ProductGroup = await _storeService.GetProductGroupAsync(gId.Value);
                if (ProductGroup != null)
                {
                    PageSubTitle = "گروه " + ProductGroup.Title;
                    Products = Products!.Where(w => w.ProductGroupId == gId.Value).ToList();
                }
            }
            if (height != null)
            {
                PageSubTitle = "قد های " + height;
                Products = Products!.Where(w => w.ProductPrices.Any(x => x.Height == height.Value)).ToList();
            }
            if (!string.IsNullOrEmpty(tag))
            {
                PageSubTitle = "برچسب " + tag;
                Products = Products!.Where(w => w.TagsList.Any(x => x.Trim() == tag.Replace("-", " "))).ToList();
            }
            if (!string.IsNullOrEmpty(color))
            {
                PageSubTitle = "رنگ های " + color;
                Products = Products!.Where(w => w.ProductColors.Any(x => x.ColorName == color)).ToList();
            }
            if (!string.IsNullOrEmpty(search))
            {
                PageSubTitle = "جستجو شده بر اساس: " + search;
                Products = Products!.Where(w => w.ProductGroup!.Title!.Contains(search) ||
                w.TagsList.Any(x => x == search) ||
                w.ProductName!.Contains(search)).ToList();
            }
            if (Products!.Count % 12 == 0)
            {
                TotalPage = Products.Count / 12;
            }
            else
            {
                TotalPage = (Products.Count / 12) + 1;
            }
            CurrentPage = pageId!.Value;
            Products = Products!.Skip((pageId.GetValueOrDefault() - 1) * 12).Take(pageId.GetValueOrDefault() * 12).ToList();
        }
        public async Task<IActionResult> OnGetAddtoCart(string where, int pcount, string pid, string pcolor, string pheight, string psize, string retUrl = "/Products")
        {
            Guid pguid = Guid.Parse(pid);
            Product product = await _storeService.GetProductAsync(pguid);
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string guid = string.Empty;
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                guid = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
                Cart? cart = await _storeService.GetCartByIdAsync(guid);
                if (cart == null)
                {
                    bool ex = Core.Utility.CookieExtensions.RemoveCookie("crt");
                }
            }
            if (product != null)
            {
                (_, int netprice, int discount, _, _) = await _storeService.GetProductFinanceInfoAsync(pguid);


                AddToCartVM addToCartVM = new()
                {
                    Price = product.ProductPrice.GetValueOrDefault(),
                    NetPrice = netprice,
                    Discount = discount,
                    ProductCode = product.ProductCode,
                    Quantity = pcount,
                    Comment = "خرید از طریق " + where,
                    prColor = pcolor,
                    prHeight = pheight,
                    prSize = psize,
                    ReturnUrl = retUrl
                };
                if (!string.IsNullOrEmpty(guid))
                {
                    addToCartVM.CartId = Guid.Parse(guid);
                }
                (Cart cart, string result) = await _storeService.AddToCart(addToCartVM);
                if (cart != null)
                {

                    Core.Utility.CookieExtensions.SetCookie("crt", cart.Id.ToString(), DateTime.Now.AddHours(72));
                }
                return new JsonResult(result);
            }
            return new JsonResult("no");
        }

        public async Task<IActionResult> GetShopCart()
        {
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string guid = string.Empty;
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                guid = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
            }
            if (!string.IsNullOrEmpty(guid))
            {
                Cart cart = await _storeService.GetCartByIdAsync(guid);
                return ViewComponent("CartComponent", new { cart });
            }
            return Content("سبد خرید خالی است !");
        }

        public async Task<IActionResult> OnGetProductPriceInfo(string prCode, int? height, int? size)
        {
            if (string.IsNullOrEmpty(prCode))
            {
                return new JsonResult("کد محصول مشخص نشده است !");
            }
            if (height == null)
            {
                return new JsonResult("قد مشخص نشده است !");
            }
            if (size == null)
            {
                return new JsonResult("سایز مشخص نشده است !");
            }
            (int Pprice, int netPPrice, int discount, int? disPercent, bool exist) = await _storeService.GetProductPriceFacilityData(prCode, size, height);
            return new JsonResult(new { Price = Pprice, NetPrice = netPPrice, Discount = discount, DiscountPercent = disPercent, Exist = exist });
        }
        
        public async Task<PartialViewResult> OnGetShowProductSizes(string pId)
        {
            List<ProductSize> productSizes = await _storeService.GetProductSizesByProductIdAsync(Guid.Parse(pId));
            return Partial("_ProductSizes", productSizes.ToList());
        }
        public async Task<IActionResult> OnGetCheckCartForAdding(string pId)
        {
            bool allowAdd = false;
            Product product = await _storeService.GetProductAsync(Guid.Parse(pId));
            if (product == null)
            {
                return new JsonResult(allowAdd);
            }
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            string guid = string.Empty;
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                guid = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
            }

            if (product.AllowedSinglePurchase != false)
            {
                if (!string.IsNullOrEmpty(guid))
                {
                    Cart cart = await _storeService.GetCartByIdAsync(guid);
                    if (cart != null)
                    {
                        foreach (var item in cart.CartItems.ToList())
                        {
                            Product product1 = await _storeService.GetProductByCodeAsync(item.ProductCode!);
                            if (product1 != null)
                            {
                                if (product1.AllowedSinglePurchase == true)
                                {
                                    allowAdd = true;
                                }
                            }
                        }
                    }
                }
            }

            return new JsonResult(allowAdd);
        }
        
        public async Task<IActionResult> OnPostCheckRemoveCartItem(int ciId, string cId)
        {
            (bool canRemove, bool wasRemoved, string message, List<int> CIsAlsoMustRemove) Res = await _storeService.Check_cartItemDeletionStatusAsync(ciId, cId);
            if (Res.canRemove)
            {
                if (Res.wasRemoved)
                {
                    await _storeService.SaveChangesAsync();
                    return new JsonResult(new { canRemove = "yes", removed = "yes", mess = string.Empty, remCIs = Res.CIsAlsoMustRemove });
                }
                else
                {
                    return new JsonResult(new { canRemove = "no", removed = "no", mess = Res.message, remCIs = Res.CIsAlsoMustRemove });
                }
            }
            else
            {
                return new JsonResult(new { canRemove = "no", removed = "no", mess = Res.message, remCIs = Res.CIsAlsoMustRemove });
            }
            
        }
        public async Task<IActionResult> OnPostCheckRemoveCPIFromCartItem(int cpiId, string cId)
        {
            (bool canRemove, bool wasRemoved, string message, List<int> CIsAlsoMustRemove) Res = await _storeService.Check_prInfoDeletionStatusAsync(cpiId, cId);
            if (Res.canRemove)
            {
                if (Res.wasRemoved)
                {
                    await _storeService.SaveChangesAsync();
                    return new JsonResult(new { canRemove = "yes", removed = "yes", mess = string.Empty, remCIs = Res.CIsAlsoMustRemove });
                }
                else
                {
                    return new JsonResult(new { canRemove = "no", removed = "no", mess = Res.message, remCIs = Res.CIsAlsoMustRemove });
                }
            }
            else
            {
                return new JsonResult(new { canRemove = "no", removed = "no", mess = Res.message, remCIs = Res.CIsAlsoMustRemove });
            }

        }
        public async Task<IActionResult> OnPostRemoveCartItems(int[] cisId, string cId)
        {

            bool Res = await _storeService.RemoveCIsFromCartAsync(cId!, cisId.ToList());
            if (Res)
            {
                _storeService.SaveChanges();
                return new JsonResult(new { rem = "yes" });
            }
            else
            {
                return new JsonResult(new { rem = "no" });
            }
        }

        
    }
}
