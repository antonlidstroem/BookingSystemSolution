using BookingSystem.Console.DTO;

namespace BookingSystem.Console.Interface
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> CreateBookingAsync(BookingDto booking);
        Task<BookingDto?> GetBookingByIdAsync(int id);
    }
}
