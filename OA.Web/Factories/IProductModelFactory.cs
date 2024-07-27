using OA.Core.Domain;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public interface IProductModelFactory
    {
        Task<IList<ProductModel>> PrepareProductListModelAsync(IEnumerable<Product> categories);

        Task<ProductModel> PrepareProductModelAsync(ProductModel model, Product product, bool excludeProperties = false);
    }
}
