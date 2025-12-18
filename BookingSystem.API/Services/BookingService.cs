using BookingSystem.API.Interface;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;
using Microsoft.EntityFrameworkCore;
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
        // dto.StartDate och dto.EndDate är redan DateOnly, ingen konvertering behövs
        bool available = await IsRoomAvailableAsync(dto.RoomId, dto.StartDate, dto.EndDate);

        if (!available)
        {
            throw new InvalidOperationException("Rummet är redan bokat under dessa datum");
        }

        var entity = new Booking
        {
            RoomId = dto.RoomId,
            CustomerId = dto.CustomerId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        dto.BookingId = entity.BookingId;
        return dto;
    }



    public async Task<bool> IsRoomAvailableAsync(int roomId, DateOnly start, DateOnly end)
    {
        return !await _repo.GetAllBookingsQuery()
            .AnyAsync(b => b.RoomId == roomId &&
                           b.StartDate <= end &&
                           b.EndDate >= start);
    }



    public async Task<List<BookingDto>> GetAllAsync()
    {
        var bookings = await _repo.GetAllAsync();
        return bookings.Select(b => new BookingDto
        {
            BookingId = b.BookingId,
            RoomId = b.RoomId,
            CustomerId = b.CustomerId,
            StartDate = b.StartDate,
            EndDate = b.EndDate
        }).ToList();
    }

    public async Task<BookingDto?> GetByIdAsync(int id)
    {
        var b = await _repo.GetByIdAsync(id);
        if (b == null) return null;
        return new BookingDto
        {
            BookingId = b.BookingId,
            RoomId = b.RoomId,
            CustomerId = b.CustomerId,
            StartDate = b.StartDate,
            EndDate = b.EndDate
        };
    }

    public async Task<List<BookingDto>> GetBookingsForRoomAsync(int roomId)
    {
        var bookings = await _repo.GetBookingsForRoomAsync(roomId);
        return bookings.Select(b => new BookingDto
        {
            BookingId = b.BookingId,
            RoomId = b.RoomId,
            CustomerId = b.CustomerId,
            StartDate = b.StartDate,
            EndDate = b.EndDate
        }).ToList();
    }

}
