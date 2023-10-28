using DataLayer.Entities.Store;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.General
{
    public class MantoShalvarOrderVM
    {
        public string? ProductCode { get; set; }
        /// <summary>
        /// دور سینه
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "دور سینه")]
        public int? AroundTheChest { get; set; }
        /// <summary>
        /// دور باسن
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "دور باسن")]
        public int? AroundTheHips { get; set; }        
        /// <summary>
        /// دور کمر
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "دور کمر")]
        public int? AroundTheWaist { get; set; }
        /// <summary>
        /// قد مانتو
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "قد مانتو")]
        public int? Length { get; set; }
        /// <summary>
        /// قد شلوار
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "قد شلوار")]
        public int? PantsLenght { get; set; }
        [Display(Name = "یادداشت")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Comment { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تعداد")]
        public int? Quantity { get; set; }
        [Display(Name = "قیمت")]
        public int? Price { get; set; }
        public string? ItemType { get; set; }
        public Product? Product { get; set; }
        public List<ProductPrice> ProductPrices { get; set; } = new();
        public string? Currency { get; set; }
        public List<(int Height, int Price, int NetPrice, int DiscountVal, int DiscountPercent)> ProductPricesFinance { get; set; } = new();
        public string? ReturnUrl { get; set; }
    }
    
}
