using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.Interfaces;

namespace SocialGeoPlatform.Core.Application.Services;

public class PlaceService
{
    private readonly IPlaceRepository _placeRepository;

    public PlaceService(IPlaceRepository placeRepository)
    {
        _placeRepository = placeRepository ?? throw new ArgumentNullException(nameof(placeRepository));
    }

    public async Task<List<Place>> GetAllPlacesAsync() => await _placeRepository.GetAllPlacesAsync();
    public async Task<Place> GetPlaceByIdAsync(Guid id) => await _placeRepository.GetPlaceByIdAsync(id);
    public async Task<Place> CreatePlaceAsync(Place place) => await _placeRepository.CreatePlaceAsync(place);
    public async Task<Place> UpdatePlaceAsync(Place place) => await _placeRepository.UpdatePlaceAsync(place);
    public async Task DeletePlaceAsync(Guid id) => await _placeRepository.DeletePlaceAsync(id);
}
