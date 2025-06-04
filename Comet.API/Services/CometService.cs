using Comet.API.Interfaces.Repositories;
using Comet.API.Interfaces.Services;
using Comet.API.Models.Filters;
using Comet.API.Models.Responses;
using Microsoft.Extensions.Caching.Memory;

namespace Comet.API.Services;

internal sealed class CometService : ICometService
{
    private readonly IMemoryCache cache;
    private readonly ILogger<CometService> logger;
    private readonly ICometRepository cometRepository;

    public CometService(ICometRepository cometRepository, IMemoryCache cache, ILogger<CometService> logger)
    {
        this.cometRepository = cometRepository;
        this.cache = cache;
        this.logger = logger;
    }

    public async Task<List<CometGroupedResponse>?> GetGroupedCometsAsync(CometFilterDto filter)
    {
        var cacheKey =
            $"comets_{filter.YearFrom}_{filter.YearTo}_{filter.RecordedClassification}_{filter.NameContains}_{filter.SortBy}_{filter.SortDescending}";

        if (cache.TryGetValue<List<CometGroupedResponse>>(cacheKey, out var cached))
            return cached;

        try
        {
            // Redis will be better here, for a distributed system
            var result = await cometRepository.GetFilteredGroupedAsync(filter);
            cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "There was an error retrieving comets from the database. Exception message: {message}",
                ex.Message);
            
            throw;
        }
    }
}