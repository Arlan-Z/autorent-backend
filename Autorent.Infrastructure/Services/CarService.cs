using Autorent.Application.DTO.Cars;
using Autorent.Domain.Entities;
using Autorent.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Autorent.Infrastructure.Services
{
    public class CarService
    {
        private readonly ApplicationDbContext _db;

        public CarService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CarResponseDto>> GetAll()
        {
            return await _db.Cars
                .Select(c => new CarResponseDto
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    PriceHour = c.PriceHour,
                    PriceDay = c.PriceDay,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<CarResponseDto?> GetById(int id)
        {
            return await _db.Cars
                .Where(c => c.Id == id)
                .Select(c => new CarResponseDto
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    PriceHour = c.PriceHour,
                    PriceDay = c.PriceDay,
                    ImageUrl = c.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CarResponseDto> Create(CarCreateDto dto)
        {
            var car = new Car
            {
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                PriceHour = dto.PriceHour,
                PriceDay = dto.PriceDay,
                ImageUrl = dto.ImageUrl
            };

            _db.Cars.Add(car);
            await _db.SaveChangesAsync();

            return new CarResponseDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                PriceHour = car.PriceHour,
                PriceDay = car.PriceDay,
                ImageUrl = car.ImageUrl
            };
        }

        public async Task<CarResponseDto?> Update(int id, CarUpdateDto dto)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return null;

            car.Brand = dto.Brand;
            car.Model = dto.Model;
            car.Year = dto.Year;
            car.PriceHour = dto.PriceHour;
            car.PriceDay = dto.PriceDay;
            car.ImageUrl = dto.ImageUrl;

            await _db.SaveChangesAsync();

            return new CarResponseDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                PriceHour = car.PriceHour,
                PriceDay = car.PriceDay,
                ImageUrl = car.ImageUrl
            };
        }

        public async Task<bool> Delete(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return false;

            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
