using System.Text.Json.Serialization;

namespace CometHandler.Models.DTO;

internal sealed record CometDto
{
    public string Name { get; init; }

    public string Id { get; init; }

    public string NameType { get; init; }

    [JsonPropertyName("recclass")] 
    public string RecordedClassification { get; init; }

    public string? Mass { get; init; }

    public string Fall { get; init; }

    public DateTimeOffset? Year { get; init; }

    [JsonPropertyName("reclat")] 
    public string? RecorderLatitude { get; init; }

    [JsonPropertyName("reclong")] 
    public string? RecorderLongitude { get; init; }

    public GeoLocationDto? Geolocation { get; init; }

    // Fields used for aggregation and search
    [JsonPropertyName(":@computed_region_cbhk_fwbd")]
    public string? ComputedRegionCbhkFwbd { get; init; }

    [JsonPropertyName(":@computed_region_nnqa_25f4")]
    public string? ComputedRegionNnqa25f4 { get; init; }
}

// This is a comment