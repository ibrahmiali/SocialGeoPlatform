using Microsoft.Extensions.Logging;
using SocialGeoPlatform.Core.Application.Services;
using SocialGeoPlatform.Core.Domain.Interfaces;
using SocialGeoPlatform.Infrastructure.Data;
using SocialGeoPlatform.Mobile.ViewModels;
using SocialGeoPlatform.Mobile.Views;
using Supabase;
namespace SocialGeoPlatform.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps() // إضافة لدعم الخرائط
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddHttpClient("ApiClient", client =>
            client.BaseAddress = new Uri("https://socialgeo-api-9dc5bf9d8da5.herokuapp.com/"));
            // تسجيل Supabase Client
            builder.Services.AddScoped(_ => new Supabase.Client(
                "https://izocvnoxizhbfxeuqzuy.supabase.co",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Iml6b2N2bm94aXpoYmZ4ZXVxenV5Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDM0OTM3NTIsImV4cCI6MjA1OTA2OTc1Mn0.2KNsK4DjxgjvlsGOZvWeyQvOFCzGBXzD4EMxAc8bTUI",
                new SupabaseOptions { AutoConnectRealtime = true }));

            // تسجيل IEventRepository مع SupabaseEventRepository
            builder.Services.AddScoped<IEventRepository, SupabaseEventRepository>();
            builder.Services.AddScoped<EventService>();
            builder.Services.AddScoped<EventService>();
            builder.Services.AddSingleton<MapViewModel>();
            builder.Services.AddSingleton<MapPage>();
            builder.Services.AddSingleton<App>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
