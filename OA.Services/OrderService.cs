using OA.Core.Domain;
using OA.Data;

namespace OA.Services
{
    public class OrderService : IOrderService
    {
        protected readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public virtual async Task DeleteOrderAsync(Order order)
        {
            await _orderRepository.DeleteAsync(order);
        }

        public virtual async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public virtual async Task<Order> GetByOrderIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertOrderAsync(Order order)
        {
            await _orderRepository.InsertAsync(order);
        }

        public virtual async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }
    }
}
