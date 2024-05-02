using BitPayDll;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Drawing;

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
        public async Task GoToPayment(string cartId,string BackUrl, string Currency, string siteloc)
        {
            try
            {
                Cart? cart = await _storeService.GetCartByIdAsync(cartId);
                //if (cart == null)
                //{
                //    return NotFound("سفارش مشخص نیست !");
                //}
                //if (cart.CheckOut)
                //{
                //    return NotFound("سفارش پرداخت شده است !");
                //}
                int Amnt = cart?.CartSum ?? 100; 
                if (Currency == "IRR")
                {
                    Amnt *= 10;
                }
                //حداقل 1000 ریال
                string Amount = Amnt.ToString();
                string FactorId = cart?.OrderNumber.ToString() ?? "xyz";
                string Name = cart?.BuyerName + " " + cart?.BuyerFamily;
                string Email = string.Empty;
                string Description = $"سفارش شماره {cart?.OrderNumber}";
                string testAPI = "adxcv-zzadq-polkjsad-opp13opoz-1sdf455aadzmck1244567";
                string Url = "https://bitpay.ir/payment-test/gateway-send";

                string Redirect = BackUrl;
                BitPay bitpay = new BitPay();

                int result = bitpay.Send(Url, testAPI, Amount, Redirect, FactorId, Name, Email, Description);

                if (result > 0)
                {
                    string go = string.Format("https://bitpay.ir/payment-test/gateway-{0}-get", result);
                    Response.Redirect(go);
                }
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
            }
            
            
        }
        public ActionResult Get(IDictionary<string, object> dictionary)
        {
            BitPay bitpay = new();

            string url = "https://bitpay.ir/payment-test/gateway-result-second";
            HttpClient httpClient = new();
            ;
            
            string api = "Your Api";
            string tid= HttpContext.Request.ToString();
            
            string trans_id = "5423";// HttpContext.Request["trans_id"];

            string id_get = "234";// HttpContext.Request["id_get"];

            string factorId = "321"; //HttpContext.Request["factorId"];

            int result = bitpay.Get(url, api, trans_id, id_get);

            if (result == 1)
            {
                //true
                TempData["msg"] = string.Format("پرداخت شما با شماره فاکتور {0} موفقیت انجام شد", factorId);
            }
            else
            {
                //false
                TempData["msg"] = "خطا در پرداخت";
            }
            return View();

        }
    }
}
