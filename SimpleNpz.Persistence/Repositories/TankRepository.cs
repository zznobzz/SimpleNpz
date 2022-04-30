using Microsoft.EntityFrameworkCore;
using SimpleNpz.Domain.Entities;
using SimpleNpz.Domain.Repositories;

namespace SimpleNpz.Persistence.Repositories;

public class TankRepository:ITankRepository
{
    private readonly RepositoryDbContext _dbContext;

    public TankRepository(RepositoryDbContext context) => _dbContext = context;

    public async Task<IEnumerable<Tank>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Tanks.ToListAsync(cancellationToken);

    public async Task<Tank> GetByIdAsync(long id, CancellationToken cancellationToken = default) =>
        await _dbContext.Tanks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public void Insert(Tank tank) => _dbContext.Tanks.Add(tank);

    public void Remove(Tank tank) => _dbContext.Tanks.Remove(tank);
}