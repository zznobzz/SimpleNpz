using SimpleNpz.Domain.Repositories;

namespace SimpleNpz.Persistence.Repositories;

public class RepositoryManager:IRepositoryManager
{
    private readonly ITankRepository _tankRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RepositoryManager(RepositoryDbContext context)
    {
        _tankRepository = new TankRepository(context);
        _unitOfWork = new UnitOfWork(context);
    }

    public ITankRepository TankRepository => _tankRepository;
    public IUnitOfWork UnitOfWork => _unitOfWork;
}