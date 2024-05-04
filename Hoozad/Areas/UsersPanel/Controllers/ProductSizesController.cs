using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("prsizes")]
    public class ProductSizesController : Controller
    {
        
        private readonly IStoreService _storeService;

        public ProductSizesController(IStoreService storeService)
        {            
            _storeService = storeService;
        }

        // GET: UsersPanel/ProductSizes
        public async Task<IActionResult> Index()
        {
            var myContext =await _storeService.GetProductSizesAsync();
            return View(myContext);
        }

        // GET: UsersPanel/ProductSizes/Details/5
        [PermissionCheckerByPermissionName("prsdet")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _storeService.GetProductSizesAsync() == null)
            {
                return NotFound();
            }

            var productSize = await _storeService.GetProductSizeByIdAsync(id.Value);
            if (productSize == null)
            {
                return NotFound();
            }

            return View(productSize);
        }

        // GET: UsersPanel/ProductSizes/Create
        [PermissionCheckerByPermissionName("prsadd")]
        public async Task<IActionResult> Create()
        {
            ViewData["ProductId"] = new SelectList(await _storeService.GetProductsAsync(), "ProductId", "ProductName");
            return View();
        }

        // POST: UsersPanel/ProductSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("prsadd")]
        public async Task<IActionResult> Create(ProductSize productSize)
        {
            if (ModelState.IsValid)
            {
               _storeService.CreateProductSize(productSize);
                await _storeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(await _storeService.GetProductsAsync(), "ProductId", "ProductName", productSize.ProductId);
            return View(productSize);
        }

        // GET: UsersPanel/ProductSizes/Edit/5
        [PermissionCheckerByPermissionName("prsedit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _storeService.GetProductSizesAsync() == null)
            {
                return NotFound();
            }

            var productSize = await _storeService.GetProductSizeByIdAsync(id.Value);
            if (productSize == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(await _storeService.GetProductsAsync(), "ProductId", "ProductName", productSize.ProductId);
            return View(productSize);
        }

        // POST: UsersPanel/ProductSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("prsedit")]
        public async Task<IActionResult> Edit(int id, ProductSize productSize)
        {
            if (id != productSize.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _storeService.UpdateProductSize(productSize);
                    await _storeService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSizeExists(productSize.Id))
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
            ViewData["ProductId"] = new SelectList(await _storeService!.GetProductsAsync(), "ProductId", "ProductName", productSize.ProductId);
            return View(productSize);
        }

        // GET: UsersPanel/ProductSizes/Delete/5
        [PermissionCheckerByPermissionName("prsdel")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _storeService!.GetProductSizesAsync() == null)
            {
                return NotFound();
            }

            var productSize = await _storeService.GetProductSizeByIdAsync(id.Value);
            if (productSize == null)
            {
                return NotFound();
            }

            return View(productSize);
        }

        // POST: UsersPanel/ProductSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("prsdel")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _storeService.GetProductSizesAsync()== null)
            {
                return Problem("Entity set 'MyContext.ProductSizes'  is null.");
            }
            var productSize = await _storeService.GetProductSizeByIdAsync(id);
            if (productSize != null)
            {
                _storeService.DeleteProductSize(productSize);
                await _storeService.SaveChangesAsync();
            }            
           
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSizeExists(int id)
        {
            ProductSize productSize = _storeService.GetProductSizeByIdAsync(id).Result;
            return productSize != null;
        }
    }
}
