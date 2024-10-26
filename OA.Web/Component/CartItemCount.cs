using Microsoft.AspNetCore.Mvc;
using OA.Core.Domain;
using OA.Services;
using System.Security.Claims;

namespace OA_WEB.Component;

public class CartItemCountViewComponent : ViewComponent
{
    #region Fields

    private readonly IShoppingCartItemService _shoppingCartItemService;

    #endregion

    #region Ctor

    public CartItemCountViewComponent(IShoppingCartItemService shoppingCartItemService)
    {
        _shoppingCartItemService = shoppingCartItemService;
    }

    #endregion

    #region Methods

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userIdClaim, out int userId))
        {
            var items = await _shoppingCartItemService.GetAllCartItemsAsync(userId, (int)ShoppingCartType.ShoppingCart);
            var count = 0;
            foreach (var item in items)
            {
                count += item.Quantity;
            }

            return View(count);
        }

        return View(0); 
    }

    #endregion
}
