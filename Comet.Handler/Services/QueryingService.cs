using CometHandler.Configurations;
using CometHandler.Interfaces.Services;
using CometHandler.Models.Application;
using CometHandler.Models.DTO;
using Microsoft.Extensions.Options;

namespace CometHandler.Services;

internal sealed class QueryingService : IQueryingService
{
    private readonly ILogger<QueryingService> logger;
    private readonly string endpoint;
    private readonly Uri baseAddress;
    private readonly HttpClient httpClient;

    public QueryingService(IOptions<AppConfiguration> options,
        IHttpClientFactory httpClientFactory,
        ILogger<QueryingService> logger)
    {
        this.logger = logger;
        endpoint = options.Value.Endpoint;
        baseAddress = new Uri(options.Value.ApiService);
        httpClient = httpClientFactory.CreateClient();

        httpClient.BaseAddress = baseAddress;
    }

    public async Task<Result<IEnumerable<CometDto>?>> GetCometsAsync()
    {
        try
        {
            // Here we also may do some transformations/caching/etc somewhere here
            var result = await httpClient.GetAsync(endpoint);
            result.EnsureSuccessStatusCode();

            var comets = await result.Content.ReadFromJsonAsync<IEnumerable<CometDto>>();

            return Result<IEnumerable<CometDto>?>.Success(comets ?? []);
        }
        catch (HttpRequestException e)
        {
            logger.LogError("Endpoint returned unsuccessful response code. Status code: {code}", e.StatusCode);
        }
        catch (Exception e)
        {
            logger.LogError("An error occurred while querying for new comets. Exception message: {message}",
                e.Message);
        }

        return Result<IEnumerable<CometDto>>.Failure("An error occurred while querying for new comets");
    }
}