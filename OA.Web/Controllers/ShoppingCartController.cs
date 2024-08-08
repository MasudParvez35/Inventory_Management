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
            var cartItems = await _shoppingCartItemService.GetAllItems();
            var model = await _shoppingCartItemModelFactory.PrepareShoppingCartItemListModelAsync(cartItems);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(ShoppingCartItemModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user ID from the claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    // Create a new shopping cart item with the logged-in user ID
                    var cart = new ShoppingCartItem()
                    {
                        UserId = userId, // Use the user ID from the claims
                        ProductId = model.ProductId,
                        Quantity = model.Quantity
                    };

                    await _shoppingCartItemService.InsertShoppingCartItemAsync(cart);

                    TempData["successMessage"] = "Item added to cart successfully!";

                    return RedirectToAction("List");
                }
                else
                {
                    // Handle the case where the user ID is not valid
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
    }
}
