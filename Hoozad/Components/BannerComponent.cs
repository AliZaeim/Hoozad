using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class BannerComponent : ViewComponent
    {
        private readonly ISuppService _suppService;
        public BannerComponent(ISuppService suppService)
        {
            _suppService = suppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? count = 1,int? priority = 1)
        {
            List<Banner> banners = await _suppService.GetBannersAsync();
            Banner banner = banners!.Where(x => x.ItemCount == count && x.IsActive && x.ShowPriority == priority).ToList().FirstOrDefault()!;
            return await Task.FromResult(View("/Pages/Components/_GetBanner.cshtml", banner));
        }
    }
}
