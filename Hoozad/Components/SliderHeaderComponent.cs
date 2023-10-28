using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class SliderHeaderComponent : ViewComponent
    {
        private readonly ISuppService _suppService;
        public SliderHeaderComponent(ISuppService suppService)
        {
            _suppService = suppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Slider> sliders = await _suppService.GetSlidersAsync();
            sliders = sliders.Where(w => w.IsActive).OrderBy(_ => _.ShowPriority).ToList();
            return await Task.FromResult(View("/Pages/Components/_GetSliderHeader.cshtml",sliders));
        }
    
    }
}
