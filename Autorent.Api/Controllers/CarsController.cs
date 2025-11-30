using Autorent.Application.DTO.Cars;
using Autorent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Autorent.Api.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _carService.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var car = await _carService.GetById(id);
            return car == null ? NotFound() : Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateDto dto)
            => Ok(await _carService.Create(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CarUpdateDto dto)
        {
            var car = await _carService.Update(id, dto);
            return car == null ? NotFound() : Ok(car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _carService.Delete(id) ? Ok() : NotFound();
    }
}
