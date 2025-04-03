namespace SocialGeoPlatform.Core.Application.DTOs;

public class InvitationDto
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
}