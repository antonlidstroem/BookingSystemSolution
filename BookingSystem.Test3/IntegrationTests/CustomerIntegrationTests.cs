using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BookingSystem.DTO.DTO;
using Xunit;

namespace BookingSystem.Test3.IntegrationTests
{
    public class CustomerIntegrationTests : IntegrationTestBase
    {
        [Fact]
        public async Task CustomerWithId2_HasNameBob()
        {
            var response = await HttpClient.GetAsync("/api/Customers/2");
            response.EnsureSuccessStatusCode();

            var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();

            Assert.NotNull(customer);
            Assert.Equal(2, customer.CustomerId);
            Assert.Equal("Bob", customer.Name); // matchar SeedHelper
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateCustomer_NewCustomer_ReturnsCreated()
        {
            var newCustomer = new CustomerDto
            {
                Name = "Charlie"
            };
            var response = await HttpClient.PostAsJsonAsync("/api/Customers", newCustomer);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var createdCustomer = await response.Content.ReadFromJsonAsync<CustomerDto>();
            Assert.NotNull(createdCustomer);
            Assert.Equal("Charlie", createdCustomer.Name);
        }

        [Fact]
        public async Task DeleteCustomer_CustomerWithId1_ReturnsNoContent()
        {
            var response = await HttpClient.DeleteAsync("/api/Customers/1");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var getResponse = await HttpClient.GetAsync("/api/Customers/1");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
        [Fact]
        public async Task UpdateCustomer_CustomerWithId2_ReturnsUpdatedCustomer()
        {
            var updatedCustomer = new CustomerDto
            {
                CustomerId = 2,
                Name = "Robert"
            };
            var response = await HttpClient.PutAsJsonAsync("/api/Customers/2", updatedCustomer);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var returnedCustomer = await response.Content.ReadFromJsonAsync<CustomerDto>();
            Assert.NotNull(returnedCustomer);
            Assert.Equal(2, returnedCustomer.CustomerId);
            Assert.Equal("Robert", returnedCustomer.Name);
        }

        
    }
}
