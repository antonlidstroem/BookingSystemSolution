using BookingSystem.DAL.Data;
using BookingSystem.DAL.Model;
using System.Collections.Generic;

namespace BookingSystem.Test3
{
    public static class SeedHelper
    {
        public static void SeedDatabase(BookingSystemAPIContext context)
        {
            // Rooms
            var rooms = new List<Room>
            {
                new Room { RoomId = 1, Name = "Kontoret" },
                new Room { RoomId = 2, Name = "Konferensrum A" },
                new Room { RoomId = 3, Name = "Mötesrum B" }
            };
            context.Room.AddRange(rooms);

            // Customers
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "Alice" },
                new Customer { CustomerId = 2, Name = "Bob" }
            };
            context.Customer.AddRange(customers);

            // Bookings
            var bookings = new List<Booking>
            {
                new Booking { BookingId = 1, RoomId = 1, CustomerId = 1, StartDate = new DateOnly(2024,7,1), EndDate = new DateOnly(2024,7,1) },
                new Booking { BookingId = 2, RoomId = 2, CustomerId = 2, StartDate = new DateOnly(2024,7,1), EndDate = new DateOnly(2024,7,1) }
            };
            context.Booking.AddRange(bookings);

            context.SaveChanges();
        }
    }
}
