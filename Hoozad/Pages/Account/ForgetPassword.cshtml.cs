using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Account
{
    public class ForgetPasswordModel : PageModel
    {
        private readonly IUserService _userService;
        public ForgetPasswordModel(IUserService userService)
        {
            _userService = userService;
        }
        public class FPModel
        {
            [Display(Name ="تلفن همراه")]
             [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [RegularExpression("^09[0|1|2|3][0-9]{8}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
            public string? Cellphone { get; set; }
        }
        [BindProperty]
        public FPModel? ForgotPasswordModel { get; set; } = new();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            User? user = await _userService.GetUserByCellphone(ForgotPasswordModel!.Cellphone!);
            if (user == null)
            {
                ModelState.AddModelError("ForgotPasswordModel.Cellphone", "کاربری با این تلفن همراه موجود نمی باشد !");
                return Page();
            }
            TempData["send"] = "yes";
            return Page();
        }
    }
}
