using AcmeCorp.Domain.Services;
using AcmeCorp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }    
        [HttpGet("{id}")]
        public Task<Order> GetById(int id)
        {
            return _orderService.GetOrderByIdAsync(id);
        }
        [HttpGet("customer/{customerId}")]
        public Task<IEnumerable<Order>> GetByCustomerId(int customerId)
        {
            return _orderService.GetOrdersByCustomerIdAsync(customerId);
        }
        [HttpPost]
        public Task Add(Order order)
        {
            return _orderService.AddOrderAsync(order);
        }
        [HttpPut]
        public Task Update(Order order)
        {
            return _orderService.UpdateOrderAsync(order);
        }
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return _orderService.DeleteOrderAsync(id);
        }
    }
}