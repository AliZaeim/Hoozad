using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("gnews")]
    public class BlogGroupsController : Controller
    {        
        private readonly ISuppService _suppService;
        public BlogGroupsController(ISuppService suppService)
        {            
            _suppService = suppService;
        }

        // GET: UsersPanel/BlogGroups
        public async Task<IActionResult> Index()
        {
              return View(await _suppService.GetBlogGroupsAsync());
        }

        // GET: UsersPanel/BlogGroups/Details/5
        [PermissionCheckerByPermissionName("gnedet")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _suppService.GetBlogGroupsAsync() == null)
            {
                return NotFound();
            }

            var blogGroup = await _suppService.GetBlogGroupByIdAsync(id.Value);
            if (blogGroup == null)
            {
                return NotFound();
            }

            return View(blogGroup);
        }

        // GET: UsersPanel/BlogGroups/Create
        [PermissionCheckerByPermissionName("gneadd")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/BlogGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("gneadd")]
        public async Task<IActionResult> Create(BlogGroup blogGroup)
        {
            if (ModelState.IsValid)
            {
                _suppService.CreateBlogGroup(blogGroup);
                await _suppService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogGroup);
        }

        // GET: UsersPanel/BlogGroups/Edit/5
        [PermissionCheckerByPermissionName("gneedit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _suppService.GetBlogGroupsAsync() == null)
            {
                return NotFound();
            }

            var blogGroup = await _suppService.GetBlogGroupByIdAsync(id.Value);
            if (blogGroup == null)
            {
                return NotFound();
            }
            return View(blogGroup);
        }

        // POST: UsersPanel/BlogGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("gneedit")]
        public async Task<IActionResult> Edit(int id, BlogGroup blogGroup)
        {
            if (id != blogGroup.BlogGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _suppService.UpdateBlogGroup(blogGroup);
                    await _suppService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogGroupExists(blogGroup.BlogGroupId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogGroup);
        }

        // GET: UsersPanel/BlogGroups/Delete/5
        [PermissionCheckerByPermissionName("gnedel")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _suppService.GetBlogGroupsAsync() == null)
            {
                return NotFound();
            }
            var blogGroup = await _suppService.GetBlogGroupByIdAsync(id.Value);
            if (blogGroup == null)
            {
                return NotFound();
            }

            return View(blogGroup);
        }

        // POST: UsersPanel/BlogGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("gnedel")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _suppService.GetBlogGroupsAsync() == null)
            {
                return Problem("Entity set 'MyContext.BlogGroups'  is null.");
            }
            var blogGroup = await _suppService.GetBlogGroupByIdAsync(id);
            if (blogGroup != null)
            {
                blogGroup.IsDeleted = true;
                _suppService.UpdateBlogGroup(blogGroup);
                await _suppService.SaveChangesAsync();
            }           
            return RedirectToAction(nameof(Index));
        }

        private bool BlogGroupExists(int id)
        {
          return _suppService.ExistBlogGroup(id);
        }
    }
}
