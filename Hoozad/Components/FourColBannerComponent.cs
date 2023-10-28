using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class FourColBannerComponent : ViewComponent
    {
        private readonly ISuppService _suppService;
        public FourColBannerComponent(ISuppService suppService)
        {
            _suppService = suppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Banner> banners = await _suppService.GetBannersAsync();
            Banner banner = banners!.Where(x => x.ItemCount == 4 && x.IsActive).ToList().FirstOrDefault()!;
            return await Task.FromResult(View("/Pages/Components/_Get4ColBanner.cshtml",banner));
        }
    }
}
