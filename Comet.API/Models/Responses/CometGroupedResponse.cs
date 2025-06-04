namespace Comet.API.Models.Responses;

public sealed record CometGroupedResponse
{
    public int? Year { get; init; }
    public int Count { get; init; }
    public double TotalMass { get; init; }
    public List<string> Names { get; init; }
}