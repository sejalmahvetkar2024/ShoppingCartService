using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrder
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(long id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(long userId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(long id);
    }
}
