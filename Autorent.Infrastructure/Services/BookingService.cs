using Autorent.Application.DTO.Booking;
using Autorent.Application.Interfaces;
using Autorent.Domain.Entities;
using Autorent.Domain.Enums;
using Autorent.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autorent.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _db;

        public BookingService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsCarAvailable(int carId, DateTime start, DateTime end)
        {
            return !await _db.Bookings
                .AnyAsync(b =>
                    b.CarId == carId &&
                    (b.Status == BookingStatus.Pending || b.Status == BookingStatus.Confirmed) &&
                    (
                        (start >= b.StartDate && start < b.EndDate) ||
                        (end > b.StartDate && end <= b.EndDate) ||
                        (start <= b.StartDate && end >= b.EndDate)
                    )
                );
        }

        public async Task<BookingResponseDto> CreateBooking(int userId, BookingCreateDto dto)
        {
            if (!await IsCarAvailable(dto.CarId, dto.StartDate, dto.EndDate))
                throw new Exception("Car is already booked for this time.");

            var car = await _db.Cars.FindAsync(dto.CarId)
                ?? throw new Exception("Car not found.");

            var hours = (decimal)(dto.EndDate - dto.StartDate).TotalHours;

            var booking = new Booking
            {
                CarId = dto.CarId,
                UserId = userId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = BookingStatus.Pending,
                Price = hours * (car.PriceHour ?? 0)
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            return MapToDto(booking, car.Brand, car.Model);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetUserBookings(int userId)
        {
            return await _db.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Car)
                .Select(b => MapToDto(b, b.Car.Brand, b.Car.Model))
                .ToListAsync();
        }

        public async Task<BookingResponseDto?> GetBooking(int id, int userId)
        {
            return await _db.Bookings
                .Where(b => b.Id == id && b.UserId == userId)
                .Include(b => b.Car)
                .Select(b => MapToDto(b, b.Car.Brand, b.Car.Model))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CancelBooking(int id, int userId)
        {
            var booking = await GetUserBookingEntity(id, userId);
            if (booking == null) return false;

            if (booking.Status == BookingStatus.Completed)
                throw new Exception("Completed bookings cannot be canceled.");

            booking.Status = BookingStatus.Canceled;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmBooking(int id, int userId)
        {
            var booking = await GetUserBookingEntity(id, userId);
            if (booking == null) return false;

            if (booking.Status == BookingStatus.Canceled)
                throw new Exception("Canceled bookings cannot be confirmed.");

            if (booking.Status == BookingStatus.Completed)
                throw new Exception("Completed bookings cannot be confirmed.");

            booking.Status = BookingStatus.Confirmed;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteBooking(int id, int userId)
        {
            var booking = await GetUserBookingEntity(id, userId);
            if (booking == null) return false;

            if (booking.Status == BookingStatus.Canceled)
                throw new Exception("Canceled bookings cannot be completed.");

            booking.Status = BookingStatus.Completed;
            await _db.SaveChangesAsync();
            return true;
        }

        private async Task<Booking?> GetUserBookingEntity(int id, int userId)
        {
            return await _db.Bookings
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        }

        private static BookingResponseDto MapToDto(Booking b, string brand, string model)
        {
            return new BookingResponseDto
            {
                Id = b.Id,
                CarId = b.CarId,
                CarBrand = brand,
                CarModel = model,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Price = b.Price,
                Status = b.Status.ToString()
            };
        }
    }
}
