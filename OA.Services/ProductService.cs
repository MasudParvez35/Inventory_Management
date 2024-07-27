using OA.Core.Domain;
using OA.Data;

namespace OA.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productsRepository;

        public ProductService(IRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task InsertProductAsync(Product product)
        {
            await _productsRepository.InsertAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productsRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await _productsRepository.DeleteAsync(product);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productsRepository.GetByIdAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _productsRepository.GetAllAsync();
        }
    }
}
