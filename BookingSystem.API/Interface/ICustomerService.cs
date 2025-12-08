using BookingSystem.DTO.DTO;

namespace BookingSystem.API.Interface
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
    }
}
