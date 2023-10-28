using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        public class RegisterVM
        {
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام")]
            public string? Name { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "نام خانوادگی")]
            public string? Family { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "تلفن همراه")]
            [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
            public string? Cellphone { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "کلمه عبور")]
            [MinLength(6, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد!")]
            public string? Password { get; set; }
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [Display(Name = "تکرار کلمه عبور")]
            [Compare("Password", ErrorMessage = "تایید کلمه عبور نادرست است !")]
            public string? RePassword { get; set; }
            [Display(Name = "نام کاربری")]
            //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [MinLength(6, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد!")]
            public string? UserName { get; set; }
            public string? ReturnUrl { get; set; }
        }
        [BindProperty]
        public RegisterVM RegisterViewModel { get; set; } = new();
        public SiteInfo SiteInfo { get; set; } = new();

        public async Task OnGet(string? RetUrl)
        {
            SiteInfo = await _userService.GetLastSiteInfoAsync();
            if (!string.IsNullOrEmpty(RetUrl))
            {

                RegisterViewModel.ReturnUrl = RetUrl;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (await _userService.ExistUserByCellphoneAsync(RegisterViewModel.Cellphone!))
            {
                ModelState.AddModelError("RegisterViewModel.Cellphone", "تلفن همراه قبلا ثبت شده است !");
                return Page();
            }
            if (await _userService.ExistUserByUserNameAsync(RegisterViewModel.UserName!))
            {
                ModelState.AddModelError("RegisterViewModel.UserName", "نام کاربری قبلا ثبت شده است !");
                return Page();
            }
            User user = new()
            {
                Name = RegisterViewModel.Name,
                Family = RegisterViewModel.Family,
                Cellphone = RegisterViewModel.Cellphone,
                Password = RegisterViewModel.Password,
                UserName = RegisterViewModel.UserName,
                IsActive = true,
                RegDate = DateTime.Now
            };
            if (string.IsNullOrEmpty(user.UserName))
            {
                user.UserName = RegisterViewModel.Cellphone;
            }
            UserRole userRole = new()
            {
                User = user,
                RoleId = 2,
                RegisterDate = DateTime.Now,
                IsActive = true
            };
            _userService.CreateUserRole(userRole);
            await _userService.SaveChangesAsync();
            if (string.IsNullOrEmpty(RegisterViewModel.ReturnUrl))
            {
                TempData["Success"] = "yes";
                return RedirectToPage("Register");
            }
            else
            {
                User userReg = await _userService.GetUserByPasswordandUserName(RegisterViewModel.Password!, RegisterViewModel.UserName!);
                if (userReg != null)
                {
                    if (userReg.IsActive)
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
                            IsPersistent = true
                        };
                        await HttpContext.SignInAsync(principal, properties);
                    }
                }
                return Redirect(RegisterViewModel.ReturnUrl);
            }

        }
    }
}
