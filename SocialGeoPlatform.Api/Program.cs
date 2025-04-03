using SocialGeoPlatform.Core.Application.Services;
using SocialGeoPlatform.Core.Domain.Interfaces;
using SocialGeoPlatform.Infrastructure.Data;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddHttpClient(); // ????? HttpClient
builder.Services.AddScoped(sp => new Supabase.Client(
    builder.Configuration["Supabase:Url"]!,
    builder.Configuration["Supabase:Key"],
    new SupabaseOptions { AutoConnectRealtime = true }));
builder.Services.AddScoped<IEventRepository, SupabaseEventRepository>();
builder.Services.AddScoped<IUserRepository, SupabaseUserRepository>();
builder.Services.AddScoped<IPlaceRepository, SupabasePlaceRepository>();
builder.Services.AddScoped<IInvitationRepository, SupabaseInvitationRepository>();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PlaceService>();
builder.Services.AddScoped<InvitationService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run($"http://*:{Environment.GetEnvironmentVariable("PORT") ?? "5000"}");