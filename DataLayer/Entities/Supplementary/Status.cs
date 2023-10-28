using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Supplementary
{
    /// <summary>
    /// وضعیت های سفارش
    /// </summary>
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام وضعیت")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Name { get; set; }
        [Display(Name ="توضیحات")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? Comment { get; set; }
        [Display(Name ="شروع فرآیند")]
        public bool StartOfProcess { get; set; }
        [Display(Name ="پایان فرآیند")]
        public bool EndOfProcess { get; set; }
        [Display(Name ="تحویل به پست")]
        public bool DeliverToPost { get; set; }
        [Display(Name ="تحویل به مشتری")]
        public bool DeliverToCustomer { get; set; }
        [Display(Name ="فعال/غیرفعال")]
        public bool IsActive { get; set; }
    }
}
