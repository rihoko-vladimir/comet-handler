using CometHandler.Models.Application;
using CometHandler.Models.DTO;

namespace CometHandler.Interfaces.Services;

internal interface ICometService
{
    public Task<Result<int?>> InsertNewCometsAsync(IEnumerable<CometDto> comets);
}