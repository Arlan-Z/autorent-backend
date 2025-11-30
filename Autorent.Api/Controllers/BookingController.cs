using Autorent.Application.DTO.Booking;
using Autorent.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Autorent.Api.Controllers
{
    [ApiController]
    [Route("api/booking")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        private int UserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingCreateDto dto)
        {
            try
            {
                var booking = await _bookingService.CreateBooking(UserId, dto);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("my")]
        public async Task<IActionResult> MyBookings()
        {
            try
            {
                var bookings = await _bookingService.GetUserBookings(UserId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var booking = await _bookingService.GetBooking(id, UserId);
                if (booking == null)
                    return NotFound(new { error = "Booking not found" });

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var result = await _bookingService.CancelBooking(id, UserId);
                return result
                    ? Ok("Booking canceled")
                    : NotFound(new { error = "Booking not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> Confirm(int id)
        {
            try
            {
                var result = await _bookingService.ConfirmBooking(id, UserId);
                return result
                    ? Ok("Booking confirmed")
                    : NotFound(new { error = "Booking not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                var result = await _bookingService.CompleteBooking(id, UserId);
                return result
                    ? Ok("Booking completed")
                    : NotFound(new { error = "Booking not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> CheckAvailable(int carId, DateTime start, DateTime end)
        {
            try
            {
                var available = await _bookingService.IsCarAvailable(carId, start, end);
                return Ok(new { available });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
