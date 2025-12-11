using BookingSystem.API.Interface;
using BookingSystem.DTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingsController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookingDto>>> GetAll()
        {
            var bookings = await _service.GetAllAsync();
            return Ok(bookings);
            //return null;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto?>> GetById(int id)
        {
            var booking = await _service.GetByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create(BookingDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById),
                    new { id = created.BookingId },
                    created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }

        [HttpGet("room/{roomId}")]
        public async Task<ActionResult<List<BookingDto>>> GetBookingsForRoom(int roomId)
        {
            var bookings = await _service.GetBookingsForRoomAsync(roomId);
            return Ok(bookings);
        }

        [HttpGet("available")]
        public async Task<ActionResult<bool>> IsRoomAvailable(
            [FromQuery] int roomId, 
            [FromQuery] DateOnly start, 
            [FromQuery] DateOnly end)
        {
            var available = await _service.IsRoomAvailableAsync(roomId, start, end);
            return Ok(available);
        }
    }
}
