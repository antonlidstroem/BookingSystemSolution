using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BookingSystem.Test3.IntegrationTests.RealDb
{
    public class CustomerIntegrationTests : IntegrationTestBaseRealDb
    {
        public CustomerIntegrationTests(WebApplicationFactory<BookingSystem.API.Program> factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task GetCustomersFromWebApi()
        {
            // Arrange

            // Act
            var response = await HttpClient.GetAsync("/api/Customers");
            var stringResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(stringResult);
            Assert.Contains("Anton", stringResult);
        }
    }
}
