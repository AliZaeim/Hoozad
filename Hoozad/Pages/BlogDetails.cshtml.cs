using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class BlogDetailsModel : PageModel
    {
        private readonly ISuppService _suppService;
        public BlogDetailsModel(ISuppService suppService)
        {
            _suppService = suppService;
        }
        public string Code { get; set; } = "";
        public Blog Blog { get; set; } = new();
        public List<BlogGroup> BlogGroups { get; set; } = new();
        public List<Blog> Blogs { get; set; } = new();
        public List<Blog> RecentBlogs { get; set; } = new();
        public List<Blog> ArchiveBlogs { get; set; } = new();
        public List<string> TopTags { get; set; } = new();
        public List<IGrouping<string, string>> GroupingTopTags { get; set; } = new();
        public SiteInfo SiteInfo { get; set; } = new();
        public async Task<IActionResult> OnGet(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound();
            }
            BlogGroups = await _suppService.GetBlogGroupsAsync();
            BlogGroups = BlogGroups.Where(w => w.IsActive).ToList();
            Blogs = await _suppService.GetBlogsAsync();
            Blogs = Blogs.Where(w => w.BlogIsActive).ToList();
            TopTags = Blogs.SelectMany(x => x.TagsList).ToList();
            GroupingTopTags = TopTags.GroupBy(g => g.Trim()).OrderByDescending(r => r.Count()).ToList();
            RecentBlogs = Blogs.OrderByDescending(r => r.BlogDate).ToList();
            SiteInfo = await _suppService.GetLastSiteInfoAsync();
            Blog = await _suppService.GetBlogByCodeAsync(code);
            Code = code;
            return Page();
        }
    }
}
