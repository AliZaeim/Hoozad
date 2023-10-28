using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class WareHouse
    {
        public WareHouse()
        {

        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "تاریخ")]

        public DateTime? RegDate { get; set; }

        [Display(Name = "تعداد ورود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Input { get; set; } = 0;
        [Display(Name = "تعداد خروج")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Export { get; set; } = 0;
        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public Guid? ProductId { get; set; }

        [Display(Name = "جزئیات سفارش")]
        public int? CartItemId { get; set; }
        [Display(Name = "حذف شده")]
        public bool IsDeleted { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? WareHouse_Comment { get; set; }
        #region Relations
        [Display(Name = "محصول")]
        [ForeignKey(nameof(ProductId))]        
        public Product? Product { get; set; }

        [ForeignKey(nameof(CartItemId))]
        [Display(Name = "جزئیات سفارش")]
        public CartItem? CartItem { get; set; }
        #endregion
    }
}
