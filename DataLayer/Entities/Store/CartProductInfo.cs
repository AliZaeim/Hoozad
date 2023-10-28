using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class CartProductInfo
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "رنگ")]
        public string? Color { get; set; }
        [Display(Name = "سایز")]
        public int? Size { get; set; }
        [Display(Name = "قد")]
        public int? Height { get; set; }
        [Display(Name = "قیمت خالص")]
        public int? NetPrice { get; set; }
        [Display(Name ="قیمت")]
        public int? Price { get; set; }
        [Display(Name = "تعداد")]        
        public int Quantity { get; set; } 
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        public int? CartItemId { get; set; }
        #region Relations
        [ForeignKey(nameof(CartItemId))]
        public CartItem? CartItem { get; set; }
        #endregion
    }
}
