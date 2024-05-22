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
    public class OrderControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public OrderControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetById_ShouldReturnOrder()
        {
            var response = await _client.GetAsync("/api/order/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(responseString);
            Assert.NotNull(order);
        }

        [Fact]
        public async Task Add_ShouldAddOrder()
        {
            var order = new Order { ProductName = "Product A", CustomerId = 1, Quantity = 1, Price = 9.99M };
            var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/order", content);
            response.EnsureSuccessStatusCode();
        }
    }
}