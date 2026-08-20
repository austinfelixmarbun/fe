using LodgingReservation_BE.Services;
using LodgingReservation_BE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LodgingReservation_BE.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class ReservationController : ControllerBase
        {
            private readonly IReservationService _reservationService;

            public ReservationController(IReservationService reservationService)
            {
                _reservationService = reservationService;
            }

            [HttpGet] 
            public async Task<IActionResult> GetReservations([FromQuery] string? status, [FromQuery] DateTime? date)
            {
                var reservations = await _reservationService.GetAllAsync(status, date);
                var response = reservations.Select(_reservationService.ToResponseDto).ToList();
                return Ok(response);
            }

            [HttpGet("{id:long}")] 
            public async Task<IActionResult> GetReservation(long id)
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null) return NotFound();
                return Ok(_reservationService.ToResponseDto(reservation));
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateReservation dto)
            {
            long userId = long.Parse(User.FindFirst("userId")!.Value);
            try
                {
                    ReservationResponse? result = await _reservationService.CreateAsync(dto, userId);
                    return Created($"/api/reservations/{result!.Id}", result);
            }
                catch (Exception ex)
                {
                    return BadRequest(new { status = "error", message = ex.Message });
                }
            }
        }

}
