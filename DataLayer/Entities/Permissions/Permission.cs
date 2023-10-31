﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Permissions
{
    public class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermisison>();
            Permissions = new HashSet<Permission>();
        }
        [Key]
        public int PermissionId { get; set; }
        [Display(Name = "عنوان دسترسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? PermissionTitle { get; set; }
        [Display(Name = "نام دسترسی به انگلیسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? PermissionName { get; set; }
        [Display(Name = "والد")]
        public int? ParentId { get; set; }
        #region Relations
        [ForeignKey(nameof(ParentId))]
        public Permission? Parent { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<RolePermisison> RolePermissions { get; set; }
        #endregion
    }
}
