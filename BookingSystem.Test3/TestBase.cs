using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.DAL.Data;
using BookingSystem.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Test3
{
    public class TestBase : IDisposable
    {
        public DbContextOptions<BookingSystemAPIContext> dbContextOptions = new DbContextOptionsBuilder<BookingSystemAPIContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        protected BookingSystemAPIContext context;

        public TestBase()
        {
            context = new BookingSystemAPIContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
        }

        public void SeedDatabase()
        {
            List<Room> rooms = new List<Room>()
            {
                new Room {RoomId = 1, Name = "Kontoret"},
                new Room {RoomId = 2, Name = "Konferensrum A"},
                new Room {RoomId = 3, Name = "Mötesrum B"}
            };
            context.Room.AddRange(rooms);

            List<Customer> customers = new List<Customer>()
            {
                new Customer {CustomerId = 1, Name = "Alice"},
                new Customer {CustomerId = 2, Name = "Bob"}
            };
            context.Customer.AddRange(customers);

            List<Booking> bookings = new List<Booking>()
            {
                new Booking {BookingId = 1, RoomId = 1, CustomerId = 1, StartTime = new DateTime(2024, 7, 1, 9, 0, 0), EndTime = new DateTime(2024, 7, 1, 10, 0, 0)},
                new Booking {BookingId = 2, RoomId = 2, CustomerId = 2, StartTime = new DateTime(2024, 7, 1, 11, 0, 0), EndTime = new DateTime(2024, 7, 1, 12, 0, 0)}
            };
            context.Booking.AddRange(bookings);

            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
