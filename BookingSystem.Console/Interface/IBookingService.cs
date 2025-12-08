using BookingSystem.DTO.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSystem.Console.Interface
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto?> GetBookingByIdAsync(int id);
        Task<List<BookingDto>> GetBookingsForRoomAsync(int roomId);
        Task<(bool Success, string? Error, BookingDto? Booking)> CreateBookingAsync(BookingDto dto);
        Task<bool> IsRoomAvailableAsync(int roomId, System.DateTime start, System.DateTime end);
    }
}
