using BookingSystem.API.Services;
using BookingSystem.DAL.Data;
using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BookingSystem.IntegrationTests
{
    public class BookingServiceIntegrationTests : IDisposable
    {
        private readonly BookingSystemAPIContext _context;
        private readonly BookingService _service;

        public BookingServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<BookingSystemAPIContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unik DB per testklass
                .Options;

            _context = new BookingSystemAPIContext(options);

            // Seed-data: ett rum och en kund
            _context.Room.Add(new Room { RoomId = 1, Name = "Rum A" });
            _context.Customer.Add(new Customer { CustomerId = 1, Name = "Anna" });
            _context.SaveChanges();

            _service = new BookingService(new DAL.Repositories.BookingRepository(_context));
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateBooking_WhenRoomIsAvailable()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                RoomId = 1,
                CustomerId = 1,
                StartTime = DateTime.Today.AddHours(10),
                EndTime = DateTime.Today.AddHours(12)
            };

            // Act
            var created = await _service.CreateAsync(bookingDto);

            // Assert
            Assert.NotNull(created);
            Assert.Equal(bookingDto.RoomId, created.RoomId);
            Assert.Equal(bookingDto.CustomerId, created.CustomerId);

            var allBookings = await _service.GetAllAsync();
            Assert.Single(allBookings); // bara en bokning i DB
        }

        [Fact]
        public async Task IsRoomAvailableAsync_ShouldReturnFalse_WhenRoomIsBooked()
        {
            // Arrange: skapa en befintlig bokning
            _context.Booking.Add(new Booking
            {
                RoomId = 1,
                CustomerId = 1,
                StartTime = DateTime.Today.AddHours(10),
                EndTime = DateTime.Today.AddHours(12)
            });
            _context.SaveChanges();

            // Act
            bool available = await _service.IsRoomAvailableAsync(1, DateTime.Today.AddHours(11), DateTime.Today.AddHours(13));

            // Assert
            Assert.False(available);
        }

        [Fact]
        public async Task GetBookingsForRoomAsync_ShouldReturnCorrectBookings()
        {
            // Arrange: flera bokningar
            _context.Booking.AddRange(new List<Booking>
            {
                new Booking { RoomId = 1, CustomerId = 1, StartTime = DateTime.Today.AddHours(8), EndTime = DateTime.Today.AddHours(9) },
                new Booking { RoomId = 2, CustomerId = 1, StartTime = DateTime.Today.AddHours(10), EndTime = DateTime.Today.AddHours(11) },
            });
            _context.SaveChanges();

            // Act
            var room1Bookings = await _service.GetBookingsForRoomAsync(1);
            var room2Bookings = await _service.GetBookingsForRoomAsync(2);

            // Assert
            Assert.Single(room1Bookings);
            Assert.Single(room2Bookings);
            Assert.Equal(1, room1Bookings[0].RoomId);
            Assert.Equal(2, room2Bookings[0].RoomId);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // rensa DB efter tester
            _context.Dispose();
        }
    }
}
