using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OA.Core.Domain;
using OA.Services;
using OA_WEB.Factories;
using OA_WEB.Models;
using System.Security.Claims;

namespace OA_WEB.Controllers
{
    public class ShoppingCartController : Controller
    {
        #region Fields

        protected readonly IShoppingCartItemService _shoppingCartItemService;
        protected readonly IShoppingCartModelFactory _shoppingCartItemModelFactory;

        #endregion

        #region Ctor

        public ShoppingCartController(IShoppingCartItemService shoppingCartItemService,
            IShoppingCartModelFactory shoppingCartItemModelFactory)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _shoppingCartItemModelFactory = shoppingCartItemModelFactory;
        }

        #endregion

        #region Methods

        #region Shopping Cart

        public async Task<IActionResult> CartList()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    var shoppingCartItems = await _shoppingCartItemService.GetAllCartItemByUserIdAsync(userId);
                    var model = await _shoppingCartItemModelFactory.PrepareShoppingCartItemListModelAsync(shoppingCartItems);

                    return View(model);
                }
            }

            return RedirectToAction("CartList");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out int userId))
                {
                    // Check if the item already exists in the user's cart
                    var existingCartItem = (await _shoppingCartItemService.GetAllCartItemByUserIdAsync(userId))
                                            .FirstOrDefault(item => item.ProductId == productId && item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart);

                    if (existingCartItem != null)
                    {
                        existingCartItem.Quantity += quantity;
                        await _shoppingCartItemService.UpdateShoppingCartItemAsync(existingCartItem);
                    }
                    else
                    {
                        var cart = new ShoppingCartItem()
                        {
                            UserId = userId,
                            ProductId = productId,
                            Quantity = quantity,
                            ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart,
                        };

                        await _shoppingCartItemService.InsertShoppingCartItemAsync(cart);
                        TempData["successMessage"] = "Item added to cart successfully!";
                    }

                    return RedirectToAction("CartList");
                }
            }

            return RedirectToAction("CartList");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCartItem(int id, int quantity)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                var shoppingCartItem = await _shoppingCartItemService.GetShoppingCartItemByIdAsync(id);

                if (shoppingCartItem != null && shoppingCartItem.UserId == userId)
                {
                    shoppingCartItem.Quantity = quantity;

                    await _shoppingCartItemService.UpdateShoppingCartItemAsync(shoppingCartItem);
                }
            }

            return RedirectToAction("CartList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var shoppingCartItem = await _shoppingCartItemService.GetShoppingCartItemByIdAsync(id);

            if (shoppingCartItem != null)
            {
                await _shoppingCartItemService.DeleteShoppingCartItemAsync(shoppingCartItem);

                TempData["successMessage"] = "Item removed from cart successfully!";
            }

            return RedirectToAction("CartList");
        }

        #endregion

        #region Wishlist

        public async Task<IActionResult> Wishlist()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    var wiahlistItems = await _shoppingCartItemService.GetAllWishlistItemByUserIdAsync(userId);
                    var model = await _shoppingCartItemModelFactory.PrepareShoppingCartItemListModelAsync(wiahlistItems);

                    return View(model);
                }
            }

            return RedirectToAction("Wishlist");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToWishlist(ShoppingCartItemModel model)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out int userId))
                {
                    var existingWishlistItem = (await _shoppingCartItemService.GetAllWishlistItemByUserIdAsync(userId))
                                       .FirstOrDefault(item => item.ProductId == model.ProductId && item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist);

                    if (existingWishlistItem != null)
                    {
                        // Update the quantity of the existing item
                        existingWishlistItem.Quantity += model.Quantity;
                        await _shoppingCartItemService.UpdateShoppingCartItemAsync(existingWishlistItem);
                        TempData["successMessage"] = "Wishlist item quantity updated successfully!";
                    }
                    else
                    { 
                        var wishList = new ShoppingCartItem()
                        {
                            UserId = userId,
                            ProductId = model.ProductId,
                            Quantity = model.Quantity,
                            ShoppingCartTypeId = (int)ShoppingCartType.Wishlist,
                        };

                    await _shoppingCartItemService.InsertShoppingCartItemAsync(wishList);
                    TempData["successMessage"] = "Item added to cart successfully!";
                }

                    return RedirectToAction("Wishlist");
                }
            }

            model = await _shoppingCartItemModelFactory.PrepareShoppingCartItemModelAsync(model, null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWishlistItem(int id, int quantity)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                var shoppingCartItem = await _shoppingCartItemService.GetShoppingCartItemByIdAsync(id);

                if (shoppingCartItem != null && shoppingCartItem.UserId == userId)
                {
                    shoppingCartItem.Quantity = quantity;

                    await _shoppingCartItemService.UpdateShoppingCartItemAsync(shoppingCartItem);
                }
            }

            return RedirectToAction("Wishlist");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int id)
        {
            var shoppingCartItem = await _shoppingCartItemService.GetShoppingCartItemByIdAsync(id);

            if (shoppingCartItem != null)
            {
                await _shoppingCartItemService.DeleteShoppingCartItemAsync(shoppingCartItem);

                TempData["successMessage"] = "Item removed from cart successfully!";
            }

            return RedirectToAction("Wishlist");
        }

        public async Task<IActionResult> MoveToCart(int id)
        {
            var shoppingCartItem = await _shoppingCartItemService.GetShoppingCartItemByIdAsync(id);

            if (shoppingCartItem != null)
            {
                shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;

                await _shoppingCartItemService.UpdateShoppingCartItemAsync(shoppingCartItem);

                TempData["successMessage"] = "Item moved to cart successfully!";
            }

            return RedirectToAction("Wishlist");
        }
        
        #endregion
    }

    #endregion
}
