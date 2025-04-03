using Postgrest.Attributes;
using Postgrest.Models;
using SocialGeoPlatform.Core.Domain.ValueObjects;

namespace SocialGeoPlatform.Core.Domain.Entities;

[Table("events")]
public class Event : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("title")]
    public string Title { get; set; } = string.Empty; // قيمة افتراضية لتجنب null

    [Column("description")]
    public string? Description { get; set; } // nullable لأنه اختياري

    [Column("location")]
    public string Location { get; set; } = "POINT(0 0)"; // قيمة افتراضية

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public Event()
    {
        // مُنشئ افتراضي مطلوب لـ Supabase
    }

    public Event(string title, string description, GeoPoint location)
    {
        Id = Guid.NewGuid();
        Title = string.IsNullOrEmpty(title) ? throw new ArgumentNullException(nameof(title)) : title;
        Description = description;
        Location = $"POINT({location.Longitude} {location.Latitude})";
        CreatedAt = DateTime.UtcNow;
    }

    public GeoPoint ToGeoPoint()
    {
        var coords = Location.Replace("POINT(", "").Replace(")", "").Split(' ');
        return GeoPoint.Create(double.Parse(coords[0]), double.Parse(coords[1]));
    }
}
