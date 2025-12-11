using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Identity.Client;

namespace BookingSystem.Test3.IntegrationTests
{
    public class BookingIntegrationalTest
    {
        [Fact]
        public async Task CreateNewBooking()
        {
            var webAppFactory = new WebApplicationFactory<BookingSystem.API.Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync("/api/bookings");
            var stringResult = await response.Content.ReadAsStringAsync();

            //Assert.Contains()

            // Arrange
            // Act
            // Assert

        }
    }
}
