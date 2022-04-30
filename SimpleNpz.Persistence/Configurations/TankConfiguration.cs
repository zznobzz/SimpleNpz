using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleNpz.Domain.Entities;
using SimpleNpz.Domain.Shared.Tanks;

namespace SimpleNpz.Persistence.Configurations;

public class TankConfiguration:IEntityTypeConfiguration<Tank>
{
    public void Configure(EntityTypeBuilder<Tank> builder)
    {
        builder.ToTable(nameof(Tank) + "s");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(TankConsts.TankNameMaxLength).IsRequired();
    }
}