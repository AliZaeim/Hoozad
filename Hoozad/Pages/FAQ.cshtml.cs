using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class FAQModel : PageModel
    {
        private readonly IGenericService<FAQ> _genericService;
        public FAQModel(IGenericService<FAQ> genericService)
        {
            _genericService = genericService;
        }
        public List<FAQ> FAQs { get; set; } = new();
        public async Task OnGet()
        {
            FAQs =(List<FAQ>)await _genericService.GetAllAsync();
        }
    }
}
