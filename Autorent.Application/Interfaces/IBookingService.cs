using Autorent.Application.DTO.Booking;

namespace Autorent.Application.Interfaces
{
    public interface IBookingService
    {
        Task<bool> IsCarAvailable(int carId, DateTime start, DateTime end);
        Task<BookingResponseDto> CreateBooking(int userId, BookingCreateDto dto);
        Task<IEnumerable<BookingResponseDto>> GetUserBookings(int userId);
        Task<BookingResponseDto?> GetBooking(int id, int userId);

        Task<bool> CancelBooking(int id, int userId);
        Task<bool> ConfirmBooking(int id, int userId);
        Task<bool> CompleteBooking(int id, int userId);
    }
}
