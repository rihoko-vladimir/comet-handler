namespace CometHandler.Models.DTO;

internal sealed record GeoLocationDto
{
    public string Type { get; init; }

    public List<double> Coordinates { get; init; }
}