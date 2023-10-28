using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class ProductSize
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// سایز
        /// </summary>
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سایز")]
        public int? Size { get; set; }
        /// <summary>
        /// دور سینه
        /// </summary>
        [Display(Name = "دور سینه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? ChestAround { get; set; }
        /// <summary>
        /// دور کمر
        /// </summary>
        [Display(Name = "دور کمر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? WaistAround { get; set; }
        /// <summary>
        /// دور باسن
        /// </summary>
        [Display(Name = "دور باسن")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? HipsAround { get; set; }
        /// <summary>
        /// دور بازو
        /// </summary>
        [Display(Name = "دور بازو")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? ArmAround { get; set; }
        /// <summary>
        /// قد آستین
        /// </summary>
        [Display(Name = "قد آستین")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? SleeveLength { get; set; }
        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public Guid? ProductId { get; set; }
        #region Relations
        [ForeignKey(nameof(ProductId))]
        [Display(Name = "محصول")]
        public Product? Product { get; set; }
        #endregion
    }
}
