using OA.Services;
using OA_WEB.Models;
using OA.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OA_WEB.Factories;

public class ProductModelFactory : IProductModelFactory
{
    #region Fields

    private readonly ICategoryService _categoryService;

    #endregion

    #region Ctor

    public ProductModelFactory(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    #endregion

    #region Methods

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
                    WarehouseId = product.WarehouseId,
                    Price = product.BuyingPrice,
                    Quantity = product.Quantity,
                };
            }

            var productCategory = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            model.CategoryName = productCategory.Name;
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

    #endregion
}
