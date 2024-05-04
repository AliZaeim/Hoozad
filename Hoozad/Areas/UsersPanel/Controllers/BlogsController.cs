using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Context;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Authorization;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using System.Text.RegularExpressions;
using Core.DTOs.General;
using Core.Utility;
using Core.Security;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("news")]
    public class BlogsController : Controller
    {        
        private ISuppService _suppService;
        public BlogsController(ISuppService suppService)
        {            
            _suppService = suppService;
        }

        // GET: UsersPanel/Blogs
        public async Task<IActionResult> Index()
        {            
            return View(await _suppService.GetBlogsAsync());
        }

        // GET: UsersPanel/Blogs/Details/5
        [PermissionCheckerByPermissionName("nedet")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || await _suppService.GetBlogsAsync() == null)
            {
                return NotFound();
            }

            var blog = await _suppService.GetBlogById(id.Value);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: UsersPanel/Blogs/Create
        [PermissionCheckerByPermissionName("neadd")]
        public async Task<IActionResult> Create()
        {
            List<BlogGroup> blogGroups = await _suppService.GetBlogGroupsAsync();
            blogGroups = blogGroups.Where(w => w.IsActive).ToList();
            ViewData["BlogGroupId"] = new SelectList(blogGroups.ToList(), "BlogGroupId", "BlogGroupTitle");
            return View();
        }

        // POST: UsersPanel/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("neadd")]
        public async Task<IActionResult> Create(Blog blog, IFormFile BlogImageInBlog,IFormFile BlogImageInBlogDetails)
        {
            List<BlogGroup> blogGroups = await _suppService.GetBlogGroupsAsync();
            blogGroups = blogGroups.Where(w => w.IsActive).ToList();
            if (ModelState.IsValid)
            {
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                if (BlogImageInBlog == null)
                {
                    ViewData["BlogGroupId"] = new SelectList(blogGroups.Where(w => w.IsActive).ToList(), "Id", "Title", blog.BlogGroupId);
                    ModelState.AddModelError("BlogImageInBlog", "تصویر را انتخاب کنید !");
                    return View(blog);
                }
                if (BlogImageInBlogDetails == null)
                {
                    ViewData["BlogGroupId"] = new SelectList(blogGroups.Where(w => w.IsActive).ToList(), "Id", "Title", blog.BlogGroupId);
                    ModelState.AddModelError("BlogImageInBlogDetails", "تصویر را انتخاب کنید !");
                    return View(blog);
                }
                if (BlogImageInBlog != null)
                {
                    FileValidation fileValidation1 = await BlogImageInBlog.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ViewData["BlogGroupId"] = new SelectList(blogGroups.Where(w => w.IsActive).ToList(), "Id", "Title", blog.BlogGroupId);
                        ModelState.AddModelError("BlogImageInBlog", fileValidation1.Message!);
                        return View(blog);
                    }
                    blog.BlogImageInBlog = BlogImageInBlog.SaveUploadedImage("wwwroot/images/blogs", true);
                }
                if (BlogImageInBlogDetails != null)
                {
                    FileValidation fileValidation1 = await BlogImageInBlogDetails.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ViewData["BlogGroupId"] = new SelectList(blogGroups.Where(w => w.IsActive).ToList(), "Id", "Title", blog.BlogGroupId);
                        ModelState.AddModelError("BlogImageInBlogDetails", fileValidation1.Message!);
                        return View(blog);
                    }
                    blog.BlogImageInBlogDetails = BlogImageInBlogDetails.SaveUploadedImage("wwwroot/images/blogs", true);
                }
                blog.BlogId = Guid.NewGuid();
                blog.BlogDate = DateTime.Now;
                _suppService.CreateBlog(blog);
                await _suppService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Select(x => x.Value!.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            ViewData["BlogGroupId"] = new SelectList(blogGroups.ToList(), "BlogGroupId", "BlogGroupTitle");
            return View(blog);
        }

        // GET: UsersPanel/Blogs/Edit/5
        [PermissionCheckerByPermissionName("needit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || await _suppService.GetBlogsAsync() == null)
            {
                return NotFound();
            }
            List<BlogGroup> blogGroups = await _suppService.GetBlogGroupsAsync();
            blogGroups = blogGroups.Where(w => w.IsActive).ToList();
            var blog = await _suppService.GetBlogById(id.Value);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["BlogGroupId"] = new SelectList(blogGroups, "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
            return View(blog);
        }

        // POST: UsersPanel/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("needit")]
        public async Task<IActionResult> Edit(Guid id, Blog blog, IFormFile? BlogImageInBlog, IFormFile? BlogImageInBlogDetails)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                    if (BlogImageInBlog != null)
                    {
                        List<BlogGroup> blogGroupsx = await _suppService.GetBlogGroupsAsync();
                        blogGroupsx = blogGroupsx.Where(w => w.IsActive).ToList();
                        FileValidation fileValidation1 = await BlogImageInBlog.UploadedImageValidation(50, ex)!;
                        if (!fileValidation1.IsValid)
                        {
                            ViewData["BlogGroupId"] = new SelectList(blogGroupsx.Where(w => w.IsActive).ToList(), "Id", "Title", blog.BlogGroupId);
                            ModelState.AddModelError("BlogImageInBlog", fileValidation1.Message!);
                            return View(blog);
                        }
                        blog.BlogImageInBlog = BlogImageInBlog.SaveUploadedImage("wwwroot/images/blogs", true);
                    }
                    if (BlogImageInBlogDetails != null)
                    {                       
                        FileValidation fileValidation1 = await BlogImageInBlogDetails.UploadedImageValidation(50, ex)!;
                        if (!fileValidation1.IsValid)
                        {
                            List<BlogGroup> blogGroupsy = await _suppService.GetBlogGroupsAsync();
                            blogGroupsy = blogGroupsy.Where(w => w.IsActive).ToList();
                            ViewData["BlogGroupId"] = new SelectList(blogGroupsy.Where(w => w.IsActive).ToList(), "Id", "Title", blog.BlogGroupId);
                            ModelState.AddModelError("BlogImageInBlogDetails", fileValidation1.Message!);
                            return View(blog);
                        }
                        blog.BlogImageInBlogDetails = BlogImageInBlogDetails.SaveUploadedImage("wwwroot/images/blogs", true);
                    }
                    _suppService.UpdateBlog(blog);
                    await _suppService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
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
            List<BlogGroup> blogGroups = await _suppService.GetBlogGroupsAsync();
            blogGroups = blogGroups.Where(w => w.IsActive).ToList();
            ViewData["BlogGroupId"] = new SelectList(blogGroups, "BlogGroupId", "BlogGroupTitle", blog.BlogGroupId);
            return View(blog);
        }

        // GET: UsersPanel/Blogs/Delete/5
        [PermissionCheckerByPermissionName("nedel")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || await _suppService.GetBlogsAsync() == null)
            {
                return NotFound();
            }
            var blog = await _suppService.GetBlogById(id.Value);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: UsersPanel/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("nedel")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (await _suppService.GetBlogsAsync() == null)
            {
                return Problem("Entity set 'MyContext.Blogs'  is null.");
            }
            var blog = await _suppService.GetBlogById(id);
            if (blog != null)
            {
                blog.IsDeleted = true;
                _suppService.UpdateBlog(blog);
                await _suppService.SaveChangesAsync();
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(Guid id)
        {
          return _suppService.ExistBlog(id);
        }
    }
}
