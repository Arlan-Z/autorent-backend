using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autorent.Domain.Entities
{
    public class Car
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("brand")]
        public string Brand { get; set; } = null!;

        [Column("model")]
        public string Model { get; set; } = null!;

        [Column("year")]
        public int Year { get; set; }

        [Column("image_url")]
        public string? ImageUrl { get; set; }

        [Column("price_hour")]
        public decimal? PriceHour { get; set; }

        [Column("price_day")]
        public decimal? PriceDay { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }
}
