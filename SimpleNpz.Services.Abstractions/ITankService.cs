using SimpleNpz.Contracts.TankDtos;

namespace SimpleNpz.Services.Abstractions;

public interface ITankService
{
    Task<IEnumerable<TankDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TankDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<TankDto> CreateAsync(CreateTankRequestDto input, CancellationToken cancellationToken = default);
    Task UpdateAsync(long tankId, UpdateTankRequestDto input, CancellationToken cancellationToken = default);
    Task ChangeTankVolumeAsync(long tankId, float inputVolume, CancellationToken cancellationToken= default);
    Task DeleteAsync(long tankId, CancellationToken cancellationToken = default);
    Task UpdateTanksVolume(CancellationToken cancellationToken = default);
}