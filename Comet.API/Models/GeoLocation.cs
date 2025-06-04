namespace CometHandler.Models;

internal sealed record GeoLocation
{
    public string Type { get; init; }

    public List<double> Coordinates { get; init; }
}