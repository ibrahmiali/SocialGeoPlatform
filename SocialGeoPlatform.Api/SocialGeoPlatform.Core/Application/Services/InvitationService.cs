using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.Interfaces;

namespace SocialGeoPlatform.Core.Application.Services;

public class InvitationService
{
    private readonly IInvitationRepository _invitationRepository;

    public InvitationService(IInvitationRepository invitationRepository)
    {
        _invitationRepository = invitationRepository ?? throw new ArgumentNullException(nameof(invitationRepository));
    }

    public async Task<List<Invitation>> GetInvitationsByUserAsync(Guid userId) => await _invitationRepository.GetInvitationsByUserAsync(userId);
    public async Task<Invitation> GetInvitationByIdAsync(Guid id) => await _invitationRepository.GetInvitationByIdAsync(id);
    public async Task<Invitation> CreateInvitationAsync(Invitation invitation) => await _invitationRepository.CreateInvitationAsync(invitation);
    public async Task<Invitation> UpdateInvitationAsync(Invitation invitation) => await _invitationRepository.UpdateInvitationAsync(invitation);
    public async Task DeleteInvitationAsync(Guid id) => await _invitationRepository.DeleteInvitationAsync(id);
}