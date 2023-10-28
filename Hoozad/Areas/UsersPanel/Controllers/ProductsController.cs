using Core.DTOs.Admin;
using Core.DTOs.General;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IStoreService _storeService;
        public ProductsController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: ProductsController
        public async Task<ActionResult> Index(int? gId)
        {
            if (gId == null)
            {
                var products = await _storeService.GetProductsAsync();
                return products != null ?
                            View(products) :
                            Problem("Entity set 'MyContext.Products'  is null.");
            }
            else
            {
                ViewData["gid"] = gId.Value;
                var products = await _storeService.GetProductsAsync();
                products = products.Where(w => w.ProductGroupId == gId.Value).ToList();
                return products != null ?
                            View(products) :
                            Problem("Entity set 'MyContext.Products'  is null.");
            }

        }

        // GET: ProductsController/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null || await _storeService.GetProductsAsync() == null)
            {
                return NotFound();
            }

            var product = await _storeService.GetProductAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> ValidateProductCode(string code)
        {

            if (code.Length < 2)
            {
                return Json(new { valid = false, message = "طول کد کمتر از 2 کاراکتر است !" });
            }

            bool Ex = await _storeService.ExistProductByCodeAsync(code);

            if (Ex == true)
            {
                return Json(new { valid = false, message = "کد تکراری است !" });
            }
            else
            {
                return Json(new { valid = true, message = "کد تایید است !" });
            }

        }
        public async Task<IActionResult> AddPrice(string pId)
        {
            if (pId == null)
            {
                return NotFound("محصول مشخص نشده است !");
            }
            Product product = await _storeService.GetProductAsync(Guid.Parse(pId));
            if (product == null)
            {
                return NotFound("محصول درست مشخص نشده است !");
            }
            Guid prId = Guid.Parse(pId);
            ProductPriceViewModel productPriceViewModel = new()
            {
                ProductId = prId,
                Sizes = await _storeService.GetSizesByProdoctId(prId) 
            };
            return PartialView(productPriceViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPrice(ProductPriceViewModel productPriceViewModel)
        {
            if (!ModelState.IsValid)
            {
                productPriceViewModel.Sizes = await _storeService.GetSizesByProdoctId(productPriceViewModel.ProductId.GetValueOrDefault());
                return PartialView(productPriceViewModel);                
            }
            ProductPrice? productPrice = await _storeService.GetProductPriceBySizeandHeightAsync(productPriceViewModel.Height,productPriceViewModel.Size);
            if (productPrice == null)
            {
                (bool response, string message) = await _storeService.CreateProductPrice(productPriceViewModel);
                if (response)
                {
                    _storeService.SaveChanges();
                }
            }
            else
            {
                
                ModelState.AddModelError("Height", "سایز و قد تکراری هستند !");
                productPriceViewModel.Sizes = await _storeService.GetSizesByProdoctId(productPriceViewModel.ProductId.GetValueOrDefault());
                return PartialView(productPriceViewModel);
            }
           

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditPrice(int? prId)
        {
            if (prId == null)
            {
                return NotFound();
            }
            ProductPrice productPrice = await _storeService.GetProductPriceById(prId.Value);
            if (productPrice == null)
            {
                return NotFound();
            }

            return PartialView(productPrice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPrice(ProductPrice productPrice)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(productPrice);
            }
            bool al = await _storeService.AllowedEdit(productPrice.Id,productPrice.Height,productPrice.Size);
            if (al)
            {
                _storeService.UpdateProductPrice(productPrice);
                await _storeService.SaveChangesAsync();
                
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemovePrice(int? prId)
        {
            if (prId == null)
            {
                return NotFound();
            }
            ProductPrice productPrice = await _storeService.GetProductPriceById(prId.Value);
            if (productPrice == null)
            {
                return NotFound();
            }

            return PartialView(productPrice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePrice(int id)
        {
            ProductPrice productPrice = await _storeService.GetProductPriceById(id);
            _storeService.DeleteProductPrice(productPrice);
            await _storeService.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddColor(string pId)
        {
            if (pId == null)
            {
                return NotFound("محصول مشخص نشده است !");
            }
            Product product = await _storeService.GetProductAsync(Guid.Parse(pId));
            if (product == null)
            {
                return NotFound("محصول درست مشخص نشده است !");
            }
            ProductColorVM productColorVM = new()
            {
                ProductId = Guid.Parse(pId),
            };
            return PartialView(productColorVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddColor(ProductColorVM productColorVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(productColorVM);
            }
            _storeService.CreateProductColor(new ProductColor { ProductId = productColorVM.ProductId, Color = productColorVM.Color, ColorName = productColorVM.ColorName });
            await _storeService.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditColor(int? PrcId)
        {
            if (PrcId == null)
            {
                return NotFound();
            }
            ProductColor productColor = await _storeService.GetProductColorByIdAsync(PrcId.Value);
            if (productColor == null)
            {
                return NotFound();
            }

            return PartialView(productColor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditColor(ProductColor productColor)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(productColor);
            }
            _storeService.UpdateProductColor(productColor);
            await _storeService.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveColor(int? prcId)
        {
            if (prcId == null)
            {
                return NotFound();
            }
            ProductColor productColor = await _storeService.GetProductColorByIdAsync(prcId.Value);
            if (productColor == null)
            {
                return NotFound();
            }

            return PartialView(productColor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveColor(int id)
        {
            ProductColor productColor = await _storeService.GetProductColorByIdAsync(id);
            if (productColor != null)
            {
                _storeService.DeleteProductColor(productColor);
                await _storeService.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound(ModelState);
            }
            
        }
        public async Task<IActionResult> AddSize(string pId)
        {
            if (pId == null)
            {
                return NotFound("محصول مشخص نشده است !");
            }
            Product product = await _storeService.GetProductAsync(Guid.Parse(pId));
            if (product == null)
            {
                return NotFound("محصول درست مشخص نشده است !");
            }
            ProductSizeVM productSizeVM = new()
            {
                ProductId = Guid.Parse(pId),
            };
            return PartialView(productSizeVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSize(ProductSizeVM productSizeVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(productSizeVM);
            }
            _storeService.CreateProductSize(new ProductSize
            {
                ProductId = productSizeVM.ProductId,
                WaistAround = productSizeVM.WaistAround,
                Size = productSizeVM.Size,
                ArmAround = productSizeVM.ArmAround,
                ChestAround = productSizeVM.ChestAround,
                HipsAround = productSizeVM.HipsAround,
                SleeveLength = productSizeVM.SleeveLength,
            });
            await _storeService.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddComponent(string pId)
        {
            if (pId == null)
            {
                return NotFound("محصول مشخص نشده است !");
            }
            Product product = await _storeService.GetProductAsync(Guid.Parse(pId));
            if (product == null)
            {
                return NotFound("محصول درست مشخص نشده است !");
            }
            List<Product> products = await _storeService.GetProductsAsync();

            ProductComponentVM productComponentVM = new()
            {
                ProductId = Guid.Parse(pId),
                BaseProduct = product,
                Products = products
            };
            return PartialView(productComponentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComponent(ProductComponentVM productComponentVM)
        {
            if (!ModelState.IsValid)
            {
                productComponentVM.Products = await _storeService.GetProductsAsync();
                return PartialView(productComponentVM);
            }
            ProductComponent productComponent = new()
            {

                ProductId = productComponentVM.ProductId,
                ProductCode = productComponentVM.ProductCode,
                RegDate = DateTime.Now
            };

            _storeService.CreateProductComponent(productComponent);
            await _storeService.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveComponent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductComponent? productComponent = await _storeService.GetProductComponentByIdAsync(id.Value);
            if (productComponent == null)
            {
                return NotFound();
            }

            return PartialView(productComponent);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveComponent(int id)
        {
            ProductComponent? productComponent = await _storeService.GetProductComponentByIdAsync(id);
            if (productComponent != null)
            {
                _storeService.DeleteProductComponent(productComponent);
                await _storeService.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound(ModelState);
            }

        }
        // GET: ProductsController/Create
        public async Task<ActionResult> Create(int? gid)
        {
            List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
            if (gid == null)
            {
                ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title");
            }
            else
            {
                ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", gid.Value);
                ViewData["gid"] = gid;
            }

            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, IFormFile? ProductImage1, IFormFile? ProductImage2, IFormFile? ProductImage3, IFormFile? ProductImage4)
        {
            try
            {
                List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Select(x => x.Value!.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                    ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
                    return View(product);
                }
                if (await _storeService.ExistProductByCodeAsync(product.ProductCode!))
                {
                    ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
                    ModelState.AddModelError("ProductCode", "کد محصول تکراری است !");
                    return View(product);
                }
                if (ProductImage1 == null)
                {
                    ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
                    ModelState.AddModelError("ProductImage1", "تصویر را انتخاب کنید !");
                    return View(product);
                }
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                if (ProductImage1 != null)
                {
                    FileValidation fileValidation1 = await ProductImage1.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("ProductImage1", fileValidation1.Message!);
                        return View(product);
                    }
                    product.ProductImage1 = ProductImage1.SaveUploadedImage("wwwroot/images/store/products", true);
                }
                if (ProductImage2 != null)
                {
                    FileValidation fileValidation1 = await ProductImage2.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("ProductImage2", fileValidation1.Message!);
                        return View(product);
                    }
                    product.ProductImage2 = ProductImage2.SaveUploadedImage("wwwroot/images/store/products", true);
                }
                if (ProductImage3 != null)
                {
                    FileValidation fileValidation1 = await ProductImage3.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("ProductImage3", fileValidation1.Message!);
                        return View(product);
                    }
                    product.ProductImage3 = ProductImage3.SaveUploadedImage("wwwroot/images/store/products", true);
                }
                if (ProductImage4 != null)
                {
                    FileValidation fileValidation1 = await ProductImage4.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("ProductImage4", fileValidation1.Message!);
                        return View(product);
                    }
                    product.ProductImage4 = ProductImage4.SaveUploadedImage("wwwroot/images/store/products", true);
                }
                product.CreatedDate = DateTime.Now;
                _storeService.CreateProduct(product);
                await _storeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
                ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
                return View(product);
            }
        }
        public async Task<bool> ChangeStatus(string id, int status)
        {
            Product product = await _storeService.GetProductAsync(Guid.Parse(id));
            if (product == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            product.IsActive = st;
            _storeService.UpdateProduct(product);
            await _storeService.SaveChangesAsync();
            return st;

        }
        public async Task<bool> ChangeBuyStatus(string id, int status)
        {
            Product product = await _storeService.GetProductAsync(Guid.Parse(id));
            if (product == null)
            {
                return false;
            }

            bool st = false;
            if (status == 1)
            {
                st = true;
            }
            product.AllowedSinglePurchase = st;
            _storeService.UpdateProduct(product);
            await _storeService.SaveChangesAsync();
            return st;

        }
        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _storeService.GetProductAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
            ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Product product, IFormFile? ProductImage1, IFormFile? ProductImage2, IFormFile? ProductImage3, IFormFile? ProductImage4)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Select(x => x.Value!.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                    List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
                    ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
                    return View(product);
                }
                if (id != product.ProductId)
                {
                    return NotFound();
                }
                //if (ProductImage1 == null)
                //{
                //    List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
                //    ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
                //    ModelState.AddModelError("ProductImage1", "تصویر را انتخاب کنید !");
                //    return View(product);
                //}
                string[] ex = { ".jpg", ".jpeg", ".gif", ".png" };
                if (ProductImage1 != null)
                {
                    FileValidation fileValidation1 = await ProductImage1.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("ProductImage1", fileValidation1.Message!);
                        return View(product);
                    }
                    product.ProductImage1 = ProductImage1.SaveUploadedImage("wwwroot/images/store/products", true);
                }
                if (ProductImage2 != null)
                {
                    FileValidation fileValidation1 = await ProductImage2.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("ProductImage2", fileValidation1.Message!);
                        return View(product);
                    }
                    product.ProductImage2 = ProductImage2.SaveUploadedImage("wwwroot/images/store/products", true);
                }
                if (ProductImage3 != null)
                {
                    FileValidation fileValidation1 = await ProductImage3.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("ProductImage3", fileValidation1.Message!);
                        return View(product);
                    }
                    product.ProductImage3 = ProductImage3.SaveUploadedImage("wwwroot/images/store/products", true);
                }
                if (ProductImage4 != null)
                {
                    FileValidation fileValidation1 = await ProductImage4.UploadedImageValidation(50, ex)!;
                    if (!fileValidation1.IsValid)
                    {
                        ModelState.AddModelError("ProductImage4", fileValidation1.Message!);
                        return View(product);
                    }
                    product.ProductImage4 = ProductImage4.SaveUploadedImage("wwwroot/images/store/products", true);
                }

                _storeService.UpdateProduct(product);
                await _storeService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                List<ProductGroup> groups = await _storeService.GetProductGroupsAsync();
                ViewData["ProductGroupId"] = new SelectList(groups.Where(w => w.IsActive).ToList(), "Id", "Title", product.ProductGroupId);
                return View(product);
            }
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var product = await _storeService.GetProductAsync(Guid.Parse(id));
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                Product product = await _storeService.GetProductAsync(id);
                if (product != null)
                {
                    _storeService.DeleteProduct(product);
                    await _storeService.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Product product = await _storeService.GetProductAsync(id);
                return View(product);
            }
        }

        public async Task<JsonResult> GetProductCode()
        {
            List<Product> products = new();
            products = await _storeService.GetProductsAsync();
            List<string?> codes = new();
            codes = products.Select(_ => _.ProductCode).ToList();
            string GCode = string.Empty;
            GCode = Core.Prodocers.Generators.GenerateUniqueString(codes.ToList()!, 2, 2, 1, 0);
            return Json(GCode);
        }
    }
}
