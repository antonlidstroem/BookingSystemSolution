using BookingSystem.API.Services;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;
using Moq;
using Xunit;

namespace BookingSystem.Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _repoMock;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _repoMock = new Mock<ICustomerRepository>();
            _service = new CustomerService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "Anna" },
                new Customer { CustomerId = 2, Name = "Bob" }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Anna", result[0].Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenExists()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "Anna" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(customer);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Anna", result!.Name);
        }
    }
}
