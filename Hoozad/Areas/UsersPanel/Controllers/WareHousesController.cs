using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    public class WareHousesController : Controller
    {
        
        private readonly IStoreService _storeService;
        public WareHousesController( IStoreService storeService)        
        {           
            _storeService = storeService;
        }

        // GET: UsersPanel/WareHouses
        public async Task<IActionResult> Index()
        {             
            List<WareHouse> wareHouses = await _storeService.GetWareHousesAsync();            
            return View(wareHouses.ToList());
        }
        public async Task<JsonResult> GetProductGroups()
        {
            List<ProductGroup> productGroups = await _storeService.GetProductGroupsAsync();
            return Json(productGroups.Select(x => new {id = x.Id, name = x.Title}));
        }
        public async Task<JsonResult> GetGroupProducts(int gId)
        {
            ProductGroup productGroup = await _storeService.GetProductGroupAsync(gId);
            var products = productGroup.Products;
            return Json(products.Select(x => new { id = x.ProductGroupId, name = x.ProductName }));
        }
        //public async Task<JsonResult> GetProductItems(int pId)
        //{
        //    Product product = await _storeService.GetproductBy(pId);            
        //    List<ProductItem> productItems = product.ProductItems.ToList();
        //    return Json(productItems.Select(x => new { id = x.Id, name = x.Name }));
        //}

        // GET: UsersPanel/WareHouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _storeService.GetWareHousesAsync() == null)
            {
                return NotFound();
            }

            var wareHouse = await _storeService.GetWareHouseByIdAsync(id.GetValueOrDefault());
            if (wareHouse == null)
            {
                return NotFound();
            }

            return View(wareHouse);
        }

        // GET: UsersPanel/WareHouses/Create
        public async Task<IActionResult> Create()
        {
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.ProductComponents!.Count()==0).ToList();
            //products = products.Where(w => w.IsActive).ToList();
            ViewData["ProductId"] = new SelectList(products.ToList(), "ProductId", "ProductName");
            return View();
        }

        // POST: UsersPanel/WareHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                wareHouse.RegDate = DateTime.Now;
                _storeService.CreateWareHouse(wareHouse);
                await _storeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.ProductComponents!.Count() == 0).ToList();
            ViewData["ProductId"] = new SelectList(products.ToList(), "ProductId", "ProductName",wareHouse.ProductId);
            return View(wareHouse);
        }

        // GET: UsersPanel/WareHouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _storeService.GetProductsAsync() == null)
            {
                return NotFound();
            }

            var wareHouse = await _storeService.GetWareHouseByIdAsync(id.GetValueOrDefault());
            if (wareHouse == null)
            {
                return NotFound();
            }
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.ProductComponents!.Count() == 0).ToList();
            ViewData["ProductId"] = new SelectList(products.ToList(), "ProductId", "ProductName",wareHouse.ProductId);
            return View(wareHouse);
        }

        // POST: UsersPanel/WareHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WareHouse wareHouse)
        {
            if (id != wareHouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (wareHouse.RegDate == null)
                    {
                        wareHouse.RegDate = DateTime.Now;
                    }
                    
                    _storeService.UpdateWareHouse(wareHouse);
                    await _storeService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WareHouseExists(wareHouse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            List<Product> products = await _storeService.GetProductsAsync();
            products = products.Where(w => w.ProductComponents!.Count() == 0).ToList();
            ViewData["ProductId"] = new SelectList(products.ToList(), "ProductId", "ProductName", wareHouse.ProductId);
            return View(wareHouse);
        }

        // GET: UsersPanel/WareHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _storeService.GetWareHousesAsync() == null)
            {
                return NotFound();
            }
            var wareHouse = await _storeService.GetWareHouseByIdAsync(id.Value);
            if (wareHouse == null)
            {
                return NotFound();
            }
            return View(wareHouse);
        }

        // POST: UsersPanel/WareHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _storeService.GetWareHousesAsync() == null)
            {
                return Problem("Entity set 'MyContext.WareHouses'  is null.");
            }
            var wareHouse = await _storeService.GetWareHouseByIdAsync(id);
            if (wareHouse != null)
            {
                _storeService.DeleteWareHouse(wareHouse);
            }
            
            await _storeService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WareHouseExists(int id)
        {
          return _storeService.ExistWareHouse(id);
        }
    }
}
