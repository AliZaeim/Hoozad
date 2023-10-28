using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Supplementary
{
    public class BannerItem
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "نام")]
        public string? Name { get; set; }
        [Display(Name = "تصویر دسکتاپ")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Image { get; set; }
        [Display(Name = "تصویر موبایل")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? MobileImage { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "آدرس لینک")]
        public string? LinkUrl { get; set; }
        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "بسته بنر")]
        public int? BannerId { get; set; }
        #region Relations
        [ForeignKey(nameof(BannerId))]
        public Banner? Banner { get; set; }
        #endregion
    }
}
