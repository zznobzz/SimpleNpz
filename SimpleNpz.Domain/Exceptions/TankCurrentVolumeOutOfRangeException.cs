using SimpleNpz.Domain.Entities;

namespace SimpleNpz.Domain.Exceptions;

public sealed class TankCurrentVolumeOutOfRangeException:BadRequestException
{
    public TankCurrentVolumeOutOfRangeException(Tank tank, float inputVolume) 
        : base($"Новый показатель ({inputVolume}) объема резервуара с ID:{tank.Id} не попадает в диапазон допустимых показателей {tank.MinVolume} - {tank.MaxVolume}")
    {
    }
}