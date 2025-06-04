using Microsoft.EntityFrameworkCore;

namespace Comet.API.Models.Context;

internal sealed class ApplicationContext: DbContext
{
    public DbSet<Comet> Comets { get; init; }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Comet>();

        entity.HasKey(c => c.Id); 
        
        entity.OwnsOne(c => c.Geolocation);
    }
}