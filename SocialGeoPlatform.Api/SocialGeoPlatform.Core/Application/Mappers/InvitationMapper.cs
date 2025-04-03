using SocialGeoPlatform.Core.Application.DTOs;
using SocialGeoPlatform.Core.Domain.Entities;

namespace SocialGeoPlatform.Core.Application.Mappers;

public static class InvitationMapper
{
    public static InvitationDto ToDto(Invitation invitation)
    {
        return new InvitationDto
        {
            Id = invitation.Id,
            EventId = invitation.EventId,
            SenderId = invitation.SenderId,
            ReceiverId = invitation.ReceiverId,
            Status = invitation.Status,
            CreatedAt = invitation.CreatedAt
        };
    }
}