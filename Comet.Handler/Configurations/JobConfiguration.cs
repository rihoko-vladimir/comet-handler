namespace CometHandler.Configurations;

internal sealed record JobConfiguration
{
    public const string Key = "Jobs";

    public string Name { get; init; }

    public string Schedule { get; init; }
}