using OA.Data;
using OA.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace OA.Services;

public class ShoppingCartItemService : IShoppingCartItemService
{
    protected readonly IRepository<ShoppingCartItem> _shoppingCartRepository;

    public ShoppingCartItemService(IRepository<ShoppingCartItem> shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }

    #region Methods

    public virtual async Task DeleteShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
    {
        await _shoppingCartRepository.DeleteAsync(shoppingCartItem);
    }

    public virtual async Task<IEnumerable<ShoppingCartItem>> GetAllCartItemsAsync(int userId = 0, int cartId = 0, int productId = 0)
    {
        return await _shoppingCartRepository.Table
            .Where(x => (userId == 0 || x.UserId == userId) && 
                    (cartId == 0 || x.ShoppingCartTypeId == cartId) &&
                    (productId == 0 || x.ProductId == productId))
            .ToListAsync();
    }

    public virtual async Task<ShoppingCartItem> GetShoppingCartItemByIdAsync(int shoppingCartItemId)
    {
        return await _shoppingCartRepository.GetByIdAsync(shoppingCartItemId);
    }

    public virtual async Task InsertShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
    {
        await _shoppingCartRepository.InsertAsync(shoppingCartItem);
    }

    public virtual async Task UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
    {
        await _shoppingCartRepository.UpdateAsync(shoppingCartItem);
    }

    #endregion
}
