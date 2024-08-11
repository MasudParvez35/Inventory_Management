using OA.Core.Domain;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories
{
    public interface IAdminProductModelFactory
    {
        Task<IList<ProductModel>> PrepareProductListModelAsync(IEnumerable<Product> products);

        Task<ProductModel> PrepareProductModelAsync(ProductModel model, Product product, bool excludeProperties = false);
    }
}
