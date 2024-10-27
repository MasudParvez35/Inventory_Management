using OA.Core.Domain;

namespace OA.Services;

public interface IProductService
{
    Task InsertProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Product product);
    Task<Product> GetProductByIdAsync(int productId);
    Task<IEnumerable<Product>> GetAllProductAsync();
    Task<int> GetTotalProductAsync();
    Task<List<Product>> GetAllProductsByCategoryId(int categoryId = 0);
}
