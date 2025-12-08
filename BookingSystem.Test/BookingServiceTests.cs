using BookingSystem.API.Services;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;
using Moq;
using Xunit;

namespace BookingSystem.Test
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _repoMock;
        private readonly BookingService _service;

        public BookingServiceTests()
        {
            _repoMock = new Mock<IBookingRepository>();
            _service = new BookingService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBooking_WhenRoomAvailable()
        {
            // Arrange
            var dto = new BookingDto
            {
                BookingId = 0,
                RoomId = 1,
                StartTime = DateTime.Today.AddHours(10),
                EndTime = DateTime.Today.AddHours(12)
            };

            _repoMock.Setup(r => r.GetBookingsForRoomAsync(dto.RoomId))
                     .ReturnsAsync(new List<Booking>()); // inga bokningar ännu

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.RoomId, result.RoomId);
        }

        [Fact]
        public async Task IsRoomAvailableAsync_ShouldReturnFalse_WhenOverlap()
        {
            // Arrange
            var existing = new Booking
            {
                RoomId = 1,
                StartTime = DateTime.Today.AddHours(10),
                EndTime = DateTime.Today.AddHours(12)
            };

            _repoMock.Setup(r => r.GetBookingsForRoomAsync(1))
                     .ReturnsAsync(new List<Booking> { existing });

            // Act
            var available = await _service.IsRoomAvailableAsync(1, DateTime.Today.AddHours(11), DateTime.Today.AddHours(13));

            // Assert
            Assert.False(available);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllBookings()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new Booking { BookingId = 1, RoomId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) },
                new Booking { BookingId = 2, RoomId = 2, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(bookings);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
