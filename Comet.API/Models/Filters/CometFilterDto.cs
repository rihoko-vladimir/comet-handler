using System.ComponentModel.DataAnnotations;

namespace Comet.API.Models.Filters;

public sealed record CometFilterDto
{
    [Range(1, 9999)]
    public int? YearFrom { get; init; }

    [Range(1, 9999)]
    public int? YearTo { get; init; }

    public string? RecordedClassification { get; init; }

    public string? NameContains { get; init; }

    [Required]
    [RegularExpression("year|count|mass", ErrorMessage = "Sorting is available only by 'year', 'count' or 'mass'")]
    public string SortBy { get; set; } = "year";

    public bool SortDescending { get; set; } = false;
}