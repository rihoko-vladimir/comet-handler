using Comet.API.Models.Filters;
using Comet.API.Models.Responses;

namespace Comet.API.Interfaces.Services;

public interface ICometService
{
    Task<List<CometGroupedResponse>?> GetGroupedCometsAsync(CometFilterDto filter);
}