
namespace SimpleNpz.Contracts.TankDtos;

public class TankDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int MaxVolume { get; set; }
    public int MinVolume { get; }
    public float CurrentVolume { get; set; }
}