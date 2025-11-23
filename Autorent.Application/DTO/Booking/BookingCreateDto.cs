namespace Autorent.Application.DTO.Booking
{
    public class BookingCreateDto
    {
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
