using System.Globalization;
using Mapster;
using SimpleNpz.Contracts.TankDtos;
using SimpleNpz.Domain.Entities;
using SimpleNpz.Domain.Exceptions;
using SimpleNpz.Domain.Repositories;
using SimpleNpz.Services.Abstractions;

namespace SimpleNpz.Services;

internal sealed class TankService:ITankService
{
    private readonly IRepositoryManager _repositoryManager;
    public TankService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

    public async Task<IEnumerable<TankDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var tanks = await _repositoryManager.TankRepository.GetAllAsync(cancellationToken);
        return tanks.Adapt<IEnumerable<TankDto>>();
    }

    public async Task<TankDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var tank = await _repositoryManager.TankRepository.GetByIdAsync(id, cancellationToken);
        if (tank is null)
        {
            throw new TankNotFoundException(id);
        }

        return tank.Adapt<TankDto>();
    }

    public async Task<TankDto> CreateAsync(CreateTankRequestDto input, CancellationToken cancellationToken = default)
    {
        //var tank = input.Adapt<Tank>();
        var tank = new Tank(input.Name, input.Description, input.MaxVolume);
        _repositoryManager.TankRepository.Insert(tank);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        return tank.Adapt<TankDto>();
    }

    public async Task UpdateAsync(long tankId, UpdateTankRequestDto input, CancellationToken cancellationToken = default)
    {
        var tank = await _repositoryManager.TankRepository.GetByIdAsync(tankId, cancellationToken);
        if (tank is null)
        {
            throw new TankNotFoundException(tankId);
        }

        tank.Name = input.Name;
        tank.Description = input.Description;
        tank.MaxVolume = input.MaxVolume;
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task UpdateTanksVolume(CancellationToken cancellationToken = default)
    {
        var tanks = await _repositoryManager.TankRepository.GetAllAsync();
        var rnd = new Random();
        var volumePart = rnd.NextDouble();
        foreach (var tank in tanks)
        {
            var volume = rnd.Next((int)tank.MinVolume, (int)tank.MaxVolume) + volumePart;
            tank.ChangeVolume((float) volume); //Дичь какая-то
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task ChangeTankVolumeAsync(long tankId, float inputVolume, CancellationToken cancellationToken = default)
    {
        var tank = await _repositoryManager.TankRepository.GetByIdAsync(tankId, cancellationToken);
        if (tank is null)
        {
            throw new TankNotFoundException(tankId);
        }
        tank.ChangeVolume(inputVolume);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long tankId, CancellationToken cancellationToken = default)
    {
        var tank = await _repositoryManager.TankRepository.GetByIdAsync(tankId, cancellationToken);
        if (tank is null)
        {
            throw new TankNotFoundException(tankId);
        }
        _repositoryManager.TankRepository.Remove(tank);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}