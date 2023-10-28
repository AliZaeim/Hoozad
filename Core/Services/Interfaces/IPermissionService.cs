using Core.DTOs.Admin;
using DataLayer.Entities.Permissions;

namespace Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region General
        void SaveChanges();
        Task SaveChangesAsync();
        #endregion General
        #region Permisison
        Task<List<Permission>> GetPermissionsAsync();
        Task<Permission?> GetPermissionByIdAsync(int id);
        Task<Permission?> GetPermissionByNameAsync(string name);
        void Insert(Permission permission);
        void Update(Permission permission);
        void Remove(Permission permission);
        bool ExitPermission(int id);
        bool CheckPermissionByName(string PermissionName, string UserName);
        Task<List<Permission>?> GetChildPermissionsOfPermission(int parentId);
        #endregion Permission
        #region RolePermission
        Task<List<RolePermisison>> GetRolePermissionsAsync();
        Task<RolePermisison?> GetRolePermissionByIdAsync(int id);
        Task<List<RolePermisison>> GetRolePermissionsByRoleId(int roleId);
        void Insert(RolePermisison rolePermission);
        void Update(RolePermisison rolePermission);
        void Remove(RolePermisison rolePermission);
        bool ExitRolePermission(int id);
        Task<List<Permission>?> GetPermissionsOfRole(int? roleId);
        Task<bool> UpdatePermisisonsOfRole(PermissionsOfRoleViewModel permissionsOfRoleViewModel);
        #endregion RolePermission
    }
}
