using BookingSystem.DTO.DTO;

namespace BookingSystem.API.Interface
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAllAsync();
        Task<BookingDto?> GetByIdAsync(int id);
        Task<BookingDto> CreateAsync(BookingDto dto);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime start, DateTime end);
        Task<List<BookingDto>> GetBookingsForRoomAsync(int roomId);
    }
}
