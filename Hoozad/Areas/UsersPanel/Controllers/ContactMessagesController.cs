using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("cmess")]
    public class ContactMessagesController : Controller
    {
        
        private readonly ISuppService _suppServie;
        public ContactMessagesController(ISuppService suppServie)
        {            
            _suppServie = suppServie;
        }

        // GET: UsersPanel/ContactMessages
        public async Task<IActionResult> Index()
        {
              return View(await _suppServie.GetContactMessagesAsync());
        }

        // GET: UsersPanel/ContactMessages/Details/5
        [PermissionCheckerByPermissionName("cmdet")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _suppServie.GetContactMessagesAsync() == null)
            {
                return NotFound();
            }

            var contactMessage = await _suppServie.GetContactMessageByIdAsync(id.Value);
            if (contactMessage == null)
            {
                return NotFound();
            }

            return View(contactMessage);
        }

        // GET: UsersPanel/ContactMessages/Create
        [PermissionCheckerByPermissionName("cmadd")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersPanel/ContactMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("cmadd")]
        public async Task<IActionResult> Create(ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                contactMessage.RegDate = DateTime.Now;
                _suppServie.CreateContactMessage(contactMessage);
                await _suppServie.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactMessage);
        }

        // GET: UsersPanel/ContactMessages/Edit/5
        [PermissionCheckerByPermissionName("cmedit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _suppServie.GetContactMessagesAsync() == null)
            {
                return NotFound();
            }

            var contactMessage = await _suppServie.GetContactMessageByIdAsync(id.Value);
            if (contactMessage == null)
            {
                return NotFound();
            }
            return View(contactMessage);
        }

        // POST: UsersPanel/ContactMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("cmedit")]
        public async Task<IActionResult> Edit(int id, ContactMessage contactMessage)
        {
            if (id != contactMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _suppServie.UpdateContactMessage(contactMessage);
                    await _suppServie.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactMessageExists(contactMessage.Id))
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
            return View(contactMessage);
        }

        // GET: UsersPanel/ContactMessages/Delete/5
        [PermissionCheckerByPermissionName("cmdel")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _suppServie.GetContactMessagesAsync() == null)
            {
                return NotFound();
            }
            var contactMessage = await _suppServie.GetContactMessageByIdAsync(id.Value);
            if (contactMessage == null)
            {
                return NotFound();
            }
            return View(contactMessage);
        }

        // POST: UsersPanel/ContactMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionCheckerByPermissionName("cmdel")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _suppServie.GetContactMessagesAsync() == null)
            {
                return Problem("Entity set 'MyContext.ContactMessages'  is null.");
            }
            var contactMessage = await _suppServie.GetContactMessageByIdAsync(id);
            if (contactMessage != null)
            {
                _suppServie.DeleteContactMessage(contactMessage);
                await _suppServie.SaveChangesAsync();
            }          
            return RedirectToAction(nameof(Index));
        }

        private bool ContactMessageExists(int id)
        {
            return _suppServie.ExistContactMessage(id);
        }
    }
}
