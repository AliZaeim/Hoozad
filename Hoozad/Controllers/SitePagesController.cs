using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Web.Controllers
{
    public class SitePagesController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _contextAccessor;
        public SitePagesController(IStoreService storeService, IHttpContextAccessor contextAccessor)
        {
            _storeService = storeService;
            _contextAccessor = contextAccessor;

        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("GoToPayment")]
        public async Task<IActionResult> GoToPayment(string cartId,string BackUrl, string Currency, string siteloc)
        {
            Cart? cart = await _storeService.GetCartByIdAsync(cartId);
            if (cart == null)
            {
                return NotFound("سفارش مشخص نیست !");
            }
            if (cart.CheckOut)
            {
                return NotFound("سفارش پرداخت شده است !");
            }
            (bool IsSuccess, string Content) = _storeService.GetNextPayToken(cart.CartSum, cart.OrderNumber!, cart.BuyerCellphone!, BackUrl, Currency);
            if (IsSuccess)
            {
               
                string json = Content;
                dynamic data = JObject.Parse(json);
                string tid = data["trans_id"];
                string eUrl = "https://nextpay.org/nx/gateway/payment/" + tid;
                return Redirect(eUrl);
                //Core.Utility.CookieExtensions.SetHttpContextAccessor(_contextAccessor);
                //if (Core.Utility.CookieExtensions.ExistCookie("crt"))
                //{
                //    Core.Utility.CookieExtensions.RemoveCookie("crt");
                //}
            }
            else
            {
                string json = Content;
                dynamic data = JObject.Parse(json);
                string resCode = data["code"];
                ViewData["code"] = resCode;
                return View();
            }
        }
    }
}
