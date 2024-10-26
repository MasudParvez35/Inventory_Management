using OA.Core.Domain;

namespace OA.Services;

public interface IShoppingCartItemService
{
    Task<ShoppingCartItem> GetShoppingCartItemByIdAsync(int shoppingCartItemId);

    Task InsertShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);

    Task UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);

    Task DeleteShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);

    Task<IEnumerable<ShoppingCartItem>> GetAllCartItemsAsync(int userId = 0, int cartId = 0, int productId = 0);
}
