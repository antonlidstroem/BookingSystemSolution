using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Test3;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BookingSystem.Test3.IntegrationTests.RealDb
{
    public class RoomIntegrationTests : IntegrationTestBaseRealDb
    {
        public RoomIntegrationTests(WebApplicationFactory<BookingSystem.API.Program> factory)
        : base(factory)
        {

        }

        [Fact]
        public async Task GetRoomsFromWebApi()
        {
            // Arrange

            // Act
            var response = await HttpClient.GetAsync("/api/Rooms");
            var stringResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotNull(stringResult);
            Assert.Contains("Sovrummet", stringResult);
        }
    }
}

