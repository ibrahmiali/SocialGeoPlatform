using SocialGeoPlatform.Core.Application.DTOs;
using SocialGeoPlatform.Core.Domain.Entities;

namespace SocialGeoPlatform.Core.Application.Mappers;

public static class PlaceMapper
{
    public static PlaceDto ToDto(Place place)
    {
        var geoPoint = place.ToGeoPoint();
        return new PlaceDto
        {
            Id = place.Id,
            Name = place.Name,
            Description = place.Description,
            Latitude = geoPoint.Latitude,
            Longitude = geoPoint.Longitude,
            CreatedAt = place.CreatedAt
        };
    }
}