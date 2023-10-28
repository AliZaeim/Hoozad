using Core.DTOs.Admin;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly MyContext _context;
        public PermissionService(MyContext context)
        {
            _context = context;
        }
        public bool CheckPermissionByName(string PermissionName, string UserName)
        {
            try
            {
                User? user = _context.Users!.SingleOrDefault(_ => _.UserName == UserName);
                if (user == null)
                {
                    return false;
                }
                List<int?> rolesOfuser = _context.UserRoles!.Where(w => w.UserId == user.Id && w.IsActive).Select(x => x.RoleId).ToList(); 
                if (!rolesOfuser.Any())
                {
                    return false;
                }
                

                List<int?> RolesofPermission = _context.RolePermisisons.Include(x => x.Permission)
                                            .Where(w => w.Permission!.PermissionName == PermissionName)
                                            .Select(x => x.RoleId).ToList();
                return RolesofPermission.Any(rolesOfuser.Contains);
            }
            catch (Exception ex)
            {
                string message = ex.InnerException!.Message;
                return false;
            }
        }

        public bool ExitPermission(int id)
        {
            return _context.Permissions.ToList().Exists(_ => _.PermissionId == id);
        }

        public bool ExitRolePermission(int id)
        {
            return _context.RolePermisisons.ToList().Exists(_ => _.RP_Id == id);
        }

        public async Task<List<Permission>?> GetChildPermissionsOfPermission(int parentId)
        {
            List<Permission> permissions = await _context.Permissions.ToListAsync();
            Permission? permission = permissions.SingleOrDefault(x => x.PermissionId == parentId);
            if (permission != null)
            {
                return permission.Permissions!.ToList();
            }
            return null;
        }

        public async Task<Permission?> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions.Include(_ => _.Parent).Include(_ => _.RolePermissions).Include(_ => _.Permissions).SingleOrDefaultAsync(x => x.PermissionId == id);
        }

        public async Task<Permission?> GetPermissionByNameAsync(string name)
        {
            List<Permission> permissions = await _context.Permissions.Include(_ => _.Parent).Where(w => w.PermissionName == name).ToListAsync();
            Permission? permission = permissions.FirstOrDefault();
            return permission;
        }

        public async Task<List<Permission>> GetPermissionsAsync()
        {
            return  await _context.Permissions.Include(_ => _.Parent).ToListAsync();
        }

        public async Task<List<Permission>?> GetPermissionsOfRole(int? roleId)
        {
            if (roleId == null)
            {
                return null!;
            }
            List<RolePermisison> rolePermissions = await _context.RolePermisisons.Include(_ => _.Permission).ToListAsync();
            rolePermissions = rolePermissions.Where(w => w.RoleId == roleId.Value).ToList();
            return rolePermissions?.Select(x => x.Permission).ToList()!;
        }

        public async Task<RolePermisison?> GetRolePermissionByIdAsync(int id)
        {
            return await _context.RolePermisisons.Include(_ => _.Permission).SingleOrDefaultAsync(_ => _.PermissionId == id);
        }

        public async Task<List<RolePermisison>> GetRolePermissionsAsync()
        {
            return await _context.RolePermisisons.Include(_ => _.Permission).ToListAsync();
        }

        public async Task<List<RolePermisison>> GetRolePermissionsByRoleId(int roleId)
        {
            List<RolePermisison> rolePermissions = await _context.RolePermisisons.Include(_ => _.Permission).ToListAsync();
            rolePermissions = rolePermissions.Where(w => w.RoleId == roleId).ToList();
            return rolePermissions;
        }

        public void Insert(Permission permission)
        {
            _context.Permissions.Add(permission);
        }

        public void Insert(RolePermisison rolePermission)
        {
            _context.RolePermisisons.Add(rolePermission);
        }

        public void Remove(Permission permission)
        {
            _context.Permissions.Remove(permission);
        }

        public void Remove(RolePermisison rolePermission)
        {
            _context.RolePermisisons.Remove(rolePermission);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Permission permission)
        {
            _context.Permissions.Update(permission);
        }

        public void Update(RolePermisison rolePermission)
        {
            _context.RolePermisisons.Update(rolePermission);
        }

        public async Task<bool> UpdatePermisisonsOfRole(PermissionsOfRoleViewModel permissionsOfRoleViewModel)
        {
            List<RolePermisison> rolePermissions = await _context.RolePermisisons.Include(_ => _.Permission).ToListAsync();
            rolePermissions = rolePermissions.Where(w => w.RoleId == permissionsOfRoleViewModel.RoleId).ToList();
            List<int> rolePermissionsId = rolePermissions.Select(w => w.PermissionId!.GetValueOrDefault()).ToList();
            List<int> Sub = rolePermissionsId.ExceptBy(permissionsOfRoleViewModel.SelectedPermissionIds.ToList(), e => e).ToList();
            List<int> Add = permissionsOfRoleViewModel.SelectedPermissionIds.ExceptBy(rolePermissionsId.ToList(), e => e).ToList();
            bool save = false;
            foreach (var item in Sub.ToList())
            {
                RolePermisison? rolePermission = await _context.RolePermisisons.SingleOrDefaultAsync(x => x.PermissionId == item);
                if (rolePermission != null)
                {
                    save = true;
                    _context.RolePermisisons.Remove(rolePermission);

                }

            }
            foreach (var item in Add.ToList())
            {
                save = true;
                _context.RolePermisisons.Add(
                    new RolePermisison
                    {                        
                        RoleId = permissionsOfRoleViewModel.RoleId,
                        PermissionId = item
                    });

            }

            return save;
        }
    }
}
