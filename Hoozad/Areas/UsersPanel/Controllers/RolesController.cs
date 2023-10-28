using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class RolesController : Controller
    {
        
        private readonly IUserService _userService;
        public RolesController(IUserService userService)
        {
            _userService = userService;
            
        }

        // GET: UsersPanel/Roles
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetRolesAsync());
        }

        // GET: UsersPanel/Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _userService.GetRolesAsync() == null)
            {
                return NotFound();
            }

            var role = await _userService.GetRoleAsync(id.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: UsersPanel/Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _userService.CreateRole(role);
                await _userService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: UsersPanel/Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _userService.GetRolesAsync() == null)
            {
                return NotFound();
            }

            var role = await _userService.GetRoleAsync(id.Value);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: UsersPanel/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.UpdateRole(role);
                    await _userService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleId))
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
            return View(role);
        }

        // GET: UsersPanel/Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _userService.GetRolesAsync() == null)
            {
                return NotFound();
            }

            var role = await _userService.GetRoleAsync(id.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: UsersPanel/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _userService.GetRolesAsync() == null)
            {
                return Problem("Entity set 'MyContext.Roles'  is null.");
            }
            var role = await _userService.GetRoleAsync(id);
            if (role != null)
            {
                _userService.DeleteRole(role);
                await _userService.SaveChangesAsync();
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
          return _userService.ExistRole(id);
        }
    }
}
