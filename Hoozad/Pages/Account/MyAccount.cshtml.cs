using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account
{
    public class MyAccountModel : PageModel
    {
        private readonly IUserService _userService;
        public MyAccountModel(IUserService userService)
        {
            _userService = userService;
        }
        public class AccountViewModel
        {
            public string? Name { get; set; }
            public string? Family { get; set; }
            public string? Avatar { get; set; }
        }
        public AccountViewModel AccountVM{ get; set; } = new();
        public async Task OnGet()
        {
            
            if (User.Identity!.IsAuthenticated)
            {
                User user = await _userService.GetUserByUsernameAsync(User.Identity!.Name?? "admin");
                if (user != null) {
                    AccountVM.Name = user.Name;
                    AccountVM.Family = user.Family;
                    AccountVM.Avatar = "/Images/Avatar.png";
                }

            }
        }
    }
}
