using Microsoft.AspNetCore.Mvc;
using OA.Core.Domain;
using OA.Services;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        #region Fields

        protected readonly IAccountService _accountService;
        protected readonly ICategoryService _categoryService;
        protected readonly IProductService _productService;
        protected readonly IOrderService _orderService;

        #endregion

        #region Ctor

        public DashBoardController(IAccountService accountService,
            ICategoryService categoryService,
            IProductService productService,
            IOrderService orderService)
        {
            _accountService = accountService;
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrderAsync();
            decimal totalSell = 0;
            foreach (var order in orders)
            {
                if (order.OrderStatusId == (int)OrderStatus.Completed)
                    totalSell += order.TotalAmount;
            }

            var model = new DashboardViewModel
            {
                TotalUsers = await _accountService.GetTotalUsersAsync(),
                TotalCategories = await _categoryService.GetTotalCategoriesAsync(),
                TotalProducts = await _productService.GetTotalProductAsync(),
                TotalOrders = await _orderService.GetTotalOrdersAsync(),
                TotalSell = totalSell
            };

            return View(model);
        }

        #endregion
    }
}
