using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("orstatus")]
    public class StatusController : Controller
    {
        private readonly IGenericService<Status> _Statusservice;
        public StatusController(IGenericService<Status> StatusService)
        {
            _Statusservice = StatusService;
        }

        // GET: UsersPanel/Status
        public async Task<IActionResult> Index()
        {
            return View(await _Statusservice.GetAllAsync());
        }

        // GET: UsersPanel/Status/Details/5
        [PermissionCheckerByPermissionName("orsadd")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _Statusservice.GetAllAsync() == null)
            {
                return NotFound();
            }

            var status = await _Statusservice.GetByIdAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: UsersPanel/Status/Create
        [PermissionCheckerByPermissionName("orsadd")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("orsadd")]
        public async Task<IActionResult> Create(Status status)
        {
            if (ModelState.IsValid)
            {
                _Statusservice.Create(status);
                await _Statusservice.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: UsersPanel/Status/Edit/5
        [PermissionCheckerByPermissionName("orsedit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _Statusservice.GetAllAsync() == null)
            {
                return NotFound();
            }

            var status = await _Statusservice.GetByIdAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: UsersPanel/Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("orsedit")]
        public async Task<IActionResult> Edit(int id, Status status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Statusservice.Edit(status);
                    await _Statusservice.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.Id))
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
            return View(status);
        }

        // GET: UsersPanel/Status/Delete/5
        [PermissionCheckerByPermissionName("orsdel")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _Statusservice.GetAllAsync() == null)
            {
                return NotFound();
            }

            var status = await _Statusservice.GetByIdAsync(id.Value);

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: UsersPanel/Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("orsdel")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _Statusservice.GetAllAsync() == null)
            {
                return Problem("Entity set 'MyContext.Statuses'  is null.");
            }
            var status = await _Statusservice.GetByIdAsync(id);
            if (status != null)
            {
                _Statusservice.Delete(status);
                await _Statusservice.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(int id)
        {
            return _Statusservice.ExistEntity(id);
        }
    }
}
