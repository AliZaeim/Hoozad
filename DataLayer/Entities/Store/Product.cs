using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class Product
    {
        public Product()
        {
            ProductComments = new List<ProductComment>();
            ProductPrices = new List<ProductPrice>();
            ProductColors = new List<ProductColor>();
            ProductSizes = new List<ProductSize>();
            ProductComponents = new List<ProductComponent>();
        }
        [Key]
        public Guid ProductId { get; set; }
        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
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
        [Display(Name = "شرح کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? ProductSmallComment { get; set; }

        [Display(Name = "درصد تخفیف")]
        public float? ProductPercentDiscount { get; set; }
        [Display(Name = "مبلغ تخفیف")]
        public int? ProductValueDiscount { get; set; }
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
        [Display(Name = "حداقل موجودی دفتر انبار جهت هشدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? MinimumStockForWarning { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ProductGroupId { get; set; }
        [Display(Name = "بازدید")]
        public int? Views { get; set; } = 0;
        [Display(Name = "اجازه خرید تکی دارد؟")]
        [Required(ErrorMessage = "لطفا وضعیت اجازه خرید را انتخاب کنید !")]
        public bool? AllowedSinglePurchase { get; set; }
        #region ClothProperties
        /// <summary>
        /// دور سینه
        /// </summary>

        [Display(Name = "دور سینه")]//Manto- Shomiz
        public int? AroundTheChest { get; set; }
        /// <summary>
        /// دور باسن
        /// </summary>

        [Display(Name = "دور باسن")]//Manto-Shomiz-Pants
        public int? AroundTheHips { get; set; }
        /// <summary>
        /// قد مانتو
        /// </summary>

        [Display(Name = "قد مانتو")]//Manto-Shomiz-Pants
        public int? Length { get; set; }
        /// <summary>
        /// دور کمر
        /// </summary>

        [Display(Name = "دور کمر")]//Manto-Pants
        public int? AroundTheWaist { get; set; }
        #endregion

        [Display(Name = "حــذف")]
        public bool IsDeleted { get; set; }

        [NotMapped]
        public IList<string> ProductNameList
        {
            get { return (ProductName ?? string.Empty).Split(" "); }
        }
        [NotMapped]
        public IList<string> TagsList
        {
            get { return (ProductTagsList ?? string.Empty).Split("-"); }
        }
        #region Relations
        [Display(Name = "گروه")]
        public ProductGroup? ProductGroup { get; set; }
        public ICollection<ProductComment>? ProductComments { get; set; }
        public List<ProductPrice> ProductPrices { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductComponent> ProductComponents { get; set; }
        #endregion
        [NotMapped]
        public string ProductFullName
        {
            get { return ProductGroup?.Title + " " + ProductName; }
        }

    }
}
