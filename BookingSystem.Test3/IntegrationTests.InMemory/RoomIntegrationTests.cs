using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.DTO.DTO;

namespace BookingSystem.Test3.IntegrationTests
{
    public class RoomIntegrationTests : IntegrationTestBaseInMemory
    {
        [Fact]
        public async Task CreateRoom_NewRoom_ReturnsCreated()
        {
            // Arrange
            var newRoom = new RoomDto
            {
                Name = "Conference Room",
            };

            // Act
            var response = await HttpClient.PostAsJsonAsync("/api/Rooms", newRoom);
            var createdRoom = await response.Content.ReadFromJsonAsync<RoomDto>();
            
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(createdRoom);
            Assert.Equal("Conference Room", createdRoom.Name);
        }

        [Fact]
        public async Task DeleteRoom_RoomWithId1_ReturnsNoContent()
        {
            // Arrange
            var roomId = 1;
            
            // Act
            var response = await HttpClient.DeleteAsync($"/api/Rooms/{roomId}");
       
            var getResponse = await HttpClient.GetAsync($"/api/Rooms/{roomId}");
            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task UpdateRoom_RoomWithId2_ReturnsUpdatedRoom()
        {
            // Arrange
            var updatedRoom = new RoomDto
            {
                RoomId = 2,
                Name = "Updated Room Name"
            };
            
            // Act
            var response = await HttpClient.PutAsJsonAsync("/api/Rooms/2", updatedRoom);
            var returnedRoom = await response.Content.ReadFromJsonAsync<RoomDto>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(returnedRoom);
            Assert.Equal(2, returnedRoom.RoomId);
            Assert.Equal("Updated Room Name", returnedRoom.Name);
        }
    }
}
