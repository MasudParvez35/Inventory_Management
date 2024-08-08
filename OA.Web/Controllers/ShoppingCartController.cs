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
        protected readonly IShoppingCartItemService _shoppingCartItemService;
        protected readonly IShoppingCartModelFactory _shoppingCartItemModelFactory;

        public ShoppingCartController(IShoppingCartItemService shoppingCartItemService,
            IShoppingCartModelFactory shoppingCartItemModelFactory)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _shoppingCartItemModelFactory = shoppingCartItemModelFactory;
        }

        public async Task<IActionResult> List()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    var shoppingCartItems = await _shoppingCartItemService.GetShoppingCartItemsByUserIdAsync(userId);

                    var model = await _shoppingCartItemModelFactory.PrepareShoppingCartItemListModelAsync(shoppingCartItems);

                    return View(model);
                }
                else
                {
                    TempData["errorMessage"] = "Failed to retrieve user ID.";
                }
            }
            else
            {
                TempData["errorMessage"] = "User is not logged in.";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(ShoppingCartItemModel model)
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
                        Quantity = model.Quantity
                    };

                    await _shoppingCartItemService.InsertShoppingCartItemAsync(cart);

                    TempData["successMessage"] = "Item added to cart successfully!";

                    return RedirectToAction("List");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to retrieve user ID.";
                }
            }
            else
            {
                TempData["errorMessage"] = "Invalid cart item details.";
            }

            model = await _shoppingCartItemModelFactory.PrepareShoppingCartItemModelAsync(model, null);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(ShoppingCartItem cart, ShoppingCartItemModel model)
        {
            var ShoppingCartItem = await _shoppingCartItemService.GetShoppingCartItemByIdAsync(model.UserId);

            if (ShoppingCartItem != null)
            {
                //ShoppingCartItem.Quantity = item.Quantity;
                await _shoppingCartItemService.UpdateShoppingCartItemAsync(ShoppingCartItem);
            }

            TempData["successMessage"] = "Cart updated successfully!";

            return RedirectToAction("List"); 
        }
    }
}
