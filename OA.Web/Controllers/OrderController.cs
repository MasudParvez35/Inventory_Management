using OA.Services;
using OA_WEB.Models;
using OA.Core.Domain;
using OA_WEB.Factories;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace OA_WEB.Controllers
{
    public class OrderController : Controller
    {
        #region Fields

        protected readonly IOrderService _orderService;
        protected readonly IProductService _productService;
        protected readonly IOrderModelFactory _orderModelFactory;
        protected readonly IShoppingCartItemService _shoppingCartItemService;

        #endregion

        #region Ctor

        public OrderController(IOrderModelFactory orderModelFactory,
            IShoppingCartItemService shoppingCartItemService,
            IOrderService orderService,
            IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
            _orderModelFactory = orderModelFactory;
            _shoppingCartItemService = shoppingCartItemService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Myorder()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);
                var model = await _orderModelFactory.PrepareOrderListModelAsync(orders);
                return View(model); 
            }

            return RedirectToAction("MyOrder");
        }

        public async Task<IActionResult> Create()
        {
            var model = await _orderModelFactory.PrepareOrderModelAsync(new OrderModel(), null);

            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    model.UserId = userId;
                }
            }

            var cartItems = await _shoppingCartItemService.GetAllCartItemsAsync(model.UserId);
            decimal totalAmount = 0;
            foreach (var item in cartItems)
            {
                var porduct = await _productService.GetProductByIdAsync(item.ProductId);
                totalAmount += porduct.SellingPrice * item.Quantity;
            }

            model.TotalAmount = totalAmount;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    UserId = model.UserId,
                    PaymentTypeId = model.PaymentTypeId,
                    OrderStatusId = model.OrderStatusId,
                    MobileNumber = model.MobileNumber,
                    TransactionId = model.TransactionId,
                    StateId = model.StateId,
                    CityId = model.CityId,
                    TotalAmount = model.TotalAmount
                };

                await _orderService.InsertOrderAsync(order);

                var cartItems = await _shoppingCartItemService.GetAllCartItemsAsync(model.UserId);
                foreach (var item in cartItems)
                {
                    await _shoppingCartItemService.DeleteShoppingCartItemAsync(item);
                }

                return RedirectToAction("MyOrder", "Order");
            }

            model = await _orderModelFactory.PrepareOrderModelAsync(model, null);

            return View(model);
        }

        #endregion
    }
}
