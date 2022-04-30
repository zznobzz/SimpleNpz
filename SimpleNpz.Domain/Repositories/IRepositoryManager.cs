namespace SimpleNpz.Domain.Repositories;

public interface IRepositoryManager
{
    ITankRepository TankRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}