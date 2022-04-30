using SimpleNpz.Domain.Exceptions;

namespace SimpleNpz.Domain.Entities;

public class Tank:BaseEntity
{
    public Tank(string name, string? description, float maxVolume)
    {
        Name = name;
        Description = description;
        MaxVolume = maxVolume;
    }

    public string Name { get; set; }
    public string? Description { get; set; }
    public float MinVolume  = 0;
    public float MaxVolume { get; set; }
    public float CurrentVolume { get; set; }


    public void ChangeVolume(float input)
    {
        if (input>MaxVolume || input<MinVolume)
        {
            throw new TankCurrentVolumeOutOfRangeException(this, input);
        }

        CurrentVolume = input;
    }

    
}