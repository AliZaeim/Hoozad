using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class ProductSizeVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سایز")]
        public int? Size { get; set; }
        [Display(Name = "دور سینه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? ChestAround { get; set; }
        [Display(Name = "دور کمر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? WaistAround { get; set; }
        [Display(Name = "دور باسن")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? HipsAround { get; set; }
        [Display(Name = "دور بازو")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? ArmAround { get; set; }
        [Display(Name = "قد آستین")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد !")]
        public string? SleeveLength { get; set; }
        public Guid? ProductId { get; set; }
    }
}
