using BookingSystem.API.Services;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;
using Moq;
using Xunit;

namespace BookingSystem.Test
{
    public class RoomServiceTests
    {
        private readonly Mock<IRoomRepository> _repoMock;
        private readonly RoomService _service;

        public RoomServiceTests()
        {
            _repoMock = new Mock<IRoomRepository>();
            _service = new RoomService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRooms()
        {
            // Arrange
            var rooms = new List<Room>
            {
                new Room { RoomId = 1, Name = "A" },
                new Room { RoomId = 2, Name = "B" }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(rooms);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("A", result[0].Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRoom_WhenExists()
        {
            // Arrange
            var room = new Room { RoomId = 1, Name = "A" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(room);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("A", result!.Name);
        }
    }
}
