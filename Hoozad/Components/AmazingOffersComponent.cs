using Microsoft.AspNetCore.Mvc;

namespace Web.Components
{
    public class AmazingOffersComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View("/Pages/Components/_GetAmazingOffers.cshtml"));
        }
    }
}
