using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.Supplementary;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Core.Services
{
    public class SuppService : ISuppService
    {
        private readonly MyContext _context;
        public SuppService(MyContext context)
        {
            _context = context;
        }
        #region Generic
        public void SaveChanges()
        {
           _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<SiteInfo> GetLastSiteInfoAsync()
        {
            return await _context.SiteInfos!.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync() ?? new SiteInfo();
        }
        public void CreateCellphoneBank(string cellphone)
        {
            CellphoneBank cellphoneBank = new()
            {
                Cellphone = cellphone,
                RegDate = DateTime.Now
            };
            _context.CellphoneBanks.Add(cellphoneBank);
        }
        #endregion Generic
        #region Slider
        public void CreateSlider(Slider slider)
        {
            _context.Sliders.Add(slider);
        }

        public async Task DeleteSlider(int id)
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            Slider slider = sliders.SingleOrDefault(x => x.Id == id)!;
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
            }           
        }

        public bool ExistSlider(int id)
        {
            return _context.Sliders.Any(x => x.Id == id);
        }

        public async Task<Slider> GetSliderById(int id)
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            return sliders!.SingleOrDefault(x => x.Id == id)!;
        }

        public async Task<List<Slider>> GetSlidersAsync()
        {
            return await _context.Sliders.ToListAsync();
        }
        public void UpdateSlider(Slider slider)
        {
            _context.Sliders.Update(slider);
        }


        #endregion Slider
        #region Banner
        public void CreateBanner(Banner Banner)
        {
            _context.Banners.Add(Banner);
        }

        public void UpdateBanner(Banner Banner)
        {
            _context.Banners.Update(Banner);
        }

        public void DeleteBanner(Banner Banner)
        {
            _context.Banners.Remove(Banner);
        }

        public async Task<List<Banner>> GetBannersAsync()
        {
            return await _context.Banners.Include(x => x.BannerItems).ToListAsync();
        }

        public async Task<Banner> GetBannerById(int id)
        {
            List<Banner> banners = await _context.Banners.Include(x => x.BannerItems).ToListAsync();
            return banners!.SingleOrDefault(x => x.Id == id)!;
        }

        public bool ExistBanner(int id)
        {
            return _context.Banners.Any(x => x.Id == id);
        }

        public void CreateBannerItem(BannerItem BannerItem)
        {
            _context.BannerItems.Add(BannerItem);
        }

        public void UpdateBannerItem(BannerItem BannerItem)
        {
            _context.BannerItems.Update(BannerItem);
        }

        public void DeleteBannerItem(BannerItem BannerItem)
        {
            _context.BannerItems.Remove(BannerItem);
        }

        public bool ExistBannerItem(int id)
        {
            return _context.BannerItems.Any(x => x.Id == id);
        }

        public async Task<BannerItem> GetBannerItemByIdAsync(int id)
        {
            List<BannerItem> bannerItems = await _context.BannerItems.Include(x => x.Banner).ToListAsync();
            return bannerItems!.SingleOrDefault(x => x.Id == id)!;
        }

        public async Task<List<BannerItem>> GetBannerItemsAsync()
        {
            return await _context.BannerItems.Include(x => x.Banner).ToListAsync();
        }

        public async Task<bool> ExistFourBannerAsync()
        {
            return await _context.Banners.Where(w => w.ItemCount == 4 && w.IsActive).AnyAsync();
        }

        public async Task<bool> ExistTwoBannerAsync()
        {
            return await _context.Banners.Where(w => w.ItemCount == 2 && w.IsActive).AnyAsync();
        }
        #endregion Banner
        #region Blog
        public void CreateBlog(Blog blog)
        {
            _context.Blogs!.Add(blog);
        }

        public void UpdateBlog(Blog blog)
        {
            _context.Blogs!.Update(blog);
        }

        public void DeleteBlog(Blog blog)
        {
            _context.Blogs!.Remove(blog);
        }

        public bool ExistBlog(Guid id)
        {
            return _context.Blogs!.Any(x => x.BlogId == id);
        }

        public async Task<List<Blog>> GetBlogsAsync()
        {
            return await _context.Blogs!.Include(x => x.BlogGroup).Include(x => x.BlogComments).ToListAsync();
        }

        public async Task<Blog> GetBlogById(Guid id)
        {
            List<Blog> blogs = await _context.Blogs!.Include(x => x.BlogGroup).Include(x => x.BlogComments).ToListAsync();
            return blogs.SingleOrDefault(x => x.BlogId == id)!;
        }
        public async Task<Blog> GetBlogByCodeAsync(string code)
        {
            List<Blog> blogs = await _context.Blogs!.Include(x => x.BlogGroup).Include(x => x.BlogComments).ToListAsync();
            return blogs.SingleOrDefault(x => x.BlogUrl == code)!;
        }
        public async Task<bool> ExistAnyBlogAsync()
        {
            return await _context.Blogs!.Where(w => w.BlogIsActive).AnyAsync();
        }
        #endregion Blog
        #region BlogGroup
        public void CreateBlogGroup(BlogGroup blogGroup)
        {
            _context.BlogGroups!.Add(blogGroup);
        }

        public void UpdateBlogGroup(BlogGroup blogGroup)
        {
            _context.BlogGroups!.Update(blogGroup);
        }

        public void DeleteBlogGroup(BlogGroup blogGroup)
        {
            _context.BlogGroups!.Remove(blogGroup);
        }

        public async Task<List<BlogGroup>> GetBlogGroupsAsync()
        {
            return await _context.BlogGroups!.Include(x => x.Blogs).ToListAsync();
        }

        public async Task<BlogGroup> GetBlogGroupByIdAsync(int id)
        {
            List<BlogGroup> blogGroups = await _context.BlogGroups!.Include(x => x.Blogs).ToListAsync();
            return blogGroups!.SingleOrDefault(x => x.BlogGroupId == id)!;
        }

        public bool ExistBlogGroup(int id)
        {
            return _context.BlogGroups!.Any(x => x.BlogGroupId == id);
        }

        public async Task<BlogGroup> GetBlogGroupByNameAsync(string name)
        {
            List<BlogGroup> blogGroups =await _context.BlogGroups!.Include(x => x.Blogs).ToListAsync();
            return blogGroups.SingleOrDefault(x => x.BlogGroupEnTitle == name)!;
        }
        #endregion BlogGroup
        #region ContactMessage
        public void CreateContactMessage(ContactMessage contactMessage)
        {
            _context.ContactMessages.Add(contactMessage);
        }

        public void UpdateContactMessage(ContactMessage contactMessage)
        {
            _context.ContactMessages.Update(contactMessage);
        }

        public void DeleteContactMessage(ContactMessage contactMessage)
        {
            _context.ContactMessages.Remove(contactMessage);
        }

        public bool ExistContactMessage(int id)
        {
            return _context.ContactMessages.Any(x => x.Id == id);
        }

        public async Task<List<ContactMessage>> GetContactMessagesAsync()
        {
            return await _context.ContactMessages.ToListAsync();
        }

        public async Task<ContactMessage> GetContactMessageByIdAsync(int id)
        {
            List<ContactMessage> contactMessages = await _context.ContactMessages.ToListAsync();
            return contactMessages.SingleOrDefault(x => x.Id == id)!;
        }
        #endregion ContactMessage
        #region State_and_county
        public void CreateState(State state)
        {
            _context.States!.Add(state);
        }

        public void UpdateState(State state)
        {
            _context.States!.Update(state);
        }

        public void DeleteState(State state)
        {
            _context.States!.Remove(state);
        }

        public async Task<List<State>> GetStatesAsync()
        {
            return await _context.States!.Include(_ => _.Counties).ToListAsync();
        }

        public async Task<State?> GetStateById(int id)
        {
            return await _context.States!.Include(_ => _.Counties).SingleOrDefaultAsync(_ => _.StateId == id);
        }

        public bool ExistState(int id)
        {
            return _context.States!.Any(_ => _.StateId == id);
        }

        public void CreateCounty(County County)
        {
            _context.Counties!.Add(County);
        }

        public void UpdateCounty(County County)
        {
            _context.Counties!.Update(County);
        }

        public void DeleteCounty(County County)
        {
            _context.Counties!.Remove(County);
        }

        public async Task<List<County>> GetCountysAsync()
        {
            return await _context.Counties!.Include(_ => _.State).ToListAsync(); 
        }

        public async Task<County?> GetCountyById(int id)
        {
            return await _context.Counties!.Include(_ => _.State).SingleOrDefaultAsync(_ => _.CountyId == id);
        }

        public bool ExistCounty(int id)
        {
            return _context.Counties!.Any(_ => _.CountyId == id);
        }

        public async Task<List<County>> GetCountiesOfState(int stateId)
        {
            State? state= await _context.States!.Include(_ =>_.Counties).SingleOrDefaultAsync(_ => _.StateId == stateId);
            return state!.Counties.ToList();
        }

        public async Task<bool> ExistColBannerAsync(int? count = 1, int? priority = 1)
        {
            return await _context.Banners.Where(w => w.ItemCount == count && w.IsActive && w.ShowPriority == priority).AnyAsync();
        }
        #endregion

    }
}
