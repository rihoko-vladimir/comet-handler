using CometHandler.Models.Application;
using CometHandler.Models.DTO;

namespace CometHandler.Interfaces.Services;

internal interface IQueryingService
{
    public Task<Result<IEnumerable<CometDto>?>> GetCometsAsync();
}