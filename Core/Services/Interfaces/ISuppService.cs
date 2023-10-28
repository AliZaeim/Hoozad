using DataLayer.Entities.Blogs;
using DataLayer.Entities.Supplementary;

namespace Core.Services.Interfaces
{
    public interface ISuppService
    {
        #region General
        void SaveChanges();
        Task SaveChangesAsync();
        Task<SiteInfo> GetLastSiteInfoAsync();
        void CreateCellphoneBank(string cellphone);
        
        #endregion General
        #region Slider
        Task<List<Slider>> GetSlidersAsync();
        Task<Slider> GetSliderById(int id);
        void CreateSlider(Slider slider);
        void UpdateSlider(Slider slider);
        Task DeleteSlider(int id);
        bool ExistSlider(int id);
        #endregion Slider
        #region Banner
        void CreateBanner(Banner Banner);
        void UpdateBanner(Banner Banner);
        void DeleteBanner(Banner Banner);
        Task<List<Banner>> GetBannersAsync();
        Task<Banner> GetBannerById(int id);
        bool ExistBanner(int id);
        Task<bool> ExistFourBannerAsync();
        Task<bool> ExistTwoBannerAsync();
        Task<bool> ExistColBannerAsync(int? count = 1,int? priority = 1);
        #endregion
        #region BannerItem
        void CreateBannerItem(BannerItem BannerItem);
        void UpdateBannerItem(BannerItem BannerItem);
        void DeleteBannerItem(BannerItem BannerItem);
        bool ExistBannerItem(int id);
        Task<BannerItem> GetBannerItemByIdAsync(int id);
        Task<List<BannerItem>> GetBannerItemsAsync();
        #endregion BannerItem
        #region Blog
        void CreateBlog(Blog blog);
        void UpdateBlog(Blog blog);
        void DeleteBlog(Blog blog);
        bool ExistBlog(Guid id);
        Task<List<Blog>> GetBlogsAsync();
        Task<Blog> GetBlogById(Guid id);
        Task<Blog> GetBlogByCodeAsync(string code);
        Task<bool> ExistAnyBlogAsync();
        #endregion Blog
        #region BlogGroup
        void CreateBlogGroup(BlogGroup blogGroup);
        void UpdateBlogGroup(BlogGroup blogGroup);
        void DeleteBlogGroup(BlogGroup blogGroup);
        Task<List<BlogGroup>> GetBlogGroupsAsync();
        Task<BlogGroup> GetBlogGroupByIdAsync(int id);
        bool ExistBlogGroup(int id);
        Task<BlogGroup> GetBlogGroupByNameAsync(string name);
        #endregion BlogGroup
        #region ContactMessage
        void CreateContactMessage(ContactMessage contactMessage);
        void UpdateContactMessage(ContactMessage contactMessage);
        void DeleteContactMessage(ContactMessage contactMessage);
        bool ExistContactMessage(int id);
        Task<List<ContactMessage>> GetContactMessagesAsync();
        Task<ContactMessage> GetContactMessageByIdAsync(int id);
        #endregion ContactMessage
        #region State
        void CreateState(State state);
        void UpdateState(State state);
        void DeleteState(State state);
        Task<List<State>> GetStatesAsync();
        Task<State?> GetStateById(int id);
        bool ExistState(int id);
        #endregion
        #region County
        void CreateCounty(County County);
        void UpdateCounty(County County);
        void DeleteCounty(County County);
        Task<List<County>> GetCountysAsync();
        Task<County?> GetCountyById(int id);
        bool ExistCounty(int id);
        Task<List<County>> GetCountiesOfState(int stateId);
        #endregion


    }
}
