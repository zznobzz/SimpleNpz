namespace SimpleNpz.Domain.Exceptions;

public class TankNameNullOrWhiteSpaceException:BadRequestException
{
    public TankNameNullOrWhiteSpaceException(string input) : base($"Имя резервуара не может быть пустым")
    {
    }
}