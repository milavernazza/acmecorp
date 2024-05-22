using AcmeCorp.Api;
using AcmeCorp.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AcmeCorp.IntegrationTests.Controllers
{
    public class CustomerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CustomerControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ShouldReturnCustomers()
        {
            var response = await _client.GetAsync("/api/customer");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<Customer>>(responseString);
            Assert.NotEmpty(customers);
        }

        [Fact]
        public async Task Add_ShouldAddCustomer()
        {
            var customer = new Customer { Name = "John Doe" };
            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/customer", content);
            response.EnsureSuccessStatusCode();
        }
    }
}