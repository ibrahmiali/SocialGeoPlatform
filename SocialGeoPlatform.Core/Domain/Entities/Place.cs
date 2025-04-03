using Postgrest.Attributes;
using Postgrest.Models;
using SocialGeoPlatform.Core.Domain.ValueObjects;

namespace SocialGeoPlatform.Core.Domain.Entities;

public class Place : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    [Column("location")]
    public string Location { get; set; } = "POINT(0 0)";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public Place()
    {
    }

    public Place(string name, string description, GeoPoint location)
    {
        Id = Guid.NewGuid();
        Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
        Description = description;
        Location = $"POINT({location.Longitude} {location.Latitude})";
        CreatedAt = DateTime.UtcNow;
    }

    public GeoPoint ToGeoPoint()
    {
        if (Location.StartsWith("POINT"))
        {
            var coords = Location.Replace("POINT(", "").Replace(")", "").Split(' ');
            return GeoPoint.Create(double.Parse(coords[0]), double.Parse(coords[1]));
        }
        throw new FormatException($"Invalid location format: {Location}");
    }
}