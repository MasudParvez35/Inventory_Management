using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Core.Domain;
using OA.Services;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public class OrderModelFactory : IOrderModelFactory
    {
        #region Fields

        protected readonly IOrderService _orderService;

        #endregion

        #region Ctor

        public OrderModelFactory(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        #region Methods

        public async Task<IList<OrderModel>> PrepareOrderListModelAsync(IEnumerable<Order> orders)
        {
            var model = new List<OrderModel>();

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var orderModel = await PrepareOrderModelAsync(null, order);
                    model.Add(orderModel);
                }
            }

            return model;
        }

        public async Task<OrderModel> PrepareOrderModelAsync(OrderModel model, Order order, bool excludeProperties = false)
        {
            if (order != null)
            {
                if (model == null)
                {
                    model = new OrderModel
                    {
                        Id = order.Id,
                        UserId = order.UserId,
                        PaymentTypeId = order.PaymentTypeId,
                        OrderStatusId = order.OrderStatusId,
                        MobileNumber = order.MobileNumber,
                        TransactionId = order.TransactionId,
                        Address = order.Address,
                        TotalAmount = order.TotalAmount,
                    };
                }

                model.PaymentTypeStr = order.PaymentType.ToString();
                model.OrderStatusStr = order.OrderStatus.ToString();
            }

            if (!excludeProperties)
            {
                model.AvailablePaymentType = Enum.GetValues(typeof(PaymentType))
                    .Cast<PaymentType>()
                    .Select(x => new SelectListItem
                    {
                        Text = x.ToString(),
                        Value = ((int)x).ToString()
                    }).ToList();
            }

            return model;
        }

        #endregion
    }
}
