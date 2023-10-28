using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AboutModel : PageModel
    {
        private readonly IGenericService<ContactMessage> _conService;
        public AboutModel(IGenericService<ContactMessage> conService)
        {
            _conService = conService;
        }
        public List<ContactMessage> ContactMessages { get; set; } = new();
        public async Task OnGet()
        {
            ContactMessages =(List<ContactMessage>)await _conService.GetAllAsync();
        }
    }
}
