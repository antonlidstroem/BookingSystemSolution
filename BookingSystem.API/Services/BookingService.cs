using BookingSystem.API.Interface;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repo;

    public BookingService(IBookingRepository repo)
    {
        _repo = repo;
    }

    public async Task<BookingDto> CreateAsync(BookingDto dto)
    {
        if (!await IsRoomAvailableAsync(dto.RoomId, dto.StartTime, dto.EndTime))
            throw new InvalidOperationException("Room is not available");

        var entity = new Booking
        {
            RoomId = dto.RoomId,
            CustomerId = dto.CustomerId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime
        };

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        dto.BookingId = entity.BookingId; // om du auto-genererar id i databasen
        return dto;
        //return null;
    }

    public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime start, DateTime end)
    {
        var bookings = await _repo.GetBookingsForRoomAsync(roomId);
        return bookings.All(b => b.EndTime <= start || b.StartTime >= end);
    }

    public async Task<List<BookingDto>> GetAllAsync()
    {
        var bookings = await _repo.GetAllAsync();
        return bookings.Select(b => new BookingDto
        {
            BookingId = b.BookingId,
            RoomId = b.RoomId,
            StartTime = b.StartTime,
            EndTime = b.EndTime
        }).ToList();
        //return null;
    }

    public async Task<BookingDto?> GetByIdAsync(int id)
    {
        var b = await _repo.GetByIdAsync(id);
        if (b == null) return null;
        return new BookingDto
        {
            BookingId = b.BookingId,
            RoomId = b.RoomId,
            StartTime = b.StartTime,
            EndTime = b.EndTime
        };
        //return null;
    }

    public async Task<List<BookingDto>> GetBookingsForRoomAsync(int roomId)
    {
        var bookings = await _repo.GetBookingsForRoomAsync(roomId);
        return bookings.Select(b => new BookingDto
        {
            BookingId = b.BookingId,
            RoomId = b.RoomId,
            StartTime = b.StartTime,
            EndTime = b.EndTime
        }).ToList();
        //return null;
    }
}
