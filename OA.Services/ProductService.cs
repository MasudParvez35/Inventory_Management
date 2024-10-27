using Microsoft.EntityFrameworkCore;
using OA.Core.Domain;
using OA.Data;

namespace OA.Services;

public class ProductService : IProductService
{
    #region Fields

    private readonly IRepository<Product> _productsRepository;

    #endregion

    #region Ctor

    public ProductService(IRepository<Product> productsRepository)
    {
        _productsRepository = productsRepository;
    }

    #endregion

    #region Methods

    public virtual async Task InsertProductAsync(Product product)
    {
        await _productsRepository.InsertAsync(product);
    }

    public virtual async Task UpdateProductAsync(Product product)
    {
        await _productsRepository.UpdateAsync(product);
    }

    public virtual async Task DeleteProductAsync(Product product)
    {
        await _productsRepository.DeleteAsync(product);
    }

    public virtual async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _productsRepository.GetByIdAsync(productId);
    }

    public virtual async Task<IEnumerable<Product>> GetAllProductAsync()
    {
        return await _productsRepository.GetAllAsync();
    }

    public virtual async Task<int> GetTotalProductAsync()
    {
        return await _productsRepository.Table.CountAsync();
    }

    public virtual async Task<List<Product>> GetAllProductsByCategoryId(int categoryId = 0)
    {
        return await _productsRepository.Table
            .Where(x => (categoryId == 0 || x.CategoryId == categoryId))
            .ToListAsync();
    }

    #endregion
}
