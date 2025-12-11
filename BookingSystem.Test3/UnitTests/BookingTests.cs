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
        [InlineData(1, 2, "2024-07-01", false)]  // Upptaget
        [InlineData(2, 1, "2024-07-02", true)]   // Ledigt datum
        [InlineData(3, 1, "2024-07-01", true)]   // Ledigt rum
        public async Task CreateBookingTest_Theory(int roomId, int customerId, string date, bool shouldSucceed)
        {
            var dateOnly = DateOnly.Parse(date);

            var newBooking = new BookingDto
            {
                RoomId = roomId,
                CustomerId = customerId,
                StartDate = dateOnly,
                EndDate = dateOnly
            };

            if (shouldSucceed)
            {
                var createdBooking = await _bookingService.CreateAsync(newBooking);

                Assert.NotNull(createdBooking);
                Assert.Equal(roomId, createdBooking.RoomId);
                Assert.Equal(customerId, createdBooking.CustomerId);
                Assert.Equal(dateOnly, createdBooking.StartDate);
                Assert.Equal(dateOnly, createdBooking.EndDate);
            }
            else
            {
                await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await _bookingService.CreateAsync(newBooking));
            }
        }



        [Theory]
        [InlineData(1, "2024-07-01", false)]  // Rummet 1 är redan bokat
        [InlineData(2, "2024-07-01", false)]  // Rummet 2 är redan bokat
        [InlineData(3, "2024-07-01", true)]   // Rummet 3 är ledigt
        [InlineData(1, "2024-07-02", true)]   // Nästa dag, alla rum lediga
        public async Task IsRoomAvailable_Theory(int roomId, string date, bool expected)
        {
            // Arrange
            var dateOnly = DateOnly.Parse(date);
            
            // Act
            var available = await _bookingService.IsRoomAvailableAsync(roomId, dateOnly, dateOnly);

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




