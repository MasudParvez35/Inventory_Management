using OA.Core.Domain;
using OA.Data;

namespace OA.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        protected readonly IRepository<ShoppingCartItem> _shoppingCartRepository;

        public ShoppingCartItemService(IRepository<ShoppingCartItem> shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public virtual async Task DeleteShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
        {
            await _shoppingCartRepository.DeleteAsync(shoppingCartItem);
        }

        public virtual async Task InsertShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
        {
            await _shoppingCartRepository.InsertAsync(shoppingCartItem);
        }

        public virtual async Task UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
        {
            await _shoppingCartRepository.UpdateAsync(shoppingCartItem);
        }
    }
}
