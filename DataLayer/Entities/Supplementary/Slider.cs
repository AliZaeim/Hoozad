using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "عنوان")]
        public string? Title { get; set; }        
        [Display(Name = "متن لینک")]
        public string? LinkText { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "آدرس لینک")]
        public string? LinkUrl { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "تصویر دسکتاپ")]
        public string? Image { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "تصویر موبایل")]
        public string? MobileImage { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "اولویت نمایش")]
        public int? ShowPriority { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }

    }
}
