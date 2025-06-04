using Comet.API.Interfaces.Services;
using Comet.API.Models.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Comet.API.Controllers;

[ApiController]
[Route("comet")]
public sealed class CometController : ControllerBase
{
    private readonly ICometService _cometService;

    public CometController(ICometService cometService)
    {
        _cometService = cometService;
    }

    [HttpGet]
    [Route("filtered")]
    public async Task<IActionResult> GetFilteredComets([FromQuery] CometFilterDto filter)
    {
        var results = await _cometService.GetGroupedCometsAsync(filter);

        return Ok(results);
    }
}