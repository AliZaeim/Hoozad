using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(6, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد!")]
        public string? ProductCode { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? ProductName { get; set; }

        [Display(Name = "عنوان سئوی صفحه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? ProductPageTitle { get; set; }
        [Display(Name = "شرح سئوی صفحه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(160, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? ProductPageDescription { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ProductPrice { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? ProductComment { get; set; }
        [Display(Name = "درصد تخفیف")]
        public float? ProductPercentDiscount { get; set; }
        [Display(Name = "مبلغ تخفیف")]
        public float? ProductValueDiscount { get; set; }
        [Display(Name = "تگها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} باید {1} رقم باشد!")]
        public string? ProductTagsList { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "تصویر اول، w:360 و h:360")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? ProductImage1 { get; set; }

        [Display(Name = "تصویر دوم، w:360 و h:360")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? ProductImage2 { get; set; }
        [Display(Name = "تصویر سوم، w:360 و h:360")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? ProductImage3 { get; set; }
        [Display(Name = "تصویر چهارم، w:360 و h:360")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? ProductImage4 { get; set; }
        [Display(Name = "توضیح عکسها ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? ProductImageAlt { get; set; }
        [Display(Name = "محبوبیت")]
        public int? Popularity { get; set; }
        [Display(Name = "برچسب روی عکس اصلی")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? ProductLabel { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ProductGroupId { get; set; }
        [Display(Name = "بازدید")]
        public int? Views { get; set; } = 0;

        [Display(Name = "حــذف")]
        public bool IsDeleted { get; set; }
        public List<ProductSizeViewModel> ProductPriceVMs { get; set; } = new();
    }
    public class ProductSizeViewModel
    {
        public int Id { get; set; }        
        [Display(Name = "قد")]
        public int? Hight { get; set; }        
        [Display(Name = "قیمت")]
        public int? Price { get; set; }
    }
}
