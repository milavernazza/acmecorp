using AcmeCorp.Domain.Services;
using AcmeCorp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }    
        [HttpGet]
        public Task<IEnumerable<Customer>> GetAll()
        {
            return _customerService.GetAllCustomersAsync();
        }
        [HttpGet("{id}")]
        public Task<Customer> GetById(int id)
        {
            return _customerService.GetCustomerByIdAsync(id);
        }
        [HttpPost]
        public Task Add(Customer customer)
        {
            return _customerService.AddCustomerAsync(customer);
        }
        [HttpPut]
        public Task Update(Customer customer)
        {
            return _customerService.UpdateCustomerAsync(customer);
        }
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return _customerService.DeleteCustomerAsync(id);
        }
    }
}