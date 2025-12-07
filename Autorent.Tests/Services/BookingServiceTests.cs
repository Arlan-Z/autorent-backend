using Autorent.Domain.Entities;
using Autorent.Domain.Enums;
using Autorent.Infrastructure.Persistence;
using Autorent.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Autorent.Tests.Services
{
    public class BookingServiceTests
    {
        private ApplicationDbContext CreateDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task IsCarAvailable_ReturnsFalse_WhenOverlappingBookingExists()
        {
            var db = CreateDb();

            db.Bookings.Add(new Booking
            {
                CarId = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(3),
                Status = BookingStatus.Confirmed
            });

            await db.SaveChangesAsync();

            var service = new BookingService(db);

            var available = await service.IsCarAvailable(
                1,
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2)
            );

            Assert.False(available);
        }
    }
}

