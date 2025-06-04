using Comet.API.Models.Filters;
using Comet.API.Models.Responses;

namespace Comet.API.Interfaces.Repositories;

public interface ICometRepository
{
    Task<List<CometGroupedResponse>> GetFilteredGroupedAsync(CometFilterDto filter);
}