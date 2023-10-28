using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;

namespace Hoozad.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISuppService _suppService;
        private readonly IStoreService _storeService;
        private IUserService _userService;

        public IndexModel(ISuppService suppService, IStoreService storeService, IUserService userService)
        {
            _suppService = suppService;
            _storeService = storeService;
            _userService = userService;
        }
        public bool Exist4Banner { get; set; }
        public bool Exist2Banner { get; set; }
        public bool ExistNewProducts { get; set; }
        public bool ExistBestSaleProducts { get; set; }
        public bool ExistSuggestedProducts { get; set; }
        public bool ExistPopulareProducts { get; set; }
        public bool ExistBestProducts { get; set; }
        public bool ExistSeasonProducts { get; set; }
        public bool ExistBlogs { get; set; }
        
        public async Task OnGet()
        {
            Exist4Banner = await _suppService.ExistFourBannerAsync();
            Exist2Banner = await _suppService.ExistTwoBannerAsync();
            ExistNewProducts = await _storeService.ExistNewProductsAsync();
            ExistBestSaleProducts = await _storeService.ExistBestSellingProductsAsync();
            ExistSuggestedProducts = await _storeService.ExistSuggestedProductsAsync();
            ExistBestProducts = await _storeService.ExistBestProductsAsync();
            ExistPopulareProducts = await _storeService.ExistPopulareProductsAsync();
            ExistSeasonProducts = await _storeService.ExistSeasonProductsAsync();
            ExistBlogs = await _suppService.ExistAnyBlogAsync();

            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("x-api-key", "VBce8m4NreqXhRfWBFLQR7PsM38FoDkHALPgiZS4SMpBkxABcNeErp70HGhHpdtJ");
            //var payload = @"{" + "\n" +
            //@" ""lineNumber"": 30007732005495" + "\n" +
            //@" ""messageTexts"": [" + "\n" +
            //@" ""Your Text 1""" + "\n" +
            //@" ]," + "\n" +
            //@" ""mobiles"": [" + "\n" +
            //@" ""09126617096""" + "\n" +
            //@" ]," + "\n" +
            //@" ""sendDateTime"": null" + "\n" +
            //@"}";
            //HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            //var response = await httpClient.PostAsync("https://api.sms.ir/v1/send/likeToLike", content);
            //var result = await response.Content.ReadAsStringAsync();
        }
        public async Task<IActionResult> OnPostAddCellphone(string cellphone)
        {
            if (string.IsNullOrEmpty(cellphone))
            {
                return Page();
            }
            _suppService.CreateCellphoneBank(cellphone);
            await _suppService.SaveChangesAsync();
            TempData["saveCell"] = "yes";
            return RedirectToPage("Index");


        }
    }
}