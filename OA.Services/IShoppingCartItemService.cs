﻿using OA.Core.Domain;

namespace OA.Services
{
    public interface IShoppingCartItemService
    {
        Task<ShoppingCartItem> GetShoppingCartItemByIdAsync(int shoppingCartItemId);
        Task InsertShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
        Task UpdateShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
        Task DeleteShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);
        Task<IEnumerable<ShoppingCartItem>> GetAllItems();
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsByUserIdAsync(int userId);
    }
}