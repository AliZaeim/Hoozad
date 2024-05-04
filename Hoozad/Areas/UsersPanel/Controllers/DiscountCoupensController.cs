using Core.Convertors;
using Core.DTOs.Admin;
using Core.Security;
using Core.Services;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("disco")]
    public class DiscountCoupensController : Controller
    {

        private readonly IGenericService<DiscountCoupen> _discountCoupenService;

        public DiscountCoupensController(IGenericService<DiscountCoupen> discountCoupenService)
        {

            _discountCoupenService = discountCoupenService;
        }
        public async Task<JsonResult> GetCoupen()
        {
            List<DiscountCoupen> discountCoupens = (List<DiscountCoupen>)await _discountCoupenService.GetAllAsync();
            List<string?> coupens = new();
            coupens = discountCoupens.Select(x => x.Code).ToList();

            string getc = Core.Prodocers.Generators.GenerateUniqueString(coupens!, 3, 3, 4, 2);
            return Json(getc);
        }

        public async Task<JsonResult> CheckCode(string Code)
        {
            if (!string.IsNullOrEmpty(Code))
            {
                List<DiscountCoupen> discountCoupens = (List<DiscountCoupen>)await _discountCoupenService.GetAllAsync();
                bool exist = discountCoupens.Any(_ => _.Code == Code);
                return Json(exist);
            }
            return Json(false);
        }
        // GET: UsersPanel/DiscountCoupens
        public async Task<IActionResult> Index()
        {
            return View(await _discountCoupenService.GetAllAsync());
        }

        // GET: UsersPanel/DiscountCoupens/Details/5
        [PermissionCheckerByPermissionName("dcdet")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _discountCoupenService.GetAll() == null)
            {
                return NotFound();
            }

            var discountCoupen = await _discountCoupenService.GetByIdAsync(id.Value);
            if (discountCoupen == null)
            {
                return NotFound();
            }

            return View(discountCoupen);
        }

        // GET: UsersPanel/DiscountCoupens/Create
        [PermissionCheckerByPermissionName("dcadd")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/DiscountCoupens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("dcadd")]
        public async Task<IActionResult> Create(DiscountCoupenVM discountCoupenVM)
        {
            if (ModelState.IsValid)
            {
                List<DiscountCoupen> discountCoupens = (List<DiscountCoupen>)await _discountCoupenService.GetAllAsync();
                bool Exist = discountCoupens.Any(a => a.Code == discountCoupenVM.Code?.Trim());
                if (Exist)
                {
                    ModelState.AddModelError("Code", "کد وارد شده تکراری است !");
                    return View(discountCoupenVM);
                }
                DiscountCoupen discountCoupen = new()
                {
                    Code = discountCoupenVM.Code,
                    Comment = discountCoupenVM.Comment,
                    Percent = discountCoupenVM.Percent,
                    Number = discountCoupenVM.Number,

                    ExpiredDate = discountCoupenVM.ExpiredDate?.ToMiladiWithoutTime(),
                    IsActive = discountCoupenVM.IsActive,
                };
                if (!string.IsNullOrEmpty(discountCoupenVM.StartedDate))
                {
                    discountCoupen.StartedDate = discountCoupenVM.StartedDate?.ToMiladiWithoutTime();
                }
                if (!string.IsNullOrEmpty(discountCoupenVM.ExpiredDate))
                {
                    discountCoupen.ExpiredDate = discountCoupenVM.ExpiredDate?.ToMiladiWithoutTime();
                }
                _discountCoupenService.Create(discountCoupen);
                await _discountCoupenService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discountCoupenVM);
        }

        // GET: UsersPanel/DiscountCoupens/Edit/5
        [PermissionCheckerByPermissionName("dcedit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _discountCoupenService.GetAll() == null)
            {
                return NotFound();
            }

            var discountCoupen = await _discountCoupenService.GetByIdAsync(id.Value);
            if (discountCoupen == null)
            {
                return NotFound();
            }
            DiscountCoupenVM discountCoupenVM = new()
            {
                Code = discountCoupen.Code,
                Comment = discountCoupen.Comment,
                Percent = discountCoupen.Percent,
                Number = discountCoupen.Number,
                StartedDate = discountCoupen.StartedDate.ToShamsiN(),
                ExpiredDate = discountCoupen.ExpiredDate.ToShamsiN(),
                IsActive = discountCoupen.IsActive,
            };
            return View(discountCoupenVM);
        }

        // POST: UsersPanel/DiscountCoupens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("dcedit")]
        public async Task<IActionResult> Edit(int id, DiscountCoupenVM discountCoupenVM)
        {
            if (id != discountCoupenVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DiscountCoupen? discountCoupen = await _discountCoupenService.GetByIdAsync(id);
                    if (discountCoupen != null)
                    {
                        discountCoupen.Number = discountCoupenVM.Number;
                        discountCoupen.Comment = discountCoupenVM.Comment;
                        discountCoupen.Code = discountCoupenVM.Code;
                        discountCoupen.IsActive = discountCoupenVM.IsActive;

                        if (!string.IsNullOrEmpty(discountCoupenVM.StartedDate))
                        {
                            discountCoupen.StartedDate = discountCoupenVM.StartedDate?.ToMiladiWithoutTime();
                        }
                        if (!string.IsNullOrEmpty(discountCoupenVM.ExpiredDate))
                        {
                            discountCoupen.ExpiredDate = discountCoupenVM.ExpiredDate?.ToMiladiWithoutTime();
                        }
                        _discountCoupenService.Edit(discountCoupen);
                        await _discountCoupenService.SaveChangesAsync();
                    }

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountCoupenExists(discountCoupenVM.Id))
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
            return View(discountCoupenVM);
        }

        // GET: UsersPanel/DiscountCoupens/Delete/5
        [PermissionCheckerByPermissionName("dcdel")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _discountCoupenService.GetAll() == null)
            {
                return NotFound();
            }

            var discountCoupen = await _discountCoupenService.GetByIdAsync(id.Value);
            if (discountCoupen == null)
            {
                return NotFound();
            }

            return View(discountCoupen);
        }

        // POST: UsersPanel/DiscountCoupens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("dcdel")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_discountCoupenService.GetAll() == null)
            {
                return Problem("Entity set 'MyContext.DiscountCoupens'  is null.");
            }
            var discountCoupen = await _discountCoupenService.GetByIdAsync(id);
            if (discountCoupen != null)
            {
                _discountCoupenService.Delete(discountCoupen);
                await _discountCoupenService.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Index));
        }

        private bool DiscountCoupenExists(int id)
        {
            return _discountCoupenService.ExistEntity(id);
        }
    }
}
