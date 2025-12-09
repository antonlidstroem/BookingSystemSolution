using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Test3.UnitTests
{
    public class RoomTests : TestBase
    {

        [Fact]
        public void GetRoomTest()
        {
            // Arrange
            var roomName = "Mötesrum B";
            // Act
            var room = context.Room.FirstOrDefault(r => r.Name == roomName);
            // Assert
            Assert.NotNull(room);
            Assert.Equal(3, room.RoomId);
        }

        [Fact]
        public void GetAllRoomsTest()
        {
            // Arrange
            // Act
            var rooms = context.Room.ToList();
            // Assert
            Assert.Equal(3, rooms.Count);
        }
    }
}
