using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.Interfaces;
using SocialGeoPlatform.Core.Domain.ValueObjects;

namespace SocialGeoPlatform.Core.Application.Services;

public class EventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    public async Task<List<Event>> GetNearbyEventsAsync(GeoPoint center, double radiusInMeters)
    {
        return await _eventRepository.GetNearbyEventsAsync(center, radiusInMeters);
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _eventRepository.GetAllEventsAsync();
    }
}