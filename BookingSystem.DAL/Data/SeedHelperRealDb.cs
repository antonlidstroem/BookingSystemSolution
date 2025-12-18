using BookingSystem.DAL.Data;
using BookingSystem.DAL.Model;

public static class SeedHelperRealDb
{
    public static void SeedDatabase(BookingSystemAPIContext context)
    {
        // 1. Rooms
        if (!context.Room.Any())
        {
            context.Room.AddRange(
                new Room { Name = "Sovrummet" },
                new Room { Name = "Barnrummet" },
                new Room { Name = "Köket" }
            );
            context.SaveChanges();
        }

        // 2. Customers
        if (!context.Customer.Any())
        {
            context.Customer.AddRange(
                new Customer { Name = "Michelle" },
                new Customer { Name = "Anton" }
            );
            context.SaveChanges();
        }

        // 3. Bookings (nu finns FK-data!)
        if (!context.Booking.Any())
        {
            var room1 = context.Room.First();
            var room2 = context.Room.Skip(1).First();

            var customer1 = context.Customer.First();
            var customer2 = context.Customer.Skip(1).First();

            context.Booking.AddRange(
                new Booking
                {
                    RoomId = room1.RoomId,
                    CustomerId = customer1.CustomerId,
                    StartDate = new DateOnly(2024, 7, 1),
                    EndDate = new DateOnly(2024, 7, 1)
                },
                new Booking
                {
                    RoomId = room2.RoomId,
                    CustomerId = customer2.CustomerId,
                    StartDate = new DateOnly(2024, 7, 1),
                    EndDate = new DateOnly(2024, 7, 1)
                }
            );

            context.SaveChanges();
        }
    }
}
