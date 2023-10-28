using Azure.Core;
using Core.Convertors;
using Core.DTOs.Admin;
using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace Core.Services
{
    public class StoreService : IStoreService
    {
        private readonly MyContext _context;
        public StoreService(MyContext context)
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
        public async Task<List<State>> GetStatesAsync()
        {
            return await _context.States!.Include(x => x.Counties).ToListAsync();
        }
        public async Task<List<County>> GetCountsAsync()
        {
            return await _context.Counties!.Include(x => x.State).ToListAsync();
        }
        public async Task<List<County>> GetCountiesofState(int stateId)
        {
            List<County> counties = await _context.Counties!.Include(x => x.State).ToListAsync();
            return counties.Where(w => w.StateId == stateId).ToList();
        }
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            List<User> users = await _context.Users!.Include(x => x.UserRoles).ToListAsync();
            return users.SingleOrDefault(x => x.UserName == userName)!;
        }
        public async Task<State> GetStateByIdAsync(int stateId)
        {
            List<State> states = await _context.States!.Include(x => x.Counties).ToListAsync();
            return states.SingleOrDefault(x => x.StateId == stateId)!;
        }
        public async Task<County> GetCountyByIdAsync(int countyId)
        {
            List<County> counties = await _context.Counties!.Include(x => x.State).ToListAsync();
            return counties.SingleOrDefault(x => x.CountyId == countyId)!;
        }
        public (bool IsSuccess, string Content) GetNextPayToken(int Amount, string OrderNumber, string CustomerCellphone, string BackUrl, string Currency)
        {
            string url = "https://nextpay.org/nx/gateway/token";
            string cur = Currency;
            if (!string.IsNullOrEmpty(Currency))
            {
                if (Currency == "تومان")
                {
                    cur = "IRT";
                }
                if (Currency == "ریال")
                {
                    cur = "IRR";
                }
            }
            RestClient client = new(url);
            RestRequest request = new(url, Method.Post);
            _ = request.AddParameter("Content-Type", "application/x-www-form-urlencoded");
            _ = request.AddParameter("api_key", "a95c372d-2ab8-470f-a6cf-c6ffe2feb9ba");
            _ = request.AddParameter("amount", Amount.ToString());
            _ = request.AddParameter("order_id", OrderNumber);
            _ = request.AddParameter("customer_phone", CustomerCellphone);
            _ = request.AddParameter("currency", cur);
            _ = request.AddParameter("auto_verify", "yes");            
            _ = request.AddParameter("callback_uri", BackUrl);
            RestResponse response = client.Execute(request);
            return (response.IsSuccessful, response.Content!);
        }
        #endregion
        #region ProductGroup
        public void CreateProductGroup(ProductGroup productGroup)
        {
            _context?.ProductGroups!.Add(productGroup);
        }

        public void DeleteProductGroup(ProductGroup productGroup)
        {
            _context?.ProductGroups!.Remove(productGroup);
        }

        public bool ExistProductGroup(int id)
        {
            return _context.ProductGroups!.Any(a => a.Id == id);
        }

        public async Task<ProductGroup> GetProductGroupAsync(int id)
        {
            List<ProductGroup> productGroups = await _context.ProductGroups!.Include(x => x.Products).ToListAsync();
            return productGroups.SingleOrDefault(x => x.Id == id)!;
        }

        public async Task<List<ProductGroup>> GetProductGroupsAsync()
        {
            return await _context!.ProductGroups!.Include(x => x.Products).ToListAsync();
        }



        public void UpdateProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups!.Update(productGroup);
        }
        #endregion ProductGroup
        #region Product
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products!.Include(x => x.ProductGroup).Include(x => x.ProductPrices).Include(x => x.ProductColors).Include(x => x.ProductComponents).Include(x => x.ProductSizes).ToListAsync();
        }
        public async Task<Product> GetProductAsync(Guid id)
        {
            List<Product> products = await _context.Products!.Include(x => x.ProductGroup).Include(x => x.ProductPrices).Include(x => x.ProductColors).Include(x => x.ProductSizes).Include(x => x.ProductComponents).ToListAsync();
            return products.SingleOrDefault(x => x.ProductId == id)!;
        }
        public void CreateProduct(Product product)
        {
            _context.Products!.Add(product);
        }
        public void UpdateProduct(Product product)
        {
            _context.Products!.Update(product);
        }
        public void DeleteProduct(Product product)
        {
            _context.Products!.Remove(product);
        }
        public bool ExistProduct(Guid id)
        {
            return _context.Products!.Any(x => x.ProductId == id);
        }
        public async Task<bool> ExistProductByCodeAsync(string productCode)
        {
            return await _context.Products!.AnyAsync(x => x.ProductCode == productCode);
        }
        public async Task<(bool Res, string Message)> CreateProductPrice(ProductPriceViewModel productPriceViewModel)
        {
            bool ExistHight = false; bool Result = false; string Message = string.Empty;
            if (productPriceViewModel.Height != null)
            {
                ExistHight = await _context.ProductPrices.AnyAsync(z => z.ProductId == productPriceViewModel.ProductId && z.Height == productPriceViewModel.Height.Value && z.Size == productPriceViewModel.Size);
            }
            if (!ExistHight)
            {
                ProductPrice productPrice = new()
                {
                    Size = productPriceViewModel.Size,
                    Price = productPriceViewModel.Price,
                    Height = productPriceViewModel.Height,
                    ProductId = productPriceViewModel.ProductId,
                };
                _context.ProductPrices.Add(productPrice);
                Result = true;
            }
            else
            {
                Message = "اطلاعات این سایز و قد قبلا ثبت شده است !";
            }
            return (Result, Message);
        }
        public void UpdateProductPrice(ProductPrice productPrice)
        {
            _context.ProductPrices.Update(productPrice);
        }
        public void DeleteProductPrice(ProductPrice productPrice)
        {
            _context.ProductPrices.Remove(productPrice);
        }
        public async Task<ProductPrice> GetProductPriceById(int id)
        {
            var productPrices = await _context.ProductPrices.Include(x => x.Product).ToListAsync();
            return productPrices.SingleOrDefault(x => x.Id == id)!;
        }
        public async Task<(int Price, int NetPrice, int DiscountVal, int DiscountPercent, bool Exist)> GetProductFinanceInfoAsync(Guid id)
        {
            (int price, int netPrice, int disValue, int disPercent, bool exist) Result = (0, 0, 0, 0, false);
            List<Product> products = await _context.Products!.Include(x => x.ProductGroup).Include(_ => _.ProductComponents).ToListAsync();
            Product product = products.SingleOrDefault(w => w.ProductId == id)!;
            List<WareHouse> wareHouses = new();

            if (product != null)
            {
                int? Remain = 0;
                if (product.ProductComponents.Count == 0)
                {
                    wareHouses = await _context.WareHouses.Where(w => w.ProductId == id).ToListAsync();
                    if (wareHouses.Any())
                    {
                        Remain = wareHouses.Sum(x => x.Input - x.Export);
                        if (Remain <= 0)
                        {
                            Result.exist = false;
                        }
                        else
                        {
                            Result.exist = true;
                        }
                    }
                }
                else
                {
                    bool ResExist = true;
                    foreach (var item in product.ProductComponents.Select(x => x.ProductCode).ToList())
                    {
                        //Product? CompPr = await _context.Products!.SingleOrDefaultAsync(_ => _.ProductCode == item!);

                        wareHouses = await _context.WareHouses.Where(w => w.Product!.ProductCode == item).ToListAsync();
                        if (wareHouses.Any() && ResExist == true)
                        {
                            Remain = wareHouses.Sum(x => x.Input - x.Export);
                            if (Remain <= 0)
                            {
                                Result.exist = false;
                                ResExist = false;

                            }
                            else
                            {
                                Result.exist = true;
                                ResExist = true;
                            }
                        }
                        else
                        {
                            Result.exist = false;
                            ResExist = false;
                        }
                    }
                }


                Result.price = product.ProductPrice.GetValueOrDefault();
                if (product.ProductValueDiscount.HasValue)
                {
                    Result.disValue = product.ProductValueDiscount.GetValueOrDefault();
                    Result.disPercent = (int)(decimal.Divide(product.ProductValueDiscount.GetValueOrDefault(), product.ProductPrice.GetValueOrDefault()) * 100);
                }
                if (product.ProductPercentDiscount.HasValue && product.ProductValueDiscount == null)
                {
                    Result.disPercent = (int)product.ProductPercentDiscount.GetValueOrDefault();
                    decimal mult = (decimal)(product.ProductPrice.GetValueOrDefault() * product.ProductPercentDiscount);
                    int discountValue = (int)decimal.Divide(mult, 100);
                    Result.disValue = discountValue;
                }
                if (product.ProductGroup != null)
                {
                    if (product.ProductGroup.DiscountValue != null)
                    {
                        Result.disValue = product.ProductGroup.DiscountValue.Value;
                        Result.disPercent = (int)(decimal.Divide(product.ProductGroup.DiscountValue.GetValueOrDefault(), product.ProductPrice.GetValueOrDefault()) * 100);
                    }
                    if (product.ProductGroup.DiscountPercent.HasValue && product.ProductGroup.DiscountValue == null)
                    {
                        Result.disPercent = product.ProductGroup.DiscountPercent.GetValueOrDefault();
                        decimal mult = product.ProductPrice.GetValueOrDefault() * product.ProductGroup.DiscountPercent.GetValueOrDefault();
                        int discountValue = (int)decimal.Divide(mult, 100);
                        Result.disValue = discountValue;
                    }
                }
                Result.netPrice = product.ProductPrice.GetValueOrDefault() - Result.disValue;
            }
            return Result;
        }
        public async Task<List<(int Height, int Price, int NetPrice, int DiscountVal, int DiscountPercent)>> GetProductPricesFinanceAsync(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                return null!;
            }
            List<(int Hght, int Prc, int NetPrc, int DisVal, int DisPer)> Fin = new();
            var products = await _context.Products!.Include(x => x.ProductGroup).Include(x => x.ProductPrices).ToListAsync();
            Product product = products.SingleOrDefault(w => w.ProductCode == productCode)!;
            if (product == null)
            {
                return null!;
            }
            foreach (var item in product.ProductPrices.ToList())
            {
                (int Hght, int price, int netPrice, int disValue, int disPercent) Result = new()
                {
                    price = product.ProductPrice.GetValueOrDefault(),
                    Hght = item.Height.GetValueOrDefault()
                };
                if (product.ProductValueDiscount.HasValue)
                {
                    Result.disValue = product.ProductValueDiscount.GetValueOrDefault();
                    Result.disPercent = (int)(decimal.Divide(product.ProductValueDiscount.GetValueOrDefault(), item.Price.GetValueOrDefault()) * 100);
                }
                if (product.ProductPercentDiscount.HasValue && product.ProductValueDiscount == null)
                {
                    Result.disPercent = product.ProductValueDiscount.GetValueOrDefault();
                    decimal mult = (decimal)(item.Price.GetValueOrDefault() * product.ProductPercentDiscount);
                    int discountValue = (int)decimal.Divide(mult, 100);
                    Result.disValue = discountValue;
                }
                if (product.ProductGroup != null)
                {
                    if (product.ProductGroup.DiscountValue != null)
                    {
                        Result.disValue = product.ProductGroup.DiscountValue.Value;
                        Result.disPercent = (int)(decimal.Divide(product.ProductGroup.DiscountValue.GetValueOrDefault(), item.Price.GetValueOrDefault()) * 100);
                    }
                    if (product.ProductGroup.DiscountPercent.HasValue && product.ProductGroup.DiscountValue == null)
                    {
                        Result.disPercent = product.ProductGroup.DiscountPercent.GetValueOrDefault();
                        decimal mult = item.Price.GetValueOrDefault() * product.ProductGroup.DiscountPercent.GetValueOrDefault();
                        int discountValue = (int)decimal.Divide(mult, 100);
                        Result.disValue = discountValue;
                    }
                }
                Result.netPrice = item.Price.GetValueOrDefault() - Result.disValue;
                Result.price = item.Price.GetValueOrDefault();
                Fin.Add(Result);
            }
            return Fin;
        }
        public async Task<Product> GetProductByCodeAsync(string Code)
        {
            List<Product> products = await _context.Products!.Include(x => x.ProductGroup).Include(x => x.ProductColors).Include(x => x.ProductComponents).Include(x => x.ProductPrices).Include(_ => _.ProductSizes).ToListAsync();
            return products.SingleOrDefault(x => x.ProductCode == Code)!;
        }
        public async Task<string> GenerateProductCode(int PgroupId)
        {
            List<Product> products = await _context.Products!.Include(x => x.ProductGroup).Where(w => w.ProductGroupId == PgroupId).ToListAsync();
            List<ProductGroup> productGroups = await _context.ProductGroups!.ToListAsync();
            ProductGroup productGroup = productGroups.SingleOrDefault(x => x.Id == PgroupId)!;
            //Manto-Pants-Coat-Scarf-Shomiz
            if (productGroup == null)
            {
                return string.Empty;
            }
            string Name = productGroup?.EnTitle!;
            Random rnd = new();
            int nmb = rnd.Next();
            Name += nmb.ToString();
            while (products.Any(a => a.ProductCode == Name))
            {
                nmb = rnd.Next();
                Name += nmb.ToString();
            }
            return Name;
        }
        public async Task<bool> ExistNewProductsAsync()
        {
            List<Product> products = await _context.Products!.ToListAsync();
            return products.Where(w => w.TagsList.Any(z => z.Trim() == "جدید")).Any();
        }
        public async Task<bool> ExistBestSellingProductsAsync()
        {
            List<Product> products = await _context.Products!.ToListAsync();
            return products.Where(w => w.TagsList.Any(z => z.Trim() == "پرفروش" || z.Trim() == "پر فروش")).Any();
        }
        public async Task<bool> ExistSuggestedProductsAsync()
        {
            List<Product> products = await _context.Products!.ToListAsync();
            return products.Where(w => w.TagsList.Any(z => z.Trim() == "پیشنهادی")).Any();
        }
        public async Task<bool> ExistPopulareProductsAsync()
        {
            List<Product> products = await _context.Products!.ToListAsync();
            return products.Where(w => w.TagsList.Any(z => z.Trim() == "محبوب")).Any();
        }

        public async Task<bool> ExistBestProductsAsync()
        {
            List<Product> products = await _context.Products!.ToListAsync();
            return products.Where(w => w.TagsList.Any(z => z.Trim() == "برتر")).Any();
        }

        public async Task<bool> ExistSeasonProductsAsync()
        {
            List<Product> products = await _context.Products!.ToListAsync();
            return products.Where(w => w.TagsList.Any(z => z.Trim() == "فصل")).Any();
        }
        #endregion Product
        #region SiteInfo
        public async Task<List<SiteInfo>> GetSiteInfosAsync()
        {
            return await _context.SiteInfos!.ToListAsync();
        }

        public async Task<SiteInfo> GetSiteInfoAsync(int id)
        {
            List<SiteInfo> siteInfos = await _context.SiteInfos!.ToListAsync();
            return siteInfos.SingleOrDefault(x => x.Id == id)!;
        }

        public void CreateSiteInfo(SiteInfo siteInfo)
        {
            _context.SiteInfos!.Add(siteInfo);
        }

        public void UpdateSiteInfo(SiteInfo siteInfo)
        {
            _context.SiteInfos!.Update(siteInfo);
        }

        public void DeleteSiteInfo(SiteInfo siteInfo)
        {
            _context.SiteInfos!.Remove(siteInfo);
        }

        public bool ExistSiteInfo(int id)
        {
            return _context.SiteInfos!.Any(x => x.Id == id);
        }
        public async Task<SiteInfo> GetLastSiteInfoAsync()
        {
            return await _context.SiteInfos!.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync() ?? new SiteInfo();
        }
        #endregion SiteInfo
        #region ProductColor
        public async Task<List<ProductColor>> GetProductColorsByProductIdAsync(Guid pId)
        {
            return await _context.ProductColors.Include(x => x.Product).Where(w => w.ProductId == pId).ToListAsync();
        }
        public async Task<List<ProductColor>> GetProductColorsByProductCodeAsync(string pCode)
        {
            return await _context.ProductColors.Include(x => x.Product).Where(w => w.Product!.ProductCode == pCode).ToListAsync();
        }
        public async Task<ProductColor> GetProductColorByIdAsync(int id)
        {
            List<ProductColor> productColors = await _context.ProductColors.Include(x => x.Product).ToListAsync();
            return productColors.SingleOrDefault(x => x.Id == id)!;
        }
        public void CreateProductColor(ProductColor productColor)
        {
            _context.ProductColors.Add(productColor);
        }
        public void UpdateProductColor(ProductColor productColor)
        {
            _context.ProductColors.Update(productColor);
        }
        public void DeleteProductColor(ProductColor productColor)
        {
            _context.ProductColors.Remove(productColor);
        }
        #endregion ProductColor
        #region Cart
        public async Task<(Cart cart, string Result)> AddToCartOp(AddToCartVM addToCartVM)
        {
            Cart? cart = null;
            string state = "no";
            int? PSize = null; int? PHeight = null;
            if (!string.IsNullOrEmpty(addToCartVM.prSize))
            {
                PSize = int.Parse(addToCartVM.prSize);
            }
            if (!string.IsNullOrEmpty(addToCartVM.prHeight))
            {
                PHeight = int.Parse(addToCartVM.prHeight);
            }
            var (Price, NetPrice, DiscountValue, DiscountPercent, Exist) = await GetProductPriceFacilityData(addToCartVM.ProductCode!, PSize, PHeight);
            if (addToCartVM.CartId.HasValue)
            {
                cart = await _context.Carts!.Include(x => x.CartItems).SingleOrDefaultAsync(x => x.Id == addToCartVM.CartId!.GetValueOrDefault()) ?? null;
                if (cart != null)
                {
                    if (!cart.CheckOut && cart.IsActive)
                    {
                        if (cart.CartItems.Any(z => z.ProductCode == addToCartVM.ProductCode))
                        {
                            CartItem? cartitem = await _context.CartItems!.Include(_ => _.CartProductInfos).Include(_ => _.Cart).SingleOrDefaultAsync(x => x.ProductCode == addToCartVM.ProductCode);


                            if (cartitem != null)
                            {

                                cartitem.Quantity += addToCartVM.Quantity;
                                cartitem.Price = addToCartVM.Price;
                                if (cartitem.CartProductInfos.Any(x => x.Color == addToCartVM.prColor && x.Size == PSize && x.Height == PHeight))
                                {
                                    CartProductInfo? cartProductInfo = cartitem.CartProductInfos.SingleOrDefault(z => z.Color == addToCartVM.prColor && z.Size == PSize && z.Height == PHeight);
                                    if (cartProductInfo != null)
                                    {
                                        cartProductInfo.Quantity = addToCartVM.Quantity;
                                    }

                                }
                                else
                                {
                                    cartitem.CartProductInfos.Add(new()
                                    {
                                        Color = addToCartVM.prColor,
                                        Size = PSize,
                                        Height = PHeight,
                                        Quantity = addToCartVM.Quantity,
                                        Price = Price,
                                        NetPrice = NetPrice,
                                        RegDate = DateTime.Now

                                    });
                                }

                                _context.CartItems!.Update(cartitem);
                                await _context.SaveChangesAsync();
                                state = "yes";
                            }
                            else
                            {
                                CartItem cartItem = new()
                                {
                                    ProductCode = addToCartVM.ProductCode,
                                    Price = Price,
                                    NetPrice = NetPrice,
                                    Discount = Price,
                                    Comment = addToCartVM.Comment,
                                    Quantity = addToCartVM.Quantity
                                };

                                cartItem.CartProductInfos.Add(new()
                                {
                                    Color = addToCartVM.prColor,
                                    Size = PSize,
                                    Height = PHeight,
                                    Quantity = addToCartVM.Quantity,
                                    Price = Price,
                                    NetPrice = NetPrice,
                                    RegDate = DateTime.Now
                                });
                                cart.CartItems.Add(cartItem);
                                await _context.SaveChangesAsync();
                                state = "yes";
                            }
                        }
                        else
                        {
                            CartItem cartItem = new()
                            {
                                ProductCode = addToCartVM.ProductCode,
                                Price = Price,
                                NetPrice = NetPrice,
                                Discount = Price,
                                Comment = addToCartVM.Comment,
                                Quantity = addToCartVM.Quantity
                            };

                            cartItem.CartProductInfos.Add(new()
                            {
                                Color = addToCartVM.prColor,
                                Size = PSize,
                                Height = PHeight,
                                Quantity = addToCartVM.Quantity,
                                Price = Price,
                                NetPrice = NetPrice,
                                RegDate = DateTime.Now
                            });
                            cart.CartItems.Add(cartItem);
                            await _context.SaveChangesAsync();
                            state = "yes";
                        }
                    }
                }
                else
                {
                    SiteInfo siteInfo = await _context.SiteInfos!.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync() ?? throw new ArgumentException(nameof(SiteInfo));
                    cart = new()
                    {
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Currency = siteInfo?.SiteCurrency
                    };
                    CartItem cartItem = new()
                    {
                        ProductCode = addToCartVM.ProductCode,
                        Price = Price,
                        NetPrice = NetPrice,
                        Discount = DiscountValue,
                        Comment = addToCartVM.Comment,
                        Quantity = addToCartVM.Quantity,
                    };

                    cartItem.CartProductInfos.Add(new()
                    {
                        Color = addToCartVM.prColor,
                        Size = PSize,
                        Height = PHeight,
                        Price = Price,
                        NetPrice = NetPrice,
                        RegDate = DateTime.Now
                    });
                    cart.CartItems.Add(cartItem);
                    _context.Carts!.Add(cart);
                    await _context.SaveChangesAsync();
                    state = "yes";
                }

            }
            else
            {
                SiteInfo siteInfo = await _context.SiteInfos!.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync() ?? throw new ArgumentException(nameof(SiteInfo));
                cart = new()
                {
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Currency = siteInfo?.SiteCurrency
                };
                CartItem cartItem = new()
                {
                    ProductCode = addToCartVM.ProductCode,
                    Price = Price,
                    NetPrice = NetPrice,
                    Discount = DiscountValue,
                    Comment = addToCartVM.Comment,
                    Quantity = addToCartVM.Quantity,
                };

                cartItem.CartProductInfos.Add(new()
                {
                    Color = addToCartVM.prColor,
                    Size = PSize,
                    Height = PHeight,
                    Price = Price,
                    NetPrice = NetPrice,
                    RegDate = DateTime.Now
                });
                cart.CartItems.Add(cartItem);
                _context.Carts!.Add(cart);
                await _context.SaveChangesAsync();
                state = "yes";
            }
            return (cart!, state);
        }
        public async Task<(Cart cart, string Result)> AddToCart(AddToCartVM addToCartVM)
        {
            string Res = "no";
            Cart? cart = null;
            int? PSize = null; int? PHeight = null;
            Product? product = await _context.Products!.SingleOrDefaultAsync(_ => _.ProductCode == addToCartVM.ProductCode);

            if (!string.IsNullOrEmpty(addToCartVM.prSize))
            {
                PSize = int.Parse(addToCartVM.prSize);
            }
            if (!string.IsNullOrEmpty(addToCartVM.prHeight))
            {
                PHeight = int.Parse(addToCartVM.prHeight);
            }
            (int price, int netPrice, int disval, int? dispercent, bool exist) = await GetProductPriceFacilityData(addToCartVM.ProductCode!, PSize, PHeight);
            if (addToCartVM.CartId.HasValue)
            {
                cart = await _context.Carts!.Include(x => x.CartItems).SingleOrDefaultAsync(_ => _.Id == addToCartVM.CartId);
                if (cart != null)
                {
                    CartItem? cartItem = await _context.CartItems!.Include(_ => _.Cart).Include(_ => _.CartProductInfos).SingleOrDefaultAsync(_ => _.ProductCode == addToCartVM.ProductCode && _.CartId == cart.Id);
                    if (cartItem != null)
                    {
                        CartProductInfo? cartProductInfo = cartItem.CartProductInfos.SingleOrDefault(_ => _.Color == addToCartVM.prColor && _.Size == PSize && _.Height == PHeight);
                        if (cartProductInfo != null)
                        {
                            cartProductInfo.Quantity += addToCartVM.Quantity;
                            cartProductInfo.Price = addToCartVM.Price;
                            cartProductInfo.NetPrice = addToCartVM.NetPrice;
                            cartProductInfo.CartItem!.Cart!.CartSum += addToCartVM.Quantity * netPrice;
                            cartProductInfo.CartItemId = cartItem.Id;
                            cartItem.CartProductInfos.Add(cartProductInfo);
                            cartItem.Quantity += addToCartVM.Quantity;
                            cart.CartSum += cartItem.CartProductInfos.Count * netPrice;
                            cart.CartItems!.Add(cartItem);
                            _context.Carts!.Update(cart);
                            await _context.SaveChangesAsync();
                            Res = "yes";
                        }
                        else
                        {
                            CartProductInfo cartProductInfoNew = new()
                            {
                                RegDate = DateTime.Now,
                                Color = addToCartVM.prColor,
                                Size = PSize,
                                Height = PHeight,
                                NetPrice = netPrice,
                                Price = price,
                                Quantity = addToCartVM.Quantity
                            };
                            cartItem.Cart!.CartSum += addToCartVM.Quantity * netPrice;
                            cartItem.CartProductInfos.Add(cartProductInfoNew);
                            cartItem.Quantity += addToCartVM.Quantity;
                            cart.CartItems.Add(cartItem);
                            _context.Carts!.Update(cart);
                            await _context.SaveChangesAsync();
                            Res = "yes";
                        }
                    }
                    else
                    {

                        CartItem cartItemNew = new()
                        {
                            ProductCode = addToCartVM.ProductCode,
                            Price = price,
                            NetPrice = netPrice,
                            Discount = disval,
                            Comment = addToCartVM.Comment,
                            Quantity = addToCartVM.Quantity,
                        };
                        cartItemNew.CartProductInfos.Add(new()
                        {
                            RegDate = DateTime.Now,
                            Color = addToCartVM.prColor,
                            Size = PSize,
                            Height = PHeight,
                            NetPrice = netPrice,
                            Price = price,
                            Quantity = addToCartVM.Quantity
                        });
                        cart.CartSum += addToCartVM.Quantity * netPrice;
                        cart.CartItems.Add(cartItemNew);

                        _context.Carts!.Update(cart);
                        await _context.SaveChangesAsync();
                        Res = "yes";
                    }

                }
                else
                {
                    List<string?> orderNumbers = new();
                    if (await _context.Carts!.ToListAsync() != null)
                    {
                        orderNumbers = await _context.Carts!.Select(_ => _.OrderNumber).ToListAsync();
                    }
                    string neworderNumber = Prodocers.Generators.GenerateKey(orderNumbers.ToList()!, 8);
                    SiteInfo? siteInfo = await _context.SiteInfos!.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                    string cur = "ریال";
                    if (siteInfo != null)
                    {
                        cur = siteInfo.SiteCurrency!;
                    }

                    cart = new()
                    {
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        OrderNumber = neworderNumber,
                        CartSum = addToCartVM.Quantity * netPrice,
                        Currency = cur
                    };
                    CartItem cartItem = new()
                    {
                        ProductCode = addToCartVM.ProductCode,
                        Price = price,
                        NetPrice = netPrice,
                        Discount = disval,
                        Comment = addToCartVM.Comment,
                        Quantity = addToCartVM.Quantity,
                    };

                    cartItem.CartProductInfos.Add(new()
                    {
                        Color = addToCartVM.prColor,
                        Size = PSize,
                        Height = PHeight,
                        Price = price,
                        Quantity = addToCartVM.Quantity,
                        NetPrice = netPrice,
                        RegDate = DateTime.Now
                    });
                    //cart.CartSum = addToCartVM.Quantity * netPrice;
                    cart.CartItems.Add(cartItem);
                    _context.Carts!.Add(cart);
                    await _context.SaveChangesAsync();
                    Res = "yes";
                }
            }
            else
            {
                List<string?> orderNumbers = new();
                if (await _context.Carts!.ToListAsync() != null)
                {
                    orderNumbers = await _context.Carts!.Select(_ => _.OrderNumber).ToListAsync();
                }
                string neworderNumber = Prodocers.Generators.GenerateKey(orderNumbers.ToList()!, 8);
                SiteInfo? siteInfo = await _context.SiteInfos!.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync();
                string cur = "ریال";
                if (siteInfo != null)
                {
                    cur = siteInfo.SiteCurrency!;
                }
                cart = new()
                {
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Currency = cur,
                    OrderNumber = neworderNumber,
                    CartSum = addToCartVM.Quantity * netPrice
                };
                CartItem cartItem = new()
                {
                    ProductCode = addToCartVM.ProductCode,
                    Price = price,
                    NetPrice = netPrice,
                    Discount = disval,
                    Comment = addToCartVM.Comment,
                    Quantity = addToCartVM.Quantity,
                };

                cartItem.CartProductInfos.Add(new()
                {
                    Color = addToCartVM.prColor,
                    Size = PSize,
                    Height = PHeight,
                    Price = price,
                    Quantity = addToCartVM.Quantity,
                    NetPrice = netPrice,
                    RegDate = DateTime.Now
                });
                _context.Carts!.Add(cart);
                cart.CartItems.Add(cartItem);

                await _context.SaveChangesAsync();
                Res = "yes";
            }
            return (cart!, Res);

        }
        public async Task<(int Price, int NetPrice, int DiscountValue, int? DiscountPercent, bool Exist)> GetProductPriceFacilityData(string PrCode, int? Size, int? Height)
        {
            int Pprice = 0; int netPPrice = 0; int discount = 0; int? disPercent = null; bool exist = false;
            Product? product = await _context.Products!.Include(_ => _.ProductGroup).Include(x => x.ProductPrices).Include(x => x.ProductComponents).SingleOrDefaultAsync(_ => _.ProductCode == PrCode);
            List<WareHouse> wareHouses = new();

            if (product != null)
            {
                int? Remain = 0;

                wareHouses = await _context.WareHouses.Where(w => w.Product!.ProductId == product.ProductId).ToListAsync();
                if (wareHouses.Any())
                {
                    Remain = wareHouses.Sum(x => x.Input - x.Export);
                    if (Remain <= 0)
                    {
                        exist = false;
                    }
                    else
                    {
                        exist = true;
                    }
                }
                Pprice = product.ProductPrice.GetValueOrDefault();
                ProductPrice? productPrice = null;
                bool usedPrDiscount = false;
                if (Size != null && Height != null)
                {
                    productPrice = product.ProductPrices.SingleOrDefault(_ => _.Size == Size && _.Height == Height.Value);
                    if (productPrice != null)
                    {
                        Pprice = productPrice.Price.GetValueOrDefault();
                    }
                }
                if (product != null)
                {
                    if (product.ProductPercentDiscount.HasValue)
                    {
                        usedPrDiscount = true;
                        discount = (int)(Pprice * product.ProductPercentDiscount.Value / 100);
                        disPercent = (int)product.ProductPercentDiscount.Value;
                    }
                    else
                    {
                        if (product.ProductValueDiscount.HasValue)
                        {
                            discount = product.ProductValueDiscount.Value;
                            disPercent = (product.ProductValueDiscount.Value * 100) / Pprice;
                            usedPrDiscount = true;
                        }
                    }
                    if (product.ProductGroup != null && usedPrDiscount)
                    {
                        if (product.ProductGroup.DiscountPercent.HasValue)
                        {
                            discount = Pprice * product.ProductGroup.DiscountPercent.Value / 100;
                        }
                        else
                        {
                            if (product.ProductGroup.DiscountValue.HasValue)
                            {
                                discount = product.ProductGroup.DiscountValue.Value;
                            }
                        }
                    }
                }
                netPPrice = Pprice - discount;
            }
            return (Pprice, netPPrice, discount, disPercent, exist);
        }
        public async Task<Cart> GetCartByIdAsync(Guid id)
        {
            List<Cart> carts = await _context.Carts!.Include(x => x.CartItems).Include(_ => _.CartStatuses).Include(_ => _.User).ToListAsync();
            return carts!.SingleOrDefault(x => x.Id == id)!;
        }
        public async Task<Cart> GetCartByIdAsync(string id)
        {
            Guid gid = Guid.Parse(id);
            List<Cart> carts = await _context.Carts!.Include(x => x.CartItems).Include(x => x.CartStatuses).ToListAsync();
            Cart cart = carts!.SingleOrDefault(x => x.Id == gid && x.IsActive)!;
            return cart;
        }
        public async Task RemoveItemFromCart(string cartId, int itemId)
        {
            CartItem? cartItem = await _context.CartItems!.Include(x => x.Cart).Include(x => x.CartProductInfos).SingleOrDefaultAsync(x => x.CartId.ToString() == cartId && x.Id == itemId);
            if (cartItem != null)
            {

                cartItem.Cart!.CartSum -= cartItem.Quantity * cartItem.NetPrice;
                _context.CartItems!.Remove(cartItem);
            }
        }
        public async Task<List<CartItem>> GetCartItemsofCart(string cartId)
        {
            Guid gid = Guid.Parse(cartId);
            return await _context.CartItems!.Include(x => x.Cart).Include(x => x.Cart!.CartStatuses).Include(_ => _.CartProductInfos).Where(w => w.CartId == gid).ToListAsync();
        }
        public async Task<string> UpdateCartItemQuantity(int cartitemId, int count)
        {
            string Result = "no";
            CartItem cartItem = await _context.CartItems!.SingleOrDefaultAsync(x => x.Id == cartitemId) ?? new();
            if (cartItem != null)
            {
                if (cartItem.Quantity != count)
                {
                    cartItem.Quantity = count;
                    _context.CartItems!.Update(cartItem);
                    Result = "yes";
                }
            }
            return Result;
        }
        public async Task<CartItem> GetCartItemByIdAsync(int id)
        {
            List<CartItem> cartItems = await _context.CartItems!.Include(x => x.Cart).Include(_ => _.CartProductInfos).ToListAsync();
            return cartItems.SingleOrDefault(x => x.Id == id)!;
        }
        public string UpdateCartItem(CartItem cartItem)
        {
            _context.CartItems!.Update(cartItem);
            return "yes";
        }
        public void UpdateCart(Cart cart)
        {
            _context.Carts!.Update(cart);
        }
        public async Task<List<CartItem>> GetOrdersCartItemsForLogin(string UserNameorCellPhone)
        {
            User? user = await _context.Users!.SingleOrDefaultAsync(x => x.Cellphone == UserNameorCellPhone);
            if (user == null)
            {
                user = await _context.Users!.SingleOrDefaultAsync(x => x.UserName == UserNameorCellPhone);
            }
            List<CartItem> cartItems = new();
            if (user != null)
            {
                cartItems = await _context.CartItems!.Include(_ => _.Cart).Include(_ => _.CartProductInfos).Where(w => w.Cart!.CheckOut && w.Cart!.UserId == user.Id).ToListAsync();
            }
            return cartItems;
        }
        #endregion Cart
        #region WareHouse
        public void CreateWareHouse(WareHouse wareHouse)
        {
            _context.WareHouses.Add(wareHouse);
        }

        public void UpdateWareHouse(WareHouse wareHouse)
        {
            _context.WareHouses.Update(wareHouse);
        }

        public void DeleteWareHouse(WareHouse wareHouse)
        {
            _context.WareHouses.Remove(wareHouse);
        }

        public async Task<WareHouse> GetWareHouseByIdAsync(int id)
        {
            List<WareHouse> wareHouses = await _context.WareHouses.Include(x => x.Product).Include(x => x.Product!.ProductColors).Include(x => x.Product!.ProductPrices).Include(x => x.CartItem).Include(r => r.Product!.ProductGroup).ToListAsync();
            return wareHouses.SingleOrDefault(x => x.Id == id)!;
        }

        public async Task<List<WareHouse>> GetWareHousesAsync()
        {
            return await _context.WareHouses.Include(x => x.Product).Include(x => x.Product!.ProductColors).Include(x => x.Product!.ProductPrices).Include(x => x.CartItem).Include(r => r.Product!.ProductGroup)
                 .ToListAsync();
        }

        public bool ExistWareHouse(int id)
        {
            return _context.WareHouses.Any(x => x.Id == id);
        }
        public async Task<int> GetProductWareHouseRemainAsync(string productCode)
        {
            int Remain = 0;
            if (!string.IsNullOrEmpty(productCode))
            {
                Product? product = await _context.Products!.SingleOrDefaultAsync(_ => _.ProductCode == productCode);
                if (product != null)
                {
                    Remain = await _context.WareHouses.Include(_ => _.Product).Where(w => w.Product!.ProductCode == productCode).SumAsync(s => s.Input.GetValueOrDefault() - s.Export.GetValueOrDefault());
                }

            }
            return Remain;

        }

        public async Task<User> GetUserByCellphoneAsync(string cellphone)
        {
            return await _context!.Users!.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Cellphone == cellphone);
        }
        #endregion
        #region Reports
        public async Task<List<Cart>> GetCartsAsync()
        {
            return await _context.Carts!.Where(w => !w.CheckOut).Include(x => x.CartItems).ToListAsync();
        }
        public async Task<(bool HasTwoStagePay, int TwoStageValue)> GetTwoStageInfoAsync(string cartId)
        {
            bool hastwostage = false; int twostageval = 0;
            Cart? cart = await _context.Carts!.Include(x => x.CartItems).SingleOrDefaultAsync(x => x.Id.ToString() == cartId);
            List<(Product product, int ProPrice, int count)> products = new();
            foreach (var item in cart!.CartItems.ToList())
            {
                Product? product = await _context.Products!.SingleOrDefaultAsync(x => x.ProductCode == item.ProductCode);
                //var prInfo = await GetProductFinanceInfoAsync(item.Id);
                if (product != null)
                {
                    products.Add((product, item.Price, item.Quantity));
                }
            }
            foreach (var (product, ProPrice, count) in products)
            {

            }
            return (hastwostage, twostageval);
        }

        #endregion
        #region ProductSize
        public async Task<List<ProductSize>> GetProductSizesByProductIdAsync(Guid pId)
        {
            return await _context.ProductSizes.Include(x => x.Product).Where(w => w.ProductId == pId).ToListAsync();
        }

        public async Task<List<ProductSize>> GetProductSizesByProductCodeAsync(string pCode)
        {
            return await _context.ProductSizes.Include(x => x.Product).Where(w => w.Product!.ProductCode == pCode).ToListAsync();
        }

        public async Task<ProductSize> GetProductSizeByIdAsync(int id)
        {
            List<ProductSize> productSizes = await _context.ProductSizes.Include(x => x.Product).ToListAsync();
            return productSizes.SingleOrDefault(x => x.Id == id)!;
        }

        public void CreateProductSize(ProductSize productSize)
        {
            _context.ProductSizes.Add(productSize);
        }

        public void UpdateProductSize(ProductSize productSize)
        {
            _context.ProductSizes.Update(productSize);
        }

        public void DeleteProductSize(ProductSize productSize)
        {
            _context.ProductSizes.Remove(productSize);
        }

        public async Task<List<ProductSize>> GetProductSizesAsync()
        {
            return await _context.ProductSizes.Include(_ => _.Product).Include(_ => _.Product!.ProductGroup).ToListAsync();
        }
        public async Task<List<int?>> GetSizesByProdoctId(Guid prodoctId)
        {
            List<ProductSize> productSizes = await _context.ProductSizes.Where(w => w.ProductId == prodoctId).ToListAsync();
            return productSizes.Select(x => x.Size).ToList();
        }

        #endregion ProductSize
        #region DiscountCoupen
        public async Task<DiscountCoupen?> GetDiscountCoupenByCodeAsync(string Code)
        {
            DiscountCoupen? discountCoupen = await _context.DiscountCoupens!.SingleOrDefaultAsync(x => x.Code!.Equals(Code));
            return discountCoupen;
        }

        public async Task<(bool Validity, List<(string Message, string Note)> Messages)> CheckCoupenByCodeAsync(string Code, string UserName)
        {
            bool valid = true; string message = "کوپن تخفیف معتبر است"; string note = string.Empty;
            List<(string, string)> messages = new();
            User? user = await _context.Users!.SingleOrDefaultAsync(_ => _.UserName == UserName);
            if (user != null)
            {
                DiscountCoupen? discountCoupen = await _context.DiscountCoupens!.SingleOrDefaultAsync(x => x.Code!.Equals(Code.Trim()));

                if (discountCoupen != null)
                {
                    if (discountCoupen.IsActive)
                    {
                        if (discountCoupen.StartedDate != null)
                        {
                            if (DateTime.Now.Date < discountCoupen.StartedDate)
                            {
                                valid = false;
                                message = "کوپن تخفیف نامعتبر است !";
                                note = " تاریخ شروع " + discountCoupen.StartedDate.ToShamsiN() + " می باشد";
                                messages.Add((message, note));
                            }
                        }
                        if (DateTime.Now.Date > discountCoupen.ExpiredDate)
                        {
                            valid = false;
                            message = "کوپن تخفیف نامعتبر است !";
                            note = " تاریخ پایان " + discountCoupen.ExpiredDate.ToShamsiN() + " می باشد";
                            messages.Add((message, note));
                        }
                        if (discountCoupen.Remain == 0)
                        {
                            valid = false;
                            message = "کوپن تخفیف نامعتبر است !";
                            note = "تعداد قابل استفاده پایان یافته است";
                            messages.Add((message, note));
                        }
                    }
                    else
                    {
                        valid = false;
                        message = "کوپن تخفیف نامعتبر است !";
                        note = "کوپن فعال نمی باشد !";
                        messages.Add((message, note));
                    }
                }
                else
                {
                    valid = false;
                    message = "کوپن تخفیف نامعتبر است !";
                    note = "کوپن موجود نمی باشد !";
                    messages.Add((message, note));
                }
            }
            else
            {
                valid = false;
                message = "کوپن تخفیف نامعتبر است !";
                note = "کاربر موجود نمی باشد !";
                messages.Add((message, note));

            }

            return (valid, messages);
        }


        #endregion
        #region Order
        public async Task<List<Cart>> GetCartsAsOrders()
        {
            List<Cart> carts = await _context.Carts!.Include(_ => _.CartItems).Include(_ => _.CartStatuses).ToListAsync();
            return carts.Where(_ => _.IsActive && _.CheckOut).ToList();
        }

        public async Task<(bool, string, string)> AddStatusToOrderAsync(AddStatusVM addStatusVM)
        {
            Cart? cart = await _context.Carts!.Include(_ => _.CartStatuses).SingleOrDefaultAsync(_ => _.Id.ToString() == addStatusVM.CartId);
            bool res = false; string message = string.Empty;
            if (cart != null)
            {
                if (cart.CheckOut)
                {
                    Guid crtId = Guid.Parse(addStatusVM.CartId!);
                    Status? status = await _context.Statuses.SingleOrDefaultAsync(_ => _.Id == addStatusVM.StatusId);
                    User? user = await _context.Users!.SingleOrDefaultAsync(_ => _.UserName == addStatusVM.UserName);
                    if (status != null)
                    {
                        cart.CartStatuses.Add(
                            new CartStatus()
                            {
                                CartId = crtId,
                                RegDate = DateTime.Now,
                                Status = status,
                                Comment = addStatusVM.Comment,
                                IsActive = true,
                                LastCreateUser = user?.FullName,

                            }
                        );
                        _context.SaveChanges();
                        res = true;
                        message = "وضعیت اضافه شد";
                    }
                }
            }
            return (res, message, addStatusVM.CartId!);
        }

        public async Task<List<Status>> GetStatusesAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<CartStatus?> GetCartStatusbyIdAsync(int id)
        {
            return await _context.CartStatuses!.Include(c => c.CartId).Include(_ => _.Status).SingleOrDefaultAsync(_ => _.Id == id);
        }
        public void UpdateStatus(CartStatus cartStatus)
        {
            _context.CartStatuses!.Update(cartStatus);
        }

        public async Task<List<CartStatus>> GetCartStatusesByCartIdAsync(Guid crtid)
        {
            return await _context.CartStatuses!.Include(_ => _.Cart).Include(_ => _.Status).Where(w => w.CartId == crtid).ToListAsync();
        }
        public async Task<CartStatus?> GetCartLastStatusAsync(Guid crtid)
        {
            return await _context.CartStatuses!
                .Include(_ => _.Cart).Include(_ => _.Status)
                .Where(w => w.CartId == crtid).OrderByDescending(_ => _.RegDate).FirstOrDefaultAsync();
        }
        public async Task<Status?> GetStatusByIdAsync(int id)
        {
            return await _context.Statuses.SingleOrDefaultAsync(_ => _.Id == id);
        }
        #endregion Order
        #region ProductComponent
        public async Task<List<ProductComponent>> GetProductComponentsByProductIdAsync(Guid pId)
        {
            return await _context.ProductComponents.Include(_ => _.Product).Where(w => w.ProductId == pId).ToListAsync();
        }

        public async Task<List<ProductComponent>> GetProductComponentsByProductCodeAsync(string pCode)
        {
            return await _context.ProductComponents.Include(_ => _.Product).Where(_ => _.Product!.ProductCode == pCode).ToListAsync();
        }

        public async Task<List<ProductComponent>> GetProductComponentsAsync()
        {
            return await _context.ProductComponents.Include(_ => _.Product).ToListAsync();
        }

        public async Task<ProductComponent?> GetProductComponentByIdAsync(int id)
        {
            return await _context.ProductComponents.Include(x => x.Product).SingleOrDefaultAsync(_ => _.Id == id);
        }

        public void CreateProductComponent(ProductComponent productComponent)
        {
            _context.Add(productComponent);
        }

        public void UpdateProductComponent(ProductComponent productComponent)
        {
            _context.Update(productComponent);
        }

        public void DeleteProductComponent(ProductComponent productComponent)
        {
            _context.Remove(productComponent);
        }
        #endregion ProductComponent
        #region ProductPrice
        public async Task<ProductPrice?> GetProductPriceBySizeandHeightAsync(int? Height, int? Size)
        {
            if (Height == null || Size == null)
            {
                return null;
            }
            return await _context.ProductPrices.SingleOrDefaultAsync(x => x.Height == Height && x.Size == Size);
        }
        public async Task<bool> AllowedEdit(int ProductPriceId, int? Height, int? Size)
        {
            if (Height == null || Size == null)
            {
                return false;
            }
            ProductPrice? productPrice = await _context.ProductPrices.AsNoTracking().SingleOrDefaultAsync(x => x.Height == Height && x.Size == Size);
            if (productPrice != null)
            {
                if (productPrice.Id != ProductPriceId)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region CartProductInfo

        /// <summary>
        /// لیست محصولاتی که در اجزای محصولات دیگر با این محصول مشترک هستند
        /// </summary>
        /// <param name="prCode"></param>
        /// <returns></returns>
        public async Task<List<Product>?> GetCommonProductsofThisAsync(string prCode)
        {
            if (string.IsNullOrEmpty(prCode))
            {
                return null;
            }
            Product? product = await _context.Products!.Include(_ => _.ProductComponents).SingleOrDefaultAsync(x => x.ProductCode == prCode);
            if (product == null)
            {
                return null;
            }

            List<ProductComponent> SetProducts = await _context.Products!.Include(_ => _.ProductComponents)
                .Where(w => w.ProductComponents.Count > 1 && w.ProductComponents.Any(z => z.ProductCode == prCode)).SelectMany(_ => _.ProductComponents).ToListAsync();
            SetProducts = SetProducts.Where(w => w.ProductCode != prCode).ToList();
            List<Product> SetProductsProduct = new();
            foreach (var item in SetProducts)
            {
                Product? setProduct = await _context.Products!.Include(_ => _.ProductComponents).SingleOrDefaultAsync(_ => _.ProductCode == item.ProductCode);
                if (setProduct != null)
                {
                    SetProductsProduct.Add(setProduct);
                }
            }
            //SetProducts = SetProducts.Where(w => w.ProductCode != prCode).ToList();
            //return SetProducts!.Select(_ => _.Product!).ToList() ?? null;
            return SetProductsProduct.ToList();
        }

        public async Task<bool> CheckAllowSingleSell(string productCode, string CartId)
        {
            bool AllowSingleAdd = false;

            Product? product = await _context.Products!.Include(_ => _.ProductComponents).SingleOrDefaultAsync(x => x.ProductCode == productCode && x.IsActive);
            if (!string.IsNullOrEmpty(CartId))
            {
                Cart? cart = await _context.Carts!.Include(_ => _.CartItems).SingleOrDefaultAsync(x => x.Id.ToString() == CartId);

                if (cart != null)
                {
                    if (product != null)
                    {
                        if (product.AllowedSinglePurchase.GetValueOrDefault() == false)
                        {
                            List<ProductComponent> SetProducts = await _context.Products!.Include(_ => _.ProductComponents)
                            .Where(w => w.IsActive && w.ProductComponents.Count() > 1 && w.ProductComponents.Any(z => z.ProductCode == productCode)).SelectMany(_ => _.ProductComponents).ToListAsync();
                            SetProducts = SetProducts.Where(w => w.ProductCode != productCode).ToList();
                            List<string> SetProductCodes = SetProducts.Select(x => x.ProductCode!).ToList();
                            if (SetProductCodes != null)
                            {
                                if (SetProductCodes.Any())
                                {
                                    List<string> CartProductCodes = cart.CartItems.Select(x => x.ProductCode!).ToList();
                                    if (CartProductCodes.Intersect(SetProductCodes).Any())
                                    {
                                        AllowSingleAdd = true;
                                    }
                                    else
                                    {
                                        AllowSingleAdd = false;
                                    }
                                }
                                else
                                {
                                    AllowSingleAdd = false;
                                }
                            }
                            else
                            {
                                AllowSingleAdd = false;
                            }
                        }
                        else
                        {
                            AllowSingleAdd = true;
                        }
                    }


                }
                else
                {
                    AllowSingleAdd = product!.AllowedSinglePurchase.GetValueOrDefault();
                }
            }
            else
            {
                AllowSingleAdd = product!.AllowedSinglePurchase.GetValueOrDefault();
            }
            return AllowSingleAdd;
        }
        //Remove CPI region



        public async Task<bool> CheckAllProductOfCartIsSingleSellAsync(Guid cartId)
        {
            Cart? cart = await _context.Carts!.Include(_ => _.CartItems).SingleOrDefaultAsync(_ => _.Id == cartId);
            if (cart != null)
            {
                if (!cart.CartItems.Any())
                {
                    List<Product> cartProducts = await GetProductsofCartAsync(cartId);
                    if (cartProducts.Any())
                    {
                        if (cartProducts.All(x => x.AllowedSinglePurchase.GetValueOrDefault()))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public async Task<List<Product>> GetProductsofCartAsync(Guid cartId)
        {
            Cart? cart = await _context.Carts!.Include(_ => _.CartItems).SingleOrDefaultAsync(_ => _.Id == cartId);
            if (cart != null)
            {
                List<Product> products = new();
                foreach (var item in cart.CartItems)
                {
                    Product? product = await _context.Products!.SingleOrDefaultAsync(_ => _.ProductCode == item.ProductCode);
                    if (product != null) products.Add(product);
                }
            }
            return new List<Product>();
        }



        public async Task<bool> RemoveCIFromCartAsync(string cartId, int itemId)
        {
            CartItem? cartItem = await _context.CartItems!.Include(x => x.Cart).Include(x => x.CartProductInfos).SingleOrDefaultAsync(x => x.CartId.ToString() == cartId && x.Id == itemId);
            if (cartItem != null)
            {

                cartItem.Cart!.CartSum -= cartItem.Quantity * cartItem.NetPrice;
                _context.CartItems!.Remove(cartItem);
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveCIsFromCartAsync(string cartId, List<int> cisId)
        {
            if (string.IsNullOrEmpty(cartId)) return false;
            int allSum = 0;
            try
            {
                foreach (var item in cisId)
                {
                    CartItem? cartItem = await _context.CartItems!.Include(_ => _.Cart).SingleOrDefaultAsync(_ => _.Id == item && _.CartId.ToString() == cartId);
                    
                    if (cartItem != null)
                    {
                        allSum = cartItem.Quantity * cartItem.NetPrice;
                        Cart? cart = await _context.Carts!.FindAsync(cartItem.CartId);
                        if ( cart != null)
                        {
                            cart.CartSum -= allSum;
                        }
                        
                        List<CartProductInfo> cartProductInfos = await _context.CartProductInfos.Where(W => W.CartItemId == cartItem.Id).ToListAsync();
                        _context.CartProductInfos.RemoveRange(cartProductInfos);
                        _context.CartItems!.Remove(cartItem);

                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {

                string m = ex.InnerException.Message;
                return false;
            }
        }



        public async Task<List<CartProductInfo>> GetProductInfosofCartItemAsync(int ciId)
        {
            List<CartProductInfo> cartProductInfos = await _context.CartProductInfos.Include(_ => _.CartItem)
                .Where(w => w.CartItemId == ciId).ToListAsync();
            return cartProductInfos;
        }

        public async Task<(bool prInfoCanRemove, bool wasRemoved, string Message, List<int> ProductsCIMustBeRemoved)> Check_prInfoDeletionStatusAsync(int? prInfoId, string cartId)
        {
            bool canRemove = true; string message = string.Empty; List<int> prdsCodeMustBeRemoved = new(); bool wasremoved = false;
            Cart? cart = await _context.Carts!.Include(_ => _.CartItems).SingleOrDefaultAsync(_ => _.Id.ToString() == cartId);
            if (cart == null)
            {
                canRemove = false;
            }

            CartProductInfo? cartProductInfo = await _context.CartProductInfos.Include(_ => _.CartItem).SingleOrDefaultAsync(_ => _.Id == prInfoId);
            CartItem? cartItem = null;
            if (cartProductInfo == null)
            {

                canRemove = false;
            }
            else
            {
                cartItem = await _context.CartItems!.Include(_ => _.CartProductInfos).Include(_ => _.Cart)
                                       .SingleOrDefaultAsync(_ => _.Id == cartProductInfo!.CartItemId);
            }

            //چک شود آیا تمام محصولات کارت مجوز فروش تکی دارند یا خیر
            //اگر دارند ویژگی محصول می تواند حذف شود
            // اگر ندارند باید بررسی شود که ویژگی محصول برای کارت آیتم یک ردیف می باشد یا بیشتر
            //اگر ویژگی بیشتر از یک ردیف باشد، ویژگی انتخاب شده می تواند حذف شود
            if (canRemove)
            {
                bool AllProfCartIsSingleSell = await CheckAllProductOfCartIsSingleSellAsync(Guid.Parse(cartId));
                if (!AllProfCartIsSingleSell)
                {
                    if (cartItem == null)
                    {
                        canRemove = false;
                    }
                    else
                    {
                        if (cartItem.CartProductInfos.Count == 1)
                        {
                            prdsCodeMustBeRemoved.Add(cartItem.Id);
                            Product? PoroductofCartItem = await _context.Products!.SingleOrDefaultAsync(_ => _.ProductCode == cartItem.ProductCode);
                            List<Product>? ComPrdsofThis = await GetCommonProductsofThisAsync(cartItem.ProductCode!);
                            if (ComPrdsofThis != null)
                            {
                                if (ComPrdsofThis.Any(a => a.AllowedSinglePurchase.GetValueOrDefault() == false))
                                {
                                    List<Product> productsCanNotSingleSell = ComPrdsofThis.Where(w => w.AllowedSinglePurchase.GetValueOrDefault() == false).ToList();
                                    if (productsCanNotSingleSell != null)
                                    {
                                        if (productsCanNotSingleSell.Any())
                                        {
                                            message = "<p>" + "حذف" + " " + PoroductofCartItem?.ProductName + " " + "باعث حذف محصول یا محصولات زیر نیز از سبد خواهد شد :" + "</p>";
                                            foreach (var pr in productsCanNotSingleSell.ToList())
                                            {
                                                //پیدا کردن محصولات مشترک برای هر محصولی که اجازه فروش تکی ندارد
                                                List<Product>? CommonProducts_ofPrCanNotSingleSell = await GetCommonProductsofThisAsync(pr.ProductCode!);
                                                if (CommonProducts_ofPrCanNotSingleSell != null)
                                                {
                                                    if (CommonProducts_ofPrCanNotSingleSell.Count == 1 && CommonProducts_ofPrCanNotSingleSell.Any(a => a.ProductCode != PoroductofCartItem!.ProductCode))
                                                    {
                                                        canRemove = true;
                                                    }
                                                    else
                                                    {
                                                        if (cart!.CartItems.Any(a => a.ProductCode == pr.ProductCode))
                                                        {
                                                            CartItem? cartCartItem = await _context.CartItems!.SingleOrDefaultAsync(_ => _.ProductCode == pr.ProductCode);
                                                            if (cartCartItem != null)
                                                            {
                                                                prdsCodeMustBeRemoved.Add(cartCartItem.Id);
                                                            }

                                                            message += "<p><span class='bg bg-danger zpr-1 zpl-1 text-white rounded '>" + pr.ProductName + "</span></p>";
                                                            canRemove = false;
                                                        }

                                                    }
                                                }

                                            }
                                        }
                                    }

                                }
                            }

                        }
                        else if (cartItem.CartProductInfos.Count >1)
                        {
                            canRemove=true;
                        }
                    }

                }
            }
            if (canRemove)
            {
                message = string.Empty;
                int cpiSum = cartProductInfo!.Quantity * cartProductInfo.NetPrice.GetValueOrDefault();
                _context.CartProductInfos.Remove(cartProductInfo!);
                if (cartItem != null)
                {
                    if (cartItem.CartProductInfos.Count == 1)
                    {
                        cart!.CartSum -= cpiSum;
                        _context.CartItems!.Remove(cartItem);
                        wasremoved = true;
                    }
                    else if (cartItem.CartProductInfos.Count>1)
                    {
                        cart!.CartSum -= cpiSum;
                        _context.CartProductInfos.Remove(cartProductInfo);
                        wasremoved = true;
                    }

                }

                
            }
            return (canRemove, wasremoved, message, prdsCodeMustBeRemoved);
        }

        public async Task<(bool prInfoCanRemove, bool wasRemoved, string Message, List<int> ProductsCIMustBeRemoved)> Check_cartItemDeletionStatusAsync(int? ciId, string cartId)
        {
            bool canRemove = true; string message = string.Empty; List<int> prdsCodeMustBeRemoved = new(); bool wasremoved = false;
            Cart? cart = await _context.Carts!.Include(_ => _.CartItems).SingleOrDefaultAsync(_ => _.Id.ToString() == cartId);
            if (cart == null)
            {
                canRemove = false;
            }


            CartItem? cartItem = await _context.CartItems!.Include(_ => _.Cart).Include(_ => _.CartProductInfos).SingleOrDefaultAsync(_ => _.Id == ciId);
            if (cartItem == null)
            {
                canRemove = false;
            }


            //چک شود آیا تمام محصولات کارت مجوز فروش تکی دارند یا خیر
            //اگر دارند ویژگی محصول می تواند حذف شود
            // اگر ندارند باید بررسی شود که ویژگی محصول برای کارت آیتم یک ردیف می باشد یا بیشتر
            //اگر ویژگی بیشتر از یک ردیف باشد، ویژگی انتخاب شده می تواند حذف شود
            if (canRemove)
            {
                bool AllProfCartIsSingleSell = await CheckAllProductOfCartIsSingleSellAsync(Guid.Parse(cartId));
                if (!AllProfCartIsSingleSell)
                {

                    if (cartItem!.CartProductInfos.Count == 1)
                    {
                        prdsCodeMustBeRemoved.Add(cartItem.Id);
                        Product? PoroductofCartItem = await _context.Products!.SingleOrDefaultAsync(_ => _.ProductCode == cartItem.ProductCode);
                        List<Product>? ComPrdsofThis = await GetCommonProductsofThisAsync(cartItem.ProductCode!);
                        if (ComPrdsofThis != null)
                        {
                            if (ComPrdsofThis.Any(a => a.AllowedSinglePurchase.GetValueOrDefault() == false))
                            {
                                List<Product> productsCanNotSingleSell = ComPrdsofThis.Where(w => w.AllowedSinglePurchase.GetValueOrDefault() == false).ToList();
                                if (productsCanNotSingleSell != null)
                                {
                                    if (productsCanNotSingleSell.Any())
                                    {
                                        message = "<p>" + "حذف" + " " + PoroductofCartItem?.ProductName + " " + "باعث حذف محصول یا محصولات زیر نیز از سبد خواهد شد :" + "</p>";
                                        foreach (var pr in productsCanNotSingleSell.ToList())
                                        {
                                            //پیدا کردن محصولات مشترک برای هر محصولی که اجازه فروش تکی ندارد
                                            List<Product>? CommonProducts_ofPrCanNotSingleSell = await GetCommonProductsofThisAsync(pr.ProductCode!);
                                            if (CommonProducts_ofPrCanNotSingleSell != null)
                                            {
                                                if (CommonProducts_ofPrCanNotSingleSell.Count == 1 && CommonProducts_ofPrCanNotSingleSell.Any(a => a.ProductCode != PoroductofCartItem!.ProductCode))
                                                {
                                                    canRemove = true;
                                                }
                                                else
                                                {
                                                    if (cart!.CartItems.Any(a => a.ProductCode == pr.ProductCode))
                                                    {
                                                        CartItem? cartCartItem = await _context.CartItems!.SingleOrDefaultAsync(_ => _.ProductCode == pr.ProductCode);
                                                        if (cartCartItem != null)
                                                        {
                                                            prdsCodeMustBeRemoved.Add(cartCartItem.Id);
                                                        }

                                                        message += "<p><span class='bg bg-danger zpr-1 zpl-1 text-white rounded '>" + pr.ProductName + "</span></p>";
                                                        canRemove = false;
                                                    }

                                                }
                                            }

                                        }
                                    }
                                }

                            }
                        }

                    }


                }
            }
            if (canRemove)
            {
                message = string.Empty;

                if (cartItem != null)
                {
                    int cartItemSum = cartItem.Quantity * cartItem.NetPrice;
                    _context.CartItems!.Remove(cartItem);
                    cart!.CartSum -= cartItemSum;
                    wasremoved = true;

                }
                
            }
            return (canRemove, wasremoved, message, prdsCodeMustBeRemoved);
        }

        public async Task<Cart?> GetCartByOrderNumberAsync(string orderNumber)
        {
            return await _context.Carts!.Include(_ => _.CartItems).Include(_ => _.CartStatuses)
                .SingleOrDefaultAsync(_ => _.OrderNumber == orderNumber);
        }

        public async Task<List<CellphoneBank>> GetCellphoneBanksAsync()
        {
            return await _context.CellphoneBanks.ToListAsync();
        }


        //Remove CPI region



        #endregion
    }
}
