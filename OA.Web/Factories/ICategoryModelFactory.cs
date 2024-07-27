using OA.Core.Domain;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public interface ICategoryModelFactory
    {
        Task<IList<CategoryModel>> PrepareCategoryListModelAsync(IEnumerable<Category> categories);

        Task<CategoryModel> PrepareCategoryModelAsync(CategoryModel model, Category category, bool excludeProperties = false);
    }
}
