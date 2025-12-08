using BookingSystem.DTO.DTO;

namespace BookingSystem.API.Interface
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CustomerDto dto);
        Task<CustomerDto> UpdateAsync(CustomerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
