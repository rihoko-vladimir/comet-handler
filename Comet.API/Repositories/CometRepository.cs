using Comet.API.Interfaces.Repositories;
using Comet.API.Models.Context;
using Comet.API.Models.Filters;
using Comet.API.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace Comet.API.Repositories;

internal sealed class CometRepository: ICometRepository
{
    private readonly ApplicationContext _context;

    public CometRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<CometGroupedResponse>> GetFilteredGroupedAsync(CometFilterDto filter)
    {
        // Everything here may be wrapped up to handle errors + result pattern
        var query = _context.Comets.AsNoTracking().AsQueryable();

        if (filter.YearFrom is not null)
            query = query.Where(c => c.Year.HasValue && c.Year.Value.Year >= filter.YearFrom.Value);

        if (filter.YearTo is not null)
            query = query.Where(c => c.Year.HasValue && c.Year.Value.Year <= filter.YearTo.Value);

        if (!string.IsNullOrWhiteSpace(filter.RecordedClassification))
            query = query.Where(c => c.RecordedClassification == filter.RecordedClassification);

        if (!string.IsNullOrWhiteSpace(filter.NameContains))
            query = query.Where(c => c.Name.Contains(filter.NameContains));

        var grouped = await query
            .GroupBy(c => c.Year.HasValue ? c.Year.Value.Year : (int?)null)
            .Select(g => new CometGroupedResponse
            {
                Year = g.Key,
                Count = g.Count(),
                TotalMass = g.Sum(x => x.Mass ?? 0),
                Names = g.Select(x => x.Name).ToList()
            })
            .ToListAsync();

        return filter.SortBy switch
        {
            "count" => filter.SortDescending ? grouped.OrderByDescending(g => g.Count).ToList() : grouped.OrderBy(g => g.Count).ToList(),
            "mass"  => filter.SortDescending ? grouped.OrderByDescending(g => g.TotalMass).ToList() : grouped.OrderBy(g => g.TotalMass).ToList(),
            _       => filter.SortDescending ? grouped.OrderByDescending(g => g.Year).ToList() : grouped.OrderBy(g => g.Year).ToList()
        };
    }
}