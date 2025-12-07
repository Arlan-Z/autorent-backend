using Autorent.Domain.DTOs.Cars;

namespace Autorent.Domain.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarResponseDto>> GetAll();
        Task<CarResponseDto?> GetById(int id);
        Task<CarResponseDto> Create(CarCreateDto dto);
        Task<CarResponseDto?> Update(int id, CarUpdateDto dto);
        Task<bool> Delete(int id);
    }
}
