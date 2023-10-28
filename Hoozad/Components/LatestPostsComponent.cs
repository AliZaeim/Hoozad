using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class LatestPostsComponent : ViewComponent
    {
        private readonly ISuppService _suppService;
        public LatestPostsComponent(ISuppService suppService)
        {
            _suppService = suppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Blog> blogs = await _suppService.GetBlogsAsync();
            blogs = blogs.Where(w => w.BlogIsActive).OrderByDescending(r => r.BlogDate).Take(6).ToList();
            return await Task.FromResult(View("/Pages/Components/_GetLastestPosts.cshtml",blogs));
        }
    }
}
