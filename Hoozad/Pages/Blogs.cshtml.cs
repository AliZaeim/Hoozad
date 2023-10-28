using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Linq;

namespace Web.Pages
{
    public class BlogsModel : PageModel
    {
        private readonly ISuppService _suppService;
        public BlogsModel(ISuppService suppService)
        {
            _suppService = suppService;
        }
        public List<BlogGroup> BlogGroups { get; set; } = new();
        public List<Blog> Blogs { get; set; } = new();
        public List<Blog> RecentBlogs { get; set; } = new();
        public List<Blog> ArchiveBlogs { get; set; } = new();
        public List<string> TopTags { get; set; } = new();
        public List<IGrouping<string, string>> GroupingTopTags { get; set; } = new();
        public int TotalRecs { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string Pagetitle { get; set; } = "";
        public async Task OnGet(int? pageId,int? ary,int? arm, string grName, string tag)
        {
            BlogGroups = await _suppService.GetBlogGroupsAsync();
            BlogGroups = BlogGroups.Where(w => w.IsActive).ToList();
            Blogs = await _suppService.GetBlogsAsync();
            Blogs = Blogs.Where(w => w.BlogIsActive).ToList();
            TotalRecs = Blogs.Count;
            if (TotalRecs % 12 == 0)
            {
                PageCount = TotalRecs / 12;
            }
            else
            {
                PageCount = (TotalRecs / 12) + 1;
            }
            TopTags = Blogs.SelectMany(x => x.TagsList).ToList();
            GroupingTopTags  = TopTags.GroupBy(g => g.Trim()).OrderByDescending(r => r.Count()).ToList();
            PersianCalendar PC = new();
            RecentBlogs = Blogs.OrderByDescending(r => r.BlogDate).ToList();
            
            if (!string.IsNullOrEmpty(grName))
            {
                BlogGroup blogGroup = await _suppService.GetBlogGroupByNameAsync(grName.Trim());
                Pagetitle = " گروه " + blogGroup?.BlogGroupTitle;
                Blogs = Blogs.Where(w => w.BlogGroup!.BlogGroupEnTitle == grName).ToList();
            }
            if (ary != null && arm !=null)
            {
                Pagetitle = " ماه " +arm.Value + " سال " +  ary.Value;
                Blogs = Blogs.Where(w => PC.GetYear(w.BlogDate!.Value) == ary.Value && PC.GetMonth(w.BlogDate!.Value) == arm.Value).ToList();
            }
            if (!string.IsNullOrEmpty(tag))
            {
                Pagetitle = " برچسب " + tag;
                Blogs = Blogs.Where(w => w.TagsList.Any(x => x.Trim() == tag)).ToList();
            }
            if (pageId == null)
            {
                Blogs = Blogs.Take(12).ToList();
                CurrentPage = 1;
                
            }
            else
            {
                Pagetitle = " صفحه " + pageId.Value; 
                Blogs = Blogs.Skip((pageId.Value-1)*12).Take(12).ToList();
                CurrentPage = pageId.Value;
            }
        }
    }
}
