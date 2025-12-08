using BookingSystem.API.Interface;
using BookingSystem.DTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomsController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoomDto>>> GetAll()
        {
            var rooms = await _service.GetAllAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto?>> GetById(int id)
        {
            var room = await _service.GetByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }
    }
}
