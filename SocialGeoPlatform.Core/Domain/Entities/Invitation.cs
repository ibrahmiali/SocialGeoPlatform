using Postgrest.Attributes;
using Postgrest.Models;

namespace SocialGeoPlatform.Core.Domain.Entities;

public class Invitation : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("event_id")]
    public Guid EventId { get; set; }

    [Column("sender_id")]
    public Guid SenderId { get; set; }

    [Column("receiver_id")]
    public Guid ReceiverId { get; set; }

    [Column("status")]
    public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public Invitation()
    {
    }

    public Invitation(Guid eventId, Guid senderId, Guid receiverId)
    {
        Id = Guid.NewGuid();
        EventId = eventId;
        SenderId = senderId;
        ReceiverId = receiverId;
        CreatedAt = DateTime.UtcNow;
    }
}