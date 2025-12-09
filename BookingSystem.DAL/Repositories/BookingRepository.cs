using BookingSystem.DAL.Data;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingSystemAPIContext _context;

        public BookingRepository(BookingSystemAPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Booking.AddAsync(booking);
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Booking.AsNoTracking().ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Booking.AsNoTracking().FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<List<Booking>> GetBookingsForRoomAsync(int roomId)
        {
            return await _context.Booking
                .AsNoTracking()
                .Where(b => b.RoomId == roomId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
