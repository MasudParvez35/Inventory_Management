using OA.Core.Domain;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories;

public class CategoryModelFactory : ICategoryModelFactory
{
    public async Task<IList<CategoryModel>> PrepareCategoryListModelAsync(IEnumerable<Category> categories)
    {
        var model = new List<CategoryModel>();

        foreach (var category in categories)
            model.Add(await PrepareCategoryModelAsync(null, category));

        return model;
    }

    public async Task<CategoryModel> PrepareCategoryModelAsync(CategoryModel model, Category category, bool excludeProperties = false)
    {
        if (category != null)
        {
            if (model == null)
            {
                model = new CategoryModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                };
            }
        }
        return model;
    }
}
