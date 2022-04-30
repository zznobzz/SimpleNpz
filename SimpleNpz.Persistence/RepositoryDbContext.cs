using Microsoft.EntityFrameworkCore;
using SimpleNpz.Domain.Entities;

namespace SimpleNpz.Persistence;

public sealed class RepositoryDbContext : DbContext
{
    public RepositoryDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Tank> Tanks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
}