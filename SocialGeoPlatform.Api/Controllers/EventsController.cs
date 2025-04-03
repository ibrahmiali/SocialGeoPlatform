using Microsoft.AspNetCore.Mvc;
using SocialGeoPlatform.Core.Application.Services;
using SocialGeoPlatform.Core.Application.Mappers;
using SocialGeoPlatform.Core.Application.DTOs;
using SocialGeoPlatform.Core.Domain.ValueObjects;

namespace SocialGeoPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService)
    {
        _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
    }

    [HttpGet("nearby")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EventDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetNearbyEvents(double latitude, double longitude, double radiusInMeters = 1000)
    {
        try
        {
            var center = GeoPoint.Create(longitude, latitude);
            var events = await _eventService.GetNearbyEventsAsync(center, radiusInMeters);
            return Ok(events.Select(EventMapper.ToDto));
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("admin/events")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EventDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllEvents()
    {
        try
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events.Select(EventMapper.ToDto));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}