using OA.Services;
using OA.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using OA_WEB.Areas.Admin.Models;
using OA_WEB.Areas.Admin.Factories;

namespace OA_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAdminProductModelFactory _productModelFactory;

        #endregion

        #region Ctor

        public ProductController(
            IProductService productService, 
            IWebHostEnvironment webHostEnvironment, 
            IAdminProductModelFactory productModelFactory)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _productModelFactory = productModelFactory;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> List(int categoryId = 0)
        {
            var model = await _productModelFactory.PrepareProductListModelAsync(categoryId);

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return RedirectToAction("List");

            var model = await _productModelFactory.PrepareProductModelAsync(null, product);

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _productModelFactory.PrepareProductModelAsync(new ProductModel(), null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (model.CategoryId == 0)
                {
                    ModelState.AddModelError("CategoryId", "Please select a category.");
                    model = await _productModelFactory.PrepareProductModelAsync(model, null);
                    return View(model);
                }

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string imagePath = Path.Combine(wwwRootPath, @"Image");

                    using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.ImagePath = @"\Image\" + fileName;

                    var Product = new Product()
                    {
                        Name = model.Name,
                        ImagePath = model.ImagePath,
                        Description = model.Description,
                        CategoryId = model.CategoryId,
                        BuyingPrice = model.BuyingPrice,
                        SellingPrice = model.SellingPrice,
                        Quantity = model.Quantity,
                    };

                    await _productService.InsertProductAsync(Product);
                }

                return RedirectToAction("List");
            }

            model = await _productModelFactory.PrepareProductModelAsync(model, null);

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return RedirectToAction("List");

            var model = await _productModelFactory.PrepareProductModelAsync(null, product);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel model, IFormFile? file)
        {
            var product = await _productService.GetProductByIdAsync(model.Id);
            if (product == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string imagePath = Path.Combine(wwwRootPath, @"Image");

                    using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    product.ImagePath = @"\Image\" + fileName;  // Update product's image path if a new file is uploaded
                }
                else
                {
                    // Retain the existing image path if no new file is uploaded
                    model.ImagePath = product.ImagePath;
                }

                // Update other product fields
                product.Name = model.Name;
                product.Description = model.Description;
                product.CategoryId = model.CategoryId;
                product.BuyingPrice = model.BuyingPrice;
                product.SellingPrice = model.SellingPrice;
                product.Quantity = model.Quantity;

                await _productService.UpdateProductAsync(product);

                return RedirectToAction("List");
            }

            model = await _productModelFactory.PrepareProductModelAsync(model, product);

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return RedirectToAction("List");

            var model = await _productModelFactory.PrepareProductModelAsync(null, product);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductModel model)
        {
            var product = await _productService.GetProductByIdAsync(model.Id);
            if (product == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                product.Name = model.Name;
                product.ImagePath = model.ImagePath;
                product.Description = model.Description;
                product.CategoryId = model.CategoryId;
                product.BuyingPrice = model.BuyingPrice;
                product.SellingPrice = model.SellingPrice;
                product.Quantity = model.Quantity;

                await _productService.DeleteProductAsync(product);

                return RedirectToAction("List");
            }

            return View(model);
        }

        #endregion
    }
}
