using OA.Services;
using OA.Core.Domain;
using OA_WEB.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OA_WEB.Areas.Admin.Factories;

public class AdminProductModelFactory : IAdminProductModelFactory
{
    #region Fields

    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    #endregion

    #region Ctor

    public AdminProductModelFactory(ICategoryService categoryService, 
        IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    #endregion

    #region Methods

    public async Task<ProductListModel> PrepareProductListModelAsync(int categoryId, int page = 1, int pageSize = 5)
    {
        var model = new ProductListModel();

        if (categoryId > 0)
            model.CategoryName = (await _categoryService.GetCategoryByIdAsync(categoryId)).Name;

        var allCategories = await _categoryService.GetAllCategory();
        model.AvailableCategories = allCategories.Select(category => new SelectListItem
        {
            Value = category.Id.ToString(),
            Text = category.Name,
            Selected = category.Id == categoryId
        }).ToList();

        // Retrieve products by category, apply pagination
        var products = await _productService.GetAllProductsByCategoryId(categoryId);
        int totalProducts = products.Count();
        int totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

        var paginatedProducts = products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Prepare the product models
        foreach (var product in paginatedProducts)
            model.Products.Add(await PrepareProductModelAsync(null, product, true));

        // Set pagination data
        model.CurrentPage = page;
        model.TotalPages = totalPages;

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

    #endregion
}
