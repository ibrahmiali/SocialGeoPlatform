using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.ValueObjects;

namespace SocialGeoPlatform.Core.Domain.Interfaces;

/// <summary>
/// Defines operations for accessing event data.
/// </summary>
public interface IEventRepository
{
    Task<List<Event>> GetNearbyEventsAsync(GeoPoint center, double radiusInMeters);
    Task<List<Event>> GetAllEventsAsync();
}
