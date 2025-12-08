using BookingSystem.API.Interface;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;

namespace BookingSystem.API.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;

        public RoomService(IRoomRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<RoomDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<RoomDto?> GetByIdAsync(int id)
        {
            var room = await _repo.GetByIdAsync(id);
            return room == null ? null : MapToDto(room);
        }

        private static RoomDto MapToDto(Room r) => new RoomDto
        {
            RoomId = r.RoomId,
            Name = r.Name
        };
    }
}
