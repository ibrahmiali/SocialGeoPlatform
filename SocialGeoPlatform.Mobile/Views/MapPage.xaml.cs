using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using SocialGeoPlatform.Mobile.ViewModels;

namespace SocialGeoPlatform.Mobile.Views;

public partial class MapPage : ContentPage
{
    private readonly MapViewModel _viewModel;

    public MapPage(MapViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
        SetupMap();
        BindEvents();
    }

    private async void SetupMap()
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            }

            if (location != null)
            {
                MapView.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Location(location.Latitude, location.Longitude), Distance.FromKilometers(1)));
            }
            else
            {
                MapView.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Location(24.7136, 46.6753), Distance.FromKilometers(1)));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to get location: {ex.Message}", "OK");
            MapView.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Location(24.7136, 46.6753), Distance.FromKilometers(1)));
        }
    }

    private void BindEvents()
    {
        _viewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(_viewModel.Events))
            {
                MapView.Pins.Clear();
                foreach (var evt in _viewModel.Events)
                {
                    var pin = new Pin
                    {
                        Label = evt.Title,
                        Address = evt.Description,
                        Location = new Location(evt.Latitude, evt.Longitude)
                    };
                    pin.MarkerClicked += async (s, args) =>
                    {
                        args.HideInfoWindow = false;
                        await DisplayAlert(evt.Title, evt.Description, "OK");
                    };
                    MapView.Pins.Add(pin);
                }
            }
        };
    }
}