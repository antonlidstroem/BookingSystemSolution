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

        // GET: api/rooms
        [HttpGet]
        public async Task<ActionResult<List<RoomDto>>> GetAll()
        {
            var rooms = await _service.GetAllAsync();
            return Ok(rooms);
        }

        // GET: api/rooms/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto?>> GetById(int id)
        {
            var room = await _service.GetByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        // POST: api/rooms
        [HttpPost]
        public async Task<ActionResult<RoomDto>> Create(RoomDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.RoomId }, created);
        }

        // PUT: api/rooms/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<RoomDto>> Update(int id, RoomDto dto)
        {
            if (id != dto.RoomId)
                return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(dto);
            return Ok(updated);
        }

        // DELETE: api/rooms/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
