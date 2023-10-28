using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class CartModel : PageModel
    {
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartModel(IStoreService storeService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public Cart Cart { get; set; } = new();
        public SiteInfo SiteInfo { get; set; } = new();
        public async Task OnGet()
        {
            SiteInfo = await _storeService.GetLastSiteInfoAsync();
            Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
            
            if (Core.Utility.CookieExtensions.ExistCookie("crt"))
            {
                string guid = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
                if (!string.IsNullOrEmpty(guid))
                {
                    Cart = await _storeService.GetCartByIdAsync(guid);
                }
            }
        }
        public async Task<IActionResult> OnPostUpdateCart(List<CartItem> cartItems)
        {
            if (cartItems != null)
            {
                if (cartItems.Count != 0)
                {
                    foreach (var item in cartItems)
                    {
                        CartItem cartItem = await _storeService.GetCartItemByIdAsync(item.Id);
                        if (cartItem != null)
                        {
                            if (item.Quantity != 0)
                            {
                                
                                if (cartItem.Quantity != item.Quantity)
                                {
                                    cartItem.Quantity = item.Quantity;
                                    
                                }
                                _storeService.UpdateCartItem(cartItem);
                                await _storeService.SaveChangesAsync();

                            }
                            else
                            {
                                if (item.CartId != null)
                                {
                                    await _storeService.RemoveItemFromCart(item.CartId.Value.ToString(), item.Id);
                                    _storeService.SaveChanges();
                                }                                
                            }                            
                        }
                    }                     
                }
            }
            return new RedirectToPageResult("/Cart");
        }
        public async Task<IActionResult> OnPostTakeCoupen(string? coupen)
        {
            if(User.Identity!.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(coupen))
                {
                    DiscountCoupen? discountCoupen = await _storeService.GetDiscountCoupenByCodeAsync(coupen);
                    (bool Cvalid, List<(string Cmessage, string Cnote)>) Res = await _storeService.CheckCoupenByCodeAsync(coupen,User.Identity!.Name ?? "nothing");
                    
                }
            }
            else
            {
                
            }

            return RedirectToPage("/Cart");
        }
        
    }
}
