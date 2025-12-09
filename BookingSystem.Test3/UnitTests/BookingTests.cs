using System;
using BookingSystem.API.Interface;
using BookingSystem.API.Services;
using BookingSystem.DAL.Data;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using BookingSystem.DAL.Repositories;
using BookingSystem.DTO.DTO;
using Microsoft.EntityFrameworkCore;


namespace BookingSystem.Test3.UnitTests
{
    public class BookingTests : TestBase
    {
        private readonly BookingService _bookingService;

        public BookingTests()
        {
            _bookingService = new BookingService(new BookingRepository(context));
        }



        [Theory]
        [InlineData(1, 2, "2024-07-01T09:00", "2024-07-01T10:00", false)]  // Upptaget
        [InlineData(2, 1, "2024-07-01T12:00", "2024-07-01T13:00", true)]   // Ledigt
        public async Task CreateBookingTest_Theory(int roomId, int customerId, string start, string end, bool shouldSucceed)
        {
            // Arrange
            var startTime = DateTime.Parse(start);
            var endTime = DateTime.Parse(end);

            var newBooking = new BookingDto
            {
                RoomId = roomId,
                CustomerId = customerId,
                StartTime = startTime,
                EndTime = endTime
            };

            if (shouldSucceed)
            {
                // Act
                var createdBooking = await _bookingService.CreateAsync(newBooking);

                // Assert
                Assert.NotNull(createdBooking);
                Assert.Equal(roomId, createdBooking.RoomId);
                Assert.Equal(customerId, createdBooking.CustomerId);
                Assert.Equal(startTime, createdBooking.StartTime);
                Assert.Equal(endTime, createdBooking.EndTime);
            }
            else
            {
                // Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await _bookingService.CreateAsync(newBooking));
            }
        }


        [Theory]
        [InlineData(1, "2024-07-01T09:00", "2024-07-01T10:00", false)]  // Krockar med seed-bokning
        [InlineData(2, "2024-07-01T11:30", "2024-07-01T12:30", false)]  // Överlappar bokning
        [InlineData(3, "2024-07-01T09:00", "2024-07-01T10:00", true)]   // Inget bokat, ledigt
        public async Task IsRoomAvailable_Theory(int roomId, string start, string end, bool expected)
        {
            // Arrange
            var startTime = DateTime.Parse(start);
            var endTime = DateTime.Parse(end);

            // Act
            var available = await _bookingService.IsRoomAvailableAsync(roomId, startTime, endTime);

            // Assert
            Assert.Equal(expected, available);
        }

        [Fact]
        public async Task GetAllBookingsTest()
        {
            var bookings = await _bookingService.GetAllAsync();

            // Assert
            Assert.NotNull(bookings);
            Assert.True(bookings.Count >= 2); 
        }

        [Fact]
        public async Task GetBookingByIdTest()
        {
            // Arrange
            var bookingId = 1;

            // Act
            var booking = await _bookingService.GetByIdAsync(bookingId);

            // Assert
            Assert.NotNull(booking);
            Assert.Equal(1, booking!.BookingId);
        }

        [Fact]
        public async Task GetBookingsForRoomTest()
        {
            // Arrange
            var roomId = 1;

            // Act
            var bookings = await _bookingService.GetBookingsForRoomAsync(roomId);

            // Assert
            Assert.NotNull(bookings);
            Assert.All(bookings, b => Assert.Equal(roomId, b.RoomId));
        }


    }
}




