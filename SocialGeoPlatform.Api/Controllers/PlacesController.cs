using Microsoft.AspNetCore.Mvc;
using SocialGeoPlatform.Core.Application.DTOs;
using SocialGeoPlatform.Core.Application.Mappers;
using SocialGeoPlatform.Core.Application.Services;
using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.ValueObjects;

namespace SocialGeoPlatform.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlacesController : ControllerBase
{
    private readonly PlaceService _placeService;

    public PlacesController(PlaceService placeService)
    {
        _placeService = placeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var places = await _placeService.GetAllPlacesAsync();
        var placeDtos = places.Select(PlaceMapper.ToDto).ToList();
        return Ok(placeDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var place = await _placeService.GetPlaceByIdAsync(id);
        if (place == null) return NotFound();
        return Ok(PlaceMapper.ToDto(place));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlaceDto placeDto)
    {
        var place = new Place(placeDto.Name, placeDto.Description, new GeoPoint(placeDto.Longitude, placeDto.Latitude));
        var createdPlace = await _placeService.CreatePlaceAsync(place);
        return CreatedAtAction(nameof(GetById), new { id = createdPlace.Id }, PlaceMapper.ToDto(createdPlace));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PlaceDto placeDto)
    {
        var place = await _placeService.GetPlaceByIdAsync(id);
        if (place == null) return NotFound();
        place.Name = placeDto.Name;
        place.Description = placeDto.Description;
        place.Location = $"POINT({placeDto.Longitude} {placeDto.Latitude})";
        var updatedPlace = await _placeService.UpdatePlaceAsync(place);
        return Ok(PlaceMapper.ToDto(updatedPlace));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var place = await _placeService.GetPlaceByIdAsync(id);
        if (place == null) return NotFound();
        await _placeService.DeletePlaceAsync(id);
        return NoContent();
    }
}