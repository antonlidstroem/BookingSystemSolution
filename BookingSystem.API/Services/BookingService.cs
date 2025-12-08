using BookingSystem.API.Interface;
using BookingSystem.DTO.DTO;

public class BookingService : IBookingService
{
    public Task<List<BookingDto>> GetAllAsync() => throw new NotImplementedException();
    public Task<BookingDto?> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<BookingDto> CreateAsync(BookingDto dto) => throw new NotImplementedException();
    public Task<bool> IsRoomAvailableAsync(int roomId, DateTime start, DateTime end) => throw new NotImplementedException();
    public Task<List<BookingDto>> GetBookingsForRoomAsync(int roomId) => throw new NotImplementedException();
}
