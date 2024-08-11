using OA.Core.Domain;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories
{
    public interface IAdminCategoryModelFactory
    {
        Task<IList<CategoryModel>> PrepareCategoryListModelAsync(IEnumerable<Category> categories);

        Task<CategoryModel> PrepareCategoryModelAsync(CategoryModel model, Category category, bool excludeProperties = false);
    }
}
