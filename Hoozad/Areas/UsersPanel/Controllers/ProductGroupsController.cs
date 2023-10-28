using Core.DTOs.General;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class ProductGroupsController : Controller
    {
        private readonly IStoreService _storeService;
        public ProductGroupsController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: ProductGroupController
        public async Task<ActionResult> Index()
        {
            return View(await _storeService.GetProductGroupsAsync());
        }

        // GET: ProductGroupController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _storeService.GetProductGroupAsync(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }
        public async Task<bool> ChangeStatusGroup(int id, int status)
        {
            ProductGroup? productGroup = await _storeService.GetProductGroupAsync(id);
            if (productGroup == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            productGroup.IsActive = st;
            _storeService.UpdateProductGroup(productGroup);
            await _storeService.SaveChangesAsync();
            return st;

        }
        // GET: ProductGroupController/Create
        public async Task<ActionResult> Create()
        {
            ViewData["ParentId"] = new SelectList(await _storeService.GetProductGroupsAsync(), "Id", "EnTitle");
            return View();
        }

        // POST: ProductGroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductGroup productGroup, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "تصویر گروه را انتخاب کنید !");
                    return View(productGroup);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                FileValidation fileValidation1 = await Image.UploadedImageValidation(50, 400, 400, ex)!;

                if (!fileValidation1.IsValid)
                {
                    ModelState.AddModelError("Image", fileValidation1.Message!);
                    return View(productGroup);
                }
                productGroup.Image = Image.SaveUploadedImage("wwwroot/images/pgroups", true);
                _storeService.CreateProductGroup(productGroup);
                await _storeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(await _storeService.GetProductGroupsAsync(), "Id", "Title", productGroup.ParentId);
            return View(productGroup);
        }

        // GET: ProductGroupController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _storeService.GetProductGroupAsync(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }
            List<ProductGroup> productGroups = await _storeService.GetProductGroupsAsync();
            productGroups = productGroups.Where(w => w.Id != productGroup.Id).ToList();
            ViewData["ParentId"] = new SelectList(productGroups, "Id", "Title", productGroup.ParentId);
            return View(productGroup);
        }

        // POST: ProductGroupController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductGroup productGroup, IFormFile? Image)
        {
            if (id != productGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image == null && string.IsNullOrEmpty(productGroup.Image))
                    {
                        ModelState.AddModelError("Image", "تصویر اسلایدر رو انتخاب کنید !");
                        return View(productGroup);
                    }
                    if (Image != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                        FileValidation fileValidation1 = await Image.UploadedImageValidation(50, 400, 400, ex)!;

                        if (!fileValidation1.IsValid)
                        {
                            ModelState.AddModelError("Image", fileValidation1.Message!);
                            return View(productGroup);
                        }
                        productGroup.Image = Image.SaveUploadedImage("wwwroot/images/pgroups", true);
                    }

                    _storeService.UpdateProductGroup(productGroup);
                    await _storeService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGroupExists(productGroup.Id))
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
            List<ProductGroup> productGroups = await _storeService.GetProductGroupsAsync();
            productGroups = productGroups.Where(w => w.Id != productGroup.Id).ToList();
            ViewData["ParentId"] = new SelectList(productGroups, "Id", "Title", productGroup.ParentId);
            return View(productGroup);
        }

        // GET: ProductGroupController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productGroup = await _storeService.GetProductGroupAsync(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // POST: UsersPanel/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var productGroup = await _storeService.GetProductGroupAsync(id);
            if (productGroup != null)
            {
                _storeService.DeleteProductGroup(productGroup);
                await _storeService.SaveChangesAsync();
            }            
            return RedirectToAction(nameof(Index));
        }
        private bool ProductGroupExists(int id)
        {
            return _storeService.ExistProductGroup(id);
        }
    }
}
