using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class ProductPrice
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سایز")]
        public int? Size { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "قد")]
        public int? Height { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "قیمت")]
        public int? Price { get; set; }
        
        public Guid? ProductId { get; set; }
        #region Relations
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
        #endregion
    }
}
