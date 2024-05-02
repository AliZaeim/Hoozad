using Core.DTOs.General;
using Core.Security;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    //[PermissionCheckerByPermissionName("banneritems")]
    public class BannerItemsController : Controller
    {
       
        private readonly ISuppService _suppService;

        public BannerItemsController(ISuppService suppService)
        {          
            _suppService = suppService;
        }

        // GET: UsersPanel/BannerItems
        public async Task<IActionResult> Index(int? bannerId)
        {  
            if (bannerId == null)
            {
                ViewData["zTitle"] = "تمام بنرهای ثبت شده";
                return View(await _suppService.GetBannerItemsAsync());                
            }
            else
            {
                List<BannerItem> bannerItems = await _suppService.GetBannerItemsAsync();
                bannerItems = bannerItems.Where(w => w.BannerId == bannerId.Value).ToList();
                Banner banner = await _suppService.GetBannerById(bannerId.Value);
                ViewData["zTitle"] = "بنرهای ثبت شده" + " بسته " + banner?.Name;
                return View(bannerItems.ToList());
            }
            
        }

        // GET: UsersPanel/BannerItems/Details/5
        [PermissionCheckerByPermissionName("bidet")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _suppService.GetBannerItemsAsync() == null)
            {
                return NotFound();
            }

            var bannerItem = await _suppService.GetBannerItemByIdAsync(id.Value);
            if (bannerItem == null)
            {
                return NotFound();
            }

            return View(bannerItem);
        }

        // GET: UsersPanel/BannerItems/Create
        [PermissionCheckerByPermissionName("biadd")]
        public async Task<IActionResult> Create(int? bannerId)
        {
            if (bannerId == null)
            {
                ViewData["BannerId"] = new SelectList(await _suppService.GetBannersAsync(), "Id", "Name");
            }
            else
            {
                ViewData["BId"] = bannerId;
                ViewData["BannerId"] = new SelectList(await _suppService.GetBannersAsync(), "Id", "Name",bannerId.Value);
            }
            
            return View();
        }

        // POST: UsersPanel/BannerItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[PermissionCheckerByPermissionName("biadd")]
        public async Task<IActionResult> Create(BannerItem bannerItem, IFormFile? Image, IFormFile? MobileImage)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "لطفا تصویر را انتخاب کنید !");
                    return View(bannerItem);
                }
                if (MobileImage == null)
                {
                    ModelState.AddModelError("MobileImage", "لطفا تصویر را انتخاب کنید !");
                    return View(bannerItem);
                }
                if (Image != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                    FileValidation fileValidation1 = await Image.UploadedImageValidation(200, ex)!;

                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("Image", fileValidation1.Message!);
                        return View(bannerItem);
                    }
                    bannerItem.Image = Image.SaveUploadedImage("wwwroot/images/banners", true);
                }
                if (MobileImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                    FileValidation fileValidation1 = await MobileImage.UploadedImageValidation(50, ex)!;

                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("MobileImage", fileValidation1.Message!);
                        return View(bannerItem);
                    }
                    bannerItem.MobileImage = MobileImage.SaveUploadedImage("wwwroot/images/banners", true);
                }
                _suppService.CreateBannerItem(bannerItem);
                await _suppService.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {bannerId =bannerItem.BannerId});
            }
            ViewData["BannerId"] = new SelectList(await _suppService.GetBannersAsync(), "Id", "Name", bannerItem.BannerId);
            return View(bannerItem);
        }

        // GET: UsersPanel/BannerItems/Edit/5
        //[PermissionCheckerByPermissionName("biadd")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _suppService.GetBannerItemsAsync() == null)
            {
                return NotFound();
            }

            var bannerItem = await _suppService.GetBannerItemByIdAsync(id.Value);
            if (bannerItem == null)
            {
                return NotFound();
            }
            ViewData["BannerId"] = new SelectList(await _suppService.GetBannersAsync(), "Id", "Name", bannerItem.BannerId);
            return View(bannerItem);
        }

        // POST: UsersPanel/BannerItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[PermissionCheckerByPermissionName("biedit")]
        public async Task<IActionResult> Edit(int id, BannerItem bannerItem, IFormFile? Image, IFormFile? MobileImage)
        {
            if (id != bannerItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                        FileValidation fileValidation1 = await Image.UploadedImageValidation(200, ex)!;

                        if (!fileValidation1.IsValid)
                        {
                            ModelState.AddModelError("Image", fileValidation1.Message!);
                            return View(bannerItem);
                        }
                        bannerItem.Image = Image.SaveUploadedImage("wwwroot/images/banners", true);
                    }
                    if (MobileImage != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                        FileValidation fileValidation1 = await MobileImage.UploadedImageValidation(200, ex)!;

                        if (!fileValidation1.IsValid)
                        {
                            ModelState.AddModelError("MobileImage", fileValidation1.Message!);
                            return View(bannerItem);
                        }
                        bannerItem.MobileImage = MobileImage.SaveUploadedImage("wwwroot/images/banners", true);
                    }
                    _suppService.UpdateBannerItem(bannerItem);
                    await _suppService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerItemExists(bannerItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { bannerId = bannerItem.BannerId });
            }
            ViewData["BannerId"] = new SelectList(await _suppService.GetBannersAsync(), "Id", "Name", bannerItem.BannerId);
            return View(bannerItem);
        }

        // GET: UsersPanel/BannerItems/Delete/5
        //[PermissionCheckerByPermissionName("bidel")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _suppService.GetBannerItemsAsync() == null)
            {
                return NotFound();
            }

            var bannerItem = await _suppService.GetBannerItemByIdAsync(id.Value);
            if (bannerItem == null)
            {
                return NotFound();
            }

            return View(bannerItem);
        }

        // POST: UsersPanel/BannerItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[PermissionCheckerByPermissionName("bidel")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _suppService.GetBannerItemsAsync() == null)
            {
                return Problem("Entity set 'MyContext.BannerItems'  is null.");
            }
            var bannerItem = await _suppService.GetBannerItemByIdAsync(id);
            int? bnnerId = bannerItem.BannerId;
            if (bannerItem != null)
            {
                _suppService.DeleteBannerItem(bannerItem);
                await _suppService.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { bannerId = bnnerId });
        }

        private bool BannerItemExists(int id)
        {
          return _suppService.ExistBannerItem(id);
        }
    }
}
