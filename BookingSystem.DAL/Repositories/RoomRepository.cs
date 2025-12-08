using BookingSystem.DAL.Data;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingSystemAPIContext _context;

        public RoomRepository(BookingSystemAPIContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _context.Room.ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Room.FindAsync(id);
        }

        public async Task AddAsync(Room room)
        {
            await _context.Room.AddAsync(room);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
