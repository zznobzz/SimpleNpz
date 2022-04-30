using SimpleNpz.Domain.Repositories;
using SimpleNpz.Services.Abstractions;

namespace SimpleNpz.Services;

public sealed class ServiceManager:IServiceManager
{
  private readonly ITankService _tankService;

  public ServiceManager(IRepositoryManager repositoryManager)
   {
       _tankService = new TankService(repositoryManager);

   }

   public ITankService TankService => _tankService;
}