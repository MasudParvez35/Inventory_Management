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
                    var shoppingCartItems = await _shoppingCartItemService.GetAllCartItemAsync(userId);
                    var model = await _shoppingCartItemModelFactory.PrepareShoppingCartItemListModelAsync(shoppingCartItems);

                    return View(model);
                }
            }

            return RedirectToAction("CartList");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out int userId))
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

                    return RedirectToAction("Wishlist");
                }
            }

            return RedirectToAction("Wishlist");
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
                    var wiahlistItems = await _shoppingCartItemService.GetAllWishlistItemAsync(userId);
                    var model = await _shoppingCartItemModelFactory.PrepareShoppingCartItemListModelAsync(wiahlistItems);

                    return View(model);
                }
            }

            return RedirectToAction("Wishlist");
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(ShoppingCartItemModel model)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out int userId))
                {
                    var cart = new ShoppingCartItem()
                    {
                        UserId = userId,
                        ProductId = model.ProductId,
                        Quantity = model.Quantity,
                        ShoppingCartTypeId = (int)ShoppingCartType.Wishlist,
                    };

                    await _shoppingCartItemService.InsertShoppingCartItemAsync(cart);
                    TempData["successMessage"] = "Item added to cart successfully!";

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

        #endregion
    }

    #endregion
}
