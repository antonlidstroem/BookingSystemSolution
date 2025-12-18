using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;

namespace BookingSystem.API.Interface
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAllAsync();
        Task<BookingDto?> GetByIdAsync(int id);
        Task<BookingDto> CreateAsync(BookingDto dto);
        Task<bool> IsRoomAvailableAsync(int roomId, DateOnly start, DateOnly end);
        Task<List<BookingDto>> GetBookingsForRoomAsync(int roomId);
     

    }
}
