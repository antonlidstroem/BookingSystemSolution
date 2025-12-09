using BookingSystem.API.Interface;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using BookingSystem.DTO.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

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
            //return new List<RoomDto>();
        }

        public async Task<RoomDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
            //return null;

        }

        public async Task<RoomDto> CreateAsync(RoomDto dto)
        {
            var entity = new Room { Name = dto.Name };
            //var entity = new Room { Name = dto.Name + "_fel"};
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
            //throw new Exception("Medvetet fel");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync();
            return true;
            //return false;
        }

        private static RoomDto MapToDto(Room r) => new RoomDto
        {
            RoomId = r.RoomId,
            Name = r.Name
        };
    }
}
