using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class SiteInfo
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "ساعت کاری")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? WorkTime { get; set; }
        [Display(Name = "تلفن 1")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Phone1 { get; set; }
        [Display(Name = "تلفن 2")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Phone2 { get; set; }
        [Display(Name = "تلفن همراه 1")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Mobile1 { get; set; }
        [Display(Name = "تلفن همراه 2")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Mobile2 { get; set; }
        [Display(Name = "پیام صفحه اصلی")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? MainPageMessage { get; set; }
        [Display(Name = "آدرس")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Address { get; set; }
        [Display(Name = "آدرس مسیریاب")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? AddressLink { get; set; }
        [Display(Name = "واحد پول سایت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? SiteCurrency { get; set; }
        [Display(Name = "حداقل مبلغ ارسال رایگان")]        
        public int? FreeShippingValue { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name = "هزینه ارسال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ShippingCost { get; set; }
        [Display(Name = "ایمیل")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [DataType(DataType.EmailAddress,ErrorMessage ="ایمیل وارد شده نامعتبر است !")]
        public string? Email { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "آدرس اینستاگرام")]
        public string? InstagramLink { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "آدرس واتزآپ")]
        public string? WhatsupLink { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "آدرس تلگرام")]
        public string? TelegramLink { get; set; }
    }
}
