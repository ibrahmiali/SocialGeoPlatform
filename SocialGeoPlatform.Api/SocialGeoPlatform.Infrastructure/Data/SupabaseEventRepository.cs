using Supabase;
using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.Interfaces;
using SocialGeoPlatform.Core.Domain.ValueObjects;
using System.Net.Http.Json;

namespace SocialGeoPlatform.Infrastructure.Data;

public class SupabaseEventRepository : IEventRepository
{
    private readonly Supabase.Client _supabase;
    private readonly HttpClient _httpClient;

    public SupabaseEventRepository(Supabase.Client supabase, HttpClient httpClient)
    {
        _supabase = supabase ?? throw new ArgumentNullException(nameof(supabase));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.BaseAddress = new Uri("https://izocvnoxizhbfxeuqzuy.supabase.co/rest/v1/"); // استبدل بـ URL الخاص بك
        _httpClient.DefaultRequestHeaders.Add("apikey", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Iml6b2N2bm94aXpoYmZ4ZXVxenV5Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDM0OTM3NTIsImV4cCI6MjA1OTA2OTc1Mn0.2KNsK4DjxgjvlsGOZvWeyQvOFCzGBXzD4EMxAc8bTUI"); // استبدل بـ API Key
    }

    public async Task<List<Event>> GetNearbyEventsAsync(GeoPoint center, double radiusInMeters)
    {
        var payload = new
        {
            point = $"POINT({center.Longitude} {center.Latitude})",
            radius = radiusInMeters
        };
        var response = await _httpClient.PostAsJsonAsync("rpc/get_nearby_events", payload);
        response.EnsureSuccessStatusCode();
        var events = await response.Content.ReadFromJsonAsync<List<Event>>();
        return events ?? new List<Event>();
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        var response = await _supabase.From<Event>().Get();
        return response.Models.ToList();
    }
}
