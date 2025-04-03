namespace SocialGeoPlatform.Core.Domain.ValueObjects;

public record GeoPoint(double Longitude, double Latitude)
{
    public static GeoPoint Create(double longitude, double latitude)
    {
        if (longitude < -180 || longitude > 180) throw new ArgumentOutOfRangeException(nameof(longitude));
        if (latitude < -90 || latitude > 90) throw new ArgumentOutOfRangeException(nameof(latitude));
        return new GeoPoint(longitude, latitude);
    }
}