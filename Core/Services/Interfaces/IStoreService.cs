using Core.DTOs.Admin;
using Core.DTOs.General;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;

namespace Core.Services.Interfaces
{
    public interface IStoreService
    {
        #region Generic
        Task SaveChangesAsync();
        void SaveChanges();
        Task<List<State>> GetStatesAsync();
        Task<List<County>> GetCountsAsync();
        Task<List<County>> GetCountiesofState(int stateId); 
        Task<User> GetUserByUserNameAsync(string userName);
        Task<State> GetStateByIdAsync(int stateId);
        Task<County> GetCountyByIdAsync(int countyId);
        (bool IsSuccess, string Content) GetNextPayToken(int Amount, string OrderNumber, string CustomerCellphone, string BackUrl, string Currency);
        Task<List<CellphoneBank>> GetCellphoneBanksAsync();
        #endregion
        #region ProductGroup
        Task<List<ProductGroup>> GetProductGroupsAsync();
        Task<ProductGroup> GetProductGroupAsync(int id);
        void CreateProductGroup(ProductGroup productGroup);
        void UpdateProductGroup(ProductGroup productGroup);
        void DeleteProductGroup(ProductGroup productGroup);
        bool ExistProductGroup(int id);
        #endregion
        #region Product
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(Guid id);
        Task<Product> GetProductByCodeAsync(string Code);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        bool ExistProduct(Guid id);
        Task<bool> ExistProductByCodeAsync(string productCode);
        Task<(bool Res, string Message)> CreateProductPrice(ProductPriceViewModel productPriceViewModel);
        void UpdateProductPrice(ProductPrice productPrice);
        void DeleteProductPrice(ProductPrice productPrice);
        Task<ProductPrice> GetProductPriceById(int id);
        Task<(int Price,int NetPrice,int DiscountVal,int DiscountPercent,bool Exist)> GetProductFinanceInfoAsync(Guid id);
        Task<List<(int Height,int Price, int NetPrice, int DiscountVal, int DiscountPercent)>> GetProductPricesFinanceAsync(string productCode);
        Task<string> GenerateProductCode(int PgroupId);
        Task<bool> ExistNewProductsAsync();
        Task<bool> ExistBestSellingProductsAsync();
        Task<bool> ExistSuggestedProductsAsync();
        Task<bool> ExistPopulareProductsAsync();
        Task<bool> ExistBestProductsAsync();
        Task<bool> ExistSeasonProductsAsync();
        Task<(int Price, int NetPrice, int DiscountValue, int? DiscountPercent,bool Exist)> GetProductPriceFacilityData(string PrCode, int? Size, int? Height);
        Task<bool> CheckAllowSingleSell(string productCode, string CartId);
        Task<List<Product>?> GetCommonProductsofThisAsync(string prCode);
        #endregion Product
        #region ProductColor
        Task<List<ProductColor>> GetProductColorsByProductIdAsync(Guid pId);
        Task<List<ProductColor>> GetProductColorsByProductCodeAsync(string pCode);
        Task<ProductColor> GetProductColorByIdAsync(int id);
        void CreateProductColor(ProductColor productColor);
        void UpdateProductColor(ProductColor productColor);
        void DeleteProductColor(ProductColor productColor);
        #endregion ProductColor
        #region ProductSize
        Task<List<ProductSize>> GetProductSizesByProductIdAsync(Guid pId);
        Task<List<ProductSize>> GetProductSizesByProductCodeAsync(string pCode);
        Task<List<ProductSize>> GetProductSizesAsync();
        Task<ProductSize> GetProductSizeByIdAsync(int id);
        void CreateProductSize(ProductSize productSize);
        void UpdateProductSize(ProductSize productSize);
        void DeleteProductSize(ProductSize productSize);
        Task<List<int?>> GetSizesByProdoctId(Guid prodoctId);
        #endregion ProductSize
        #region ProductComponent
        Task<List<ProductComponent>> GetProductComponentsByProductIdAsync(Guid pId);
        Task<List<ProductComponent>> GetProductComponentsByProductCodeAsync(string pCode);
        Task<List<ProductComponent>> GetProductComponentsAsync();
        Task<ProductComponent?> GetProductComponentByIdAsync(int id);
        void CreateProductComponent(ProductComponent productComponent);
        void UpdateProductComponent(ProductComponent productComponent);
        void DeleteProductComponent(ProductComponent productComponent);
        #endregion ProductComponent
        #region SiteInfo
        Task<List<SiteInfo>> GetSiteInfosAsync();
        Task<SiteInfo> GetSiteInfoAsync(int id);
        void CreateSiteInfo(SiteInfo siteInfo);
        void UpdateSiteInfo(SiteInfo siteInfo);
        void DeleteSiteInfo(SiteInfo siteInfo);
        bool ExistSiteInfo(int id);
        Task<SiteInfo> GetLastSiteInfoAsync();
        #endregion SiteInfo
        #region Cart
        Task<(Cart cart, string Result)> AddToCartOp(AddToCartVM addToCartVM);
        Task<(Cart cart, string Result)> AddToCart(AddToCartVM addToCartVM);
        Task<Cart> GetCartByIdAsync(Guid id);
        Task<Cart> GetCartByIdAsync(string id);
        Task RemoveItemFromCart(string cartId, int itemId);
        Task<List<CartItem>> GetCartItemsofCart(string cartId);
        Task<string> UpdateCartItemQuantity(int cartitemId, int count);
        string UpdateCartItem(CartItem cartItem);
        Task<CartItem> GetCartItemByIdAsync(int id);
        Task<(bool HasTwoStagePay, int TwoStageValue)> GetTwoStageInfoAsync(string cartId);
        void UpdateCart(Cart cart);
        Task<List<Cart>> GetCartsAsOrders();
        Task<List<CartItem>> GetOrdersCartItemsForLogin(string UserNameorCellPhone);

