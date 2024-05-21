using AcmeCorp.Domain.Entities;
using AcmeCorp.Domain.Repositories;
using AcmeCorp.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorp.Application.Services
{
    public class OrderrService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
       public Task<Order> GetOrderByIdAsync(int orderId)
        {
            return _orderRepository.GetOrderByIdAsync(orderId);
        }
        public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }
          public Task AddOrderAsync(Order order)
        {
            return _orderrRepository.AddOrderAsync(order);
        }
        public Task UpdateOrderAsync(Order order)
        {
            return _orderRepository.UpdateOrderAsync(order);
        }
        public Task DeleteOrderAsync(int orderId)
        {
            return _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}