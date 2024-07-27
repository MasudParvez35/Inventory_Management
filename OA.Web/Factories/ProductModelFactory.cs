using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Core.Domain;
using OA.Services;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public class ProductModelFactory : IProductModelFactory
    {
        private readonly ICategoryService _categoryService;

        public ProductModelFactory(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IList<ProductModel>> PrepareProductListModelAsync(IEnumerable<Product> products)
        {
            var model = new List<ProductModel>();

            foreach (var product in products)
                model.Add(await PrepareProductModelAsync(null, product));

            return model;
        }

        public async Task<ProductModel> PrepareProductModelAsync(ProductModel model, Product product, bool excludeProperties = false)
        {
            if (product != null)
            {
                if (model == null)
                {
                    model = new ProductModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImagePath = product.ImagePath,
                        CategoryId = product.CategoryId,
                        BuyingPrice = product.BuyingPrice,
                        SellingPrice = product.SellingPrice,
                        Quantity = product.Quantity,
                    };
                }
                else
                {
                    model.Id = product.Id;
                    model.Name = product.Name;
                    model.Description = product.Description;
                    model.ImagePath = product.ImagePath;
                    model.CategoryId = product.CategoryId;
                    model.BuyingPrice = product.BuyingPrice;
                    model.SellingPrice = product.SellingPrice;
                    model.Quantity = product.Quantity;
                }

                var productCategory = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
                model.CategoryName = productCategory?.Name;
            }

            if (!excludeProperties)
            {
                var allCategories = await _categoryService.GetAllCategory();
                model.AvailableCategoryOptions = allCategories.Select(category => new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                }).ToList();
            }

            return model;
        }
    }
}
