using CometHandler.Interfaces.Services;
using Quartz;

namespace CometHandler.Jobs;

internal sealed class CometProcessingJob(
    ICometService cometService,
    IQueryingService queryingService,
    ILogger<CometProcessingJob> logger) : IJob
{
    public static readonly JobKey JobKey = new("comet-requester");

    public async Task Execute(IJobExecutionContext context)
    {
        var cometsResult = await queryingService.GetCometsAsync();

        if (!cometsResult.IsSuccess)
        {
            logger.LogWarning("Failed to get new comets");
            // Here we should notify or do something else
        }
        
        await cometService.InsertNewCometsAsync(cometsResult.Value!);
    }
}