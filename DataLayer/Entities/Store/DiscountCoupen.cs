﻿using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Store
{
    public class DiscountCoupen
    {
        public DiscountCoupen()
        {

        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Code { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Comment { get; set; }
        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Number { get; set; }
        [Display(Name = "باقیمانده")]
        public int? Remain { get; set; }
        [Display(Name = "درصد تخفیف")]
        public float? Percent { get; set; }
        [Display(Name = "مبلغ تخفیف")]
        public decimal? Value { get; set; }
        [Display(Name = "تاریخ شروع اعتبار")]
        public DateTime? StartedDate { get; set; }
        [Display(Name = "تاریخ پایان اعتبار")]
        public DateTime? ExpiredDate { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }

    }
}
