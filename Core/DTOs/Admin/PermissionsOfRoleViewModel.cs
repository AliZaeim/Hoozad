using DataLayer.Entities.Permissions;
using DataLayer.Entities.User;

namespace Core.DTOs.Admin
{
    public class PermissionsOfRoleViewModel
    {
        public int RoleId { get; set; }
        public List<int> SelectedPermissionIds { get; set; } = new();
        public List<Permission> Permissions { get; set; } = new();
        public List<RolePermisison> RolePermissions { get; set; } = new();
        public Role? Role { get; set; }
        public string? UserName { get; set; }
    }
}
