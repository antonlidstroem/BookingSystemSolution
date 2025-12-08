using BookingSystem.DTO.DTO;

namespace BookingSystem.API.Interface
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetAllAsync();
        Task<RoomDto?> GetByIdAsync(int id);
    }
}
