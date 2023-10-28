using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Blogs
{
    public class Blog
    {
        public Blog()
        {
            BlogComments = new HashSet<BlogComment>();
        }
        [Key]
        public Guid BlogId { get; set; }
        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? BlogTitle { get; set; }
        [Display(Name = "تاریخ خبر")]
        
        public DateTime? BlogDate { get; set; }
        [Display(Name = "تصویر  صفحه بلاگ")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? BlogImageInBlog { get; set; }
        [Display(Name ="تصویر صفحه جزئیات")]
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BlogImageInBlogDetails { get; set; }
        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? BlogText { get; set; }
        [Display(Name = "عنوان سئو صفحه خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BlogPageTitle { get; set; }
        [Display(Name = "شرح سئو صفحه خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BlogPageDescription { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "خلاصه خبر")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? BlogSummary { get; set; }
        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? BlogTags { get; set; }
        [Display(Name = "فعال / غیرفعال")]
        public bool BlogIsActive { get; set; }
        [Display(Name = "گروه خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? BlogGroupId { get; set; }
        [Display(Name = "عبارت لینک خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? BlogUrl { get; set; }
        [Display(Name = "لینک  خارجی خبر")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string? BlogRefferalLink { get; set; }
        [Display(Name ="متن لینک")]        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BlogLinkText { get; set; }
        [Display(Name = "تعداد مشاهده")]
        public int? BlogViewsCount { get; set; }
        [Display(Name = "کد محصولات مرتبط")]
        public string? ProductCodes { get; set; }
        
        [Display(Name ="نویسنده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Author { get; set; }
        [NotMapped]
        public IList<string> ProductCodeList
        {
            get { return (ProductCodes ?? string.Empty).Split("-"); }
        }
        
        [NotMapped]
        public IList<string> TagsList
        {
            get { return (BlogTags ?? string.Empty ).Split("-"); }
        }
        public bool IsDeleted { get; set; }
        #region Relations
        [Display(Name = "گروه خبر")]
        public BlogGroup? BlogGroup { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        

        #endregion
    }
}
