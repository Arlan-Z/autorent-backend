namespace Autorent.Domain.DTOs.Booking
{
    public class BookingCreateDto
    {
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
