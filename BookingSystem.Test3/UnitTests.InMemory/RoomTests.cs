using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.API.Services;
using BookingSystem.DAL.Model;
using BookingSystem.DAL.Repositories;
using BookingSystem.DTO.DTO;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Test3.UnitTests.InMemory
{
    public class RoomTests : TestBase
    {
        private readonly RoomService _roomService;

        public RoomTests()
        {
            _roomService = new RoomService(new RoomRepository(context));
        }

        [Fact]
        public async Task GetRoomByIdTest()
        {
            // Arrange
            var roomName = "Mötesrum B";
            // Act
            var room = await _roomService.GetByIdAsync(3);
            // Assert
            Assert.NotNull(room);
            Assert.Equal(roomName, room!.Name);
        }

        [Fact]
        public async Task GetAllRoomsTest()
        {
            // Arrange
            // Act
            var rooms = await _roomService.GetAllAsync();
            // Assert
            Assert.Equal(3, rooms.Count);
        }

        [Fact]
        public async Task CreateRoomTest()
        {
            // Arrange
            var newRoom = new RoomDto
            {
                Name = "Nytt Rum"
            };
            // Act
            var createdRoom = await _roomService.CreateAsync(newRoom);

            // Assert
            Assert.NotNull(createdRoom);
            Assert.Equal("Nytt Rum", createdRoom.Name);
        }

        [Fact]
        public async Task UpdateRoomTest()
        {
            // Arrange
            var room = await _roomService.GetByIdAsync(1);
            Assert.NotNull(room);

            // Act
            room.Name = "Uppdaterat Rum";
            await _roomService.UpdateAsync(room);

            // Assert
            var updatedRoom = await _roomService.GetByIdAsync(1);
            Assert.NotNull(updatedRoom);
            Assert.Equal("Uppdaterat Rum", updatedRoom.Name);
        }

        [Fact]
        public async Task DeleteRoomTest()
        {
            // Arrange
            var room = await _roomService.GetByIdAsync(2);
            Assert.NotNull(room);
            
            // Act
            await _roomService.DeleteAsync(2);

            // Assert
            //var deletedRoom = context.Room.FirstOrDefault(r => r.RoomId == 2);
            var deletedRoom = await _roomService.GetByIdAsync(2);
            Assert.Null(deletedRoom);
        }
    }
}
