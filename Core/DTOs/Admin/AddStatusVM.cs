using DataLayer.Entities.Supplementary;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class AddStatusVM
    {
        [Display(Name ="وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? StatusId { get; set; }
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        [Display(Name ="توضیحات")]
        public string? Comment { get; set; }
        public string? CartId { get; set; }
        
        public string? UserName { get; set; }
        public List<Status> Statuses { get; set; } = new();

    }
}
