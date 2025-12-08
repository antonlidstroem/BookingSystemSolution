using BookingSystem.DAL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Interface
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task AddAsync(Room room);
        Task SaveChangesAsync();
    }
}
