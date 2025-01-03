﻿using OA.Services;
using OA.Core.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OA_WEB.Areas.Admin.Models;
using OA_WEB.Areas.Admin.Factories;

namespace OA_WEB.Areas.Admin.Controllers;

[Area("Admin")]
public class OrderController : Controller
{
    #region Fields

    protected readonly IOrderService _orderService;
    protected readonly IProductService _productService;
    protected readonly IAdminOrderModelFactory _orderModelFactory;
    protected readonly IShoppingCartItemService _shoppingCartItemService;

    #endregion

    #region Ctor

    public OrderController(IAdminOrderModelFactory orderModelFactory,
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

    public async Task<IActionResult> List()
    {
        var orders = await _orderService.GetAllOrderAsync();
        var model = await _orderModelFactory.PrepareOrderListModelAsync(orders);

        return View(model);
    }

    public async Task<IActionResult> Myorder()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userIdClaim, out int userId))
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            var model = await _orderModelFactory.PrepareOrderListModelAsync(orders);

            return View(model); 
        }

        return RedirectToAction("List");
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
                Phone = model.MobileNumber,
                TransactionNumber = model.TransactionId,
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

            return RedirectToAction("List", "Order");
        }

        model = await _orderModelFactory.PrepareOrderModelAsync(model, null);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Capture(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order != null)
        {
            order.OrderStatusId = (int)OrderStatus.Completed; 
            await _orderService.UpdateOrderAsync(order); 
        }

        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
            return RedirectToAction("List");

        await _orderService.DeleteOrderAsync(order);

        return RedirectToAction("List");
    }

    #endregion
}
