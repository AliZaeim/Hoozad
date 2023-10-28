using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DataLayer.Entities.Supplementary;

namespace Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperties]
        public class LoginVM
        {
            [Display(Name = "نام کاربری")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string? UserName { get; set; }
            [Display(Name = "رمز ورود")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string? Password { get; set; }
            [Display(Name = "مرا به خاطر بسپار")]
            public bool RememberMe { get; set; }
            public string? RetUrl { get; set; }
        }
        [BindProperty]
        public LoginVM LoginVModel { get; set; } = new();
        public SiteInfo SiteInfo { get; set; } = new();
        public async Task OnGet(string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                LoginVModel.RetUrl = ReturnUrl;
            }
            SiteInfo = await _userService.GetLastSiteInfoAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!await _userService.ExistUserByPasswordUserNameAsync(LoginVModel.Password!, LoginVModel.UserName!))
            {
                ModelState.AddModelError("LoginVModel.UserName", "نام کاربری یا رمز ورود اشتباه است !");
                return Page();
            }
            if (!await _userService.ExistUserByUserNameAsync(LoginVModel.UserName!))
            {
                ModelState.AddModelError("LoginVModel.UserName", "نام کاربری یا رمز ورود اشتباه است !");
                return Page();
            }
            if (!await _userService.ExistUserByPasswordAsync(LoginVModel.Password!))
            {
                ModelState.AddModelError("LoginVModel.UserName", "نام کاربری یا رمز ورود اشتباه است !");
                return Page();
            }
            User user = await _userService.GetUserByPasswordandUserName(LoginVModel.Password!,LoginVModel.UserName!);
            if (user != null)
            {
                if (user.IsActive)
                {
                    bool isAdmin = await _userService.CheckUserIsAdminAsync(user.Id);
                        var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName!),
                        new Claim("fullname", user.FullName),
                        new Claim("admin", isAdmin.ToString())

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = LoginVModel.RememberMe
                    };
                    await HttpContext.SignInAsync(principal, properties);
                    if (!string.IsNullOrEmpty(LoginVModel.RetUrl))
                    {
                        return Redirect(LoginVModel.RetUrl);
                    }
                    else
                    {
                        if (await _userService.CheckUserIsAdminAsync(user.Id))
                        {
                            return Redirect("/UsersPanel/Home/Index");
                        }
                        else
                        {
                            return Redirect("/MyAccount");
                        }
                    }
                   

                }
            }
            else
            {
                ModelState.AddModelError("LoginVModel.UserName", "کاربری شما در سایت غیرفعال است، به مدیر سایت اطلاع دهید !");
                return Page();
            }
            return Page();
        }
        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
