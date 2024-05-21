using AcmeCorp.Domain.Entities;
using AcmeCorp.Domain.Repositories;
using AcmeCorp.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorp.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return _customerRepository.GetAllCustomersAsync();
        }
        public Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return _customerRepository.GetCustomerByIdAsync(customerId);
        }
          public Task AddCustomerAsync(Customer customer)
        {
            return _customerRepository.AddCustomerAsync(customer);
        }
        public Task UpdateCustomerAsync(Customer customer)
        {
            return _customerRepository.UpdateCustomerAsync(customer);
        }
        public Task DeleteCustomerAsync(int customerId)
        {
            return _customerRepository.DeleteCustomerAsync(customerId);
        }
    }
}