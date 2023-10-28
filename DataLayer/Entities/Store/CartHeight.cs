using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class CartHeight
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "قد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? Height { get; set; }
        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Quantity { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? Regdate { get; set; }
        public int CartItemId { get; set; }
        #region Relations
        [ForeignKey(nameof(CartItemId))]
        public CartItem? CartItem { get; set; }
        #endregion
    }
}
