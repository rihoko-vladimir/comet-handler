namespace Comet.API.Models;

public sealed record Comet
{
    public string Name { get; init; }

    public string Id { get; init; }

    public string NameType { get; init; }

    public string RecordedClassification { get; init; }

    public int? Mass { get; init; }

    public string Fall { get; init; }

    public DateTimeOffset? Year { get; init; }

    public string? RecorderLatitude { get; init; }

    public string? RecorderLongitude { get; init; }

    public GeoLocation? Geolocation { get; init; }

    public string? ComputedRegionCbhkFwbd { get; init; }
    
    public string? ComputedRegionNnqa25f4 { get; init; }
}