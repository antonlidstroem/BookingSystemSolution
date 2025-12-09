using BookingSystem.DAL.Data;
using BookingSystem.DTO.DTO;
using BookingSystem.DAL.Model;
using Microsoft.EntityFrameworkCore;


namespace BookingSystem.Test3.UnitTests
{
    public class BookingTests : TestBase
    {
       

       

        //[InlineData(new RoomDto {RoomId = 1, Name = "Kontoret"})]
        //[Theory]

        //public void TestRoomAvailability(RoomDto room, bool expectedValue)
        //{

        //    //Kolla om rummet är tillgängligt för bokning. Har det en bokning som överlappar med den föreslagna tiden?

        //    // Arrange


        //    // Act

        //    // Assert

        //}

        [Fact]
        public void TestCreateBooking()
        {
            // Skapa en bokning för ett rum om rummet är tillgängligt.

            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void TestGetBookingsForRoom()
        {
            // Hämta alla bokningar för ett specifikt rum.

            // Arrange
            // Act
            // Assert
        }

       
    }
}
