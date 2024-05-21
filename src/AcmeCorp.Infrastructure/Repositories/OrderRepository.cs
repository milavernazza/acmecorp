using AcmeCorp.Domain.Entities;
using AcmeCorp.Domain.Repositories;
using AcmeCorp.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorp.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        //In-memory storage for simplicty 
        private readonly List <Order> _orders = new();

        public Task AddOrderAsync(Order order)
        {
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public Task DeleteOrderAsync(int orderId)
        {
            var order = orders.Find(o => o.Id == orderId);
            if (order != null)
            {
                _orders.Remove(order);
            }
            return Task.CompletedTask;
        }
        public Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = _orders.Find(o => o.Id == orderId);
            return Task.FromResult(order);
        }
        public Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            var orders = _orders.FindAll(o => o.CustomerId == customerId);
            return Task.FromResult<IEnumerable<Order>>(orders);
        }
        public Task UpdateOrderAsync(Order order)
        {
            var existingOrder = orders.Find(o => o.Id == order.Id);
            if (existingOrder !=null)
            {
                existingOrder.ProductName = order.ProductName;
                existingOrder.Quantity = customer.Quantity;
                existingOrder.Price = customer.Price;
            }
            return Task.CompletedTask;
        }