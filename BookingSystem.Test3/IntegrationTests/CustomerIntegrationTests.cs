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
    }
}
