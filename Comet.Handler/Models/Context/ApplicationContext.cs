using Microsoft.EntityFrameworkCore;

namespace CometHandler.Models.Context;

internal sealed class ApplicationContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Comet> Comets { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Comet>();

        entity.HasKey(c => c.Id); 

        entity.HasIndex(c => c.Name);
        entity.HasIndex(c => c.RecordedClassification);
        entity.HasIndex(c => c.Year);
        
        entity.OwnsOne(c => c.Geolocation);
    }
}