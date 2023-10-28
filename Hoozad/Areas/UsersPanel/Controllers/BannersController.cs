using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class BannersController : Controller
    {
        
        private readonly ISuppService _suppService;

        public BannersController(ISuppService suppService)
        {            
            _suppService = suppService;
        }

        // GET: UsersPanel/Banners
        public async Task<IActionResult> Index()
        {
              return View(await _suppService.GetBannersAsync());
        }

        // GET: UsersPanel/Banners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null ||  await _suppService.GetBannersAsync() == null)
            {
                return NotFound();
            }

            var banner = await _suppService.GetBannerById(id.Value);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // GET: UsersPanel/Banners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Banner banner)
        {
            if (ModelState.IsValid)
            {
               _suppService.CreateBanner(banner);
                await _suppService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        // GET: UsersPanel/Banners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _suppService.GetBannersAsync() == null)
            {
                return NotFound();
            }

            var banner = await _suppService.GetBannerById(id.Value);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: UsersPanel/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Banner banner)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _suppService.UpdateBanner(banner);
                    await _suppService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(banner.Id))
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
            return View(banner);
        }

        // GET: UsersPanel/Banners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _suppService.GetBannersAsync() == null)
            {
                return NotFound();
            }

            var banner = await _suppService.GetBannerById(id.Value);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // POST: UsersPanel/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _suppService.GetBannersAsync() == null)
            {
                return Problem("Entity set 'MyContext.Banners'  is null.");
            }
            var banner = await _suppService.GetBannerById(id);
            if (banner != null)
            {
                _suppService.DeleteBanner(banner);
                await _suppService.SaveChangesAsync();
            }           
            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(int id)
        {
          return _suppService.ExistBanner(id);
        }
    }
}
