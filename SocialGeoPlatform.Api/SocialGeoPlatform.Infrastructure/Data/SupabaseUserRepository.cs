using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.Interfaces;

namespace SocialGeoPlatform.Infrastructure.Data;

public class SupabaseUserRepository : IUserRepository
{
    private readonly Supabase.Client _supabase;

    public SupabaseUserRepository(Supabase.Client supabase)
    {
        _supabase = supabase ?? throw new ArgumentNullException(nameof(supabase));
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var response = await _supabase.From<User>().Get();
        return response.Models;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var response = await _supabase.From<User>().Where(x => x.Id == id).Single();
        return response;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var response = await _supabase.From<User>().Insert(user);
        return response.Models.First();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var response = await _supabase.From<User>().Update(user);
        return response.Models.First();
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _supabase.From<User>().Where(x => x.Id == id).Delete();
    }
}