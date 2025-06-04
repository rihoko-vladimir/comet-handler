namespace CometHandler.Configurations;

internal sealed record AppConfiguration
{
    public const string Key = "Application";

    public string ApiService { get; init; }

    public string Endpoint { get; init; }

    public IEnumerable<JobConfiguration> Jobs { get; init; }
}