        Task<(bool prInfoCanRemove,bool wasRemoved, string Message,List<int> ProductsCIMustBeRemoved)> Check_prInfoDeletionStatusAsync(int? prInfoId,string cartId);
        Task<(bool prInfoCanRemove, bool wasRemoved, string Message, List<int> ProductsCIMustBeRemoved)> Check_cartItemDeletionStatusAsync(int? ciId, string cartId);
        Task<Cart?> GetCartByOrderNumberAsync(string orderNumber);
        #endregion Cart
        #region WareHouse
        void CreateWareHouse(WareHouse wareHouse);
        void UpdateWareHouse(WareHouse wareHouse);
        void DeleteWareHouse(WareHouse wareHouse);
        Task<WareHouse> GetWareHouseByIdAsync(int id);
        Task<List<WareHouse>> GetWareHousesAsync();
        bool ExistWareHouse(int id);
        Task<int> GetProductWareHouseRemainAsync(string productCode);
        #endregion
        #region Reports
        Task<List<Cart>> GetCartsAsync();
        #endregion
        #region DiscountCoupen
        Task<DiscountCoupen?> GetDiscountCoupenByCodeAsync(string Code);
        Task<(bool Validity, List<(string Message,string Note)> Messages)> CheckCoupenByCodeAsync(string Code,string UserName);
        #endregion
        #region Status
        Task<List<Status>> GetStatusesAsync();
        Task<(bool,string,string)> AddStatusToOrderAsync(AddStatusVM addStatusVM);
        Task<CartStatus?> GetCartStatusbyIdAsync(int id);
        Task<List<CartStatus>> GetCartStatusesByCartIdAsync(Guid crtid);
        Task<CartStatus?> GetCartLastStatusAsync(Guid crtid);
        void UpdateStatus(CartStatus cartStatus);
        Task<Status?> GetStatusByIdAsync(int id);
        #endregion Status
        #region ProductPrice
        Task<ProductPrice?> GetProductPriceBySizeandHeightAsync(int? Height, int? Size);
        Task<bool> AllowedEdit(int ProductPriceId,int? Height, int? Size);
        #endregion
        #region ProductInfo 
        
        /// <summary>
        /// چک وضعیت فروش تکی محصولات سبد خرید
        /// اگر همه مجوز فروش تکی داشته باشند true 
        /// و در غیر اینصورت false
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Task<bool> CheckAllProductOfCartIsSingleSellAsync(Guid cartId);
        Task<List<Product>> GetProductsofCartAsync(Guid cartId);
        Task<bool> RemoveCIFromCartAsync(string cartId, int itemId);
        Task<bool> RemoveCIsFromCartAsync(string cartId, List<int> cisId);
        
        Task<List<CartProductInfo>> GetProductInfosofCartItemAsync(int ciId);

        #endregion


    }
}
