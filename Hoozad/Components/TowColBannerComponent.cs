using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class TowColBannerComponent : ViewComponent
    {
        private readonly ISuppService _suppService;
        public TowColBannerComponent(ISuppService suppService)
        {
            _suppService = suppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? priority = 1)
        {
            List<Banner> banners = await _suppService.GetBannersAsync();
            Banner banner = banners!.Where(x => x.ItemCount == 2 && x.IsActive && x.ShowPriority == priority).ToList().FirstOrDefault()!;
            return await Task.FromResult(View("/Pages/Components/_Get2ColBanner.cshtml",banner));
        }
    }
}
