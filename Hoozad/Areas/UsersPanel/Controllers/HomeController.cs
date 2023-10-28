using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
