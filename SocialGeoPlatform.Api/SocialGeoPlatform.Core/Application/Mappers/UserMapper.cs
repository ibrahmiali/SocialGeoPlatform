using SocialGeoPlatform.Core.Application.DTOs;
using SocialGeoPlatform.Core.Domain.Entities;

namespace SocialGeoPlatform.Core.Application.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
}