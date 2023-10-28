using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.General
{
    public class AddToCartVM
    {
        public int Id { get; set; }
        [Display(Name = "کد محصول")]
        public string? ProductCode { get; set; }
        [Display(Name = "تعداد")]
        public int Quantity { get; set; }
        [Display(Name = "قیمت")]
        public int Price { get; set; }
        [Display(Name = "تخفیف")]
        public int? Discount { get; set; }
        [Display(Name = "قیمت خالص")]
        public int NetPrice { get; set; }
        public Guid? CartId { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Comment { get; set; }
        public string? ItemType { get; set; }//order-product
        public string? prColor { get; set; }
        public string? prHeight { get; set; }
        public string? prSize { get; set; }
        public List<(int Price, int NetPrice, int DiscountVal, int DiscountPercent)> ProductPricesFinance { get; set; } = new();

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
        public string? Length { get; set; }
        /// <summary>
        /// دور کمر
        /// </summary>

        [Display(Name = "دور کمر")]//Manto-Pants
        public int? AroundTheWaist { get; set; }
        [Display(Name = "قد شلوار")]
        public int? PantsLenght { get; set; }
        [Display(Name = "قد شومیز")]
        public int? ShomizLenght { get; set; }
        [Display(Name = "قد وست")]
        public int? VestLenght { get; set; }
        [Display(Name = "قد شکت")]
        public int? ShaketLenght { get; set; }
        #endregion
        public string? ReturnUrl { get; set; }
    }
}
