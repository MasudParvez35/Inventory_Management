using OA.Services;
using OA_WEB.Models;
using OA.Core.Domain;

namespace OA_WEB.Factories
{
    public class ShoppingCartModelFactory : IShoppingCartModelFactory
    {
        protected readonly IProductService _productService;
        protected readonly IShoppingCartItemService _shoppingCartItemService;

        public ShoppingCartModelFactory(IShoppingCartItemService shoppingCartItemService, 
            IProductService productService)
        {
            _productService = productService;
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
                        Quantity = shoppingCartItem.Quantity,
                        ShoppingCartTypeId = shoppingCartItem.ShoppingCartTypeId,
                    };

                    // Fetch additional product details
                    var product = await _productService.GetProductByIdAsync(shoppingCartItem.ProductId);
                    if (product != null)
                    {
                        model.ProductName = product.Name;
                        model.ProductImage = product.ImagePath;
                        model.Price = product.SellingPrice;
                    }
                }
            }

            return model;
        }
    }
}
