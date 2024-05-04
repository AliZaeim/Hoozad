using DataLayer.Context;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class CartStatusController : Controller
    {
        private readonly MyContext _context;

        public CartStatusController(MyContext context)
        {
            _context = context;
        }

        // GET: UsersPanel/CartStatus
        public async Task<IActionResult> Index()
        {
            var myContext = _context.CartStatuses!.Include(c => c.Cart);
            return View(await myContext.ToListAsync());
        }

        // GET: UsersPanel/CartStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartStatuses == null)
            {
                return NotFound();
            }

            var cartStatus = await _context.CartStatuses
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartStatus == null)
            {
                return NotFound();
            }

            return View(cartStatus);
        }

        // GET: UsersPanel/CartStatus/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id");
            return View();
        }

        // POST: UsersPanel/CartStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comment,RegDate,LastCreateUser,IsActive,CartId")] CartStatus cartStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", cartStatus.CartId);
            return View(cartStatus);
        }

        // GET: UsersPanel/CartStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartStatuses == null)
            {
                return NotFound();
            }

            var cartStatus = await _context.CartStatuses.FindAsync(id);
            if (cartStatus == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", cartStatus.CartId);
            return View(cartStatus);
        }

        // POST: UsersPanel/CartStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,RegDate,LastCreateUser,IsActive,CartId")] CartStatus cartStatus)
        {
            if (id != cartStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartStatusExists(cartStatus.Id))
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
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", cartStatus.CartId);
            return View(cartStatus);
        }

        // GET: UsersPanel/CartStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartStatuses == null)
            {
                return NotFound();
            }

            var cartStatus = await _context.CartStatuses
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartStatus == null)
            {
                return NotFound();
            }

            return View(cartStatus);
        }

        // POST: UsersPanel/CartStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartStatuses == null)
            {
                return Problem("Entity set 'MyContext.CartStatuses'  is null.");
            }
            var cartStatus = await _context.CartStatuses.FindAsync(id);
            if (cartStatus != null)
            {
                _context.CartStatuses.Remove(cartStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartStatusExists(int id)
        {
          return (_context.CartStatuses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
