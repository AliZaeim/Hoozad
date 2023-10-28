using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class HeaderComponent : ViewComponent
    {
        private readonly IStoreService _storeService;
        public HeaderComponent(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SiteInfo? siteInfo = await _storeService.GetLastSiteInfoAsync();
            return await Task.FromResult(View("/Pages/Components/_GetHeader.cshtml",siteInfo));
        }
    
    }
}
