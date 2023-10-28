using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    /// <summary>
    /// اجزای محصول
    /// </summary>
    public class ProductComponent
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid? ProductId { get; set; }
        [Display(Name = "جزء")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? ProductCode { get; set; }
        public DateTime? RegDate { get; set; }
        #region Relations
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
        
        #endregion
    }
}
