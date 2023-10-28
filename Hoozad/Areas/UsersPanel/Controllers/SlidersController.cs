using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Context;
using DataLayer.Entities.Supplementary;
using Core.Services.Interfaces;
using Core.DTOs.General;
using Core.Utility;
using DataLayer.Entities.Store;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    public class SlidersController : Controller
    {       
        private readonly ISuppService _suppService;
        public SlidersController(ISuppService suppService)
        {            
            _suppService = suppService;
        }

        // GET: UsersPanel/Sliders
        public async Task<IActionResult> Index()
        {
              return View(await _suppService.GetSlidersAsync());
        }

        // GET: UsersPanel/Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _suppService.GetSlidersAsync() == null)
            {
                return NotFound();
            }

            var slider = await _suppService.GetSliderById(id.Value);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: UsersPanel/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider, IFormFile? Image,IFormFile? MobileImage)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError("Image", "لطفا تصویر را انتخاب کنید !");
                    return View(slider);
                }
                if (MobileImage == null)
                {
                    ModelState.AddModelError("MobileImage", "لطفا تصویر را انتخاب کنید !");
                    return View(slider);
                }
                if (Image != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                    FileValidation fileValidation1 = await Image.UploadedImageValidation(200, ex)!;

                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("Image", fileValidation1.Message!);
                        return View(slider);
                    }
                    slider.Image = Image.SaveUploadedImage("wwwroot/images/sliders", true);
                }
                if (MobileImage != null)
                {
                    string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                    FileValidation fileValidation1 = await MobileImage.UploadedImageValidation(200, ex)!;

                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("MobileImage", fileValidation1.Message!);
                        return View(slider);
                    }
                    slider.MobileImage = MobileImage.SaveUploadedImage("wwwroot/images/sliders", true);
                }
                _suppService.CreateSlider(slider!);
                await _suppService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: UsersPanel/Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _suppService.GetSlidersAsync() == null)
            {
                return NotFound();
            }
            var slider = await _suppService.GetSliderById(id.Value);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: UsersPanel/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Slider slider, IFormFile? Image,IFormFile? MobileImage)
        {
            if (id != slider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                        FileValidation fileValidation1 = await Image!.UploadedImageValidation(200, ex)!;

                        if (!fileValidation1.IsValid)
                        {
                            ModelState.AddModelError("Image", fileValidation1.Message!);
                            return View(slider);
                        }
                        slider.Image = Image!.SaveUploadedImage("wwwroot/images/sliders", true);
                    }
                    if (MobileImage != null)
                    {
                        string[] ex = { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".webp" };
                        FileValidation fileValidation1 = await MobileImage!.UploadedImageValidation(200, ex)!;

                        if (!fileValidation1.IsValid)
                        {
                            ModelState.AddModelError("MobileImage", fileValidation1.Message!);
                            return View(slider);
                        }
                        slider.MobileImage = MobileImage!.SaveUploadedImage("wwwroot/images/sliders", true);
                    }
                    _suppService.UpdateSlider(slider!);
                    await _suppService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.Id))
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
            return View(slider);
        }

        // GET: UsersPanel/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _suppService.GetSlidersAsync() == null)
            {
                return NotFound();
            }

            var slider = await _suppService.GetSliderById(id.Value);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: UsersPanel/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _suppService.GetSlidersAsync() == null)
            {
                return Problem("Entity set 'MyContext.Sliders'  is null.");
            }
            var slider = await _suppService.GetSliderById(id);
            if (slider != null)
            {
                await _suppService.DeleteSlider(id);
                _suppService.SaveChanges();
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
          return _suppService.ExistSlider(id);
        }
    }
}
