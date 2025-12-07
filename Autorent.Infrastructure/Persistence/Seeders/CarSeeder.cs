using Autorent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autorent.Infrastructure.Persistence.Seeders
{
    public static class CarSeeder
    {
        public static async Task Seed(ApplicationDbContext db)
        {
            if (await db.Cars.AnyAsync())
                return;

            var cars = new List<Car>
            {
                new Car
                {
                    Brand = "Mercedes-Benz",
                    Model = "CLA45 S AMG 4Matic",
                    Year = 2020,
                    PriceHour = 12,
                    PriceDay = 273,
                    ImageUrl = "https://www.netcarshow.com/Mercedes-Benz-CLA45_S_AMG_4Matic-2020-1280-bf89f74783a866f1388cc38d904cf32f23.jpg"
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Golf GTD",
                    Year = 2017,
                    PriceHour = 6,
                    PriceDay = 115,
                    ImageUrl = "https://www.netcarshow.com/Volkswagen-Golf_GTD-2017-1280-e21cb13c4b5b2035cc34d62130dadd25bc.jpg"
                },
                new Car
                {
                    Brand = "Acura",
                    Model = "RSX",
                    Year = 2005,
                    PriceHour = 2,
                    PriceDay = 46,
                    ImageUrl = "https://www.netcarshow.com/Acura-RSX-2005-1280-98ed8376114fe541655a08d0903c9061db.jpg"
                },
            };

            db.Cars.AddRange(cars);
            await db.SaveChangesAsync();
        }
    }
}
