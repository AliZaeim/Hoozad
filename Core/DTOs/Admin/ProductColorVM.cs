using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class ProductColorVM
    {
        public int Id { get; set; }
        [Display(Name = "رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Color { get; set; }
        [Display(Name = "نام رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? ColorName { get; set; }
        
        public Guid? ProductId { get; set; }
    }
}
