using Microsoft.AspNetCore.Mvc;
using OA.Services;
using System.Security.Claims;

namespace OA_WEB.Component
{
    public class WishlistItemCount : ViewComponent
    {
        #region Fields

        private readonly IShoppingCartItemService _shoppingCartItemService;

        #endregion

        #region Ctor

        public WishlistItemCount(IShoppingCartItemService shoppingCartItemService)
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
                var items = await _shoppingCartItemService.GetAllWishlistItemByUserIdAsync(userId);
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
}
