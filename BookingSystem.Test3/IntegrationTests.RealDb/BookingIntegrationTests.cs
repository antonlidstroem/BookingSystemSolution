using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Test3;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BookingSystem.Test3.IntegrationTests.RealDb
{
    public class BookingIntegrationTests : IntegrationTestBaseRealDb
    {
        public BookingIntegrationTests(WebApplicationFactory<BookingSystem.API.Program> factory)
        : base(factory)
        {

        }

        [Fact]
        public async Task GetBookingsFromWebApi()
        {
            // Arrange


            // Act
            var response = await HttpClient.GetAsync("/api/Bookings");
            response.EnsureSuccessStatusCode();

            var bookings = await response.Content.ReadFromJsonAsync<List<BookingDto>>(); 

            // Assert
            Assert.NotNull(bookings);
            Assert.All(bookings, b =>
            {
                Assert.True(b.RoomId > 0);
                Assert.True(b.CustomerId > 0);

            });

        }

        [Fact]
        public async Task GetBookingsByRoomId()
        {
            // Arrange
            int roomId = 1;

            // Act
            var response = await HttpClient.GetAsync($"/api/Bookings/room/{roomId}");
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadFromJsonAsync<List<BookingDto>>();

            // Assert
            Assert.NotNull(stringResult);
            Assert.All(stringResult, b => Assert.Equal(roomId, b.RoomId));
        }

        [Fact]
        public async Task GetBookingById()
        {
            // Arrange
            var bookings = await HttpClient.GetFromJsonAsync<List<BookingDto>>("/api/Bookings");
            Assert.NotEmpty(bookings);

            var bookingId = bookings!.First().BookingId;

            // Act
            var response = await HttpClient.GetAsync($"/api/Bookings/{bookingId}");
            response.EnsureSuccessStatusCode();

            var booking = await response.Content.ReadFromJsonAsync<BookingDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(booking);
            Assert.Equal(bookingId, booking!.BookingId);
        }
    }
}

