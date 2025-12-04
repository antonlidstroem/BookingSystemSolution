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
        public BookingSystemAPIContext (DbContextOptions<BookingSystemAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BookingSystem.DAL.Model.Customer> Customer { get; set; } = default!;
        public DbSet<BookingSystem.DAL.Model.Booking> Booking { get; set; } = default!;
        public DbSet<BookingSystem.DAL.Model.Room> Room { get; set; } = default!;
    }
}
