using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ISuppService _suppService;
        public ContactModel(ISuppService suppService)
        {
            _suppService = suppService;
        }
        public SiteInfo SiteInfo { get; set; } = new();
        [BindProperty]
        public ContactMessage ContactMessage { get; set; } = new();
        public async Task OnGet()
        {
            TempData["Saved"] = null;
            SiteInfo = await _suppService.GetLastSiteInfoAsync();
        }
        public async Task<IActionResult> OnPost()
        {
            ContactMessage.IsActive = true;
            ContactMessage.RegDate = DateTime.Now;
            _suppService.CreateContactMessage(ContactMessage);
            await _suppService.SaveChangesAsync();
            TempData["Saved"] = "yes";
            SiteInfo = await _suppService.GetLastSiteInfoAsync();
            ContactMessage = new();
            return RedirectToPage("/Contact");
            
        }

    }
}
