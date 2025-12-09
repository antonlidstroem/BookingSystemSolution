using BookingSystem.API.Services;
using BookingSystem.DAL.Data;
using BookingSystem.DAL.Repositories;
using BookingSystem.DTO.DTO;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookingSystem.Test
{
    public class CustomerServiceIntegrationTests
    {
        private CustomerService GetService(out BookingSystemAPIContext context)
        {
            var options = new DbContextOptionsBuilder<BookingSystemAPIContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new BookingSystemAPIContext(options);
            var repo = new CustomerRepository(context);
            return new CustomerService(repo);
        }

        [Fact]
        public async Task CreateAndDeleteCustomer_ShouldWork()
        {
            // Arrange
            var service = GetService(out var context);

            var dto = new CustomerDto { Name = "TestCustomer" };
            var created = await service.CreateAsync(dto);

            Assert.NotNull(created);
            Assert.Equal("TestCustomer", created.Name);

            // Act - delete
            bool deleted = await service.DeleteAsync(created.CustomerId);

            // Assert
            Assert.True(deleted);
            var allCustomers = await service.GetAllAsync();
            Assert.Empty(allCustomers);
        }
    }
}
