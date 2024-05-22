# AcmeCorp API

## Overview

AcmeCorp API is a .NET Core web API designed to manage customers, their contact information, and their orders. This project demonstrates the use of SOLID and DDD principles, clean architecture patterns, asynchronous programming, and includes unit and integration tests. The API is containerized using Docker and can be deployed on AWS using infrastructure as code (IaC).

## Project Structure

The project is organized into several layers:

- *AcmeCorp.Api*: The presentation layer, containing API controllers.
- *AcmeCorp.Application*: The application layer, containing business logic and service classes.
- *AcmeCorp.Domain*: The domain layer, containing entities and repository interfaces.
- *AcmeCorp.Infrastructure*: The infrastructure layer, containing repository implementations and data access logic.
- *AcmeCorp.UnitTests*: The unit tests project.
- *AcmeCorp.IntegrationTests*: The integration tests project.

## Domain Entities

### Customer

```csharp
namespace AcmeCorp.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ContactInfo ContactInfo { get; set; } = new ContactInfo();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
```
### Order

```csharp
namespace AcmeCorp.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Customer Customer { get; set; } = new Customer();
    }
}
```

### ContactInfo

```csharp
namespace AcmeCorp.Domain.Entities
{
    public class ContactInfo
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
```

## Repositories

### ICustomerRepository

```csharp
namespace AcmeCorp.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
    }
}
```

### IOrderRepository

```csharp
namespace AcmeCorp.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}
```

## Services

### CustomerService

```csharp
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

        public Task<Customer> GetCustomerByIdAsync(int id)
        {
            return _customerRepository.GetCustomerByIdAsync(id);
        }

        public Task AddCustomerAsync(Customer customer)
        {
            return _customerRepository.AddCustomerAsync(customer);
        }

        public Task UpdateCustomerAsync(Customer customer)
        {
            return _customerRepository.UpdateCustomerAsync(customer);
        }

        public Task DeleteCustomerAsync(int id)
        {
            return _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
```

### OrderService

```csharp
namespace AcmeCorp.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            return _orderRepository.GetOrderByIdAsync(id);
        }

        public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }

        public Task AddOrderAsync(Order order)
        {
            return _orderRepository.AddOrderAsync(order);
        }

        public Task UpdateOrderAsync(Order order)
        {
            return _orderRepository.UpdateOrderAsync(order);
        }

        public Task DeleteOrderAsync(int id)
        {
            return _orderRepository.DeleteOrderAsync(id);
        }
    }
}
```

## API Controllers

### CustomerController

```csharp
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
```

### OrderController

```csharp
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
```

## Running the Application

### Prerequisites

- .NET Core SDK
- Docker

### Build and Run

1. Clone the repository:

bash
git clone https://github.com/milaverazza/acmecorp.git
cd acmecorp


2. Build the solution:

bash
dotnet build


3. Run the application:

bash
dotnet run --project src/AcmeCorp.Api/AcmeCorp.Api.csproj


### Docker

1. Build the Docker image:

bash
docker build -t acmecorpapi -f src/AcmeCorp.Api/Dockerfile .


2. Run the Docker container:

bash
docker run -p 5000:80 acmecorpapi

## Running Tests

To run the unit and integration tests, use the following command:

bash
dotnet test

## Deployment

### AWS Deployment with Terraform

This project includes Terraform configuration for deploying the application to AWS. 

1. Initialize Terraform:

bash
cd terraform
terraform init


2. Apply the Terraform configuration:

bash
terraform apply


This will set up the necessary AWS infrastructure, including ECS, ALB, and other resources, to deploy the AcmeCorp API.

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on the code of conduct, and the process for submitting pull requests to us.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.