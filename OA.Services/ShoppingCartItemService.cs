using OA.Core.Domain;
using OA.Data;
using System.Linq.Expressions;

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

        public virtual async Task<IEnumerable<ShoppingCartItem>> GetAllCartItemAsync(int userId)
        {
            Expression<Func<ShoppingCartItem, bool>> predicate = item => item.UserId == userId;
            predicate = item => item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart;

            // Use the repository to find items by the predicate
            return await _shoppingCartRepository.FindByAsync(predicate);
        }

        public virtual async Task<IEnumerable<ShoppingCartItem>> GetAllWishlistItemAsync(int userId)
        {
            Expression<Func<ShoppingCartItem, bool>> predicate = item => item.UserId == userId;
            predicate = item => item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist;

            // Use the repository to find items by the predicate
            return await _shoppingCartRepository.FindByAsync(predicate);
        }

        public virtual async Task<IEnumerable<ShoppingCartItem>> GetAllItems()
        {
            return await _shoppingCartRepository.GetAllAsync();
        }

        public virtual async Task<ShoppingCartItem> GetShoppingCartItemByIdAsync(int shoppingCartItemId)
        {
            return await _shoppingCartRepository.GetByIdAsync(shoppingCartItemId);
        }

        public virtual async Task<IEnumerable<ShoppingCartItem>> GetAllShoppingCartByUserIdAsync(int userId)
        {
            // Define the predicate to filter by userId
            Expression<Func<ShoppingCartItem, bool>> predicate = item => item.UserId == userId;

            // Use the repository to find items by the predicate
            return await _shoppingCartRepository.FindByAsync(predicate);
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
