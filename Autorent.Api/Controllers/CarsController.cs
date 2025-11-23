using Autorent.Application.DTO.Cars;
using Autorent.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Autorent.Api.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly CarService _service;

        public CarsController(CarService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var car = await _service.GetById(id);
            return car == null ? NotFound() : Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateDto dto)
            => Ok(await _service.Create(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CarUpdateDto dto)
        {
            var car = await _service.Update(id, dto);
            return car == null ? NotFound() : Ok(car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _service.Delete(id) ? Ok() : NotFound();
    }
}
