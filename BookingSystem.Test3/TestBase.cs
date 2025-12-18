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

            SeedHelperInMemory.SeedDatabase(context);
        }


        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
