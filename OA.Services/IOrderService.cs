using OA.Core.Domain;

namespace OA.Services
{
    public interface IOrderService
    {
        Task InsertOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int id);
        Task <IEnumerable<Order>> GetAllOrderAsync();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<int> GetTotalOrdersAsync();
    }
}
