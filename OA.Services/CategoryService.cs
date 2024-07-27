using OA.Core.Domain;
using OA.Data;

namespace OA.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }

        public async Task InsertCategoryAsync(Category category)
        {
            await _categoryRepository.InsertAsync(category);  
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}
