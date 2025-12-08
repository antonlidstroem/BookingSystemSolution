using BookingSystem.API.Services;
using BookingSystem.DAL.Repositories;
using BookingSystem.DTO.DTO;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookingSystem.Test
{
    public class RoomServiceIntegrationTests
    {
        private RoomService GetService(out TestDbContext context)
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new TestDbContext(options);
            var repo = new RoomRepository(context);
            return new RoomService(repo);
        }

        [Fact]
        public async Task CreateAndDeleteRoom_ShouldWork()
        {
            var service = GetService(out var context);

            var dto = new RoomDto { Name = "TestRoom" };
            var created = await service.CreateAsync(dto);

            Assert.NotNull(created);
            Assert.Equal("TestRoom", created.Name);

            bool deleted = await service.DeleteAsync(created.RoomId);
            Assert.True(deleted);

            var allRooms = await service.GetAllAsync();
            Assert.Empty(allRooms);
        }
    }
}
