namespace SimpleNpz.Domain.Exceptions;

public class TankNotFoundException:NotFoundException
{
    public TankNotFoundException(long tankId) : base($"Резервуар с ID:{tankId} не найден")
    {
    }
}