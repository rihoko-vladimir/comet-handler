using CometHandler.Models;
using CometHandler.Models.DTO;

namespace CometHandler.Mapping;

internal static class CometMapper
{
    public static Comet ToModel(CometDto dto)
    {
        return new Comet
        {
            Name = dto.Name,
            Id = dto.Id,
            NameType = dto.NameType,
            RecordedClassification = dto.RecordedClassification,
            Mass = ParseMass(dto.Mass),
            Fall = dto.Fall,
            Year = dto.Year,
            RecorderLatitude = dto.RecorderLatitude,
            RecorderLongitude = dto.RecorderLongitude,
            Geolocation = dto.Geolocation is not null
                ? new GeoLocation
                {
                    Type = dto.Geolocation.Type,
                    Coordinates = dto.Geolocation.Coordinates
                }
                : null,
            ComputedRegionCbhkFwbd = dto.ComputedRegionCbhkFwbd,
            ComputedRegionNnqa25f4 = dto.ComputedRegionNnqa25f4
        };
    }

    private static int? ParseMass(string? massRaw)
    {
        if (string.IsNullOrWhiteSpace(massRaw)) return null;

        return int.TryParse(massRaw, out var mass) ? mass : null;
    }
}