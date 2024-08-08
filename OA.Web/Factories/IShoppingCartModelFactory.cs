using OA.Core.Domain;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public interface IShoppingCartModelFactory
    {
        Task<IList<ShoppingCartItemModel>> PrepareShoppingCartItemListModelAsync(IEnumerable<ShoppingCartItem> shoppingCartItems);
        Task<ShoppingCartItemModel> PrepareShoppingCartItemModelAsync(ShoppingCartItemModel model, ShoppingCartItem shoppingCartItem, bool excludeProperties = false);
    }
}
