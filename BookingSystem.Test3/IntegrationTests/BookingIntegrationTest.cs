using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace BookingSystem.Test3.IntegrationTests
{
    public class BookingIntegrationTests : IntegrationTestBase
    {
        [Fact]
        public async Task GetAllBookings_ReturnsSeededBookings()
        {
            var response = await HttpClient.GetAsync("/api/Bookings");
            response.EnsureSuccessStatusCode();

            var bookings = await response.Content.ReadFromJsonAsync<BookingDto[]>();

            Assert.NotNull(bookings);
            Assert.True(bookings.Length >= 2);
            Assert.Contains(bookings, b => b.RoomId == 1);
            Assert.Contains(bookings, b => b.RoomId == 2);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateBooking_ConflictingBooking_ReturnsConflict()
        {
            var conflictingBooking = new BookingDto
            {
                RoomId = 1,
                CustomerId = 2,
                StartDate = new DateOnly(2024, 7, 1),
                EndDate = new DateOnly(2024, 7, 1)
            };

            var response = await HttpClient.PostAsJsonAsync("/api/Bookings", conflictingBooking);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task CreateBooking_NewBooking_ReturnsCreated()
        {
            var newBooking = new BookingDto
            {
                RoomId = 3,
                CustomerId = 2,
                StartDate = new DateOnly(2024, 7, 1),
                EndDate = new DateOnly(2024, 7, 1)
            };

            var response = await HttpClient.PostAsJsonAsync("/api/Bookings", newBooking);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdBooking = await response.Content.ReadFromJsonAsync<BookingDto>();
            Assert.NotNull(createdBooking);
            Assert.Equal(3, createdBooking.RoomId);
            Assert.Equal(2, createdBooking.CustomerId);
        }
    }
}
