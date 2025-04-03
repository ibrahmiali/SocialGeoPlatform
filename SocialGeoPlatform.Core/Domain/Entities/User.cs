using Postgrest.Attributes;
using Postgrest.Models;

namespace SocialGeoPlatform.Core.Domain.Entities;

public class User : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("username")]
    public string Username { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public User()
    {
    }

    public User(string username, string email)
    {
        Id = Guid.NewGuid();
        Username = string.IsNullOrEmpty(username) ? throw new ArgumentNullException(nameof(username)) : username;
        Email = string.IsNullOrEmpty(email) ? throw new ArgumentNullException(nameof(email)) : email;
        CreatedAt = DateTime.UtcNow;
    }
}