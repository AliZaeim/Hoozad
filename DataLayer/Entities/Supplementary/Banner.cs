using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class Banner
    {
        public Banner()
        {
            BannerItems = new List<BannerItem>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "نام")]
        public string? Name { get; set; }
        [Display(Name = "فعال/ غیرفعال")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نوع بسته")]
        public int? ItemCount { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "اولویت نمایش")]
        public int? ShowPriority { get; set; }
        #region Relations
        public ICollection<BannerItem> BannerItems { get; set; }
        #endregion
    }
}
