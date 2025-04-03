using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SocialGeoPlatform.Core.Application.Services;
using SocialGeoPlatform.Core.Domain.ValueObjects;
using SocialGeoPlatform.Mobile.Models;
using System.Collections.ObjectModel;

namespace SocialGeoPlatform.Mobile.ViewModels;

public partial class MapViewModel : ObservableObject
{
    private readonly EventService _eventService;

    [ObservableProperty]
    private ObservableCollection<EventModel> _events = new();

    public MapViewModel(EventService eventService)
    {
        _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        LoadEventsAsync();
    }

    [RelayCommand]
    private async Task Refresh()
    {
        await LoadEventsAsync();
    }

    private async Task LoadEventsAsync()
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            }

            GeoPoint userLocation = location != null
                ? new GeoPoint(location.Longitude, location.Latitude)
                : new GeoPoint(46.6753, 24.7136);

            var domainEvents = await _eventService.GetNearbyEventsAsync(userLocation, 1000);
            Events = new ObservableCollection<EventModel>(domainEvents.Select(e => new EventModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Latitude = e.ToGeoPoint().Latitude,
                Longitude = e.ToGeoPoint().Longitude
            }));
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to get location: {ex.Message}", "OK");
        }
    }
}