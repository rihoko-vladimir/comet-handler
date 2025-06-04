using CometHandler.Models;
using CometHandler.Models.Application;

namespace CometHandler.Interfaces.Repositories;

internal interface ICometRepository
{
    public Task<Result<int?>> ReplaceCometsAsync(IList<Comet> newComets);
}