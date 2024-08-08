using OA.Core.Domain;
using OA.Services;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public class ShoppingCartModelFactory : IShoppingCartModelFactory
    {
        protected readonly IShoppingCartItemService _shoppingCartItemService;

        public ShoppingCartModelFactory(IShoppingCartItemService shoppingCartItemService)
        {
            _shoppingCartItemService = shoppingCartItemService;
        }

        public async Task<IList<ShoppingCartItemModel>> PrepareShoppingCartItemListModelAsync(IEnumerable<ShoppingCartItem> shoppingCartItems)
        {
            var model = new List<ShoppingCartItemModel>();

            foreach (var shoppingCartItem in shoppingCartItems)
                model.Add(await PrepareShoppingCartItemModelAsync(null, shoppingCartItem));

            return model;
        }

        public async Task<ShoppingCartItemModel> PrepareShoppingCartItemModelAsync(ShoppingCartItemModel model, ShoppingCartItem shoppingCartItem, bool excludeProperties = false)
        {
            if (shoppingCartItem != null)
            {
                if (model == null)
                {
                    model = new ShoppingCartItemModel()
                    {
                        Id = shoppingCartItem.Id,
                        UserId = shoppingCartItem.UserId,
                        ProductId = shoppingCartItem.ProductId,
                        Quantity = shoppingCartItem.Quantity
                    };
                }
            }

            return model;
        }
    }
}
