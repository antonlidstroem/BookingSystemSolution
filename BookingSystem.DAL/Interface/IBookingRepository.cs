using BookingSystem.DAL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Interface
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<List<Booking>> GetBookingsForRoomAsync(int roomId);
        Task AddAsync(Booking booking);
        Task SaveChangesAsync();
    }
}
