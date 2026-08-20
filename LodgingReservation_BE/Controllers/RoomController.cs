using Microsoft.AspNetCore.Mvc;
using LodgingReservation_BE.Services;
using LodgingReservation_BE.DTOs;
using LodgingReservation_BE.Models;

namespace LodgingReservation_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            var result = await _roomService.GetAllAsync(search, page, limit);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoom dto)
        {
            try
            {
                var result = await _roomService.CreateAsync(dto);
                if (result == null)
                {
                    return BadRequest(new { message = "Gagal membuat kamar baru." });
                }
                return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { status = "error", message = ex.Message });
            }
        }
    }
}
