using Core.DTOs.Admin;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("roles")]
    public class RolesController : Controller
    {
        
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public RolesController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
            
        }
        //[PermissionCheckerByPermissionName("rolmanage")]
        public async Task<IActionResult> PermissionsOfRole(int? roleId)
        {
            if (roleId == null)
            {
                return NotFound();
            }
            Role? role = await _userService.GetRoleAsync(roleId.Value);

            if (role == null)
            {
                return NotFound();
            }
            PermissionsOfRoleViewModel permissionsOfRoleViewModel = new();
            List<RolePermisison> rolePermissions = await _permissionService.GetRolePermissionsByRoleId(roleId.Value);
            permissionsOfRoleViewModel.Permissions = await _permissionService.GetPermissionsAsync();
            permissionsOfRoleViewModel.RoleId = roleId.Value;
            permissionsOfRoleViewModel.Role = role;
            permissionsOfRoleViewModel.RolePermissions = rolePermissions.ToList();
            permissionsOfRoleViewModel.SelectedPermissionIds = rolePermissions.Select(x => x.PermissionId!.Value).ToList();
            permissionsOfRoleViewModel.UserName = User.Identity!.Name;
            return View(permissionsOfRoleViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[PermissionCheckerByPermissionName("rolmanage")]
        public async Task<IActionResult> PermissionsOfRole(PermissionsOfRoleViewModel permissionsOfRoleViewModel)
        {


            if (!ModelState.IsValid)
            {
                permissionsOfRoleViewModel.Permissions = await _permissionService.GetPermissionsAsync();
                permissionsOfRoleViewModel.RolePermissions = await _permissionService.GetRolePermissionsByRoleId(permissionsOfRoleViewModel.RoleId);
                permissionsOfRoleViewModel.Role = await _userService.GetRoleAsync(permissionsOfRoleViewModel.RoleId);
                return View(permissionsOfRoleViewModel);
            }
            permissionsOfRoleViewModel.UserName = User.Identity!.Name;
            bool save = await _permissionService.UpdatePermisisonsOfRole(permissionsOfRoleViewModel);
            if (save)
            {
                await _permissionService.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
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
