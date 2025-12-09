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
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto dto)
        {
            var entity = new Customer { Name = dto.Name };
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<CustomerDto> UpdateAsync(CustomerDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.CustomerId);
            if (entity == null) throw new KeyNotFoundException("Customer not found");

            entity.Name = dto.Name;
            await _repo.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        private static CustomerDto MapToDto(Customer c) => new CustomerDto
        {
            CustomerId = c.CustomerId,
            Name = c.Name
        };
    }
}
