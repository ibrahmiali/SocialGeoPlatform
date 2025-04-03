using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.Interfaces;

namespace SocialGeoPlatform.Infrastructure.Data;

public class SupabasePlaceRepository : IPlaceRepository
{
    private readonly Supabase.Client _supabase;

    public SupabasePlaceRepository(Supabase.Client supabase)
    {
        _supabase = supabase ?? throw new ArgumentNullException(nameof(supabase));
    }

    public async Task<List<Place>> GetAllPlacesAsync()
    {
        var response = await _supabase.From<Place>().Get();
        return response.Models;
    }

    public async Task<Place> GetPlaceByIdAsync(Guid id)
    {
        var response = await _supabase.From<Place>().Where(x => x.Id == id).Single();
        return response;
    }

    public async Task<Place> CreatePlaceAsync(Place place)
    {
        var response = await _supabase.From<Place>().Insert(place);
        return response.Models.First();
    }

    public async Task<Place> UpdatePlaceAsync(Place place)
    {
        var response = await _supabase.From<Place>().Update(place);
        return response.Models.First();
    }

    public async Task DeletePlaceAsync(Guid id)
    {
        await _supabase.From<Place>().Where(x => x.Id == id).Delete();
    }
}