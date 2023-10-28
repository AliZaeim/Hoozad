using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class ProductPriceViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سایز")]
        public int? Size { get; set; }
        [Display(Name = "قد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Height { get; set; }       

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Price { get; set; }       
        public Guid? ProductId { get; set; }
        public List<int?> Sizes { get; set; } = new();
    }
}
