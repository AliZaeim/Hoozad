using DataLayer.Entities.Store;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class ProductComponentVM
    {
        public Guid? ProductId { get; set; }
        public Product? BaseProduct { get; set; }
        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public string? ProductCode { get; set; }
        public List<Product>? Products { get; set; }
    }
}
