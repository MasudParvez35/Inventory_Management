using OA.Core.Domain;
namespace OA.Services
{
    public interface ICategoryService
    {
        Task InsertCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllCategory();
    }
}
