using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingSystem.DAL.Model;

namespace BookingSystem.DAL.Data
{
    public class BookingSystemAPIContext : DbContext
    {
        public BookingSystemAPIContext(DbContextOptions<BookingSystemAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Room> Room { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Seed bara om miljö = produktion eller utveckling
        //    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Testing")
        //    {
        //        modelBuilder.Entity<Room>().HasData(
        //            new Room { RoomId = 1, Name = "Kontoret" },
        //            new Room { RoomId = 2, Name = "Konferensrum A" },
        //            new Room { RoomId = 3, Name = "Mötesrum B" }
        //        );

        //        modelBuilder.Entity<Customer>().HasData(
        //            new Customer { CustomerId = 1, Name = "Alice" },
        //            new Customer { CustomerId = 2, Name = "Bob" }
        //        );

        //        modelBuilder.Entity<Booking>().HasData(
        //            new Booking
        //            {
        //                BookingId = 1,
        //                RoomId = 1,
        //                CustomerId = 1,
        //                StartDate = new DateOnly(2024, 7, 1),
        //                EndDate = new DateOnly(2024, 7, 1)
        //            },
        //            new Booking
        //            {
        //                BookingId = 2,
        //                RoomId = 2,
        //                CustomerId = 2,
        //                StartDate = new DateOnly(2024, 7, 1),
        //                EndDate = new DateOnly(2024, 7, 1)
        //            }
        //        );
        //    }
        //}

    }
}
