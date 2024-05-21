using AcmeCorp.Domain.Entities;
using AcmeCorp.Domain.Repositories;
using AcmeCorp.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorp.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        //In-memory storage for simplicty 
        private readonly List <Customer> _customers = new();

        public Task AddCustomerAsync(Customer customer)
        {
            _customers.Add(customer);
            return Task.CompletedTask;
        }

        public Task DeleteCustomerAsync(int customerId)
        {
            var customer = _customers.Find(c => c.Id == customerId);
            if (customer != null)
            {
                _customers.Remove(customer);
            }
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return Task.FromResult<IEnumerable<Customer>>(_customers);
        }
        public Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var customer = _customers.Find(c => c.Id == customerId);
            return Task.FromResult(customer);
        }
        public Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = _customers.Find(c => c.Id == customer.Id);
            if (existingCustomer !=null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.ContactInfo = customer.ContactInfo;
                existingCustomer.Orders = customer.Orders;
            }
            return Task.CompletedTask;
        }
    }
}