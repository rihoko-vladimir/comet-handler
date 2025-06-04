using CometHandler.Interfaces.Repositories;
using CometHandler.Models;
using CometHandler.Models.Application;
using CometHandler.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace CometHandler.Repositories;

internal sealed class CometRepository(ApplicationContext applicationContext) : ICometRepository
{
    // UOW is an overhead here
    private readonly DbSet<Comet> comets = applicationContext.Comets;

    public async Task<Result<int?>> ReplaceCometsAsync(IList<Comet> newComets)
    {
        // Just override everything, doing a data synchronisation is overhead here(based on the data source), but might be applicable for specific needs
        await comets.ExecuteDeleteAsync();
        await comets.AddRangeAsync(newComets);
        var saved = await applicationContext.SaveChangesAsync();
        // Exceptions may occur here - handle and do a result template

        return Result<int?>.Success(saved);
    }
}