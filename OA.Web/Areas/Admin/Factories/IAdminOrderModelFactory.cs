using OA.Core.Domain;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories
{
    public interface IAdminOrderModelFactory
    {
        Task<IList<OrderModel>> PrepareOrderListModelAsync(IEnumerable<Order> orders);

        Task<OrderModel> PrepareOrderModelAsync(OrderModel model, Order order, bool excludeProperties = false);
    }
}
