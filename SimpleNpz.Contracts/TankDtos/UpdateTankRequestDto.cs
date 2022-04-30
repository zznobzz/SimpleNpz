using System.ComponentModel.DataAnnotations;
using SimpleNpz.Domain.Shared.Tanks;

namespace SimpleNpz.Contracts.TankDtos;

public class UpdateTankRequestDto
{
    [Required]
    [StringLength(TankConsts.TankNameMaxLength)]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public float MaxVolume { get; set; }
}