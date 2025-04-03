using SocialGeoPlatform.Core.Domain.Entities;

namespace SocialGeoPlatform.Core.Domain.Interfaces;

public interface IInvitationRepository
{
    Task<List<Invitation>> GetInvitationsByUserAsync(Guid userId);
    Task<Invitation> GetInvitationByIdAsync(Guid id);
    Task<Invitation> CreateInvitationAsync(Invitation invitation);
    Task<Invitation> UpdateInvitationAsync(Invitation invitation);
    Task DeleteInvitationAsync(Guid id);
}