namespace Autorent.Application.DTO.Cars
{
    public class CarCreateDto
    {
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public decimal? PriceHour { get; set; }
        public decimal? PriceDay { get; set; }
        public string? ImageUrl { get; set; }
    }
}
