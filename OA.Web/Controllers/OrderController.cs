using Microsoft.AspNetCore.Mvc;
using OA.Core.Domain;
using OA.Services;
using OA_WEB.Factories;
using OA_WEB.Models;
using System.Security.Claims;

namespace OA_WEB.Controllers
{
    public class OrderController : Controller
    {
        #region Fields

        protected readonly IOrderModelFactory _orderModelFactory;
        protected readonly IOrderService _orderService;

        #endregion

        #region Ctor

        public OrderController(IOrderModelFactory orderModelFactory, 
            IOrderService orderService)
        {
            _orderModelFactory = orderModelFactory;
            _orderService = orderService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> List()
        {
            var orders = await _orderService.GetAllOrderAsync();
            var model = await _orderModelFactory.PrepareOrderListModelAsync(orders);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _orderModelFactory.PrepareOrderModelAsync(new OrderModel(), null);

            if (model.AvailableOrderStatus.Any())
            {
                model.OrderStatusId = int.Parse(model.AvailableOrderStatus.First().Value);
            }

            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    model.UserId = userId;
                }
            }

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
                    Address = model.Address
                };

                await _orderService.InsertOrderAsync(order);

                return RedirectToAction("List", "Order");
            }

            model = await _orderModelFactory.PrepareOrderModelAsync(model, null);

            return View(model);
        }

        #endregion
    }
}
