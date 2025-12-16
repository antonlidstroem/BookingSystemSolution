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
            // Arrange
            // Act
            var response = await HttpClient.GetAsync("/api/Customers/2");
            response.EnsureSuccessStatusCode();
            var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(2, customer.CustomerId);
            Assert.Equal("Bob", customer.Name); // matchar SeedHelper
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateCustomer_NewCustomer_ReturnsCreated()
        {
            // Arrange
            var newCustomer = new CustomerDto
            {
                Name = "Charlie"
            };

            // Act
            var response = await HttpClient.PostAsJsonAsync("/api/Customers", newCustomer);
            var createdCustomer = await response.Content.ReadFromJsonAsync<CustomerDto>();

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(createdCustomer);
            Assert.Equal("Charlie", createdCustomer.Name);
        }

        [Fact]
        public async Task DeleteCustomer_CustomerWithId1_ReturnsNoContent()
        {
            // Arrange
            var customerId = 1;
            
            // Act
            var response = await HttpClient.DeleteAsync($"/api/Customers/{customerId}");
       
            var getResponse = await HttpClient.GetAsync($"/api/Customers/{customerId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
        [Fact]
        public async Task UpdateCustomer_CustomerWithId2_ReturnsUpdatedCustomer()
        {
            // Arrange
            var updatedCustomer = new CustomerDto
            {
                CustomerId = 2,
                Name = "Robert"
            };
            
            // Act
            var response = await HttpClient.PutAsJsonAsync("/api/Customers/2", updatedCustomer);
            var returnedCustomer = await response.Content.ReadFromJsonAsync<CustomerDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(returnedCustomer);
            Assert.Equal(2, returnedCustomer.CustomerId);
            Assert.Equal("Robert", returnedCustomer.Name);
        }

        
    }
}
