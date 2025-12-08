using BookingSystem.API.Interface;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;

namespace BookingSystem.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            return customer == null ? null : MapToDto(customer);
        }

        private static CustomerDto MapToDto(Customer c) => new CustomerDto
        {
            CustomerId = c.CustomerId,
            Name = c.Name
        };
    }
}
