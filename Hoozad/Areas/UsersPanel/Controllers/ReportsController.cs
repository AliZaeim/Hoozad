using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("reports")]
    public class ReportsController : Controller
    {
        private readonly IStoreService _storeService;
        public ReportsController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        [PermissionCheckerByPermissionName("carts")]
        public async Task<IActionResult> GetCarts()
        {
            List<Cart> carts = await _storeService.GetCartsAsync();
            carts = carts.Where(w => !w.CheckOut).ToList();
            return View(carts);
        }
        [PermissionCheckerByPermissionName("crdet")]
        public async Task<IActionResult> CartDetails(Guid? id)
        {
            if (id == null || await _storeService.GetProductsAsync() == null)
            {
                return NotFound();
            }
            var cart = await _storeService.GetCartByIdAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }
        [PermissionCheckerByPermissionName("ordes")]
        public async Task<IActionResult> GetOrders()
        {
            List<Cart> carts = await _storeService.GetCartsAsOrders();
            return View(carts);
        }
        [PermissionCheckerByPermissionName("ordet")]
        public async Task<IActionResult> GetOrderDetails(Guid? id)
        {
            if (id == null) { return NotFound(); }
            Cart? cart = await _storeService.GetCartByIdAsync(id!.Value);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }
        public async Task<IActionResult> AddStatus(string cartId)
        {
            AddStatusVM addStatusVM = new()
            {
                CartId = cartId,
                UserName = User.Identity!.Name,                
                Statuses = await _storeService.GetStatusesAsync()
                
            };
            return PartialView(addStatusVM);
        }
        [HttpPost]

        public async Task<IActionResult> AddStatus(AddStatusVM addStatusVM)
        {
            if (!ModelState.IsValid)
            {
                addStatusVM.Statuses = await _storeService.GetStatusesAsync();
            }
            (bool res,string mess,string cartId) Res = await _storeService.AddStatusToOrderAsync(addStatusVM);
            return Json(new { Res.res, Res.mess,Res.cartId });
            
        }
        
        public async Task<IActionResult> ShowCartStatuses(string cartId)
        {
            Cart crt = await _storeService.GetCartByIdAsync(Guid.Parse(cartId));
            if (crt == null) { return NotFound(); } 
            return PartialView(await _storeService.GetCartStatusesByCartIdAsync(Guid.Parse(cartId)));
        }
        public async Task<IActionResult> GetCellphones()
        {
            return View(await _storeService.GetCellphoneBanksAsync());
        }
    }
}
