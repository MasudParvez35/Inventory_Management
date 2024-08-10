using OA.Core.Domain;
using OA_WEB.Models;

namespace OA_WEB.Factories
{
    public interface IOrderModelFactory
    {
        Task<IList<OrderModel>> PrepareOrderListModelAsync(IEnumerable<Order> orders);

        Task<OrderModel> PrepareOrderModelAsync(OrderModel model, Order order, bool excludeProperties = false);
    }
}
