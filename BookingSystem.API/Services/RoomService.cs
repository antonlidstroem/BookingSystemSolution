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
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<RoomDto> CreateAsync(RoomDto dto)
        {
            var entity = new Room { Name = dto.Name };
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<RoomDto> UpdateAsync(RoomDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.RoomId);
            if (entity == null) throw new KeyNotFoundException("Room not found");

            entity.Name = dto.Name;
            await _repo.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        private static RoomDto MapToDto(Room r) => new RoomDto
        {
            RoomId = r.RoomId,
            Name = r.Name
        };
    }
}
