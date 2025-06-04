using CometHandler.Jobs;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace CometHandler.Controllers;

[Route("handler")]
[ApiController]
public class HandlerController(IScheduler scheduler) : ControllerBase
{
    [Route("trigger-processing")]
    [HttpPost]
    public async Task<IActionResult> TriggerProcessing(CancellationToken cancellationToken)
    {
        await scheduler.TriggerJob(CometProcessingJob.JobKey, cancellationToken);

        return Ok();
    }
}