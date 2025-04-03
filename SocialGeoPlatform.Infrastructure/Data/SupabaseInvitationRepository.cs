using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.Interfaces;

namespace SocialGeoPlatform.Infrastructure.Data;

public class SupabaseInvitationRepository : IInvitationRepository
{
    private readonly Supabase.Client _supabase;

    public SupabaseInvitationRepository(Supabase.Client supabase)
    {
        _supabase = supabase ?? throw new ArgumentNullException(nameof(supabase));
    }

    public async Task<List<Invitation>> GetInvitationsByUserAsync(Guid userId)
    {
        var response = await _supabase.From<Invitation>().Where(x => x.ReceiverId == userId).Get();
        return response.Models;
    }

    public async Task<Invitation> GetInvitationByIdAsync(Guid id)
    {
        var response = await _supabase.From<Invitation>().Where(x => x.Id == id).Single();
        return response;
    }

    public async Task<Invitation> CreateInvitationAsync(Invitation invitation)
    {
        var response = await _supabase.From<Invitation>().Insert(invitation);
        return response.Models.First();
    }

    public async Task<Invitation> UpdateInvitationAsync(Invitation invitation)
    {
        var response = await _supabase.From<Invitation>().Update(invitation);
        return response.Models.First();
    }

    public async Task DeleteInvitationAsync(Guid id)
    {
        await _supabase.From<Invitation>().Where(x => x.Id == id).Delete();
    }
}