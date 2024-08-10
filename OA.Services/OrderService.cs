using OA.Core.Domain;
using OA.Data;

namespace OA.Services
{
    public class OrderService : IOrderService
    {
        #region Fields

        protected readonly IRepository<Order> _orderRepository;

        #endregion

        #region Ctor

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #endregion

        #region Methods

        public virtual async Task DeleteOrderAsync(Order order)
        {
            await _orderRepository.DeleteAsync(order);
        }

        public virtual async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public virtual async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _orderRepository.FindByAsync(x => x.UserId == userId);
        }

        public virtual async Task InsertOrderAsync(Order order)
        {
            await _orderRepository.InsertAsync(order);
        }

        public virtual async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }

        #endregion
    }
}
