using DataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Permissions
{
    public class RolePermisison
    {
        [Key]
        public int RP_Id { get; set; }
        [Required]
        public int? RoleId { get; set; }
        [Required]
        public int? PermissionId { get; set; }
        [Display(Name = "نقش")]
        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }
        [Display(Name = "دسترسی")]
        [ForeignKey(nameof(PermissionId))]
        public Permission? Permission { get; set; }
    }
}
