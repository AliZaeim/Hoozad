using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("perms")]
    public class PermissionsController : Controller
    {
        
        private readonly IPermissionService _permissionService;
        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
            
        }

        // GET: UsersPanel/Permissions
        public async Task<IActionResult> Index()
        {
            
            return View(await _permissionService.GetPermissionsAsync());
        }

        // GET: UsersPanel/Permissions/Details/5
        [PermissionCheckerByPermissionName("perdet")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _permissionService.GetPermissionsAsync() == null)
            {
                return NotFound();
            }

            var permission = await _permissionService.GetPermissionByIdAsync(id.Value);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // GET: UsersPanel/Permissions/Create
        [PermissionCheckerByPermissionName("peradd")]
        public async Task<IActionResult> Create()
        {
            List<Permission> permissions = await _permissionService.GetPermissionsAsync();
            
            ViewData["ParentId"] = new SelectList(permissions, "PermissionId", "PermissionName");
            return View();
        }

        // POST: UsersPanel/Permissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("peradd")]
        public async Task<IActionResult> Create( Permission permission)
        {
            if (ModelState.IsValid)
            {
                _permissionService.Insert(permission);
                await _permissionService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<Permission> permissions = await _permissionService.GetPermissionsAsync();

            ViewData["ParentId"] = new SelectList(permissions, "PermissionId", "PermissionName",permission.ParentId);
            return View(permission);
        }

        // GET: UsersPanel/Permissions/Edit/5
        [PermissionCheckerByPermissionName("peredit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _permissionService.GetPermissionsAsync() == null)
            {
                return NotFound();
            }

            var permission = await _permissionService.GetPermissionByIdAsync(id.Value);
            if (permission == null)
            {
                return NotFound();
            }
            List<Permission> permissions = await _permissionService.GetPermissionsAsync();

            ViewData["ParentId"] = new SelectList(permissions, "PermissionId", "PermissionName", permission.ParentId);
            return View(permission);
        }

        // POST: UsersPanel/Permissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("peredit")]
        public async Task<IActionResult> Edit(int id, Permission permission)
        {
            if (id != permission.PermissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _permissionService.Update(permission);
                    await _permissionService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionExists(permission.PermissionId))
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
            List<Permission> permissions = await _permissionService.GetPermissionsAsync();

            ViewData["ParentId"] = new SelectList(permissions, "PermissionId", "PermissionName",permission.ParentId);
            return View(permission);
        }

        // GET: UsersPanel/Permissions/Delete/5
        [PermissionCheckerByPermissionName("perdel")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _permissionService.GetPermissionsAsync() == null)
            {
                return NotFound();
            }

            var permission = await _permissionService.GetPermissionByIdAsync(id.Value);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // POST: UsersPanel/Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("perdel")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _permissionService.GetPermissionsAsync() == null)
            {
                return Problem("Entity set 'MyContext.Permissions'  is null.");
            }
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission != null)
            {
               _permissionService.Remove(permission);
                await _permissionService.SaveChangesAsync();
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool PermissionExists(int id)
        {
          return _permissionService.ExitPermission(id);
        }
    }
}
