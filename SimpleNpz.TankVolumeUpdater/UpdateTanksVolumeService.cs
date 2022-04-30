using System.Globalization;
using SimpleNpz.Services.Abstractions;

namespace SimpleNpz.TankVolumeUpdater;

public class UpdateTanksVolumeService
{
    private readonly ITankService _tankService;

    public UpdateTanksVolumeService(ITankService tankService) => _tankService = tankService;

    public async Task DoUpdate()
    {
        var tanks =await _tankService.GetAllAsync();
        var rnd = new Random();
        
        foreach (var tank in tanks)
        {
            var volume = rnd.Next(tank.MinVolume, tank.MaxVolume) + rnd.NextDouble();
            await _tankService.ChangeTankVolumeAsync(tank.Id,float.Parse(volume.ToString(CultureInfo.InvariantCulture)));
        }
        
    }
}