using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.API.Interface;
using BookingSystem.API.Services;
using BookingSystem.DAL.Repositories;
using BookingSystem.DTO.DTO;

namespace BookingSystem.Test3.UnitTests.InMemory
{
    public class CustomerTests : TestBase
    {
        private readonly CustomerService _customerService;

        public CustomerTests()
        {
            _customerService = new CustomerService(new CustomerRepository(context));
        }

        [Fact]
        public async Task GetCustomerByIdTest()
        {
            // Arrange
            var customerName = "Alice";
            // Act
            var customer = await _customerService.GetByIdAsync(1);
            // Assert
            Assert.NotNull(customer);
            Assert.Equal(customerName, customer!.Name);
        }

        [Fact]
        public async Task GetAllCustomersTest()
        {
            // Act
            var customers = await _customerService.GetAllAsync();
            // Assert
            Assert.NotNull(customers);
            Assert.Equal(2, customers.Count);

        }

        [Fact]
        public async Task CreateCustomerTest()
        {
            // Arrange
            var newCustomer = new CustomerDto
            {
                Name = "Charlie"
            };
            // Act
            var createdCustomer = await _customerService.CreateAsync(newCustomer);
            // Assert
            Assert.NotNull(createdCustomer);
            Assert.Equal("Charlie", createdCustomer.Name);

        }

        [Fact]
        public async Task UpdateCustomerTest()
        {
            // Arrange
            var customer = await _customerService.GetByIdAsync(1);
            Assert.NotNull(customer);
            // Act
            customer.Name = "Updated Alice";
            await _customerService.UpdateAsync(customer);
            // Assert
            var updatedCustomer = await _customerService.GetByIdAsync(1);
            Assert.NotNull(updatedCustomer);
            Assert.Equal("Updated Alice", updatedCustomer.Name);
        }

        [Fact]
        public async Task DeleteCustomerTest()
        {
            // Arrange
            var customer = await _customerService.GetByIdAsync(2);
            Assert.NotNull(customer);
            // Act
            await _customerService.DeleteAsync(2);
            // Assert
            var deletedCustomer = await _customerService.GetByIdAsync(2);
            Assert.Null(deletedCustomer);
        }

    }
}
