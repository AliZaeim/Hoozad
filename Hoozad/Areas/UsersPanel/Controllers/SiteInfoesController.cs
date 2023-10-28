using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class SiteInfoesController : Controller
    {

        private readonly IStoreService _siteInfoService;
        public SiteInfoesController(IStoreService siteInfoService)
        {
            _siteInfoService = siteInfoService;
        }

        // GET: UsersPanel/SiteInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _siteInfoService.GetSiteInfosAsync());
        }

        // GET: UsersPanel/SiteInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _siteInfoService.GetSiteInfosAsync() == null)
            {
                return NotFound();
            }

            var siteInfo = await _siteInfoService.GetSiteInfoAsync(id.GetValueOrDefault());
            if (siteInfo == null)
            {
                return NotFound();
            }

            return View(siteInfo);
        }

        // GET: UsersPanel/SiteInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/SiteInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SiteInfo siteInfo)
        {
            if (ModelState.IsValid)
            {
                siteInfo.RegDate = DateTime.Now;
                _siteInfoService.CreateSiteInfo(siteInfo);
                await _siteInfoService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteInfo);
        }

        // GET: UsersPanel/SiteInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _siteInfoService.GetSiteInfosAsync() == null)
            {
                return NotFound();
            }

            var siteInfo = await _siteInfoService.GetSiteInfoAsync(id.Value);
            if (siteInfo == null)
            {
                return NotFound();
            }
            return View(siteInfo);
        }

        // POST: UsersPanel/SiteInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SiteInfo siteInfo)
        {
            if (id != siteInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _siteInfoService.UpdateSiteInfo(siteInfo);
                    await _siteInfoService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteInfoExists(siteInfo.Id))
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
            return View(siteInfo);
        }

        // GET: UsersPanel/SiteInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _siteInfoService.GetSiteInfosAsync() == null)
            {
                return NotFound();
            }

            var siteInfo = await _siteInfoService.GetSiteInfoAsync(id.Value);
            if (siteInfo == null)
            {
                return NotFound();
            }

            return View(siteInfo);
        }

        // POST: UsersPanel/SiteInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _siteInfoService.GetSiteInfosAsync() == null)
            {
                return Problem("Entity set 'MyContext.SiteInfos'  is null.");
            }
            var siteInfo = await _siteInfoService.GetSiteInfoAsync(id);
            if (siteInfo != null)
            {
                _siteInfoService.DeleteSiteInfo(siteInfo);
                await _siteInfoService.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SiteInfoExists(int id)
        {
            return _siteInfoService.ExistSiteInfo(id);
        }
    }
}
