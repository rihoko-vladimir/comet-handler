using CometHandler.Interfaces.Repositories;
using CometHandler.Interfaces.Services;
using CometHandler.Mapping;
using CometHandler.Models.Application;
using CometHandler.Models.DTO;

namespace CometHandler.Services;

internal sealed class CometService(ICometRepository cometRepository) : ICometService
{
    public async Task<Result<int?>> InsertNewCometsAsync(IEnumerable<CometDto> comets)
    {
        var mappedComets = comets.Select(CometMapper.ToModel);
        var result = await cometRepository.ReplaceCometsAsync(mappedComets.ToList());
        // Do something with a result
        
        return result;
    }
}