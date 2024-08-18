﻿using OA.Services;
using OA.Core.Domain;
using OA_WEB.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OA_WEB.Areas.Admin.Factories
{
    public class AdminOrderModelFactory : IAdminOrderModelFactory
    {
        #region Fields

        protected readonly ICityService _cityService;
        protected readonly IStateService _stateService;
        protected readonly IOrderService _orderService;
        protected readonly IAccountService _accountService;

        #endregion

        #region Ctor

        public AdminOrderModelFactory(IOrderService orderService,
            IAccountService accountService,
            ICityService cityService,
            IStateService stateService)
        {
            _cityService = cityService;
            _orderService = orderService;
            _stateService = stateService;
            _accountService = accountService;
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
                        StateId = order.StateId,
                        CityId = order.CityId,
                        TotalAmount = order.TotalAmount,
                    };
                }

                var user = await _accountService.GetUserByIdAsync(order.UserId);
                var state = await _stateService.GetStateByIdAsync(order.StateId);
                var city = await _cityService.GetCityByIdAsync(order.CityId);

                model.UserName = user.Name;
                model.PaymentTypeStr = order.PaymentType.ToString();
                model.OrderStatusStr = order.OrderStatus.ToString();
                model.StateName = state.Name;
                model.CityName = city.Name;
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
