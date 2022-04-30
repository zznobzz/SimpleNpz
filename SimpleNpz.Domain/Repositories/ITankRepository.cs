using SimpleNpz.Domain.Entities;

namespace SimpleNpz.Domain.Repositories;

public interface ITankRepository
{
    Task<IEnumerable<Tank>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Tank> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    void Insert(Tank tank);
    void Remove(Tank tank);
}