using SocialGeoPlatform.Core.Domain.Entities;

namespace SocialGeoPlatform.Core.Domain.Interfaces;

public interface IPlaceRepository
{
    Task<List<Place>> GetAllPlacesAsync();
    Task<Place> GetPlaceByIdAsync(Guid id);
    Task<Place> CreatePlaceAsync(Place place);
    Task<Place> UpdatePlaceAsync(Place place);
    Task DeletePlaceAsync(Guid id);
}