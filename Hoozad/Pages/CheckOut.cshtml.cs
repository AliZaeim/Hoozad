using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace Web.Pages
{
    public class ChackOutModel : PageModel
    {
        private readonly IStoreService _storeService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChackOutModel(IStoreService storeService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _storeService = storeService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        public class CheckoutVM
        {
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
            [Display(Name = "نام خریدار")]
            public string? BuyerName { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
            [Display(Name = "نام خانوادگی خریدار")]
            public string? BuyerFamily { get; set; }
            [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
            [Display(Name = "تلفن همراه خریدار")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string? BuyerCellphone { get; set; }
            [Display(Name = "خریدار و تحویل گیرنده یک نفر هستند؟")]
            public bool BuyerIsRecepient { get; set; }
            [Display(Name = "نام تحویل گیرنده")]
            public string? RecepientName { get; set; }
            [Display(Name = "نام خانوادگی تحویل گیرنده")]
            public string? RecepientFamily { get; set; }
            [Display(Name = "تلفن همراه تحویل گیرنده")]
            [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
            public string? RecepientCellphone { get; set; }
            [Display(Name = "استان")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public int? StateId { get; set; }
            [Display(Name = "شهرستان")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public int? CountyId { get; set; }
            [Display(Name = "آدرس تحویل")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string? Address { get; set; }
            
            [Display(Name = "کد پستی")]
            public string? PostalCode { get; set; }
            [Display(Name = "یادداشت")]
            [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
            public string? Comment { get; set; }
            [Display(Name = "کرایه حمل")]
            public int ShippingCost { get; set; }= 0;

            [Display(Name = "ارسال با تیپاکس")]
            public bool ShippingWithTipax { get; set; }
            [Display(Name = "ارسال با پست پیشتاز")]
            public bool ShippingWithPost { get; set; } = true;
            [Display(Name = "ارسال با اسنپ")]
            public bool ShippingWithSnapp { get; set; }
            public List<State> States { get; set; } = new();
            public List<County> Counties { get; set; } = new();
            public string? CartId { get; set; }
            public Cart Cart { get; set; } = new();
            public string? ShippingMessage { get; set; }
            public int CartSum { get; set; }
            
            public bool IsPost { get; set; }
            #region Relations
            [ForeignKey(nameof(StateId))]
            public State? State { get; set; }
            [ForeignKey(nameof(CountyId))]
            public County? County { get; set; }
            #endregion

        }
        [BindProperty]
        public CheckoutVM CheckoutVModel { get; set; } = new();
        public Cart Cart { get; set; } = new();
        public List<CartItem> CartItems { get; set; } = new();
        public SiteInfo SiteInfo { get; set; } = new();
        public NextPayPaymentResultVM NextPayPaymentResultVM { get; set; } = new();
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
                    CheckoutVModel.CartId = guid;
                    CartItems = await _storeService.GetCartItemsofCart(guid);
                    
                }

            }
            if (User.Identity!.IsAuthenticated)
            {
                User user = await _storeService.GetUserByUserNameAsync(User.Identity.Name!);

                CheckoutVModel.BuyerName = user?.Name;
                CheckoutVModel.BuyerFamily = user?.Family;
                CheckoutVModel.BuyerCellphone = user?.Cellphone;

            }
            CheckoutVModel.States = await _storeService.GetStatesAsync();
            CheckoutVModel.BuyerIsRecepient = true;
        }
        public async Task<IActionResult> OnPost()
        {
            SiteInfo siteInfo = await _storeService.GetLastSiteInfoAsync();
            if (!ModelState.IsValid)
            {

                Core.Utility.CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                if (Core.Utility.CookieExtensions.ExistCookie("crt"))
                {
                    string guid = Core.Utility.CookieExtensions.ReadCookie("crt").ToString();
                    if (!string.IsNullOrEmpty(guid))
                    {
                        Cart = await _storeService.GetCartByIdAsync(guid);
                        
                    }

                }
                CheckoutVModel.States = await _storeService.GetStatesAsync();
                CheckoutVModel.Counties = await _storeService.GetCountiesofState(CheckoutVModel.StateId.GetValueOrDefault());
                
            }
            if (!string.IsNullOrEmpty(CheckoutVModel.CartId))
            {
                int? userId = null;
                if (await _userService.ExistUserByCellphoneAsync(CheckoutVModel.BuyerCellphone!))
                {
                    User user = await _userService.GetUserByCellphone(CheckoutVModel.BuyerCellphone!);
                    if (user != null)
                    {
                        userId = user.Id;
                    }
                }
                else
                {
                    string password = Core.Prodocers.Generators.GenerateUniqueString(4, 0, 2, 2);
                    User user = new()
                    {
                        Name = CheckoutVModel.BuyerName,
                        Family = CheckoutVModel.BuyerFamily,
                        Cellphone = CheckoutVModel.BuyerCellphone,
                        UserName = CheckoutVModel.BuyerCellphone,
                        IsActive = true,
                        RegDate = DateTime.Now,
                        Password = password
                    };
                    if (CheckoutVModel.BuyerIsRecepient)
                    {
                        user.Address = CheckoutVModel.Address;
                        user.CountyId = CheckoutVModel.CountyId;
                        user.PostalCode = CheckoutVModel.PostalCode;
                    }
                    UserRole userRole = new()
                    {
                        User = user,
                        RoleId = 2,
                        RegisterDate = DateTime.Now,
                        IsActive = true,

                    };
                    _userService.CreateUserRole(userRole);
                    await _userService.SaveChangesAsync();
                    bool res = await _userService.SendVerificationCodeAsync(password,CheckoutVModel.BuyerCellphone!);
                    userId = user.Id;
                }
                Cart crt = await _storeService.GetCartByIdAsync(CheckoutVModel.CartId);
                List<CartItem> cartItems = await _storeService.GetCartItemsofCart(crt.Id.ToString());
                int cartSum = 0;
                if (cartItems != null)
                {
                    cartSum = cartItems.SelectMany(_ => _.CartProductInfos).Sum(x => x.Quantity * x.NetPrice.GetValueOrDefault());
                }
                int ShippingValue = 0;
                List<string>? cartTrackingCodes = new();
                List<Cart> carts = await _storeService.GetCartsAsync();
                cartTrackingCodes = carts.Select(_ => _.TracingNumber!).ToList();
                if (cartSum < siteInfo.FreeShippingValue)
                {
                    if (!CheckoutVModel.ShippingWithSnapp && !CheckoutVModel.ShippingWithPost && !CheckoutVModel.ShippingWithTipax)
                    {
                        ModelState.AddModelError("CheckoutVModel.ShippingPaymentOnDelivery", "روش ارسال کالا را انتخاب کنید !");
                        CheckoutVModel.ShippingMessage = "روش ارسال کالا را انتخاب کنید !";
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
                        CheckoutVModel.States = await _storeService.GetStatesAsync();
                        CheckoutVModel.Counties = await _storeService.GetCountiesofState(CheckoutVModel.StateId.GetValueOrDefault());
                        return Page();
                    }
                    if (CheckoutVModel.ShippingWithPost)
                    {
                        if (siteInfo != null)
                        {
                            if (siteInfo.ShippingCost == null)
                            {
                                if (siteInfo.SiteCurrency == "تومان")
                                {
                                    ShippingValue = 40000;
                                }
                                else
                                {
                                    ShippingValue = 400000;
                                }

                            }
                            else
                            {                                
                                ShippingValue = siteInfo.ShippingCost.GetValueOrDefault();
                            }
                        }
                    }
                }
                cartSum += ShippingValue;

                crt.BuyerName = CheckoutVModel.BuyerName;
                crt.BuyerFamily = CheckoutVModel.BuyerFamily;
                crt.BuyerCellphone = CheckoutVModel.BuyerCellphone;
                crt.BuyerIsRecepient = CheckoutVModel.BuyerIsRecepient;
                crt.RecipientName = CheckoutVModel.RecepientName;
                crt.RecipientFamily = CheckoutVModel.RecepientFamily;
                crt.RecipientPhone = CheckoutVModel.RecepientCellphone;
                crt.Address = CheckoutVModel.Address;
                crt.PostalCode = CheckoutVModel.PostalCode;
                crt.ShippingWithTipax = CheckoutVModel.ShippingWithTipax;
                crt.ShippingWithSnapp = CheckoutVModel.ShippingWithSnapp;
                crt.ShippingWithPost = CheckoutVModel.ShippingWithPost;
                State state = await _storeService.GetStateByIdAsync(CheckoutVModel.StateId.GetValueOrDefault());
                crt.StateName = state?.StateName;
                County county = await _storeService.GetCountyByIdAsync(CheckoutVModel.CountyId.GetValueOrDefault());
                crt.CountyName = county?.CountyName;
                crt.Freight = ShippingValue;
                crt.UserId = userId;
                crt.PaymentDate = DateTime.Now;
                crt.CartSum = cartSum;
                string TrCode = Core.Prodocers.Generators.GenerateUniqueString(cartTrackingCodes!.ToList(), 0, 0, 8, 0);
                crt.TracingNumber = TrCode;
                //crt.CheckOut = true;

                _storeService.UpdateCart(crt);
                await _storeService.SaveChangesAsync();                
                
                string userName = "نام کاربری شما : " + CheckoutVModel.BuyerCellphone;
                bool send = _userService.SendUserName_and_Password(userName, "passwoed", CheckoutVModel.BuyerCellphone!);
                TempData["successBuy"] = "yes";
                TempData["crtTracknumber"] = TrCode;
                string cur = "IRR";
                if (siteInfo != null)
                {
                    if (!string.IsNullOrEmpty(siteInfo.SiteCurrency))
                    {
                        if (siteInfo.SiteCurrency == "تومان")
                        {
                            cur = "IRT";
                        }
                    }
                }
                return RedirectToAction("GoToPayment", "SitePages", new {cartId=crt.Id.ToString(), BackUrl = "https://hoozadstyle.ir", Currency = cur});
                //return RedirectToPage(nameof(Index));
                

            }



            return Page();
        }
        
        public async Task<IActionResult> OnGetCountiesofState(int sId)
        {
            List<County> counties = await _storeService.GetCountiesofState(sId);
            return Partial("_CountiesOfState", counties.OrderBy(x => x.CountyName).ToList());
        }
    }
}
