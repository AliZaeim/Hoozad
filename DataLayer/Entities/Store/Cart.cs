using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "کاربر")]
        public int? UserId { get; set; }
        [Display(Name = "تسویه حساب")]
        public bool CheckOut { get; set; }
        [Display(Name = "تاریخ پرداخت")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "کوپن تخفیف")]
        public string? DiscountCoupen { get; set; }
        [Display(Name = "درصد کوپن تخفیف")]
        public float? DiscountPercent { get; set; }
        [Display(Name = "مبلغ کوپن تخفیف")]
        public int? DiscountValue { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "خریدار و تحویل گیرنده یک نفر هستند؟")]
        public bool BuyerIsRecepient { get; set; }
        [Display(Name = "واحد پول")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Currency { get; set; }
        [Display(Name = "نام خریدار")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BuyerName { get; set; }
        [Display(Name = "فامیلی خریدار")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BuyerFamily { get; set; }
        [Display(Name = "تلفن همراه خریدار")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BuyerCellphone { get; set; }
        [Display(Name = "نام تحویل گیرنده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? RecipientName { get; set; }
        [Display(Name = "نام خانوادگی تحویل گیرنده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? RecipientFamily { get; set; }
        [Display(Name = "تلفن تحویل گیرنده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? RecipientPhone { get; set; }
        [Display(Name = "استان")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? StateName { get; set; }
        [Display(Name = "شهرستان")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? CountyName { get; set; }
        [Display(Name = "آدرس تحویل")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Address { get; set; }
        [Display(Name = "کد پستی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? PostalCode { get; set; }
        [Display(Name = "پرداخت کرایه هنگام تحویل (تیپاکس)")]
        public bool ShippingWithTipax { get; set; }
        [Display(Name = "پرداخت کرایه هنگام تحویل (اسنپ)")]
        public bool ShippingWithSnapp { get; set; }

        [Display(Name = "ارسال با پست")]
        public bool ShippingWithPost { get; set; }
        [Display(Name = "هزینه ارسال")]
        public int? Freight { get; set; }
        [Display(Name="پرداخت دو مرحله ای")]
        public bool TwoStagePayment { get; set; }
        [Display(Name = "مبلغ پرداخت دو مرحله ای")]
        public int TwoStagePayValue { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Comment { get; set; }
        [Display(Name = "کد پیگیری")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? TracingNumber { get; set; }
        [Display(Name = "شماره سفارش")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? OrderNumber { get; set; }
        [Display(Name ="مبلغ نهایی کارت")]
        public int CartSum { get; set; }
        #region Relations
        [ForeignKey(nameof(UserId))]
        public User.User? User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<CartStatus> CartStatuses { get; set; }
        #endregion
    }
}
