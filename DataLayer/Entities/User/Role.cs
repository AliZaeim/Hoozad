using DataLayer.Entities.Permissions;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.User
{
    public class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RolePermisisons = new HashSet<RolePermisison>();
        }
       
        [Key]
        public int RoleId { get; set; }
        [Display(Name = "نام نقش")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? RoleName { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? RoleTitle { get; set; }
        
        [Display(Name ="وضعیت حذف")]
        public bool IsDeleted { get; set; }
        
        #region Relations
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RolePermisison>  RolePermisisons { get; set; }

        #endregion
    }
}
