using Microsoft.AspNetCore.Mvc;
using OA.Core.Domain;
using OA.Services;
using OA_WEB.Factories;
using OA_WEB.Models;

namespace OA_WEB.Controllers
{
    public class ProductController : Controller
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        #region Ctor

        public ProductController(
            IWebHostEnvironment webHostEnvironment, 
            IProductService productService, 
            IProductModelFactory productModelFactory)
        {
            _webHostEnvironment = webHostEnvironment;
            _productService = productService;
            _productModelFactory = productModelFactory;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> List()
        {
            var products = await _productService.GetAllProductAsync();
            var model = await _productModelFactory.PrepareProductListModelAsync(products);

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

        #endregion
    }
}
