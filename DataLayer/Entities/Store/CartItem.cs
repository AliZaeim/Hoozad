
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class CartItem
    {
        public CartItem()
        {
            
            CartProductInfos = new List<CartProductInfo>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کد محصول")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? ProductCode { get; set; }       
        
        [Display(Name ="تعداد")]
        public int Quantity { get; set; }
        [Display(Name ="قیمت")]
        public int Price { get; set; }
        [Display(Name = "تخفیف")]
        public int? Discount { get; set; }
        [Display(Name = "قیمت خالص")]
        public int NetPrice { get; set; }
       
        public Guid? CartId { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Comment { get; set; }
        
        
        #region Relations       
        [ForeignKey(nameof(CartId))]
        public Cart? Cart { get; set; }
        
        public ICollection<CartProductInfo> CartProductInfos { get; set; }
        #endregion
    }
}
