using SocialGeoPlatform.Core.Application.DTOs;
using SocialGeoPlatform.Core.Domain.Entities;

namespace SocialGeoPlatform.Core.Application.Mappers;

public static class EventMapper
{
    public static EventDto ToDto(Event entity)
    {
        var geoPoint = entity.ToGeoPoint(); // تحويل Location من string إلى GeoPoint
        return new EventDto(entity.Id, entity.Title, entity.Description!,
            $"POINT({geoPoint.Longitude} {geoPoint.Latitude})");
    }
}
